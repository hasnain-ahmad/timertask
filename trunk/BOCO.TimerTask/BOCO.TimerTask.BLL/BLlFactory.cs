// File:    BLlFactory.cs
// Author:  LvJinMing
// Created: 2010年6月4日 15:26:17
// Purpose: Class

using System;
using System.Collections.Generic;
using System.Text;

namespace BOCO.TimerTask.BLL
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class BLlFactory
    {
        private static BLLService _bll;
        private static readonly object objlock = new object();
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
