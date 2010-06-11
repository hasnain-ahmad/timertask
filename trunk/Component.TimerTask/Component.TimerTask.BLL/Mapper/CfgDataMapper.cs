using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model;
using System.Xml;
using System.IO;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.BLL.Mapper
{

    /// <summary>
    /// Date: 2010/6/10
    /// Author:LvJinMing
    /// Name:
    /// FullName:Component.TimerTask.BLL.Mapper.CfgDataMapper
    /// 数据映射器
    /// </summary>
    internal static class CfgDataMapper
    {
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
