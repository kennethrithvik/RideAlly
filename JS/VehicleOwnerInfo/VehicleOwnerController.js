window.onload = function () {

    function GetParameterValues(param) {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }


    var Toemployee = null;
    var queryString = GetParameterValues('id');
    var sid = queryString;
    var obj = { sid: sid };

    console.log("queryString");
    console.log(queryString);
    var surl = "http://192.168.3.185:8025/Rideally.service/api/schedule/GetSEByID/" + queryString;
    var scheduleId = null;
   
    $.ajax({
        type: "POST",

        url: "http://192.168.3.185:8025/Rideally.service/api/schedule/GetSEByID",
        datatype: "json",
        data: obj,

        success: function (data) {
            console.log(data);
            scheduleId = data.schedule.ScheduleId;
            Toemployee = data.employee.EmployeeID;
            $("#carownername").text(data.employee.EmployeeName);
            $("#carownergender").text(data.employee.Gender);
            $("#carownerMobileNo").text(data.employee.MobileNo);
            $date = data.schedule.ScheduledDate;
            var mainParts = $date.split('T');
            $("#carownerscheduledate").text(mainParts[0]);
            $("#carownerscheduletime").text(data.schedule.ScheduledTime);
            $("#carownervehiclebrand").text(data.employee.Vehicle.Brand.BrandName);
            $("#carownervehicletype").text(data.employee.Vehicle.Brand.VType.VehicleMasterType);
            $("#carownervehiclemodel").text(data.employee.Vehicle.ModelName);
            $("#carownervehicleseat").text(data.schedule.SeatsAvailable);
            // console.log(data);

            //console.log($Toemployee);
            markers = [];
            var obj =
                   {
                       lat: data.FromAddress.Latitude,
                       lng: data.FromAddress.Longitude,
                       title: "Start Point"
                   }
            markers.push(obj);
            var obj =
                   {
                       lat: data.ToAddress.Latitude,
                       lng: data.ToAddress.Longitude,
                       title: "End Point"

                   }
            markers.push(obj);

            //console.log(markers);
            //$.each(data, function (i, item) {

            //    var obj =
            //        {
            //            lat: item.Latitude,
            //            lng: item.Longitude
            //        }

            //    markers.push(obj);

            //});

            //function pinSymbol(color) {
            //    return {
            //        path: 'M31.5,0C14.1,0,0,14,0,31.2C0,53.1,31.5,80,31.5,80S63,52.3,63,31.2C63,14,48.9,0,31.5,0z M31.5,52.3 c-11.8,0-21.4-9.5-21.4-21.2c0-11.7,9.6-21.2,21.4-21.2s21.4,9.5,21.4,21.2C52.9,42.8,43.3,52.3,31.5,52.3z',
            //        fillColor: color,
            //        fillOpacity: 1,
            //        strokeColor: '#000',
            //        strokeWeight: 0,
            //        scale: 1,
            //    };
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 10,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };


            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var infoWindow;
            var lat_lng = new Array();
            var latlngbounds = new google.maps.LatLngBounds();
            var geocoder = new google.maps.Geocoder;

            for (i = 0; i < 2; i++) {
                var data1 = markers[i]
                var myLatlng = new google.maps.LatLng(markers[i].lat, markers[i].lng);
                lat_lng.push(myLatlng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data1.title,
                    //icon: pinSymbol("#fff")
                });
                latlngbounds.extend(marker.position);

                infoWindow = new google.maps.InfoWindow();
                geocodeLatLng(geocoder, infoWindow, marker, map);

                (function (marker, data1) {
                    //console.log(data1);
                    //console.log(marker)

                    google.maps.event.addListener(marker, "click", function (e) {

                        infoWindow.setContent(data1.title)
                        //marker.setIcon(gicons[marker.icon]);
                        infoWindow.open(map, marker);

                    });
                    //google.maps.event.addListener(marker, "mouseover", function () {
                    //    marker.setIcon(gicons["yellow"]);
                    //});
                    //google.maps.event.addListener(marker, "mouseout", function () {
                    //    marker.setIcon(gicons["blue"]);
                    //});
                    //google.maps.event.addDomListener(marker, 'load', function (e) {
                    //    marker.setIcon(gicons["blue"]);

                    //});
                })
                    (marker, data1);
            }
            map.setCenter(latlngbounds.getCenter());
            map.fitBounds(latlngbounds);

            //***********Reverse Geocoder****************//
            function geocodeLatLng(geocoder1, infowindow1, marker1, map1) {
                var ll = { lat: marker1.position.lat(), lng: marker1.position.lng() };
                geocoder1.geocode({ 'location': ll }, function (results, status) {

                    if (status === google.maps.GeocoderStatus.OK) {
                        if (results[1]) {
                            //console.log(results[1].formatted_address);
                            ret = results[1].formatted_address;

                        }
                        else {
                            ret = "No Description found";
                        }
                    }
                    else {
                        ret = "No Description found";
                    }
                    var content_str = "<div style='width:100px;'>" + ret + "</div>";
                    infowindow1.setContent(content_str);
                    infowindow1.open(map1, marker1);
                });
            }

            //***********ROUTING****************//

            //Intialize the Path Array
            var path = new google.maps.MVCArray();

            //Intialize the Direction Service
            var service = new google.maps.DirectionsService();

            //Set the Path Stroke Color
            var poly = new google.maps.Polyline({ map: map, strokeColor: '#4986E7' });

            //  var Source = [12.967259, 77.722570, 12.968692, 77.726141, 12.986488, 77.730895];//office location

            //Loop and Draw Path Route between the Points on MAP
            for (var i = 0; i < lat_lng.length; i++) {
                if ((i + 1) < lat_lng.length) {
                    var src = lat_lng[i];
                    var des = lat_lng[i + 1];
                    path.push(src);
                    poly.setPath(path);
                    service.route({
                        origin: src,
                        destination: des,
                        travelMode: google.maps.DirectionsTravelMode.DRIVING
                    }, function (result, status) {
                        if (status == google.maps.DirectionsStatus.OK) {
                            for (var i = 0, len = result.routes[0].overview_path.length; i < len; i++) {
                                path.push(result.routes[0].overview_path[i]);
                            }
                        }
                    });
                }
            }
            //console.log(markers);

        }
    });

    $("#reqforcarownerbtn").on('click', function () {

        var currentdate = new Date();
        var date = currentdate.getDate() + "/" + (currentdate.getMonth() + 1)
                        + "/" + currentdate.getFullYear();
        var time = currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds();
        note = {

            ToEmployeeId: Toemployee,
            FromEmployeeId: localStorage.getItem("EmployeeID"),//logedin employeeID
            Status: "New",
            MessageTime: time,
            MessageDate: date,
            Message: "Can I Ride With You",
            ScheduleId: scheduleId
        }
        console.log(note);
        console.log("hi");

        $.ajax({
            url: "http://192.168.3.185:8025/Rideally.service/api/Notification",
            type: 'POST',
            data: note,
            dataType: 'JSON',
            success: function (data) {
                console.log(data);


            },
            failure: function (msg) {
                console.log(msg);
            }
        });


        alert("Request has been sent");
    });

    $("#cancelcarownerbtn").on('click', function () {

        window.location = "http://192.168.3.185:8025/Rideallyfinalui/View/SeekRide.html";//redirect to the list of carowners
    });
};