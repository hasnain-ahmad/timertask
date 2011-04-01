/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : TaskWorkerEngine.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 定时任务执行引擎
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Utility;
using Component.TimerTask.BLL;

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

        //private Thread _EngineThread;

        /// <summary>
        /// Construction Function
        /// </summary>
        public TaskWorkerEngine(int paraIdleSecs)
        {
            _IBLLLogic = BLL.BLLFactory.GetBllLogic();
            _IdleSpanInMSecs = paraIdleSecs;
        }

        #region Private Function

        /// <summary>
        /// 引擎线程函数
        /// </summary>
        private void ThreadFuncEngine(object state)
        {
            IBLLEngineRescue ibllEngineRescue = BLLFactory.GetBLLEngineRes();
            while (true)
            {
                //发送心跳数据
                ibllEngineRescue.WriteHeart();

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

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
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
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadFuncEngine));
                //_EngineThread = new Thread(new ThreadStart(ThreadFuncEngine));
                //_EngineThread.IsBackground = true;
                //_EngineThread.Start();

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

        /// <summary>
        /// 查询任务的执行状态
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 手动停止一个正在执行的任务
        /// [不影响后续任务执行]
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        public bool StopRuningTask(Int64 paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
                if (task != null)
                {
                    task.Worker.ManualStopWork();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 手动执行一个任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <param name="paraRunType"></param>
        /// <returns></returns>
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
                    Console.WriteLine("任务列表中不存在ID为：{0}的任务。", paraTaskId);
                    return false;
                }
            }
        }
        #endregion

        #endregion

        #region 维护任务列表

        /// <summary>
        /// 向引擎中添加任务
        /// </summary>
        /// <param name="paraTask"></param>
        public void AddWorkingTask(TaskEntity paraTask)
        {
            IWorkingTask itask = null;
            lock (((ICollection)_TaskList).SyncRoot)
            {
                itask = GetWorkingTask(_IBLLLogic.GetTask(paraTask));
                _TaskList.Add(itask);
            }
            Console.WriteLine("新增一条任务,下次执行时间为：{0}：{1}", itask.NextRunTime, paraTask.ToString());
        }

        /// <summary>
        /// 修改引擎中的任务
        /// </summary>
        /// <param name="paraTask"></param>
        public void ModifyTask(TaskEntity paraTask)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTask.ID; });
                if (task != null)
                {
                    task.UpdateTask(paraTask);
                    //task.Task.TaskEntity.Name = paraTask.Name;
                    //task.Task.TaskEntity.DateEnd = paraTask.DateEnd;
                    //task.Task.TaskEntity.DateStart = paraTask.DateStart;
                    //task.Task.TaskEntity.Enable = paraTask.Enable;
                    //task.Task.TaskEntity.ExtraParaStr = paraTask.ExtraParaStr;
                    //task.Task.TaskEntity.RegestesAppName = paraTask.RegestesAppName;
                    //task.Task.TaskEntity.RunSpaceTime = paraTask.RunSpaceTime;
                    //task.Task.TaskEntity.RunSpaceType = paraTask.RunSpaceType;
                    //task.Task.TaskEntity.RunTimeOutSecs = paraTask.RunTimeOutSecs;

                    //task.RebuildTaskRunTimeList();
                    Console.WriteLine("更新一条任务：{0}", paraTask.ToString());
                }
                else
                {
                    Console.WriteLine("需要更新的任务在任务列表中没有({0})", paraTask.ToString());
                }

            }
            
        }

        /// <summary>
        /// 删除引擎中的任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        public void DelTask(long paraTaskId)
        {
            lock (((ICollection)_TaskList).SyncRoot)
            {
                IWorkingTask task = _TaskList.Find(delegate(IWorkingTask wt) { return wt.Task.TaskEntity.ID == paraTaskId; });
                if (task != null)
                {
                    _TaskList.Remove(task);
                    task.Dispose();
                    Console.WriteLine("删除一条任务 ID：{0}", paraTaskId);
                }
            }
            
        }

        #endregion

    }
}
