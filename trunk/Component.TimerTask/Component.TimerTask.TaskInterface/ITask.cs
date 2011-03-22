/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : ITask.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 线程结束的通知委托
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * * 2011/3/22      V2.1            吕金明      修改之前不太合理的需要用户写trycatch的逻辑，修改完成回调由线程池完成
 * ********************************************************************************/
using System;


namespace Component.TimerTask.TaskInterface
{

    /// <summary>
    /// 定时任务要实现的接口
    /// </summary>
    public abstract class ITask
    {

        #region Normal

        private System.Threading.WaitCallback _OnFuncComplete = null;
        /// <summary>
        /// 通知外部事件，线程执行结束
        /// </summary>
        /// <remarks>调度引擎本身调用，其它部件调用无效</remarks>
        /// <value>The thread complete func.</value>
        public System.Threading.WaitCallback OnFuncComplete
        {
            set { _OnFuncComplete = value; }
        }

        /// <summary>
        /// 执行任务的附加参数
        /// </summary>
        /// <value>The extra para STR.</value>
        public string ExtraParaStr { get; set; }

        #endregion

        #region Normal Methed

        /// <summary>
        /// 具体执行任务的方法
        /// <remarks>重写该方法后，请在方法后面调用base.RunTask()；否则线程任务执行结束事件无法通知管理引擎</remarks>
        /// </summary>
        public void RunTask()
        {
            try
            {
                this.StartRuning();
            }
            catch (Exception ex)
            {
                Console.WriteLine("调用的任务线程中出现异常：" + ex.Message);
            }

            if (_OnFuncComplete != null)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(this._OnFuncComplete);
            }
        }

        #endregion

        #region Abstract Method
        /// <summary>
        /// 停止正在执行的任务
        /// </summary>
        public abstract void StopRuning();

        /// <summary>
        /// 开始执行任务
        /// </summary>
        public abstract void StartRuning();

        #endregion

    }
}