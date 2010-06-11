using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.Utility
{
    public class AssemblyHelper
    {

        private static string _AssPath;
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
