/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : TaskFrequence.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 任务执行频率
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
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
