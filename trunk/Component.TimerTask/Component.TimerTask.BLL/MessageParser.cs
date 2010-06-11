using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using System.IO;

namespace Component.TimerTask.BLL
{
    /// <summary>
    /// 消息解释 转换 器
    /// </summary>
    public class MessageParser
    {
        /// <summary>
        /// 检查并设置任务的频率
        /// [对用户输入的信息做检验]
        /// </summary>
        /// <param name="paraTask"></param>
        public static void CheckAndSetTaskFrequence(ref TaskEntity paraTask)
        {
            switch (paraTask.RunSpaceType)
            {
                case TaskFrequence.Day:
                    paraTask.RunSpaceTime = 24 * 60 * 60;
                    break;
                case TaskFrequence.Hour:
                    paraTask.RunSpaceTime = 60 * 60;
                    break;
                case TaskFrequence.Minute:
                    paraTask.RunSpaceTime = 60;
                    break;
                case TaskFrequence.Week:
                    paraTask.RunSpaceTime = 7 * 24 * 60 * 60;
                    break;
                case TaskFrequence.Once:    //如果执行一次，设置间隔为２分钟，结束时间为开始时间＋２０秒（给２０秒的缓冲区）
                    paraTask.RunSpaceTime = 2 * 60;
                    paraTask.DateEnd = paraTask.DateStart.AddSeconds(20);
                    break;
            }
        }

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
            XmlNode iNode = doc.CreateNode(XmlNodeType.XmlDeclaration,"","");
            doc.AppendChild(iNode);

            XmlNode rootNode = doc.CreateElement("Root");
            doc.AppendChild(rootNode);

            XmlNode addNode = doc.CreateElement("AddList");
            if (paraAddedTasks != null && paraAddedTasks.Count > 0)
            {
                foreach (TaskEntity task in paraAddedTasks)
                {
                    XmlElement ele = doc.CreateElement("Task");
                    ele.SetAttribute("Name", task.Name);
                    ele.SetAttribute("Enable", task.Enable.ToString());
                    ele.SetAttribute("DateStart", task.DateStart.ToString("T"));
                    ele.SetAttribute("DateEnd", task.DateEnd.ToString("T"));
                    ele.SetAttribute("RunSpaceTime", task.RunSpaceTime.ToString());
                    ele.SetAttribute("RunSpaceType", task.RunSpaceType.ToString());
                    ele.SetAttribute("ExtraParaStr", task.ExtraParaStr);
                    ele.SetAttribute("ExeCommandParaMeter", task.ExeCommandParaMeter);
                    ele.SetAttribute("RunTimeOutSecs", task.RunTimeOutSecs.ToString());
                    ele.SetAttribute("RegestesAppName", task.RegestesAppName);
                    
                    addNode.AppendChild(ele);
                }
            }
            rootNode.AppendChild(addNode);

            XmlNode mdfNode = doc.CreateElement("UpdateList");
            if (paraUpdateTasks != null && paraUpdateTasks.Count > 0)
            {
                foreach (TaskEntity task in paraUpdateTasks)
                {
                    XmlElement ele = doc.CreateElement("Task");
                    ele.SetAttribute("ID", task.ID.ToString());
                    ele.SetAttribute("Name", task.Name);
                    ele.SetAttribute("Enable", task.Enable.ToString());
                    ele.SetAttribute("DateStart", task.DateStart.ToString("T"));
                    ele.SetAttribute("DateEnd", task.DateEnd.ToString("T"));
                    ele.SetAttribute("RunSpaceTime", task.RunSpaceTime.ToString());
                    ele.SetAttribute("RunSpaceType", task.RunSpaceType.ToString());
                    ele.SetAttribute("ExtraParaStr", task.ExtraParaStr);
                    ele.SetAttribute("ExeCommandParaMeter", task.ExeCommandParaMeter);
                    ele.SetAttribute("RunTimeOutSecs", task.RunTimeOutSecs.ToString());
                    ele.SetAttribute("RegestesAppName", task.RegestesAppName);
                    mdfNode.AppendChild(ele);
                }
            }
            rootNode.AppendChild(mdfNode);

            XmlNode delNode = doc.CreateElement("DeleteList");
            if (paraDeletedTasks != null && paraDeletedTasks.Count > 0)
            {
                foreach (long task in paraDeletedTasks)
                {
                    XmlElement ele = doc.CreateElement("Task");
                    ele.SetAttribute("ID", task.ToString());
                    delNode.AppendChild(ele);
                }
            }
            rootNode.AppendChild(delNode);

            XmlNode runNode = doc.CreateElement("RunList");
            if (paraRunImmediateTask != null && paraRunImmediateTask.Count > 0)
            {
                for(int i=0;i< paraRunImmediateTask.Count;i++)
                {
                    XmlElement ele = doc.CreateElement("Task");
                    ele.SetAttribute("ID", paraRunImmediateTask[i].ToString());
                    ele.SetAttribute("Type", paraRunType[i].ToString());
                    runNode.AppendChild(ele);
                }
            }
            rootNode.AppendChild(runNode);
            
            XmlNode stpNode = doc.CreateElement("StopList");
            if (paraStopTask != null && paraStopTask.Count > 0)
            {
                foreach (long task in paraStopTask)
                {
                    XmlElement ele = doc.CreateElement("Task");
                    ele.SetAttribute("ID", task.ToString());
                    stpNode.AppendChild(ele);
                }
            }
            rootNode.AppendChild(stpNode);

            return doc.OuterXml;
            
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
            paraAddedTasks = new List<TaskEntity>();
            paraDeletedTasks = new List<long>();
            paraUpdateTasks = new List<TaskEntity>();
            paraRunImmediateTask = new List<long>();
            paraRunType = new List<RunTaskType>();
            paraStopTask = new List<long>();

            XmlDocument doc = new XmlDocument();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(paraMessage));
            doc.Load(ms);
            XmlNode iNode = doc.SelectSingleNode("Root");

            XmlNode addNode = iNode.SelectSingleNode("AddList");
            foreach (XmlElement ele in addNode.ChildNodes)
            {
                TaskEntity entity = new TaskEntity();
                entity.Name = ele.GetAttribute("Name");
                entity.Enable = bool.Parse(ele.GetAttribute("Enable"));
                entity.DateStart = DateTime.Parse(ele.GetAttribute("DateStart"));
                entity.DateEnd = DateTime.Parse(ele.GetAttribute("DateEnd"));
                entity.RunSpaceTime = Int64.Parse(ele.GetAttribute("RunSpaceTime"));
                entity.RunSpaceType = (TaskFrequence)Enum.Parse(typeof(TaskFrequence), ele.GetAttribute("RunSpaceType"));
                entity.ExtraParaStr = ele.GetAttribute("ExtraParaStr");
                entity.ExeCommandParaMeter = ele.GetAttribute("ExeCommandParaMeter");
                entity.RunTimeOutSecs = Int64.Parse(ele.GetAttribute("RunTimeOutSecs"));
                entity.RegestesAppName = ele.GetAttribute("RegestesAppName");

                CheckAndSetTaskFrequence(ref entity);
                paraAddedTasks.Add(entity);
            }

            XmlNode mdfNode = iNode.SelectSingleNode("UpdateList");
            foreach (XmlElement ele in mdfNode.ChildNodes)
            {
                TaskEntity entity = new TaskEntity();
                entity.Name = ele.GetAttribute("Name");
                entity.Enable = bool.Parse(ele.GetAttribute("Enable"));
                entity.DateStart = DateTime.Parse(ele.GetAttribute("DateStart"));
                entity.DateEnd = DateTime.Parse(ele.GetAttribute("DateEnd"));
                entity.RunSpaceTime = Int64.Parse(ele.GetAttribute("RunSpaceTime"));
                entity.RunSpaceType = (TaskFrequence)Enum.Parse(typeof(TaskFrequence), ele.GetAttribute("RunSpaceType"));
                entity.ExtraParaStr = ele.GetAttribute("ExtraParaStr");
                entity.ExeCommandParaMeter = ele.GetAttribute("ExeCommandParaMeter");
                entity.RunTimeOutSecs = Int64.Parse(ele.GetAttribute("RunTimeOutSecs"));
                entity.RegestesAppName = ele.GetAttribute("RegestesAppName");
                entity.SetKeyID(Int64.Parse(ele.GetAttribute("ID")));

                CheckAndSetTaskFrequence(ref entity);
                paraUpdateTasks.Add(entity);
            }

            XmlNode delNode = iNode.SelectSingleNode("DeleteList");
            foreach (XmlElement ele in delNode.ChildNodes)
            {
                paraDeletedTasks.Add(
                    Int64.Parse(ele.GetAttribute("ID")));
            }

            XmlNode runNode = iNode.SelectSingleNode("RunList");
            foreach (XmlElement ele in runNode.ChildNodes)
            {
                paraRunImmediateTask.Add(
                    Int64.Parse(ele.GetAttribute("ID")));
                paraRunType.Add((RunTaskType)Enum.Parse(typeof(RunTaskType),ele.GetAttribute("Type")));
            }

            //释放内存流
            ms.Close();
            ms.Dispose();
        }
    }
}
