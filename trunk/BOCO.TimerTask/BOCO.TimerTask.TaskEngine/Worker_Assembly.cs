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
    /// 执行者：动态库方式
    /// </summary>
    class Worker_Assembly : Worker
    {
        private Thread _Thread;
        private ITimeWorkTask _WorkInterface;

        public Worker_Assembly(WorkingTask paraTask, BLL.IBLLLogic paraBll)
            : base(paraTask, paraBll)
        {
        }

        private void WorkMonitor(object paraMonitorDest)
        {
            if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
            {
                Thread.Sleep((int)_Task.Task.TaskEntity.RunTimeOutSecs * 1000);

                ITimeWorkTask th = (ITimeWorkTask)paraMonitorDest;
                if (th != null) th.StopRuning();

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
                    _WorkInterface = (ITimeWorkTask)obj;

                    _WorkInterface.ThreadCompleteFunc = Process_Exited;
                    _WorkInterface.ExtraParaStr = _Task.Task.TaskEntity.ExtraParaStr;
                    _Thread = new Thread(new ThreadStart(_WorkInterface.TaskExecuteFunc));
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

        public override void EnforceKillWork()
        {
            try
            {
                base.EnforceKillWork();

                if (_Thread != null && _Thread.ThreadState == System.Threading.ThreadState.Running)
                {
                    if (_WorkInterface != null)
                    {
                        _WorkInterface.StopRuning();
                        _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, "EnforceKillWork", LogType.EnforceKillWork);
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
