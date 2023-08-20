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
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace WLD_SAHAFA
{
    public partial class WLD : System.Web.UI.Page
    {
        private System.Timers.Timer timer = new System.Timers.Timer();

        public string Repno { get; set; }
        public string Counterno { get; set; }

        public Thread T = null;
        private TcpListener tcpListener;
        private Thread listenThread;
        string opr;
        // Set the TcpListener on port 13000.
        Int32 port = 9100;
        IPAddress localAddr = IPAddress.Any;
        Byte[] bytes = new Byte[256];

        SqlConnection conn = connection.DatabaseFactory.GetConnection();

        //private System.Timers.Timer timer;

        // Define global variables
        private List<string> receivedDataList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();

            FetchAndUpdateValues();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    this.tcpListener = new TcpListener(IPAddress.Any, port);
            //    this.listenThread = new Thread(new ThreadStart(ListenForClients));
            //    this.listenThread.Start();

            //    // Set the timer interval and attach the event handler
            //    timer.Interval = 2000; // 2000 milliseconds = 2 seconds
            //    timer.Elapsed += Timer_Elapsed;
            //    timer.Start();
            //}
            // This event will be triggered every 2 seconds
            // Fetch and update values from the database

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


        public void ParseData(string data)
        {

            // Calculate the length of the string data
            int dataLength = data.Length;
            if (dataLength == 8)
            {
                //// Return an empty string when dataLength is 8
                //return string.Empty;
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

                    SqlCommand cmd = new SqlCommand("INSERT INTO ORDER_LIST (TOKEN_NO,COUNTER_NO,STATUS) values (@TOKEN_NO,@COUNTER_NO,@STATUS)", conn);

                    cmd.Parameters.AddWithValue("@TOKEN_NO", Repno.ToString());
                    cmd.Parameters.AddWithValue("@COUNTER_NO", Counterno.ToString());
                    cmd.Parameters.AddWithValue("@STATUS", "0".ToString());

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    FetchAndUpdateValues();
                }
                
                catch
                {
                    ////Return an empty string when dataLength is 8
                    //return string.Empty;
                }

                finally
                {
                    conn.Close();
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            // Fetch and update values from the database
            FetchAndUpdateValues();
        }

        private void FetchAndUpdateValues()
        {
            //try
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT TOP 8 TOKEN_NO, COUNTER_NO, STATUS, [DATE] FROM ORDER_LIST WHERE STATUS = '0' AND CONVERT(DATE, [DATE]) = CONVERT(DATE, GETDATE()) ORDER BY ID DESC;", conn);

            //    conn.Open();
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
            //                    token1.InnerText = "TOKEN NO: "+ReceiptNo;
            //                    counter1.InnerText = "COUNTER NO: " + CounterNo;
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
            //                    token6.InnerText = ReceiptNo;
            //                    counter6.InnerText = CounterNo;
            //                    break;
            //                case 6:
            //                    token7.InnerText = ReceiptNo;
            //                    counter7.InnerText = CounterNo;
            //                    break;
            //                case 7:
            //                    token8.InnerText = ReceiptNo;
            //                    counter8.InnerText = CounterNo;
            //                    break;
            //            }

            //            labelIndex++;
            //        }
            //        dr.Close();
            //    }
            //}

            //catch
            //{

            //}

            //finally
            //{
            //    conn.Close();
            //}
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

        public static string passvalues(string repno, string counterno)
        {
            // Example: Construct the JSON data using param1 and param2
            string jsonData = "{\"Repno\": \"" + repno + "\", \"Counterno\": \"" + counterno + "\"}";

            // Return the JSON data
            return jsonData;
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

    }
}
