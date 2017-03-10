$(function () {

    var Vehicle = [];

    var surl = "http://192.168.3.185:8025/Rideally.service/api/Vehicle";
    var vehicles = [];

    $.getJSON(surl, function (data) {
        Vehicle = data;
        console.log(data);


        //populating  Vehicletype Dropdown
        $.each(data, function (i, Vehicle) {
            vehicles[i] = Vehicle.VehicleType.VehicleTypeMaster.VehicleMasterType;

        })
        console.log("hi");
        console.log(vehicles);
        var unique = $.makeArray($(vehicles).filter(function (i, itm) {
            return i == vehicles.indexOf(itm);
        }));
        console.log(unique);

        var select = document.getElementById("SelectVehicleType");

        for (var i = 0; i < unique.length; i++) {
            var opt = unique[i];
            var el = document.createElement("option");
            el.textContent = opt;
            el.value = opt;
            select.appendChild(el);
        }




    });

    $("#SelectVehicleType").on("change", function () {


        $("#SelectBrandType").empty();


        console.log($("#SelectVehicleType option:selected").val());


        $brands = [];

        $.each(Vehicle, function (i, item) {
            if ($("#SelectVehicleType option:selected").val() == item.VehicleType.VehicleTypeMaster.VehicleMasterType) {
                $brands[i] = item.Brand.BrandName;
            }

        })
        var unique = $.makeArray($($brands).filter(function (i, itm) {
            return i == $brands.indexOf(itm);

        }));
        $.each(unique, function (i, item) {



            option = "<option>" + item + "</option>";
            $("#SelectBrandType").append(option);

        })







    });


    $("#SelectBrandType").on("change", function () {


        $("#SelectVehicleDesc").empty();


        console.log($("#SelectBrandType option:selected").val());


        $VehicleDesc = [];

        $.each(Vehicle, function (i, item) {
            if ($("#SelectBrandType option:selected").val() == item.Brand.BrandName) {
                $VehicleDesc[i] = item.VehicleType.VehicleTypeDesc;
            }

        })
        var unique = $.makeArray($($VehicleDesc).filter(function (i, itm) {
            return i == $VehicleDesc.indexOf(itm);

        }));
        $.each(unique, function (i, item) {



            option = "<option>" + item + "</option>";
            $("#SelectVehicleDesc").append(option);

        })







    });


    $("#SelectVehicleDesc").on("change", function () {


        $("#SelectModelName").empty();


        console.log($("#SelectVehicleDesc option:selected").val());


        $ModelNames = [];
        $Vehicle2 = [];

        $.each(Vehicle, function (i, item) {
            if ($("#SelectVehicleDesc option:selected").val() == item.VehicleType.VehicleTypeDesc) {
                $ModelNames[i] = item.ModelName;
                $Vehicle2[i] = item;
            }

        })
        var unique = $.makeArray($($ModelNames).filter(function (i, itm) {
            return i == $ModelNames.indexOf(itm);

        }));

        $.each(unique, function (i, item) {
            option = "<option>" + item + "</option>";
            $("#SelectModelName").append(option);


        });









    });

});