/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : RunTaskType.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 任务被执行的方式
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.Model.Enums
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
        ///// <summary>
        ///// 立即执行，影响后续计划
        ///// </summary>
        //ImmediateDisturb
    }
}
