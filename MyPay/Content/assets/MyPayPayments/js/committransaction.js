$("#sendotp").click(function () {
    GenerateTransactionOTP();
});

if ($("#dvMsg").html().trim() == "Invalid OTP") {
    showotpdiv();
}
if ($("#dvMsg").html().trim() == "Invalid OTP Attempt Exceeded") {
    $("#sendotp").hide();
    $("#submitotp").hide();
    $(".otp-form-group").html("");
    $(".resend_otp").html("");
}
$(document).ready(function () {
    $("#btnClose").click(function () {
        $("#hdnCancelled").val("Order Cancelled ");
        if ($("#dvMsgInsufficient").html().trim() != "") {
            $("#hdnCancelled").val("Order Cancelled Due To Insufficient Balance ");
        }
    });
});
function showotpdiv() {
    $("#sendotp").hide();
    $("#submitotp").show();
    $(".otp-form-group").show();
    $(".resend_otp").show();
    var counter = 59;
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
function GenerateTransactionOTP() {
    debugger;
    $("#AjaxLoader").show();
    setTimeout(function () {
        $("#dvMsg").html("");
        var TransactionOTP = new Object();
        TransactionOTP.OrderToken = $("#hdnOrderToken").val();
        TransactionOTP.MerchantId = $("#hdnMerchantId").val();
        TransactionOTP.Phonenumber = $("#hdnPhonenumber").val();
        if (TransactionOTP != null) {
            $.ajax({
                type: "POST",
                url: "/MyPayPayments/GenerateTransactionOTP",
                data: JSON.stringify(TransactionOTP),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    debugger;
                    if (response == "success") {

                        showotpdiv();
                    }
                    else {

                        $("#dvMsg").html(response);
                    }
                },
                failure: function (response) {
                    $("#dvMsg").html(response.responseText);
                    return false;
                },
                error: function (response) {
                    $("#dvMsg").html(response.responseText);
                    return false;
                }
            });
        }
        else {
            return false;
        }
        $("#AjaxLoader").hide();
    }, 10);
}

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