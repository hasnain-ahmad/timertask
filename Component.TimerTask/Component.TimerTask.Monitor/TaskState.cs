/*******************************************************************************
 * * 版权所有(C) LJM Info 2011
 * * 文件名称   : TaskState.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2011年2月16日
 * * 内容摘要   : 
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * 
 * ********************************************************************************/
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
