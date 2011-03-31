using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model;
using System.Xml;
using Component.TimerTask.Config;
using Component.TimerTask.Utility;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 操作注册应用程序文件的接口
    /// </summary>
    internal class BLLAppRegistOpt : IBLLAppRegistOpt
    {
        #region IBLLAppRegistOpt 成员

        /// <summary>
        /// 获取所有已经注册的程序
        /// </summary>
        /// <returns></returns>
        public List<Component.TimerTask.Model.TaskAssembly> GetAllApps()
        {
            List<TaskAssembly> list = new List<TaskAssembly>();

            XmlDocument doc = new XmlDocument();

            string filePath = AssemblyHelper.GetAssemblyPath() + StaticConfig.REGEST_APP_CFG_FILE;
            doc.Load(filePath);
            foreach (XmlElement iNode in doc.SelectSingleNode("RegistedApps").ChildNodes)
            {
                if (iNode.Name == "App")
                {
                    list.Add(Mapper.CfgDataMapper.MappingTaskAsssembly(iNode));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取某个已经注册的程序
        /// </summary>
        /// <param name="paraAppName">注册程序名称</param>
        /// <returns></returns>
        public Component.TimerTask.Model.TaskAssembly GetRegestedApp(string paraAppName)
        {
            return GetAllApps().Find(delegate(TaskAssembly ta)
            {
                return ta.UserName == paraAppName;
            });
        }

        #endregion
    }
}
