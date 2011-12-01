/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : SocketHelper.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : Socket帮助类
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Component.TimerTask.Utility
{
    /// <summary>
    /// Socket帮助类
    /// </summary>
    public class SocketHelper
    {
        /// <summary>
        /// 通信握手信息
        /// </summary>
        public const string HANDSHAKE = "OK";

        /// <summary>
        /// 获取Socket连接
        /// </summary>
        /// <param name="paraPoint"></param>
        /// <returns></returns>
        public static Socket GetSocket(IPEndPoint paraPoint)
        {
            Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            skt.Connect(paraPoint);
            return skt;
        }

        /// <summary>
        /// 获取Socket监听连接
        /// </summary>
        /// <param name="paraPoint"></param>
        /// <returns></returns>
        public static Socket GetSocketListen(IPEndPoint paraPoint)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(paraPoint);
            socket.Listen(20);
            return socket;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendStr">要发送的信息(utf8格式)</param>
        /// <returns>返回服务器传回的信息</returns>
        public static byte[] SendMessage(Socket socket, string sendStr)
        {
            byte[] sendbyte = new byte[4096];
            sendbyte = Encoding.Default.GetBytes(sendStr);
            socket.Send(sendbyte);
            socket.Receive(sendbyte);
            return sendbyte;
        }

        /// <summary>
        /// 只发送消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="sendStr"></param>
        public static void Send(Socket socket, string sendStr)
        {
            byte[] sendbyte = new byte[4096];
            sendbyte = Encoding.Default.GetBytes(sendStr);
            socket.Send(sendbyte);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendByte">要发送的字节流</param>
        /// <returns></returns>
        public static byte[] SendMessage(Socket socket, byte[] sendByte)
        {
            socket.Send(sendByte);
            socket.Receive(sendByte);
            return sendByte;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendByte">要发送对象</param>
        /// <returns></returns>
        public static object SendMessageGetObj(Socket socket, object request)
        {
            byte[] sendbyte = new byte[4096];
            sendbyte = SerializeBinary(request);
            socket.Send(sendbyte);
            socket.Receive(sendbyte);
            object obj = DeserializeBinary(sendbyte);
            return obj;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendByte">要发送对象</param>
        /// <returns></returns>
        public static byte[] SendMessageGetByte(Socket socket, object request)
        {
            byte[] sendbyte = new byte[4096];
            sendbyte = SerializeBinary(request);
            socket.Send(sendbyte);
            socket.Receive(sendbyte);
            return sendbyte;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendStr">要发送的字串</param>
        /// <returns>返回对象</returns>
        public static string SendMessageGetStr(Socket socket, string sendStr)
        {
            byte[] sendbyte = Encoding.Default.GetBytes(sendStr);
            socket.Send(sendbyte);
            sendbyte = Encoding.Default.GetBytes(HANDSHAKE);
            socket.Receive(sendbyte);
            string obj = Encoding.Default.GetString(sendbyte);
            return obj;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendStr">要发送的字串</param>
        /// <returns>返回对象</returns>
        public static object SendMessageGetObj(Socket socket, string sendStr)
        {
            byte[] sendbyte = Encoding.Default.GetBytes(sendStr);
            socket.Send(sendbyte);
            socket.Receive(sendbyte);
            object obj = DeserializeBinary(sendbyte);
            return obj;
        }



        //[Serializable]

        ///   <summary>
        ///   序列化为二进制字节数组   
        ///   </summary>
        ///   <param   name="request">要序列化的对象</param>   
        ///   <returns>字节数组</returns>
        private static byte[] SerializeBinary(object request)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream.GetBuffer();
        }

        ///   <summary>
        ///   从二进制数组反序列化得到对象
        ///   </summary>
        ///   <param name="buf">字节数组</param>
        ///   <returns>得到的对象</returns>
        private static object DeserializeBinary(byte[] buf)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream(buf);
            memStream.Position = 0;
            BinaryFormatter deserializer = new BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);
            memStream.Close();
            return newobj;
        }

        /// <summary>
        /// 获取本机IP配置
        /// </summary>
        /// <returns></returns>
        public static IPEndPoint GetIpEndPoint()
        {
            IPEndPoint ip = new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
            return ip;
            //IPAddress ipAddress;
            //String ipString = ConfigurationManager.AppSettings.Get("SocketIP");
            //if (string.IsNullOrEmpty(ipString))
            //{
            //    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //    ipAddress = ipHostInfo.AddressList[0];
            //}
            //else
            //{
            //    ipAddress = IPAddress.Parse(ipString);
            //}

            //int port;
            //String portString = ConfigurationManager.AppSettings.Get("SocketPort");
            //if (string.IsNullOrEmpty(portString))
            //{
            //    port = 11001;
            //}
            //else
            //{
            //    port = int.Parse(portString);
            //}
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            //return localEndPoint;
        }

        /// <summary>
        /// 释放Socket资源
        /// </summary>
        /// <param name="socket"></param>
        public static void CloseSocket(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
            GC.Collect();
        }
    }

    ///// <summary>
    ///// 异步传递的状态对象
    ///// </summary>
    //public class StateObject
    //{
    //    public Socket workSocket = null;
    //    public const int BufferSize = 1024;
    //    public byte[] buffer = new byte[BufferSize];
    //    public StringBuilder sb = new StringBuilder();
    //}
}
