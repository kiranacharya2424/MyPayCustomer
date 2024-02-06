var WalletResponse = '';

$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            InsuranceLoad();
        }, 10);
    $("#DivWallet").trigger("click");
});
function InsuranceLoad() {
    $('#AjaxLoader').show();
    $("#DivInsurance").show();
    $("#DivProceedToPay").hide();
    $("#DivPin").hide();
    $('#AjaxLoader').hide();
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    $("html, body").animate({ scrollTop: "0" });
}

function showInsuranceDetails(objID, providername) {


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
                $("#dvMessage").html("");
                $("html, body").animate({ scrollTop: "0" });
                $("#dvInputDetails").hide();
                $("#dvInputDetails").html("");
                if (objID == "45") {
                    $("#dvSelect").show();
                    $("#DivStep1").show();
                    $("#DivStep2").hide();
                    $("#dvDebitNoteInputDetails").hide();
                    $("#dvPolicyInputDetails").hide();
                }
                else if (objID == "58") {
                    $("#dvDebitNoteInputDetails").show();
                    $("#dvPolicyInputDetails").hide();
                    $("#DivStep1").hide();
                    $("#DivStep2").show();

                }
                else if (objID == "68") {
                    $("#dvDebitNoteInputDetails").hide();
                    $("#dvPolicyInputDetails").show();
                    $("#DivStep1").hide();
                    $("#DivStep2").show();
                }
                else if (objID == "69" || objID == "75" || objID == "106" || objID == "107" || objID == "108") {
                    $("#PolicyNumber").val("");
                    $("#DateofBirth").val("");
                    $("#dvDebitNoteInputDetails").hide();
                    $("#dvPolicyInputDetails").show();
                    $("#DivStep1").hide();
                    $("#DivStep2").show();
                }
                else if (objID == "78") {
                    var InsuranceType = "";
                    $('#AjaxLoader').show();
                    setTimeout(
                        function () {
                            $.ajax({
                                type: "POST",
                                url: "/MyPayUser/MyPayUserInsuranceDetails",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: '{"ServiceId":"' + objID + '","InsuranceType":"' + InsuranceType + '"}',
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
                                    var str = "";
                                    $("#dvInputDetails").html("");
                                    if (IsValidJson) {
                                        if (arr['Message'] == "success") {
                                            if (objID == "78") {
                                                if (jsonData != null && jsonData.shikhardetail != null) {
                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Customer Name</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="CustomerName" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Customer Name">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Mobile Number</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="MobileNumber" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Mobile Number" onkeypress="return isNumberKey(this, event);">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Email</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="Email" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Email">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Policy Type</label >';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<select id="ddlPolicyType" class="form-control form-control-lg">';
                                                    str += '<option value="0">Select Policy Type</option>';
                                                    str += '<option value="Fresh">Fresh</option>';
                                                    str += '<option value="Renew">Renew</option></select>';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Branch</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="Branch" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Branch">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Policy Number</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="PolicyNumber" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Policy Number">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Policy Category</label >';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<select id="ddlPolicyName" class="form-control form-control-lg">';
                                                    str += '<option value="0">Select Policy Name</option>';
                                                    for (var i = 0; i < jsonData.shikhardetail.policies.length; ++i) {
                                                        str += '<option value="' + jsonData.shikhardetail.policies[i].value + '">' + jsonData.shikhardetail.policies[i].label + '</option>';
                                                    }
                                                    str += '</select>';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Address</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="Address" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Address">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Policy Description</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="PolicyDescription" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Policy Description">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                    str += '<div class="form-group">';
                                                    str += '<label class="form-label text-soft">Amount (Rs)</label>';
                                                    str += '<div class="form-control-wrap">';
                                                    str += '<div class="input-group">';
                                                    str += '<input type="text" id="Amount" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Amount" onkeypress="return isNumberKey(this, event);">';
                                                    str += '</div>';
                                                    str += '</div>';
                                                    str += '</div>';

                                                }
                                            }
                                            $("#dvInputDetails").append(str);
                                            $("#dvInputDetails").show();
                                            $("#dvPolicyInputDetails").hide();
                                            $("#dvDebitNoteInputDetails").hide();
                                            $("#DivStep1").hide();
                                            $("#DivStep2").show();
                                            $('#AjaxLoader').hide();
                                            return false;
                                        }
                                        else {
                                            $('#AjaxLoader').hide();
                                            $("#dvMessage").html(arr['message']);
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
                else if (objID == "79") {
                    var str = "";
                    $("#dvInputDetails").html("");
                    str += '<div class="form-group">';
                    str += '<label class="form-label text-soft">Acceptance No</label>';
                    str += '<div class="form-control-wrap">';
                    str += '<div class="input-group">';
                    str += '<input type="text" id="AcceptanceNo" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Acceptance No">';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';

                    $("#dvInputDetails").append(str);
                    $("#dvInputDetails").show();
                    $("#dvPolicyInputDetails").hide();
                    $("#dvDebitNoteInputDetails").hide();
                    $("#DivStep1").hide();
                    $("#DivStep2").show();

                }

                $("#DivDetail").hide();
                $("#DivInsurance").hide();
                $("#DivProceedToPay").hide();
                $("#hdnServiceID").val(objID);
                $("#hdheading").html(providername);
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
    $("#DivStep1").hide();
    $("#DivStep2").show();
    $("#DivInsurance").hide();
    $("#DivDetail").hide();
    $("#DivProceedToPay").hide();
    $("#dvMessage").html("");
});

$("#btnPayBack").click(function () {
    $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });
    $("#DivStep1").hide();
    $("#DivStep2").hide();
    $("#DivInsurance").show();
    $("#DivProceedToPay").hide();
    $("#DivDetail").hide();
    $("#dvMessage").html("");
});

$("#btnPayBackStep1").click(function () {
    $("#DivStep1").hide();
    $("#DivStep2").hide();
    $("#DivInsurance").show();
    $("#DivProceedToPay").hide();
    $("#DivDetail").hide();
    $("#dvMessage").html("");
});

$("#btnPay").click(function () {
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = $("#MobileNumber").val();
    if (Number == "") {
        $("#dvMessage").html("Please enter Mobile Number");
        return false;
    }
    var Amount = $("#hdnAmount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please select Amount");
        return false;
    }
    $("#DivInsurance").hide();
    $("#DivStep2").hide();
    $("#DivStep1").hide();
    $("#DivDetail").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
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
        $('#PaymentPopup').modal('show');
        var ServiceId = $("#hdnServiceID").val();
        if (ServiceId == "68" || ServiceId == "69" || ServiceId == "75" || ServiceId == "78" || ServiceId == "106" || ServiceId == "107" || ServiceId == "108") {
            $("#PolicyPopup").text($("#hdnPolicyNumber").val());
            $("#PolicyPopup").closest("tr").show();
            $("#MobilePopup").closest("tr").hide();
            $("#CustomerNamePopup").closest("tr").hide();
            $("#PolicyCategoryPopup").closest("tr").hide();
            $("#PolicyTypePopup").closest("tr").hide();
        }
        if (ServiceId == "79") {
            $("#ProformaPopup").text($("#hdnPolicyNumber").val());
            $("#ProformaPopup").closest("tr").show();
            $("#PolicyPopup").closest("tr").hide();
            $("#MobilePopup").closest("tr").hide();
            $("#CustomerNamePopup").closest("tr").hide();
            $("#PolicyCategoryPopup").closest("tr").hide();
            $("#PolicyTypePopup").closest("tr").hide();
        }
        if (ServiceId == "45") {
            $("#CustomerNamePopup").text($("#CustomerName").val());
            $("#PolicyCategoryPopup").text($("#ddlPolicyCategory :selected").val());
            $("#PolicyTypePopup").text($("#ddlPolicyType :selected").val());
            $("#CustomerNamePopup").closest("tr").show();
            $("#PolicyCategoryPopup").closest("tr").show();
            $("#PolicyTypePopup").closest("tr").show();
            $("#PolicyPopup").closest("tr").hide();
            $("#ProformaPopup").closest("tr").hide();
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
    var Amount = "";
    Amount = $("#hdnAmount").val();

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
                    if (IsValidJson) {
                        var arr = jsonData;
                        if (arr['Message'] == "success") {
                            $("#MobilePopup").text($("#hdnMobileNumber").val());
                            $("#AmountPopup").text(arr['Amount']);
                            $("#CashbackPopup").text(arr['CashbackAmount']);
                            $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                            $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                            $("#smartpayCoins").text(arr['MPCoinsDebit']);
                            $("#lblCashback").text(arr['CashbackAmount']);
                            $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
                            $("#TxnAmountPopup").text(arr['Amount']);
                            $("#lblAmount").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            $("#TotalAmountPopup").text(parseFloat(Amount + ServiceCharge).toFixed(2));
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
                            $("#dvMessage").html(arr['message']);
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

function Pay() {

    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var PolicyCategory = "";
    var PolicyType = "";
    var CustomerName = "";
    var PolicyNumber = "";
    var InsuranceType = "";
    var SessionId = "";
    var TxnId = "";
    var Address = "";
    var Branch = "";
    var PolicyName = "";
    var Email = "";
    var PolicyDescription = "";
    var RequestId = "";
    if (ServiceID == "45") {
        PolicyCategory = $("#ddlPolicyCategory :selected").val();
        if (PolicyCategory == "" || PolicyCategory == "0") {
            $("#dvMessage").html("Please select Policy Category");
            return false;
        }
        PolicyType = $("#ddlPolicyType :selected").val();
        if (PolicyType == "" || PolicyType == "0") {
            $("#dvMessage").html("Please select Policy Type");
            return false;
        }
        CustomerName = $("#CustomerName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter Customer Name");
            return false;
        }
        PolicyNumber = $("#PolicyNumber").val();
        InsuranceType = $("#ddlInsuranceType :selected").val();
        if (InsuranceType == "" || InsuranceType == "0") {
            $("#dvMessage").html("Please select Insurance");
            return false;
        }
    }
    if (ServiceID == "78") {
        PolicyName = $("#ddlPolicyName :selected").val();
        if (PolicyName == "" || PolicyName == "0") {
            $("#dvMessage").html("Please select Policy Name");
            return false;
        }
        PolicyType = $("#ddlPolicyType :selected").val();
        if (PolicyType == "" || PolicyType == "0") {
            $("#dvMessage").html("Please select Policy Type");
            return false;
        }
        CustomerName = $("#CustomerName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter Customer Name");
            return false;
        }
        PolicyNumber = $("#PolicyNumber").val();
        if (PolicyNumber == "") {
            $("#dvMessage").html("Please enter Policy Number");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }

        MobileNumber = $("#MobileNumber").val();
        if (MobileNumber == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }
        else {
            $("#hdnMobileNumber").val(MobileNumber);
        }
        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else {
            $("#hdnAmount").val(Amount);
        }
        Branch = $("#Branch").val();
        if (Branch == "") {
            $("#dvMessage").html("Please enter Branch");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
        Address = $("#Address").val();
        if (Address == "") {
            $("#dvMessage").html("Please enter Address");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
        PolicyDescription = $("#PolicyDescription").val();
        if (PolicyDescription == "") {
            $("#dvMessage").html("Please enter Policy Description");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
        Email = $("#Email").val();
        if (Email == "") {
            $("#dvMessage").html("Please enter Email");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
    }
    var MobileNumber = $("#hdnMobileNumber").val();

    var Amount = $("#hdnAmount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
    }
    if (ServiceID == "79") {
        RequestId = $("#AcceptanceNo").val();
        if (RequestId == "") {
            $("#dvMessage").html("Please enter Acceptance No");
            return false;
        }
    }

    if (ServiceID == "58") {
        SessionId = $("#hdnSessionId").val();
        if (SessionId == "") {
            $("#dvMessage").html("Please enter SessionId");
            return false;
        }
    }
    if (ServiceID == "68") {
        TxnId = $("#hdnTxnId").val();
        if (TxnId == "") {
            $("#dvMessage").html("Please enter TxnId");
            return false;
        }
        PolicyNumber = $("#PolicyNumber").val();
        if (PolicyNumber == "") {
            $("#dvMessage").html("Please enter Policy Number");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
    }
    if (ServiceID == "69" || ServiceID == "75" || ServiceID == "106" || ServiceID == "107" || ServiceID == "108") {
        SessionId = $("#hdnSessionId").val();
        if (SessionId == "") {
            $("#dvMessage").html("Please enter SessionId");
            return false;
        }

        InsuranceType = "jyoti-life-insurance";
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
                    url: "/MyPayUser/MyPayUserInsurance",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","PolicyCategory":"' + PolicyCategory + '","PolicyType":"' + PolicyType + '","CustomerName":"' + CustomerName + '","MobileNumber":"' + MobileNumber + '","PolicyNumber":"' + PolicyNumber + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","InsuranceType":"' + InsuranceType + '","SessionId":"' + SessionId + '","TxnId":"' + TxnId + '","RequestId":"' + RequestId + '","Address":"' + Address + '","PolicyName":"' + PolicyName + '","Email":"' + Email + '","Branch":"' + Branch + '","PolicyDescription":"' + PolicyDescription + '","BankId":"' + $("#hfBankId").val() + '"}',
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

$("#btnGetDetail").click(function () {
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var InsuranceType = $("#ddlInsuranceType :selected").val();
    if (InsuranceType == "" || InsuranceType == "0") {
        $("#dvMessage").html("Please select Insurance");
        return false;
    }
    $("#DivInsurance").hide();
    $("#DivStep2").show();
    $("#DivStep1").hide();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserInsuranceDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: '{"ServiceId":"' + ServiceID + '","InsuranceType":"' + InsuranceType + '"}',
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
                    var str = "";
                    $("#dvInputDetails").html("");
                    if (IsValidJson) {
                        if (arr['Message'] == "success") {
                            if (ServiceID == "45") {
                                if (jsonData != null && jsonData.PolicyCategories != null) {
                                    str += '<div class="form-group">';
                                    str += '<label class="form-label text-soft">Policy Category</label >';
                                    str += '<div class="form-control-wrap">';
                                    str += '<div class="input-group">';
                                    str += '<select id="ddlPolicyCategory" class="form-control form-control-lg">';
                                    str += '<option value="0">Select Policy Category</option>';
                                    for (var i = 0; i < jsonData.PolicyCategories.length; ++i) {
                                        str += '<option value="' + jsonData.PolicyCategories[i] + '">' + jsonData.PolicyCategories[i] + '</option>';
                                    }
                                    str += '</select>';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';

                                    str += '<div class="form-group">';
                                    str += '<label class="form-label text-soft">Policy Type</label >';
                                    str += '<div class="form-control-wrap">';
                                    str += '<div class="input-group">';
                                    str += '<select id="ddlPolicyType" class="form-control form-control-lg">';
                                    str += '<option value="0">Select Policy Type</option>';
                                    str += '<option value="Fresh">Fresh</option>';
                                    str += '<option value="Renew">Renew</option></select>';
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
                                    str += '<label class="form-label text-soft">Policy Number</label>';
                                    str += '<div class="form-control-wrap">';
                                    str += '<div class="input-group">';
                                    str += '<input type="text" id="PolicyNumber" class="form-control form-control-lg fw-medium" autocomplete="off" placeholder="Policy Number" onkeypress="return isNumberKey(this, event);">';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';

                                    str += '<div class="form-group">';
                                    str += '<label class="form-label text-soft">Mobile Number</label>';
                                    str += '<div class="form-control-wrap">';
                                    str += '<div class="input-group">';
                                    str += '<input type="text" id="MobileNumber" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Mobile Number" onkeypress="return isNumberKey(this, event);">';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';

                                    str += '<div class="form-group">';
                                    str += '<label class="form-label text-soft">Amount (Rs)</label>';
                                    str += '<div class="form-control-wrap">';
                                    str += '<div class="input-group">';
                                    str += '<input type="text" id="Amount" class="form-control form-control-lg fw-medium" maxlength="10" autocomplete="off" placeholder="Amount" onkeypress="return isNumberKey(this, event);">';
                                    str += '</div>';
                                    str += '</div>';
                                    str += '</div>';

                                }
                            }
                            $("#dvInputDetails").append(str);
                            $("#dvInputDetails").show();
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html("");
                            $("#txnMsg").html("");
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(arr['message']);
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
    $("#DivDetail").hide();
    $("#DivProceedToPay").hide();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
});

$("#btnGetInsuranceDetail").click(function () {
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select Insurance");
        return false;
    }
    var Amount = "";
    var PolicyCategory = "";
    var PolicyType = "";
    var CustomerName = "";
    var PolicyNumber = "";
    var MobileNumber = "";
    var DateofBirth = "";
    var DebitNote = "";
    var RequestId = "";
    var Branch = "";
    var PolicyName = "";
    var Email = "";
    var PolicyDescription = "";

    if (ServiceID == "45") {
        PolicyCategory = $("#ddlPolicyCategory :selected").val();
        if (PolicyCategory == "" || PolicyCategory == "0") {
            $("#dvMessage").html("Please select Policy Category");
            return false;
        }
        PolicyType = $("#ddlPolicyType :selected").val();
        if (PolicyType == "" || PolicyType == "0") {
            $("#dvMessage").html("Please select Policy Type");
            return false;
        }
        CustomerName = $("#CustomerName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter Customer Name");
            return false;
        }
        PolicyNumber = $("#PolicyNumber").val();

        MobileNumber = $("#MobileNumber").val();
        if (MobileNumber == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }
        else {
            $("#hdnMobileNumber").val(MobileNumber);
        }
        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else {
            $("#hdnAmount").val(Amount);
        }
    }
    if (ServiceID == "58") {
        DebitNote = $("#DebitNote").val();
    }
    if (ServiceID == "68" || ServiceID == "69" || ServiceID == "75" || ServiceID == "106" || ServiceID == "107" || ServiceID == "108") {
        PolicyNumber = $("#PolicyNumber").val();
        if (PolicyNumber == "") {
            $("#dvMessage").html("Please enter Policy Number");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }

        DateofBirth = $("#DateofBirth").val();
        if (DateofBirth == "") {
            $("#dvMessage").html("Please select your Date of Birth");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
    }
    if (ServiceID == "78") {
        PolicyName = $("#ddlPolicyName :selected").val();
        if (PolicyName == "" || PolicyName == "0") {
            $("#dvMessage").html("Please select Policy Name");
            return false;
        }
        PolicyType = $("#ddlPolicyType :selected").val();
        if (PolicyType == "" || PolicyType == "0") {
            $("#dvMessage").html("Please select Policy Type");
            return false;
        }
        CustomerName = $("#CustomerName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter Customer Name");
            return false;
        }
        PolicyNumber = $("#PolicyNumber").val();
        if (PolicyNumber == "") {
            $("#dvMessage").html("Please enter Policy Number");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }

        MobileNumber = $("#MobileNumber").val();
        if (MobileNumber == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }
        else {
            $("#hdnMobileNumber").val(MobileNumber);
        }
        Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        else {
            $("#hdnAmount").val(Amount);
        }
        Branch = $("#Branch").val();
        if (Branch == "") {
            $("#dvMessage").html("Please enter Branch");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
        PolicyDescription = $("#PolicyDescription").val();
        if (PolicyDescription == "") {
            $("#dvMessage").html("Please enter Policy Description");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
        Email = $("#Email").val();
        if (Email == "") {
            $("#dvMessage").html("Please enter Email");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
    }
    if (ServiceID == "79") {
        RequestId = $("#AcceptanceNo").val();
        if (RequestId == "" || RequestId == "0") {
            $("#dvMessage").html("Please enter Acceptance No");
            return false;
        }
        else {
            $("#dvMessage").html("");
        }
    }

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#tblInsuranceDetail").html("");
            var str = "";
            if (ServiceID == "45") {
                str += '<div class="card mt-4">';
                str += '<div class="card-inner p-0">';
                str += '<div class="row">';
                str += '<div class="col-md-6 mb-3">';
                str += '<label class="mb-0 text-soft w-100">Policy Category</label>';
                str += '<span class="fw-medium">' + PolicyCategory + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3 text-sm-right">';
                str += '<label class="mb-0 text-soft w-100">Policy Type</label>';
                str += '<span class="fw-medium">' + PolicyType + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3">';
                str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                str += '<span class="fw-medium">' + CustomerName + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3 text-sm-right">';
                str += '<label class="mb-0 text-soft w-100">Mobile Number</label>';
                str += '<span class="fw-medium">' + MobileNumber + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3">';
                str += '<label class="mb-0 text-soft w-100">Amount</label>';
                str += '<span class="fw-medium">' + Amount + '</span>';
                str += '</div>';
                if (PolicyNumber != "") {
                    str += '<div class="col-md-6 mb-3 text-sm-right">';
                    str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                    str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                    str += '</div>';
                }
                str += '</div>';
                str += '</div>';
                str += '</div>';
                $("#hdnAmount").val(Amount);
                $("#hdnPolicyNumber").val(PolicyNumber);
                $("#DivInsurance").hide();
                $("#DivStep2").hide();
                $("#DivStep1").hide();
                $("#tblInsuranceDetail").append(str);
                $("#lblAmount").text($("#hdnAmount").val());
                $("#lblAmountInsurance").text($("#hdnAmount").val());
                ServiceCharge();
                $("#DivDetail").show();
            }
            else if (ServiceID == "78") {
                str += '<div class="card mt-4">';
                str += '<div class="card-inner p-0">';
                str += '<div class="row">';
                str += '<div class="col-md-6 mb-3">';
                str += '<label class="mb-0 text-soft w-100">Name</label>';
                str += '<span class="fw-medium">' + CustomerName + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3 text-sm-right">';
                str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                str += '<span class="fw-medium">' + Amount + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3">';
                str += '<label class="mb-0 text-soft w-100">Branch</label>';
                str += '<span class="fw-medium">' + Branch + '</span>';
                str += '</div>';
                str += '<div class="col-md-6 mb-3 text-sm-right">';
                str += '<label class="mb-0 text-soft w-100">Policy Type</label>';
                str += '<span class="fw-medium">' + PolicyName + '</span>';
                str += '</div>';
                str += '</div>';
                str += '</div>';
                str += '</div>';
                $("#hdnAmount").val(Amount);
                $("#hdnPolicyNumber").val(PolicyNumber);
                $("#DivInsurance").hide();
                $("#DivStep2").hide();
                $("#DivStep1").hide();
                $("#tblInsuranceDetail").append(str);
                $("#lblAmount").text($("#hdnAmount").val());
                $("#lblAmountInsurance").text($("#hdnAmount").val());
                ServiceCharge();
                $("#DivDetail").show();
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserInsuranceDetails",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{"ServiceId":"' + ServiceID + '","InsuranceType":"","DebitNote":"' + DebitNote + '","PolicyNumber":"' + PolicyNumber + '","DOB":"' + DateofBirth + '","RequestId":"' + RequestId + '"}',
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
                                if (ServiceID == "58") {
                                    if (jsonData != null && jsonData.DebitNoteNo != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Debit Note No</label>';
                                        str += '<span class="fw-medium">' + jsonData.DebitNoteNo + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Contact Number</label>';
                                        str += '<span class="fw-medium">' + jsonData.ContactNo + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Address</label>';
                                        str += '<span class="fw-medium">' + jsonData.Address + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.PayableAmount);
                                        $("#hdnMobileNumber").val(jsonData.ContactNo);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                    }

                                }
                                else if (ServiceID == "68") {
                                        if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer ID</label>';
                                        str += '<span class="fw-medium">' + jsonData.CustomerId + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.CustomerName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Product Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.ProductName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Address</label>';
                                        str += '<span class="fw-medium">' + jsonData.Address + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Next Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.NextDueDate + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Fine Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.FineAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Amount</label>';
                                        str += '<span class="fw-medium">' + jsonData.Amount + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        var totalAmount = parseFloat(parseFloat(jsonData.Amount) + parseFloat(jsonData.FineAmount)).toFixed(2);
                                        $("#hdnAmount").val(totalAmount);
                                        //$("#hdnMobileNumber").val(jsonData.ContactNo);
                                        $("#hdnTxnId").val(jsonData.TransactionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "69") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.Name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.PremiumAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Fine Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.FineAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Totl Fine</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.TotalFine + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Rebate Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.RebateAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Payment Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.PaymentDate + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.DueDate + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Status</label>';
                                        str += '<span class="fw-medium">' + jsonData.PolicyStatus + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Maturity Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.MaturityDate + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.Amount);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "75") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.CustomerName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Next Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.DueDate + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Fine Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.FineAmount + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.Amount);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "106") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.Name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Product Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.ProductName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.PremiumAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Fine Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.FineAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Total Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.TotalAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Pay Mode</label>';
                                        str += '<span class="fw-medium">' + jsonData.Paymode + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Next Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.DueDate + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Current Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.CurrentDueDate + '</span>';
                                        str += '</div>';                                        
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.TotalAmount);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "107") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.Name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Product Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.ProductName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.PremiumAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Fine Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.FineAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Total Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.TotalAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Pay Mode</label>';
                                        str += '<span class="fw-medium">' + jsonData.Paymode + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Next Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.DueDate + '</span>';
                                        str += '</div>';
                                        //str += '<div class="col-md-6 mb-3">';
                                        //str += '<label class="mb-0 text-soft w-100">Current Due Date</label>';
                                        //str += '<span class="fw-medium">' + jsonData.CurrentDueDate + '</span>';
                                        //str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.TotalAmount);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "108") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.Name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Product Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.ProductName + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.PremiumAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Fine Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.FineAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Total Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.TotalAmount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Pay Mode</label>';
                                        str += '<span class="fw-medium">' + jsonData.Paymode + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Next Due Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.DueDate + '</span>';
                                        str += '</div>';
                                        //str += '<div class="col-md-6 mb-3">';
                                        //str += '<label class="mb-0 text-soft w-100">Current Due Date</label>';
                                        //str += '<span class="fw-medium">' + jsonData.CurrentDueDate + '</span>';
                                        //str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.TotalAmount);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "77") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Policy Number</label>';
                                        str += '<span class="fw-medium">' + PolicyNumber + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.detail.premium_amount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Product Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.detail.product_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Payment Mode</label>';
                                        str += '<span class="fw-medium">' + jsonData.detail.payment_mode + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.detail.assured_name + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.detail.premium_amount);
                                        $("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(PolicyNumber);
                                    }
                                }
                                else if (ServiceID == "79") {
                                    if (jsonData != null) {
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.Insured + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Premium Amount</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.TpPremium + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Sum Assured</label>';
                                        str += '<span class="fw-medium">रु ' + jsonData.SumInsured + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Class Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.ClassName + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#hdnAmount").val(jsonData.TpPremium);
                                        //$("#hdnSessionId").val(jsonData.SessionId);
                                        $("#hdnPolicyNumber").val(jsonData.ProformaNo);
                                    }
                                }
                                $("#DivInsurance").hide();
                                $("#DivStep2").hide();
                                $("#DivStep1").hide();
                                $("#tblInsuranceDetail").append(str);
                                $("#lblAmount").text($("#hdnAmount").val());
                                $("#lblAmountInsurance").text($("#hdnAmount").val());
                                ServiceCharge();
                                $("#DivDetail").show();
                                $('#AjaxLoader').hide();
                                return false;
                            }
                            else {
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("Invalid Credentials");
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
            }

            $('#AjaxLoader').hide();
            //$("#dvMessage").html("");
            //$("#txnMsg").html("");
            return false;
        }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);

    $("#DivProceedToPay").hide();
    //$("#dvMessage").html("");
    //$("#txnMsg").html("");
});


