// File:    TaskEntity.cs
// Author:  LvJinMing
// Created: 2010年5月28日 9:26:17
// Purpose: Definition of Enum TaskRuningState

using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.Model
{
    /// <summary>
    /// 定时任务实体
    /// </summary>
    [Serializable]
    public class TaskEntity
    {
        private Int64 _ID;
        /// <summary>
        /// 任务ID(如果是新建计划，这个字段可以不用设置)
        /// </summary>
        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private bool _Enable;
        /// <summary>
        /// 任务是否可用
        /// </summary>
        public bool Enable
        {
            get { return _Enable; }
            set { _Enable = value; }
        }

        private DateTime _DateStart;
        /// <summary>
        /// 任务开始执行时间
        /// </summary>
        public DateTime DateStart
        {
            get { return _DateStart; }
            set { _DateStart = value; }
        }

        private DateTime _DateEnd;
        /// <summary>
        /// 任务结束执行时间
        /// </summary>
        public DateTime DateEnd
        {
            get { return _DateEnd; }
            set { _DateEnd = value; }
        }

        private Int64 _RunSpaceTime = 1;
        /// <summary>
        /// 任务执行间隔[秒]
        /// </summary>
        public Int64 RunSpaceTime
        {
            get { return _RunSpaceTime; }
            set { _RunSpaceTime = value; }
        }

        private TaskFrequence _RunSpaceType;
        /// <summary>
        /// 任务执行频率类型
        /// </summary>
        public TaskFrequence RunSpaceType
        {
            get { return _RunSpaceType; }
            set { _RunSpaceType = value; }
        }

        private string _ExtraParaStr;
        /// <summary>
        /// 接口执行附加参数
        /// </summary>
        public string ExtraParaStr
        {
            get { return _ExtraParaStr; }
            set { _ExtraParaStr = value; }
        }

        private Int64 _RunTimeOutSecs = -1;
        /// <summary>
        /// 任务执行超时时间,如果超时时间到了,任务没有执行结束,则强制结束,不限制设为-1
        /// </summary>
        public Int64 RunTimeOutSecs
        {
            get { return _RunTimeOutSecs; }
            set { _RunTimeOutSecs = value; }
        }

        private string _RegestesAppName;

        public string RegestesAppName
        {
            get { return _RegestesAppName; }
            set { _RegestesAppName = value; }
        }

        public TaskEntity() { }

        public TaskEntity(Int64 paraID,
            string paraName,
            bool paraEnable,
            DateTime paraDateStart,
            DateTime paraDateEnd,
            Int64 paraRunSpaceTime,
            TaskFrequence paraRunSpaceType,
            string paraExtraParaStr,
            //string paraExeCommandParaMeter,
            Int64 paraRunTimeOutSecs,
            string paraRegestesAppName
            )
        {
            _ID = paraID;
            _Name = paraName;
            _Enable = paraEnable;
            _DateStart = paraDateStart;
            _DateEnd = paraDateEnd;
            _RunSpaceTime = paraRunSpaceTime;
            _RunSpaceType = paraRunSpaceType;
            _ExtraParaStr = paraExtraParaStr;
            //_ExeCommandParaMeter = paraExeCommandParaMeter;
            _RunTimeOutSecs = paraRunTimeOutSecs;
            _RegestesAppName = paraRegestesAppName;
        }

        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="paraID"></param>
        public void SetKeyID(Int64 paraID)
        {
            _ID = paraID;
        }

    }
}
