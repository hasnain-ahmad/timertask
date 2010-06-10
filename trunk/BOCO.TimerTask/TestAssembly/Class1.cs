using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.ITimerComponent;
using System.Threading;
using System.Windows.Forms;

namespace TestAssembly
{
    public class Class1 : ITimeWorkTask
    {
        System.Windows.Forms.Form frm;
        public override void TaskExecuteFunc()
        {
            frm = new Form1();
            frm.Show();
            Console.WriteLine("Task Closed By Self");
            Application.DoEvents();
            Thread.Sleep(5000);
            
            frm.Close();
            base.TaskExecuteFunc();
        }

        public override void StopRuning()
        {
            Console.WriteLine("This Task OutDate Killed");
            //frm.Dispose();
            
        }
    }
}
