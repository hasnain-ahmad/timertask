using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.ITimerComponent;
using System.Threading;

namespace TestAssembly
{
    public class Class1 : ITimeWorkTask
    {
        System.Windows.Forms.Form frm;
        public override void TaskExecuteFunc()
        {
            frm = new Form1();
            frm.ShowDialog();
            Console.WriteLine("Closed");
            base.TaskExecuteFunc();
        }

        public override void StopRuning()
        {
            frm.Dispose();
            
        }
    }
}
