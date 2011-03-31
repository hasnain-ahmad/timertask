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
