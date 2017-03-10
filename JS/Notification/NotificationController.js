$(function () {
    var employeeid = localStorage.getItem("EmployeeID");
    var surl = "http://192.168.3.185:8025/Rideally.service/api/Notification/" + employeeid;
    var coridererid = null;
  
    $.ajax({
        dataType: "json",
        url: surl,
        async: false,
        type: 'GET',
        success: function (data) {

            $.each(data, function (i, x) {
                $date = x.MessageDate;

                var mainParts = $date.split('T');
                var tr = '<tr>';
                tr += '<td>' + mainParts[0] + '</td>';
                var employeename = null;

                var url = "http://192.168.3.185:8025/Rideally.service/api/Employee/" + x.FromEmployeeId;
                coridererid = x.FromEmployeeId;
                $.ajax({
                    dataType: "json",
                    url: url,
                    async: false,
                    type: 'GET',
                    success: function (data) {
                    
                        employeename = data.EmployeeName;

                    },
                    failure: function (msg) {
                        console.log(msg);
                    }
                });


                tr += '<td>' + employeename + '</td>';
                tr += '<td>' + x.Message + '</td>';

                if (x.RequestType == 0) {

                    tr += '<td>' + ' <input type="button" class="acceptbutton" id=' + x.FromEmployeeId + '_' + x.ScheduleId +'_'+"a"+ ' style="width:100px;color:#0BAFA0;" value="ACCEPT"/></td>';

                    tr += '<td>' + ' <input type="button" class="rejectbutton" id=' + x.FromEmployeeId + '_' + x.ScheduleId +'_'+"r"+ ' style="width:100px;color:#0BAFA0;" value="REJECT"/></td>';

                }
                else {
                    tr += '<td></td>'
                    tr += '<td></td>'
                }
                tr += '</tr>';
                $("#notificationtable").append(tr);



            });
        },
        failure: function (msg) {
            console.log(msg);
        }
    });


    $("#Notificationclosebtn").on('click', function () {
       
        window.location = "http://192.168.3.185:8025/Rideallyfinalui/View/Home.html";
    });


    $(".acceptbutton").on('click', function () {

        var ids = (this.id).split('_');

        var currentdate = new Date();
        var date = currentdate.getDate() + "/" + (currentdate.getMonth() + 1)
                        + "/" + currentdate.getFullYear();
        var time = currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds();
        if ($("#" + this.id).attr('value') === "ACCEPT") {
            note = {

                ToEmployeeId: ids[0],
                FromEmployeeId: employeeid,//logedin employeeID
                Status: "New",
                MessageTime: time,
                RequestType: 1,
                MessageDate: date,
                Message: "Hurray your request has been Accepted...!",
                ScheduleId: ids[1]

            }

            sendnotification(note, this.id, "accept");
            var corider = { sid: ids[1], coriderid: coridererid };

            $.ajax({
                url: "http://192.168.3.185:8025/Rideally.service/api/Notification/RiderAccept",
                async: false,
                type: 'PUT',
                data: corider,
                datatype: 'json',
                success: function (data) {

                    console.log(data);
                },
                failure: function (msg) {
                    console.log(msg);
                }

            });

        } else if ($("#" + this.id).attr('value') === "COMPLETED") {
            console.log(this.id + "completed");
            console.log(ids[1])
            var scheduleid = { schid: ids[1] }
            $.ajax({
                url: "http://192.168.3.185:8025/Rideally.service/api/Notification/Update",
                async: false,
                type: 'POST',
                data: scheduleid,
                datatype: 'json',
                success: function (data) {

                    console.log(data);
                },
                failure: function (msg) {
                    console.log(msg);
                }

            });

        }

    });
    $(".rejectbutton").on('click', function () {
        var ids = (this.id).split('_');
        var currentdate = new Date();
        var date = currentdate.getDate() + "/" + (currentdate.getMonth() + 1)
                        + "/" + currentdate.getFullYear();
        var time = currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds();
        note = {

            ToEmployeeId: ids[0],
            FromEmployeeId: employeeid,//logedin employeeID
            Status: "New",
            MessageTime: time,
            RequestType: 2,
            MessageDate: date,
            Message: "Sorry your request has been Rejected",
            ScheduleId:ids[1]
        }
        console.log(note);
        sendnotification(note, this.id, "reject");


    });
    var sendnotification = function (note, id, state) {
        
       // alert($("#"+id).attr('value'));
        //console.log(note);
      var ids = id.split('_');
        $.ajax({
            url: "http://192.168.3.185:8025/Rideally.service/api/Notification",
            type: 'POST',
            data: note,
            dataType: 'JSON',
            success: function (data) {
                //console.log(data);
                if(data==true)
                {
                    if (state == "accept") {
                        $("#" + id).attr("value", "COMPLETED");
                        $("#" + ids[0] + "_" + ids[1] + "_" +"r").hide();
                        alert("Thanks for Accepting Request");

                    } else if (state == "reject") {
                        $("#" + ids[0] + "_" + ids[1] + "_" + "a").hide();
                        $("#" + ids[0] + "_" + ids[1] + "_" + "r").hide();
                        alert("You are successfully Rejected Request");

                    }
                    //alert("hi");
                    
                    //console.log($("#"+id));
                    //$('#id').replaceWith('<button type="button" class="acceptbutton" id="Completebutton" style="width:100px;color:#0BAFA0;">Complete</button>')
                }

            },
            failure: function (msg) {
                console.log(msg);
            }
        });

    }
});