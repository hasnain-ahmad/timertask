/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : AssemblyHelper.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 程序集相关帮助
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.Utility
{
    /// <summary>
    /// 程序集相关帮助
    /// </summary>
    public class AssemblyHelper
    {

        private static string _AssPath =string.Empty;
        /// <summary>
        /// 获取Assembly的运行路径
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyPath()
        {
            if (string.IsNullOrEmpty(_AssPath))
            {
                string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是<file://> 的长度

                string[] arrSection = _CodeBase.Split(new char[] { '/' });

                string _FolderPath = "";
                for (int i = 0; i < arrSection.Length - 1; i++)
                {
                    _FolderPath += arrSection[i] + "/";
                }
                _AssPath = _FolderPath;
            }
            return _AssPath;
        }
    }
}
