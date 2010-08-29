﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.DAL.Mapper
{

    /// <summary>
    /// Date: 2010-6-20 11:02
    /// Author: Administrator
    /// FullName: Component.TimerTask.DAL.Mapper.DataMapper
    /// Class: 数据实体映射器
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