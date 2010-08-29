/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : Task.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 任务富实体
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.Model
{
    /// <summary>
    /// 任务富实体
    /// </summary>
    [Serializable]
    public class Task
    {
        private TaskEntity _TaskEntity;
        /// <summary>
        /// TaskEntity
        /// </summary>
        public TaskEntity TaskEntity
        {
            get { return _TaskEntity; }
        }

        private TaskAssembly _TaskAssembly;
        /// <summary>
        /// TaskAssembly
        /// </summary>
        public TaskAssembly TaskAssembly
        {
            get { return _TaskAssembly; }
        }


        private Task() { }

        public Task(TaskEntity paraEntity, TaskAssembly paraAssembly)
        {
            _TaskEntity = paraEntity;
            _TaskAssembly = paraAssembly;
        }
    }
}
