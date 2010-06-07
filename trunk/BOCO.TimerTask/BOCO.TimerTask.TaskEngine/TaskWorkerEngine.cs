using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using BOCO.TimerTask.Utility;
using System.Threading;
using System.Collections;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.TaskEngine
{
    /// <summary>
    /// 定时任务执行引擎
    /// </summary>
    internal class TaskWorkerEngine : ITaskWorkerEngine
    {
        private List<WorkingTask> _TaskList = new List<WorkingTask>();

        private SocketService _SocketService;

        private BLL.IBLLLogic _IBLLLogic;

        private Thread _EngineThread;

        /// <summary>
        /// Construction Function
        /// </summary>
        public TaskWorkerEngine()
        {
            _IBLLLogic = BLL.BLlFactory.GetBllLogic();
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
                        TimeSpan ts = DateTime.Now - task.LastRunTime;
                        if (task.Task.TaskEntity.Enable && task.RunState != TaskRuningState.OutTime && ts.TotalSeconds >= task.Task.TaskEntity.RunSpaceTime)
                        {
                            task.Worker.DoWork( RunTaskType.TaskListInTime);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有的工作列表
        /// </summary>
        /// <returns></returns>
        private List<WorkingTask> GetWorkingTask()
        {
            List<WorkingTask> list = new List<WorkingTask>();
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
        private WorkingTask GetWorkingTask(Task task)
        {
            WorkingTask wt = new WorkingTask(task, _IBLLLogic);
            DateTime date = _IBLLLogic.GetTaskLastRunTime(task.TaskEntity.ID);
            wt.SetTaskCurrentState(date);
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
            return true;
        }

        public TaskRuningState GetTaskRuningState(Int64 paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                WorkingTask t = _TaskList.Find(delegate(WorkingTask task) { return task.Task.TaskEntity.ID == paraTaskId; });
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
                WorkingTask task = _TaskList.Find(delegate(WorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
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
                WorkingTask task = _TaskList.Find(delegate(WorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
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
                WorkingTask task = _TaskList.Find(delegate(WorkingTask wt) { return wt.Task.TaskEntity.ID == paraTask.ID; });
                if (task != null)
                {
                    task.Task.TaskEntity.Name = paraTask.Name;
                    task.Task.TaskEntity.DateEnd = paraTask.DateEnd;
                    task.Task.TaskEntity.DateStart = paraTask.DateStart;
                    task.Task.TaskEntity.Enable = paraTask.Enable;
                    task.Task.TaskEntity.ExeCommandParaMeter = paraTask.ExeCommandParaMeter;
                    task.Task.TaskEntity.ExtraParaStr = paraTask.ExtraParaStr;
                    task.Task.TaskEntity.RegestesAppName = paraTask.RegestesAppName;
                    task.Task.TaskEntity.RunSpaceTime = paraTask.RunSpaceTime;
                    task.Task.TaskEntity.RunSpaceType = paraTask.RunSpaceType;
                    task.Task.TaskEntity.RunTimeOutSecs = paraTask.RunTimeOutSecs;
                }

            }
        }

        public void DelTask(long paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                WorkingTask task = _TaskList.Find(delegate(WorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
                _TaskList.Remove(task);

            }
        }

        #endregion

    }
}
