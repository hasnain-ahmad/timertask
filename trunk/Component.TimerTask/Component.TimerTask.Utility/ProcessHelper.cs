using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Component.TimerTask.Utility
{
    /// <summary>
    /// 进程相关操作方法
    /// </summary>
    public static class ProcessHelper
    {
        /// <summary>
        /// 当前进程是否已经启动
        /// </summary>
        /// <returns></returns>
        public static bool IsCurrentProcessHasLoaded()
        {
            Process currentP = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(currentP.ProcessName).Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 杀掉进程
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess(string processName)
        {
            Process[] pArr = null;
            pArr = Process.GetProcessesByName(processName);
            for (int i = 0; i < pArr.Length; i++)
            {
                pArr[i].Kill();
            }
        }
    }
}
