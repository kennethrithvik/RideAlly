$(function () {
    
    var surl = "/api/Employee";
    $employees = [];
    $.getJSON(surl, function (data) {
        console.log(data);
    });
});