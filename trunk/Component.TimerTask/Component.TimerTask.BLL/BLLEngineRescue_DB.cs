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
