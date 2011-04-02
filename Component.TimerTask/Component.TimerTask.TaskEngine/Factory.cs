/*******************************************************************************
 * * 版权所有(C) LJM Info 2011
 * * 文件名称   : Factory.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2011年4月2日
 * * 内容摘要   : 工厂类
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * 
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using Component.TimerTask.BLL;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.TaskEngine.Workers;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    /// 工厂类
    /// </summary>
    static public class Factory
    {
        private static ITaskWorkerEngine _Engine = null;
        /// <summary>
        /// Gets the task engine.
        /// </summary>
        /// <param name="paraEngineIdleSecs">The para engine idle secs.</param>
        /// <returns></returns>
        public static ITaskWorkerEngine GetTaskEngine(int paraEngineIdleSecs)
        {
            if (_Engine == null)
            {
                _Engine = new TaskWorkerEngine(paraEngineIdleSecs);
            }
            return _Engine;
        }

        /// <summary>
        /// Gets the worker.
        /// </summary>
        /// <param name="wt">The wt.</param>
        /// <param name="bll">The BLL.</param>
        /// <param name="asType">As type.</param>
        /// <returns></returns>
        internal static IWorker GetWorker(WorkingTask wt, IBLLLogic bll, AssemblyType asType)
        {
            switch (asType)
            {
                case AssemblyType.Dll:
                    return new Worker_Assembly(wt, bll);
                    break;

                case AssemblyType.Exe:
                    return new Worker_Excutable(wt, bll);
                    break;
                default:
                    throw new Exception("尚未定义的工作类型,AssemblyType:" + asType.ToString());
                    break;

            }

        }
    }
}
