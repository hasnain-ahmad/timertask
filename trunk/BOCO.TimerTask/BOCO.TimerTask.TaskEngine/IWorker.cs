using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.TaskEngine
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

        void EnforceKillWork();
    }
}
