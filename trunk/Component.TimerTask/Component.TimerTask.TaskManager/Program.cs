using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Component.TimerTask.TaskEngine;

namespace Component.TimerTask.TaskManager
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

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nCmdShow">0 隐藏，1 显示.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        extern static bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion

        internal const string STR_CAPTION_TITLE = "定时任务管理器";

        /// <summary>
        /// App Entry
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                #region 互斥
                System.Threading.Mutex mutex = new System.Threading.Mutex(false, "SINGLE_INSTANCE_MUTEX_TIMERMANAGER");
                if (!mutex.WaitOne(0, false))  //请求互斥的所有权
                {
                    mutex.Close();
                    mutex = null;
                }
                if (mutex == null)
                {
                    Console.WriteLine("已经有一个实例启动");
                    Thread.Sleep(5000);
                    return;
                    //Environment.Exit(0);
                }
                #endregion

                #region Prepare
                Console.Title = STR_CAPTION_TITLE;
                CloseBtn();
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
                //Console.Beep();
                Console.WriteLine("定时任务管理器已经启动...");

                //设置当前路径为DLL所在路径(如果不用这句话那么如果可执行程序的启动路径就是Web的跟目录)
                //Console.WriteLine(System.Environment.CurrentDirectory);
                System.Environment.CurrentDirectory = Utility.AssemblyHelper.GetAssemblyPath();
                //Console.WriteLine(System.Environment.CurrentDirectory);
                #endregion

                try
                {
                    int engineIdleTimeSec = 2;
                    if (!string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["TimerTaskEngineIdelSec"]))
                    {
                        int tmp;
                        if (int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["TimerTaskEngineIdelSec"], out tmp))
                        {
                            engineIdleTimeSec = tmp;
                            Console.WriteLine("定时任务管理器空闲时间间隔已经被配置为{0}秒。", tmp);
                        }
                        else
                        {
                            Console.WriteLine("配置定时任务管理器空闲时间间隔有误，将按照默认配置进行处理。");
                        }
                    }
                    else
                    {
                        Console.WriteLine("未找到定时任务管理器空闲时间间隔配置，将按照默认配置进行处理。");
                    }
                    //开始启动定时任务管理引擎
                    ITaskWorkerEngine taskEngine = TaskEngineFactory.GetTaskEngine(engineIdleTimeSec);
                    taskEngine.Start();

                    //阻塞当前线程，因为控制台程序如果不阻塞会自动退出
                    while (true)
                    {
                        Console.Read();
                    }
                }
                catch (Exception ex)
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// 控制台关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("该控制台程序不允许关闭");
            e.Cancel = true;
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
            //隐藏控制台窗口
            ShowWindow(windowHandle, 0);
            ShowWindow(windowHandle, 1);
        }
    }
}
