var WalletResponse = '';
var discount = 0;
var payable = 0;
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            TopupLoad();

        }, 10);
    $("#DivWallet").trigger("click");
});
function TopupLoad() {

    $('#AjaxLoader').show();
    $("#DivTopup").show();
    $("#DivProceedToPay").hide();
    $("#DivPin").hide();
    $('#AjaxLoader').hide();
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }

    $("html, body").animate({ scrollTop: "0" });
}

$("#MobileNo").keypress(function (event) {
    debugger;
    if (event.keyCode == 13) {
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});

$("#MobileNo").keyup(function (event) {
    debugger;
    if (event.keyCode == 13) {
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
    var MobileNo = $(this).val();
    var regex1 = /^([9][8][5][0-9]{7})$/;
    var regex2 = /^([9][7][4-5][0-9]{7})$/;
    var regex3 = /^([9][8][46][0-9]{7})$/;
    var regex4 = /^([9][8][0-2][0-9]{7})$/;
    var regex5 = /^([9][6][0-9]{8}|[9][8][8][0-9]{7})$/;
    $('#Providerlist option:selected').removeAttr('selected');
    if (MobileNo.match(regex1)) {
        $('#Providerlist option[value="2"]').attr("selected", "selected").change();
        $("#btnPay").removeClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer active d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#hdnServiceID").val("2");
    }
    else if (MobileNo.match(regex2)) {
        $('#Providerlist option[value="2"]').attr("selected", "selected").change();
        $("#btnPay").removeClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer active d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#hdnServiceID").val("2");
    }
    else if (MobileNo.match(regex3)) {
        $('#Providerlist option[value="2"]').attr("selected", "selected").change();
        $("#btnPay").removeClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer active d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#hdnServiceID").val("2");
    }
    else if (MobileNo.match(regex4)) {
        $('#Providerlist option[value="3"]').attr("selected", "selected").change();
        $("#btnPay").removeClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer active d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#hdnServiceID").val("3");
    }
    else if (MobileNo.match(regex5)) {
        $('#Providerlist option[value="4"]').attr("selected", "selected").change();
        $("#btnPay").removeClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer active d-block");
        $("#hdnServiceID").val("4");
    }
    else if (MobileNo.substring(0, 3) == "976") {
        $('#Providerlist option[value="2"]').attr("selected", "selected").change();
        $("#btnPay").removeClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer active d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#hdnServiceID").val("2");
    }
    else {
        $('#Providerlist option[value="0"]').attr("selected", "selected").change();
        $("#btnPay").addClass("disabled");
        $("#dvNTC").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvNCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#dvSMARTCELL").attr("class", "badge rounded-pill bg-light border-0 cursor-pointer d-block");
        $("#hdnServiceID").val("0");
    }
});

$("#btnPay").click(function () {
    $('#btnapplycoupon').show();
    $('#btnremovecoupon').hide();
    var Providerlist = $("#Providerlist").val();
    if (Providerlist == "") {
        $("#dvMessage").html("Please select Providerlist");
        return false;
    }
    var MobileNo = $("#MobileNo").val();
    if (MobileNo == "") {
        $("#dvMessage").html("Please enter MobileNo");
        return false;
    }
    var Amount = $("#Amount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
    }

    if (parseFloat(Amount) < 10 || parseFloat(Amount) > 5000) {
        $("#txnMsg").html("Recharge amount should be between <br> Rs. 10 to 5000");
        $("#DivErrMessage").modal("show");
        return false;
    }

    //Case of smart cell
    var regex5 = /^([9][6][0-9]{8}|[9][8][8][0-9]{7})$/;
    if (MobileNo.match(regex5)) {
        var amout_arr = ['20', '50', '100', '200', '500', '1000'];
        var index = amout_arr.indexOf(Amount);
        if (index < 0) {
            $("#dvMessage").html("Recharge amount should only in 20,50,100,200,500,1000");
            return false;
        }
    }
    var ServiceId = $("#hdnServiceID").val();
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
                $("#DivTopup").hide();
                $('#AjaxLoader').show();
                setTimeout(function () {
                    $("html, body").animate({ scrollTop: "0" });
                    GetBankDetail();
                    ServiceCharge();
                }, 20);

                var arr = $.parseJSON(WalletResponse);
                $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
                $("#lblAmount").text($("#Amount").val());
                $("#lblDelAmount").text($("#Amount").val());
                $("#dvcouponapply").hide();
                $("#DivProceedToPay").show();
                $("#dvMessage").html("");
                $("#txnMsg").html("");
                $('#DivWallet')[0].click();

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

});

function Back(HideDiv, ShowDiv) {
    $("#" + ShowDiv).show(100);
    $("#" + HideDiv).hide(100);
    $("#dvMessage").html("");
    $("html, body").animate({ scrollTop: "0" });
}
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
    debugger;
    var ServiceId = $("#Providerlist").val();
    var PaymentMode = $("#hfPaymentMode").val();
    //if (PaymentMode == "0") {
    //    $("#dvMessage").html("Please select Payment Mode");
    //    return false;
    //}
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ServiceCharge",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + $("#Amount").val() + '","ServiceId":"' + ServiceId + '"}',
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
                    var arr = jsonData;
                    if (arr['Message'] == "success") {
                        $("#MobilePopup").text($("#MobileNo").val());
                        $("#TxnAmountPopup").text(arr['Amount']);
                        //$("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#AmountPopup").text(arr['WalletAmountDeduct']);
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#MypayCoinCreditedPopup").text(arr['RewardPoints']);
                        $("#CouponDiscountPopup").text(discount);

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

                        var smartPayCoin = parseFloat($("#smartpayCoins").text());

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

function Pay() {
    debugger;

    var Couponcode = $('#hdncouponcode').val();
    var Providerlist = $("#Providerlist").val();
    if (Providerlist == "") {
        $("#dvMessage").html("Please select Providerlist");
        return false;
    }
    var MobileNo = $("#MobileNo").val();
    if (MobileNo == "") {
        $("#dvMessage").html("Please enter MobileNo");
        return false;
    }
    var Amount = $("#Amount").val();
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
                    url: "/MyPayUser/MyPayUserTopup",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Providerlist":"' + Providerlist + '","MobileNo":"' + MobileNo + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","Couponcode":"' + Couponcode + '","BankId":"' + $("#hfBankId").val() + '"}',
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
                                TopupLoad();
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
    debugger;
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
    TopupLoad();
});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});


function LinkBankTransaction() {
    debugger;
    var Pin = $("#Pin").val();
    if (Pin == "") {
        $("#dvMsg").html("Please enter Pin");
        return false;
    }
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var ServiceId = $("#Providerlist").val();
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserLinkedBankTransaction",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + $("#Amount").val() + '","VendorType":"' + ServiceId + '","Remarks":"' + "paybill_n_recharge" + '","BankId":"' + $("#hfBankId").val() + '","Mpin":"' + Pin + '"}',
                success: function (response) {
                    debugger;
                    var jsonData = $.parseJSON(response);
                    if (jsonData['ReponseCode'] == '1') {
                        var BankTransactionID = jsonData["TransactionId"];
                        if (BankTransactionID != 'undefined' && BankTransactionID != '') {
                            PaymentMode = '2';
                            setTimeout(
                                function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/MyPayUser/MyPayUserTopup",
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: false,
                                        data: '{"Providerlist":"' + Providerlist + '","MobileNo":"' + MobileNo + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '"}',
                                        success: function (response) {
                                            if (response == "success") {
                                                $("#DivPin").modal("hide");
                                                $("#PaymentSuccess").modal("show");
                                                $("#dvMessage").html("Success");
                                                $('#AjaxLoader').hide();
                                                TopupLoad();
                                                return false;
                                            }
                                            else if (response == "Session Expired") {
                                                debugger;
                                                alert('Logged in from another device.');
                                                window.location.href = "/MyPayUserLogin/Index";
                                                $('#AjaxLoader').hide();
                                                $("#dvMessage").html(response);
                                            }
                                            else {
                                                debugger;
                                                $('#AjaxLoader').hide();
                                                $("#DivPin").modal("hide");
                                                $("#txnMsg").html(response);
                                                $("#DivErrMessage").modal("show");
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
                                }, 100);
                        }
                    }
                    else {
                        $("#DivPin").modal("hide");
                        $("#txnMsg").html(jsonData['Message']);
                        $("#DivErrMessage").modal("show");
                        $('#AjaxLoader').hide();
                    }
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