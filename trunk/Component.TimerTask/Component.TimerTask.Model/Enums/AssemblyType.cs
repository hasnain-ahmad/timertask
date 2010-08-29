/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : AssemblyType.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 程序类型
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.Model.Enums
{
    /// <summary>
    /// 程序类型
    /// </summary>
    [Serializable]
    [EnumDescription("外部组件的程序类型")]
    public enum AssemblyType
    {
        [EnumDescription("可执行文件方式")]
        Exe = 0,
        [EnumDescription("动态库方式")]
        Dll = 1
    }
}
