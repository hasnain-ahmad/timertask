using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SQLite;
using System.Data.Common;

namespace Component.TimerTask.DAL
{

    /// <summary>
    /// Date: 2010-6-20 10:59
    /// Author: Administrator
    /// FullName: Component.TimerTask.DAL.SqliteHelper
    /// Class: SQLite帮助类
    /// </summary>
    internal static class SqliteHelper
    {
        //private const string CON_STR = "Data Source=timertaskdb.db3";

        /// <summary>
        /// 获得连接对象
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection GetSQLiteConnection()
        {
            return new SQLiteConnection(DBStructureInfo.ConnectionString);
        }

        /// <summary>
        /// 执行Command前准备
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="p"></param>
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params SQLiteParameter[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
                cmd.Parameters.AddRange(p);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string cmdText, params SQLiteParameter[] p)
        {
            DataSet ds = new DataSet();
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// 查询出一行记录
        /// ：第一行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DataRow ExecuteDataRow(string cmdText, params SQLiteParameter[] p)
        {
            DataSet ds = ExecuteDataset(cmdText, p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            return null;
        }

        /// <summary>
        /// 执行更新等操作 返回受影响的行数
        /// </summary>
        /// <param name="cmdText">a</param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, params SQLiteParameter[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(command, connection, cmdText, p);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 批量执行SQL，启用事务
        /// </summary>
        /// <param name="sqlList"></param>
        public static void ExecuteNonQuery(IEnumerable sqlList)
        {
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(command, connection, string.Empty);
                DbTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string sql in sqlList)
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 返回SQLiteDataReader对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static SQLiteDataReader ExecuteReader(string cmdText, params SQLiteParameter[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = GetSQLiteConnection();
            try
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters">传入的参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, params SQLiteParameter[] p)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(cmd, connection, cmdText, p);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 支持分页的查询
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cmdText"></param>
        /// <param name="countText"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, params SQLiteParameter[] p)
        {
            if (recordCount < 0)
                recordCount = int.Parse(ExecuteScalar(countText, p).ToString());
            DataSet ds = new DataSet();
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
            }
            return ds;
        }


        #region 采用DataSet提交数据

        /// <summary>
        /// 取得选取语句
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strTbName"></param>
        /// <returns></returns>
        private static string GetSelectSql(DataSet ds, string strTbName)
        {
            string strSql = string.Empty;
            DataTable dt;
            if (strTbName == string.Empty)
            {
                dt = ds.Tables[0];
                strTbName = dt.TableName;
            }
            else
                dt = ds.Tables[strTbName];

            foreach (DataColumn dc in dt.Columns)
            {
                strSql += dc.ColumnName + ",";
            }

            strSql = "Select " + strSql.Substring(0, strSql.Length - 1) + " From " + strTbName;
            return strSql;

        }


        /// <summary>
        /// 取得更新命令
        /// </summary>
        /// <param name="da"></param>
        /// <returns></returns>
        private static SQLiteCommand GetUpdateCommand(SQLiteDataAdapter da)
        {
            string strSql = string.Empty, strSql1 = string.Empty, strTable = string.Empty;
            SQLiteCommand cmdUpdate = new SQLiteCommand();
            using(SQLiteConnection connection = GetSQLiteConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, da.SelectCommand.CommandText);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                DataTable tb1 = rdr.GetSchemaTable();
                rdr.Close();
                connection.Close();
                foreach (DataRow row in tb1.Rows)
                {
                    //INSERT INTO temp111(w1, w2, w3, w4, w5, w6) VALUES (@w1, @w2, @w3, @w4, @w5, @w6)" +
                    strSql += row["ColumnName"].ToString() + @"=@" + row["ColumnName"].ToString() + ",";

                    if (row["DataType"].ToString() == "System.Decimal")
                        cmdUpdate.Parameters.Add(new SQLiteParameter("@" + row["ColumnName"].ToString(), (DbType)row["ProviderType"], (int)row["ColumnSize"], ParameterDirection.Input, false, ((System.Byte)(System.Convert.ToInt32(row["NumericPrecision"]))), ((System.Byte)(System.Convert.ToInt32(row["NumericScale"]))), row["ColumnName"].ToString(), System.Data.DataRowVersion.Current, null));
                    else
                        cmdUpdate.Parameters.Add(new SQLiteParameter("@" + row["ColumnName"].ToString(), (DbType)row["ProviderType"], (int)row["ColumnSize"], row["ColumnName"].ToString()));

                    strTable = row["BaseTableName"].ToString();
                    if ((bool)row["isKey"])
                    {
                        strSql1 += row["ColumnName"].ToString() + "=@Original_" + row["ColumnName"].ToString() + " And ";
                        cmdUpdate.Parameters.Add(new SQLiteParameter("@Original_" + row["ColumnName"].ToString(), (DbType)row["ProviderType"], (int)row["ColumnSize"], row["ColumnName"].ToString()));
                    }
                }

                strSql = " Update " + strTable + "  SET " + strSql.Substring(0, strSql.Length - 1) + " where " + strSql1.Substring(0, strSql1.Length - 4);
                cmdUpdate.CommandText = strSql;
                cmdUpdate.Connection = da.SelectCommand.Connection;
                return cmdUpdate;
            }

        }

        /// <summary>
        /// 取得插入命令
        /// </summary>
        /// <param name="da"></param>
        /// <returns></returns>
        private static SQLiteCommand GetInsertCommand(SQLiteDataAdapter da)
        {
            string strSql = string.Empty, strSql1 = string.Empty, strTable = string.Empty;
            SQLiteCommand cmdAdd = new SQLiteCommand(da.SelectCommand.CommandText);
            using(SQLiteConnection connection = GetSQLiteConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, da.SelectCommand.CommandText);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                DataTable tb1 = rdr.GetSchemaTable();
                rdr.Close();
                connection.Close();
                foreach (DataRow row in tb1.Rows)
                {

                    //INSERT INTO temp111(w1, w2, w3, w4, w5, w6) VALUES (@w1, @w2, @w3, @w4, @w5, @w6)" +
                    strSql += row["ColumnName"].ToString() + ",";
                    strSql1 += "@" + row["ColumnName"].ToString() + ",";

                    if (row["DataType"].ToString() == "System.Decimal")
                        cmdAdd.Parameters.Add(new SQLiteParameter("@" + row["ColumnName"].ToString(), (DbType)row["ProviderType"], (int)row["ColumnSize"], ParameterDirection.Input, false, ((System.Byte)(System.Convert.ToInt32(row["NumericPrecision"]))), ((System.Byte)(System.Convert.ToInt32(row["NumericScale"]))), row["ColumnName"].ToString(), System.Data.DataRowVersion.Current, null));
                    else
                        cmdAdd.Parameters.Add(new SQLiteParameter("@" + row["ColumnName"].ToString(), (DbType)row["ProviderType"], (int)row["ColumnSize"], row["ColumnName"].ToString()));
                    strTable = row["BaseTableName"].ToString();
                }

                strSql = " INSERT INTO " + strTable + "(" + strSql.Substring(0, strSql.Length - 1) + ") Values (" + strSql1.Substring(0, strSql1.Length - 1) + ")";
                cmdAdd.CommandText = strSql;
                cmdAdd.Connection = da.SelectCommand.Connection;
                return cmdAdd;
            }
        }


        /// <summary>
        /// 取得删除命令
        /// </summary>
        /// <param name="da"></param>
        /// <returns></returns>
        private static SQLiteCommand GetDeleteCommand(SQLiteDataAdapter da)
        {
            string strSql = string.Empty, strTable = string.Empty;
            SQLiteCommand cmdDel = new SQLiteCommand(da.SelectCommand.CommandText);
            using (SQLiteConnection connection = GetSQLiteConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, da.SelectCommand.CommandText);
                SQLiteDataReader rdr = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                DataTable tb1 = rdr.GetSchemaTable();
                rdr.Close();
                connection.Close();
                foreach (DataRow row in tb1.Rows)
                {

                    if ((bool)row["isKey"])
                    {
                        strSql += row["ColumnName"].ToString() + "=@Original_" + row["ColumnName"].ToString() + " And ";
                        cmdDel.Parameters.Add(new SQLiteParameter("@Original_" + row["ColumnName"].ToString(), (DbType)row["ProviderType"], (int)row["ColumnSize"], row["ColumnName"].ToString()));
                    }

                    strTable = row["BaseTableName"].ToString();
                }
                strSql = "DELETE FROM " + strTable + " WHERE " + strSql.Substring(0, strSql.Length - 4);
                cmdDel.CommandText = strSql;
                cmdDel.Connection = da.SelectCommand.Connection;
                return cmdDel;
            }
        }

        /// <summary>
        /// 保存记录集
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strTbName"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static bool SaveDataSet(DataSet ds, string strTbName, ref string strErr)
        {
            bool bi = true;
            string strSqlSelect = GetSelectSql(ds, strTbName);
            SQLiteConnection cn = GetSQLiteConnection();
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(strSqlSelect, cn);
                da.UpdateCommand = GetUpdateCommand(da);
                da.DeleteCommand = GetDeleteCommand(da);
                da.InsertCommand = GetInsertCommand(da);

                if (string.IsNullOrEmpty(strTbName)) da.Update(ds);
                else da.Update(ds, strTbName);
            }
            catch (Exception e)
            {
                strErr = e.Message;
                bi = false;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cn.Dispose();
            }
            return bi;
        }
        
        /// <summary>
        /// 不标识名字,取第一个dt,dt必须带名字
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public static bool SaveDataSet(DataSet ds, ref string strErr)
        {
            return SaveDataSet(ds, string.Empty, ref strErr);
        }

 
        #endregion
    }
}
