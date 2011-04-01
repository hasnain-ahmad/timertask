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
        /// 下次运行时间
        /// </summary>
        /// <value>The next run time.</value>
        DateTime NextRunTime { get; }
        
        /// <summary>
        /// 通知-任务完成
        /// </summary>
        void Notify_WorkComplete();
        
        /// <summary>
        /// 通知-任务开始
        /// </summary>
        void Notify_WorkStarted();
        
        /// <summary>
        /// 任务运行状态
        /// </summary>
        /// <value>The state of the run.</value>
        TaskRuningState RunState { get; }
        
        /// <summary>
        /// Task
        /// </summary>
        /// <value>The task.</value>
        Task Task { get; }
        
        /// <summary>
        /// 工作者
        /// </summary>
        /// <value>The worker.</value>
        IWorker Worker { get; }
        
        /// <summary>
        /// 更新任务属性
        /// </summary>
        /// <param name="taskEntity">The task entity.</param>
        void UpdateTask(TaskEntity taskEntity);
    }
}
