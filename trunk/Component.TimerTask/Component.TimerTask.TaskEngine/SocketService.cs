using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Component.TimerTask.BLL;
using Component.TimerTask.Model;
using Component.TimerTask.Utility;
using Component.TimerTask.Model.Enums;

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
            Thread thread = new Thread(new ThreadStart(ThreadFuncRecieve));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 循环监听函数
        /// </summary>
        private void ThreadFuncRecieve()
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

                    //开始更新引擎中的任务列表
                    foreach (TaskEntity entity in addedList)
                    {
                        _Engine.AddWorkingTask(entity);
                    }
                    foreach (TaskEntity entity in updateList)
                    {
                        _Engine.ModifyTask(entity);
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
                    LogEntity log = new LogEntity();
                    log.LogContent = ex.Message;
                    log.LogType = Component.TimerTask.Model.Enums.LogType.SocketServerRecievveError;
                    _IBLLLogic.WriteLog(log);
                }
            }
        }

    }
}
