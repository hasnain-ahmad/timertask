using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BOCO.TimerTask.Model;
using BOCO.TimerTask.Model.Enums;

namespace BOCO.TimerTask.DAL.Mapper
{
    /// <summary>
    /// 数据映射器
    /// </summary>
    internal static class DataMapper
    {
        public static TaskEntity MappingTaskEntity(TaskDataSet.PL_TimerTaskRow paraDr)
        {
            return new TaskEntity(paraDr.ID,
                paraDr.Name,
                bool.Parse(paraDr.Enable),
                paraDr.DateStart,
                paraDr.DateEnd,
                paraDr.RunSpaceTimeSecs,
                (TaskFrequence)Enum.Parse(typeof(TaskFrequence), paraDr.RunSpaceType),
                paraDr.ExtraParaStr,
                paraDr.ExeCommandParaMeter,
                paraDr.RunTimeOutSecs,
                paraDr.TaskAppName
                );
        }

        public static void ReseveMappingTaskEntity(TaskEntity paraTaskEntity, ref TaskDataSet.PL_TimerTaskRow paraRow)
        {
            paraRow.Name = paraTaskEntity.Name;
            paraRow.CreateDate = DateTime.Now;
            paraRow.DateEnd = paraTaskEntity.DateEnd;
            paraRow.DateStart = paraTaskEntity.DateStart;
            paraRow.Enable = paraTaskEntity.Enable.ToString();
            paraRow.ExeCommandParaMeter = paraTaskEntity.ExeCommandParaMeter;
            paraRow.ExtraParaStr = paraTaskEntity.ExtraParaStr;
            paraRow.Name = paraTaskEntity.Name;
            paraRow.RunSpaceTimeSecs = paraTaskEntity.RunSpaceTime;
            paraRow.RunSpaceType = paraTaskEntity.RunSpaceType.ToString();
            paraRow.RunTimeOutSecs = paraTaskEntity.RunTimeOutSecs;
            paraRow.TaskAppName = paraTaskEntity.RegestesAppName;
        }

        public static LogEntity MappingLogEntity(TaskDataSet.PL_TimerTask_LogRow paraDr)
        {
            return new LogEntity(
                paraDr.ID,
                paraDr.TaskID,
                paraDr.LogDate,
                (LogType)Enum.Parse(typeof(LogType), paraDr.LogType),
                paraDr.LogContent,
                paraDr.TaskName
                );
        }

        public static void ReserMappingLogEntity(LogEntity paraLog, ref TaskDataSet.PL_TimerTask_LogRow paraLogRow)
        {
            paraLogRow.LogDate = paraLog.LogDate;
            paraLogRow.LogType = paraLog.LogType.ToString();
            paraLogRow.TaskID = paraLog.TaskID;
            paraLogRow.TaskName = paraLog.TaskName;
            paraLogRow.LogContent = paraLog.LogContent;

        }
        
    }
}
