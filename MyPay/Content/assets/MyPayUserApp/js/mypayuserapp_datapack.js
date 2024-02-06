var DataPacks = '';
var SelectedPack = '';
var WalletResponse = '';
var discount = 0;
var payable = 0;

$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            DataPackLoad();
        }, 10);
    $("#DivWallet").trigger("click");
});
function DataPackLoad() {
    $('#AjaxLoader').show();
    $("#DivDataPack").show();
    $("#DivProceedToPay").hide();
    $("#DivDataPackStep3").hide();
    $("#DivPin").hide();
    $('#AjaxLoader').hide();
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    $("html, body").animate({ scrollTop: "0" });
}


$("#MobileNumber").keypress(function (event) {

    if (event.keyCode == 13) {
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});

function showDataPackDetails(objID, providername) {

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
                $("#DivDataPackStep2").show();
                $("#DivDataPackStep3").hide();
                $("#DivDataPack").hide();
                $("#DivProceedToPay").hide();
                $("#hdnServiceID").val(objID);
                $("#lblProviderName").html(providername);
                $('#AjaxLoader').show();
                setTimeout(
                    function () {
                        $.ajax({
                            type: "POST",
                            url: "/MyPayUser/MyPayUserDataPackDetails",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: '{"ServiceId":"' + parseInt(objID) + '"}',
                            async: false,
                            success: function (response) {
                                ;
                                var jsonData;
                                var IsValidJson = false;
                                try {
                                    jsonData = $.parseJSON(response);
                                    var IsValidJson = true;
                                }
                                catch (err) {

                                }
                                DataPacks = '';
                                var arr = jsonData;
                                DataPacks = jsonData;
                                var str = "";

                                $("#tblNCellDataPackage").html("");

                                if (IsValidJson) {
                                    if (arr['Message'] == "success") {
                                        if (jsonData != null && jsonData.packages != null && jsonData.packages.length > 0) {

                                            if (objID == "42") {
                                                $("#tblNCellDataPackage").show();
                                                $("#tblNTCDataPackage").hide();
                                                str = '<table><tbody>';
                                                for (var i = 0; i < jsonData.packages.length; ++i) {
                                                    str += "<tr><td>" + jsonData.packages[i].product_name + "</td>";
                                                    str += "<td>" + jsonData.packages[i].amount + "</td>";
                                                    str += "<td><a href='javascript:void(0);' class='btn btn-primary' onclick='Buy(&apos;" + jsonData.packages[i].product_code + "&apos;,&apos;" + jsonData.packages[i].amount + "&apos;,&apos;" + jsonData.packages[i].package_id + "&apos;,&apos;" + i + "&apos;);'>Buy</a></td>";
                                                    str += "<td><a href='javascript:void(0);' class='btn btn-primary' onclick='PackageDetails(&apos;" + jsonData.packages[i].priority_no + "&apos;,&apos;" + jsonData.packages[i].product_name + "&apos;,&apos;" + jsonData.packages[i].amount + "&apos;,&apos;" + jsonData.packages[i].short_detail + "&apos;,&apos;" + jsonData.packages[i].product_code + "&apos;,&apos;" + jsonData.packages[i].description + "&apos;,&apos;" + jsonData.packages[i].prodcut_type + "&apos;,&apos;" + jsonData.packages[i].validity + "&apos;)'>Details</a></td></tr>";
                                                }

                                                str += '</tbody></table>';
                                                $("#tblNCellDataPackage").append(str);
                                            }
                                            else if (objID == "61") {
                                                $("#tblNCellDataPackage").hide();
                                                $("#tblNTCDataPackage").show();
                                                $("#uldatapack").html("");
                                                str = '<table><tbody>';
                                                for (var i = 0; i < jsonData.packages.length; ++i) {
                                                    if (jsonData.packages[i].product_type == "Data Pack") {
                                                        str += '<tr><td>' + jsonData.packages[i].product_name + '</td>';
                                                        str += '<td>' + jsonData.packages[i].amount + '</td>';
                                                        str += '<td><a href="javascript:void(0);" class="btn btn-primary" onclick="Buy(&apos;' + jsonData.packages[i].product_name + '&apos;,&apos;' + jsonData.packages[i].amount + '&apos;,&apos;' + jsonData.packages[i].package_id + '&apos;,&apos;' + i + '&apos;);">Buy</a></td>';
                                                        str += '<td><a href="javascript:void(0);" class="btn btn-primary" onclick="PackageDetails(&apos;' + jsonData.packages[i].priority_no + '&apos;,&apos;' + jsonData.packages[i].product_name + '&apos;,&apos;' + jsonData.packages[i].amount + '&apos;,&apos;' + jsonData.packages[i].short_detail.replace(/\n|\r/g, "") + '&apos;,&apos;' + jsonData.packages[i].product_code + '&apos;,&apos;' + jsonData.packages[i].description + '&apos;,&apos;' + jsonData.packages[i].prodcut_type + '&apos;,&apos;' + jsonData.packages[i].validity + '&apos;)">Details</a></td></tr>';
                                                    }
                                                }
                                                str += '</tbody></table>';
                                                $("#uldatapack").append(str);
                                                $("#ulvoicepack").html("");
                                                str = "";
                                                str = '<table><tbody>';
                                                for (var i = 0; i < jsonData.packages.length; ++i) {
                                                    if (jsonData.packages[i].product_type != "Data Pack") {
                                                        str += '<tr><td>' + jsonData.packages[i].product_name + '</td>';
                                                        str += '<td>' + jsonData.packages[i].amount + '</td>';
                                                        str += '<td><a href="javascript:void(0);" class="btn btn-primary" onclick="Buy(&apos;' + jsonData.packages[i].product_name + '&apos;,&apos;' + jsonData.packages[i].amount + '&apos;,&apos;' + jsonData.packages[i].package_id + '&apos;,&apos;' + i + '&apos;);">Buy</a></td>';
                                                        str += '<td><a href="javascript:void(0);" class="btn btn-primary" onclick="PackageDetails(&apos;' + jsonData.packages[i].priority_no + '&apos;,&apos;' + jsonData.packages[i].product_name + '&apos;,&apos;' + jsonData.packages[i].amount + '&apos;,&apos;' + jsonData.packages[i].short_detail + '&apos;,&apos;' + jsonData.packages[i].product_code + '&apos;,&apos;' + jsonData.packages[i].description + '&apos;,&apos;' + jsonData.packages[i].prodcut_type + '&apos;,&apos;' + jsonData.packages[i].validity + '&apos;)">Details</a></td></tr>';
                                                    }
                                                }
                                                str += '</tbody></table>';
                                                $("#ulvoicepack").append(str);
                                                //str += "<table><tbody><tr><td>" + jsonData.packages[i].product_name + "</td>";
                                                //str += "<td>" + jsonData.packages[i].amount + "</td>";
                                                //str += "<td><a href='javascript:void(0);' class='btn btn-primary' onclick='Buy(&apos;" + jsonData.packages[i].product_code + "&apos;,&apos;" + jsonData.packages[i].amount + "&apos;,&apos;&apos;,&apos;" + i + "&apos;);'>Buy</a></td></tr></tbody></table>";
                                                ////$("#tblDataPackage ").append($("<option></option>").val(jsonData.detail[i].value + "`" + jsonData.detail[i].amount).html(jsonData.detail[i].name));
                                            }

                                            //$("#tblDataPackage tbody").append(str);
                                            //$("#Amount").val($("#ddlSubscription").val().split('`')[1]);
                                            $('#AjaxLoader').hide();
                                            return false;
                                        }

                                        //$("#spnDueAmount").html($("#Amount").val());
                                        //$("#lblAmount").text($("#Amount").val());
                                        $("#dvMessage").html("");
                                        $("#txnMsg").html("");

                                        $('#AjaxLoader').hide();
                                        return false;
                                    }
                                    else {
                                        $('#AjaxLoader').hide();
                                        $("#dvMessage").html("Invalid Credentials");
                                    }
                                }
                                else if (response == "ServiceDown") {
                                    $('#AjaxLoader').hide();
                                    $("#DivDataPackStep2").hide();
                                    $("#DivDataPack").show();
                                    window.location.href = "/MyPayUser/MyPayUserServiceDown";
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
//$("#ddlSubscription").click(function () {
//    $("#Amount").val($("#ddlSubscription").val().split('`')[1]);
//});
$("#btnPackageBack").click(function () {
    $("#DivDataPackStep2").hide();
    $("#DivDataPackStep3").hide();
    $("#DivDataPack").show();
    $("#DivProceedToPay").hide();
});
$("#btnPayBack").click(function () {
    $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
    $("#DivDataPackStep2").show();
    $("#DivDataPackStep3").hide();
    $("#DivDataPack").hide();
    $("#DivProceedToPay").hide();
});

$("#btnPay").click(function () {


    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = $("#MobileNumber").val();
    if (Number == "") {
        $("#dvMessage").html("Please enter Number");
        return false;
    }

    if (Number.length != 10) {
        $("#dvMessage").html("Please enter  Valid Number");
        return false;
    }

    var Amount = $("#Amount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
    }


    var IsValid = false;
    var regex1 = /^([9][8][5][0-9]{7})$/;
    var regex2 = /^([9][7][4-5][0-9]{7})$/;
    var regex3 = /^([9][8][46][0-9]{7})$/;
    var regex4 = /^([9][8][0-2][0-9]{7})$/;
    //Ncell Validation
    if (ServiceID == 42) {
        if (Number.match(regex4)) {
            IsValid = true;
        }
    }

    //Ntc Validation
    else {
        if (Number.match(regex1)) {
            IsValid = true;
        }
        else if (Number.match(regex2)) {
            IsValid = true;
        }
        else if (Number.match(regex3)) {
            IsValid = true;
        }
        else if (Number.substring(0, 3) == "976") {
            IsValid = true;
        }
    }

    if (!IsValid) {
        $("#dvMessage").html("Please enter valid Number");
        return;
    }
    $("#DivDataPack").hide();
    $("#DivDataPackStep2").hide();
    $("#DivDataPackStep3").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    $("#lblAmount").text($("#Amount").val());
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
        $("#CouponDiscountPopup").text(parseFloat(discount));
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

    if (ServiceId == "61") {
        if (SelectedPack.priority_no == 0) {
            ServiceId = "63";
            $("#hdnServiceID").val("63");
        }
        else if (SelectedPack.priority_no == 1) {
            ServiceId = "61";
            $("#hdnServiceID").val("61");
        }
        else if (SelectedPack.priority_no == 2) {
            ServiceId = "62";
            $("#hdnServiceID").val("62");
        }
    }
    var PaymentMode = $("#hfPaymentMode").val();
    var Amount = $("#Amount").val();
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
                        $("#MobilePopup").text($("#MobileNumber").val());
                        //$("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        var smartPayCoin = parseFloat($("#smartpayCoins").text());
                        $('#DivCoin').show();
                        if (smartPayCoin <= 0) {
                            $('#DivCoin').hide();
                        }

                        $("#TxnAmountPopup").text(arr['Amount']);
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#MypayCoinCreditedPopup").text(arr['RewardPoints']);
                        $("#MypayCoinDebitedPopup").closest('tr').hide();
                        $("#MobilePopup").text($("#MobileNumber").val());
                        $("#MobilePopup").closest("tr").show();
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
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var PackageId = $("#hdnPackageID").val();
    var ProductCode = $("#hdnProductCode").val();
    if (ProductCode == "") {
        $("#dvMessage").html("Please select DataPackage");
        return false;
    }
    var CustomerID = $("#MobileNumber").val();
    if (CustomerID == "") {
        $("#dvMessage").html("Please enter MobileNumber");
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

    var VendorJsonLookup = JSON.stringify(SelectedPack);
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
                    url: "/MyPayUser/MyPayUserDataPack",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","MobileNumber":"' + CustomerID + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","ProductCode":"' + ProductCode + '","PackageId":"' + PackageId + '","VendorJsonLookup":"' + VendorJsonLookup.replaceAll("\"", "'") + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
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

});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});

function Buy(productcode, amount, packageid, index) {

    $("#hdnPackageID").val(packageid);
    $("#hdnProductCode").val(productcode);
    $("#Amount").val(amount);
    $("#spnDueAmount").html(amount);
    $("#DivDataPack").hide();
    $("#DivDataPackStep2").hide();
    $("#DivDataPackStep3").show();
    $("#DivProceedToPay").hide();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
    SelectedPack = DataPacks.packages[index];

}

function PackageDetails(priority_no, product_name, amount, short_detail, product_code, description, prodcut_type, validity) {

    $("#PackagedetailsPopup").modal();
    $("#Priority_NoPopup").html(priority_no);
    $("#Product_NametPopup").html(product_name);
    $("#Amountdata").html(amount);
    $("#Short_DetailsPopup").html(short_detail);
    $("#Product_CodePopup").html(product_code);
    $("#DescriptionPopup").html(description);
    $("#Product_TypePopup").html(prodcut_type);
    $("#ValidityPopup").html(validity);
}