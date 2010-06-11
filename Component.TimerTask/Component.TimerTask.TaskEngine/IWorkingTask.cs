using System;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Model;
namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 正在工作的任务
    /// </summary>
    interface IWorkingTask
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
