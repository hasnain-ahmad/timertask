using System;
using System.Collections.Generic;
using System.Text;
using Component.TimerTask.Utility;

namespace Component.TimerTask.DAL
{
    /// <summary>
    /// 数据库结构信息
    /// </summary>
    class DBStructureInfo
    {
        public static string ConnectionString = string.Format( "Data Source={0}\\timertaskdb.db3",AssemblyHelper.GetAssemblyPath());

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
[ExeCommandParaMeter] NVARCHAR2(200)  NULL,
[RunTimeOutSecs] INTEGER
);";

        public static readonly string INIT_DB_SQL_CREATETABLE_LOG = @"CREATE TABLE [PL_TimerTask_Log] (
[ID] INTEGER  NOT NULL PRIMARY KEY,
[LogDate] DATE  NULL,
[TaskID] INTEGER  NULL,
[TaskName] NVARCHAR2(50)  NULL,
[LogType] NVARCHAR2(50)  NULL,
[LogContent] NVARCHAR2(500)  NULL
);";
        public static readonly string DB_SQL_SYSTABS = @"select tbl_name from sqlite_master";
        public static readonly string DB_SQL_SYSTABS_TBCOUNT = @"select COUNT(*) from sqlite_master";

    }
}
