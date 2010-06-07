// File:    ITimeWorkTask.cs
// Author:  JinMingLv
// Created: 2010年5月31日 10:32:29
// Purpose: Definition of Interface ITimeWorkTask

using System;


namespace BOCO.TimerTask.ITimerComponent
{
    /// <summary>
    /// 线程结束的通知委托
    /// </summary>
    public delegate void DEL_THREADCOMPLETE(object sender, EventArgs e);

    /// <summary>
    /// 定义定时任务要实现的接口
    /// </summary>
    public abstract class ITimeWorkTask
    {
        private DEL_THREADCOMPLETE _ThreadCompleteFunc;
        /// <summary>
        /// 通知外部事件，线程执行结束
        /// </summary>
        public DEL_THREADCOMPLETE ThreadCompleteFunc
        {
            set { _ThreadCompleteFunc = value; }
        }

        //public ITimeWorkTask()
        //{
        //}

        /// <summary>
        /// 执行任务的附加参数
        /// </summary>
        protected string ExtraParaStr { get; set; }
        /// <summary>
        /// 具体执行任务的方法
        /// <remarks>重写该方法后，请在方法后面调用base.TaskExecuteFunc()；否则线程任务执行结束事件无法通知管理引擎</remarks>
        /// </summary>
        public virtual void TaskExecuteFunc()
        {
            if (_ThreadCompleteFunc != null)
            {
                _ThreadCompleteFunc.Invoke(this, null);
            }
        }

        /// <summary>
        /// 停止正在执行的任务
        /// </summary>
        public abstract void StopRuning();

    }

    #region 旧接口
    ///// <summary>
    ///// 定义定时任务要实现的接口
    ///// </summary>
    //public interface ITimeWorkTask
    //{
    //    /// <summary>
    //    /// 执行任务的附加参数
    //    /// </summary>
    //    string ExtraParaStr { get; set; }
    //    /// <summary>
    //    /// 具体执行任务的方法
    //    /// </summary>
    //    void TaskExecuteFunc();

    //    /// <summary>
    //    /// 停止正在执行的任务
    //    /// </summary>
    //    void StopRuning();

    //}
    #endregion
}