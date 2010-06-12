using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component.TimerTask.Monitor
{
    public enum TaskState
    {
        超时,
        等待执行,
        正在执行,
        已删除
    }
}
