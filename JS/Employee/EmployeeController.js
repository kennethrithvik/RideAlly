$(function () {
    
    var surl = "http://192.168.3.185:8025/Rideally.service/api/Employee";
    $employees = [];
    $.getJSON(surl, function (data) {
        console.log(data);
    });
});