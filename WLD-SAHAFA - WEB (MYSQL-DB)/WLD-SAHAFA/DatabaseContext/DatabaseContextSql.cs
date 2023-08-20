using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.DynamicData;
using WLD_SAHAFA.Extension;

namespace WLD_SAHAFA
{
    public class DatabaseContextSql : IDisposable
    {
        private readonly SqlConnection _con;

        public DatabaseContextSql()
        {
            _con = DatabaseFactorySql.GetConnection();
            _con.Open();
        }

        public void Dispose()
        {
            _con.Close();
            _con.Dispose();
        }
        public IList<T> GetDataTable<T>(string query)
        {
            using (var cmd = new SqlCommand(query, _con))
            using (var da = new SqlDataAdapter(cmd))
            {
                var dataTable = new DataTable();
                da.Fill(dataTable);
                return dataTable.ToList<T>();
            }
        }
    }
    public static class DatabaseFactorySql
    {
        public static SqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            return new SqlConnection(connString);
        }
    }
}