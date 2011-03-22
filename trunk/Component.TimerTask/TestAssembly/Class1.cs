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


        public override void StopRuning()
        {
            Console.WriteLine("手动结束掉");
            //通过设置标记为来是上面的任务执行方法结束
            //frm.Dispose();
            
        }

        public override void StartRuning()
        {
            frm = new Form1();
            frm.Show();
            Application.DoEvents();
            Thread.Sleep(20000);
            Console.WriteLine("任务执行完，自己结束");
            frm.Close();
        }
    }
}
