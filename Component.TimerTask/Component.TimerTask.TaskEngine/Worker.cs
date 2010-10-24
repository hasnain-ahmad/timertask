/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : Worker.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 工作者模式： 工作执行者
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
    /// 工作者模式： 工作执行者
    /// <remarks>定义为抽象类,防止直接去创建这个类</remarks>
    /// </summary>
    internal abstract class Worker : IWorker
    {
        protected BLL.IBLLLogic _BLL;
        protected WorkingTask _WrkTask;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraTask"></param>
        /// <param name="paraBll"></param>
        public Worker(WorkingTask paraTask, BLL.IBLLLogic paraBll)
        {
            _WrkTask = paraTask;
            _BLL = paraBll;
        }

        #region private function

        /// <summary>
        /// 进程结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Process_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("**************************  Work End {0}", _WrkTask.Task.TaskEntity.Name);

            #region 记录到日志中
            string log = _WrkTask.Task.TaskEntity.Name + " Work Complete.";
            _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, log, LogType.TaskRunEnd);
            #endregion

            #region 更新下一步工作
            _WrkTask.Notify_WorkComplete();
            #endregion

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region IWorker 成员

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="paraRunType">被调度方式</param>
        public virtual void DoWork(RunTaskType paraRunType)
        {

            #region 记录到日志中
            string log = _WrkTask.Task.TaskEntity.Name + " Start Working.";

            LogType logtype = LogType.TaskRunStart;
            switch (paraRunType)
            {
                case RunTaskType.TaskListInTime:
                    logtype = LogType.TaskRunStart;
                    break;
                case RunTaskType.ImmediateNoDisturb:
                    logtype = LogType.TaskRunStart_Immediate;
                    break;
                //case RunTaskType.ImmediateDisturb:
                //    logtype = LogType.TaskRunStart_Immediate_Interupt;
                //    break;
            }

            _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, log, logtype);
            #endregion

            #region 更新下一步工作
            if (paraRunType != RunTaskType.ImmediateNoDisturb)
            {
                _WrkTask.Notify_WorkStarted();
            }
            #endregion
            Console.WriteLine("{0} 下次执行时间:{1}", _WrkTask.Task.TaskEntity.Name, _WrkTask.NextRunTime);
        }

        public virtual void ManualStopWork()
        {
            #region 记录到日志中
            _BLL.WriteLog(_WrkTask.Task.TaskEntity.ID, _WrkTask.Task.TaskEntity.Name, "EnforceKillWork", LogType.EnforceKillWork);
            #endregion
        }

        #endregion
    }
}
