/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : Worker_Excutable.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 执行者：可执行文件方式
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

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
            if (_WrkTask.Task.TaskEntity.RunTimeOutSecs > 0)
            {
                Thread.Sleep((int)_WrkTask.Task.TaskEntity.RunTimeOutSecs * 1000);

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
                string destFile = Utility.AssemblyHelper.GetAssemblyPath() + _WrkTask.Task.TaskAssembly.AppFile;
                if (File.Exists(destFile))
                {
                    FileInfo fi = new FileInfo(destFile);

                    try
                    {
                        _Process = Process.Start(fi.FullName, _WrkTask.Task.TaskEntity.ExtraParaStr);
                    }
                    catch   //捕获执行异常问题
                    {
                        string s = "无法执行目标对象，执行目标对象出现异常:" + fi.FullName;
                        Console.WriteLine("执行任务发生异常：{0}", s);
                        _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, s, LogType.RunExeFileError);
                        return;
                    }
                    _Process.EnableRaisingEvents = true;
                    _Process.Exited += new EventHandler(Process_Exited);

                    #region 监控超时
                    if (_WrkTask.Task.TaskEntity.RunTimeOutSecs > 0)
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
                    _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, s, LogType.TaskConfigAssemblyFileNotFind);
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogEntity log = new LogEntity();
                log.LogContent = ex.Message;
                log.LogType = LogType.EnforceKillWorkError;
                log.TaskID = _WrkTask.Task.TaskEntity.ID;
                log.TaskName = _WrkTask.Task.TaskEntity.Name;
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
                log.TaskID = _WrkTask.Task.TaskEntity.ID;
                log.TaskName = _WrkTask.Task.TaskEntity.Name;
                _BLL.WriteLog(log);
            }
        }

        #endregion
    }
}
