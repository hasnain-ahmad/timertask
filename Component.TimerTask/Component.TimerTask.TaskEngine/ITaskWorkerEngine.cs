/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : ITaskWorkerEngine.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 定时任务引擎
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Model;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 定时任务引擎
    /// </summary>
    public interface ITaskWorkerEngine
    {
        /// <summary>
        /// 空闲时间（秒）
        /// </summary>
        int IdleSpanInMSecs { get; set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        bool IsRuning { get; }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        bool Start();

        bool Stop();

        /// <summary>
        /// 查询任务的执行状态
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        TaskRuningState GetTaskRuningState(Int64 paraTaskId);

        /// <summary>
        /// 手动执行一个任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        bool ManualRunTask(Int64 paraTaskId, RunTaskType paraRunType);

        /// <summary>
        /// 手动停止一个正在执行的任务
        /// [不影响后续任务执行]
        /// </summary>
        /// <param name="paraTaskId"></param>
        /// <returns></returns>
        bool StopRuningTask(Int64 paraTaskId);

        /// <summary>
        /// 向引擎中添加任务
        /// </summary>
        /// <param name="paraTask"></param>
        void AddWorkingTask(TaskEntity paraTask);
        /// <summary>
        /// 修改引擎中的任务
        /// </summary>
        /// <param name="paraTask"></param>
        void ModifyTask(TaskEntity paraTask);
        /// <summary>
        /// 删除引擎中的任务
        /// </summary>
        /// <param name="paraTaskId"></param>
        void DelTask(Int64 paraTaskId);
    }
}
