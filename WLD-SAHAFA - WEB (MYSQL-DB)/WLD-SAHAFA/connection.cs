using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WLD_SAHAFA
{
    public class connection
    {
        public sealed class DatabaseFactory
        {
            static MySqlConnection databaseConnection = null;
            public static MySqlConnection getDBConnection()
            {
                if (databaseConnection == null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                    databaseConnection = new MySqlConnection(connectionString);
                }
                return databaseConnection;
            }
            //public static SqlConnection GetConnection()
            //{
            //    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            //    string connString = myConnection.ConnectionString.ToString();
            //    SqlConnection con = new SqlConnection(connString);
            //    return con;
            //}

        }
    }
}