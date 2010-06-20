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
