/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : SocketService.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 定时任务Socket监听服务
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Component.TimerTask.BLL;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Utility;

namespace Component.TimerTask.TaskEngine
{
    /// <summary>
    ///  定时任务Socket监听服务
    /// </summary>
    internal class SocketService
    {
        private Socket _Socket;
        private BLL.IBLLLogic _IBLLLogic;
        private ITaskWorkerEngine _Engine;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraList"></param>
        /// <param name="paraEngine"></param>
        /// <param name="paraBllLogic"></param>
        public SocketService(ITaskWorkerEngine paraEngine, IBLLLogic paraBllLogic)
        {
            _Engine = paraEngine;
            _IBLLLogic = paraBllLogic;
        }

        public void StartListen(IPEndPoint paraPoint)
        {
            _Socket = SocketHelper.GetSocketListen(paraPoint);
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadFuncRecieve));
            //Thread thread = new Thread(new ThreadStart(ThreadFuncRecieve));
            //thread.IsBackground = true;
            //thread.Start();
        }

        /// <summary>
        /// 循环监听函数
        /// </summary>
        private void ThreadFuncRecieve(object state)
        {
            while (true)
            {
                try
                {
                    byte[] recieveByte = new byte[1024];
                    Socket recieveSocket = _Socket.Accept();
                    //关键地方，Recieve方法会阻塞线程
                    recieveSocket.Receive(recieveByte);
                    SocketHelper.Send(recieveSocket, SocketHelper.HANDSHAKE);
                    string recieveContent = Encoding.Default.GetString(recieveByte);
                    recieveSocket.Close();
                    
                    //解析取到的消息
                    List<TaskEntity> addedList;
                    List<Int64> deledList;
                    List<TaskEntity> updateList;
                    List<Int64> stopedList;
                    List<Int64> runList;
                    List<RunTaskType> runTypeList;
                    MessageParser.ParseMessage(recieveContent, out addedList, out deledList, out updateList, out runList,out runTypeList, out stopedList);

                    #region 先保存入库
                    //----入库在发消息前已经保存，这里不再保存
                    //foreach (TaskEntity entity in addedList)
                    //{
                    //    _IBLLLogic.AddTask2DB(entity);
                    //}
                    //foreach (TaskEntity entity in updateList)
                    //{
                    //    _IBLLLogic.UpdateTask2DB(entity);
                    //}
                    //foreach (Int64 entitu in deledList)
                    //{
                    //    _IBLLLogic.DeleteTask2DB(entitu);
                    //}

                    #endregion

                    //用于检查从Socket接收过来的实体是否在本地有配置
                    List<string> regestApps = _IBLLLogic.GetRegestedApp();
                    //开始更新引擎中的任务列表
                    foreach (TaskEntity entity in addedList)
                    {
                        if (regestApps.Contains(entity.RegestesAppName))
                        {
                            _Engine.AddWorkingTask(entity);
                        }
                        else
                        {
                            string s = "传入的计划尚未在本地配置：" + "\t" + entity.ToString();
                            Console.WriteLine(s);
                            _IBLLLogic.WriteLog(entity.ID, entity.Name, s, LogType.SocketRecieveTaskNoExist);
                        }
                    }
                    foreach (TaskEntity entity in updateList)
                    {
                        if (regestApps.Contains(entity.RegestesAppName))
                        {
                            _Engine.ModifyTask(entity);
                        }
                        else
                        {
                            string s = "传入的计划尚未在本地配置：" + "\t" + entity.ToString();
                            Console.WriteLine(s);
                            _IBLLLogic.WriteLog(entity.ID, entity.Name, s, LogType.SocketRecieveTaskNoExist);
                        }
                    }
                    foreach (Int64 entitt in deledList)
                    {
                        _Engine.DelTask(entitt);
                    }
                    for (int i = 0; i < runList.Count; i++)
                    {
                        _Engine.ManualRunTask(runList[i], runTypeList[i]);
                    }
                    foreach (Int64 entity in stopedList)
                    {
                        _Engine.StopRuningTask(entity);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    LogEntity log = new LogEntity();
                    log.LogContent = ex.Message;
                    log.LogType = Component.TimerTask.Model.Enums.LogType.SocketServerRecievveError;
                    _IBLLLogic.WriteLog(log);
                }
            }
        }

    }
}
