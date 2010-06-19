using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Component.TimerTask.TaskInterface;

namespace TestTask
{
    public class Class1 : ITask
    {
        System.Windows.Forms.Form frm;

        /// <summary>
        /// 任务执行入口
        /// </summary>
        public override void RunTask()
        {
            try
            {
                frm = new Form1();
                frm.Show();
                Console.WriteLine("Task Closed By Self");
                Application.DoEvents();
                Thread.Sleep(5000);

                frm.Close();

                //加上此方法确保能通知系统任务运行完成
                base.RunTask();
            }
            catch(Exception ex) //线程执行方法需要进行异常捕获,最好是能通过委托告诉主线程进行记录
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void StopRuning()
        {
            Console.WriteLine("This Task OutDate Killed");
            //通过设置标记为来是上面的任务执行方法结束
            //frm.Dispose();
            
        }
    }
}
