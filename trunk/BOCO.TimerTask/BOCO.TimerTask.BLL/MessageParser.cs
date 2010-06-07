using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.BLL
{
    /// <summary>
    /// 消息解释 转换 器
    /// </summary>
    public class MessageParser
    {
        /// <summary>
        /// 组装一条消息(所有接口都通过此方法组装)
        /// </summary>
        /// <param name="paraAddedTasks"></param>
        /// <param name="paraDeletedTasks"></param>
        /// <param name="paraUpdateTasks"></param>
        /// <param name="paraRunImmediateTask"></param>
        /// <param name="paraStopTask"></param>
        /// <returns></returns>
        public static string BuildMessage(List<TaskEntity> paraAddedTasks, List<Int64> paraDeletedTasks, List<TaskEntity> paraUpdateTasks, List<Int64> paraRunImmediateTask, List<RunTaskType> paraRunType, List<Int64> paraStopTask)
        {
            XmlDocument doc = new XmlDocument();
             doc.CreateNode(XmlNodeType.XmlDeclaration);
            
        }

        /// <summary>
        /// 翻译一条消息
        /// </summary>
        /// <param name="paraMessage"></param>
        /// <param name="paraAddedTasks"></param>
        /// <param name="paraDeletedTasks"></param>
        /// <param name="paraUpdateTasks"></param>
        public static void ParseMessage(string paraMessage, out List<TaskEntity> paraAddedTasks, out List<Int64> paraDeletedTasks, out List<TaskEntity> paraUpdateTasks, out List<Int64> paraRunImmediateTask, out List<RunTaskType> paraRunType, out List<Int64> paraStopTask)
        {
            throw new NotImplementedException();
        }
    }
}
