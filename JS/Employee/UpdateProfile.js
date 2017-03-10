$(function () {

    
    $.ajax({
        url: "http://192.168.3.185:8025/Rideally.service/api/Employee/",
        type: 'GET',
        data: { id: localStorage.getItem("EmployeeID") },
        dataType: 'JSON',
        success: function (data) {
            console.log(data);
            document.getElementById("UpdateEmpId").textContent = data.EmployeeID;
            document.getElementById("UpdateEmpName").value = data.EmployeeName;
            document.getElementById("UpdateEmailId").value = data.EmailID;
            document.getElementById("UpdateMobId").value = data.MobileNo;
        },
        failure: function (msg) {
            console.log(msg);
        }

    });
    $("#btnUpdate").on('click', function () {

        var flag = true;
        var Name = $("#UpdateEmpName").val();

        if (Name == "") {
            alert("Please Enter Your Name!");
            flag = false;
        }

        if (Name != "") {

            if (Name.length >= 50 || Name.length == 1) {
                alert("Name should contain 2 to 50 characters only");
                flag = false;
            }

            var NameString = /^[A-Za-z\s]+$/;

            if (!NameString.test(Name)) {

                alert("Enter a Valid Name!");
                flag = false;

            }
        }


        var Email = $("#UpdateEmailId").val();
        if (Email == "") {
            alert('Please Enter Email ID!');
            flag = false;
        }

        if (Email != "") {
            var atposition = Email.indexOf("@");
            var dotposition = Email.lastIndexOf(".");
            if (atposition < 1 || dotposition < atposition + 2 || dotposition + 2 >= Email.length) {
                alert("Please enter a valid Email address!");
                flag = false;
            }

        }

        var MobNum = $("#UpdateMobId").val();
        if (MobNum == "") {
            alert('Mobile Number is Required!');
            flag = false;
        }

        if (MobNum != "") {

            if (isNaN(MobNum)) {
                alert("Enter Numeric value only!");
                flag = false;

            }

            if (MobNum.length != 10) {
                alert("Enter Correct Mobile Number!");
                flag = false;
            }
        }


        if (flag == true) {
            employeeid = $("#UpdateEmpId").text();
            employeename = ($("#UpdateEmpName").val());
            emailid = ($("#UpdateEmailId").val());
            mobileno = ($("#UpdateMobId").val());




            employeeobj = {
                EmployeeID: employeeid,
                EmployeeName: employeename,
                EmailID: emailid,
                MobileNo: mobileno
            };
            console.log(employeeobj);

            $.ajax({
                url: "http://192.168.3.185:8025/Rideally.service/api/UpdateEmployee",
                type: 'POST',
                data: employeeobj,
                dataType: 'JSON',
                success: function (data) {
                    console.log(data);
                    alert('Profile Updated Successfully')
                },
                failure: function (msg) {
                    console.log(msg);
                }

            });
        }
    });

});

