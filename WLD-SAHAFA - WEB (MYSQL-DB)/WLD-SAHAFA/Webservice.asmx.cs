using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using WLD_SAHAFA.Constants;
using WLD_SAHAFA.DatabaseContext;
using WLD_SAHAFA.Helper;
using WLD_SAHAFA.Models;

namespace WLD_SAHAFA
{
    /// Summary description for TempratureConverter  
    /// </summary>  
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.   
    [System.Web.Script.Services.ScriptService]
    public class Webservice : System.Web.Services.WebService
    {
        [WebMethod]
        public string GETData()
        {
            MySqlConnection con = connection.DatabaseFactory.getDBConnection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "SELECT P_RECEIPT_NO, P_MWT_LOC FROM tb_patient_info WHERE DATE(P_RECEIPT_DAY) = CURDATE() AND P_USER_ID = 'admin' ORDER BY P_RECEIPT_NO DESC LIMIT 8";
            con.Open();

            MySqlDataReader dr = cmd.ExecuteReader();

            var section = new StringBuilder();
            while (dr.Read())
            {
                String tokenNo = (dr["P_RECEIPT_NO"]).ToString();
                String counterNo = (dr["P_MWT_LOC"]).ToString();

                section.Append(@"<div class='grid-item'>
                <div class='token'>Token: " + tokenNo + @"</div>
                <div class='counter'>Counter: " + counterNo + @"</div>
                </div>");
            }

            dr.Close();
            con.Close();

            return section.ToString();
        }
        [WebMethod]
        public object GetDetails(IList<Counter> oldCounters)
        {
            var counters = GetData();
            var speechText = WebserviceHelper.BuildSpeechText(oldCounters, counters);
            return new
            {
                html = WebserviceHelper.BuildHtml(counters),
                textEnglish = speechText.Item1,
                textArabic = speechText.Item2,
                data= counters
            };
        }
        private IList<Counter> GetData()
        {
            var _context = new DatabaseContextMySql();
            return _context.GetDataTable<Counter>(QueryConstants.GetCounterDetails);
        }
   
    }
}
