using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using WLD_SAHAFA.Extension;

namespace WLD_SAHAFA.DatabaseContext
{
    public static class DatabaseFactoryMySql
    {
        public static MySqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            return new MySqlConnection(connString);
        }
    }

    public class DatabaseContextMySql: IDisposable
    {
        private readonly MySqlConnection _con;

        public DatabaseContextMySql()
        {
            _con = DatabaseFactoryMySql.GetConnection();
            _con.Open();
        }

        public void Dispose()
        {
            _con.Close();
            _con.Dispose();
        }
        public IList<T> GetDataTable<T>(string query)
        {
            using (var cmd = new MySqlCommand(query, _con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                var dataTable = new DataTable();
                da.Fill(dataTable);
                return dataTable.ToList<T>();
            }
        }
    }
}