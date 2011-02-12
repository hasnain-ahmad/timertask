/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : BLlFactory.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 工厂类
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class BLlFactory
    {
        private static BLLService _bll;
        private static readonly object objlock = new object();

        private static IBLLEngineRescue _IbllEngineRes = null;
        private static readonly object objLockEngionRes = new object();

        /// <summary>
        /// 获取引擎营救接口
        /// </summary>
        /// <returns></returns>
        public static IBLLEngineRescue GetBLLEngineRes()
        {
            if (_IbllEngineRes == null)
            {
                lock (objLockEngionRes)
                {
                    if (_IbllEngineRes == null)
                    {
                        lock (objLockEngionRes)
                        {
                            _IbllEngineRes = new BLLEngineRescue();
                        }
                    }
                }
            }
            return _IbllEngineRes;
        }

        /// <summary>
        /// 获取业务逻辑服务接口
        /// </summary>
        /// <returns></returns>
        public static IBLLService GetBLL()
        {
            if (_bll == null)
            {
                lock (objlock)
                {
                    if (_bll == null)
                    {
                        lock (objlock)
                        {
                            _bll = new BLLService();
                        }
                    }
                }
            }
            return _bll;
        }

        /// <summary>
        /// 获取业务逻辑服务接口
        /// </summary>
        /// <returns></returns>
        public static IBLLLogic GetBllLogic()
        {
            if (_bll == null)
            {
                lock (objlock)
                {
                    if (_bll == null)
                    {
                        lock (objlock)
                        {
                            _bll = new BLLService();
                        }
                    }
                }
            }
            return _bll;
        }
    }
}
