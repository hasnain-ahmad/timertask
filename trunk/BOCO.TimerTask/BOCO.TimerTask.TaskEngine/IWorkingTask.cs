using System;
namespace BOCO.TimerTask.TaskEngine
{
    interface IWorkingTask
    {
        DateTime LastRunTime { get; }
        DateTime NextRunTime { get; }
        void Notify_WorkComplete();
        void Notify_WorkStarted();
        BOCO.TimerTask.Model.Enums.TaskRuningState RunState { get; }
        BOCO.TimerTask.Model.Task Task { get; }
        IWorker Worker { get; }

        /// <summary>
        /// 重构构建任务执行的时间列表
        /// [对任务有更新的时候重新构建]
        /// </summary>
        void RebuildTaskRunTimeList();
    }
}
