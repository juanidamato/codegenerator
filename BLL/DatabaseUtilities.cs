using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codegenerator.Models;
using Microsoft.Data.SqlClient;
namespace codegenerator.BLL
{
    public class DatabaseUtilities
    {

        public static bool TestConnection(string connectionString)
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
                return false;
            }
        }

        public static List<SQLTableModel> GetDatabaseTables(string connectionString)
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
                    command.CommandText = "select id,name from sys.sysobjects where xtype='U' order by name";
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

        public static List<SQLTableFieldModel> GetTableFields(string connectionString,int tableId)
        {
            SqlConnection conn = new SqlConnection();
            List<SQLTableFieldModel> fieldList = null;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select cols.name, types.name as 'fieldType', cols.isnullable from sys.syscolumns cols inner join sys.systypes types  on cols.xusertype=types.xusertype where cols.id=@P1 order by colorder";
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
