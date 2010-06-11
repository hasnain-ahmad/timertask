// File:    ITaskWorkerEngine.cs
// Author:  Administrator
// Created: 2010年5月27日 17:14:20
// Purpose: Definition of Interface ITaskWorkerEngine

using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Model;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 定时任务引擎
    /// </summary>
    public interface ITaskWorkerEngine
    {
        /// <summary>
        /// 空闲时间（秒）
        /// </summary>
        int IdleSpanInMSecs { get; set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        bool IsRuning { get; }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        bool Start();

        bool Stop();

        /// <summary>
        /// 查询任务的执行状态
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        TaskRuningState GetTaskRuningState(Int64 paraTaskId);

        /// <summary>
        /// 手动执行一个任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        bool ManualRunTask(Int64 paraTaskId, RunTaskType paraRunType);

        /// <summary>
        /// 手动停止一个正在执行的任务
        /// [不影响后续任务执行]
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        bool StopRuningTask(Int64 paraTaskId);

        /// <summary>
        /// 向引擎中添加任务
        /// </summary>
        /// <param name="paraTask"></param>
        void AddWorkingTask(TaskEntity paraTask);
        /// <summary>
        /// 修改引擎中的任务
        /// </summary>
        /// <param name="paraTask"></param>
        void ModifyTask(TaskEntity paraTask);
        /// <summary>
        /// 删除引擎中的任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        void DelTask(Int64 paraTaskId);
    }
}
