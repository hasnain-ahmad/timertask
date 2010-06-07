using System;
using System.Diagnostics;
using System.Threading;
using BOCO.TimerTask.ITimerComponent;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.TaskEngine
{
    /// <summary>
    /// 工作者模式： 工作执行者
    /// </summary>
    internal class Worker : IWorker
    {
        private BLL.IBLLLogic _BLL;
        private WorkingTask _Task;

        private Process _Process;

        private Thread _Thread;
        private ITimeWorkTask _WorkInterface;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraAssType">程序类型:dll或者exe</param>
        public Worker(WorkingTask paraTask, BLL.IBLLLogic paraBll)
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
                    _Task.WorkStarted();
                }
                #endregion

                #region 开始工作
                if (_Task.Task.TaskAssembly.AssemblyType == AssemblyType.Exe)
                {
                    _Process = Process.Start(_Task.Task.TaskAssembly.AppFile, _Task.Task.TaskEntity.ExeCommandParaMeter);
                    _Process.Exited += new EventHandler(Process_Exited);
                }

                if (_Task.Task.TaskAssembly.AssemblyType == AssemblyType.Dll)
                {
                    object obj = System.Reflection.Assembly.Load(_Task.Task.TaskAssembly.AppFile).CreateInstance(_Task.Task.TaskAssembly.ProtocolNameSpace + "." + _Task.Task.TaskAssembly.ProtocolClass);
                    _WorkInterface = (ITimeWorkTask)obj;

                    _WorkInterface.ThreadCompleteFunc = Process_Exited;

                    _Thread = new Thread(new ThreadStart(_WorkInterface.TaskExecuteFunc));
                    _Thread.IsBackground = true;
                    _Thread.Start();
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
            _Task.WorkComplete();
            #endregion
        }

        void IWorker.EnforceKillWork()
        {
            try
            {
                if (_Task.Task.TaskAssembly.AssemblyType == AssemblyType.Exe)
                {
                    if (_Process != null && !_Process.HasExited)
                    {
                        _Process.Kill();
                        _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, "EnforceKillWork", LogType.EnforceKillWork);
                    }
                }
                if (_Task.Task.TaskAssembly.AssemblyType == AssemblyType.Dll)
                {
                    if (_Thread != null && _Thread.ThreadState == System.Threading.ThreadState.Running)
                    {
                        if (_WorkInterface != null)
                        {
                            _WorkInterface.StopRuning();
                            _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, "EnforceKillWork", LogType.EnforceKillWork);
                        }
                    }
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
