/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : IWorkingTask.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 正在工作的任务
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 正在工作的任务
    /// </summary>
    interface IWorkingTask : IDisposable
    {
        /// <summary>
        /// 上次运行时间
        /// </summary>
        DateTime LastRunTime { get; }

        /// <summary>
        /// Gets the next run time.
        /// </summary>
        /// <value>The next run time.</value>
        DateTime NextRunTime { get; }
        /// <summary>
        /// Notify_s the work complete.
        /// </summary>
        void Notify_WorkComplete();
        /// <summary>
        /// Notify_s the work started.
        /// </summary>
        void Notify_WorkStarted();
        /// <summary>
        /// Gets the state of the run.
        /// </summary>
        /// <value>The state of the run.</value>
        TaskRuningState RunState { get; }
        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <value>The task.</value>
        Task Task { get; }
        /// <summary>
        /// Gets the worker.
        /// </summary>
        /// <value>The worker.</value>
        IWorker Worker { get; }
        /// <summary>
        /// 重构构建任务执行的时间列表
        /// [对任务有更新的时候重新构建]
        /// </summary>
        void RebuildTaskRunTimeList();
    }
}
