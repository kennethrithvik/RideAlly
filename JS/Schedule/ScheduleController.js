$(function () {

    
    $("#RiderDetail").hide();
    

    $("#btnFindRide").on("click", function () {

        var HomeAddressObj, OfficeAddressObj;
        var ScheduledViewTime = $("#FindRideTime").val();
        var ScheduledViewDate = $("#FindRideDate").val();
        var Direction = $(" input[type='radio']:checked").val();
        var obj;

        console.log(ScheduledViewDate);
        console.log(ScheduledViewTime);
        console.log(Direction);

        $.ajax({
            url: "http://192.168.3.185:8025/Rideally.service/api/Employee/",

            data: { id: localStorage.getItem("EmployeeID") },
            type: 'GET',
            datatype: "json",
            success: function (data) {
                console.log("Employee Obj :");
                console.log(data);
                HomeAddressObj = { Latitude: data.HomeAddress.Latitude, Longitude: data.HomeAddress.Longitude };
                OfficeAddressObj = { Latitude: data.OfficeAddress.Latitude, Longitude: data.OfficeAddress.Longitude };

                var MyLocation = new google.maps.LatLng(data.HomeAddress.Latitude, data.HomeAddress.Longitude);

                console.log(HomeAddressObj);
                console.log(OfficeAddressObj);
                obj = {
                    ScheduledTime: ScheduledViewTime,
                    ScheduledDate: ScheduledViewDate,
                    direction: Direction,
                    HomeAddressLatitude: data.HomeAddress.Latitude,
                    HomeAddressLongitude: data.HomeAddress.Longitude, 
                    OfficeAddressLatitude: data.OfficeAddress.Latitude,
                    OfficeAddressLongitude: data.OfficeAddress.Longitude
                };
                console.log(obj);
               
                $.ajax({
                    url: "http://192.168.3.185:8025/Rideally.service/api/Schedule/Retrieve",
                    data: obj,
                    type: 'POST',
                    datatype: "json",
                    success: function (data) {
                        console.log(data);
                        $("#RiderDetail tr").remove();
                        $("#RiderDetail").show();
                        var ColumnName = "<tr><th>" + "Name" + "</th><th>" + "EmailID" + "</th><th>" + "MobileNo" + "</th><th>" + "Show" + "</th></tr>";
                        $("#RiderDetail").append(ColumnName);
                        for (var i = 0; i < data.length; i++) {
                            
                            ScheduleMatch(data[i],i,MyLocation)
                        }


                    },
                    failure: function (msg) {
                        console.log(msg);
                    }

                })
            },
            failure: function (msg) {
                console.log(msg);
            }
        })



    })
    ScheduleMatch = function (data, p, MyLocation) {

        console.log("Schedule Object:");
        console.log(data);
        var FromLat = data.FromAddressLatitude;
        var FromLng = data.FromAddressLongitude;
       

        console.log("FromLat");
        console.log(FromLat);
        console.log("FromLng");
        console.log(FromLng);


        var ToLat = data.ToAddressLatitude;
        var ToLng = data.ToAddressLongitude


        console.log("ToLat");
        console.log(ToLat);
        console.log("ToLng");
        console.log(ToLng);


        var Start = new google.maps.LatLng(FromLat, FromLng);
        var End = new google.maps.LatLng(ToLat, ToLng);


        var directionsService = new google.maps.DirectionsService();
        var request = {
            origin: Start,
            destination: End,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };

        var polyline = new google.maps.Polyline({
            path: [],
            strokeColor: '#FF0000',
            strokeWeight: 3
        });


        var pointsArray = [];


        console.log("index1");
        console.log(p);
        var x;
        directionsService.route(request, function (result, status) {
            if (status == google.maps.DirectionsStatus.OK) {


                pointsArray = result.routes[0].overview_path;
                var path = result.routes[0].overview_path;
                var legs = result.routes[0].legs;
                for (i = 0; i < legs.length; i++) {
                    var steps = legs[i].steps;
                    for (j = 0; j < steps.length; j++) {
                        var nextSegment = steps[j].path;
                        for (k = 0; k < nextSegment.length; k++) {
                            polyline.getPath().push(nextSegment[k]);

                        }
                    }
                }



                var MyPolyLine = new google.maps.Polyline({ path: pointsArray });

                x = p;
                if (google.maps.geometry.poly.isLocationOnEdge(MyLocation, MyPolyLine, 0.003)) {
                    console.log(MyLocation + "Your Location is on the way");
                    console.log(x);
                    console.log(data);
                   
                    //var z = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    //var Sch = "<p>" + data.Offerer.EmployeeName + z + data.Offerer.MobileNo + z + data.Offerer.EmailID + z + data.SeatsAvailable + z + data.ScheduledTime + z + data.ScheduledStatus + "</p>";
                    //$("#Schedules").append(Sch);


                    //---------------------------
                    var row = "<tr ><td>" + data.Name + "</td><td>"
                          + data.EmailID + "</td><td>"
                          + data.PhoneNo + "</td><td>"
                          + '<a href="VehicleOwnerInfo.html?id=' + data.ScheduleID + '"><input type="button" value="Show Details" style="width:100px;color:#0BAFA0;"></a>';
                          //+ '<input type="button" value="Show Detail" style="width:100px;color:#0BAFA0;"  onclick="VeiwCarOwnerInfo(' + data.ScheduleId + ')"></button></td></tr>';
                    $("#RiderDetail").append(row);


                } else {
                    console.log(MyLocation + "Your Location is Far Away");
                    console.log(x);
                    console.log(data);

                }






            }


        });
    }
   

});



