// File:    ITimeWorkTask.cs
// Author:  JinMingLv
// Created: 2010年5月31日 10:32:29
// Purpose: Definition of Interface TaskInterface

using System;


namespace Component.TimerTask.TaskInterface
{
    /// <summary>
    /// 线程结束的通知委托
    /// </summary>
    public delegate void DEL_TaskComplete(object sender, EventArgs e);

    /// <summary>
    /// 定时任务要实现的接口
    /// </summary>
    public abstract class ITask
    {

        #region Normal

        private DEL_TaskComplete _ThreadCompleteFunc;
        /// <summary>
        /// 通知外部事件，线程执行结束
        /// </summary>
        /// <value>The thread complete func.</value>
        public DEL_TaskComplete ThreadCompleteFunc
        {
            set { _ThreadCompleteFunc = value; }
        }

        /// <summary>
        /// 执行任务的附加参数
        /// </summary>
        /// <value>The extra para STR.</value>
        public string ExtraParaStr { get; set; }

        #endregion

        #region Virtual Methed

        /// <summary>
        /// 具体执行任务的方法
        /// <remarks>重写该方法后，请在方法后面调用base.RunTask()；否则线程任务执行结束事件无法通知管理引擎</remarks>
        /// </summary>
        public virtual void RunTask()
        {
            if (_ThreadCompleteFunc != null)
            {
                _ThreadCompleteFunc.Invoke(this, null);
            }
        }

        #endregion

        #region Abstract Method
        /// <summary>
        /// 停止正在执行的任务
        /// </summary>
        public abstract void StopRuning();

        #endregion

    }
}