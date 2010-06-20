// File:    TaskService.asmx.cs.cs
// Author:  LvJinMing
// Created: 2010年6月4日 15:26:17
// Purpose: Definition of Enum TaskRuningState

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI.MobileControls;
using Component.TimerTask.BLL;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace TimerTaskService
{
    /// <summary>
    /// WebService对外需要实现的接口
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Description = "定时任务管理器Web服务接口")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class TaskService : System.Web.Services.WebService, IBLLService
    {
        private static IBLLService _BLL = BLlFactory.GetBLL();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello Boco!";
        }



        #region IBLLService 成员

        [WebMethod(Description = "添加定时任务")]
        public TaskEntity AddTask(string paraName, DateTime paraDateStart, DateTime paraDateEnd, string paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, string paraExtraStr, Int64 paraRunTimeOutSecs)
        {
            return _BLL.AddTask(paraName,
                paraDateStart,
                paraDateEnd,
                paraAppName,
                paraRunSpaceTimeSecs,
                paraRunSpaceType,
                paraExtraStr,
                paraRunTimeOutSecs);
        }

        [WebMethod(Description = "删除定时任务")]
        public void DelTask(Int64 paraID)
        {
            _BLL.DelTask(paraID);
        }

        [WebMethod(Description = "更新任务")]
        public void UpdateTask(Int64 paraTaskID, string paraName, DateTime paraDateStart, DateTime paraDateEnd, string paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, string paraExtraStr, Int64 paraRunTimeOutSecs)
        {
            _BLL.UpdateTask(paraTaskID,
                paraName,
                paraDateStart,
                paraDateEnd,
                paraAppName,
                paraRunSpaceTimeSecs,
                paraRunSpaceType,
                paraExtraStr, paraRunTimeOutSecs);
        }

        [WebMethod(Description = "查询计划列表")]
        public List<TaskEntity> GetTaskListByApp(string paraAppName)
        {
            return _BLL.GetTaskListByApp(paraAppName);
        }

        [WebMethod(Description = "查询计划列表")]
        public List<TaskEntity> GetTaskEntityList()
        {
            return _BLL.GetTaskEntityList();
        }

        [WebMethod(Description = "写日志")]
        public void WriteLog(LogEntity paraLogEntity)
        {
            _BLL.WriteLog(paraLogEntity);
        }

        [WebMethod(Description = "查看一段时间的日志")]
        public DataTable GetTaskLogByDate(DateTime paraDateStart, DateTime paraDateEnd)
        {
            return _BLL.GetTaskLogByDate(paraDateStart, paraDateEnd);
        }

        [WebMethod(Description = "查看一个已制定计划的日志")]
        public DataTable GetTaskLogByTask(Int64 paraTaskId)
        {
            return _BLL.GetTaskLogByTask(paraTaskId);
        }

        [WebMethod(Description = "查看一个已注册程序的日志")]
        public DataTable GetTaskLogByApp(string paraRegestedAppName)
        {
            return _BLL.GetTaskLogByApp(paraRegestedAppName);
        }

        [WebMethod(Description = "服务是否启动")]
        public bool IsTaskManagerAlive()
        {
            return _BLL.IsTaskManagerAlive();
        }

        [WebMethod(Description = "获取已经注册的任务（制定任务的时候要选择）")]
        public List<string> GetRegestedApp()
        {
            return _BLL.GetRegestedApp();
        }

        [WebMethod(Description = "查看任务目前的执行状态")]
        public TaskRuningState GetTaskRunState(Int64 paraTaskId)
        {
            return _BLL.GetTaskRunState(paraTaskId);
        }

        [WebMethod(Description = "停止正在执行的任务")]
        public void StopRuningTask(Int64 paraTaskId)
        {
            _BLL.StopRuningTask(paraTaskId);
        }

        [WebMethod(Description = "立即执行一个任务")]
        public void RunTaskImmediate(Int64 paraTaskID)
        {
            _BLL.RunTaskImmediate(paraTaskID);
        }

        [WebMethod(Description = "启动任务管理器进程")]
        public bool StartTaskManager()
        {
            return _BLL.StartTaskManager();
        }

        #endregion


    }
}
