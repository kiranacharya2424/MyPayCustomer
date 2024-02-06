var WalletResponse = '';
var discount = 0;
var payable = 0;

$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            CreditCardLoad();
        }, 10);
    $("#DivWallet").trigger("click");
});
function CreditCardLoad() {
    $('#AjaxLoader').show();
    $("#DivCreditCard").show();
    $("#DivProceedToPay").hide();
    $("#DivPin").hide();
    $('#AjaxLoader').hide();
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }

    $("html, body").animate({ scrollTop: "0" });
}


$("#btnNextStep").click(function () {

    if ($("#BankCodes").val() == "") {
        $("#txnMsg").html("Please select Bank.");
        $("#DivErrMessage").modal("show");
        return;
    }
    else if ($("#Number").val() == "") {
        $("#txnMsg").html("Please enter Credit Card Number.");
        $("#DivErrMessage").modal("show");
        return;
    }
    else if ($("#Number").val().length < 16) {
        $("#txnMsg").html("Please enter valid Credit Card Number.");
        $("#DivErrMessage").modal("show");
        return;
    }
    else if ($("#CardOwner").val() == "") {
        $("#txnMsg").html("Please enter Credit Card owner.");
        $("#DivErrMessage").modal("show");
        return;
    }
    else if ($("#Amount").val() == "") {
        $("#txnMsg").html("Please enter amount.");
        $("#DivErrMessage").modal("show");
        return;
    }
    else if (parseFloat($("#Amount").val()) < 100 || parseFloat($("#Amount").val()) > 100000) {
        $("#txnMsg").html("Please enter valid amount.");
        $("#DivErrMessage").modal("show");
        return;
    }
    var ServiceId = $("#hdnServiceID").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var Amount = $("#Amount").val();
            var Code = $("#BankCodes").val();
            $("#hdnBaseAmount").val(Amount);
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserCreditCardDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + Amount + '","Code":"' + Code + '"}',
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
                        var arr = jsonData;

                        if (arr['Message'].toLowerCase() == "success") {
                            $("#spnBankName").html($("#BankCodes option:selected").text());
                            $("#spnCardNumber").html($("#Number").val());
                            $("#spnCreditCardOwner").html($("#CardOwner").val());
                            $("#spnCreditCardAmount").html($("#Amount").val());
                            $("#spnCreditCardFee").html(arr['Details']);
                            $("#spnDueAmount").html(parseFloat($("#Amount").val()) + parseFloat(arr['Details']));
                            $("#DivCreditCardStep2").show();
                            $("#DivCreditCard").hide();
                            $("#DivProceedToPay").hide();
                            $("#lblAmount").text($("#Amount").val());
                            $("#dvMessage").html("");
                            $("#txnMsg").html("");
                            ServiceCharge();
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

});

$("#btnPayBack").click(function () {
    $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
    $("#DivCreditCardStep2").hide();
    $("#DivCreditCard").show();
    $("#DivProceedToPay").hide();
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
    var Amount = $("#spnDueAmount").html();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
    }
    $("#DivCreditCard").hide();
    $("#DivCreditCardStep2").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    $("#lblAmount").text($("#spnDueAmount").html());
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
        $('#PaymentPopup').modal('show');
        $("#CouponDiscountPopup").text(discount);
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
    var Amount = $("#hdnBaseAmount").val();
    var PaymentMode = $("#hfPaymentMode").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ServiceCharge",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + Amount + '","ServiceId":"' + ServiceId + '"}',
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

                    if (arr['Message'].toLowerCase() == "success") {
                        $("#MobilePopup").closest("tr").hide();
                        $("#CreditCardFeePopup").text(parseFloat($("#spnCreditCardFee").text()));
                        $("#CreditCardFeePopup").closest("tr").show();
                        $("#Amount").text(arr['Amount']);
                        $("#AmountPopup").text(arr['Amount']);
                        $("#TxnAmountPopup").text(arr['Amount']);
                        $("#CardNumberPopup").text($("#Number").val());
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#spnServiceChargeAmount").text(arr['ServiceChargeAmount']);
                        $("#spnCashbackAmount").text(arr['CashbackAmount']);


                        $("#MypayCoinDebitedPopup").closest('tr').hide();
                        var MPCoinsDebit = parseFloat(arr['MPCoinsDebit']);
                        if (PaymentMode == "4") {
                            $("#AmountPopup").text(arr['WalletAmountDeduct']);
                            if (MPCoinsDebit > 0) {
                                $("#MypayCoinDebitedPopup").closest('tr').show();
                            }
                            var DeductAmount = parseFloat(arr['WalletAmountDeduct']);
                            var DeductServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = parseFloat(DeductAmount + DeductServiceCharge) - discount;
                            $("#TotalAmountPopup").text(parseFloat(DeductAmount + DeductServiceCharge).toFixed(2));
                        }
                        else {
                            var smartPayCoin = parseFloat($("#smartpayCoins").text());
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = parseFloat(Amount + ServiceCharge + parseFloat($("#spnCreditCardFee").text()) - arr['CashbackAmount'] - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));
                        }

                        $('#DivCoin').show();
                        if (smartPayCoin <= 0) {
                            $('#DivCoin').hide();
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

$("#Number").keyup(function () {
    EnableDisablePayButton();
});
$("#Amount").keyup(function () {
    EnableDisablePayButton();
});
$("#CardOwner").keyup(function () {
    EnableDisablePayButton();
});
$("#BankCodes").change(function () {
    EnableDisablePayButton();
});

function EnableDisablePayButton() {


    if ($("#BankCodes").val() == "undefined" || $("#BankCodes").val() == "0") {
        $("#btnNextStep").addClass("disabled");
        return;
    }
    if ($("#CardOwner").val() == "") {
        $("#btnNextStep").addClass("disabled");
        return;
    }
    if ($("#Number").val() == "") {
        $("#btnNextStep").addClass("disabled");
        return;
    }
    if ($("#Number").val().length < 16) {
        $("#btnNextStep").addClass("disabled");
        return;
    }
    if ($("#Amount").val() == "") {
        $("#btnNextStep").addClass("disabled");
        return;
    }
    if (parseFloat($("#Amount").val()) < 100 || parseFloat($("#Amount").val()) > 100000) {
        $("#btnNextStep").addClass("disabled");
        return;
    }
    $("#btnNextStep").removeClass("disabled");
}

function Pay() {

    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Code = $("#BankCodes").val();
    if (Code == "") {
        $("#dvMessage").html("Please Select Bank");
        return false;
    }
    var BankName = $("#BankCodes option:selected").text();
    if (BankName == "") {
        $("#dvMessage").html("Please Select Bank");
        return false;
    }
    var CardNumber = $("#Number").val();
    if (CardNumber == "") {
        $("#dvMessage").html("Please enter CardNumber");
        return false;
    }

    var ServiceChargeAmount = $("#spnCreditCardFee").text();
    if (ServiceChargeAmount == "") {
        $("#dvMessage").html("Please enter Service Charge Amount");
        return false;
    }
    var Amount = $("#spnDueAmount").html();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
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
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserCreditCard",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","BankName":"' + BankName + '","BankCode":"' + Code + '","CardNumber":"' + CardNumber + '","ServiceCharge":"' + ServiceChargeAmount + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
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
                            if (jsonData.Message == "Success") {
                                if (jsonData.IsCouponUnlocked == true) {
                                    $("#ScratchCardwonpopup").modal("show");
                                    setTimeout(function () {
                                        $("#ScratchCardwonpopup").modal("hide");
                                    }, 3000);
                                }
                                $("#DivPin").modal("hide");
                                $("#PaymentSuccess").modal("show");
                                $("#dvMessage").html("Success");
                                $('#AjaxLoader').hide();
                                CreditCardLoad();
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
    CreditCardLoad();
});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});