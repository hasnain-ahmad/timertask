/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : IDataAccess.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 数据访问接口
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.DAL
{

    /// <summary>
    /// Date: 2010-6-20 10:58
    /// Author: Administrator
    /// FullName: Component.TimerTask.DAL.IDataAccess
    /// Interface: 数据访问接口
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// 按可用状态获取计划
        /// </summary>
        /// <param name="paraEnable">if set to <c>true</c> [para enable].</param>
        /// <returns></returns>
        List<TaskEntity> GetTasks(bool paraEnable);

        /// <summary>
        /// 获取所有计划
        /// </summary>
        /// <returns></returns>
        List<TaskEntity> GetTasks();

        /// <summary>
        /// 获取所有计划
        /// </summary>
        /// <param name="paraTaskName">Name of the para task.</param>
        /// <returns></returns>
        List<TaskEntity> GetTasks(string paraTaskName);

        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="paraTask">The para task.</param>
        /// <returns>返回新建计划的ID</returns>
        Int64 AddTask(TaskEntity paraTask);

        /// <summary>
        /// 更新计划
        /// </summary>
        /// <param name="paraTaskId">The para task id.</param>
        /// <param name="paraTask">The para task.</param>
        void ModifyTask(Int64 paraTaskId, TaskEntity paraTask);

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="paraID">The para ID.</param>
        /// <returns></returns>
        void RemoveTask(Int64 paraID);

        /// <summary>
        /// 彻底删除任务
        /// [只要在计划添加失败的时候才调用,平时不要调用]
        /// </summary>
        /// <param name="paraID">The para ID.</param>
        /// <returns></returns>
        bool DelTaskComplet(Int64 paraID);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraLog">The para log.</param>
        /// <returns></returns>
        bool WriteLog(LogEntity paraLog);

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="paraTaskid">The para taskid.</param>
        /// <param name="paraTaskName">Name of the para task.</param>
        /// <param name="paraContent">Content of the para.</param>
        /// <param name="paraLogType">Type of the para log.</param>
        void WriteLog(Int64 paraTaskid, string paraTaskName, string paraContent, Model.Enums.LogType paraLogType);

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        DataSet GetAllLog();

        /// <summary>
        /// 查看一个已注册程序的日志
        /// </summary>
        /// <param name="paraRegestedAppName">Name of the para regested app.</param>
        /// <returns></returns>
        DataTable GetLog(string paraRegestedAppName);

        /// <summary>
        /// 获取某个计划对应的日志
        /// </summary>
        /// <param name="paraTaskId">The para task id.</param>
        /// <returns></returns>
        DataTable GetLog(Int64 paraTaskId);

        /// <summary>
        /// 查看一段时间的日志
        /// </summary>
        /// <param name="paraDateStart">The para date start.</param>
        /// <param name="paraDateEnd">The para date end.</param>
        /// <returns></returns>
        DataTable GetLog(DateTime paraDateStart,DateTime paraDateEnd);

        /// <summary>
        /// 获取最近一次执行记录
        /// </summary>
        /// <param name="paraTaskId">The para task id.</param>
        /// <param name="paraLogType">Type of the para log.</param>
        /// <returns></returns>
        LogEntity GetLog_LatestRun(Int64 paraTaskId, LogType paraLogType);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        bool Save2DB();


        #region 心跳相关

        /// <summary>
        /// 写心跳时间
        /// </summary>
        void WriteHeartDate();

        /// <summary>
        /// 读心跳时间
        /// </summary>
        /// <returns></returns>
        DateTime ReadHeartDate();

        /// <summary>
        /// 初始化心跳表，插入一条记录
        /// </summary>
        void InitHeartTable();
        #endregion
    }
}
