using System;
using System.Collections.Generic;
using System.Text;
using BOCO.TimerTask.Model;
using System.Xml;
using System.IO;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.BLL.Mapper
{

    /// <summary>
    /// Date: 2010/6/10
    /// Author:LvJinMing
    /// Name:
    /// FullName:BOCO.TimerTask.BLL.Mapper.CfgDataMapper
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
