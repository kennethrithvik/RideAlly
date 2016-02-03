$(document).ready(function () {
    
    $("#btnSubmitVehicle").click(function () {
        var flag=true;
        var vehicleNo = $("#vehiclenumber").val();
        var color = $("#color").val();
        var VehicleType = $("#SelectVehicleType option:selected").val();
        var BrandName = $("#SelectBrandType option:selected").val();
        var VehicleDesc = $("#SelectVehicleDesc option:selected").val();
        var modelName = $("#SelectModelName option:selected").val();

        if (vehicleNo == "" )
        {
            alert("All Fields are mandatory to be filled");
            flag = false;
        }
        else if (color == "") {
            alert("All Fields are mandatory to be filled");
            flag = false;
        }
        else
        {
            var surl = "/api/Employee/AddVehicleDetail";
            var vehicle = {
                vehicleNo: vehicleNo, color: color, modelName: modelName, EmployeeId: localStorage.getItem("EmployeeID"), VehicleType: VehicleType, BrandName: BrandName, VehicleDesc: VehicleDesc
            }
            $.ajax({
                url: surl,
                type: "POST",
                datatype: 'json',
                data: vehicle,
                success: function (data) {
                    if (data == true) {
                        alert("EmployeeVehicles Added Successfully!");
                    }

                    console.log(data);
                }
            });
        }
       
       
    });

    $("#btnUpdate").click(function () {
        date = $("#dte").val();
        starttime = $("#StartTime").val();
        Direction = $(" input[type='radio']:checked").val();
        var flag = true;
        if (date == "")
        {
            alert("All Fields are mandatory to be filled");
            flag = false;
        }
        else if (starttime == "") {
            alert("All Fields are mandatory to be filled");
            flag = false;
        }
        else if (Direction == "") {
            alert("All Fields are mandatory to be filled");
            flag = false;
        }
        else
        {
            var Sch = {
                EmployeeID: localStorage.getItem("EmployeeID"),
                date: date,
                starttime: starttime,
                ScheduleStatus: "Active",
                Direction: Direction


            };
            var surl = "/api/Schedule/InsertSchedule";
            $.ajax({
                url: surl,
                type: "POST",
                datatype: 'json',
                data: Sch,
                success: function (data) {
                    alert("Schedule Added Successfully!");
                    console.log(data);
                }
            });
        }
       

    });
});