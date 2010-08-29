/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : RegestAppCfgHelper.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 配置的程序集操作 帮助类
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Component.TimerTask.Model;
using Component.TimerTask.Utility;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 注册的程序集操作
    /// </summary>
    class RegestAppCfgHelper
    {

        public const string REGEST_APP_CFG_FILE = "RegestedApps.xml";

       
        /// <summary>
        /// 获取所有已经注册的程序
        /// </summary>
        /// <returns></returns>
        public static List<TaskAssembly> GetAllApps()
        {
            List<TaskAssembly> list = new List<TaskAssembly>();

            XmlDocument doc = new XmlDocument();

            string filePath = AssemblyHelper.GetAssemblyPath()  + REGEST_APP_CFG_FILE;
            doc.Load(filePath);
            foreach (XmlElement iNode in doc.SelectSingleNode("RegestedApps").ChildNodes)
            {
                if (iNode.Name == "App")
                {
                    list.Add(Mapper.CfgDataMapper.MappingTaskAsssembly(iNode));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有已经注册的程序
        /// </summary>
        /// <returns></returns>
        public static TaskAssembly GetRegestedApp(string paraAppName)
        {
            return GetAllApps().Find(delegate(TaskAssembly ta)
            {
                return ta.UserName == paraAppName;
            });
        }

    }
}
