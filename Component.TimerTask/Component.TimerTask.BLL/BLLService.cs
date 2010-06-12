﻿// File:    BLLService.cs
// Author:  LvJinMing
// Created: 2010年6月4日 15:26:17
// Purpose: Class

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Component.TimerTask.DAL;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Utility;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 外部接口实现类
    /// </summary>
    internal class BLLService : IBLLLogic
    {
        private const string TIMERMANAGER_PROCESSNAME = "Component.TimerTask.TaskManager";

        private DAL.IDataAccess _DataAccess = DAL.DALFactory.GetDataAccess();

        #region private function
        /// <summary>
        /// Gets the regested apps.
        /// </summary>
        /// <returns></returns>
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
                if(socket != null)
                    SocketHelper.CloseSocket(socket);
            }
        }

        #endregion

        #region IBLLService 成员

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="paraEntity">The para entity.</param>
        /// <returns></returns>
        public TaskEntity AddTask(TaskEntity paraEntity)
        {
            TaskAssembly assembly = RegestAppCfgHelper.GetRegestedApp(paraEntity.RegestesAppName);
            if (assembly == null)
            {
                return null;
            }
            return AddTask(paraEntity.Name,
                paraEntity.DateStart,
                paraEntity.DateEnd,
                paraEntity.RegestesAppName,
                paraEntity.RunSpaceTime,
                paraEntity.RunSpaceType,
                paraEntity.ExtraParaStr,
                paraEntity.RunTimeOutSecs);
        }

        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="paraDateStart"></param>
        /// <param name="paraDateEnd"></param>
        /// <param name="paraAppName"></param>
        /// <param name="paraRunSpaceTimeSecs">周期数（秒）</param>
        /// <param name="paraRunSpaceType">周期类型（便于存储和下次查看）</param>
        /// <param name="paraExtraStr"></param>
        /// <param name="paraRunTimeOutSecs">执行超时时间，如果不限定，则给-1，如果限定了，在指定时间内未执行完成，则强制结束（exe直接结束进程，dll通过接口通知结束）</param>
        /// <returns></returns>
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
                entity.ExtraParaStr = paraExtraStr;
                entity.Name = paraName;
                entity.RunSpaceTime = paraRunSpaceTimeSecs;
                entity.RunSpaceType = paraRunSpaceType;
                entity.RunTimeOutSecs = paraRunTimeOutSecs;
                entity.RegestesAppName = paraAppName;

                //先校验，后保存

                //输入校验
                MessageParser.CheckAndSetTaskFrequence(ref entity);
                //保存到数据库
                Int64 id = _DataAccess.AddTask(entity);
                entity.SetKeyID(id);
                
                //发送消息同步到任务管理器中
                string message = MessageParser.BuildMessage(new List<TaskEntity>() { entity }, null, null, null, null, null);
                this.SendXMLSocket2Server(message);
                return entity;
            }
        }

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="paraID"></param>
        /// <returns></returns>
        public bool DelTask(Int64 paraID)
        {
            //发送消息同步到任务管理器中
            string message = MessageParser.BuildMessage(null, new List<long>() { paraID }, null, null, null, null);
            this.SendXMLSocket2Server(message);
            return _DataAccess.RemoveTask(paraID);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="paraEntity">The para entity.</param>
        /// <returns></returns>
        public bool UpdateTask(TaskEntity paraEntity)
        {
            TaskAssembly assembly = RegestAppCfgHelper.GetRegestedApp(paraEntity.RegestesAppName);
            if (assembly == null)
            {
                return false;
            }

            return UpdateTask(
                paraEntity.ID,
                paraEntity.Name,
                paraEntity.DateStart,
                paraEntity.DateEnd,
                paraEntity.RegestesAppName,
                paraEntity.RunSpaceTime,
                paraEntity.RunSpaceType,
                paraEntity.ExtraParaStr,
                paraEntity.RunTimeOutSecs);
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="paraTaskID"></param>
        /// <param name="paraName"></param>
        /// <param name="paraDateStart"></param>
        /// <param name="paraDateEnd"></param>
        /// <param name="paraAppName"></param>
        /// <param name="paraRunSpaceTimeSecs"></param>
        /// <param name="paraRunSpaceType"></param>
        /// <param name="paraExtraStr"></param>
        /// <param name="paraRunTimeOutSecs"></param>
        /// <returns></returns>
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
                entity.ExtraParaStr = paraExtraStr;
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

        /// <summary>
        /// 查询计划列表
        /// </summary>
        /// <param name="paraAppName"></param>
        /// <returns></returns>
        public List<TaskEntity> GetTaskListByApp(string paraAppName)
        {
            return _DataAccess.GetTasks(paraAppName);
        }

        /// <summary>
        /// 查询计划列表
        /// </summary>
        /// <returns></returns>
        public List<TaskEntity> GetTaskEntityList()
        {
            return _DataAccess.GetTasks();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraLogEntity"></param>
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

        /// <summary>
        /// 查看一段时间的日志
        /// </summary>
        /// <param name="paraDateStart"></param>
        /// <param name="paraDateEnd"></param>
        /// <returns></returns>
        public DataTable GetTaskLogByDate(DateTime paraDateStart, DateTime paraDateEnd)
        {
            return _DataAccess.GetLog(paraDateStart, paraDateEnd);
        }

        public DataTable GetTaskLogByTask(Int64 paraTaskId)
        {
            return _DataAccess.GetLog(paraTaskId);
        }

        /// <summary>
        /// 查看一个已注册程序的日志
        /// </summary>
        /// <param name="paraRegestedAppName"></param>
        /// <returns></returns>
        public DataTable GetTaskLogByApp(string paraRegestedAppName)
        {
            return _DataAccess.GetLog(paraRegestedAppName);
        }

        /// <summary>
        /// 服务是否启动
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取已经注册的任务（制定任务的时候要选择）
        /// </summary>
        /// <returns></returns>
        public List<string> GetRegestedApp()
        {
            List<string> sc = new List<string>();
            foreach (TaskAssembly ass in GetRegestedApps())
            {
                sc.Add(ass.UserName);
            }
            return sc;
        }

        /// <summary>
        /// 查看任务目前的执行状态
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        public TaskRuningState GetTaskRunState(Int64 paraTaskId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 停止正在执行的任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        public void StopRuningTask(Int64 paraTaskId)
        {
            string message = MessageParser.BuildMessage(null, null, null, null, null,
                new List<long>() { paraTaskId });
            this.SendXMLSocket2Server(message);
        }

        /// <summary>
        /// 立即执行一个任务
        /// </summary>
        /// <param name="paraTaskID"></param>
        public void RunTaskImmediate(Int64 paraTaskID)
        {
            RunTaskType runType = RunTaskType.ImmediateNoDisturb;
            //if (isDisTurbBackTask == false)
            //{
            //    runType = RunTaskType.ImmediateNoDisturb;
            //}
            string message = MessageParser.BuildMessage(null, null, null,
                new List<long>() { paraTaskID }, new List<RunTaskType>() { runType }, null);
            this.SendXMLSocket2Server(message);
        }

        /// <summary>
        /// 启动任务管理器进程
        /// </summary>
        /// <returns></returns>
        public bool StartTaskManager()
        {
            Process[] arr = Process.GetProcessesByName("Component.TimerTask.TaskManager");
            if (arr.Length > 0)
            {
                LogEntity log = new LogEntity();
                log.LogContent = "已经有进程启动，无法再次启动进程。";
                log.LogType = LogType.TaskManagerStartError;
                log.TaskID = -1;
                WriteLog(log);
                return false;
            }
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
                log.LogContent = "当前目录下不存在定时任务管理器程序（Component.TimerTask.TaskManager.exe）";
                log.LogType = LogType.TaskManagerStartError;
                log.TaskID = -1;
                WriteLog(log);
                return false;
            }
            //}
        }



        /// <summary>
        /// 查询（可用的）富计划对象
        /// [外部接口不用使用，内部处理计划用]
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 获取计划的上次执行时间,如果查询不到,返回DateTime.MinValue
        /// </summary>
        /// <param name="paraTaskID"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 获取计划富实体
        /// </summary>
        /// <param name="paraEntity"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraTaskid"></param>
        /// <param name="paraTaskName"></param>
        /// <param name="paraContent"></param>
        /// <param name="paraLogType"></param>
        public void WriteLog(long paraTaskid, string paraTaskName, string paraContent, LogType paraLogType)
        {
            _DataAccess.WriteLog(paraTaskid, paraTaskName, paraContent, paraLogType);
        }

        #endregion

        #region IBLLLogic 成员


        /// <summary>
        /// Adds the task2 DB.
        /// </summary>
        /// <param name="paraTask">The para task.</param>
        public void AddTask2DB(TaskEntity paraTask)
        {
            Int64 id = _DataAccess.AddTask(paraTask);
            paraTask.SetKeyID(id);
        }

        /// <summary>
        /// Updates the task2 DB.
        /// </summary>
        /// <param name="paraTask">The para task.</param>
        public void UpdateTask2DB(TaskEntity paraTask)
        {
            _DataAccess.ModifyTask(paraTask.ID, paraTask);
        }

        /// <summary>
        /// Deletes the task2 DB.
        /// </summary>
        /// <param name="paraTaskID">The para task ID.</param>
        public void DeleteTask2DB(long paraTaskID)
        {
            _DataAccess.RemoveTask(paraTaskID);
        }

        #endregion
    }
}
