/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : TaskEngineFactory.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 工厂类
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
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
