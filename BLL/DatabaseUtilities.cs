using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static List<string> GetDatabaseTables(string connectionString)
        {
            SqlConnection conn = new SqlConnection();
            List<string> tableList = null;
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (SqlCommand command=new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select name from sys.sysobjects where xtype='U' order by name";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if( reader.HasRows)
                        {
                            tableList = new List<string>();
                            while (reader.Read())
                            {
                                tableList.Add((string)reader.GetString(0));
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
    }
}
