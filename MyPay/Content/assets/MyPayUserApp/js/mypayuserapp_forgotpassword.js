const togglePassword = document.querySelector(".passcode-switch");
const password = document.querySelector("#Password");
if (togglePassword != undefined) {
    togglePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}
const toggleRePassword = document.querySelector(".re-passcode-switch");
const rePassword = document.querySelector("#RePassword");
if (toggleRePassword != undefined) {
    toggleRePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = rePassword.getAttribute("type") === "password" ? "text" : "password";
        rePassword.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("is-shown");
    });
}

$(document).on('click', '.toggle-password', function () {

    $(this).toggleClass("fa-eye-slash fa-eye");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

 
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
}
function ForgotPasswordClick(step) {
    if (step == 1) {
        var ContactNumber = $("#ContactNumber").val();
        if (ContactNumber.trim() != "") {
            $('#AjaxLoader').show();
            setTimeout(
                function () {
                    $.ajax({
                        type: "POST",
                        url: "/MyPayUserLogin/ForgotPassword",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"ContactNumber":"' + ContactNumber + '"}',
                        success: function (response) {
                            if (response == "success") {
                                $('#AjaxLoader').hide();
                                $("#spnContact").html(ContactNumber.charAt(0) + ContactNumber.charAt(1) + "xxxxxx" + ContactNumber.charAt(8) + ContactNumber.charAt(9));
                                $("#forgotStep1").hide();
                                $("#forgotStep2").show();
                                $("#dvMessage").html("");
                                showotpdiv();
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
            $("#dvMessage").html("Please enter contact number");
        }
    }
    else if (step == 2) {
        var ContactNumber = $("#ContactNumber").val();
        var OTP = $("#OTP").val();
        var Password = $("#Password").val();
        var RePassword = $("#RePassword").val();
        if (ContactNumber == "") {
            $("#dvMessage").html("Please enter contact number");
        }
        else if (OTP == "") {
            $("#dvMessage").html("Please enter OTP");
        }
        else if (OTP.length < 6) {
            $("#dvMessage").html("Please enter 6 digit OTP");
        }
        else if (Password == "") {
            $("#dvMessage").html("Please enter Password");
        }
        else if (RePassword == "") {
            $("#dvMessage").html("Please enter Confirm Password");
        }
        else if (Password != RePassword) {
            $("#dvMessage").html("Password and Confirm Password does not match");
        }
        else {
            if (ContactNumber.trim() != "") {
                $('#AjaxLoader').show();
                setTimeout(
                    function () {
                        $.ajax({
                            type: "POST",
                            url: "/MyPayUserLogin/ResetPassword",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            data: '{"ContactNumber":"' + ContactNumber + '","VerificationCode":"' + OTP + '","Password":"' + Password + '","RePassword":"' + RePassword + '"}',
                            success: function (response) {
                                if (response != null) {

                                    if (response == "success") {
                                        window.location.href = "/MyPayUserLogin/Login";
                                    }
                                    else {
                                        $("#dvMessage").html(response);
                                        $('#AjaxLoader').hide();
                                    }
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
    }
    return false;
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
function btnResendCode() {
    var ContactNumber = $("#ContactNumber").val();
    var OTP = $("#OTP").val();
    var Type = "Register";
    if (ContactNumber.trim() != "") {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUserLogin/ForgotPassword",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ContactNumber":"' + ContactNumber + '"}',
                    success: function (response) {
                        if (response == "success") {
                            $('#AjaxLoader').hide();
                            showotpdiv();
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

function btnFailureotp() {
    $("#OTPFailurePopup").modal('show');
}

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