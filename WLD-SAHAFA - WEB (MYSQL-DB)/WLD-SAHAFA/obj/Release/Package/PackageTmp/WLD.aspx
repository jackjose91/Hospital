<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WLD.aspx.cs" Inherits="WLD_SAHAFA.WLD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Waiting List Display</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<style>
    /* CSS styles for top panel */
    body {
        margin: 0;
        padding: 0;
        font-family: Arial, sans-serif;
        overflow: hidden;
    }

    .content-container {
        height: calc(100vh - 100px); /* Adjust the height based on your top panel height */
        display: flex;
        justify-content: space-between;
    }

    .top-panel {
        background-color: #f2f2f2;
        padding: 20px;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .logo1 {
        margin-right: 10px;
        height: 50px; /* Adjust the height as needed */
    }

    .heading {
        font-size: 24px;
        font-weight: bold;
        flex-grow: 1;
        text-align: center;
    }

    .current-time {
        font-size: 18px;
        margin-left: 10px;
    }

    .video-grid-container {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
        margin-top: 20px;
        padding: 0 20px;
        width: 98%;
        height: 70vh;
        margin-left: 0; /* Remove the left margin */
    }

    .video-container {
        width: calc(60% + 40%); /* Adjust the width based on the grid width */
        height: 100%;
        margin-bottom: 20px;
        margin-right: 20px; /* Add a right margin for the gap */
    }

    .video-container video {
        width: 100%;
        height: 100%;
        object-fit: contain;
    }

    .grid-section {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        grid-template-rows: repeat(4 , 1fr);
        gap: 5px; /* Adjust the gap between grid items as desired */
        width: 80%; /* Adjust the width of the grid section as needed */
        margin: 0 auto; /* Center the grid section horizontally */
    }

    .grid-item {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      background-color: #e3f2fd; /* Light blue background color for grid items */
      padding: 20px;
      text-align: center;
    }

    .grid-item:nth-child(4n-3),
    .grid-item:nth-child(4n-2) {
        background-color: #1976d2; /* Medium dark blue background color for odd rows */
        color: #fff; /* White text color for odd rows */
    }

    .grid-item:nth-child(4n-1),
    .grid-item:nth-child(4n) {
        background-color: #bbdefb; /* Light blue background color for even rows */
    }

    .token,
    .counter {
      font-size: 25px;
      margin-bottom: 10px;
    }

</style>
</head>
<body>
    <form id="form" runat="server" >
        <div class="top-panel">
            <img class="logo1" src="IMAGES/COMPANY_LOGO/LOGO3.png" alt="Company Logo 1" />
            <h1 class="heading">Welcome to DR. SULAIMAN AL HABIB HOSPITAL</h1>
            <span id="current-time" class="current-time"></span>
        </div>

        <div class="video-grid-container">
            <div class="video-container">
                <video id="videoPlayer" src="VIDEOS/Advertisement/V1.mp4" controls="controls" autoplay muted ></video>
            </div>
            <div class="grid-section">
            <div class="grid-item">
                <div class="token" runat="server" id="token1"></div>
                <div class="counter" runat="server" id="counter1"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token2"></div>
                <div class="counter" runat="server" id="counter2"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token3"></div>
                <div class="counter" runat="server" id="counter3"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token4"></div>
                <div class="counter" runat="server" id="counter4"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token5"></div>
                <div class="counter" runat="server" id="counter5"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token6"></div>
                <div class="counter" runat="server" id="counter6"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token7"></div>
                <div class="counter" runat="server" id="counter7"></div>
            </div>
            <div class="grid-item">
                <div class="token" runat="server" id="token8"></div>
                <div class="counter" runat="server" id="counter8"></div>
            </div>
            <div class="grid-item">
            <div class="token" runat="server" id="token9"></div>
            <div class="counter" runat="server" id="counter9"></div>
            </div>
<%--        <div class="grid-item">
        <asp:Label runat="server" ID="label1" Text="Token:" CssClass="token"></asp:Label>
        <div class="token" runat="server" id="token1"></div>
        <asp:Label runat="server" ID="labelCounter1" Text="Counter:" CssClass="counter"></asp:Label>
        <div class="counter" runat="server" id="counter1"></div>
        </div>
        <!-- Repeat the above code for the remaining grid items (2 to 8) -->
        <!-- Add asp:Label controls for each token and counter value -->
        <div class="grid-item">
        <asp:Label runat="server" ID="label2" Text="Token:" CssClass="token"></asp:Label>
        <div class="token" runat="server" id="token2"></div>
        <asp:Label runat="server" ID="labelCounter2" Text="Counter:" CssClass="counter"></asp:Label>
        <div class="counter" runat="server" id="counter2"></div>
         </div>--%>
         </div>
        </div>
        <asp:Button ID="btnTriggerClick" runat="server" Text="Trigger Click" OnClick="btnTriggerClick_Click" style="display:none;" />
         <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" OnClientClick="return false;" AutoPostBack="false" style="display:none;" />

        <!-- Rest of the content -->


</form>
<%--    <script>

    //function updateCounterValue(value) {
    //    document.getElementById('counter1').innerText = value;
    //}

    //// Function to make the AJAX request to the server-side method
    //function fetchDataAndUpdate() {
    //    var xmlhttp = new XMLHttpRequest();
    //    xmlhttp.onreadystatechange = function () {
    //        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
    //            // Get the response from the server-side function
    //            var response = xmlhttp.responseText;

    //            // Update the counter element with the received value
    //            updateCounterValue(response);
    //        }
    //    };

    //    // Make the AJAX request to the server-side method
    //    xmlhttp.open("GET", "WLD.aspx/passvalues", true);
    //    xmlhttp.setRequestHeader("Content-type", "application/json");
    //    xmlhttp.send();
    //}

    //// Call the function on page load
    //document.addEventListener("DOMContentLoaded", function () {
    //    // Call fetchDataAndUpdate immediately on page load
    //    fetchDataAndUpdate();

    //    // Call fetchDataAndUpdate every 1 second (1000 milliseconds)
    //    setInterval(fetchDataAndUpdate, 1000);
    //});

        //// Function to perform the redirection
        //function redirectToGetData() {

        //// Redirect to the GETDATA.aspx page with query parameters   ////WORKING CODE
        //window.location.href = 'FetchData.aspx';
        //}

        //// Call the function after a short delay (adjust as needed)
        //setTimeout(redirectToGetData, 2000);

        // JavaScript code to update the current time
        setInterval(function () {
        var currentTime = new Date();

        // Get the day, month, and year
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var dayOfWeek = days[currentTime.getDay()];
        var day = currentTime.getDate();
        var month = currentTime.toLocaleString('default', { month: 'long' });
        var year = currentTime.getFullYear();

        // Get the formatted time
        var hours = currentTime.getHours();
        var minutes = currentTime.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12;
        minutes = minutes < 10 ? '0' + minutes : minutes;

        var formattedTime = dayOfWeek + ' ' + day + ' ' + month + ' ' + year + ', ' + hours + '.' + minutes + ' ' + ampm;

        document.getElementById("current-time").textContent = formattedTime;
        }, 1000);

        // JavaScript code to play the video continuously
        var videoPlayer = document.getElementById("videoPlayer");

        videoPlayer.addEventListener("ended", function () {
            videoPlayer.currentTime = 0;
            videoPlayer.play();
        });

        // JavaScript code to play the video automatically
        window.addEventListener('DOMContentLoaded', function () {
            var videoPlayer = document.getElementById('videoPlayer');
            videoPlayer.play();
        });


        function fetchDataAndDisplay() {
            // Fetch data from the server
            $.ajax({
                type: 'POST',
                url: 'WLD.aspx/UpdateData',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    // Update the token and counter elements with fetched data
                    document.getElementById('token1').innerText = data.Token;
                    document.getElementById('counter1').innerText = data.Counter;
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error fetching data:', errorThrown);
                }
            });
        }

        // Fetch data immediately on page load
        fetchDataAndDisplay();

        // Periodically fetch data every 5 seconds (5000 milliseconds)
        setInterval(fetchDataAndDisplay, 1000);


        //// Function to fetch data from the server and update the tokens and counters
        //function fetchDataAndCallUpdate() {
        //    var xmlhttp = new XMLHttpRequest();
        //    xmlhttp.onreadystatechange = function () {
        //        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
        //            var data = JSON.parse(xmlhttp.responseText);
        //            var tokenValue = data.Token;
        //            var counterValue = data.Counter;
        //            UpdateTokensAndCounters(tokenValue, counterValue);
        //        }
        //    };
        //    xmlhttp.open("GET", "WLD.aspx/ParseData", true);
        //    xmlhttp.setRequestHeader("Content-type", "application/json");
        //    xmlhttp.send();
        //}

        //// Function to update the tokens and counters on the page
        //function UpdateTokensAndCounters(tokenValue, counterValue) {
        //    // JavaScript code to update the token and counter elements on the page
        //    // Perform actions based on token and counter values
        //    if (counterValue === 'C1') {
        //        document.getElementById('token1').innerText = tokenValue;
        //        document.getElementById('counter1').innerText = counterValue;
        //    } else if (counterValue === 'C2') {
        //        document.getElementById('token2').innerText = tokenValue;
        //        document.getElementById('counter2').innerText = counterValue;
        //    } else if (counterValue === 'C3') {
        //        document.getElementById('token3').innerText = tokenValue;
        //        document.getElementById('counter3').innerText = counterValue;
        //    } else if (counterValue === 'C4') {
        //        document.getElementById('token4').innerText = tokenValue;
        //        document.getElementById('counter4').innerText = counterValue;
        //    } else if (counterValue === 'C5') {
        //        document.getElementById('token5').innerText = tokenValue;
        //        document.getElementById('counter5').innerText = counterValue;
        //    } else if (counterValue === 'C6') {
        //        document.getElementById('token6').innerText = tokenValue;
        //        document.getElementById('counter6').innerText = counterValue;
        //    } else if (counterValue === 'C7') {
        //        document.getElementById('token7').innerText = tokenValue;
        //        document.getElementById('counter7').innerText = counterValue;
        //    } else if (counterValue === 'C8') {
        //        document.getElementById('token8').innerText = tokenValue;
        //        document.getElementById('counter8').innerText = counterValue;
        //    }
        //}

        //// Fetch data immediately on page load
        //fetchDataAndCallUpdate();

        //// Periodically fetch data every 5 seconds (5000 milliseconds)
        //setInterval(fetchDataAndCallUpdate, 5000);

        // Similar updates for other tokens and counters can be done here.
        // You may use loops or other logic to update multiple elements as needed.


    //    // Call the function on page load and every 2 seconds
    //    document.addEventListener("DOMContentLoaded", function () {
    //    // Call the function to fetch data and update the tokens and counters on page load
    //    fetchDataAndCallUpdate();

    //     //Call the function every 2 seconds to update the tokens and counters automatically
    //    setInterval(fetchDataAndCallUpdate, 1000); // 1000 milliseconds (2 seconds)
    //});

</script>--%>

<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script type="text/javascript">
  $(document).ready(function fetchDataAndDisplay()
    {
        // Fetch data from the server
        $.ajax({
            type: 'GET',
            url: 'http://localhost:63780/FetchData.aspx/FetchUpdatedData', // Update the URL if needed
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                //    // Check if data is empty or null
                //    if (data && data.length > 0)
                //    {
                //        // Update the token and counter elements with fetched data
                //        for (var i = 0; i < data.length; i++)
                //        {
                //            var receiptNo = data[i].ReceiptNo;
                //            var counterNo = data[i].CounterNo;

                //            // Update the appropriate token and counter elements
                //            var tokenElement = document.getElementById('token' + (i + 1));
                //            var counterElement = document.getElementById('counter' + (i + 1));

                //            if (tokenElement && counterElement)
                //            {
                //                tokenElement.innerText = receiptNo;
                //                counterElement.innerText = counterNo;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        // Handle empty data by setting all tokens and counters to "null"
                //        for (var i = 1; i <= 8; i++)
                //        {
                //            var tokenElement = document.getElementById('token' + i);
                //            var counterElement = document.getElementById('counter' + i);

                //            if (tokenElement && counterElement)
                //            {
                //                tokenElement.innerText = "null";
                //                counterElement.innerText = "null";
                //            }
                //        }
                //    }
                //},
                //error: function (xhr, textStatus, errorThrown)
                //{
                //    console.error('Error fetching data:', errorThrown);
                //}
            }
        });
    });
    // Fetch data immediately on page load
    fetchDataAndDisplay();

    // Periodically fetch data every 2 seconds (2000 milliseconds)
  setInterval(fetchDataAndDisplay, 10000);

</script>
</body>
</html>
