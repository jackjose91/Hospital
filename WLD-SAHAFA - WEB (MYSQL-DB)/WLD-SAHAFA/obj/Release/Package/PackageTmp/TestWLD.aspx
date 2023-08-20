<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWLD.aspx.cs" Inherits="WLD_SAHAFA.TestWLD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Waiting List Display</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="js/jquery-3.6.0.min.js"></script> 
    <link rel="stylesheet" type="text/css" href="Style/style1.css" /> 
     
       <script>
           $(document).ready(function () {
               setInterval(function () {
                   test();
               }, 5000);

               function test() {
                   $.ajax({
                       type: "POST",
                       url: "Webservice.asmx/GETData",
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (data) {
                           console.log(data.d)
                           console.log($('#divSection').html());
                           $('#divSection').html(data.d)
                       }
                   });
               }
           });

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

           //// JavaScript code to play the video continuously
           //document.addEventListener('DOMContentLoaded', function () {
           //    var videoPlayer = document.getElementById('videoPlayer');

           //    videoPlayer.addEventListener('ended', function () {
           //        videoPlayer.currentTime = 0;
           //        videoPlayer.play();
           //    });

           //    videoPlayer.play();
           //});

           $(document).ready(function () {
               var mediaContainer = $('.video-container');

               function loadMedia(mediaFiles, mediaType) {
                   mediaFiles.forEach(function (filename) {
                       var mediaElement;

                       if (mediaType === 'image') {
                           mediaElement = $('<img>').attr('src', 'images/' + filename);
                       } else if (mediaType === 'video') {
                           mediaElement = $('<video>').attr('src', 'videos/' + filename)
                               .attr('controls', 'controls')
                               .attr('autoplay', 'autoplay');
                       }

                       mediaContainer.append(mediaElement);
                   });
               }

               // Replace 'GetMediaFiles.aspx' with the actual path to your server-side script
               $.getJSON('TestWLD.aspx', function (data) {
                   loadMedia(data.images, 'image');
                   loadMedia(data.videos, 'video');
               })
                   .fail(function (jqxhr, textStatus, error) {
                       console.error('Error fetching media files:', error);
                   });
           });

           //$(document).ready(function () {
           //    var mediaContainer = $('.media-container');

           //    function loadMedia(mediaFiles, mediaType) {
           //        mediaFiles.forEach(function (filename) {
           //            var mediaElement;

           //            if (mediaType === 'image') {
           //                mediaElement = $('<img>').attr('src', 'images/' + filename);
           //            } else if (mediaType === 'video') {
           //                mediaElement = $('<video>').attr('src', 'videos/' + filename)
           //                    .attr('controls', 'controls')
           //                    .attr('autoplay', 'autoplay');
           //            }

           //            mediaContainer.append(mediaElement);
           //        });
           //    }

           //    // Fetch media file names using AJAX
           //    $.getJSON('api/MediaController/GetMediaFiles', function (data) {
           //        loadMedia(data.images, 'image');
           //        loadMedia(data.videos, 'video');
           //    })
           //        .fail(function (jqxhr, textStatus, error) {
           //            console.error('Error fetching media files:', error);
           //        });
           //});

       </script>
</head>
<body>
    <form id="form" runat="server">
        <div class="top-panel">
            <img class="logo1" src="IMAGES/COMPANY_LOGO/LOGO3.png" alt="Company Logo 1" />
            <span id="current-time" class="current-time"></span>
        </div>

        <div class="video-grid-container">

        <div class="video-container">
                <video id="videoPlayer" src="VIDEO/Advertisement/V1.mp4" controls="controls" autoplay muted></video>
            </div>

            <div class="grid-section" id="divSection">
            </div>
        </div>
        <!-- Rest of the content -->
    </form>
</body>
</html>
