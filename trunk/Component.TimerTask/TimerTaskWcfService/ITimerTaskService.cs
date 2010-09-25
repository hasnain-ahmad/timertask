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

namespace TimerTaskWcfService
{
    // 注意: 如果更改此处的接口名称“IService1”，也必须更新 App.config 中对“IService1”的引用。
    [ServiceContract]
    public interface ITimerTaskService : IBLLService
    {
        #region Microsoft Demo
        //[OperationContract]
        //string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);
        #endregion
        // 任务: 在此处添加服务操作
        #region 维护任务相关
        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="paraName">计划名称</param>
        /// <param name="paraDateStart">第一次执行时间</param>
        /// <param name="paraDateEnd">执行结束日期</param>
        /// <param name="paraAppName">添加任务对应的程序名称(在RegestedApps.xml中配置的名称)</param>
        /// <param name="paraRunSpaceTimeSecs">周期数（秒）</param>
        /// <param name="paraRunSpaceType">周期类型（便于存储和下次查看）</param>
        /// <param name="paraExtraStr">The para extra STR.</param>
        /// <param name="paraRunTimeOutSecs">执行超时时间，如果不限定，则给-1，如果限定了，在指定时间内未执行完成，则强制结束（exe直接结束进程，dll通过接口通知结束）</param>
        /// <returns></returns>
        [OperationContract]
        TaskEntity AddTask(String paraName, DateTime paraDateStart, DateTime paraDateEnd, String paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, String paraExtraStr, Int64 paraRunTimeOutSecs);

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="paraID">计划ID</param>
        /// <returns></returns>
        [OperationContract]
        void DelTask(Int64 paraID);

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="paraTaskID">The para task ID.</param>
        /// <param name="paraName">Name of the para.</param>
        /// <param name="paraDateStart">The para date start.</param>
        /// <param name="paraDateEnd">The para date end.</param>
        /// <param name="paraAppName">Name of the para app.</param>
        /// <param name="paraRunSpaceTimeSecs">The para run space time secs.</param>
        /// <param name="paraRunSpaceType">Type of the para run space.</param>
        /// <param name="paraExtraStr">The para extra STR.</param>
        /// <param name="paraRunTimeOutSecs">The para run time out secs.</param>
        [OperationContract]
        void UpdateTask(Int64 paraTaskID, String paraName, DateTime paraDateStart, DateTime paraDateEnd, String paraAppName, Int64 paraRunSpaceTimeSecs, TaskFrequence paraRunSpaceType, String paraExtraStr, Int64 paraRunTimeOutSecs);


        /// <summary>
        /// 查询计划列表
        /// </summary>
        /// <param name="paraAppName">Name of the para app.</param>
        /// <returns></returns>
        [OperationContract]
        List<TaskEntity> GetTaskListByApp(String paraAppName);

        /// <summary>
        /// 查询计划列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TaskEntity> GetTaskEntityList();


        #endregion

        #region 日志相关接口

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraLogEntity">The para log entity.</param>
        [OperationContract]
        void WriteLog(LogEntity paraLogEntity);

        /// <summary>
        /// 查看一段时间的日志
        /// </summary>
        /// <param name="paraDateStart">The para date start.</param>
        /// <param name="paraDateEnd">The para date end.</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetTaskLogByDate(DateTime paraDateStart, DateTime paraDateEnd);

        /// <summary>
        /// 查看一个已制定计划的日志
        /// </summary>
        /// <param name="paraTaskId">The para task id.</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetTaskLogByTask(Int64 paraTaskId);

        /// <summary>
        /// 查看一个已注册程序的日志
        /// </summary>
        /// <param name="paraRegestedAppName">Name of the para regested app.</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetTaskLogByApp(string paraRegestedAppName);

        #endregion

        #region 任务管理器相关接口

        /// <summary>
        /// 服务是否启动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool IsTaskManagerAlive();

        /// <summary>
        /// 获取已经注册的任务（制定任务的时候要选择）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<String> GetRegestedApp();

        /// <summary>
        /// 查看任务目前的执行状态
        /// </summary>
        /// <param name="paraTaskId">The para task id.</param>
        /// <returns></returns>
        [OperationContract]
        TaskRuningState GetTaskRunState(Int64 paraTaskId);

        /// <summary>
        /// 停止正在执行的任务
        /// </summary>
        /// <param name="paraTaskId">The para task id.</param>
        [OperationContract]
        void StopRuningTask(Int64 paraTaskId);

        /// <summary>
        /// 立即执行一个任务
        /// </summary>
        /// <param name="paraTaskID">The para task ID.</param>
        [OperationContract]
        void RunTaskImmediate(Int64 paraTaskID);

        /// <summary>
        /// 启动任务管理器进程
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool StartTaskManager();
        #endregion
    }

    #region Microsoft Demo
    //// 使用下面示例中说明的数据协定将复合类型添加到服务操作
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
    #endregion
}
