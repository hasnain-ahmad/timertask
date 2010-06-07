using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.ITimerComponent;
using System.Threading;

namespace TestAssembly
{
    public class Class1 : ITimeWorkTask
    {

        public override void TaskExecuteFunc()
        {
            Thread.Sleep(10000);
            //System.Windows.Forms.MessageBox.Show("Hello I has Complete");

            Console.WriteLine("Closed");
            base.TaskExecuteFunc();
        }

        public override void StopRuning()
        {
            
            Thread.Sleep(10000);
            //System.Windows.Forms.MessageBox.Show("Hello I has Stoped");
            
        }
    }
}
