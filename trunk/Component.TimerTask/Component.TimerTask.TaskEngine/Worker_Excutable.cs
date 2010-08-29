﻿using System;
using System.Diagnostics;
using System.Threading;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using System.IO;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 执行者：可执行文件方式
    /// </summary>
    class Worker_Excutable : Worker
    {
        Process _Process;

        public Worker_Excutable(WorkingTask paraTask, BLL.IBLLLogic paraBll)
            : base(paraTask, paraBll)
        {
        }

        private void WorkMonitor(object paraMonitorDest)
        {
            if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
            {
                Thread.Sleep((int)_Task.Task.TaskEntity.RunTimeOutSecs * 1000);

                Process p = (Process)paraMonitorDest;
                if (p != null && !p.HasExited) p.Kill();

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
                base.DoWork(paraRunType);

                #region 开始工作
                string destFile = Utility.AssemblyHelper.GetAssemblyPath() + _Task.Task.TaskAssembly.AppFile;
                if (File.Exists(destFile))
                {
                    FileInfo fi = new FileInfo(destFile);

                    try
                    {
                        _Process = Process.Start(fi.FullName, _Task.Task.TaskEntity.ExtraParaStr);
                    }
                    catch   //捕获执行异常问题
                    {
                        string s = "无法执行目标对象，执行目标对象出现异常:" + fi.FullName;
                        Console.WriteLine("执行任务发生异常：{0}", s);
                        _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, s, LogType.RunExeFileError);
                        return;
                    }
                    _Process.EnableRaisingEvents = true;
                    _Process.Exited += new EventHandler(Process_Exited);

                    #region 监控超时
                    if (_Task.Task.TaskEntity.RunTimeOutSecs > 0)
                    {
                        ParameterizedThreadStart threadStart = new ParameterizedThreadStart(WorkMonitor);
                        Thread th = new Thread(threadStart);
                        th.IsBackground = true;
                        th.Start(_Process);
                    }
                    #endregion
                }
                else
                {
                    string s = string.Format("目标位置不存在文件,无法执行该任务({0})", destFile); ;
                    Console.WriteLine(s);
                    _BLL.WriteLog(_Task.Task.TaskEntity.ID, _Task.Task.TaskEntity.Name, s, LogType.TaskConfigAssemblyFileNotFind);
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
                Console.WriteLine("执行任务发生异常：{0}", ex.Message);
            }
        }

        public override void ManualStopWork()
        {
            try
            {
                if (_Process != null && !_Process.HasExited)
                {
                    _Process.Kill();
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
