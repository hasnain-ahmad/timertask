/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : CfgDataMapper.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 数据映射器
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.BLL.Mapper
{

    /// <summary>
    /// Date: 2010/6/10
    /// Author:LvJinMing
    /// FullName:Component.TimerTask.BLL.Mapper.CfgDataMapper
    /// 数据映射器
    /// </summary>
    internal static class CfgDataMapper
    {
        /// <summary>
        /// Mappings the task asssembly.
        /// </summary>
        /// <param name="paraNode">The para node.</param>
        /// <returns></returns>
        public static TaskAssembly MappingTaskAsssembly(XmlElement paraNode)
        {
            TaskAssembly entity = new TaskAssembly();
            entity.AppFile = paraNode.GetAttribute("dll");
            FileInfo fi = new FileInfo(entity.AppFile);
            if (fi.Extension.ToLower() == ".exe")
            {
                entity.AssemblyType = AssemblyType.Exe;
            }
            else
            {
                entity.AssemblyType = AssemblyType.Dll;
            }
            entity.ProtocolClass = paraNode.GetAttribute("class");
            entity.ProtocolNameSpace = paraNode.GetAttribute("namespace");
            entity.UserName = paraNode.GetAttribute("name");
            return entity;
        }
    }
}
