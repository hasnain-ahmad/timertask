using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using BOCO.TimerTask.TaskEngine;

namespace BOCO.TimerTask.TaskManager
{
    class Program
    {
        #region Windows Api
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
        #endregion

        internal const string STR_CAPTION_TITLE = "定时任务管理器（亿阳信通）";

        /// <summary>
        /// App Entry
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region 互斥
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "SINGLE_INSTANCE_MUTEX");
            if (!mutex.WaitOne(0, false))  //请求互斥的所有权
            {
                mutex.Close();
                mutex = null;
            }
            if (mutex == null)
            {
                Console.WriteLine("已经有一个实例启动");
                Console.ReadKey();
                return;
                //Environment.Exit(0);
            }
            #endregion

            #region Prepare
            Console.Title = STR_CAPTION_TITLE;
            CloseBtn();
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Console.WriteLine("定时任务管理器已经启动...");
            #endregion

            try
            {
                //开始启动定时任务管理引擎
                ITaskWorkerEngine taskEngine = TaskEngineFactory.GetTaskEngine();
                taskEngine.Start();
                
                //阻塞当前线程，因为控制台程序如果不阻塞会自动退出
                while (true)
                {
                    Console.Read();
                }
                //Thread.Sleep(5000);
            }
            catch(Exception ex)
            {
                Console.WriteLine("程序异常：" + ex.Message);
            }

            #region 程序运行结束，可以释放锁
            mutex.Close();
            mutex = null;
            #endregion

            Console.WriteLine("程序结束,互斥锁已经释放。");

            Thread.Sleep(10000);
            //发布后去掉下面这句
            //Console.Read();
        }

        /// <summary>
        /// 控制台关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        static void CloseBtn()
        {
            IntPtr windowHandle = FindWindow(null, STR_CAPTION_TITLE);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
        }
    }
}
