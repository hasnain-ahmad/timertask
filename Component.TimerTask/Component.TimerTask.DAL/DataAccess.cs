/*******************************************************************************
 * * 版权所有(C) LJM Info 2010
 * * 文件名称   : DataAccess.cs
 * * 当前版本   : 1.0.0.1
 * * 作    者   : 吕金明 (lvjm@163.com)
 * * 设计日期   : 2010年8月29日
 * * 内容摘要   : 数据访问类
 * * 修改记录   : 
 * * 日    期       版    本        修改人      修改摘要
 * *
 * ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using Component.TimerTask.Model;
using Component.TimerTask.Model.Enums;

namespace Component.TimerTask.DAL
{

    /// <summary>
    /// Date: 2010-6-20 11:02
    /// Author: Administrator
    /// FullName: Component.TimerTask.DAL.DataAccess
    /// Class: 数据访问类
    /// </summary>
    internal class DataAccess : IDataAccess
    {
        private TaskDataSet _DataSet;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAccess()
        {
            _DataSet = new TaskDataSet();
            SetTableAutoColumnSeed();
        }

        /// <summary>
        /// 设置自增列的种子
        /// </summary>
        private void SetTableAutoColumnSeed()
        {
            try
            {
                string sql = "SELECT MAX(ID) + 1 FROM {0}";
                _DataSet.PL_TimerTask.IDColumn.AutoIncrementSeed = int.Parse("0" + SqliteHelper.ExecuteScalar(string.Format(sql, _DataSet.PL_TimerTask.TableName)).ToString());

                _DataSet.PL_TimerTask_Log.IDColumn.AutoIncrementSeed = int.Parse("0" + SqliteHelper.ExecuteScalar(string.Format(sql, _DataSet.PL_TimerTask_Log.TableName)).ToString());
            }
            catch
            {
                CheckAndInitDataBase();
                //throw;
            }
        }

        private void CheckAndInitDataBase()
        {
            string sql = DBStructureInfo.DB_SQL_SYSTABS_TBCOUNT;
            int count = int.Parse(SqliteHelper.ExecuteScalar(sql).ToString());
            if (count == 0)
            {
                SqliteHelper.ExecuteNonQuery(DBStructureInfo.INIT_DB_SQL_CREATETABLE_TASK);
                SqliteHelper.ExecuteNonQuery(DBStructureInfo.INIT_DB_SQL_CREATETABLE_LOG);

                SqliteHelper.ExecuteNonQuery(DBStructureInfo.INIT_DB_SQL_CREATETABLE_HEART);
                this.InitHeartTable();
            }
        }

        /// <summary>
        /// 加载计划数据到内存中
        /// </summary>
        private void LoadTaskDataFromDB()
        {
            _DataSet.PL_TimerTask.Clear();
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask.TableName;
            DataSet ds = SqliteHelper.ExecuteDataset(sql);
            ds.Tables[0].TableName = _DataSet.PL_TimerTask.TableName;
            _DataSet.PL_TimerTask.Merge(ds.Tables[0]);
        }

        #region IDataAccess 成员

        public List<TaskEntity> GetTasks(bool paraEnable)
        {
            return this.GetTasks().FindAll(delegate(TaskEntity entity)
            {
                return entity.Enable == paraEnable;
            });
        }

        public List<TaskEntity> GetTasks(string paraTaskName)
        {
            return this.GetTasks().FindAll(delegate(TaskEntity entity)
            {
                return entity.Name == paraTaskName;
            });
        }

        public List<TaskEntity> GetTasks()
        {
            List<TaskEntity> list = new List<TaskEntity>();
            this.LoadTaskDataFromDB();
            foreach (TaskDataSet.PL_TimerTaskRow taskRow in _DataSet.PL_TimerTask.Rows)
            {
                list.Add(Mapper.DataMapper.MappingTaskEntity(taskRow));
            }
            return list;
        }

        public Int64 AddTask(TaskEntity paraTask)
        {
            try
            {
                TaskDataSet.PL_TimerTaskRow taskRow = _DataSet.PL_TimerTask.NewPL_TimerTaskRow();
                Mapper.DataMapper.ReseveMappingTaskEntity(paraTask, ref taskRow);
                _DataSet.PL_TimerTask.AddPL_TimerTaskRow(taskRow);
                this.Save2DB();
                paraTask.SetKeyID(taskRow.ID);
                return taskRow.ID;
            }
            catch
            {
                throw;
            }
        }

        public void ModifyTask(Int64 paraTaskId, TaskEntity paraTask)
        {
            try
            {
                this.LoadTaskDataFromDB();
                TaskDataSet.PL_TimerTaskRow taskRow = _DataSet.PL_TimerTask.FindByID(paraTaskId);
                if (taskRow == null)
                {
                    throw new Exception("修改任务错误：传入的任务ID非法！");
                }
                else
                {
                    Mapper.DataMapper.ReseveMappingTaskEntity(paraTask, ref taskRow);
                }
                this.Save2DB();
            }
            catch
            {
                throw;
            }
        }

        public void RemoveTask(Int64 paraTaskId)
        {
            try
            {
                this.LoadTaskDataFromDB();
                TaskDataSet.PL_TimerTaskRow taskRow = _DataSet.PL_TimerTask.FindByID(paraTaskId);
                if (taskRow == null)
                {
                    throw new Exception("删除任务错误：传入的任务ID非法！");
                    //return;
                }
                else
                {
                    if (taskRow.Enable == bool.FalseString)
                        return;
                    taskRow.Enable = bool.FalseString;
                    this.Save2DB();
                    return;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool DelTaskComplet(long paraTaskId)
        {
            try
            {
                this.LoadTaskDataFromDB();
                TaskDataSet.PL_TimerTaskRow taskRow = _DataSet.PL_TimerTask.FindByID(paraTaskId);
                if (taskRow == null)
                {
                    //throw new Exception("修改任务错误：传入的任务ID非法！");
                    return false;
                }
                else
                {
                    taskRow.Delete();
                    this.Save2DB();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool WriteLog(LogEntity paraLog)
        {
            TaskDataSet.PL_TimerTask_LogRow logRow = _DataSet.PL_TimerTask_Log.NewPL_TimerTask_LogRow();
            Mapper.DataMapper.ReserMappingLogEntity(paraLog, ref logRow);
            _DataSet.PL_TimerTask_Log.AddPL_TimerTask_LogRow(logRow);
            return Save2DB();
        }
        public void WriteLog(long paraTaskid, string paraTaskName, string paraContent, Component.TimerTask.Model.Enums.LogType paraLogType)
        {
            LogEntity log = new LogEntity();
            log.LogContent = paraContent;
            log.LogType = paraLogType;
            log.TaskID = paraTaskid;
            log.TaskName = paraTaskName;
            this.WriteLog(log);
        }
 

        public bool Save2DB()
        {
            string s = string.Empty;
            return SqliteHelper.SaveDataSet(_DataSet, _DataSet.PL_TimerTask.TableName, ref s) |
               SqliteHelper.SaveDataSet(_DataSet, _DataSet.PL_TimerTask_Log.TableName, ref s);
        }

        #endregion

        #region GetLog


        public DataTable GetLog(string paraRegestedAppName)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " +
                _DataSet.PL_TimerTask_Log.TaskIDColumn.ColumnName + " IN ( SELECT " +
                _DataSet.PL_TimerTask.IDColumn.ColumnName + " FROM " + _DataSet.PL_TimerTask.TableName + " WHERE " +
                _DataSet.PL_TimerTask.TaskAppNameColumn.ColumnName + "='" + paraRegestedAppName + "')";
            DataTable dt = SqliteHelper.ExecuteDataset(sql).Tables[0];
            return dt;
        }

        public DataTable GetLog(DateTime paraDateStart, DateTime paraDateEnd)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " +
                _DataSet.PL_TimerTask_Log.LogDateColumn.ColumnName + ">=datetime('{0}')" + " AND " +
                _DataSet.PL_TimerTask_Log.LogDateColumn.ColumnName + "<=datetime('{1}')";
            DataTable dt = SqliteHelper.ExecuteDataset(string.Format(sql, paraDateStart.ToString("s"), paraDateEnd.ToString("s"))).Tables[0];
            //TaskDataSet.PL_TimerTask_LogDataTable table = new TaskDataSet.PL_TimerTask_LogDataTable(dt);

            return dt;// table;
        }

        public System.Data.DataSet GetAllLog()
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName;
            return SqliteHelper.ExecuteDataset(sql);
        }

        public DataTable GetLog(Int64 paraTaskId)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName;
            SQLiteParameter p = new SQLiteParameter("@TaskIDColumn", paraTaskId);
            p.DbType = DbType.Int64;
            DataTable dt = SqliteHelper.ExecuteDataset(sql, p).Tables[0];
            //TaskDataSet.PL_TimerTask_LogDataTable table = new TaskDataSet.PL_TimerTask_LogDataTable(dt);

            return dt;
            //List<LogEntity> list = new List<LogEntity>();
            //foreach (TaskDataSet.PL_TimerTask_LogRow row in table.Rows)
            //{
            //    list.Add(Mapper.DataMapper.MappingLogEntity(row));
            //}
            //return list;
        }
        #endregion

        #region IDataAccess 成员


        public LogEntity GetLog_LatestRun(long paraTaskId, LogType paraLogType)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " + " ID=( SELECT MAX(ID) FROM " +
                _DataSet.PL_TimerTask_Log.TableName + " WHERE " + _DataSet.PL_TimerTask_Log.LogTypeColumn.ColumnName + "='" + paraLogType.ToString() + "')";
            DataTable dt = SqliteHelper.ExecuteDataset(sql).Tables[0];
            TaskDataSet.PL_TimerTask_LogDataTable table = new TaskDataSet.PL_TimerTask_LogDataTable(dt);
            if (table.Rows.Count > 0)
            {
                return Mapper.DataMapper.MappingLogEntity((TaskDataSet.PL_TimerTask_LogRow)table.Rows[0]);
            }
            else
                return null;
        }

        #endregion

        #region 心跳相关


        public void WriteHeartDate()
        {
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            string sql = string.Format("UPDATE PL_TimerTask_Heart SET LogDate = '{0}'", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            SqliteHelper.ExecuteNonQuery(sql);
        }

        public DateTime ReadHeartDate()
        {
            string sql = "SELECT * FROM PL_TimerTask_Heart";
            DataRow dr = SqliteHelper.ExecuteDataRow(sql);
            if (dr == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                return DateTime.Parse(dr[0].ToString());
            }
        }

        public void InitHeartTable()
        {
            string sql = string.Format("INSERT INTO PL_TimerTask_Heart(LogDate) VALUES('{0}')", DateTime.MinValue.ToString("yyyy-MM-dd hh:mm:ss"));
            SqliteHelper.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
