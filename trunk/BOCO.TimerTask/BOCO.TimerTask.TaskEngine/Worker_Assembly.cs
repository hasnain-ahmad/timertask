//using System;
//using System.Diagnostics;
//using System.Threading;
//using BOCO.TimerTask.ITimerComponent;
//using BOCO.TimerTask.Model;
//using BOCO.TimerTask.Model.Enums;

//namespace BOCO.TimerTask.TaskEngine
//{
//    class Worker_Assembly : IWorker
//    {
//        private BLL.IBLLLogic _BLL;
//        private WorkingTask _Task;
//        private Thread _Thread;
//        private ITimeWorkTask _WorkInterface;


//        public Worker_Assembly(WorkingTask paraTask, BLL.IBLLLogic paraBll)
//        {
//            _Task = paraTask;
//            _BLL = paraBll;
//        }


//        private void ThreadMonitor(object paraMonitorDest)
//        {
//            Thread thread = (Thread)paraMonitorDest;
//            if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
//            {
//                Thread.Sleep((int)_Task.Task.TaskEntity.RunTimeOutSecs * 1000);
//                if (thread != null) thread.Abort();
//                else
//                {
//                    //记录异常
//                }
//            }
//        }

//        /// <summary>
//        /// 进程结束
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void Process_Exited(object sender, EventArgs e)
//        {
//            #region 记录到日志中
//            string log = _Task.Task.TaskEntity.Name + " Work Complete.";
//            _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, log, LogType.TaskRunEnd);
//            #endregion

//            #region 更新下一步工作
//            _Task.Notify_WorkComplete();
//            #endregion
//        }

//        #region IWorker 成员

//        public void DoWork(RunTaskType paraRunType)
//        {
//            try
//            {
//                #region 记录到日志中
//                string log = _Task.Task.TaskEntity.Name + " Start Working.";

//                LogType logtype = LogType.TaskRunStart;
//                switch (paraRunType)
//                {
//                    case RunTaskType.TaskListInTime:
//                        logtype = LogType.TaskRunStart;
//                        break;
//                    case RunTaskType.ImmediateNoDisturb:
//                        logtype = LogType.TaskRunStart_Immediate;
//                        break;
//                    case RunTaskType.ImmediateDisturb:
//                        logtype = LogType.TaskRunStart_Immediate_Interupt;
//                        break;
//                }

//                _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, log, logtype);
//                #endregion

//                #region 更新下一步工作
//                if (paraRunType != RunTaskType.ImmediateNoDisturb)
//                {
//                    _Task.Notify_WorkStarted();
//                }
//                #endregion

//                #region 开始工作

//                if (_Task.Task.TaskAssembly.AssemblyType == AssemblyType.Dll)
//                {
//                    object obj = System.Reflection.Assembly.Load(
//                        Utility.AssemblyHelper.GetAssemblyPath() +
//                        _Task.Task.TaskAssembly.AppFile).CreateInstance(_Task.Task.TaskAssembly.ProtocolNameSpace + "." + _Task.Task.TaskAssembly.ProtocolClass);
//                    _WorkInterface = (ITimeWorkTask)obj;

//                    _WorkInterface.ThreadCompleteFunc = Process_Exited;

//                    _Thread = new Thread(new ThreadStart(_WorkInterface.TaskExecuteFunc));
//                    _Thread.IsBackground = true;
//                    _Thread.Start();
//                }
//                #endregion

//                #region 监控超时
//                if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
//                {
//                    ParameterizedThreadStart threadStart = new ParameterizedThreadStart(ThreadMonitor);
//                    Thread th = new Thread(threadStart);
//                    th.IsBackground = true;
//                    th.Start(_Thread);
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                LogEntity log = new LogEntity();
//                log.LogContent = ex.Message;
//                log.LogType = LogType.EnforceKillWorkError;
//                log.TaskID = _Task.Task.TaskEntity.ID;
//                log.TaskName = _Task.Task.TaskEntity.Name;
//                _BLL.WriteLog(log);
//            }
//        }

//        public void EnforceKillWork()
//        {
//            try
//            {
//                if (_Task.Task.TaskAssembly.AssemblyType == AssemblyType.Dll)
//                {
//                    if (_Thread != null && _Thread.ThreadState == System.Threading.ThreadState.Running)
//                    {
//                        if (_WorkInterface != null)
//                        {
//                            _WorkInterface.StopRuning();
//                            _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, "EnforceKillWork", LogType.EnforceKillWork);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                LogEntity log = new LogEntity();
//                log.LogContent = ex.Message;
//                log.LogType = LogType.EnforceKillWorkError;
//                log.TaskID = _Task.Task.TaskEntity.ID;
//                log.TaskName = _Task.Task.TaskEntity.Name;
//                _BLL.WriteLog(log);
//            }
//        }

//        #endregion
//    }
//}
