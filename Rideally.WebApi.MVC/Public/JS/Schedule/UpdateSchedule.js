var scheduleobject;
$(function () {

    var employeeid = localStorage.getItem("EmployeeID");
    var eurl = "/api/Schedule/GetScheduleByEmployeeID?id=" + employeeid;
    $schedules = [];
    

    $.getJSON(eurl, function (data) {
        console.log(data);

        scheduleobject = data;

        console.log("I got it");
        //$.each(data, function (i, x) {

        document.getElementById("UpdateSchId").textContent = data.ScheduleId;
        document.getElementById("SchStatus").textContent = data.ScheduledStatus;
        $date = data.ScheduledDate;
            var mainParts = $date.split('T');
            document.getElementById("UpdateSchDate").value = mainParts[0];
        document.getElementById("UpdateSchTime").value = data.ScheduledTime;
        document.getElementById("UpdateSeatsAvail").value = data.SeatsAvailable;
            console.log(data);
            
    });





    $("#btnUpdate").on('click', function () {


        //--------------------------
        var flag = true;
        var date = $("#UpdateSchDate").val();
        if (date == "") {
            alert("Please Enter Date!");
            flag = false;
        } else if 
            (!(date.match(/^(?:(19|20)[0-9]{2})[\- \/.](0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])$/))) {
            flag = false;
            alert("Date Format Error");
        }

        var time = $("#UpdateSchTime").val();
        if (time == " ") {
            alert("Please Enter Time!");
            flag = false;
        }
        if (time != "" || time != null) {
            if (time.match(/^([1-9]|1[0-2]):([0-5]\d)\s?(AM|PM)?$/i))
                flag = true;
        }


        var seats = $("#UpdateSeatsAvail").val();
        if (seats == "") {
            alert('Seats availablity cannot be empty!');
            flag = false;
        }
        if (seats != "") {
            if (isNaN(seats)) {
                alert("Enter Numeric value only!");
                flag = false;
            }
            if (!(seats > 1 && seats < 10)) {
                alert("Enter between 1 to 10!");
                flag = false;
            }
        }
        //--------------------------


        if (flag == true) {

            scheduleid = $("#UpdateSchId").text();
            schedulestatus = $("#SchStatus").text();
            scheduledate = ($("#UpdateSchDate").val());
            scheduletime = ($("#UpdateSchTime").val());
            seatsavailable = ($("#UpdateSeatsAvail").val());

            //scheduleobj = {
            //    ScheduleId: scheduleid,
            //    ScheduledStatus: schedulestatus,
            //    ScheduledDate: scheduledate,
            //    ScheduledTime: scheduletime,
            //    SeatsAvailable: seatsavailable
            //};
            scheduleobject.SeatsAvailable = seatsavailable;
            scheduleobject.ScheduledDate = scheduledate;
            scheduleobject.ScheduledTime = scheduletime;
            //console.log(scheduleobj);

            $.ajax({
                url: "/API/Schedule",
                type: 'POST',
                data: scheduleobject,
                dataType: 'JSON',
                success: function (data) {
                    console.log(data);
                    alert('Schedule Updated Successfully')
                },
                failure: function (msg) {
                    console.log(msg);
                }

            });
        }

    });

    });


  


