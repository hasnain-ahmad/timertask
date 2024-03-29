﻿/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : WorkingTask.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 工作者模式-待执行任务实体
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 工作者模式-待执行任务实体
    /// </summary>
    internal class WorkingTask : IWorkingTask
    {
        private DateTime _NextRunTime = DateTime.MaxValue;
        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime NextRunTime
        {
            get { return _NextRunTime; }
        }
        /// <summary>
        /// 后续将要执行的时间点列表
        /// 用于支持
        /// </summary>
        private Queue<DateTime> _RunTimeList = new Queue<DateTime>();
        /// <summary>
        /// 时间队列是否已经结束，如果没有结束，则后面还可以再构建
        /// </summary>
        private bool _IsTimeQueueEnd = false;

        private Task _Task;
        /// <summary>
        /// 任务
        /// </summary>
        public Task Task
        {
            get { return _Task; }
        }

        private TaskRuningState _RunState;
        /// <summary>
        /// 当前任务的执行状态
        /// </summary>
        public TaskRuningState RunState
        {
            get { return _RunState; }
        }

        private DateTime _LastRunTime = DateTime.MinValue;
        /// <summary>
        /// 上次运行时间
        /// </summary>
        public DateTime LastRunTime
        {
            get { return _LastRunTime; }
        }

        private IWorker _Worker;
        /// <summary>
        /// 任务对应的执行者
        /// </summary>
        public IWorker Worker
        {
            get { return _Worker; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parabll"></param>
        public WorkingTask(Task task, BLL.IBLLLogic parabll, DateTime paraLastRunDate)
        {
            _Task = task;

            #region 根据任务的不同构造不同的任务执行者
            this._Worker = Factory.GetWorker(this, parabll, _Task.TaskAssembly.AssemblyType);
            #endregion

            _LastRunTime = paraLastRunDate;
            if (_Task.TaskEntity.DateEnd < DateTime.Now)
            {
                _RunState = TaskRuningState.OutTime;
            }
            else
            {
                _RunState = TaskRuningState.Waite;
            }
            this.RebuildTaskRunTimeList();
        }

        /// <summary>
        /// 通知，任务已经开始
        /// </summary>
        public void Notify_WorkStarted()
        {
            _RunState = TaskRuningState.Runing;
            _LastRunTime = DateTime.Now;

            //如果队列中还有需要执行的日期，则取出一个
            UpdateNextRunTime();

            //如果没有过期，并且时间队列还没有结束，则继续构建
            if (_RunState != TaskRuningState.OutTime &&
                !this._IsTimeQueueEnd &&
                _RunTimeList.Count == 0) //暂定小于5的时候开始构建
            {
                this.BuildTimeQueueByLastRunTime();
            }
        }

        /// <summary>
        /// 通知，任务已经完成
        /// </summary>
        public void Notify_WorkComplete()
        {
            if (_Task.TaskEntity.DateEnd < DateTime.Now)
            {
                _RunState = TaskRuningState.OutTime;
            }
            else
            {
                _RunState = TaskRuningState.Waite;
            }
        }

        #region Private Function

        /// <summary>
        /// 根据上次运行时间构建时间队列
        /// <remarks>暂定一次构建100个</remarks>
        /// <see cref="这个算法写了很长时间,应该改为策略模式"/>
        /// </summary>
        /// <returns></returns>
        private void BuildTimeQueueByLastRunTime()
        {
            if (_RunState != TaskRuningState.OutTime)
            {
                DateTime dtNow = DateTime.Now;
                if (_Task.TaskEntity.RunSpaceType == TaskFrequence.Once)
                {
                    if (_Task.TaskEntity.DateStart >= dtNow)
                    {
                        //{//如果执行频率为只一次，并且还没到开始时间，并且上次执行时间离现在大于1天
                        //if (!_RunTimeList.Contains(_Task.TaskEntity.DateStart))
                        //{
                        _RunTimeList.Enqueue(_Task.TaskEntity.DateStart);
                        _IsTimeQueueEnd = true;
                        //}
                        //}
                    }
                    return;
                }
                else if (_Task.TaskEntity.RunSpaceType == TaskFrequence.Month)
                {
                    DateTime dtBuildStart = _Task.TaskEntity.DateStart;
                    while (dtBuildStart < dtNow) dtBuildStart = dtBuildStart.AddMonths(1);
                    int i = 0;
                    while (i < 100)//构建100个
                    {
                        DateTime dtThis = dtBuildStart.AddMonths(1 * i);
                        if (dtThis <= _Task.TaskEntity.DateEnd)
                        {
                            if (!_RunTimeList.Contains(dtThis))
                            {
                                _RunTimeList.Enqueue(dtThis);
                            }
                        }
                        else
                        {
                            _IsTimeQueueEnd = true;
                            break;
                        }
                        i++;
                    }
                    return;
                }
                else
                {
                    DateTime dtBuildStart = _Task.TaskEntity.DateStart;
                    while (dtBuildStart < dtNow) dtBuildStart = dtBuildStart.AddSeconds(_Task.TaskEntity.RunSpaceTime);
                    int i = 0;
                    while (i < 100)//构建100个
                    {
                        DateTime dtThis = dtBuildStart.AddSeconds(_Task.TaskEntity.RunSpaceTime * i);
                        if (dtThis <= _Task.TaskEntity.DateEnd)
                        {
                            if (!_RunTimeList.Contains(dtThis))
                            {
                                _RunTimeList.Enqueue(dtThis);
                            }
                        }
                        else
                        {
                            _IsTimeQueueEnd = true;
                            break;
                        }
                        i++;
                    }
                    return;
                }

            }
        }

        


        /// <summary>
        /// 重构构建任务执行的时间列表
        /// [对任务有更新的时候重新构建]
        /// </summary>
        private void RebuildTaskRunTimeList()
        {
            //因为是更新，所以需要重新设置状态，后面再去计算
            _RunState = TaskRuningState.Waite;

            _IsTimeQueueEnd = false;
            _RunTimeList.Clear();

            this.BuildTimeQueueByLastRunTime();

            UpdateNextRunTime();

        }

        

        /// <summary>
        /// 更新下次执行时间
        /// </summary>
        private void UpdateNextRunTime()
        {
            if (_RunTimeList.Count > 0)
            {
                _NextRunTime = _RunTimeList.Dequeue();
            }
            else
            {
                _NextRunTime = DateTime.MaxValue;
                _RunState = TaskRuningState.OutTime;
            }
        }

        #endregion

        #region UpdateTask


        /// <summary>
        /// 更新任务属性
        /// </summary>
        /// <param name="taskEntity">The task entity.</param>
        public void UpdateTask(TaskEntity taskEntity)
        {
            this._Task.TaskEntity.Name = taskEntity.Name;
            this._Task.TaskEntity.DateEnd = taskEntity.DateEnd;
            this._Task.TaskEntity.DateStart = taskEntity.DateStart;
            this._Task.TaskEntity.Enable = taskEntity.Enable;
            this._Task.TaskEntity.ExtraParaStr = taskEntity.ExtraParaStr;
            this._Task.TaskEntity.RegestesAppName = taskEntity.RegestesAppName;
            this._Task.TaskEntity.RunSpaceTime = taskEntity.RunSpaceTime;
            this._Task.TaskEntity.RunSpaceType = taskEntity.RunSpaceType;
            this._Task.TaskEntity.RunTimeOutSecs = taskEntity.RunTimeOutSecs;

            this.RebuildTaskRunTimeList();
        }

        #endregion

        #region IDisposeble
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="WorkingTask"/> is reclaimed by garbage collection.
        /// </summary>
        ~WorkingTask()
        {
            //垃圾回收器将调用该方法，因此参数需要为false。
            Dispose(false);
        }
        /// <summary>
        /// 是否已经调用了 Dispose(bool disposing)方法。
        /// 应该定义成 private 的，这样可以使基类和子类互不影响。
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// 所有回收工作都由该方法完成。
        /// 子类应重写(override)该方法。
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // 避免重复调用 Dispose 。
            if (!disposed) return;
            // 适应多线程环境，避免产生线程错误。
            lock (this)
            {
                if (disposing)
                {
                    //------------------------------------------------
                    // 在此处写释放托管资源的代码
                    // (1) 有 Dispose() 方法的，调用其 Dispose() 方法。
                    // (2) 没有 Dispose() 方法的，将其设为 null。
                    // 例如：
                    //     xxDataTable.Dispose();
                    //     xxDataAdapter.Dispose();
                    //     xxString = null;
                    // ------------------------------------------------
                    this._RunTimeList = null;
                    this._Task = null;
                    this._Worker = null;
                }
                // ------------------------------------------------
                // 在此处写释放非托管资源
                // 例如：
                //     文件句柄等
                // ------------------------------------------------
                disposed = true;
            }
        }

        /// <summary>
        /// 该方法由程序调用，在调用该方法之后对象将被终结。
        /// 该方法定义在IDisposable接口中。
        /// </summary>
        public void Dispose()
        {
            //因为是由程序调用该方法的，因此参数为true。
            Dispose(true);
            //因为我们不希望垃圾回收器再次终结对象，因此需要从终结列表中去除该对象。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
