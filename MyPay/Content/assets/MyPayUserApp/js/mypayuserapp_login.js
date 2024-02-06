
$(document).on('click', '.toggle-password', function () {

    $(this).toggleClass("fa-eye-slash fa-eye");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

const togglePassword = document.querySelector(".passcode-switch");
const password = document.querySelector("#Password");
console.log(togglePassword)
if (togglePassword != undefined) {
    togglePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}


function showotpdiv() {
    $("#sendotp").hide();
    //$("#submitotp").show();
    $(".otp-form-group").show();
    $(".resend_otp").show();
    var counter = 120;
    setInterval(function () {
        counter--;
        if (counter >= 0) {
            span = document.getElementById("clock");
            span.innerHTML = counter;
        }
        if (counter === 0) {
            $("#sendotp").html("RESEND OTP");
            $("#sendotp").show();
            $(".resend_otp").hide();
            clearInterval(counter);
        }
    }, 1000);
}


function showstep(step) {
    switch (step) {
        case 1:
            $(".nk-wizard-content").hide();
            $("#wizard_contactnumber").show();
            break;
        case 2:
            $(".nk-wizard-content").hide();
            $("#wizard_verificationotp").show();
            break;
        case 3:
            $(".nk-wizard-content").hide();
            $("#wizard_signupdetail").show();
            break;
        case 4:
            $(".nk-wizard-content").hide();
            $("#wizard_createpassword").show();
            $("#password-field").val('');
            $("#repassword-field").val('');
            break;
        case 5:
            $(".nk-wizard-content").hide();
            $("#wizard_createpin").show();
            break;
        default:
            break;


    }
    $('#AjaxLoader').hide();

}
function btnNextStepsClick(step) {
    debugger;
    if (step == 1) {
        var ContactNumber = $("#ContactNumber").val();
        if (ContactNumber=="") {
            $("#dvMessage").html("Please Enter Phone Number");
            return false;
        }

        var IsValid = false;
        var MobileNo = ContactNumber;
        var regex1 = /^([9][8][5][0-9]{7})$/;
        var regex2 = /^([9][7][4-5][0-9]{7})$/;
        var regex3 = /^([9][8][46][0-9]{7})$/;
        var regex4 = /^([9][8][0-2][0-9]{7})$/;
        var regex5 = /^([9][6][0-9]{8}|[9][8][8][0-9]{7})$/;
        if (MobileNo.match(regex1)) {
            IsValid = true;
        }
        else if (MobileNo.match(regex2)) {
            IsValid = true;
        }
        else if (MobileNo.match(regex3)) {
            IsValid = true;
        }
        else if (MobileNo.match(regex4)) {
            IsValid = true;
        }
        else if (MobileNo.match(regex5)) {
            IsValid = true;
        }
        else if ((MobileNo.substring(0, 1) == "9") || (MobileNo.substring(0, 1) == "8") || (MobileNo.substring(0, 1) == "7")) {
            var IsRepeating = checkRepeatingDigits(MobileNo)
            if (IsRepeating) {
                IsValid = false;
            }
            else {
                IsValid = true;
            }
        }
        else {
            IsValid = false;
        }

        if (!IsValid) {
            $("#dvMessage").html("Invalid Phone Number");
            return false;
        }
        else if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUserLogin/Index",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"ContactNumber":"' + ContactNumber + '"}',
                        success: function (response) {
                            if (response == "Login") {
                                window.location.href = "/MyPayUserLogin/Login";
                                return false;
                            }
                            else if (response == "RegisterVerification") {
                                $(".nk-wizard-content").hide();
                                $(".contactno").html(ContactNumber);
                                showotpdiv();
                                $("#wizard_verificationotp").show();
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
                            }
                            else if (response != "RegisterVerification") {
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html(response);
                            }
                        },
                        failure: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        },
                        error: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        }
                    });
                    return false;
                }, 10);
        }
        else {
            $("#dvMessage").html("Please enter valid contact number");
        }
    }
    else if (step == 2) {
        var ContactNumber = $("#ContactNumber").val();
        var OTP = $("#OTP").val();
        var Type = "Register";
        if (OTP == "") {
            $("#dvMessage").html("Please enter OTP");
        }
        else if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUserLogin/RegisterVerification",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"Type":"' + Type + '","OTP":"' + OTP + '"}',
                        success: function (response) {
                           debugger;
                            var jsonData;
                            var IsValidJson = false;
                            try {
                                jsonData = $.parseJSON(response);
                                var IsValidJson = true;
                            }
                            catch (err) {

                            }
                            if (IsValidJson) {
                                if (Type == "Verification") {

                                    if (IsValidJson && jsonData.Message == "success") {
                                        debugger;
                                        window.location.href = "/MyPayUserLogin/Dashboard";
                                    }
                                    else {
                                        $("#dvMessage").html(jsonData.Message);
                                        $('#AjaxLoader').hide();
                                    }
                                }
                                else if (Type == "Register") {
                                    debugger;
                                    if (IsValidJson && jsonData.Message == "success") {
                                        debugger;
                                        $(".nk-wizard-content").hide();
                                        $(".contactno").html(ContactNumber);
                                        $("#wizard_signupdetail").show();
                                        $('#AjaxLoader').hide();
                                        $("#dvMessage").html("");
                                        if (IsValidJson) {
                                            debugger;
                                            var Name = jsonData.Name.split(' ');
                                            $("#txtfirstname").val(Name[0]);
                                            if (Name.length > 2) {
                                                $("#txtlastname").val(Name[1] + Name[2]);
                                            }
                                            else if (Name.length > 1) {
                                                $("#txtlastname").val(Name[1]);
                                            }
                                            if (jsonData.Email != "") {
                                                $("#txtemail").val(jsonData.EmailId);
                                            }
                                        }
                                    }
                                    else {
                                        $("#dvMessage").html(jsonData.Message);
                                        $('#AjaxLoader').hide();
                                    }
                                }
                                else {
                                    $("#dvMessage").html(jsonData.Message);
                                    $('#AjaxLoader').hide();
                                }
                            }
                            else {
                                $("#dvMessage").html(response);
                                $('#AjaxLoader').hide();
                            }
                        },
                        failure: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        },
                        error: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        }
                    });
                    return false;
                }, 10);
        }
        else {
            window.location.href = "/MyPayUserLogin/Index";
        }
    }
    else if (step == 3) {
        debugger;
        var ContactNumber = $("#ContactNumber").val();
        var FirstName = $("#txtfirstname").val();
        var LastName = $("#txtlastname").val();
        var Gender = $("#drpgender :selected").val();
        var Email = $("#txtemail").val();
        var AlternateNumber = $("#txtalternatenumber").val();
        var RefferalCode = $("#txtrefferalcode").val();
        var Type = "Register";
        if (FirstName == "") {
            $("#dvMessage").html("Please enter First Name");
        }
        else if (LastName == "") {
            $("#dvMessage").html("Please enter Last Name");
        }
        else if (Gender == "0") {
            $("#dvMessage").html("Please select Gender");
        }
        else if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUserLogin/SignupDetail",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"FirstName":"' + FirstName + '","LastName":"' + LastName + '","Gender":"' + Gender + '","Email":"' + Email + '","AlternateNumber":"' + AlternateNumber + '","RefferalCode":"' + RefferalCode + '"}',
                        success: function (response) {
                            debugger;
                            if (response == "success") {
                                $(".nk-wizard-content").hide();
                                $("#wizard_createpassword").show();
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
                            }
                            else {
                                $("#dvMessage").html(response);
                            }
                            $('#AjaxLoader').hide();
                        },
                        failure: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        },
                        error: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        }
                    });
                    return false;
                }, 10);
        }
        else {
            window.location.href = "/MyPayUserLogin/Index";
        }
    }
    else if (step == 4) {
        debugger;
        var ContactNumber = $("#ContactNumber").val();
        var Password = $("#password-field").val();
        var RePassword = $("#repassword-field").val();
        if (Password == "") {
            $("#dvMessage").html("Please enter password");
        }
        else if (RePassword == "") {
            $("#dvMessage").html("Please Confirm Password");
        }
        else if (Password != RePassword) {
            $("#dvMessage").html("Password and Confirm Password does not match.");
        }
        else if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUserLogin/CreatePassword",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"Password":"' + Password + '","RePassword":"' + RePassword + '"}',
                        success: function (response) {
                            debugger;
                            if (response == "success") {
                                $(".nk-wizard-content").hide();
                                $("#wizard_createpin").show();
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
                            }
                            else {
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html(response);
                            }
                        },
                        failure: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        },
                        error: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        }
                    });
                    return false;
                }, 10);
        }
        else {
            window.location.href = "/MyPayUserLogin/Index";
        }
    }
    else if (step == 5) {
        debugger;
        var ContactNumber = $("#ContactNumber").val();
        var Pin = $("#txtpin").val();
        if (Pin == "") {
            $("#dvMessage").html("Please enter Pin");
        }
        else if (Pin.length < 4) {
            $("#dvMessage").html("Pin must be 4 characters long");
        }
        else if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $(".nk-wizard-content").hide();
                    $("#wizard_verifypin").show();
                    $('#AjaxLoader').hide();
                }, 10);
        }
        else {
            window.location.href = "/MyPayUserLogin/Index";
        }
    }
    else if (step == 6) {
        debugger;
        var ContactNumber = $("#ContactNumber").val();
        var Pin = $("#txtpin").val();
        var Password = $("#password-field").val();
        var ConfirmPin = $("#txtverifypin").val();
        if (Pin == "") {
            $("#dvMessage").html("Please enter Pin");
        }
        else if (Password == "") {
            $("#dvMessage").html("Please enter Password");
        }
        else if (ConfirmPin == "") {
            $("#dvMessage").html("Please enter ConfirmPin");
        }
        else if (Pin != ConfirmPin) {
            $("#dvMessage").html("Pin does not match please type again!");
        }
        else if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUserLogin/CreatePin",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"Pin":"' + Pin + '","ConfirmPin":"' + ConfirmPin + '","Password":"' + Password + '"}',
                        success: function (response) {
                            debugger;
                            if (response == "success") {
                                window.location.href = "/MyPayUserLogin/Dashboard";
                                $("#dvMessage").html("Please wait ...");
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
                            }
                            else {
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html(response);
                            }
                        },
                        failure: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        },
                        error: function (response) {
                            $('#AjaxLoader').hide();
                            JsonOutput = (response.responseText);
                        }
                    });
                    return false;
                }, 10);
        }
        else {
            window.location.href = "/MyPayUserLogin/Index";
        }
    }
    return false;
}

function btnResendotp() {
    var ContactNumber = $("#ContactNumber").val();
    $("#OTP").val("");
    $("#dvMessage").html("");
    var Type = "Register";
    if (ContactNumber.trim() != "" && ContactNumber.length == 10) {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUserLogin/ResendOTP",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ContactNumber":"' + ContactNumber + '"}',
                    success: function (response) {
                        debugger;
                        if (response == "Login") {
                            window.location.href = "/MyPayUserLogin/Login";
                            return false;
                        }
                        else if (response == "RegisterVerification") {
                            //$(".nk-wizard-content").hide();
                            //$(".contactno").html(ContactNumber);
                            showotpdiv();
                            /*$("#wizard_verificationotp").show();*/
                            $('#AjaxLoader').hide();
                        }
                        else if (response != "RegisterVerification") {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(response);
                        }
                    },
                    failure: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response.responseText);
                    },
                    error: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response.responseText);
                    }
                });
                return false;
            }, 10);
    }
    else {
        window.location.href = "/MyPayUserLogin/Index";
    }
}

function btnFailureotp() {
    $("#OTPFailurePopup").modal('show');
}

function ValidateEnter(el, objButton, checkNumeric, event) {
    if (event.keyCode === 13) {
        $('#' + objButton)[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
    else if (checkNumeric) {
        return isNumberKey(el, event)
    }
    else {
        return true;
    }
}
function isNumberKey(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1) {
        var range = document.selection.createRange();

        if (range.text != "") {
        }
        else {
            var number = el.value.split('.');
            if (number.length == 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
}
$("#btnSubmitPin").click(function () {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var Pin = $("#PIN").val();
            $.ajax({
                type: "POST",
                url: "/MyPayUserLogin/LoginUserWithPin",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Pin":"' + Pin + '" }',
                success: function (response) {
                    debugger;
                    if (response == "success") {
                        window.location.href = "/MyPayUserLogin/Dashboard";
                        $("#dvMessage").html("Please wait ...");
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html("");
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html(response);
                    }
                },
                failure: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                }
            });
            return false;
        }, 10);
});
$(document).ready(function () {


    document.onkeydown = function (e) {
        if (event.keyCode == 123) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
            return false;
        }
        if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
            return false;
        }
    }
});

function checkRepeatingDigits(N) {

    // Find the last digit
    var digit = N % 10;

    while (N != 0) {

        // Find the current last digit
        var current_digit = N % 10;

        // Update the value of N
        N = parseInt(N / 10);

        // If there exists any distinct
        // digit, then return No
        if (current_digit != digit) {
            return false;
        }
    }

    // Otherwise, return Yes
    return true;
}