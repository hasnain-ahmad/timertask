using System;

namespace Component.TimerTask.Model.Enums
{
    [Serializable]
    /// <summary>
    /// 任务执行频率
    /// </summary>
    public enum TaskFrequence
    {
        /// <summary>
        /// 执行一次
        /// </summary>
        Once = -1,

        /// <summary>
        /// 每月执行一次
        /// </summary>
        Month = 0,

        /// <summary>
        /// 每周执行一次
        /// </summary>
        Week = 1,

        /// <summary>
        /// 每天执行一次
        /// </summary>
        Day = 2,

        /// <summary>
        /// 小时
        /// </summary>
        Hour = 3,

        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 4,
        
        /// <summary>
        /// 自定义秒
        /// </summary>
        CustomSecs = 100
    }
}
