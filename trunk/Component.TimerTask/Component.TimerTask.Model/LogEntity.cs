/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : LogEntity.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 任务执行日志
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.Model
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
