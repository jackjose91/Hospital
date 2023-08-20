$(document).ready(function () {
    var oldData = [];
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