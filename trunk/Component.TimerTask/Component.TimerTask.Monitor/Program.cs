using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Component.TimerTask.Utility;

namespace Component.TimerTask.Monitor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 互斥
            if (ProcessHelper.IsCurrentProcessHasLoaded())
            {
                MessageBox.Show("监控器已经启动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }
            //System.Threading.Mutex mutex = new System.Threading.Mutex(false, "SINGLE_INSTANCE_MUTEX_TTASK_MONITOR");
            //if (!mutex.WaitOne(0, false))  //请求互斥的所有权
            //{
            //    mutex.Close();
            //    mutex = null;
            //}
            //if (mutex == null)
            //{
            //    MessageBox.Show("监控器已经启动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Application.Exit();
            //    return;
            //}
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
            //Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            
        }
    }
}
