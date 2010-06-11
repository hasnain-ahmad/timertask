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
