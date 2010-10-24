/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : TaskEntity.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 定时任务实体
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
            //set { _ID = value; }
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

        /// <summary>
        /// 返回计划的文本描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "计划ID {0},名称 {1},开始执行日期 {2},结束执行日期 {3},附加参数 {4},执行间隔 {5}秒,执行方式 {6},执行程序 {7}.",
                this._ID,
                this._Name,
                this._DateStart,
                this._DateEnd,
                this._ExtraParaStr,
                this._RunSpaceTime,
                this._RunSpaceType,
                this._RegestesAppName);
            //return base.ToString();
        }

    }
}
