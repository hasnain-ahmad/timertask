using System;
using System.Collections.Generic;
using System.Text;
using AppModule.NamedPipes;
using AppModule.InterProcessComm;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 引擎营救类-命名管道方式
    /// </summary>
    class BLLEngineRescue:IBLLEngineRescue
    {
        #region Private Var

        private const string STR_PIPEMSG_KEY = "TimerTask_Engine_Rescure_Pipe_Msg";
        
        /// <summary>
        /// 上次接收到的时间
        /// </summary>
        private DateTime _LastRecieveDate = DateTime.MinValue;


        //private IInterProcessConnection _clientConnection = null;
        #endregion

        #region IBLLEngineRescue 成员

        /// <summary>
        /// 写心跳数据
        /// </summary>
        public void WriteHeart()
        {
            IInterProcessConnection clientConnection = null;
            try
            {
                clientConnection = new ClientPipeConnection(STR_PIPEMSG_KEY, ".");
                clientConnection.Connect();
                clientConnection.Write(DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
                clientConnection.Close();
                clientConnection.Dispose();
            }
            catch (Exception ex)
            {
                clientConnection.Dispose();
            }
        }

        /// <summary>
        /// 开始接收心跳数据
        /// </summary>
        public void StartRecieveHeartData()
        {
            //命名管道连接
            ServerPipeConnection _PipeConnetcion = new ServerPipeConnection(STR_PIPEMSG_KEY, 512, 512, 5000, false);
            while (true)
            {
                try
                {
                    _PipeConnetcion.Disconnect();
                    _PipeConnetcion.Connect();
                    string request = _PipeConnetcion.Read();
                    if (!string.IsNullOrEmpty(request))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(request, out dt))
                        {
                            this._LastRecieveDate = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } 　

        }

        /// <summary>
        /// 是否长时间未接受到数据
        /// </summary>
        /// <param name="timeOutSeconds"></param>
        /// <returns></returns>
        public bool IsNotRecievedLongTime(int timeOutSeconds)
        {
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
