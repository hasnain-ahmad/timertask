using System;
using System.Collections.Generic;
using System.Text;

namespace BOCO.TimerTask.TaskEngine
{
    public static class TaskEngineFactory
    {
        private static ITaskWorkerEngine _Engine = null;
        public static  ITaskWorkerEngine GetTaskEngine()
        {
            if (_Engine == null)
            {
                _Engine = new TaskWorkerEngine();
            }
            return _Engine;
        }
    }
}
