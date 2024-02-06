
var WalletResponse = '';
var AmountToPay = '';
var SessionId = '';
var DueAmount = '';
var discount = 0;
var payable = 0;
$(document).ready(function () {

});


$("#btnGetDetail").click(function () {
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select service id");
        return false;
    }
    var Counter = $("#ddlCounter :selected").val();
    if (Counter == "" || Counter == "0") {
        $("#dvMessage").html("Please select counter");
        return false;
    }
    var ScNumber = $("#scNumber").val();
    if (ScNumber == "" || ScNumber == "0") {
        $("#dvMessage").html("Please enter  sc number");
        return false;
    }
    var ConsumerId = $("#consumerId").val();
    if (ConsumerId == "" || ConsumerId == "0") {
        $("#dvMessage").html("Please enter consumer id");
        return false;
    }

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserElectricityDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"ServiceId":"' + ServiceID + '","Counter":"' + Counter + '","ScNumber":"' + ScNumber + '","ConsumerId":"' + ConsumerId + '"}',
                async: false,
                success: function (response) {

                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                        AmountToPay = jsonData.total_due_amount;

                    }
                    catch (err) {

                    }
                    $('#AjaxLoader').hide();
                    if (IsValidJson) {
                        if (jsonData.Message == "success") {
                            $("#DivElectricity").hide();
                            $("#DivElectricityDetail").show();
                            $("#spn_counter").text(Counter);
                            $("#spn_scNumber").text(ScNumber);
                            $("#spn_consumerId").text(ConsumerId);
                            $("#spn_consumer").text(jsonData.consumer_name);
                            $("#spn_totalDue").text(jsonData.total_due_amount);
                            $("#enteramt").val(jsonData.total_due_amount);
                            $("#Amount").val(jsonData.total_due_amount);
                            DueAmount = jsonData.total_due_amount;
                            $("#tblBillDetail").html('');
                            SessionId = jsonData.session_id;

                            var str = "";
                            var index = 0;
                            jsonData.due_bills.forEach(function (bill) {
                                index = index + 1;
                                str += '<div class="col-md-3 line-height-1 mb-3">';
                                str += '<span  class="fw-medium" title="' + bill.due_bill_of + '"> <input type="checkbox" name="chk_amout" onClick="calculateAmout()" checked="checked" id="bill' + index + '" value=' + bill.payable_amount + '/>' + bill.due_bill_of.substring(0, 10); + '</span>';
                                str += '</div><div class="col-md-3 line-height-1 mb-3">';
                                str += '<span class="fw-medium">' + bill.bill_amount + '</span>';
                                str += '</div> <div class="col-md-3 line-height-1 mb-3">';
                                str += '<span class="fw-medium text-orange">' + bill.status + '</span>';
                                str += '</div><div class="col-md-3 line-height-1 mb-3">';
                                str += ' <span class="fw-medium">' + bill.payable_amount + '</span></div>';
                            });
                            $("#tblBillDetail").append(str);
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(jsonData.Message);
                        }
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html(response);
                    }
                }
            })
        }, 20)
})

function calculateAmout() {
    var selectedBill = $.map($('input[name="chk_amout"]:checked'), function (c) { return c.value; });
    var sum = selectedBill.reduce((pv, cv) => {
        return pv + (parseFloat(cv) || 0);
    }, 0);
    $("#spn_totalDue").text(sum);
    $("#enteramt").val(sum);

}
$("#btn_back").click(function () {
    $("#DivElectricityDetail").hide();
    $("#DivElectricity").show();
    $("#dvMessage").html("");
})

$("#btnValidateOk").click(function () {
    $('#ValidationPopup').modal('hide');

})
$("#btnPay").click(function () {
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = $("#scNumber").val();
    if (Number == "") {
        $("#dvMessage").html("Please enter SC Number");
        return false;
    }
    AmountToPay = $("#enteramt").val();
    if (parseFloat(AmountToPay) < 1) {
        $("#dvMessage").html("Entered amount should be greater than 0.");
        return;
    }
    if (parseFloat(AmountToPay) < parseFloat(DueAmount)) {
        $('#ValidationPopup').modal('show');
        return;
    }
    var Amount = "";
    if (ServiceID == "12") {

        if (AmountToPay != "0" || AmountToPay != "") {
            Amount = AmountToPay;
            $("#lblAmount").text(Amount);
        }

    }
    else {
        Amount = AmountToPay;
    }

    $("#DivElectricityDetail").hide();
    $("#DivElectricity").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    if (ServiceID == "12") {
        var selectedAmount = AmountToPay;
        if (selectedAmount != "0" || selectedAmount != "") {
            Amount = selectedAmount;
            $("#lblPayAmount").text(selectedAmount);
        }
    }
    else {
        $("#lblPayAmount").text(AmountToPay);
    }

    $("#DivProceedToPay").show();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
    $('#DivWallet')[0].click();
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
function ServiceCharge() {

    var ServiceId = $("#hdnServiceID").val();
    var PaymentMode = $("#hfPaymentMode").val();
    var Amount = "";
    if (ServiceId == "12") {
        var selectedAmount = AmountToPay;
        if (selectedAmount != "0" || selectedAmount != "") {
            Amount = selectedAmount;
            $("#lblAmount").text(selectedAmount);
        }
    }
    else {
        Amount = AmountToPay;
    }

    $('#AjaxLoader').show();
    setTimeout(
        function () {
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
                        $("#ConsumerIdPopup").closest('tr').show();
                        $("#ConsumerIdPopup").text($("#consumerId").val());
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
                        $("#lblAmount").text(Amount);
                        CheckCoinBalance(0);
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
        $('#lblAmount').text($("#TotalAmountPopup").text());
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

function Pay() {
    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }

    var Mpin = $("#Pin").val();
    if (Mpin == "") {
        $("#dvMessage").html("Please enter Pin");
        return false;
    }

    var Session_Id = SessionId;
    if (Session_Id == "") {
        $("#dvMessage").html("Please enter SessionId");
        return false;
    }

    var Counter = $("#ddlCounter :selected").val();
    var ScNumber = $("#scNumber").val();
    var ConsumerId = $("#consumerId").val();
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
                    url: "/MyPayUser/MyPayUserElectricity",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","Amount":"' + AmountToPay + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","SessionId":"' + Session_Id + '","Counter":"' + Counter + '","ScNumber":"' + ScNumber + '","ConsumerId":"' + ConsumerId + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
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
                            if (jsonData.Message == "success") {
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