using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Component.TimerTask.Model.Enums;
using Component.TimerTask.Model;

namespace Component.TimerTask.DAL
{
    /// <summary>
    /// 日志相关数据访问类
    /// </summary>
    class DataAccess_Log
    {
        LogDataSet _DataSet = new LogDataSet();
        public DataAccess_Log()
        {
            CheckAndInitTable();
        }

        #region Private Function
        /// <summary>
        /// 设置自增列的种子
        /// </summary>
        private void CheckAndInitTable()
        {
            try
            {
                string sql = "SELECT MAX(ID) FROM {0}";
                sql = string.Format(sql, _DataSet.PL_TimerTask_Log.TableName);
                DBUtility.SQLiteHelper.ExecuteDataSet(DBStructureInfo.ConnectionString_log, sql, null);
            }
            catch
            {
                CheckAndInitDataBase();
            }
        }

        private void CheckAndInitDataBase()
        {
            string sql = DBStructureInfo.DB_SQL_SYSTABS_TBCOUNT;
            int count = int.Parse(DBUtility.SQLiteHelper.ExecuteScalar(DBStructureInfo.ConnectionString_log, sql, null).ToString());
            if (count == 0)
            {
                DBUtility.SQLiteHelper.ExecuteNonQuery(DBStructureInfo.ConnectionString_log, DBStructureInfo.INIT_DB_SQL_CREATETABLE_LOG, null);
            }
        }
        #endregion

        public System.Data.DataSet GetAllLog()
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName;
            return DBUtility.SQLiteHelper.ExecuteDataSet(DBStructureInfo.ConnectionString_log, sql, null);
        }

        public DataTable GetLog(List<string> taskIds)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " +
                _DataSet.PL_TimerTask_Log.TaskIDColumn.ColumnName + " IN ( " + string.Join(",", taskIds.ToArray()) + ")";
            return DBUtility.SQLiteHelper.ExecuteDataSet(DBStructureInfo.ConnectionString_log, sql, null).Tables[0];
        }

        public DataTable GetLog(DateTime paraDateStart, DateTime paraDateEnd)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " +
                _DataSet.PL_TimerTask_Log.LogDateColumn.ColumnName + ">=datetime('{0}')" + " AND " +
                _DataSet.PL_TimerTask_Log.LogDateColumn.ColumnName + "<=datetime('{1}')";
            sql = string.Format(sql, paraDateStart.ToString("s"), paraDateEnd.ToString("s"));
            return DBUtility.SQLiteHelper.ExecuteDataSet(DBStructureInfo.ConnectionString_log, sql, null).Tables[0];
        }

        public DataTable GetLog(Int64 paraTaskId)
        {

            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " + _DataSet.PL_TimerTask_Log.TaskIDColumn.ColumnName + "=" + paraTaskId.ToString();
            return DBUtility.SQLiteHelper.ExecuteDataSet(DBStructureInfo.ConnectionString_log, sql, null).Tables[0];
        }

        public LogEntity GetLog_LatestRun(long paraTaskId, LogType paraLogType)
        {
            string sql = "SELECT * FROM " + _DataSet.PL_TimerTask_Log.TableName + " WHERE " + " ID=( SELECT MAX(ID) FROM " +
                _DataSet.PL_TimerTask_Log.TableName + " WHERE " + _DataSet.PL_TimerTask_Log.LogTypeColumn.ColumnName + "='" + paraLogType.ToString() + "')";
            DataTable dt = DBUtility.SQLiteHelper.ExecuteDataSet(DBStructureInfo.ConnectionString_log, sql, null).Tables[0];
            LogDataSet.PL_TimerTask_LogDataTable table = new LogDataSet.PL_TimerTask_LogDataTable(dt);
            if (table.Rows.Count > 0)
            {
                return Mapper.DataMapper.MappingLogEntity((LogDataSet.PL_TimerTask_LogRow)table.Rows[0]);
            }
            else
                return null;
        }

        public bool WriteLog(LogEntity paraLog)
        {
            string sql = "INSERT INTO " + _DataSet.PL_TimerTask_Log.TableName + " (" +
                _DataSet.PL_TimerTask_Log.TaskIDColumn.ColumnName + "," +
                _DataSet.PL_TimerTask_Log.TaskNameColumn.ColumnName + "," +
                _DataSet.PL_TimerTask_Log.LogTypeColumn.ColumnName + "," +
                _DataSet.PL_TimerTask_Log.LogContentColumn.ColumnName + "," +
                _DataSet.PL_TimerTask_Log.LogDateColumn.ColumnName + "" + 
                ") VALUES(" +
                "@" + _DataSet.PL_TimerTask_Log.TaskIDColumn.ColumnName + "," +
                "@" + _DataSet.PL_TimerTask_Log.TaskNameColumn.ColumnName + "," +
                "@" + _DataSet.PL_TimerTask_Log.LogTypeColumn.ColumnName + "," +
                "@" + _DataSet.PL_TimerTask_Log.LogContentColumn.ColumnName + "," +
                "@" + _DataSet.PL_TimerTask_Log.LogDateColumn.ColumnName + "" + 
                ")";
            object[] paraList = new object[5];
            paraList[0] = paraLog.TaskID;
            paraList[1] = paraLog.TaskName;
            paraList[2] = paraLog.LogType.ToString();
            paraList[3] = paraLog.LogContent;
            paraList[4] = paraLog.LogDate;
            int i = DBUtility.SQLiteHelper.ExecuteNonQuery(DBStructureInfo.ConnectionString_log, sql, paraList);
            return i > 0 ? true : false;
        }
    }
}
