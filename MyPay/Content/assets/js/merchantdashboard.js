 
    function bindDashboard(Type) {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                if (Type != "") {
                    $.ajax({
                        type: "POST",
                        url: "/MerchantLogin/BindDashboard",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: '{"Type":"' + Type + '"}',
                        success: function (response) {

                            if (response != null) {

                                if (Type == "totaltxn") {
                                    $("#AllTransactions").html(response.AllTransactions);
                                    $("#ThisMonthTransactions").html(response.ThisMonthTransactions);
                                    $("#ThisWeekTransactions").html(response.ThisWeekTransactions);
                                    $("#TodayAmount").html(response.TodayTransactions);
                                    return false;
                                }
                                else if (Type == "totalorders") {
                                    $("#TotalOrder").html(response.AllOrders);
                                    $("#ThisMonthOrders").html(response.ThisMonthOrders);
                                    $("#ThisWeekOrders").html(response.ThisWeekOrders);
                                    $("#TodayOrders").html(response.TodayOrders);

                                    return false;
                                }
                                else if (Type == "api_key") {
                                    $("#api_key").html(response.apikey);
                                    $("#resetkeys").show();
                                    $("#resetkeyslink").hide();


                                    $("#api_password").html("");
                                    $("#resetapipassword").hide();
                                    $("#resetapipasswordlink").show();
                                    return false;
                                }
                                else if (Type == "api_password") {
                                    $("#api_password").html(response.apipassword);
                                    $("#resetapipassword").show();
                                    $("#resetapipasswordlink").hide();

                                    $("#api_key").html("");
                                    $("#resetkeys").hide();
                                    $("#resetkeyslink").show();
                                    return false;
                                }
                                else if (Type == "todaycredittransactions") {
                                    $("#spntodaycredittransactions").html(response.TodayCreditTransactions);
                                    $("#spntodaycredittransactionsmonth").html(response.ThisMonthCreditTransactions);
                                    return false;
                                }
                                else if (Type == "todaydebittransactions") {
                                    $("#spntodaydebittransactions").html(response.TodayDebitTransactions);
                                    $("#spntodaydebittransactionsmonth").html(response.ThisMonthDebitTransactions);
                                    return false;
                                }
                                else if (Type == "walletbalance") {
                                    $("#spnwalletbalance").html(response.WalletBalance);
                                    $("#reqWithdrawlink").hide();
                                    $("#reqWithdraw").show();

                                    return false;
                                }
                                else if (Type == "userwalletbalance") {
                                    $("#spnuserwalletbalance").html(response.UserWalletBalance);
                                    $("#reqWithdrawWalletlink").hide();
                                    $("#reqWithdrawWallet").show();
                                    return false;
                                }
                                else if (Type == "userTotalbalance") {
                                    $("#spnuserTotalbalance").html(response.TotalBalance);
                                    //$("#reqWithdrawTotallink").hide();
                                    //$("#reqWithdrawTotal").show();
                                    return false;
                                }
                                else {
                                    return false;
                                }
                            }
                            else {
                                alert("Something went wrong. Please try again later.");
                                return false;
                            }
                        },
                        failure: function (response) {
                            alert(response.responseText);
                            return false;
                        },
                        error: function (response) {
                            alert(response.responseText);
                            return false;
                        }
                    });
                }
                else {
                    alert("please select type.");
                }

                $('#AjaxLoader').hide();
            }, 50);


    }

    $("#resendmerchantOTP").click(function () {
        ResendWithdrawalRequestOTP(); 
    });

    function ResendWithdrawalOTP() {

    }

    function ResetMerchantKeys(merchantid, e) {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        if (confirm('This Action Will Reset Merchant Keys. Do You Really Want To Continue ??'))
            $.ajax({
                type: "POST",
                url: "/MerchantLogin/ResetMerchantKeys",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"MerchantUniqueId":"' + merchantid + '"}',
                success: function (response) {
                    if (response != null) {
                        if (response.Id == "0") {
                            $("#dvMsg").html("Records not updated.");
                            return false;
                        }
                        else {
                            $("#dvMsgSuccess").html("successfully updated");
                            var tableId = $(this).data("table");
                            bindDashboard('api_key');
                            e.preventDefault();
                            e.stopPropagation();
                            return false;
                        }
                    }
                    else {
                        $("#dvMsg").html("Something went wrong. Please try again later.");
                        return false;
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

    function GetWithdrawalRequestOTP() {
        var amount = $("#txtAmount").val();
        var Bankid = $("#hdnBankId").val();
        var Remarks = $("#txtRemarks").val();
        $("#dvPopupMsgOTP").html("");

        if (amount == "" || amount == "0") {
            $("#dvPopupMsg").html('Please enter amount');
        }
        else if (Remarks == "") {
            $("#dvPopupMsg").html('Please enter comments');
        }
        else if (Bankid == "") {
            $("#dvPopupMsg").html('Please enter bank');
        }
        else {
            $('#AjaxLoader').show();
            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "/MerchantLogin/GenerateWithdrawalRequestOTP",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Amount":"' + amount + '","Remarks":"' + Remarks + '","Bankid":"' + Bankid + '","RequestType":"' + WithrawalRequestType + '"}',
                    success: function (response) {
                        if (response != null) {
                            if (response == "success") {
                                setTimeout(function () {
                                    $("#lblBank").html($("#txtBank").val());
                                    $("#lblAmount").html(amount);
                                    $("#lblRemarks").html(Remarks);
                                    $('#withdrawalrequest').modal('hide');
                                    $('#withdrawalrequestOTP').modal('show');
                                    $("#btnSubmitWithdrawalReq").hide();
                                    $("#btnSubmitWithdrawalReqOTP").show();
                                    ShowMerchantReSendOTP();
                                }, 100);
                            }
                            else {
                                $("#dvPopupMsg").html(response);
                            }
                        }
                        else {
                            JsonOutput = "Something went wrong. Please try again later.";
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvPopupMsg").html(JsonOutput);
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvPopupMsg").html(JsonOutput);
                    }
                });
                $('#AjaxLoader').hide();
            }, 100);
        }
    }

    function ResendWithdrawalRequestOTP() {
        var amount = $("#txtAmount").val();
        var Bankid = $("#hdnBankId").val();
        var Remarks = $("#txtRemarks").val();
        $("#dvPopupMsgOTP").html("");

        if (amount == "" || amount == "0") {
            $("#dvPopupMsg").html('Please enter amount');
        }
        else if (Remarks == "") {
            $("#dvPopupMsg").html('Please enter comments');
        }
        else if (Bankid == "") {
            $("#dvPopupMsg").html('Please enter bank');
        }
        else {
            $('#AjaxLoader').show();
            setTimeout(function () {
                $.ajax({
                    type: "POST",
                    url: "/MerchantLogin/ResendWithdrawalRequestOTP",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Amount":"' + amount + '","Remarks":"' + Remarks + '","Bankid":"' + Bankid + '","RequestType":"' + WithrawalRequestType + '"}',
                    success: function (response) {
                        if (response != null) {
                            if (response == "success") {
                                setTimeout(function () {
                                    $("#lblBank").html($("#txtBank").val());
                                    $("#lblAmount").html(amount);
                                    $("#lblRemarks").html(Remarks);
                                    $('#withdrawalrequest').modal('hide');
                                    $('#withdrawalrequestOTP').modal('show');
                                    $("#btnSubmitWithdrawalReq").hide();
                                    $("#btnSubmitWithdrawalReqOTP").show();
                                    ShowMerchantReSendOTP();
                                }, 100);
                            }
                            else {
                                $("#dvPopupMsg").html(response);
                            }
                        }
                        else {
                            JsonOutput = "Something went wrong. Please try again later.";
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvPopupMsg").html(JsonOutput);
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvPopupMsg").html(JsonOutput);
                    }
                });
                $('#AjaxLoader').hide();
            }, 100);
        }
    }




    var counter = 120;

    function ShowMerchantReSendOTP() {
        counter = 120;
        $("#resendmerchantOTP").hide();
        $("#submitotp").show();
        $(".otp-form-group").show();
        $(".resend_merchantotp").show();
        var myTimer = setInterval(function () {
            counter--;
            if (counter >= 0) {
                span = document.getElementById("clock");
                span.innerHTML = counter;
            }
            if (counter === 0) {
                $("#resendmerchantOTP").html("RESEND OTP");
                $("#resendmerchantOTP").show();
                $(".resend_merchantotp").hide();
                clearInterval(myTimer);
            }
        }, 1000);
    }
    function SubmitWithdrawalRequest() {
        var amount = $("#txtAmount").val();
        var Bankid = $("#hdnBankId").val();
        var Remarks = $("#txtRemarks").val();
        var OTP = $("#txtOTP").val();

        if (amount == "" || amount == "0") {
            $("#dvPopupMsgOTP").html('Please enter amount');
        }
        else if (Remarks == "") {
            $("#dvPopupMsgOTP").html('Please enter comments');
        }
        else if (Bankid == "") {
            $("#dvPopupMsgOTP").html('Please enter bank');
        }
        else {
            $("#btnSubmitWithdrawalReqOTP").hide();
            $('#AjaxLoader').show();
            setTimeout(function () {
             $.ajax({
                type: "POST",
                url: "/MerchantLogin/SubmitWithdrawalRequest",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                 data: '{"OTP":"' + OTP + '","Amount":"' + amount + '","Remarks":"' + Remarks + '","Bankid":"' + Bankid + '","RequestType":"' + WithrawalRequestType + '"}',
                success: function (response) {
                    if (response != null) {
                        debugger;
                        if (response == "success") {
                            $("#resend_merchantotp").html("");
                            $("#resendmerchantOTP").hide();
                            $("#dvPopupMsgOTP").css("color", "green");
                            $("#dvPopupMsgOTP").html("Your Withdrawal Completed Successfully.");
                            setTimeout(function () {
                                window.location.href = "/MerchantOrders/MerchantWithdrawals";
                            }, 5000);
                        }
                        else if (response == "submitted") {
                            $("#resend_merchantotp").html("");
                            $("#resendmerchantOTP").hide();
                            $("#dvPopupMsgOTP").css("color", "green");
                            $("#dvPopupMsgOTP").html("Withdraw request submitted successfully. <br>Please wait for MyPay Verification.  It will be updated within 12 working hours.");
                            //setTimeout(function () {
                            //    window.location.href = "/MerchantOrders/MerchantWithdrawalRequests";
                            //}, 5000);
                        }
                        else {
                            $("#dvPopupMsgOTP").css("color", "red");
                            $("#dvPopupMsgOTP").html(response);
                            $("#btnSubmitWithdrawalReqOTP").show();
                        }
                        setTimeout(function () {
                        }, 1000);
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                        $("#btnSubmitWithdrawalReqOTP").show();
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                    $("#dvPopupMsgOTP").html(JsonOutput);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                    $("#dvPopupMsgOTP").html(JsonOutput);
                }
             });
             $('#AjaxLoader').hide();
            }, 100);
        }
    }

    function ResetMerchantAPIPassword(merchantid, e) {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        if (confirm('This Action Will Reset Merchant API Password. Do You Really Want To Continue ??'))
            $.ajax({
                type: "POST",
                url: "/MerchantLogin/ResetMerchantAPIPassword",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"MerchantUniqueId":"' + merchantid + '"}',
                success: function (response) {
                    if (response != null) {
                        if (response.Id == "0") {
                            $("#dvMsg").html("Records not updated.");
                            return false;
                        }
                        else {
                            $("#dvMsgSuccess").html("successfully updated");
                            var tableId = $(this).data("table");
                            bindDashboard('api_password');
                            e.preventDefault();
                            e.stopPropagation();
                            return false;
                        }
                    }
                    else {
                        $("#dvMsg").html("Something went wrong. Please try again later.");
                        return false;
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