// File:    Task.cs
// Author:  LvJinMing
// Created: 2010年6月4日 15:26:17
// Purpose: Definition of Enum TaskRuningState

using System;
using System.Collections.Generic;
using System.Text;

namespace BOCO.TimerTask.Model
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
