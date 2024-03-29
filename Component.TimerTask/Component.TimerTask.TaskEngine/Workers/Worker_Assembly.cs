﻿/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : Worker_Assembly.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 执行者：动态库方式
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.TaskInterface;

namespace Component.TimerTask.TaskEngine.Workers
{
    /// <summary>
    /// 执行者：动态库方式
    /// </summary>
    class Worker_Assembly : Worker
    {
        private Thread _Thread4Work;
        private ITask _WorkInterface;

        public Worker_Assembly(WorkingTask paraTask, BLL.IBLLLogic paraBll)
            : base(paraTask, paraBll)
        {
        }

        private void ThreadFuncWorkMonitor(object paraMonitorDest)
        {
            try
            {
                if (_WrkTask.Task.TaskEntity.RunTimeOutSecs > 0)
                {
                    Thread.Sleep((int)_WrkTask.Task.TaskEntity.RunTimeOutSecs * 1000);

                    //先通过任务接口结束
                    this._WorkInterface.StopRuning();

                    //如果还没有结束，则结束线程
                    Thread th = (Thread)paraMonitorDest;
                    //Console.WriteLine(th.ThreadState.ToString());
                    if (th != null && th.ThreadState != System.Threading.ThreadState.Aborted)
                    {
                        try
                        {
                            th.Abort();
                        }
                        catch { }
                    }

                    GC.Collect();
                    GC.WaitForFullGCComplete();
                }
            }
            catch (Exception ex)
            {
                _BLL.WriteLog(
                    _WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, ex.Message, LogType.StopRuningFromInterfaceError);
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
                Console.WriteLine("**************************  Work Start {0} {1}", _WrkTask.Task.TaskEntity.Name, DateTime.Now);

                #region 开始工作
                string destFile = Utility.AssemblyHelper.GetAssemblyPath() + _WrkTask.Task.TaskAssembly.AppFile;    //最好用Path.Combine
                if (File.Exists(destFile))
                {
                    FileInfo fi = new FileInfo(destFile);
                    object obj;

                    #region 反射加载
                    try
                    {
                        Assembly abl;
                        List<Assembly> assmlys = new List<Assembly>();
                        assmlys.AddRange(AppDomain.CurrentDomain.GetAssemblies());
                        abl = assmlys.Find(delegate(Assembly aa) { return aa.Location.Equals(fi.FullName); });
                        assmlys = null;
                        if (abl == null)
                        {
                            abl = System.Reflection.Assembly.LoadFrom(fi.FullName);
                        }

                        obj = abl.CreateInstance(_WrkTask.Task.TaskAssembly.ProtocolNameSpace + "." + _WrkTask.Task.TaskAssembly.ProtocolClass);
                    }
                    catch (System.Reflection.TargetInvocationException ex)   //捕获反射错误的异常
                    {
                        string s = "无法加载目标对象，请检查与目标对象相关联的引用是否存在。" + ex.Message;
                        Console.WriteLine("执行任务发生异常：{0}", s);
                        _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, s, LogType.ReflectError);
                        return;
                    }
                    #endregion

                    #region 转换为ITask接口
                    if (obj is ITask)
                    {
                        _WorkInterface = (ITask)obj;
                    }
                    else
                    {
                        string s = "无法加载目标对象，目标对象未继承ITask接口。";
                        Console.WriteLine("执行任务发生异常：{0}", s);
                        _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, s, LogType.TypeConvertITaskError);
                        base.DoWork(paraRunType);
                        return;
                    }
                    #endregion

                    _WorkInterface.OnFuncComplete = new WaitCallback(Process_Exited);
                    _WorkInterface.ExtraParaStr = _WrkTask.Task.TaskEntity.ExtraParaStr;
                    Thread thWork = new Thread(new ThreadStart(_WorkInterface.RunTask));
                    _Thread4Work = thWork;
                    _Thread4Work.IsBackground = true;
                    _Thread4Work.Start();

                    #region 监控超时
                    if (_WrkTask.Task.TaskEntity.RunTimeOutSecs > 0)
                    {
                        //ParameterizedThreadStart threadStart = new ParameterizedThreadStart(WorkMonitor);
                        //Thread th = new Thread(threadStart);
                        //th.IsBackground = true;
                        //th.Start(thWork);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadFuncWorkMonitor), thWork);
                        //th.Start(_WorkInterface);
                    }
                    #endregion
                }
                else
                {
                    string s = string.Format("目标位置不存在文件,无法执行该任务({0})", destFile);
                    Console.WriteLine(s);
                    _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, s, LogType.TaskConfigAssemblyFileNotFind);
                    //return;
                }
                #endregion

                base.DoWork(paraRunType);
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
                if (_WorkInterface != null)
                {
                    _WorkInterface.StopRuning();
                }

                if (_Thread4Work != null && _Thread4Work.ThreadState != System.Threading.ThreadState.Aborted)
                {
                    try
                    {
                        _Thread4Work.Abort();
                    }
                    catch { }
                }
                //base.ManualStopWork();

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
