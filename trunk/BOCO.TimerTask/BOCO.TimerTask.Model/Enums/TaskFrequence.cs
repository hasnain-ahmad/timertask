using System;

namespace BOCO.TimerTask.Model.Enums
{
    [Serializable]
    /// <summary>
    /// 任务执行频率
    /// </summary>
    public enum TaskFrequence
    {
        Once = -1,
        Month = 0,
        Week = 1,
        Day = 2,
        Hour = 3,
        Minute = 4,
        /// 自定义小时
        CustomHour = 100
    }
}
