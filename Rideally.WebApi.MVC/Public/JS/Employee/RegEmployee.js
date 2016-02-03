var offlocs;

$(document).ready(function () {
    offlocs = [
           { Descp: "Tesco Main Campus", lat: "12.974511", lng: "77.727327" },
           { Descp: "Tesco DTP Campus", lat: "12.966948", lng: "77.722772" },
           { Descp: "Tesco NRE Campus", lat: "12.986220", lng: "77.731004" }
    ]
    //var offs = $("#officeadd");
    $.each(offlocs, function (i, item) {
        $("#Regofficeadd").append("<option value=" + i + ">" + item.Descp + "</option>");
    })

    $("#regmsgs").hide();
    $("#regmsgf").hide();
    $("#regsub").click(RegSubFunc);
});


    var RegSubFunc=function()
    {
        if (!marker) {
            $("#regmsgf").text("Set your Home Location");
            $("#regmsgf").show();
            return false;
        }      

        if ($("#Regofficeadd").val() === "") {

            $("#regmsgf").text("Select your Office location");
            $("#regmsgf").show();
            return false;
        }

        var RegEmp = {
            EmployeeName: $("#RegEmployeeName").val(),
            Gender: $("#RegGender").val(),
            MobileNo: $("#RegMob").val(),
            EmailID: $("#RegEmail").val(),
            HomeLatitude: marker.position.lat(),
            HomeLongitude: marker.position.lng(),
            OfficeLatitude: offlocs[$("#Regofficeadd").val()].lat,
            OfficeLongitude: offlocs[$("#Regofficeadd").val()].lng
        };
        //console.log(RegEmp);

        
        if (RegEmp.EmployeeName === "") {

            $("#regmsgf").text("Employee name cannot be null");
            $("#regmsgf").show();
            return false;
        }
        var pat=new RegExp(/[0-9]/g);
        if (pat.test(RegEmp.EmployeeName)) {

            $("#regmsgf").text("Employee name is not valid");
            $("#regmsgf").show();
            return false;
        }

        if (RegEmp.EmailID === "") {

            $("#regmsgf").text("EmailID cannot be null");
            $("#regmsgf").show();
            return false;
        }

        var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);

        if (!pattern.test(RegEmp.EmailID)) {

            $("#regmsgf").text("Email is invalid");
            $("#regmsgf").show();
            return false;
        }
        if (RegEmp.MobileNo === "") {

            $("#regmsgf").text("mobile number is required");
            $("#regmsgf").show();
            return false;
        }
        if (RegEmp.MobileNo.length< 10 || RegEmp.MobileNo.length>13) {

            $("#regmsgf").text("mobile number is invalid");
            $("#regmsgf").show();
            return false;
        }
        if (RegEmp.Gender === "") {

            $("#regmsgf").text("Select your gender");
            $("#regmsgf").show();
            return false;
        }
        //console.log(RegEmp);
        $.ajax({
            type: "POST",
            
            url: "/api/Employee",
            datatype: "json",
            data: RegEmp,

            success: function (data) {
                //data = JSON.parse(data1);
                //console.log(data);
                if (data === "True"||data==="true") {
                    $("#regmsgs").text("Registered Successfully");
                    $("#regmsgf").hide();
                    $("#regmsgs").show();
                }
                else {
                    $("#regmsgf").text("Error "+data);
                    $("#regmsgf").show();
                }

            },

            error: function (er) {
                console.log(er);
                
                $("#regmsgf").text("Error in Adding");
                $("#regmsgf").show();
            }

        });
        //return false;
    }



    var map;
    var marker;
    var infowindow;
    var geocoder;
    function initMap() {
        var myLatlng;
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        }

        function showPosition(position) {
            myLatlng = { lat: position.coords.latitude, lng: position.coords.longitude };
        }
        if (myLatlng == undefined) {
            myLatlng = { lat: 12.974086, lng: 77.728652 };
        }

        map = new google.maps.Map(document.getElementById('map'), {
            center: myLatlng,       //{lat: 12.974086, lng: 77.728652};
            zoom: 18,
            mapTypeId: google.maps.MapTypeId.ROUTEMAP
        });

        
        function placeMarker(latLng, map) {
            if (marker) {
                // Marker already created - Move it
                marker.setPosition(latLng);
            }
            else {
                // Marker does not exist - Create it
                marker = new google.maps.Marker({
                    position: latLng,
                    map: map,
                    title: 'Home Location'
                });
            }
            // info-window STARTS
            geocoder = new google.maps.Geocoder;
            var tmpll = { lat: latLng.lat(), lng: latLng.lng() };
            if (!infowindow) {
                infowindow = new google.maps.InfoWindow();
            }
            geocodeLatLng(geocoder, tmpll,infowindow);
            infowindow.open(map, marker);
            // info-window ENDS 
            map.panTo(latLng);
        }

        

        map.addListener('click', function (e) {
            var coord = { lat: e.latLng.lat(), lon: e.latLng.lng() };
            //console.log(coord);
            placeMarker(e.latLng, map);
        });

        // Create the search box and link it to the UI element.
        var input = document.getElementById('pac-input');
        var searchBox = new google.maps.places.SearchBox(input);
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

        // Bias the SearchBox results towards current map's viewport.
        map.addListener('bounds_changed', function () {
            searchBox.setBounds(map.getBounds());
        });
        // [START region_getplaces]
        // Listen for the event fired when the user selects a prediction and retrieve
        // more details for that place.
        searchBox.addListener('places_changed', function () {
            //var places = searchBox.getPlaces();
            geocoder = new google.maps.Geocoder();
            var address = input.value;
            geocoder.geocode({ 'address': address }, function (results, status) {

                if (status == google.maps.GeocoderStatus.OK) {
                    var center = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
                    //myLatlng = {lat: results[0].geometry.location.lat(), lon:results[0].geometry.location.lng()};
                    //map.setCenter(center);
                    map.panTo(center);
                }

                else {
                    alert("Geocode was not successful for the following reason: " + status);
                }
            });

        });

    }

    function geocodeLatLng(geocoder1, ll, infowindow1) {
        geocoder1.geocode({ 'location': ll }, function (results, status) {
            
            if (status === google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    //console.log(results[1].formatted_address);
                    ret= results[1].formatted_address;

                }
                else {
                    ret= "No Description found";
                }
            }
            else {
                ret= "No Description found";
            }
            var content_str = "<img src='assets/img/home-icon.ico' style='width:40px;height:40px;'/>Home Location<br/>";
            infowindow1.setContent(content_str + ret);
        });
    }