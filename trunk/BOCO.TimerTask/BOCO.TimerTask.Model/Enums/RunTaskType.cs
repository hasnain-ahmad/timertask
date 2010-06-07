using System;
using System.Collections.Generic;
using System.Text;

namespace BOCO.TimerTask.Model.Enums
{
    /// <summary>
    /// 任务被执行的方式
    /// </summary>
    [Serializable]
    public enum RunTaskType
    {
        /// <summary>
        /// 任务列表中时间到
        /// </summary>
        TaskListInTime,
        /// <summary>
        /// 立即执行不影响后续任务计划
        /// </summary>
        ImmediateNoDisturb,
        /// <summary>
        /// 立即执行，影响后续计划
        /// </summary>
        ImmediateDisturb
    }
}
