/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : DALFactory.cs
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

namespace Component.TimerTask.DAL
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class DALFactory
    {
        private static IDataAccess _DataAccess = new DataAccess();

        /// <summary>
        /// Gets the data access.
        /// </summary>
        /// <returns></returns>
        public static IDataAccess GetDataAccess()
        {
            return _DataAccess;
        }
    }
}
