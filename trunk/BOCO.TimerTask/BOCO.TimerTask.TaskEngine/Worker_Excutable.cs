using System;
using System.Diagnostics;
using System.Threading;
using BOCO.TimerTask.ITimerComponent;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.TaskEngine
{
    class Worker_Excutable : IWorker
    {
        private BLL.IBLLLogic _BLL;
        private WorkingTask _Task;

        private Process _Process;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraAssType">程序类型:dll或者exe</param>
        public Worker_Excutable(WorkingTask paraTask, BLL.IBLLLogic paraBll)
        {
            _Task = paraTask;
            _BLL = paraBll;
        }

        #region IWorker 成员

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="paraRunType">被调度方式</param>
        void IWorker.DoWork(RunTaskType paraRunType)
        {
            try
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

                #region 开始工作
                _Process = Process.Start(
                    Utility.AssemblyHelper.GetAssemblyPath() +
                    _Task.Task.TaskAssembly.AppFile, _Task.Task.TaskEntity.ExeCommandParaMeter);
                _Process.EnableRaisingEvents = true;
                _Process.Exited += new EventHandler(Process_Exited);

                #endregion

                #region 监控超时
                if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
                {
                    ParameterizedThreadStart threadStart = new ParameterizedThreadStart(ThreadMonitor);
                    Thread th = new Thread(threadStart);
                    th.IsBackground = true;
                    th.Start(_Process);
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogEntity log = new LogEntity();
                log.LogContent = ex.Message;
                log.LogType = LogType.EnforceKillWorkError;
                log.TaskID = _Task.Task.TaskEntity.ID;
                log.TaskName = _Task.Task.TaskEntity.Name;
                _BLL.WriteLog(log);
            }
        }

        private void ThreadMonitor(object paraMonitorDest)
        {
            if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
            {
                Thread.Sleep((int)_Task.Task.TaskEntity.RunTimeOutSecs * 1000);
                Thread th = (Thread)paraMonitorDest;
                if (th != null) th.Abort();
            }
        }

        /// <summary>
        /// 进程结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_Exited(object sender, EventArgs e)
        {
            #region 记录到日志中
            string log = _Task.Task.TaskEntity.Name + " Work Complete.";
            _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, log, LogType.TaskRunEnd);
            #endregion

            #region 更新下一步工作
            _Task.Notify_WorkComplete();
            #endregion
        }

        void IWorker.EnforceKillWork()
        {
            try
            {
                if (_Process != null && !_Process.HasExited)
                {
                    _Process.Kill();
                    _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, "EnforceKillWork", LogType.EnforceKillWork);
                }
            }
            catch (Exception ex)
            {
                LogEntity log = new LogEntity();
                log.LogContent = ex.Message;
                log.LogType = LogType.EnforceKillWorkError;
                log.TaskID = _Task.Task.TaskEntity.ID;
                log.TaskName = _Task.Task.TaskEntity.Name;
                _BLL.WriteLog(log);
            }
        }

        #endregion
    }
}
