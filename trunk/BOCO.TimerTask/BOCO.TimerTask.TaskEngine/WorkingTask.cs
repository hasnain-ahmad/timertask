using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.TaskEngine
{
    /// <summary>
    /// 工作者模式-待执行任务实体
    /// </summary>
    internal class WorkingTask
    {
        private Task _Task;

        public Task Task
        {
            get { return _Task; }
            set { _Task = value; }
        }

        private TaskRuningState _RunState;

        public TaskRuningState RunState
        {
            get { return _RunState; }
            set { _RunState = value; }
        }

        private DateTime _LastRunTime;

        public DateTime LastRunTime
        {
            get { return _LastRunTime; }
            set { _LastRunTime = value; }
        }

        //private DateTime _NextRunTime;

        //public DateTime NextRunTime
        //{
        //    get { return _NextRunTime; }
        //    set { _NextRunTime = value; }
        //}

        private IWorker _Worker;
        /// <summary>
        /// 任务对应的执行者
        /// </summary>
        internal IWorker Worker
        {
            get { return _Worker; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parabll"></param>
        public WorkingTask(Task task,BLL.IBLLLogic parabll)
        {
            _Task = task;
            _Worker = new Worker(this, parabll);
        }

        /// <summary>
        /// 工作已经开始
        /// </summary>
        public void Notify_WorkStarted()
        {
            _RunState = TaskRuningState.Runing;
            _LastRunTime = DateTime.Now;
        }

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
        /// 设置上次执行时间，并计算工作状态
        /// </summary>
        /// <param name="paraLastRunTime"></param>
        public void SetTaskCurrentState(DateTime paraLastRunTime)
        {
            _LastRunTime = paraLastRunTime;
            if (_Task.TaskEntity.DateEnd < DateTime.Now)
            {
                _RunState = TaskRuningState.OutTime;
            }
            else
            {
                _RunState = TaskRuningState.Waite;
            }

        }

    }
}
