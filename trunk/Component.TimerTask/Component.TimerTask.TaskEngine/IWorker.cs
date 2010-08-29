/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : IWorker.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 工作者接口
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 工作者接口
    /// </summary>
    interface IWorker
    {
        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="paraRunType">被调度方式</param>
        void DoWork(RunTaskType paraRunType);

        /// <summary>
        /// 手动停止工作
        /// </summary>
        void ManualStopWork();
    }
}
