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
            //set { _NextRunTime = value; }
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
            if (_Task.TaskAssembly.AssemblyType == AssemblyType.Exe)
            {
                _Worker = new Worker_Excutable(this, parabll);
            }
            else if (_Task.TaskAssembly.AssemblyType == AssemblyType.Dll)
            {
                _Worker = new Worker_Assembly(this, parabll);
            }
            else
                throw new Exception("尚未定义的工作类型,AssemblyType:" + _Task.TaskAssembly.AssemblyType.ToString());
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


        /// <summary>
        /// 根据上次运行时间构建时间队列
        /// <remarks>暂定一次构建100个</remarks>
        /// <see cref="这个算法写了很长时间"/>
        /// </summary>
        /// <returns></returns>
        private void BuildTimeQueueByLastRunTime()
        {
            if (_RunState != TaskRuningState.OutTime)
            {
                DateTime dtNow = DateTime.Now;
                if (_Task.TaskEntity.RunSpaceType == TaskFrequence.Once)
                {
                    if (_Task.TaskEntity.DateStart < dtNow && _LastRunTime < dtNow.AddDays(-1))
                    {
                        {//如果执行频率为只一次，并且还没到开始时间，并且上次执行时间离现在大于1天
                            //if (!_RunTimeList.Contains(_Task.TaskEntity.DateStart))
                            //{
                                _RunTimeList.Enqueue(_Task.TaskEntity.DateStart);
                                _IsTimeQueueEnd = true;
                            //}
                        }
                    }
                    return;
                }
                else if (_Task.TaskEntity.RunSpaceType == TaskFrequence.Month)
                {
                    DateTime dtBuildStart = _LastRunTime.AddMonths(1);
                    if (_Task.TaskEntity.DateStart > _LastRunTime) dtBuildStart = _Task.TaskEntity.DateStart;
                    int i = 0;
                    while (i < 100)//构建100个
                    {
                        DateTime dtThis = dtBuildStart.AddMonths(i);
                        if (dtThis <= _Task.TaskEntity.DateEnd)
                        {
                            _RunTimeList.Enqueue(dtThis);
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

        #region IWorkingTask 成员


        public void RebuildTaskRunTimeList()
        {
            _RunState = TaskRuningState.Waite;
            _IsTimeQueueEnd = false;
            _RunTimeList.Clear();
            this.BuildTimeQueueByLastRunTime();

            UpdateNextRunTime();
            
        }

        #endregion

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
    }
}
