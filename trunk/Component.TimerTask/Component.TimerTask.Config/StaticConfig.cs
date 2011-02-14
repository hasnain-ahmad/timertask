using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Component.TimerTask.Config
{
    /// <summary>
    /// 静态配置
    /// </summary>
    public static class StaticConfig
    {
        /// <summary>
        /// 定时任务服务部署的服务器，如果部署的目标机器上只有一个网卡(包括虚拟网卡在内)，则不用配置
        /// </summary>

        public static string SocketIP
        {
            get { return ConfigurationManager.AppSettings["SocketIP"]; }
        }

        public static int SocketPort
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SocketPort"]); }
        } 


        /// <summary>
        /// 定时任务管理器空闲时间，根据任务的精确程度和执行频率来定，如果任务周期大，可以稍微大点，如20秒，60秒，如果执行频率或者精度很高，可以设为1秒，2秒
        /// </summary>
        public static int TimerTaskEngineIdelSec
        {
            get { return int.Parse(ConfigurationManager.AppSettings["TimerTaskEngineIdelSec"]); }
        } 


        /// <summary>
        /// 是否需要启动Wcf服务（Wcf服务可以提供远程的定时任务管理） True/False
        /// </summary>
        public static bool IsNeedWcf
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["IsNeedWcf"]); }
        }

        /// <summary>
        /// 引擎进程名称
        /// </summary>
        public static string STR_ENGINE_PROCESS_NAME = "Component.TimerTask.TaskManager";
    }
}
