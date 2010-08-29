/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : TaskRuningState.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 任务执行状态
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;

namespace Component.TimerTask.Model.Enums
{
    [Serializable]
    /// 任务执行状态
    public enum TaskRuningState
    {
        /// <summary>
        /// 执行错误
        /// </summary>
        Error = 0,
        /// <summary>
        /// 等待执行
        /// </summary>
        Waite = 1,
        /// <summary>
        /// 执行中
        /// </summary>
        Runing = 2,
        /// <summary>
        /// 过期
        /// </summary>
        OutTime = 3
    }
}
