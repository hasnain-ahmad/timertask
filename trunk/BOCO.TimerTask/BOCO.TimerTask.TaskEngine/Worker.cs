using System;
using System.Diagnostics;
using System.Threading;
using BOCO.TimerTask.ITimerComponent;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;
using System.IO;

namespace BOCO.TimerTask.TaskEngine
{
    /// <summary>
    /// 工作者模式： 工作执行者
    /// <remarks>定义为抽象类,防止直接去创建这个类</remarks>
    /// </summary>
    internal abstract class Worker : IWorker
    {
        protected BLL.IBLLLogic _BLL;
        protected WorkingTask _Task;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraTask"></param>
        /// <param name="paraBll"></param>
        public Worker(WorkingTask paraTask, BLL.IBLLLogic paraBll)
        {
            _Task = paraTask;
            _BLL = paraBll;
        }

        #region private function

        /// <summary>
        /// 进程结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Process_Exited(object sender, EventArgs e)
        {
            #region 记录到日志中
            string log = _Task.Task.TaskEntity.Name + " Work Complete.";
            _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, log, LogType.TaskRunEnd);
            #endregion

            #region 更新下一步工作
            _Task.Notify_WorkComplete();
            #endregion
        }

        #endregion

        #region IWorker 成员

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="paraRunType">被调度方式</param>
        public virtual void DoWork(RunTaskType paraRunType)
        {

            #region 记录到日志中
            string log = _Task.Task.TaskEntity.Name + " Start Working.";

            LogType logtype = LogType.TaskRunStart;
            switch (paraRunType)
            {
                case RunTaskType.TaskListInTime:
                    logtype = LogType.TaskRunStart;
                    break;
                case RunTaskType.ImmediateNoDisturb:
                    logtype = LogType.TaskRunStart_Immediate;
                    break;
                case RunTaskType.ImmediateDisturb:
                    logtype = LogType.TaskRunStart_Immediate_Interupt;
                    break;
            }

            _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, log, logtype);
            #endregion

            #region 更新下一步工作
            if (paraRunType != RunTaskType.ImmediateNoDisturb)
            {
                _Task.Notify_WorkStarted();
            }
            #endregion

        }

        public virtual void EnforceKillWork()
        {

        }

        #endregion
    }
}
