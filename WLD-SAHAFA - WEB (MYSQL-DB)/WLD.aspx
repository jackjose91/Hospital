<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WLD.aspx.cs" Inherits="WLD_SAHAFA.WLD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Waiting List Display</title>
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
        background-color: white;
        padding: 20px;
        display: flex;
        justify-content: space-between;
        align-items: center;
      height: 34px;
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
        margin-top: 1px;
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
        width: 58%; /* Adjust the width of the grid section as needed */
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
    <form id="form1" runat="server" >
        <div class="top-panel">
            <img class="logo1" src="IMAGES/COMPANY_LOGO/LOGO3.png" alt="Company Logo 1" />
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

         </div>
        </div>
<%--    <asp:Button ID="btnTriggerClick" runat="server" Text="Trigger Click" OnClick="btnTriggerClick_Click" style="display:none;" />--%>
<%--        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" style="display:none;" />--%>

         <!-- Add a hidden field to store the JSON data -->
        <input type="hidden" id="receivedDataJSON" runat="server" />

        <!-- Rest of the content -->
    </form>

    <script>

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

        // Function to perform the redirection
        function redirectToGetData() {

        // Redirect to the GETDATA.aspx page with query parameters   ////WORKING CODE
        window.location.href = 'FetchData.aspx';
        }

        // Call the function after a short delay (adjust as needed)
        setTimeout(redirectToGetData, 2000);
    </script>
</body>
</html>
