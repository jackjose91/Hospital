using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;
using System.Web.Script.Serialization;

namespace WLD_SAHAFA
{
    public static class DatabaseConnection
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
    }

    public partial class WLD : System.Web.UI.Page
    {
       // SqlConnection conn = connection.DatabaseFactory.GetConnection();

        public string Repno { get; set; }
        public string Counterno { get; set; }
        public Thread T = null;
        private TcpListener tcpListener;
        private Thread listenThread;
        // Set the TcpListener on port 13000.
        Int32 port = 63782;
        IPAddress localAddr = IPAddress.Any;
        Byte[] bytes = new Byte[256];

        private System.Timers.Timer timer;

        // Define global variables
        private List<string> receivedDataList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();


            //FetchAndUpdateValues();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[25000];
            int bytesRead;
            Thread.Sleep(1000);
            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 25000);
                }
                catch
                {
                    //a socket error has occured
                    // System.Windows.MessageBox.Show("socket");
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    // System.Windows.MessageBox.Show("disc");
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                //if(!IsPostBack)
                ParseData(encoder.GetString(message, 0, bytesRead));


                //UpdateDataHandler(encoder.GetString(message, 0, bytesRead));
            }

            tcpClient.Close();
        }


        private void ParseData(string data)
        {
            // Calculate the length of the string data
            int dataLength = data.Length;
            if (dataLength == 8)
            {

            }

            else
            {
                try
                {
                    string[] splitDelimeter = { "," };
                    var listofwld_data = data.Split(splitDelimeter, StringSplitOptions.None).ToList();
                    string firstvalue = listofwld_data[0].ToString();
                    Repno = firstvalue.Substring(1);

                    // FOR FINDING THE COUNTER-NO FROM THE RECEIVED DATA
                    Counterno = "";
                    Counterno = C_no(listofwld_data[2], listofwld_data[4]);

                    // Update tokens and counters based on the received values
                    UpdateData(Repno, Counterno);
                    //UpdateTokensAndCounters(Repno, Counterno);

                    //passvalues();

                    //// Register the JavaScript code to be rendered on the page
                    //ClientScript.RegisterStartupScript(this.GetType(), "UpdateTokensAndCounters", script);


                    //if (Counterno == "C1")
                    //{

                    //    //token1.InnerHtml = Repno;
                    //    //counter1.InnerHtml = Counterno;

                    //    //// In your ParseData method, generate the JavaScript code to call the client-side function with updated values
                    //    //string script = $@"<script>
                    //    //var tokenValue = '{Repno}';
                    //    //var counterValue = '{Counterno}';
                    //    //UpdateTokensAndCounters(tokenValue, counterValue);
                    //    //</script>";

                    //    //// Directly write the script to the page
                    //    //Response.Write(script);
                    //}

                    //else if (Counterno == "C2")
                    //{
                    //    token2.InnerText = Repno;
                    //    counter2.InnerText = Counterno;
                    //}

                    //else if (Counterno == "C3")
                    //{
                    //    token3.InnerText = Repno;
                    //    counter3.InnerText = Counterno;
                    //}

                    //else if (Counterno == "C4")
                    //{
                    //    token4.InnerText = Repno;
                    //    counter4.InnerText = Counterno;
                    //}

                    //else if (Counterno == "C5")
                    //{
                    //    token5.InnerText = Repno;
                    //    counter5.InnerText = Counterno;
                    //}

                    //else if (Counterno == "C6")
                    //{
                    //    token6.InnerText = Repno;
                    //    counter6.InnerText = Counterno;
                    //}

                    //else if (Counterno == "C7")
                    //{
                    //    token7.InnerText = Repno;
                    //    counter7.InnerText = Counterno;
                    //}

                    //else /*(Counterno == "C8")*/
                    //{
                    //    token8.InnerText = Repno;
                    //    counter8.InnerText = Counterno;
                    //}

                    //// Call a JavaScript function to update the token and counter elements
                    //ClientScript.RegisterStartupScript(this.GetType(), "UpdateTokensCounters", "UpdateTokensAndCounters();", true);

                    //SqlCommand cmd = new SqlCommand("INSERT INTO ORDER_LIST (TOKEN_NO,COUNTER_NO,STATUS,DATE) values (@TOKEN_NO,@COUNTER_NO,@STATUS,@DATE)", conn);

                    //cmd.Parameters.AddWithValue("@TOKEN_NO", Repno.ToString());
                    //cmd.Parameters.AddWithValue("@COUNTER_NO", Counterno.ToString());
                    //cmd.Parameters.AddWithValue("@STATUS", "0".ToString());

                    //// Get the current date
                    //DateTime currentDate = DateTime.Now.Date; // This will give the current date with the time portion set to midnight (00:00:00)

                    //// Add the current date as a parameter to the SqlCommand
                    //cmd.Parameters.AddWithValue("@DATE", currentDate);

                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();

                    //FetchAndUpdateValues();
                }
                catch
                {
                    // Return an empty string when dataLength is 8
                    //return string.Empty;
                }

                finally
                {
                   // conn.Close();
                }
            }
        }

        public static string UpdateData(string Repno, string Counterno)
        {
            // Simulate data retrieval from the server (replace this with your actual data retrieval logic)
            string tokenValue = Repno;
            string counterValue = Counterno;

            var data = new Dictionary<string, string>
            {
                { "Token", tokenValue },
                { "Counter", counterValue }
            };

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);
        }

        private void UpdateTokensAndCounters(string tokenValue, string counterValue)
        {
            var data = new
            {
                Token = tokenValue,
                Counter = counterValue
            };

            var responseData = data;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(responseData);

            // Return the JSON data to the client-side script
            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(jsonData);
            Response.End();
        }


        public string passvalues()
        {
            return Counterno;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            // Fetch and update values from the database
            //FetchAndUpdateValues();
        }

        //private void FetchAndUpdateValues()
        //{
        //    //SqlCommand cmd = new SqlCommand("SELECT TOP 8 TOKEN_NO, COUNTER_NO, STATUS, [DATE] FROM ORDER_LIST WHERE STATUS = '0' AND CONVERT(DATE, [DATE]) = CONVERT(DATE, GETDATE()) ORDER BY ID DESC;", conn);

        // //   conn.Open();
        //    cmd.ExecuteNonQuery();

        //    using (SqlDataReader dr = cmd.ExecuteReader())
        //    {
        //        int labelIndex = 0;

        //        while (dr.Read())
        //        {
        //            string ReceiptNo = dr["TOKEN_NO"].ToString();
        //            string CounterNo = dr["COUNTER_NO"].ToString();

        //            // Assign the retrieved values to the appropriate label controls
        //            switch (labelIndex)
        //            {
        //                case 0:
        //                    token1.InnerText = ReceiptNo;
        //                    counter1.InnerText = CounterNo;
        //                    break;
        //                case 1:
        //                    token2.InnerText = ReceiptNo;
        //                    counter2.InnerText = CounterNo;
        //                    break;
        //                case 2:
        //                    token3.InnerText = ReceiptNo;
        //                    counter3.InnerText = CounterNo;
        //                    break;
        //                case 3:
        //                    token4.InnerText = ReceiptNo;
        //                    counter4.InnerText = CounterNo;
        //                    break;
        //                case 4:
        //                    token5.InnerText = ReceiptNo;
        //                    counter5.InnerText = CounterNo;
        //                    break;
        //                case 5:
        //                    token5.InnerText = ReceiptNo;
        //                    counter5.InnerText = CounterNo;
        //                    break;
        //                case 6:
        //                    token6.InnerText = ReceiptNo;
        //                    counter6.InnerText = CounterNo;
        //                    break;
        //                case 7:
        //                    token7.InnerText = ReceiptNo;
        //                    counter7.InnerText = CounterNo;
        //                    break;
        //                case 8:
        //                    token8.InnerText = ReceiptNo;
        //                    counter8.InnerText = CounterNo;
        //                    break;
        //            }

        //            labelIndex++;
        //        }
        //        dr.Close();
        //    }
        //  //  conn.Close();

        //}


        private void RedirectWithData(string repno, string counterno)
        {
            // Generate the JavaScript code to redirect with query parameters
            string redirectScript = $@"<script>
                // Function to perform the redirection
                function redirectToGetData() {{
                    var repno = '{HttpUtility.UrlEncode(repno)}';
                    var counterno = '{HttpUtility.UrlEncode(counterno)}';
                    var url = 'GETDATA.aspx?repno=' + repno + '&counterno=' + counterno;
                    window.location.href = url;
                }}
                // Call the function after a short delay (adjust as needed)
                setTimeout(redirectToGetData, 100);
            </script>";

            // Register the JavaScript code to be rendered on the page
            ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", redirectScript);
        }

        protected void btnTriggerClick_Click(object sender, EventArgs e)
        {
            // Check if Repno and Counterno are not empty before redirecting
            if (!string.IsNullOrEmpty(Repno) && !string.IsNullOrEmpty(Counterno))
            {
                // Redirect to the GETDATA.aspx page with query parameters
                Response.Redirect("GETDATA.aspx?repno=" + HttpUtility.UrlEncode(Repno) + "&counterno=" + HttpUtility.UrlEncode(Counterno));
            }

            else
            {

            }
        }

        public void UpdateDataHandler(string data)
        {
            try
            {
                string[] splitDelimeter = { "," };
                var listofwld_data = data.Split(splitDelimeter, StringSplitOptions.None).ToList();
                string firstvalue = listofwld_data[0].ToString();
                string Repno = firstvalue.Substring(1);

                // FOR FINDING THE COUNTER-NO FROM THE RECEIVED DATA
                string Counterno = "";
                //Counterno = C_no(listofwld_data[2], listofwld_data[4]);

                //// Create an anonymous object with the updated values
                //var updatedData = new { Repno = Repno, Counterno = Counterno };

                //// Convert the object to JSON format
                //var jsonData = new JavaScriptSerializer().Serialize(updatedData);

                //// Set the content type to JSON
                //Response.ContentType = "application/json";

                //// Send the JSON data back to the client
                //Response.Write(jsonData);
                //Response.End();
                //return string.Empty;

                //if (HttpContext.Current != null)
                //{
                //    // Access Session variables
                //    Session["repno"] = Repno;
                //    Session["counterno"] = Counterno;

                //    // Redirect to the GETDATA.aspx page
                //    Response.Redirect("GETDATA.aspx");
                //}

                //passvalues(Repno, Counterno);

                //// Set the text of the labels with the values
                //token1.InnerText = Repno;
                //counter1.InnerText = Counterno;

                //// Create an anonymous object with the updated values
                //var updatedData = new { Repno = Repno, Counterno = Counterno };

                //// Convert the object to JSON format
                //var jsonData = new JavaScriptSerializer().Serialize(updatedData);

                //// Set the content type to JSON
                //HttpContext.Current.Response.ContentType = "application/json";

                //// Return the JSON data
                //return jsonData;

                //// Register the script block to send JSON data to the client-side and update the elements
                //string updateScript = @"<script>
                //        function updateData(data) {
                //            var parsedData = JSON.parse(data);
                //            var repno = parsedData.Repno;
                //            var counterno = parsedData.Counterno;
                //            // Update the client-side elements with the new values
                //            document.getElementById('token1').innerText = repno;
                //            document.getElementById('counter1').innerText = counterno;
                //        }

                //        var jsonData = " + jsonData + @";
                //        updateData(jsonData); // Call the updateData function with the JSON data
                //    </script>";

                //// Register the updateScript to be rendered on the page
                //ClientScript.RegisterStartupScript(this.GetType(), "UpdateScript", updateScript);

                if (Counterno == "C1")
                {

                    token1.InnerHtml = Repno;
                    counter1.InnerHtml = Counterno;
                }

                else if (Counterno == "C2")
                {
                    token2.InnerText = Repno;
                    counter2.InnerText = Counterno;
                }

                else if (Counterno == "C3")
                {
                    token3.InnerText = Repno;
                    counter3.InnerText = Counterno;
                }

                else if (Counterno == "C4")
                {
                    token4.InnerText = Repno;
                    counter4.InnerText = Counterno;
                }

                else if (Counterno == "C5")
                {
                    token5.InnerText = Repno;
                    counter5.InnerText = Counterno;
                }

                else if (Counterno == "C6")
                {
                    token6.InnerText = Repno;
                    counter6.InnerText = Counterno;
                }

                else if (Counterno == "C7")
                {
                    token7.InnerText = Repno;
                    counter7.InnerText = Counterno;
                }

                else /*(Counterno == "C8")*/
                {
                    token8.InnerText = Repno;
                    counter8.InnerText = Counterno;
                }

                //// Register a script block to refresh the page after a short delay
                //string refreshScript = @"<script>
                //                setTimeout(function () {
                //                    location.reload();
                //                }, 1000); // Refresh after 1 second (adjust as needed)
                //             </script>";

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshScript", refreshScript);
            }
            catch (Exception ex)
            {
                //// Handle exceptions here if needed...
                //return string.Empty;
            }
        }

        private string C_no( string MWT_ID, string MWT_LOC)
        {
            string Counterno = "default";
            if (MWT_LOC == "R")
            {
                if (MWT_ID == "A")
                {
                    Counterno = "C1";
                    return Counterno;
                }

                else if (MWT_ID == "B")
                {
                    Counterno = "C2";
                    return Counterno;
                }

                else if (MWT_ID == "C")
                {
                    Counterno = "C3";
                    return Counterno;
                }

                else //MWT_ID == "D"
                {
                    Counterno = "C4";
                    return Counterno;
                }
            }

            else  //MWT_LOC = "L"
            {
                if (MWT_ID == "A")
                {
                    Counterno = "C8";
                    return Counterno;
                }

                else if (MWT_ID == "B")
                {
                    Counterno = "C7";
                    return Counterno;
                }

                else if (MWT_ID == "C")
                {
                    Counterno = "C6";
                    return Counterno;
                }

                else //MWT_ID == "D"
                {
                    Counterno = "C5";
                    return Counterno;
                }
            }
        }

        private void SpeakerAssistant(string token, int count, object p)
        {
            throw new NotImplementedException();
        }

        //private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    // Call the method to fetch and update the values
        //    FetchAndUpdateValues();
        //}

        //private void FetchAndUpdateValues()
        //{
        //    MySqlConnection con = WLD_SAHAFA.DatabaseConnection.getDBConnection();
        //    MySqlCommand cmd = new MySqlCommand();

        //    cmd.Connection = con;
        //    cmd.CommandText = "SELECT P_RECEIPT_NO, P_MWT_LOC FROM tb_patient_info WHERE DATE(P_RECEIPT_DAY) = CURDATE() AND P_USER_ID = 'admin' ORDER BY P_RECEIPT_NO DESC LIMIT 8";
        //    con.Open();

        //    using (MySqlDataReader dr = cmd.ExecuteReader())
        //    {
        //        int labelIndex = 0;

        //        while (dr.Read())
        //        {
        //            string receiptNo = dr["P_RECEIPT_NO"].ToString();
        //            string mwtLoc = dr["P_MWT_LOC"].ToString();

        //            // Assign the retrieved values to the appropriate label controls
        //            switch (labelIndex)
        //            {
        //                case 0:
        //                    token1.InnerText = receiptNo;
        //                    counter1.InnerText = mwtLoc;
        //                    break;
        //                case 1:
        //                    token2.InnerText = receiptNo;
        //                    counter2.InnerText = mwtLoc;
        //                    break;
        //                case 2:
        //                    token3.InnerText = receiptNo;
        //                    counter3.InnerText = mwtLoc;
        //                    break;
        //                case 3:
        //                    token4.InnerText = receiptNo;
        //                    counter4.InnerText = mwtLoc;
        //                    break;
        //                case 4:
        //                    token5.InnerText = receiptNo;
        //                    counter5.InnerText = mwtLoc;
        //                    break;
        //                case 5:
        //                    token6.InnerText = receiptNo;
        //                    counter6.InnerText = mwtLoc;
        //                    break;
        //                case 6:
        //                    token7.InnerText = receiptNo;
        //                    counter7.InnerText = mwtLoc;
        //                    break;
        //                case 7:
        //                    token8.InnerText = receiptNo;
        //                    counter8.InnerText = mwtLoc;
        //                    break;
        //            }

        //            labelIndex++;
        //        }
        //        dr.Close();
        //    }
        //    con.Close();
        //}

    }
}
