/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : IBLLLogic.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 附带其它逻辑的业务逻辑接口
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.DAL;
using Component.TimerTask.Model;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 附带其它逻辑的业务逻辑接口
    /// </summary>
    public interface IBLLLogic : IBLLService
    {
        /// <summary>
        /// 查询（可用的）富计划对象
        /// [外部接口不用使用，内部处理计划用]
        /// </summary>
        /// <returns></returns>
        List<Task> GetTaskList();

        /// <summary>
        /// 获取计划的上次执行时间,如果查询不到,返回DateTime.MinValue
        /// </summary>
        /// <param name="paraTaskID">The para task ID.</param>
        /// <returns></returns>
        DateTime GetTaskLastRunTime(Int64 paraTaskID);

        /// <summary>
        /// 获取计划富实体
        /// </summary>
        /// <param name="paraEntity">The para entity.</param>
        /// <returns></returns>
        Task GetTask(TaskEntity paraEntity);

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="paraEntity">The para entity.</param>
        /// <returns></returns>
        TaskEntity AddTask(TaskEntity paraEntity);

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="paraEntity">The para entity.</param>
        void UpdateTask(TaskEntity paraEntity);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraTaskid">The para taskid.</param>
        /// <param name="paraTaskName">Name of the para task.</param>
        /// <param name="paraContent">Content of the para.</param>
        /// <param name="paraLogType">Type of the para log.</param>
        void WriteLog(Int64 paraTaskid, string paraTaskName, string paraContent, Model.Enums.LogType paraLogType);

        //void AddTask2DB(TaskEntity paraTask);

        //void UpdateTask2DB(TaskEntity paraTask);

        //void DeleteTask2DB(Int64 paraTaskID);
    }
}
