using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codegenerator.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace codegenerator.BLL
{
    public class DatabaseUtilities
    {
        private ILogger<DatabaseUtilities> _logger;
        public DatabaseUtilities(ILogger<DatabaseUtilities> logger)
        {
            _logger=logger;
        }
        public  bool TestConnection(string connectionString)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
                if (conn.State==System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                    return true;
                }
                conn.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error in TestConnection");
                return false;
            }
        }

        public  List<SQLTableModel> GetDatabaseTables(string connectionString)
        {
            SqlConnection conn = new SqlConnection();
            List<SQLTableModel> tableList = null;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select id,name from sys.sysobjects " +
                                          "where xtype='U' and name not in ('sysdiagrams') " +
                                          "order by name";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            tableList = new List<SQLTableModel>();
                            while (reader.Read())
                            {
                                SQLTableModel table = new SQLTableModel();
                                table.id = (int)reader.GetInt32(0);
                                table.name = (string)reader.GetString(1);

                                tableList.Add(table);
                            }
                            reader.Close();
                        }
                    }
                }
                return tableList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error in GetDatabaseTables");
                tableList = null;
                return tableList;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public  List<SQLTableFieldModel> GetTableFields(string connectionString,int tableId,string tableName)
        {
            SqlConnection conn = new SqlConnection();
            List<SQLTableFieldModel> fieldList = null;
            List<SQLTableSpecialFieldModel> keyFieldList = null;
            List<SQLTableSpecialFieldModel> identityFieldList = null;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select cols.name, types.name as 'fieldType', cols.isnullable,cols.length,cols.xprec,cols.xscale " +
                                          "from sys.syscolumns cols inner join sys.systypes types  " +
                                          "on cols.xusertype=types.xusertype where cols.id=@P1 " +
                                          "order by colorder";
                    SqlParameter p1 = new SqlParameter("@P1",tableId);
                    command.Parameters.Add(p1);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            fieldList = new List<SQLTableFieldModel>();
                            while (reader.Read())
                            {
                                SQLTableFieldModel field = new SQLTableFieldModel();
                                field.name = (string)reader.GetString(0);
                                field.fieldType = (string)reader.GetString(1);
                                field.isnullable = (int)reader.GetInt32(2);
                                field.length = (int)reader.GetInt16(3);
                                field.xprec = (byte)reader.GetByte(4);
                                field.xscale = (byte)reader.GetByte(5);
                                field.isPrimaryKey = false; //populate it later
                                field.isIdentity = false;//populate it later
                                fieldList.Add(field);
                            }
                            reader.Close();
                            keyFieldList = GetTableKeyFields(connectionString, tableName);
                            if (keyFieldList != null)
                            {
                                for (int i = 0; i <= fieldList.Count - 1; i++)
                                {
                                    foreach (var keyfield in keyFieldList)
                                    {
                                        if (keyfield.COLUMN_NAME == fieldList[i].name)
                                        {
                                            fieldList[i].isPrimaryKey = true;
                                        }
                                    }
                                }
                            }
                            identityFieldList = GetTableIdentityFields(connectionString, tableName);
                            if (identityFieldList != null)
                            {
                                for (int i = 0; i <= fieldList.Count - 1; i++)
                                {
                                    foreach (var identityfield in identityFieldList)
                                    {
                                        if (identityfield.COLUMN_NAME == fieldList[i].name)
                                        {
                                            fieldList[i].isIdentity = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return fieldList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error in GetTableFields");
                fieldList = null;
                return fieldList;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public List<SQLTableSpecialFieldModel> GetTableKeyFields(string connectionString, string tableName)
        {
            SqlConnection conn = new SqlConnection();
            List<SQLTableSpecialFieldModel> fieldList = null;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT COLUMN_NAME " +
                                          "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                                          "WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 " +
                                          "AND TABLE_NAME = @P1";
                    SqlParameter p1 = new SqlParameter("@P1", tableName);
                    command.Parameters.Add(p1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            fieldList = new List<SQLTableSpecialFieldModel>();
                            while (reader.Read())
                            {
                                SQLTableSpecialFieldModel field = new SQLTableSpecialFieldModel();
                                field.COLUMN_NAME = (string)reader.GetString(0);
                                
                                fieldList.Add(field);
                            }
                            reader.Close();
                        }
                    }
                }
                return fieldList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTableKeyFields");
                fieldList = null;
                return fieldList;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public List<SQLTableSpecialFieldModel> GetTableIdentityFields(string connectionString, string tableName)
        {
            SqlConnection conn = new SqlConnection();
            List<SQLTableSpecialFieldModel> fieldList = null;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT COLUMN_NAME " +
                                          "FROM INFORMATION_SCHEMA.COLUMNS " +
                                          "WHERE COLUMNPROPERTY(object_id(TABLE_SCHEMA+'.'+TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 " +
                                          "AND TABLE_NAME = @P1";
                    SqlParameter p1 = new SqlParameter("@P1", tableName);
                    command.Parameters.Add(p1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            fieldList = new List<SQLTableSpecialFieldModel>();
                            while (reader.Read())
                            {
                                SQLTableSpecialFieldModel field = new SQLTableSpecialFieldModel();
                                field.COLUMN_NAME = (string)reader.GetString(0);

                                fieldList.Add(field);
                            }
                            reader.Close();
                        }
                    }
                }
                return fieldList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTableIdentityFields");
                fieldList = null;
                return fieldList;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}
