// File:    LogEntity.cs
// Author:  LvJinMing
// Created: 2010年6月4日 15:26:17
// Purpose: Definition of Enum TaskRuningState

using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.Model
{
    /// <summary>
    /// 任务执行日志
    /// </summary>
    [Serializable]
    public class LogEntity
    {
        private Int64 _ID;
        /// <summary>
        /// ID(如果是新增日志，则不用写这个字段)
        /// </summary>
        public Int64 ID
        {
            get { return _ID; }
            //set { _ID = value; }
        }

        private Int64 _TaskID = -1;
        /// <summary>
        /// 日志对应的任务
        /// [注意：如果是非任务日志，则为-1]
        /// </summary>
        public Int64 TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }

        private string _TaskName;

        public string TaskName
        {
            get { return _TaskName; }
            set { _TaskName = value; }
        }

        private DateTime _LogDate;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime LogDate
        {
            get { return _LogDate; }
            //set { _LogDate = value; }
        }

        private LogType _LogType;
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType
        {
            get { return _LogType; }
            set { _LogType = value; }
        }

        private string _LogContent;

        public string LogContent
        {
            get { return _LogContent; }
            set { _LogContent = value; }
        }

        public LogEntity()
        {
            _LogDate = DateTime.Now;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraID"></param>
        /// <param name="paraTaskID"></param>
        /// <param name="paraLogDate"></param>
        /// <param name="paraLogType"></param>
        public LogEntity(Int64 paraID,
            Int64 paraTaskID,
            DateTime paraLogDate,
            LogType paraLogType,
            string paraLogContent,
            string paraTaskName)
        {
            _ID = paraID;
            _LogDate = paraLogDate;
            _LogType = paraLogType;
            _TaskID = paraTaskID;
            _LogContent = paraLogContent;
            _TaskName = paraTaskName;
        }
    }
}
