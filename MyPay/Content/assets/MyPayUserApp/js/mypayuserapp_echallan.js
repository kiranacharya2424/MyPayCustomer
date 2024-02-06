
var WalletResponse = '';
var AmountToPay = '';
var SessionId = '';
var discount = 0;
var payable = 0;
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            EchallanLoad();
        }, 10);

});
function EchallanLoad() {
    $("#DivGovernmentServices").show();
    $("#DivEchallan").hide();
    $("html, body").animate({ scrollTop: "0" });
}
$("#DivGovernmentServices").click(function () {
    $("#DivGovernmentServices").hide();
    $("#DivEchallan").show();
});


function BindDistrictDropdown() {
    var selectedProvince = provinceInfo.locations.filter((x) => { return x.province_code === $("#ddlProvince").val() });
    $("#ddlDistrict").empty();
    selectedProvince[0].district.forEach(function (district) {
        $("#ddlDistrict").append($("<option></option>").val(district.district_code).html(district.district_name));
    });

}
function showChallanDetails() {
    $("#DivGovernmentServices").hide();
    $("#DivEchallan").show();
}
$("#btn_back_challan").click(function () {
    $("#DivGovernmentServices").show();
    $("#DivEchallan").hide();

})

$("#btnGetDetail").click(function () {
    AmountToPay = "";
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select service id");
        return false;
    }
    var VoucherNo = $("#chitNumber").val();
    if (VoucherNo == "") {
        $("#dvMessage").html("Please enter chit number");
        return false;
    }
    var FiscalYear = $("#ddlFiscalYear :selected").val();
    if (FiscalYear == "" || FiscalYear == "0") {
        $("#dvMessage").html("Please select  fiscal year");
        return false;
    }
    var ProvinceCode = $("#ddlProvince :selected").val();
    if (ProvinceCode == "" || ProvinceCode == "0") {
        $("#dvMessage").html("Please select  province code");
        return false;
    }
    var DistrictCode = $("#ddlDistrict :selected").val();
    if (DistrictCode == "" || DistrictCode == "0") {
        $("#dvMessage").html("Please select  district code");
        return false;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserEchallanDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"ServiceId":"' + ServiceID + '","VoucherNo":"' + VoucherNo + '","FiscalYear":"' + FiscalYear + '","ProvinceCode":"' + ProvinceCode + '","DistrictCode":"' + DistrictCode + '"}',
                async: false,
                success: function (response) {

                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                        AmountToPay = jsonData.Amout;
                        $("#hfReference").val(jsonData.Reference);
                    }
                    catch (err) {

                    }
                    if (IsValidJson) {
                        $('#AjaxLoader').hide();
                        $("#DivEchallan").hide();
                        $("#DivChallanDetail").show();
                        $("#lblAmountToPay").text(jsonData.Amout);
                        $("#Amount").val($("#lblAmountToPay").text());

                        SessionId = jsonData.Session_Id;
                        var str = "";
                        str += '<div class="card mt-4">';
                        str += '<div class="card-inner p-0">';
                        str += '<div class="row">';
                        str += '<div class="col-md-6 mb-3">';
                        str += '<label class="mb-0 text-soft w-100">Chit Number</label>';
                        str += '<span class="fw-medium">' + jsonData.ChitNumber + '</span>';
                        str += '</div>';
                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                        str += '<label class="mb-0 text-soft w-100">Name</label>';
                        str += '<span class="fw-medium">' + jsonData.FullName + '</span>';
                        str += '</div>';
                        str += '<div class="col-md-6 mb-3">';
                        str += '<label class="mb-0 text-soft w-100">Ebp Number</label>';
                        str += '<span class="fw-medium">' + jsonData.EbpNumber + '</span>';
                        str += '</div>';
                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                        str += '<label class="mb-0 text-soft w-100">Amount</label>';
                        str += '<span class="fw-medium">' + jsonData.Amout + '</span>';
                        str += '</div>';
                        str += '<div class="col-md-12 mb-12">';
                        str += '<label class="mb-0 text-soft w-100">Description</label>';
                        str += '<span class="fw-medium">' + jsonData.Description + '</span>';
                        str += '</div></div></div>';

                        $("#tblChallanDetail").html('');
                        $("#tblChallanDetail").append(str);
                    }
                    else {
                        $("#dvMessage").html(response);
                        $('#AjaxLoader').hide();
                    }

                }
            })
        }, 20)
})
$("#btn_back_detail").click(function () {
    $("#DivEchallan").show();
    $("#DivChallanDetail").hide();

})
$("#btnPay").click(function () {

    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = $("#Account").val();
    if (Number == "") {
        $("#dvMessage").html("Please enter Number");
        return false;
    }
    var Amount = "";
    if (ServiceID == "73") {

        if (AmountToPay != "0" || AmountToPay != "") {
            Amount = AmountToPay;
            $("#lblAmount").text(Amount);
        }

    }
    else {
        Amount = AmountToPay;
    }

    $("#DivGovernmentServices").hide();
    $("#DivEchallan").hide();
    $("#DivChallanDetail").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    if (ServiceID == "73") {
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
    debugger;

    var ServiceId = $("#hdnServiceID").val();
    var Amount = "";
    if (ServiceId == "73") {
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
                        $("#MobilePopup").text($("#Account").val());
                        $("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
                        $("#TxnAmountPopup").text(arr['Amount']);
                        var Amount = parseFloat(arr['Amount']);
                        var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                        var totalamount = parseFloat(Amount + ServiceCharge) - discount;
                        $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));
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
    var Reference = $("#hfReference").val();
    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }

    var CustomerID = $("#Account").val();
    if (CustomerID == "") {
        $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
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
                    url: "/MyPayUser/MyPayUserEchallan",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","Amount":"' + AmountToPay + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","SessionId":"' + Session_Id + '","Reference":"' + Reference + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
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
                                return false;
                            }
                            else {

                                $('#AjaxLoader').hide();
                                $("#DivPin").modal("hide");
                                $("#txnMsg").html(jsonData.responseMessage);
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
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});