$(document).ready(function () {
   
    $("#logmsgs").hide();
    $("#logmsgf").hide();
    $("#LogSub").click(logSubFunc);
});


var logSubFunc = function () {
    var LogEmp = {
        EmailID: $("#LogEmail").val(),
        Password: $("#LogPass").val()
    };
    //console.log(RegEmp);



    if (LogEmp.EmailID === "") {
        $("#logmsgs").hide();
        $("#logmsgf").text("EmailID cannot be null");
        $("#logmsgf").show();
        return false;
    }

    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);

    if (!pattern.test(LogEmp.EmailID)) {
        $("#logmsgs").hide();
        $("#logmsgf").text("Email is invalid");
        $("#logmsgf").show();
        return false;
    }
    if (LogEmp.Password === "") {
        $("#logmsgs").hide();
        $("#logmsgf").text("Please Enter password");
        $("#logmsgf").show();
        return false;
    }

    //console.log(LogEmp);
    $.ajax({
        type: "POST",

        url: "http://192.168.3.185:8025/Rideally.service/api/Employee/Login",
        datatype: "json",
        data: LogEmp,

        success: function (data) {
            //data = JSON.parse(data1);
            console.log(data);
            if (data == null) {
                $("#logmsgs").hide();
                $("#logmsgf").text("Error Invalid Credentials");
                $("#logmsgf").show();                
            }

            else {
                $("#logmsgs").text("Login Successful");
                $("#logmsgf").hide();
                $("#logmsgs").show();
                localStorage.setItem("EmployeeID", data.EmployeeID);
                localStorage.setItem("EmployeeName", data.EmployeeName);
                localStorage.setItem("EmailID", data.EmailID);
                window.location.href = "View/Home.html";
                //var a = localStorage.getItem("EmployeeID");
                //console.log(a);
            }

        },

        error: function (er) {
            console.log(er);
            $("#logmsgs").hide();
            $("#logmsgf").text("Error in Logging in");
            $("#logmsgf").show();
        }

    });
    //return false;
}
