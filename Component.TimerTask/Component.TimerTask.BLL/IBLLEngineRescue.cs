using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 引擎营救接口
    /// </summary>
    public interface IBLLEngineRescue
    {
        /// <summary>
        /// 发送心跳数据
        /// </summary>
        void WriteHeart();

        /// <summary>
        /// 开始接收心跳数据
        /// </summary>
        void StartRecieveHeartData();

        /// <summary>
        /// 是否长时间未接受到数据
        /// </summary>
        /// <param name="timeOutSeconds">超时时间（超过多长时间算超时）</param>
        /// <returns></returns>
        bool IsNotRecievedLongTime(int timeOutSeconds);
    }
}
