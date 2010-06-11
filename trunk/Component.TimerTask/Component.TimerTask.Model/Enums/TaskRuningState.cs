// File:    TaskRuningState.cs
// Author:  LvJinMing
// Created: 2010年5月28日 9:26:17
// Purpose: Definition of Enum TaskRuningState

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
