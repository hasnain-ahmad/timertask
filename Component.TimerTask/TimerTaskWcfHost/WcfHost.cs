using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace TimerTaskWcfHost
{
    /// <summary>
    /// Wcf宿主类
    /// </summary>
    public static class WcfHost
    {
        private static ServiceHost _host;
        /// <summary>
        /// Starts the WCF service.
        /// </summary>
        public static void StartWcfService()
        {
            _host = new ServiceHost(typeof(TimerTaskWcfService.TimerTaskService));
            _host.Open();
        }
    }
}
