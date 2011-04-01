using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Component.TimerTask.BLL;
using System.Data;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.WcfService
{
    // 注意: 如果更改此处的类名“IService1”，也必须更新 App.config 中对“IService1”的引用。
    public class TimerTaskService : ITimerTaskService
    {
        private static IBLLService _BLL = BLLFactory.GetBLL();

        #region Microsoft Demo
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
        #endregion



        #region IBLLService 成员

        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="paraDateStart"></param>
        /// <param name="paraDateEnd"></param>
        /// <param name="paraAppName"></param>
        /// <param name="paraRunSpaceTimeSecs"></param>
        /// <param name="paraRunSpaceType"></param>
        /// <param name="paraExtraStr"></param>
        /// <param name="paraRunTimeOutSecs"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="paraID"></param>
        public void DelTask(Int64 paraID)
        {
            _BLL.DelTask(paraID);
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

        /// <summary>
        /// 查询计划列表
        /// </summary>
        /// <param name="paraAppName"></param>
        /// <returns></returns>
        public List<TaskEntity> GetTaskListByApp(string paraAppName)
        {
            return _BLL.GetTaskListByApp(paraAppName);
        }

        /// <summary>
        /// 查询计划列表
        /// </summary>
        /// <returns></returns>
        public List<TaskEntity> GetTaskEntityList()
        {
            return _BLL.GetTaskEntityList();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraLogEntity"></param>
        public void WriteLog(LogEntity paraLogEntity)
        {
            _BLL.WriteLog(paraLogEntity);
        }

        /// <summary>
        /// 查看一段时间的日志
        /// </summary>
        /// <param name="paraDateStart"></param>
        /// <param name="paraDateEnd"></param>
        /// <returns></returns>
        public DataTable GetTaskLogByDate(DateTime paraDateStart, DateTime paraDateEnd)
        {
            return _BLL.GetTaskLogByDate(paraDateStart, paraDateEnd);
        }

        /// <summary>
        /// 查看一个已制定计划的日志
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        public DataTable GetTaskLogByTask(Int64 paraTaskId)
        {
            return _BLL.GetTaskLogByTask(paraTaskId);
        }

        /// <summary>
        /// 查看一个已注册程序的日志
        /// </summary>
        /// <param name="paraRegestedAppName"></param>
        /// <returns></returns>
        public DataTable GetTaskLogByApp(string paraRegestedAppName)
        {
            return _BLL.GetTaskLogByApp(paraRegestedAppName);
        }

        /// <summary>
        /// 服务是否启动
        /// </summary>
        /// <returns></returns>
        public bool IsTaskManagerAlive()
        {
            return _BLL.IsTaskManagerAlive();
        }

        /// <summary>
        /// 获取已经注册的任务（制定任务的时候要选择）
        /// </summary>
        /// <returns></returns>
        public List<string> GetRegestedApp()
        {
            return _BLL.GetRegestedApp();
        }

        /// <summary>
        /// 查看任务目前的执行状态
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        public TaskRuningState GetTaskRunState(Int64 paraTaskId)
        {
            return _BLL.GetTaskRunState(paraTaskId);
        }

        /// <summary>
        /// 停止正在执行的任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        public void StopRuningTask(Int64 paraTaskId)
        {
            _BLL.StopRuningTask(paraTaskId);
        }

        /// <summary>
        /// 立即执行一个任务
        /// </summary>
        /// <param name="paraTaskID"></param>
        public void RunTaskImmediate(Int64 paraTaskID)
        {
            _BLL.RunTaskImmediate(paraTaskID);
        }

        /// <summary>
        /// 启动任务管理器进程
        /// </summary>
        /// <returns></returns>
        public bool StartTaskManager()
        {
            return _BLL.StartTaskManager();
        }

        #endregion
    }
}
