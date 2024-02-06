$(document).ready(function () {
    showotpdiv();
});
function goBack() {
    history.go(-1);
}
function btnResendOTP() {
    var ContactNumber = $("#ContactNumber").val();
    var OTP = $("#OTP").val();
    var Type = "Verification";
    if (ContactNumber.trim() != "") {
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
                        if (response == "Login" || response == "RegisterVerification") {
                            showotpdiv();
                            $('#AjaxLoader').hide();
                            return false;
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
            }, 0);
    }
    else {
        window.location.href = "/MyPayUserLogin/Index";
    }
}
function btnSubmitStepsClick() {
    debugger;
    {
        var ContactNumber = $("#ContactNumber").val();
        var OTP = $("#OTP").val();
        var Type = $("#Type").val();
        if (OTP == "") {
            $("#dvMessage").html("Please enter OTP");
        }
        else if (ContactNumber.trim() != "") {
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
                            $('#AjaxLoader').hide();
                            try {
                                jsonData = $.parseJSON(response);
                                var IsValidJson = true;
                            }
                            catch (err) {

                            }
                            if (IsValidJson) {
                                if (Type == "Verification" && jsonData.Message == "success") {
                                    window.location.href = "/MyPayUserLogin/Dashboard";
                                }
                                $("#dvMessage").html(jsonData.Message);
                            }
                            else {
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
                }, 20);
        }
        else {
            window.location.href = "/MyPayUserLogin/Index";
        }
    }
    return false;
}
$("#OTP").keypress(function (event) {
    if (event.keyCode == 13) {
        btnSubmitStepsClick();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});