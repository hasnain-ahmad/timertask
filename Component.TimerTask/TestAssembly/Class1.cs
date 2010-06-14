using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.ITimerComponent;
using System.Threading;
using System.Windows.Forms;

namespace TestAssembly
{
    public class Class1 : ITimeWorkTask
    {
        System.Windows.Forms.Form frm;

        /// <summary>
        /// 任务执行入口
        /// </summary>
        public override void TaskExecuteFunc()
        {
            frm = new Form1();
            frm.Show();
            Console.WriteLine("Task Closed By Self");
            Application.DoEvents();
            Thread.Sleep(5000);
            
            frm.Close();

            //加上此方法确保能通知系统任务运行完成
            base.TaskExecuteFunc();
        }

        public override void StopRuning()
        {
            Console.WriteLine("This Task OutDate Killed");
            //通过设置标记为来是上面的任务执行方法结束
            //frm.Dispose();
            
        }
    }
}
