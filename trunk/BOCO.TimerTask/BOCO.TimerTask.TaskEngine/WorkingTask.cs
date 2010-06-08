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

        private DateTime _LastRunTime;
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
        internal IWorker Worker
        {
            get { return _Worker; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parabll"></param>
        public WorkingTask(Task task, BLL.IBLLLogic parabll)
        {
            _Task = task;

            //根据任务的不同构造不同的任务执行者
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
        }

        /// <summary>
        /// 被通知，任务已经开始
        /// </summary>
        public void Notify_WorkStarted()
        {
            _RunState = TaskRuningState.Runing;
            _LastRunTime = DateTime.Now;
        }

        /// <summary>
        /// 被通知，任务已经完成
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
        /// 设置上次执行时间，并计算工作状态
        /// ［从数据库中读取出来的任务，需要设置初始状态]
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
