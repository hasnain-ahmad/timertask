using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.Model;
using System.Xml;
using BOCO.TimerTask.Utility;

namespace BOCO.TimerTask.BLL
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
