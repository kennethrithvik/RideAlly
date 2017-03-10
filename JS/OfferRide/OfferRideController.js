$(document).ready(function () {
    $("#btnUpdate").on('click', function () {
        var VehicleID;
        var EmployeeID;
        var Direction;
        var FromAddress = new Object();
        var ToAddress = new Object();
        var EmpObj = new Object();
        var VehicleObj = null;
        var RemainingSeats;
        $.ajax(
          {
              url: "http://192.168.3.185:8025/Rideally.service/api/Employee",
              type: 'GET',
              data: { id: 1 },
              dataType: 'JSON',
              success: function (data) {


                  console.log("---------------------------Employee-----");
                  console.log(data);
                  EmpObj = data;
                  EmployeeID = data.EmployeeID;
                  Direction = $(" input[type='radio']:checked").val();
                  if (Direction == "From_Home") {
                      FromAddress = data.HomeAddress;
                      ToAddress = data.OfficeAddress;
                  }
                  else {
                      FromAddress = data.OfficeAddress;
                      ToAddress = data.HomeAddress;
                  }
                  console.log("Shchedule Adress ID---------");
                  console.log(FromAddress, ToAddress);

                  console.log("Vehicle ID :----------------------");
                  var x = $("#SelectModelName option:selected").val();
                  console.log(x);
                  VehicleNo = $("#vehiclenumber").val();
                  Color = $("#color").val();
                  date = $("#dte").val();
                  starttime = $("#StartTime").val();

                  var surl = "http://192.168.3.185:8025/Rideally.service/api/Vehicle";


                  $.getJSON(surl, function (data) {
                      console.log('---------------------------');
                      console.log(data);
                      console.log("Chosen Vehicle");
                      console.log($("#SelectModelName option:selected").val());
                      for (i = 0; i < data.length; i++) {
                          if ((data[i].ModelName).localeCompare($("#SelectModelName option:selected").val()) == 0) {
                              VehicleObj = data[i]
                              RemainingSeats = data[i].VehicleType.NoOfSeats;
                              console.log("--------------------Vehicle Obj-------------------------");
                              console.log(VehicleObj);

                          }
                      }

                      var veh = {
                          Vehicle: VehicleObj,
                          Color: Color,
                          VehicleNumber: VehicleNo,
                          EmployeeID: EmployeeID,

                      };
                      console.log(veh);
                      $.ajax(
                      {
                          url: "http://192.168.3.185:8025/Rideally.service/api/Employee/UpdateVehicle",
                          type: 'POST',
                          data: veh,
                          dataType: 'JSON',
                          success: function (data) {
                              console.log(data);
                          },
                          failure: function (msg) {
                              console.log(msg);
                          }
                      });


                      //update Schedule----------------------

                      var Sch = {
                          Employee: EmpObj,
                          date: date,
                          starttime: starttime,
                          FromAddress: FromAddress,
                          ToAddress: ToAddress,
                          ScheduleStatus: "Active",
                          RemainingSeats: RemainingSeats

                      };
                      console.log(Sch);
                      $.ajax(
                      {
                          url: "http://192.168.3.185:8025/Rideally.service/api/Schedule/InsertSchedule",
                          type: 'POST',
                          data: Sch,
                          dataType: 'JSON',
                          success: function (data) {
                              console.log(data);
                          },
                          failure: function (msg) {
                              console.log(msg);
                          }
                      });

                      return false;

                  });


              },
              failure: function (msg) {
                  console.log(msg);
              }
          });

    });
});