﻿/*******************************************************************************
 * * 版权所有(C) LJM Info 2011
 * * 文件名称   : BLLAppRegistOpt.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2011年3月31日
 * * 内容摘要   : 操作注册应用程序文件的接口
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * 
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Component.TimerTask.Config;
using Component.TimerTask.Model;
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