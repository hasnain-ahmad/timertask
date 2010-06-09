using System;
namespace BOCO.TimerTask.TaskEngine
{
    interface IWorkingTask
    {
        DateTime LastRunTime { get; }
        void Notify_WorkComplete();
        void Notify_WorkStarted();
        BOCO.TimerTask.Model.Enums.TaskRuningState RunState { get; }
        BOCO.TimerTask.Model.Task Task { get; }
        IWorker Worker { get; }
    }
}
