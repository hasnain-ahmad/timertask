/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : LogType.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 日志类型
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Component.TimerTask.Model.Enums
{
    /// <summary>
    /// 日志类型
    /// </summary>
    [Serializable]
    public enum LogType
    {
        /// <summary>
        /// 任务管理器启动失败
        /// </summary>
        TaskManagerStartError = 100,

        /// <summary>
        /// 任务开始执行
        /// </summary>
        TaskRunStart = 101,

        /// <summary>
        /// 任务执行结束
        /// </summary>
        TaskRunEnd = 102,

        /// <summary>
        /// Socket服务器接收消息异常
        /// </summary>
        SocketServerRecievveError = 103,

        /// <summary>
        /// Socket客户端发送消息失败
        /// </summary>
        SocketClientSendError = 104,

        /// <summary>
        /// 强制结束任务出错
        /// </summary>
        EnforceKillWorkError = 105,

        /// <summary>
        /// 任务被立即执行
        /// </summary>
        TaskRunStart_Immediate = 106,

        /// <summary>
        /// 任务被立即执行并影响后续任务
        /// </summary>
        TaskRunStart_Immediate_Interupt = 107,

        /// <summary>
        /// 手动停止一个正在工作的任务
        /// </summary>
        EnforceKillWork = 108,

        /// <summary>
        /// 任务配置文件找不到
        /// </summary>
        TaskConfigAssemblyFileNotFind = 109,

        /// <summary>
        /// 通过接口方法结束任务出错
        /// <remarks>注意，如果任务重有窗体弹出，直接结束会抱错</remarks>
        /// </summary>
        StopRuningFromInterfaceError = 110,
        /// <summary>
        /// 计划保存到数据库失败
        /// </summary>
        TaskAdd2DBError = 111,

        /// <summary>
        /// 任务执行线程抛出异常
        /// </summary>
        TaskThreadThrowException = 112,

        /// <summary>
        /// 通过反射加载目标对象时报错，有可能找不到相关引用。
        /// </summary>
        ReflectError = 113,

        /// <summary>
        /// 类型转换错误，目标对象未继承ITask接口
        /// </summary>
        TypeConvertITaskError = 114,

        /// <summary>
        /// 执行目标exe文件出现异常
        /// </summary>
        RunExeFileError = 115,

        /// <summary>
        /// 通过Socket更新引擎时不存在计划
        /// </summary>
        SocketRecieveTaskNoExist = 116

    }
}
