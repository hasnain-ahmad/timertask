using System;
using System.Diagnostics;
using System.Threading;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using System.IO;
using Component.TimerTask.TaskInterface;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 执行者：动态库方式
    /// </summary>
    class Worker_Assembly : Worker
    {
        private Thread _Thread;
        private ITask _WorkInterface;

        public Worker_Assembly(WorkingTask paraTask, BLL.IBLLLogic paraBll)
            : base(paraTask, paraBll)
        {
        }

        private void WorkMonitor(object paraMonitorDest)
        {
            try
            {
                if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
                {
                    Thread.Sleep((int)_Task.Task.TaskEntity.RunTimeOutSecs * 1000);

                    ITask th = (ITask)paraMonitorDest;
                    if (th != null) th.StopRuning();

                }
            }
            catch (Exception ex)
            {
                _BLL.WriteLog(
                    _Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, ex.Message, LogType.StopRuningFromInterfaceError);
            }
        }


        #region IWorker 成员

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="paraRunType">被调度方式</param>
        public override void DoWork(RunTaskType paraRunType)
        {
            try
            {
                #region 开始工作
                string destFile = Utility.AssemblyHelper.GetAssemblyPath() + _Task.Task.TaskAssembly.AppFile;
                if (File.Exists(destFile))
                {
                    FileInfo fi = new FileInfo(destFile);

                    object obj = System.Reflection.Assembly.LoadFrom(fi.FullName).CreateInstance(_Task.Task.TaskAssembly.ProtocolNameSpace + "." + _Task.Task.TaskAssembly.ProtocolClass);
                    _WorkInterface = (ITask)obj;

                    _WorkInterface.ThreadCompleteFunc = Process_Exited;
                    _WorkInterface.ExtraParaStr = _Task.Task.TaskEntity.ExtraParaStr;
                    _Thread = new Thread(new ThreadStart(_WorkInterface.RunTask));
                    _Thread.IsBackground = true;
                    _Thread.Start();

                    #region 监控超时
                    if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
                    {
                        ParameterizedThreadStart threadStart = new ParameterizedThreadStart(WorkMonitor);
                        Thread th = new Thread(threadStart);
                        th.IsBackground = true;
                        th.Start(_WorkInterface);
                    }
                    #endregion
                }
                else
                {
                    string s = string.Format("目标位置不存在文件,无法执行该任务({0})", destFile); ;
                    Console.WriteLine(s);
                    _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, s, LogType.TaskConfigAssemblyFileNotFind);
                    return;
                }
                #endregion

                base.DoWork(paraRunType);
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

        public override void ManualStopWork()
        {
            try
            {
                if (_Thread != null && _Thread.ThreadState == System.Threading.ThreadState.Running)
                {
                    if (_WorkInterface != null)
                    {
                        _WorkInterface.StopRuning();
                    }
                }
                base.ManualStopWork();

                GC.Collect();
                GC.WaitForPendingFinalizers();
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
