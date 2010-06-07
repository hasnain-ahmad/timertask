// File:    IDataAccess.cs
// Author:  Administrator
// Created: 2010年5月27日 16:44:57
// Purpose: Definition of Interface IDataAccess
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.DAL
{
    /// 数据访问组件
    public interface IDataAccess
    {
        /// <summary>
        /// 按可用状态获取计划
        /// </summary>
        /// <param name="paraEnable"></param>
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
        /// <returns></returns>
        List<TaskEntity> GetTasks(string paraTaskName);

        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="paraTask"></param>
        /// <returns>返回新建计划的ID</returns>
        Int64 AddTask(TaskEntity paraTask);

        /// <summary>
        /// 更新计划
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <param name="paraTask"></param>
        void ModifyTask(Int64 paraTaskId, TaskEntity paraTask);

        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="paraID"></param>
        bool RemoveTask(Int64 paraID);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="paraLog"></param>
        /// <returns></returns>
        bool WriteLog(LogEntity paraLog);

        void WriteLog(Int64 paraTaskid, string paraTaskName, string paraContent, Model.Enums.LogType paraLogType);

        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        DataSet GetAllLog();

        /// <summary>
        /// 查看一个已注册程序的日志
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        DataTable GetLog(string paraRegestedAppName);

        /// <summary>
        /// 获取某个计划对应的日志
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        DataTable GetLog(Int64 paraTaskId);

        /// <summary>
        /// 查看一段时间的日志
        /// </summary>
        /// <param name="paraDateStart"></param>
        /// <param name="paraDateEnd"></param>
        /// <returns></returns>
        DataTable GetLog(DateTime paraDateStart,DateTime paraDateEnd);

        /// <summary>
        /// 获取最近一次执行记录
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        LogEntity GetLog_LatestRun(Int64 paraTaskId, LogType paraLogType);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        bool Save2DB();

    }
}
