using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 工厂类 
    /// </summary>
    public static class TaskEngineFactory
    {
        private static ITaskWorkerEngine _Engine = null;
        public static  ITaskWorkerEngine GetTaskEngine(int paraEngineIdleSecs)
        {
            if (_Engine == null)
            {
                _Engine = new TaskWorkerEngine(paraEngineIdleSecs);
            }
            return _Engine;
        }
    }
}
