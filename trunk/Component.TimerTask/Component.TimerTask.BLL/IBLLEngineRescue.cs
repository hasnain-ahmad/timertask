/*******************************************************************************
 * * 版权所有(C) LJM Info 2011
 * * 文件名称   : IBLLEngineRescue.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2011年2月15日
 * * 内容摘要   : 
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * 
 * ********************************************************************************/
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
