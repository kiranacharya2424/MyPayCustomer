var WalletResponse = '';
var discount = 0;
var payable = 0;
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvMessage").html("");
            $("html, body").animate({ scrollTop: "0" });
            InternetLoad();
        }, 10);
    $("#DivWallet").trigger("click");
});
function InternetLoad() {
    $('#AjaxLoader').show();
    $("#DivInternet").show();
    $("#DivProceedToPay").hide();
    $("#DivPin").hide();
    $('#AjaxLoader').hide();
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    $("html, body").animate({ scrollTop: "0" });
}

function showInternetDetails(objID, providername) {

    var ServiceId = objID;
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserCheckServiceDown",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"ServiceId":"' + ServiceId + '"}',
        success: function (response) {
            if (response == "ServiceDown") {
                window.location.href = "/MyPayUser/MyPayUserServiceDown";
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserCheckServiceDown",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceId":"' + ServiceId + '"}',
                    success: function (response) {

                        if (response == "ServiceDown") {
                            window.location.href = "/MyPayUser/MyPayUserServiceDown";
                        }
                        else {
                            var str = "";
                            $("#dvInput").html("");
                            $("html, body").animate({ scrollTop: "0" });
                            if (objID == "28") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">Landline Number</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="8" autocomplete="off" placeholder="Landline Number" onkeypress="return isNumberKey(this, event);">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';

                                str += '<div class="form-group">';
                                str += '<div class="custom-control  custom-radio">';
                                str += '<input type="radio" id="Unlimited" name="packtype" value="Unlimited" class="custom-control-input" ><label class="custom-control-label" for="Unlimited">Unlimited</label>';
                                str += '</div>';
                                str += '<div class="custom-control custom-radio ml-3">  ';
                                str += '<input type="radio" id="VolumeBased" name="packtype" value="VolumeBased" class="custom-control-input" ><label class="custom-control-label" for="VolumeBased">Volume Based</label>';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                                //'<div class="form-group"><div class="">    <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">    <label class="custom-control-label" for="customRadio1">Radio</label></div><div class="custom-control custom-radio ml-3">    <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">    <label class="custom-control-label" for="customRadio2">Radio</label></div></div>'
                                str += '<div class="form-group">';
                                str += '<label class="form-label text-soft">Amount</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Amount" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Amount must be in between Rs. 10 to Rs. 10001" onkeypress="return isNumberKey(this, event);">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            else if (objID == "29") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">User Name</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="UserName" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="User Name">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';

                                //str += '<div class="form-group">';
                                //str += '<label class="form-label text-soft">Mobile Number</label>';
                                //str += '<div class="form-control-wrap">';
                                //str += '<div class="input-group">';
                                //str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Mobile Number" onkeypress="return isNumberKey(this, event);">';
                                //str += '</div>';
                                //str += '</div>';
                                //str += '</div>';

                                //str += '<div class="form-group">';
                                //str += '<label class="form-label text-soft">Amount</label>';
                                //str += '<div class="form-control-wrap">';
                                //str += '<div class="input-group">';
                                //str += '<input type="text" id="Amount" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Amount must be in between Rs. 400 to Rs. 30000" onkeypress="return isNumberKey(this, event);">';
                                //str += '</div>';
                                //str += '</div>';
                                //str += '</div>';
                            }
                            else if (objID == "31" || objID == "39") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">Customer Id</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="Customer Id" onkeypress="return isNumberKey(this, event);">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            else if (objID == "32" || objID == "35" || objID == "70") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">User Name</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="UserName" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Username">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            else if (objID == "36" || objID == "37" || objID == "38") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">User Name</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="UserName" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="User Name">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';

                                str += '<div class="form-group">';
                                str += '<label class="form-label text-soft">Amount</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Amount" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Amount" onkeypress="return isNumberKey(this, event);">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            else if (objID == "40") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">Customer Id</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="Customer Id">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            else if (objID == "41") {
                                str = '<div class="form-group">';
                                str += '<label class="form-label text-soft">User Name</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="UserName" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="User Name">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';

                                str += '<div class="form-group">';
                                str += '<label class="form-label text-soft">Mobile Number</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Mobile Number" onkeypress="return isNumberKey(this, event);">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';

                                str += '<div class="form-group">';
                                str += '<label class="form-label text-soft">Address</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Address" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="Address">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';

                                str += '<div class="form-group">';
                                str += '<label class="form-label text-soft">Amount</label>';
                                str += '<div class="form-control-wrap">';
                                str += '<div class="input-group">';
                                str += '<input type="text" id="Amount" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Amount must be in between Rs. 100 to Rs. 50000" onkeypress="return isNumberKey(this, event);">';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            else if (objID == "66") {
                                $('#AjaxLoader').show();
                                setTimeout(
                                    function () {
                                        $("#dvMessage").html("");
                                        $("html, body").animate({ scrollTop: "0" });
                                        $.ajax({
                                            type: "POST",
                                            url: "/MyPayUser/MyPayUserInternetDetails",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            data: '{"ServiceId":"' + parseInt(objID) + '","CustomerId":"","UserName":""}',
                                            async: false,
                                            success: function (response) {

                                                var jsonData;
                                                var IsValidJson = false;
                                                try {
                                                    jsonData = $.parseJSON(response);
                                                    var IsValidJson = true;
                                                }
                                                catch (err) {

                                                }
                                                var arr = jsonData;
                                                if (IsValidJson) {

                                                    if (arr['Message'] == "success") {
                                                        if (jsonData != null) {
                                                            str = '<div class="form-group">';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<select id="ddlPackage" class="form-control form-control-lg">';
                                                            str += '<option value="0">Select Package</option>';
                                                            for (var a = 0; a < jsonData.detail.packages.length; ++a) {
                                                                str += '<option value="' + jsonData.detail.packages[a].package_name + '">' + jsonData.detail.packages[a].package_name + '&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.detail.packages[a].amount + '</option>';
                                                            }
                                                            str += '</select>';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">User Name</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="UserName" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="User Name">';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">Contact Number</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Contact Number" onkeypress="return isNumberKey(this, event);">';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">Full Name</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="FullName" class="form-control form-control-lg fw-medium" maxlength="20" autocomplete="off" placeholder="Full Name">';
                                                            str += '<input type="text" id="Amount" style="display:none;" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" >';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';
                                                        }
                                                        $("#dvInput").append(str);
                                                        $('#AjaxLoader').hide();
                                                        return false;
                                                    }
                                                    else {
                                                        $('#AjaxLoader').hide();
                                                        $("#dvMessage").html(arr['Message']);
                                                    }
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
                                    }, 20);
                            }
                            else if (objID == "67") {
                                $('#AjaxLoader').show();
                                setTimeout(
                                    function () {
                                        $("#dvMessage").html("");
                                        $("html, body").animate({ scrollTop: "0" });
                                        $.ajax({
                                            type: "POST",
                                            url: "/MyPayUser/MyPayUserInternetDetails",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            data: '{"ServiceId":"' + parseInt(objID) + '","CustomerId":"","UserName":""}',
                                            async: false,
                                            success: function (response) {

                                                var jsonData;
                                                var IsValidJson = false;
                                                try {
                                                    jsonData = $.parseJSON(response);
                                                    var IsValidJson = true;
                                                }
                                                catch (err) {

                                                }
                                                var arr = jsonData;
                                                if (IsValidJson) {

                                                    if (arr['Message'] == "success") {
                                                        if (jsonData != null) {
                                                            str = '<div class="form-group">';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<select id="ddlPackage" class="form-control form-control-lg">';
                                                            str += '<option value="0">Select Package</option>';
                                                            for (var a = 0; a < jsonData.Jagritidetail.packages.length; ++a) {
                                                                str += '<option value="' + jsonData.Jagritidetail.packages[a].package_name + '">' + jsonData.Jagritidetail.packages[a].package_name + '&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Jagritidetail.packages[a].amount + '</option>';
                                                            }
                                                            str += '</select>';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">CAS ID/ STD/ ID</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="Casid" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="CAS ID/ STD/ ID" onkeypress="return isNumberKey(this, event);">';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">Customer Id</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="CustomerId" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Customer Id" onkeypress="return isNumberKey(this, event);">';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">Customer Name</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="CustomerName" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Customer Name">';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">Old ward no.</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="oldwardno" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Old ward no." onkeypress="return isNumberKey(this, event);">';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';

                                                            str += '<div class="form-group">';
                                                            str += '<label class="form-label text-soft">Mobile Number</label>';
                                                            str += '<div class="form-control-wrap">';
                                                            str += '<div class="input-group">';
                                                            str += '<input type="text" id="Number" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Mobile Number" onkeypress="return isNumberKey(this, event);">';
                                                            str += '<input type="text" id="Amount" style="display:none;" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" >';
                                                            str += '</div>';
                                                            str += '</div>';
                                                            str += '</div>';
                                                        }
                                                        $("#dvInput").append(str);
                                                        $('#AjaxLoader').hide();
                                                        return false;
                                                    }
                                                    else {
                                                        $('#AjaxLoader').hide();
                                                        $("#dvMessage").html(arr['Message']);
                                                    }
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
                                    }, 20);
                            }

                            $("#dvInput").append(str);

                            $("#DivInternetStep2").show();
                            $("#DivInternetDetail").hide();
                            $("#DivInternet").hide();
                            $("#DivProceedToPay").hide();
                            $("#hdnServiceID").val(objID);
                            $("#hdHeading").html(providername);
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

}


$("#btnDetailBack").click(function () {
    $("html, body").animate({ scrollTop: "0" });
    $("#DivInternetStep2").show();
    $("#DivInternet").hide();
    $("#DivInternetDetail").hide();
    $("#DivProceedToPay").hide();
});

$("#btnPayBack").click(function () {
    $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
    $("#DivInternetStep2").hide();
    $("#DivInternet").show();
    $("#DivProceedToPay").hide();
    $("#DivInternetDetail").hide();
});

$("#btnPay").click(function () {

    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = $("#Number").val();
    if (Number == "") {
        $("#dvMessage").html("Please enter Number");
        return false;
    }
    if (ServiceID === "31" || ServiceID === "40") {
        var selectedId = $("#ddlPackage :selected").val();
        if (selectedId === "0") {
            $("#dvMessage").html("Please select Package");
            return;
        }
    }

    $("#DivInternet").hide();
    $("#DivInternetStep2").hide();
    $("#DivInternetDetail").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    $("#lblPayAmount").text($("#hdnAmount").val());

    $("#DivProceedToPay").show();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
    $('#DivWallet')[0].click();
});

$("#btnProceedToPay").click(function () {
    var PaymentMode = $("#hfPaymentMode").val();
    if (PaymentMode == "0" || PaymentMode == "") {
        $("#dvMessage").html("Please select payment option");
        return false;
    }
    else if (PaymentMode == "1" && parseFloat($("#lblAmount").html()) > parseFloat($("#spnWalletDashboard").html())) {
        $("#txnMsg").html("Insufficient Balance");
        $("#DivErrMessage").modal("show");
    }
    else {
        ServiceCharge();

        var ServiceId = $("#hdnServiceID").val();
        $('#PaymentPopup').modal('show');
        if (ServiceId == "32" || ServiceId == "66" || ServiceId == "67") {
            $("#MobilePopup").closest("tr").hide();
            $("#PackagePopup").text($("#hdnPackage").val());
            $("#PackagePopup").closest("tr").show();
            $("#CouponDiscountPopup").text(discount);
        }
        if (ServiceId == "35" || ServiceId == "36" || ServiceId == "37" || ServiceId == "38") {
            $("#MobilePopup").closest("tr").hide();
            $("#UsernamePopup").text($("#UserName").val());
            $("#UsernamePopup").closest("tr").show();
            $("#CouponDiscountPopup").text(discount);
        }
        if (ServiceId == "39" || ServiceId == "40" || ServiceId == "28" || ServiceId == "31") {
            $("#MobilePopup").closest("tr").hide();
            $("#CustomerIdPopup").text($("#Number").val());
            $("#CustomerIdPopup").closest("tr").show();
            $("#CouponDiscountPopup").text(discount);
        }
        if (ServiceId == "29" || ServiceId == "41" || ServiceId == "66" || ServiceId == "70" || ServiceId == "32") {
            $("#MobilePopup").closest("tr").hide();
            $("#PackagePopup").closest("tr").hide();
            $("#UsernamePopup").closest("tr").show();
            $("#UsernamePopup").text($("#UserName").val());
            $("#CouponDiscountPopup").text(discount);
        }
        if (ServiceId == "67") {
            $("#PackagePopup").closest("tr").hide();
            $("#CasidPopup").closest("tr").show();
            $("#CasidPopup").text($("#Casid").val());
            $("#CouponDiscountPopup").text(discount);
        }

        $("#dvMessage").html("");
        $("#txnMsg").html("");
    }
});
$("#btnOkPopup").click(function () {
    $('#PaymentPopup').modal('hide');
    $("#DivPin").modal("show");
    $("#Pin").val("");
});
$("#btnPin").click(function () {
    Pay();
});

function ServiceCharge() {

    var ServiceId = $("#hdnServiceID").val();
    var Amount = $("#hdnAmount").val();
    var PaymentMode = $("#hfPaymentMode").val();

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ServiceCharge",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + Amount + '","ServiceId":"' + parseInt(ServiceId) + '"}',
                success: function (response) {

                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                    }
                    catch (err) {

                    }
                    var arr = jsonData;
                    if (arr['Message'] == "success") {

                        $("#MobilePopup").text($("#Number").val());
                        //$("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
                        $("#lblAmount").html(arr['Amount']);

                        var smartPayCoin = parseFloat($("#smartpayCoins").text());

                        $('#DivCoin').show();
                        if (smartPayCoin <= 0) {
                            $('#DivCoin').hide();
                        }


                        $("#TxnAmountPopup").text(arr['Amount']);
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#MypayCoinCreditedPopup").text(arr['RewardPoints']);
                        $("#MypayCoinDebitedPopup").closest('tr').hide();
                        var MPCoinsDebit = parseFloat(arr['MPCoinsDebit']);
                        if (PaymentMode == "4") {
                            $("#AmountPopup").text(arr['WalletAmountDeduct']);
                            if (MPCoinsDebit > 0) {
                                $("#MypayCoinDebitedPopup").closest('tr').show();
                            }
                            var DeductAmount = parseFloat(arr['WalletAmountDeduct']);
                            var DeductServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = (DeductAmount + DeductServiceCharge) - discount;
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));
                        }
                        else {
                            $("#AmountPopup").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = (Amount + ServiceCharge) - discount;
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));

                        }

                        var MPCoinsCredit = parseFloat(arr['RewardPoints']);

                        if (MPCoinsCredit > 0) {
                            $("#MypayCoinCreditedPopup").closest('tr').show();
                        }

                        CheckCoinBalance(0);

                        $('#AjaxLoader').hide();
                        return false;
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html("Invalid Credentials");
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
        }, 20);
}

function Pay() {

    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $('#hdncouponcode').val();
    var PlanName = $("#hdnPackage").val();
    

    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }

    if (ServiceID == "28") {
        if ($("input[type='radio'].form-control").is(':checked')) {
            var volumebased = $("input[type='radio'].form-control:checked").val();
            //alert(volumebased);
        }
    }
    var UserName = "";
    var Number = "";
    var Amount = "";
    var Address = "";
    var CustomerName = "";
    var Oldward = "";
    var Customerid = "";
    var Casid = "";
    var PlanType = "";
    Amount = $("#hdnAmount").val();
    if (ServiceID == "28") {
        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else if (Amount < 10 || Amount > 10001) {
            $("#dvMessage").html("Amount must be in between Rs. 10 to Rs. 10001");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Landline Number");
            return false;
        }
    }
    else if (ServiceID == "29") {
        //Amount = $("#Amount").val();
        //if (Amount == "") {
        //    $("#dvMessage").html("Please enter Amount");
        //    return false;
        //}
        //else if (Amount < 400 || Amount > 30000) {
        //    $("#dvMessage").html("Amount must be in between Rs. 400 to Rs. 30000");
        //    return false;
        //}
        
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Number");
            return false;
        }

        PlanType = $("#hdnPlanType").val();
        //if (PlanType == "") {
        //    $("#dvMessage").html("Please enter Plan Type");
        //    return false;
        //}

    }

    if (ServiceID == "31" || ServiceID == "40") {
        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Customer Id");
            return false;
        }
    }

    if (ServiceID == "32" || ServiceID == "35" || ServiceID == "36" || ServiceID == "37" || ServiceID == "38") {
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }
    }
    if (ServiceID == "41") {
        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }
        Address = $("#Address").val();
        if (Address == "") {
            $("#dvMessage").html("Please enter Address");
            return false;
        }
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }
    }

    if (ServiceID == "66") {
        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }
        CustomerName = $("#FullName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter Full Name");
            return false;
        }
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }
    }

    if (ServiceID == "67") {
        Package = $("#ddlPackage :selected").val();
        if (Package == "" || Package == "0") {
            $("#dvMessage").html("Please select Package");
            return false;
        }

        Casid = $("#Casid").val();
        if (Casid == "") {
            $("#dvMessage").html("Please enter CAS ID/ STD/ ID");
            return false;
        }

        CustomerName = $("#CustomerName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter Customer Name");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Contact Number");
            return false;
        }
        Oldward = $("#oldwardno").val();
        if (Oldward == "") {
            $("#dvMessage").html("Please enter Old ward no.");
            return false;
        }
        Customerid = $("#CustomerId").val();
        if (Customerid == "") {
            $("#dvMessage").html("Please enter Customer Id");
            return false;
        }
    }
    var Mpin = $("#Pin").val();
    if (Mpin == "") {
        $("#dvMessage").html("Please enter Pin");
        return false;
    }
    var PaymentMode = $("#hfPaymentMode").val();
    if (PaymentMode == "0") {
        $("#dvMessage").html("Please select Payment Mode");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserInternet",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","ReferenceNo":"' + $("#hdnReferenceNo").val() + '","Number":"' + Number + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","volumebased":"' + volumebased + '","UserName":"' + UserName + '","SessionId":"' + $("#hdnAccount").val() + '","PaymentId":"' + $("#hdnPaymentId").val() + '","Package":"' + $("#hdnPackage").val() + '","Month":"' + $("#hdnDuration").val() + '","Token":"' + $("#hdnToken").val() + '","Address":"' + Address + '","CustomerName":"' + CustomerName + '","CustomerId":"' + Customerid + '","CasId":"' + Casid + '","Oldwardno":"' + Oldward + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '","STB":"' + $("#hdnSTB").val() + '","PlanType":"' + $("#hdnPlanType").val() + '"}',
                    success: function (response) {

                        var jsonData;
                        var IsValidJson = false;
                        try {
                            jsonData = $.parseJSON(response);
                            var IsValidJson = true;
                        }
                        catch (err) {

                        }

                        if (IsValidJson) {
                            if (jsonData.Message.toLowerCase() == "success") {
                                if (jsonData.IsCouponUnlocked == true) {
                                    $("#ScratchCardwonpopup").modal("show");

                                }
                                $("#DivPin").modal("hide");
                                $("#PaymentSuccess").modal("show");
                                $("#dvMessage").html("Success");
                                $('#AjaxLoader').hide();
                                setTimeout(function () {
                                    $("#ScratchCardwonpopup").modal("hide");
                                }, 3000);
                                return false;
                            }
                            else {

                                $('#AjaxLoader').hide();
                                $("#DivPin").modal("hide");
                                $("#txnMsg").html(jsonData.response);
                                $("#DivErrMessage").modal("show");
                            }


                        }
                        else {
                            if (response == "Session Expired") {

                                alert('Logged in from another device.');
                                window.location.href = "/MyPayUserLogin/Index";
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html(response);
                            }
                            else {

                                $('#AjaxLoader').hide();
                                $("#DivPin").modal("hide");
                                $("#txnMsg").html(response);
                                $("#DivErrMessage").modal("show");
                            }
                        }


                    },
                    failure: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response);
                    },
                    error: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response);
                    }
                });
            }, 100);
    }
}
$("#DivWallet").click(function () {
    $("#PopupText").text('')
    $("#hfPaymentMode").val('1');

    $("#DivWallet").css("background", "antiquewhite");
    $("#DivCoin").css("background", "#fff");
    $("#DivBank").css("background", "#fff");

    $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
    $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
    $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    CheckCoinBalance(0);
})
$("#DivCoin").click(function () {
    var smartpayCoins = $("#smartpayCoins").text();
    if (smartpayCoins <= 0) {
        return;
    }
    $("#PopupText").text('')
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivWallet").trigger("click");
        $("#PopupText").text('Your KYC should be approved to proceed.')
        $('#PopUpMsg').modal('show');
    }
    else {
        $("#hfPaymentMode").val('4');
        $("#DivWallet").css("background", "#fff");
        $("#DivBank").css("background", "#fff");
        $("#DivCoin").css("background", "antiquewhite");

        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");


        if ($("#hfKYCStatus").val() != '3') {
            $("#DivCoin").css("background", "#efefef");
            $("#DivBank").css("background", "#efefef");
        }
    }
    CheckCoinBalance(1);
})
$("#DivBank").click(function () {
    $("#PopupText").text('')
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivWallet").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
        $("#PopupText").text('Your KYC should be approved to proceed.')
        $('#PopUpMsg').modal('show');
    }
    else {
        $("#hfPaymentMode").val('2');
        $("#DivWallet").css("background", "#fff");
        $("#DivBank").css("background", "antiquewhite");
        $("#DivCoin").css("background", "#fff");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

        if ($("#hfKYCStatus").val() == '3') {
            if ($("#hfIsBankAdded").val() == "0") {
                window.location.href = '/MyPayUser/MyPayUserBankListAll';
            }
        }
    }
    CheckCoinBalance(0);
});

function CheckCoinBalance(ShowPopup) {
    var smartpayCoinsTotal = $("#smartpayCoinsTotal").text();
    var smartpayCoins = $("#smartpayCoins").text();
    var smartpayRupees = $("#smartpayRupees").text();
    var walletbalance = $("#hfWalletbalance").val();

    if (parseFloat(walletbalance) < parseFloat(smartpayRupees)) {

        $("#DivWallet").css("background", "antiquewhite");
        $("#DivBank").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

        $("#hfPaymentMode").val('1');
        if ($("#PopupText").text() == '') {
            $("#PopupText").text('Insufficient Wallet Balance');
        }
        if (ShowPopup == '1') {
            $('#PopUpMsg').modal('show');
        }
    }
    else if (parseFloat(smartpayCoinsTotal) < parseFloat(smartpayCoins)) {

        $("#DivWallet").css("background", "antiquewhite");
        $("#DivBank").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");


        $("#hfPaymentMode").val('1');
        if ($("#PopupText").text() == '') {
            $("#PopupText").text('Insufficient MP Coins');
        }
        if (ShowPopup == '1') {
            $('#PopUpMsg').modal('show');
        }
    }
}

$("#Amount").keypress(function (event) {
    if (event.keyCode == 13) {
        $('#btnPay')[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }

});
function GetBankDetail() {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetBankDetail",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {

                    var objBank = '';
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            if (jsonData.data[i].IsPrimary == true) {
                                objBank += ' <div class="nk-wg-action cursor-pointer">';
                                objBank += ' <div class="nk-wg-action-content d-block">';
                                objBank += ' <div class="d-flex align-items-center">';
                                objBank += ' <em class="icon top-0"><img src="' + jsonData.data[i].ICON_NAME + ' " width="22"></em>';
                                objBank += ' <div class="title">' + jsonData.data[i].BankName + ' </div>';
                                objBank += '</div>';
                                $("#hfBankId").val(jsonData.data[i].Id);
                                objBank += '<div class="text-soft">';
                                objBank += '<div class="d-block"><small class="d-block">' + jsonData.data[i].Name + ' </small>' + jsonData.data[i].AccountNumber + '</div>';
                                objBank += '<div class="d-block text-uppercase text-success fw-bold fs-12px mt-1"><img src="/Content/assets/MyPayUserApp/images/dashboard/primary.png" width="12" height="12"> Primary</div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';

                            }
                            else {
                                objBank += '<div class="nk-wg-action cursor-pointer">';
                                objBank += '<div class="nk-wg-action-content">';
                                objBank += '<em class="icon"><img src="/Content/assets/MyPayUserApp/images/dashboard/banksm.svg" width="22"></em>';
                                objBank += '<div class="title">Link Your Bank </div>';
                                objBank += '</div>';
                                objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                                objBank += '</div>';
                            }
                        }

                        $("#hfIsBankAdded").val("1");
                    }
                    else {
                        $("#hfIsBankAdded").val("0");

                        objBank += '<div class="nk-wg-action cursor-pointer">';
                        objBank += '<div class="nk-wg-action-content">';
                        objBank += '<em class="icon"><img src="/Content/assets/MyPayUserApp/images/dashboard/banksm.svg" width="22"></em>';
                        objBank += '<div class="title">Link Your Bank </div>';
                        objBank += '</div>';
                        objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                        objBank += '</div>';

                        $("#showmore").css("display", "none");
                    }
                    $("#DivBank").html(objBank);
                    $('#AjaxLoader').hide();
                    return false;
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
        }, 100);
}
$("#Pin").keypress(function (event) {
    if (event.keyCode == 13) {
        $('#btnPin')[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});
$("#btnPinBack").click(function () {

});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});

$("#btnGetDetail").click(function () {

    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var UserName = "";
    var Number = "";
    var Amount = "";
    var Address = "";
    var Package = "";
    var FullName = "";
    var Oldward = "";
    var Customerid = "";
    var Casid = "";
    if (ServiceID == "28") {
        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else if (Amount < 10 || Amount > 10001) {
            $("#dvMessage").html("Amount must be in between Rs. 10 to Rs. 10001");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Landline Number");
            return false;
        }
        if ((Number.substring(0, 1) == "0") || (Number.substring(0, 2) == "00") || (Number.substring(0, 3) == "000")) {
            $("#dvMessage").html("Please enter valid Number");
            return false;
        }
    }
    //else if (ServiceID == "29") {
    //    Amount = $("#Amount").val();
    //    if (Amount == "") {
    //        $("#dvMessage").html("Please enter Amount");
    //        return false;
    //    }
    //    else if (Amount < 400 || Amount > 30000) {
    //        $("#dvMessage").html("Amount must be in between Rs. 400 to Rs. 30000");
    //        return false;
    //    }

    //    UserName = $("#UserName").val();
    //    if (UserName == "") {
    //        $("#dvMessage").html("Please enter User Name");
    //        return false;
    //    }

    //    Number = $("#Number").val();
    //    if (Number == "") {
    //        $("#dvMessage").html("Please enter Number");
    //        return false;
    //    }
    //}

    if (ServiceID == "31" || ServiceID == "39" || ServiceID == "40") {
        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Customer Id");
            return false;
        }
    }

    if (ServiceID == "29" || ServiceID == "32" || ServiceID == "35" || ServiceID == "70") {
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }
    }

    if (ServiceID == "36" || ServiceID == "37" || ServiceID == "38") {
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }

        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else if (Amount < 100) {
            $("#dvMessage").html("Minimum amount should be Rs. 100");
            return false;
        }
    }
    if (ServiceID == "41") {
        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }

        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else if (Amount < 100 || Amount > 50000) {
            $("#dvMessage").html("Amount must be in between Rs. 100 to Rs. 50000");
            return false;
        }
        Address = $("#Address").val();
        if (Address == "") {
            $("#dvMessage").html("Please enter Address");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }
    }
    if (ServiceID == "66") {
        Package = $("#ddlPackage :selected").val();
        if (Package == "" || Package == "0") {
            $("#dvMessage").html("Please select Package");
            return false;
        }

        UserName = $("#UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter User Name");
            return false;
        }

        FullName = $("#FullName").val();
        if (FullName == "") {
            $("#dvMessage").html("Please enter Full Name");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Contact Number");
            return false;
        }
    }
    if (ServiceID == "67") {

        Package = $("#ddlPackage :selected").val();
        if (Package == "" || Package == "0") {
            $("#dvMessage").html("Please select Package");
            return false;
        }

        Casid = $("#Casid").val();
        if (Casid == "") {
            $("#dvMessage").html("Please enter CAS ID/ STD/ ID");
            return false;
        }

        FullName = $("#CustomerName").val();
        if (FullName == "") {
            $("#dvMessage").html("Please enter Customer Name");
            return false;
        }

        Number = $("#Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Contact Number");
            return false;
        }
        Oldward = $("#oldwardno").val();
        if (Oldward == "") {
            $("#dvMessage").html("Please enter Old ward no.");
            return false;
        }
        Customerid = $("#CustomerId").val();
        if (Customerid == "") {
            $("#dvMessage").html("Please enter Customer Id");
            return false;
        }
    }

    $('#AjaxLoader').show();
    var str = "";
    $("#tblInternetDetail").html("");
    if (ServiceID == "29" || ServiceID == "31" || ServiceID == "32" || ServiceID == "35" || ServiceID == "39" || ServiceID == "40" || ServiceID == "70") {
        setTimeout(
            function () {
                $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserInternetDetails",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{"ServiceId":"' + parseInt(ServiceID) + '","CustomerId":"' + Number + '","UserName":"' + UserName + '"}',
                    async: false,
                    success: function (response) {

                        var jsonData;
                        var IsValidJson = false;
                        try {
                            jsonData = $.parseJSON(response);
                            var IsValidJson = true;
                        }
                        catch (err) {

                        }
                        var arr = jsonData;

                        if (IsValidJson) {

                            if (arr['Message'] == "success") {
                                if (ServiceID == "29") {
                                    $("#hdnAccount").val(jsonData.SessionID);
                                    $("#hdnAmount").val(Amount);
                                    $("#hdnPlanType").val(jsonData.plan_type);
                                    $("#hdnReferenceNo").val(jsonData.ReferenceNo);

                                    var PlanType = jsonData.plan_type;

                                    str = '<div class="card col-md-12 mt-4">';
                                    str += '<div class="card-inner p-0">';
                                    str += '<div class="row">';
                                    str += '<div class="col-md-6 mb-3">';
                                    str += '<label class="mb-0 text-soft w-100">User Id</label>';
                                    str += '<span class="fw-medium">' + UserName + '</span>';
                                    str += '</div>';
                                    str += '<div class="col-md-6 mb-3 text-sm-right">';
                                    str += '<label class="mb-0 text-soft w-100">Mobile No</label>';
                                    str += '<span class="fw-medium">' + jsonData.mobile_no + '</span>';
                                    str += '</div>';
                                    str += '<div class="col-md-6 mb-3">';
                                    str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                    str += '<span class="fw-medium">' + jsonData.CustomerName + '</span>';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';

                                    if (PlanType == "internet") {

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetSubisuAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var j = 0; j < jsonData.plan_detail_list.plan_detail_list.internet_plan_details.length; ++j) {
                                            str += '<option value="' + jsonData.plan_detail_list.plan_detail_list.internet_plan_details[j].plan_name + '_' + jsonData.plan_detail_list.plan_detail_list.internet_plan_details[j].validity + '_' + jsonData.plan_detail_list.plan_detail_list.internet_plan_details[j].amount + '">' + jsonData.plan_detail_list.plan_detail_list.internet_plan_details[j].plan_name + '(' + jsonData.plan_detail_list.plan_detail_list.internet_plan_details[j].validity + ' ) &nbsp;&nbsp;&nbsp;&nbsp;रु ' + jsonData.plan_detail_list.plan_detail_list.internet_plan_details[j].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';

                                    }
                                    else if (PlanType == "internet + tv") {
                                        //internet package

                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Internet Package</label>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetSubisuAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var j = 0; j < jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details.length; ++j) {
                                            str += '<option value="' + jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[j].plan_name + '_' + jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[j].validity + '_' + jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[j].amount + '">' + jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[j].plan_name + '(' + jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[j].validity + ' ) &nbsp;&nbsp;&nbsp;&nbsp;रु ' + jsonData.plan_detail_list.plan_detail_list.internet_details.internet_plan_details[j].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';

                                        //stb list                                        
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">STB :</label>';
                                        str += '</div>';                                        

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackageSTB" class="form-control form-control-lg" onchange="OnSelectedddlPackageSTB();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var k = 0; k < jsonData.plan_detail_list.plan_detail_list.tv_details.length; ++k) {
                                            str += '<option value="' + jsonData.plan_detail_list.plan_detail_list.tv_details[k].stb + '">' + jsonData.plan_detail_list.plan_detail_list.tv_details[k].stb + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';


                                        //tv package
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">TV Package</label>';
                                        str += '</div>';

                                        if (jsonData.plan_detail_list.plan_detail_list.tv_details.length > 0) {
                                            str += '<div class="form-control-wrap col-md-12">';
                                            str += '<div class="input-group">';
                                            str += '<select id="ddlPackageTv" class="form-control form-control-lg" onchange="GetSubisuAmountTv();">';
                                            str += '<option value="0">Select Package</option>';
                                            for (var k = 0; k < jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details.length; ++k) {
                                                str += '<option value="' + jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[k].plan_name + '_' + jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[k].validity + '_' + jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[k].amount + '">' + jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[k].plan_name + '(' + jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[k].validity + ' ) &nbsp;&nbsp;&nbsp;&nbsp;रु ' + jsonData.plan_detail_list.plan_detail_list.tv_details[0].tv_plan_details[k].amount + '</option>';
                                            }
                                            str += '</select>';
                                            str += '</div>';
                                            str += '</div>';
                                        }
                                        else {
                                            str += '<div class="form-control-wrap col-md-12">';
                                            str += '<div class="input-group">';
                                            str += '<select id="ddlPackageTv" class="form-control form-control-lg" onchange="GetSubisuAmountTv();">';
                                            str += '<option value="0">Select Package</option>';
                                            
                                            str += '</select>';
                                            str += '</div>';
                                            str += '</div>';
                                        }
                                            
                                        
                                    }
                                    else if (PlanType == "tv") {
                                        
                                        //stb list                                        
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">STB :</label>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackageSTB" class="form-control form-control-lg" onchange="OnSelectedddlPackageSTB();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var k = 0; k < jsonData.plan_detail_list_offer.plan_detail_list.length; ++k) {
                                            str += '<option value="' + jsonData.plan_detail_list_offer.plan_detail_list[k].stb + '">' + jsonData.plan_detail_list_offer.plan_detail_list[k].stb + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';


                                        //tv package
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">TV Package</label>';
                                        str += '</div>';

                                        if (jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details.length > 0) {
                                            str += '<div class="form-control-wrap col-md-12">';
                                            str += '<div class="input-group">';
                                            str += '<select id="ddlPackageTv" class="form-control form-control-lg" onchange="GetSubisuAmountTv();">';
                                            str += '<option value="0">Select Package</option>';
                                            for (var k = 0; k < jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details.length; ++k) {
                                                str += '<option value="' + jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details[k].plan_name + '_' + jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details[k].validity + '_' + jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details[k].amount + '">' + jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details[k].plan_name + '(' + jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details[k].validity + ' ) &nbsp;&nbsp;&nbsp;&nbsp;रु ' + jsonData.plan_detail_list_offer.plan_detail_list[0].tv_plan_details[k].amount + '</option>';
                                            }
                                            str += '</select>';
                                            str += '</div>';
                                            str += '</div>';
                                        }
                                        else {
                                            str += '<div class="form-control-wrap col-md-12">';
                                            str += '<div class="input-group">';
                                            str += '<select id="ddlPackageTv" class="form-control form-control-lg" onchange="GetSubisuAmountTv();">';
                                            str += '<option value="0">Select Package</option>';

                                            str += '</select>';
                                            str += '</div>';
                                            str += '</div>';
                                        }


                                    }
                                    else if (PlanType == "combo offer") {
                                        //internet package

                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Combo Offer Package</label>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetSubisuAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var j = 0; j < jsonData.plan_detail_list_offer.plan_detail_list.length; ++j) {
                                            str += '<option value="' + jsonData.plan_detail_list_offer.plan_detail_list[j].offer_name + '_' + jsonData.plan_detail_list_offer.plan_detail_list[j].validity + '_' + jsonData.plan_detail_list_offer.plan_detail_list[j].amount + '">' + jsonData.plan_detail_list_offer.plan_detail_list[j].offer_name + '(' + jsonData.plan_detail_list_offer.plan_detail_list[j].validity + ' ) &nbsp;&nbsp;&nbsp;&nbsp;रु ' + jsonData.plan_detail_list_offer.plan_detail_list[j].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';

                                    }

                                    //$("#hdnAmount").val(Amount);
                                    //ServiceCharge();
                                    //$("#DivInternet").hide();
                                    //$("#DivInternetStep2").hide();
                                    //$("#DivInternetDetail").show();
                                }
                                if (ServiceID == "31") {
                                    if (jsonData != null && jsonData.bills.length > 0) {
                                        $("#hdnAccount").val(jsonData.SessionID);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Id</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.CustomerName + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var i = 0; i < jsonData.bills.length; ++i) {
                                            str += '<option value="' + jsonData.bills[i].payment_id + '">' + jsonData.bills[i].service_details + '&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.bills[i].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';
                                    }
                                }
                                if (ServiceID == "32") {
                                    if (jsonData != null && jsonData.available_plans.length > 0) {
                                        $("#hdnAccount").val(jsonData.SessionID);
                                        $("#hdnToken").val(jsonData.Token);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-12 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Username</label>';
                                        str += '<span class="fw-medium">' + UserName + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetClassictechAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var j = 0; j < jsonData.available_plans.length; ++j) {
                                            str += '<option value="' + jsonData.available_plans[j].package + '_' + jsonData.available_plans[j].duration + '_' + jsonData.available_plans[j].amount + '">' + jsonData.available_plans[j].package + '(' + jsonData.available_plans[j].duration + ' Month) &nbsp;&nbsp;&nbsp;&nbsp;रु ' + jsonData.available_plans[j].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';
                                    }
                                }
                                if (ServiceID == "35") {
                                    if (jsonData != null && jsonData.plan_details.length > 0) {
                                        $("#hdnAccount").val(jsonData.full_name);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Username</label>';
                                        str += '<span class="fw-medium">' + UserName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Current Plan</label>';
                                        str += '<span class="fw-medium">' + jsonData.current_plan + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.full_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Days Remaining</label>';
                                        str += '<span class="fw-medium">' + jsonData.days_remaining + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetArrowNetAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var k = 0; k < jsonData.plan_details.length; ++k) {
                                            str += '<option value="' + jsonData.plan_details[k].duration + '_' + jsonData.plan_details[k].amount + '">' + jsonData.plan_details[k].duration + ' Month&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.plan_details[k].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';

                                    }
                                }
                                if (ServiceID == "39") {
                                    if (jsonData != null) {
                                        $("#hdnAccount").val(jsonData.SessionID);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">First Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer.first_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Last Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer.last_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Id</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer.cuid + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Status</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer.status + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        str += '<div class="form-group col-md-12">';
                                        str += '<div class="form-control-wrap">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlUser" class="form-control form-control-lg" onchange="GetPackageList();">';
                                        str += '<option value="0">Select User</option>';
                                        for (var a = 0; a < jsonData.connection.length; ++a) {
                                            str += '<option value="' + jsonData.connection[a].username + '">' + jsonData.connection[a].username + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '<div id="dvPackage">';
                                        str += '</div>';

                                    }
                                }
                                if (ServiceID == "40") {
                                    if (jsonData != null) {
                                        $("#hdnAccount").val(jsonData.SessionID);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Id</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.data.customer_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Expirations</label>';
                                        str += '<span class="fw-medium">' + jsonData.data.expiration + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Monthly Charge</label>';
                                        str += '<span class="fw-medium">' + jsonData.data.monthly_charge + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        str += '<div class="form-group col-md-12">';
                                        str += '<div class="form-control-wrap">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_15Days')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_15Days + '">Plan_15Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_15Days + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_180Days')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_180Days + '">Plan_180Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_180Days + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_30Days')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_30Days + '">Plan_30Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_30Days + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_60Days')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_60Days + '">Plan_60Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_60Days + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_90Days')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_90Days + '">Plan_90Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_90Days + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_1Month')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_1Month + '">Plan_1Month&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_1Month + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_3Month')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_3Month + '">Plan_3Month&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_3Month + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_6Month')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_6Month + '">Plan_6Month&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_6Month + '</option>';
                                        }
                                        if (jsonData.Available_Plans.hasOwnProperty('Plan_12Month')) {
                                            str += '<option value="' + jsonData.Available_Plans.Plan_12Month + '">Plan_12Month&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.Available_Plans.Plan_12Month + '</option>';
                                        }

                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                    }
                                }
                                if (ServiceID == "70") {
                                    if (jsonData != null) {
                                        $("#hdnAccount").val(jsonData.SessionID);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Username</label>';
                                        str += '<span class="fw-medium">' + jsonData.username + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Current Plan</label>';
                                        str += '<span class="fw-medium">' + jsonData.subscribed_package_type + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.full_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Days Remaining</label>';
                                        str += '<span class="fw-medium">' + jsonData.days_remaining + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Subscribed Package</label>';
                                        str += '<span class="fw-medium">' + jsonData.subscribed_package_name + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        str += '<div class="form-control-wrap col-md-12">';
                                        str += '<div class="input-group">';
                                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetArrowNetAmount();">';
                                        str += '<option value="0">Select Package</option>';
                                        for (var k = 0; k < jsonData.plan_details.length; ++k) {
                                            str += '<option value="' + jsonData.plan_details[k].duration + '_' + jsonData.plan_details[k].amount + '">' + jsonData.plan_details[k].duration + ' Month&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.plan_details[k].amount + '</option>';
                                        }
                                        str += '</select>';
                                        str += '</div>';
                                        str += '</div>';

                                    }
                                }
                                //ServiceCharge();
                                $("#tblInternetDetail").append(str);
                                $('#AjaxLoader').hide();
                                $("#DivInternet").hide();
                                $("#DivInternetStep2").hide();
                                $("#DivInternetDetail").show();
                                return false;
                            }
                            else {
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html(arr['Message']);
                            }
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
            }, 20);
    }
    else if (ServiceID == "28") {
        str = '<div class="card col-md-12 mt-4">';
        str += '<div class="card-inner p-0">';
        str += '<div class="row">';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">User Id</label>';
        str += '<span class="fw-medium">' + Number + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Amount</label>';
        str += '<span class="fw-medium">रु ' + Amount + '</span>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        $("#hdnAmount").val(Amount);
        ServiceCharge();
        $("#DivInternet").hide();
        $("#DivInternetStep2").hide();
        $("#DivInternetDetail").show();
    }
    //else if (ServiceID == "29") {
    //    str = '<div class="card col-md-12 mt-4">';
    //    str += '<div class="card-inner p-0">';
    //    str += '<div class="row">';
    //    str += '<div class="col-md-6 mb-3">';
    //    str += '<label class="mb-0 text-soft w-100">User Id</label>';
    //    str += '<span class="fw-medium">' + Number + '</span>';
    //    str += '</div>';
    //    str += '<div class="col-md-6 mb-3 text-sm-right">';
    //    str += '<label class="mb-0 text-soft w-100">Amount</label>';
    //    str += '<span class="fw-medium">रु ' + Amount + '</span>';
    //    str += '</div>';
    //    str += '<div class="col-md-6 mb-3">';
    //    str += '<label class="mb-0 text-soft w-100">Customer Id</label>';
    //    str += '<span class="fw-medium">' + UserName + '</span>';
    //    str += '</div>';
    //    str += '</div>';
    //    str += '</div>';
    //    str += '</div>';
    //    $("#hdnAmount").val(Amount);
    //    ServiceCharge();
    //    $("#DivInternet").hide();
    //    $("#DivInternetStep2").hide();
    //    $("#DivInternetDetail").show();
    //}
    else if (ServiceID == "36" || ServiceID == "37" || ServiceID == "38") {
        str = '<div class="card col-md-12 mt-4">';
        str += '<div class="card-inner p-0">';
        str += '<div class="row">';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">User Name</label>';
        str += '<span class="fw-medium">' + UserName + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Amount</label>';
        str += '<span class="fw-medium">रु ' + Amount + '</span>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        $("#hdnAmount").val(Amount);
        ServiceCharge();
        $("#DivInternet").hide();
        $("#DivInternetStep2").hide();
        $("#DivInternetDetail").show();
    }
    else if (ServiceID == "41") {
        str = '<div class="card col-md-12 mt-4">';
        str += '<div class="card-inner p-0">';
        str += '<div class="row">';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">Username</label>';
        str += '<span class="fw-medium">' + UserName + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Amount</label>';
        str += '<span class="fw-medium">रु ' + Amount + '</span>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        $("#hdnAmount").val(Amount);
        ServiceCharge();
        $("#DivInternet").hide();
        $("#DivInternetStep2").hide();
        $("#DivInternetDetail").show();
    }
    else if (ServiceID == "66") {
        str = '<div class="card col-md-12 mt-4">';
        str += '<div class="card-inner p-0">';
        str += '<div class="row">';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
        str += '<span class="fw-medium">' + FullName + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Package</label>';
        str += '<span class="fw-medium">' + Package + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">Contact Number</label>';
        str += '<span class="fw-medium">' + Number + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Username</label>';
        str += '<span class="fw-medium">' + UserName + '</span>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        var selectedId = $("#ddlPackage :selected").val();
        var selectedAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
            $("#lblAmountTV").text(selectedAmount[1]);
            $("#hdnAmount").val(selectedAmount[1]);
            $("#hdnPackage").val(selectedId);
            $("#Amount").val(selectedAmount[1]);
        }

        //var SelectedPlanType = $("#ddlPackage :selected").val().split("_");
        //if (SelectedPlanType[0] != "0" || SelectedPlanType[0] != "") {
        //    $("#hdnPlanName").val(SelectedPlanType[0]);
        //}

        ServiceCharge();
        $("#DivInternet").hide();
        $("#DivInternetStep2").hide();
        $("#DivInternetDetail").show();
    }
    else if (ServiceID == "67") {
        str = '<div class="card col-md-12 mt-4">';
        str += '<div class="card-inner p-0">';
        str += '<div class="row">';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
        str += '<span class="fw-medium">' + FullName + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Customer Id</label>';
        str += '<span class="fw-medium">' + Customerid + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">Contact Number</label>';
        str += '<span class="fw-medium">' + Number + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">CAS ID</label>';
        str += '<span class="fw-medium">' + Casid + '</span>';
        str += '</div>';
        str += '<div class="col-md-6 mb-3">';
        str += '<label class="mb-0 text-soft w-100">Package</label>';
        str += '<span class="fw-medium">' + Package + '</span>';
        str += '</div>';

        str += '<div class="col-md-6 mb-3 text-sm-right">';
        str += '<label class="mb-0 text-soft w-100">Old ward no.</label>';
        str += '<span class="fw-medium">' + Oldward + '</span>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        str += '</div>';
        var selectedPackageId = $("#ddlPackage :selected").val();
        var selectedPackageAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedPackageAmount[1] != "0" || selectedPackageAmount[1] != "") {
            $("#lblAmountTV").text(selectedPackageAmount[1]);
            $("#hdnAmount").val(selectedPackageAmount[1]);
            $("#hdnPackage").val(selectedPackageId);
            $("#Amount").val(selectedPackageAmount[1])
        }
        ServiceCharge();
        $("#DivInternet").hide();
        $("#DivInternetStep2").hide();
        $("#DivInternetDetail").show();
    }


    $("#tblInternetDetail").append(str);
    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    $("#lblAmountTV").html($("#Amount").val());



    $("#DivProceedToPay").hide();
    //$("#dvMessage").html("");
    //$("#txnMsg").html("");
});

function GetAmount() {

    $("#hdnAmount").val("");
    $("#lblCashback").text("0.00");
    $("#lblServiceCharge").text("0.00");
    $("#lblAmount").html("0.00");
    $("#lblAmountTV").html("0.00");
    var selectedId = $("#ddlPackage :selected").val();
    if (selectedId === "0") {
        return;
    }
    var selectedAmount = $("#ddlPackage :selected").text().split("रु");
    if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
        $("#lblAmountTV").text(selectedAmount[1]);
        $("#hdnAmount").val(selectedAmount[1]);
        $("#hdnPaymentId").val(selectedId);
        ServiceCharge();
    }
    else {
        $("#dvMessage").html("Please select Package");
        return false;
    }

}

function GetClassictechAmount() {

    $("#hdnAmount").val("");
    //var selectedId = $("#ddlPackage :selected").val();
    var selectedAmount = $("#ddlPackage :selected").val().split("_");
    if (selectedAmount[0] != "" && selectedAmount[1] != "" && selectedAmount[2] != "") {
        $("#lblAmountTV").text(selectedAmount[2]);
        $("#hdnAmount").val(selectedAmount[2]);
        $("#hdnPackage").val(selectedAmount[0]);
        $("#hdnDuration").val(selectedAmount[1]);
        ServiceCharge();
    }
    else {
        $("#dvMessage").html("Please select Package");
        return false;
    }
}

function GetSubisuAmount() {

    $("#hdnAmount").val("");
    $('#ddlPackageTv').val(0);
    $('#ddlPackageSTB').val(0);
    //var selectedId = $("#ddlPackage :selected").val();
    var selectedAmount = $("#ddlPackage :selected").val().split("_");
    if (selectedAmount[0] != "" && selectedAmount[1] != "" && selectedAmount[2] != "") {
        $("#lblAmountTV").text(selectedAmount[2]);
        $("#hdnAmount").val(selectedAmount[2]);
        $("#hdnPackage").val(selectedAmount[0]);
        $("#hdnDuration").val(selectedAmount[1]);


        //$("#hdnPlanName").val(SelectedPlanType[0]);


        ServiceCharge();
    }
    else {
        $("#dvMessage").html("Please select Package");
        return false;
    }
}

function OnSelectedddlPackageSTB() {
    $("#hdnSTB").val("");
    $("#hdnAmount").val("");    
    $('#ddlPackage').val(0);
    $('#ddlPackageTv').val(0);

    var selectedSTB = $("#ddlPackageSTB :selected").val();
    if (selectedSTB != "" && selectedSTB != "0") {
        $("#hdnSTB").val(selectedSTB);
    }
    else {
        $("#dvMessage").html("Please select STB");
        return false;
    }


}

function GetSubisuAmountTv() {

    $("#hdnSTB").val("");
    $("#hdnAmount").val("");
    //var selectedId = $("#ddlPackage :selected").val();
    /*$('select option[value="1"]').attr("selected", true);*/
    $('#ddlPackage').val(0);
    
    var selectedSTB = $("#ddlPackageSTB :selected").val();
    if (selectedSTB != "" && selectedSTB != "0") {
        $("#hdnSTB").val(selectedSTB);
    }
    else {
        $("#dvMessage").html("Please select STB");
        return false;
    }

    var selectedAmount = $("#ddlPackageTv :selected").val().split("_");
    if (selectedAmount[0] != "" && selectedAmount[1] != "" && selectedAmount[2] != "") {
        $("#lblAmountTV").text(selectedAmount[2]);
        $("#hdnAmount").val(selectedAmount[2]);
        $("#hdnPackage").val(selectedAmount[0]);
        $("#hdnDuration").val(selectedAmount[1]);


        //$("#hdnPlanName").val(SelectedPlanType[0]);


        ServiceCharge();
    }
    else {
        $("#dvMessage").html("Please select Package");
        return false;
    }
}

function GetArrowNetAmount() {

    $("#hdnAmount").val("");
    //var selectedId = $("#ddlPackage :selected").val();
    var selectedAmount = $("#ddlPackage :selected").val().split("_");
    if (selectedAmount[0] != "" && selectedAmount[1] != "") {
        $("#lblAmountTV").text(selectedAmount[1]);
        $("#hdnAmount").val(selectedAmount[1]);
        $("#hdnDuration").val(selectedAmount[0]);

        ServiceCharge();
    }
    else {
        $("#dvMessage").html("Please select Package");
        return false;
    }
}

function GetPackageList() {
    $('#AjaxLoader').show();
    var SessionId = $("#hdnAccount").val();
    var ServiceID = $("#hdnServiceID").val();
    var UserName = $("#ddlUser :selected").val();
    var str = "";
    $("#dvPackage").html("");
    setTimeout(
        function () {
            $("#dvMessage").html("");
            $("html, body").animate({ scrollTop: "0" });
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserInternetWebSurferDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"ServiceId":"' + parseInt(ServiceID) + '","SessionId":"' + SessionId + '","UserName":"' + UserName + '"}',
                async: false,
                success: function (response) {

                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                    }
                    catch (err) {

                    }
                    var arr = jsonData;
                    if (IsValidJson) {

                        if (arr['Message'] == "success") {
                            if (ServiceID == "39") {
                                if (jsonData != null) {
                                    $("#hdnAccount").val(jsonData.SessionID);
                                    str = '<div class="form-group">';
                                    str += '<div class="form-control-wrap">';
                                    str += '<div class="input-group">';
                                    str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetWebsurferAmount();">';
                                    str += '<option value="0">Select Package</option>';
                                    for (var a = 0; a < jsonData.packages.length; ++a) {
                                        str += '<option value="' + jsonData.packages[a].package_id + '">' + jsonData.packages[a].package_name + '&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.packages[a].amount + '</option>';
                                    }
                                    str += '</select>';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';
                                }
                            }
                            //ServiceCharge();
                            $("#dvPackage").append(str);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(arr['Message']);
                        }
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
        }, 20);
}

function GetWebsurferAmount() {

    $("#hdnAmount").val("");
    var selectedId = $("#ddlPackage :selected").val();
    var selectedAmount = $("#ddlPackage :selected").text().split("रु");
    if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
        $("#lblAmountTV").text(selectedAmount[1]);
        $("#hdnAmount").val(selectedAmount[1]);
        $("#hdnPaymentId").val(selectedId);
        ServiceCharge();
    }
    else {
        $("#dvMessage").html("Please select Package");
        return false;
    }

}

