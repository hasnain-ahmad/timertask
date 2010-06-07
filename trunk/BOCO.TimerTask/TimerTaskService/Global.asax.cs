using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using BOCO.TimerTask.BLL;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace TimerTaskService
{
    public class Global : System.Web.HttpApplication
    {
        ///// <summary>
        ///// 任务管理器进程
        ///// </summary>
        //internal static Process TaskManagerProcess;

        protected void Application_Start(object sender, EventArgs e)
        {
            //服务启动的时候启动定时任务管理器
            try
            {
                //Process[] arr = Process.GetProcessesByName("BOCO.TimerTask.TaskManager");
                //if (arr.Length == 0)
                //{
                //    TaskManagerProcess = Process.Start(HttpContext.Current.Server.MapPath("Bin") + "\\BOCO.TimerTask.TaskManager.exe");
                //}
                IBLLService ts = new TaskService();
                if (!ts.IsTaskManagerAlive())
                {
                    ts.StartTaskManager();
                }
            }
            catch(Exception ex)
            {
                IBLLService bll = BLlFactory.GetBLL();
                LogEntity log = new LogEntity();
                log.LogContent = ex.Message;
                //log.LogDate = DateTime.Now;
                log.LogType = LogType.TaskManagerStartError;
                log.TaskID = -1;
                bll.WriteLog(log);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}