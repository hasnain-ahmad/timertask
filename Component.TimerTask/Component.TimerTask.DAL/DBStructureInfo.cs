﻿/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : DBStructureInfo.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 数据库结构信息
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Component.TimerTask.Utility;

namespace Component.TimerTask.DAL
{
    /// <summary>
    /// 数据库结构信息
    /// </summary>
    class DBStructureInfo
    {
        /// <summary>
        /// 数据库连接串-计划任务库
        /// </summary>
        public static string ConnectionString = string.Format("Data Source={0}", Path.Combine(AssemblyHelper.GetAssemblyPath(), "timertaskdb.db3"));

        /// <summary>
        /// 数据库连接串-日志库
        /// </summary>
        public static string ConnectionString_log = string.Format("Data Source={0}", Path.Combine(AssemblyHelper.GetAssemblyPath(), "timertaskdb_log.db3"));

        /// <summary>
        /// 初始化SQL-创建计划表
        /// </summary>
        public static readonly string INIT_DB_SQL_CREATETABLE_TASK = @"CREATE TABLE [PL_TimerTask] (
            [ID] INTEGER  PRIMARY KEY NOT NULL,
            [Name] NVARCHAR2(50)  NOT NULL,
            [TaskAppName] NVARCHAR2(50)  NOT NULL,
            [Enable] Char(5)  NOT NULL,
            [CreateDate] DATE  NULL,
            [DateStart] DATE  NOT NULL,
            [DateEnd] DATE  NOT NULL,
            [RunSpaceTimeSecs] INTEGER  NOT NULL,
            [RunSpaceType] NVARCHAR2(50)  NULL,
            [ExtraParaStr] NVARCHAR2(200)  NULL,
            [RunTimeOutSecs] INTEGER
            );";


        /// <summary>
        /// 初始化SQL-创建日志表
        /// </summary>
        public static readonly string INIT_DB_SQL_CREATETABLE_LOG = @"CREATE TABLE [PL_TimerTask_Log] (
            [ID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
            [LogDate] DATE  NULL,
            [TaskID] INTEGER  NULL,
            [TaskName] NVARCHAR2(50)  NULL,
            [LogType] NVARCHAR2(50)  NULL,
            [LogContent] NVARCHAR2(500)  NULL
            );";

        /// <summary>
        /// 接收心跳数据的表
        /// </summary>
        public static readonly string INIT_DB_SQL_CREATETABLE_HEART = @"CREATE TABLE [PL_TimerTask_Heart] (
            [LogDate] DATE  NULL
            );";

        public static readonly string INIT_DB_SQL_CREATETABLE_CONFIG = @"CREATE TABLE [PL_TimerTask_Config] (
            [DataKey] NVARCHAR2(100)  NOT NULL PRIMARY KEY,
            [DataValue] NVARCHAR2(100)  NULL
            );"; 

        public static readonly string DB_SQL_SYSTABS = @"select tbl_name from sqlite_master";
        public static readonly string DB_SQL_SYSTABS_TBCOUNT = @"select COUNT(*) from sqlite_master";

    }
}
