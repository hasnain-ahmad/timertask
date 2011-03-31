/*******************************************************************************
 * * 版权所有(C) LJM Info 2011
 * * 文件名称   : IBLLAppRegistOpt.cs
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
using Component.TimerTask.Model;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 操作注册应用程序文件的接口
    /// </summary>
    public interface IBLLAppRegistOpt
    {
        /// <summary>
        /// 获取所有已经注册的程序
        /// </summary>
        /// <returns></returns>
        List<TaskAssembly> GetAllApps();

        /// <summary>
        ///  获取某个已经注册的程序
        /// </summary>
        /// <param name="paraAppName">注册程序名称</param>
        /// <returns></returns>
        TaskAssembly GetRegestedApp(string paraAppName);
    }
}
