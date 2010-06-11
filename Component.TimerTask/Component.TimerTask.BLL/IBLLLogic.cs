using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model;
using Component.TimerTask.DAL;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 附带其它逻辑的业务逻辑接口
    /// </summary>
    public interface IBLLLogic : IBLLService
    {
        //IDataAccess DataAccess { get; }

        /// <summary>
        /// 查询（可用的）富计划对象
        /// [外部接口不用使用，内部处理计划用]
        /// </summary>
        /// <returns></returns>
        List<Task> GetTaskList();

        /// <summary>
        /// 获取计划的上次执行时间,如果查询不到,返回DateTime.MinValue
        /// </summary>
        /// <param name="paraTaskID"></param>
        /// <returns></returns>
        DateTime GetTaskLastRunTime(Int64 paraTaskID);

        /// <summary>
        /// 获取计划富实体
        /// </summary>
        /// <param name="paraEntity"></param>
        /// <returns></returns>
        Task GetTask(TaskEntity paraEntity);

        TaskEntity AddTask(TaskEntity paraEntity);

        bool UpdateTask(TaskEntity paraEntity);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraTaskid"></param>
        /// <param name="paraTaskName"></param>
        /// <param name="paraContent"></param>
        /// <param name="paraLogType"></param>
        void WriteLog(Int64 paraTaskid, string paraTaskName, string paraContent, Model.Enums.LogType paraLogType);

        void AddTask2DB(TaskEntity paraTask);

        void UpdateTask2DB(TaskEntity paraTask);

        void DeleteTask2DB(Int64 paraTaskID);
    }
}
