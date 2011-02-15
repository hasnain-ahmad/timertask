/*******************************************************************************
 * * 版权所有(C) LJM Info 2011
 * * 文件名称   : BLLEngineRescue_DB.cs
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
    class BLLEngineRescue_DB : IBLLEngineRescue
    {

        private DAL.IDataAccess _DA = DAL.DALFactory.GetDataAccess();
        /// <summary>
        /// 上次接收到的时间
        /// </summary>
        private DateTime _LastRecieveDate = DateTime.MinValue;

        #region IBLLEngineRescue 成员

        public void WriteHeart()
        {
            _DA.WriteHeartDate();
        }

        public void StartRecieveHeartData()
        {
            //do nothing
        }

        public bool IsNotRecievedLongTime(int timeOutSeconds)
        {
            this._LastRecieveDate = _DA.ReadHeartDate();
            DateTime dt = DateTime.Now;
            TimeSpan ts = dt - this._LastRecieveDate;
            if (ts.Seconds > timeOutSeconds)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
