<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWLD.aspx.cs" Inherits="WLD_SAHAFA.TestWLD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Waiting List Display</title>
    <script src="js/jquery-3.6.0.min.js"></script> 
    <link rel="stylesheet" type="text/css" href="Style/style1.css" /> 
    <script src="Scripts/moment.js"></script>
       <script>
           let voices = window.speechSynthesis.getVoices();
           window.speechSynthesis.onvoiceschanged = () => {
               voices = window.speechSynthesis.getVoices();
           };
           $(document).ready(function () {     
               var oldData=[];
               setInterval(function () {
                   GetCounterDetails();
               }, 5000);
               
               function GetCounterDetails() {
                   var old = JSON.stringify({ 'oldCounters': oldData });
                   $.ajax({
                       type: "POST",
                       url: "Webservice.asmx/GetDetails",
                       data: JSON.stringify({ 'oldCounters': oldData }),
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (data) {
                           let result = data.d
                           oldData = result.data;
                           $('#divSection').html(result.html);
                           console.log(result.textEnglish);
                           console.log(result.textArabic);      
                           speechEnglish(result.textEnglish);
                           speechArabic(result.textArabic);                                             
                       }
                   });
               }
           });
           function speechEnglish(text) {
               let voices = window.speechSynthesis.getVoices();

               let speech = new SpeechSynthesisUtterance();
               speech.voice = voices[0];
               speech.lang = "en";
               speech.rate = 1;
               speech.pitch = 1;
               speech.volume = 1;
               speech.text = text;
               window.speechSynthesis.speak(speech); 
           }
           function speechArabic(text) {
               let speech = new SpeechSynthesisUtterance();      
               speech.voice = voices[33];
               speech.lang = "en";
               speech.rate = 1;
               speech.pitch = 1;
               speech.volume = 1;
               speech.text = text;
               window.speechSynthesis.speak(speech);
           }
           setInterval(function () {             
               document.getElementById("current-time").textContent = moment().format('dddd DD MMMM YYYY, h:mm A');
           }, 1000);

           document.addEventListener('DOMContentLoaded', function () {
               var mediaPlayer = document.getElementById('videoPlayer');
               var imagePlayer = document.getElementById('imagePlayer');

               var videoFiles = [
                   'V1.mp4', // Add video filenames here
                   'V2.mp4',
                   // Add more video filenames here
               ];

               var imageFiles = [
                   'P1.jpg', // Add photo filenames here
                   'P2.jpg',
                   // Add more photo filenames here
               ];

               var currentIndex = 0;
               var currentMediaType = '';

               function loadNextMedia() {
                   if (currentMediaType === 'video') {
                       mediaPlayer.style.display = 'none';
                       imagePlayer.style.display = 'block';
                       imagePlayer.src = 'photos/' + imageFiles[currentIndex % imageFiles.length];
                       currentMediaType = 'image';
                   } else {
                       imagePlayer.style.display = 'none';
                       mediaPlayer.style.display = 'block';
                       mediaPlayer.src = 'videos/' + videoFiles[currentIndex % videoFiles.length];
                       mediaPlayer.type = 'video/mp4';
                       mediaPlayer.load();
                       mediaPlayer.play();
                       currentMediaType = 'video';
                   }

                   currentIndex++;
               }

               mediaPlayer.addEventListener('ended', function () {
                   loadNextMedia();
               });

               loadNextMedia(); // Load and play the first media
           });

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
             <%--<video id="videoPlayer" src="VIDEO/Advertisement/V1.mp4" controls="controls" autoplay muted></video>--%>
            <video id="videoPlayer" controls autoplay muted></video>
            <img id="imagePlayer" />
            </div>

            <div class="grid-section" id="divSection">
            </div>
        </div>
        <!-- Rest of the content -->
    </form>
         <button id="start" class="btn btn-success mt-5 me-3">Start</button>

</body>
<%--<script src="Scripts/textToSpeech.js"></script>--%>

</html>
