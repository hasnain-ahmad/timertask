/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : TaskAssembly.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 程序集信息
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.Model
{
    /// <summary>
    /// 程序集信息
    /// </summary>
    [Serializable]
    public class TaskAssembly
    {
        private string _UserName;
        /// <summary>
        /// 用户配置的名称（唯一）
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _AppFile;
        /// <summary>
        /// 程序目录
        /// </summary>
        public string AppFile
        {
            get { return _AppFile; }
            set { _AppFile = value; }
        }

        private string _ProtocolNameSpace;
        /// <summary>
        /// 协议类命名空间
        /// </summary>
        public string ProtocolNameSpace
        {
            get { return _ProtocolNameSpace; }
            set { _ProtocolNameSpace = value; }
        }

        private string _ProtocolClass;
        /// <summary>
        /// 协议类名
        /// </summary>
        public string ProtocolClass
        {
            get { return _ProtocolClass; }
            set { _ProtocolClass = value; }
        }

        private AssemblyType _AssemblyType;
        /// <summary>
        /// 程序类型
        /// </summary>
        public AssemblyType AssemblyType
        {
            get { return _AssemblyType; }
            set { _AssemblyType = value; }
        }

        public TaskAssembly() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraAppFile"></param>
        /// <param name="paraProtocolNameSpace"></param>
        /// <param name="paraProtocolClass"></param>
        /// <param name="paraAssemblyType"></param>
        public TaskAssembly(string paraAppFile,
            string paraProtocolNameSpace,
            string paraProtocolClass,
            AssemblyType paraAssemblyType,
            string paraUserName)
        {
            _AppFile = paraAppFile;
            _AssemblyType = paraAssemblyType;
            _ProtocolClass = paraProtocolClass;
            _ProtocolNameSpace = paraProtocolNameSpace;
            _UserName = paraUserName;
        }
    }
}
