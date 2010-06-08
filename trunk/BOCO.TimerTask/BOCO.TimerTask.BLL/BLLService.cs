// File:    BLLService.cs
// Author:  LvJinMing
// Created: 2010年6月4日 15:26:17
// Purpose: Definition of Enum TaskRuningState

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;
using BOCO.TimerTask.Utility;
using BOCO.TimerTask.DAL;

namespace BOCO.TimerTask.BLL
{
    /// <summary>
    /// 外部接口实现类
    /// </summary>
    internal class BLLService : IBLLLogic
    {
        private const string TIMERMANAGER_PROCESSNAME = "BOCO.TimerTask.TaskManager";

        private DAL.IDataAccess _DataAccess = DAL.DALFactory.GetDataAccess();

        #region private function
        private List<TaskAssembly> GetRegestedApps()
        {
            return RegestAppCfgHelper.GetAllApps();
        }

        /// <summary>
        /// 给服务器发送消息
        /// </summary>
        /// <param name="paraContent"></param>
        private void SendXMLSocket2Server(string paraContent)
        {
            Socket socket = null; ;
            try
            {
                IPEndPoint ip = SocketHelper.GetIpEndPoint();
                socket = SocketHelper.GetSocket(ip);

                SocketHelper.Send(socket, paraContent);
            }
            catch (Exception ex)
            {
                LogEntity log = new LogEntity();
                log.LogContent = ex.Message;
                log.LogType = LogType.SocketClientSendError;
                log.TaskID = -1;
                WriteLog(log);
            }
            finally
            {
                SocketHelper.CloseSocket(socket);
            }
        }

        #endregion

        #region IBLLService 成员

        public TaskEntity AddTask(TaskEntity paraEntity)
        {
            TaskAssembly assembly = RegestAppCfgHelper.GetRegestedApp(paraEntity.RegestesAppName);
            if (assembly == null)
            {
                return null;
            }
            string paraStr = assembly.AssemblyType == AssemblyType.Dll ? paraEntity.ExtraParaStr : paraEntity.ExeCommandParaMeter;

            return AddTask(paraEntity.Name,
                paraEntity.DateStart,
                paraEntity.DateEnd,
                paraEntity.RegestesAppName,
                paraEntity.RunSpaceTime,
                paraEntity.RunSpaceType,
                paraStr,
                paraEntity.RunTimeOutSecs);
        }

        public TaskEntity AddTask(string paraName, DateTime paraDateStart, DateTime paraDateEnd, string paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, string paraExtraStr, Int64 paraRunTimeOutSecs)
        {
            TaskAssembly assembly = RegestAppCfgHelper.GetRegestedApp(paraAppName);
            if (assembly == null)
            {
                return null;
            }
            else
            {
                TaskEntity entity = new TaskEntity();
                entity.DateEnd = paraDateEnd;
                entity.DateStart = paraDateStart;
                entity.Enable = true;
                entity.ExeCommandParaMeter = assembly.AssemblyType == AssemblyType.Exe ? paraExtraStr : string.Empty;
                entity.ExtraParaStr = assembly.AssemblyType == AssemblyType.Dll ? paraExtraStr : string.Empty;
                entity.Name = paraName;
                entity.RunSpaceTime = paraRunSpaceTimeSecs;
                entity.RunSpaceType = paraRunSpaceType;
                entity.RunTimeOutSecs = paraRunTimeOutSecs;
                entity.RegestesAppName = paraAppName;
                //保存到数据库
                Int64 id = _DataAccess.AddTask(entity);
                entity.SetKeyID(id);
                //输入校验
                MessageParser.CheckAndSetTaskFrequence(ref entity);
                //发送消息同步到任务管理器中
                string message = MessageParser.BuildMessage(new List<TaskEntity>() { entity }, null, null, null, null, null);
                this.SendXMLSocket2Server(message);
                return entity;
            }
        }

        public bool DelTask(Int64 paraID)
        {
            //发送消息同步到任务管理器中
            string message = MessageParser.BuildMessage(null, new List<long>() { paraID }, null, null, null, null);
            this.SendXMLSocket2Server(message);
            return _DataAccess.RemoveTask(paraID);
        }

        public bool UpdateTask(TaskEntity paraEntity)
        {
            TaskAssembly assembly = RegestAppCfgHelper.GetRegestedApp(paraEntity.RegestesAppName);
            if (assembly == null)
            {
                return false;
            }
            string paraStr = assembly.AssemblyType == AssemblyType.Dll ? paraEntity.ExtraParaStr : paraEntity.ExeCommandParaMeter;

            return UpdateTask(
                paraEntity.ID,
                paraEntity.Name,
                paraEntity.DateStart,
                paraEntity.DateEnd,
                paraEntity.RegestesAppName,
                paraEntity.RunSpaceTime,
                paraEntity.RunSpaceType,
                paraStr,
                paraEntity.RunTimeOutSecs);
        }

        public bool UpdateTask(Int64 paraTaskID, string paraName, DateTime paraDateStart, DateTime paraDateEnd, string paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, string paraExtraStr, Int64 paraRunTimeOutSecs)
        {
            TaskAssembly assembly = RegestAppCfgHelper.GetRegestedApp(paraAppName);
            if (assembly == null)
            {
                return false;
            }
            else
            {
                TaskEntity entity = new TaskEntity();
                entity.DateEnd = paraDateEnd;
                entity.DateStart = paraDateStart;
                entity.Enable = true;
                entity.ExeCommandParaMeter = assembly.AssemblyType == AssemblyType.Exe ? paraExtraStr : string.Empty;
                entity.ExtraParaStr = assembly.AssemblyType == AssemblyType.Dll ? paraExtraStr : string.Empty;
                entity.Name = paraName;
                entity.RunSpaceTime = paraRunSpaceTimeSecs;
                entity.RunSpaceType = paraRunSpaceType;
                entity.RunTimeOutSecs = paraRunTimeOutSecs;
                entity.SetKeyID(paraTaskID);
                entity.RegestesAppName = paraAppName;
                _DataAccess.ModifyTask(paraTaskID, entity);


                //发送消息同步到任务管理器中
                string message = MessageParser.BuildMessage(null, null, new List<TaskEntity>() { entity }, null, null, null);
                this.SendXMLSocket2Server(message);

                return true;
            }
        }

        public List<TaskEntity> GetTaskListByApp(string paraAppName)
        {
            return _DataAccess.GetTasks(paraAppName);
        }

        public List<TaskEntity> GetTaskEntityList()
        {
            return _DataAccess.GetTasks();
        }

        public void WriteLog(LogEntity paraLogEntity)
        {
            try
            {
                _DataAccess.WriteLog(paraLogEntity);
            }
            catch
            {
                Console.WriteLine("WriteLog Error， Please Call Administrator To Check");
            }
        }

        public DataTable GetTaskLogByDate(DateTime paraDateStart, DateTime paraDateEnd)
        {
            return _DataAccess.GetLog(paraDateStart, paraDateEnd);
        }

        public DataTable GetTaskLogByTask(Int64 paraTaskId)
        {
            return _DataAccess.GetLog(paraTaskId);
        }

        public DataTable GetTaskLogByApp(string paraRegestedAppName)
        {
            return _DataAccess.GetLog(paraRegestedAppName);
        }

        public bool IsTaskManagerAlive()
        {
            Process[] arr = Process.GetProcessesByName(TIMERMANAGER_PROCESSNAME);
            if (arr.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetRegestedApp()
        {
            List<string> sc = new List<string>();
            foreach (TaskAssembly ass in GetRegestedApps())
            {
                sc.Add(ass.UserName);
            }
            return sc;
        }

        public TaskRuningState GetTaskRunState(Int64 paraTaskId)
        {
            throw new NotImplementedException();
        }

        public void StopRuningTask(Int64 paraTaskId)
        {
            string message = MessageParser.BuildMessage(null, null, null, null, null,
                new List<long>() { paraTaskId });
            this.SendXMLSocket2Server(message);
        }

        public void RunTaskImmediate(Int64 paraTaskID, bool isDisTurbBackTask)
        {
            RunTaskType runType = RunTaskType.ImmediateDisturb;
            if (isDisTurbBackTask == false)
            {
                runType = RunTaskType.ImmediateNoDisturb;
            }
            string message = MessageParser.BuildMessage(null, null, null,
                new List<long>() { paraTaskID }, new List<RunTaskType>() { runType }, null);
            this.SendXMLSocket2Server(message);
        }

        public bool StartTaskManager()
        {
            //Process[] arr = Process.GetProcessesByName("BOCO.TimerTask.TaskManager");
            //if (arr.Length > 0)
            //{
            //    LogEntity log = new LogEntity();
            //    log.LogContent = "已经有进程启动，无法再次启动进程。";
            //    log.LogDate = DateTime.Now;
            //    log.LogType = TaskLogType.TaskManagerStartError;
            //    log.TaskID = -1;
            //    WriteLog(log);
            //    return false;
            //}
            //else
            //{
            string path = AssemblyHelper.GetAssemblyPath() + TIMERMANAGER_PROCESSNAME + ".exe";
            if (File.Exists(path))
            {
                Process.Start(path);
                return true;
            }
            else
            {
                LogEntity log = new LogEntity();
                log.LogContent = "当前目录下不存在定时任务管理器程序（BOCO.TimerTask.TaskManager.exe）";
                log.LogType = LogType.TaskManagerStartError;
                log.TaskID = -1;
                WriteLog(log);
                return false;
            }
            //}
        }



        public List<Task> GetTaskList()
        {
            List<TaskEntity> entitylist = this.GetTaskEntityList().FindAll(delegate(TaskEntity entity) { return entity.Enable == true; });
            List<TaskAssembly> assList = this.GetRegestedApps();

            List<Task> list = new List<Task>();
            foreach (TaskEntity t in entitylist)
            {
                TaskAssembly assembly = assList.Find(delegate(TaskAssembly a) { return a.UserName == t.RegestesAppName; });
                if (assembly != null)
                {
                    Task task = new Task(t, assembly);
                    list.Add(task);
                }
            }
            return list;
        }

        #endregion


        #region IBLLLogic 成员


        public DateTime GetTaskLastRunTime(long paraTaskID)
        {
            LogEntity log = _DataAccess.GetLog_LatestRun(paraTaskID, LogType.TaskRunStart);
            if (log != null)
            {
                return log.LogDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region IBLLLogic 成员


        public Task GetTask(TaskEntity paraEntity)
        {
            List<TaskAssembly> assList = this.GetRegestedApps();

            List<Task> list = new List<Task>();
            TaskAssembly assembly = assList.Find(delegate(TaskAssembly a) { return a.UserName == paraEntity.RegestesAppName; });
            if (assembly != null)
            {
                Task task = new Task(paraEntity, assembly);
                return task;
            }
            return null;
        }

        #endregion

        #region IBLLLogic 成员


        public void WriteLog(long paraTaskid, string paraTaskName, string paraContent, LogType paraLogType)
        {
            _DataAccess.WriteLog(paraTaskid, paraTaskName, paraContent, paraLogType);
        }

        #endregion

        #region IBLLLogic 成员


        public void AddTask2DB(TaskEntity paraTask)
        {
            Int64 id = _DataAccess.AddTask(paraTask);
            paraTask.SetKeyID(id);
        }

        public void UpdateTask2DB(TaskEntity paraTask)
        {
            _DataAccess.ModifyTask(paraTask.ID, paraTask);
        }

        public void DeleteTask2DB(long paraTaskID)
        {
            _DataAccess.RemoveTask(paraTaskID);
        }

        #endregion
    }
}
