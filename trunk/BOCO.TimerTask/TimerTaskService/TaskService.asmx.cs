﻿// File:    TaskService.asmx.cs.cs
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
using BOCO.TimerTask.BLL;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

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

        [WebMethod]
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

        [WebMethod]
        public bool DelTask(Int64 paraID)
        {
            return _BLL.DelTask(paraID);
        }

        [WebMethod]
        public bool UpdateTask(Int64 paraTaskID, string paraName, DateTime paraDateStart, DateTime paraDateEnd, string paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, string paraExtraStr, Int64 paraRunTimeOutSecs)
        {
            return _BLL.UpdateTask(paraTaskID,
                paraName,
                paraDateStart,
                paraDateEnd,
                paraAppName,
                paraRunSpaceTimeSecs,
                paraRunSpaceType,
                paraExtraStr, paraRunTimeOutSecs);
        }

        [WebMethod]
        public List<TaskEntity> GetTaskListByApp(string paraAppName)
        {
            return _BLL.GetTaskListByApp(paraAppName);
        }

        [WebMethod]
        public List<TaskEntity> GetTaskEntityList()
        {
            return _BLL.GetTaskEntityList();
        }

        [WebMethod]
        public void WriteLog(LogEntity paraLogEntity)
        {
            _BLL.WriteLog(paraLogEntity);
        }

        [WebMethod]
        public DataTable GetTaskLogByDate(DateTime paraDateStart, DateTime paraDateEnd)
        {
            return _BLL.GetTaskLogByDate(paraDateStart, paraDateEnd);
        }

        [WebMethod]
        public DataTable GetTaskLogByTask(Int64 paraTaskId)
        {
            return _BLL.GetTaskLogByTask(paraTaskId);
        }

        [WebMethod]
        public DataTable GetTaskLogByApp(string paraRegestedAppName)
        {
            return _BLL.GetTaskLogByApp(paraRegestedAppName);
        }

        [WebMethod]
        public bool IsTaskManagerAlive()
        {
            return _BLL.IsTaskManagerAlive();
        }

        [WebMethod]
        public List<string> GetRegestedApp()
        {
            return _BLL.GetRegestedApp();
        }

        [WebMethod]
        public TaskRuningState GetTaskRunState(Int64 paraTaskId)
        {
            return _BLL.GetTaskRunState(paraTaskId);
        }

        [WebMethod]
        public void StopRuningTask(Int64 paraTaskId)
        {
            _BLL.StopRuningTask(paraTaskId);
        }

        [WebMethod]
        public void RunTaskImmediate(Int64 paraTaskID, bool isDisTurbBackTask)
        {
            _BLL.RunTaskImmediate(paraTaskID, isDisTurbBackTask);
        }

        [WebMethod]
        public bool StartTaskManager()
        {
            return _BLL.StartTaskManager();
        }

        #endregion


    }
}
