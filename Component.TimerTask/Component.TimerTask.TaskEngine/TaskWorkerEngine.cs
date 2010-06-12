using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using Component.TimerTask.Utility;
using System.Threading;
using System.Collections;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 定时任务执行引擎
    /// </summary>
    internal class TaskWorkerEngine : ITaskWorkerEngine
    {
        private List<IWorkingTask> _TaskList = new List<IWorkingTask>();

        private SocketService _SocketService;

        private BLL.IBLLLogic _IBLLLogic;

        private Thread _EngineThread;

        /// <summary>
        /// Construction Function
        /// </summary>
        public TaskWorkerEngine(int paraIdleSecs)
        {
            _IBLLLogic = BLL.BLlFactory.GetBllLogic();
            _IdleSpanInMSecs = paraIdleSecs;
        }

        #region Private Function

        /// <summary>
        /// 引擎线程函数
        /// </summary>
        private void ThreadFuncEngine()
        {
            while (true)
            {
                //开始循环处理任务
                Thread.Sleep(_IdleSpanInMSecs * 1000);
                lock (((ICollection)_TaskList).SyncRoot)
                {
                    foreach (WorkingTask task in _TaskList)
                    {
                        //处理到期的任务
                        if (IsThisTaskOnTime(task))
                        {
                            task.Worker.DoWork(RunTaskType.TaskListInTime);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 该任务是否到时间并且可以执行
        /// <remarks>这个算法写了很长时间</remarks>
        /// </summary>
        /// <param name="paraWorkTask"></param>
        //[STAThread]
        private bool IsThisTaskOnTime(WorkingTask paraWorkTask)
        {
            DateTime dtNow = DateTime.Now;
            return paraWorkTask.Task.TaskEntity.Enable &&   //可用
                paraWorkTask.RunState != TaskRuningState.OutTime &&　//没有超时
                dtNow >= paraWorkTask.NextRunTime; //间隔到了
                
        }

        /// <summary>
        /// 获取所有的工作列表
        /// [第一次加载 从数据库]
        /// </summary>
        /// <returns></returns>
        private List<IWorkingTask> GetWorkingTask()
        {
            List<IWorkingTask> list = new List<IWorkingTask>();
            List<Task> tasks = _IBLLLogic.GetTaskList();
            foreach (Task t in tasks)
            {
                list.Add(GetWorkingTask(t));
            }
            return list;
        }

        /// <summary>
        /// 构造一个工作列表
        /// [根据日志]
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private IWorkingTask GetWorkingTask(Task task)
        {
            DateTime date = _IBLLLogic.GetTaskLastRunTime(task.TaskEntity.ID);
            IWorkingTask wt = new WorkingTask(task, _IBLLLogic, date);
            return wt;
        }
        #endregion

        #region ITaskWorkerEngine 成员
        private int _IdleSpanInMSecs = 2;
        public int IdleSpanInMSecs
        {
            get
            {
                return _IdleSpanInMSecs;
            }
            set
            {
                _IdleSpanInMSecs = value;
            }
        }

        private bool _IsRuning = false;
        public bool IsRuning
        {
            get { return _IsRuning; }
        }

        public bool Start()
        {
            if (_SocketService == null)
            {
                Console.WriteLine("工作者引擎启动");

                //开始Socket监听
                _SocketService = new SocketService(this, _IBLLLogic);
                _SocketService.StartListen(SocketHelper.GetIpEndPoint());

                //构建TaskList
                _TaskList = GetWorkingTask();

                Console.WriteLine("加载任务队列" + _TaskList.Count + "条");

                //启动线程
                _EngineThread = new Thread(new ThreadStart(ThreadFuncEngine));
                _EngineThread.IsBackground = true;
                _EngineThread.Start();

                _IsRuning = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        #region 操作任务
        public bool Stop()
        {
            return false;
        }

        public TaskRuningState GetTaskRuningState(Int64 paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask t = _TaskList.Find(delegate(IWorkingTask task) { return task.Task.TaskEntity.ID == paraTaskId; });
                if (t != null)
                {
                    return t.RunState;
                }
            }
            return TaskRuningState.Error;
        }

        public bool StopRuningTask(Int64 paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
                if (task != null)
                {
                    task.Worker.EnforceKillWork();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ManualRunTask(Int64 paraTaskId, RunTaskType paraRunType)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
                if (task != null)
                {
                    task.Worker.DoWork(paraRunType);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #endregion

        #region 维护任务列表

        public void AddWorkingTask(TaskEntity paraTask)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                _TaskList.Add(GetWorkingTask(_IBLLLogic.GetTask(paraTask)));
            }
        }

        public void ModifyTask(TaskEntity paraTask)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTask.ID; });
                if (task != null)
                {
                    task.Task.TaskEntity.Name = paraTask.Name;
                    task.Task.TaskEntity.DateEnd = paraTask.DateEnd;
                    task.Task.TaskEntity.DateStart = paraTask.DateStart;
                    task.Task.TaskEntity.Enable = paraTask.Enable;
                    task.Task.TaskEntity.ExtraParaStr = paraTask.ExtraParaStr;
                    task.Task.TaskEntity.RegestesAppName = paraTask.RegestesAppName;
                    task.Task.TaskEntity.RunSpaceTime = paraTask.RunSpaceTime;
                    task.Task.TaskEntity.RunSpaceType = paraTask.RunSpaceType;
                    task.Task.TaskEntity.RunTimeOutSecs = paraTask.RunTimeOutSecs;

                    task.RebuildTaskRunTimeList();
                }

            }
        }

        public void DelTask(long paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
                _TaskList.Remove(task);
            }
        }

        #endregion

    }
}
