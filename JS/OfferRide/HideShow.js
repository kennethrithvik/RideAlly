

$(document).ready(function () {
    $('.group').hide();
    $('.datepicker').hide();
    $('.VehiclePicker').hide();

    $('#selectMe').change(function () {
        $('.group').hide();
        $('#' + $(this).val()).show();
    })


    $('#SelectTripType').change(function () {
        $('.datepicker').hide();
        $('#' + $(this).val()).show();
    })

    $('#SelectVehicleType').change(function () {
        $('.VehiclePicker').hide();
        $('#' + $(this).val()).show();
    })
});

