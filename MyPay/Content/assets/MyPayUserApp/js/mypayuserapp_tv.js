var WalletResponse = '';
var discount = 0;
var payable = 0;

$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            TVLoad();
        }, 10);
    $("#DivWallet").trigger("click");
});
function TVLoad() {

    $('#AjaxLoader').show();
    $("#DivTV").show();
    $("#DivProceedToPay").hide();
    $("#DivPin").hide();
    $('#AjaxLoader').hide();
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    $("html, body").animate({ scrollTop: "0" });
}

function showTVDetails(objID, providername) {
    debugger;
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
                if (objID === 14 || objID === 15 || objID === 25 || objID === 26 || objID === 27) {
                    $('#AjaxLoader').show();
                    var url = "/MyPayUser/MyPayUserTVAmountDetails";
                    if (objID === 26 || objID === 27) {
                        url = "/MyPayUser/MyPayUserTVPackageDetails";
                    }
                    setTimeout(
                        function () {
                            $.ajax({
                                type: "POST",
                                url: url,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: '{"ServiceId":"' + objID + '"}',
                                async: false,
                                success: function (response) {

                                    if (response != "" || response != null) {
                                        setTvForm(objID, response);

                                        $('#AjaxLoader').hide();
                                        $("#dvMessage").html("");
                                        $("#txnMsg").html("");
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
                        }, 20);
                }
                else {
                    setTvForm(objID, "");
                }

                $("#DivTVStep2").show();
                $("#DivTVDetail").hide();
                $("#DivTV").hide();
                $("#DivProceedToPay").hide();
                $("#hdnServiceID").val(objID);
                $("#lblProviderName").html(providername + " TV");
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
function setTvForm(objID, response) {

    // Reset form
    $("#dv_DishHome").hide();
    $("#dv_Sim").hide();
    $("#dv_Mero").hide();
    $("#dv_Clear").hide();
    $("#dv_Max").hide();
    $("#dv_Prabhu").hide();
    $("#dv_PG").hide();
    $("#dv_Jagriti").hide();

    $("#dv_button").hide();

    $("#txt_Dish_Customer").val("");
    $("#txt_Sim_Customer").val("");
    $("#txt_Mero_Stb").val("");
    $("#txt_Clear_Subscriber").val("");
    $("#txt_Clear_Mobile_Number").val("");
    $("#txt_Clear_Amount").val("");
    $("#txt_Max_Customer").val("");
    $("#txt_Prabhu_Customer").val("");
    $("#txt_PG_UserName").val("");
    $("#txt_PG_Contact").val("");
    $("#txt_PG_FullName").val("");
    $("#txt_Jagriti_CASID").val("");
    $("#txt_Jagriti_CustomerID").val("");
    $("#txt_Jagriti_CustomerName").val("");
    $("#txt_Jagriti_Ward").val("");
    $("#txt_Jagriti_Contact").val("");


    //Set DISHHOME TV form
    if (objID == 14) {
        $("#dv_DishHome").show(200);
        $("#dv_button").show(200);
        var str = "";
        var arr = response[0].ValidAmount;
        arr = arr.split(",");
        if (arr.length > 0) {
            for (var i = 0; i < arr.length; ++i) {
                str += '<option value="' + arr[i] + '">' + arr[i] + '</option>';
            }
        }
        $("#ddl_Dish_Amount").html("");
        $("#ddl_Dish_Amount").append(str);
    }
    //Set SIM TV form
    else if (objID == 15) {
        $("#dv_Sim").show(200);
        $("#dv_button").show(200);
        var str = "";
        var arr = response[0].ValidAmount;
        arr = arr.split(",");
        if (arr.length > 0) {
            for (var i = 0; i < arr.length; ++i) {
                str += '<option value="' + arr[i] + '">' + arr[i] + '</option>';
            }
        }
        $("#ddl_Sim_Amount").html("");
        $("#ddl_Sim_Amount").append(str);
    }
    //Set MERO TV form
    else if (objID == 16) {
        $("#dv_Mero").show(200);
        $("#dv_button").show(200);
    }
    //Set CLEAR TV form
    else if (objID == 23) {
        $("#dv_Clear").show(200);
        $("#dv_button").show(200);
    }
    //Set MAX TV form
    else if (objID == 24) {
        $("#dv_Max").show(200);
        $("#dv_button").show(200);
    }
    //Set PRABHU TV form
    else if (objID == 25) {
        $("#dv_Prabhu").show(200);
        $("#dv_button").show(200);
        var str = "";
        var arr = response[0].ValidAmount;
        arr = arr.split(",");
        if (arr.length > 0) {
            for (var i = 0; i < arr.length; ++i) {
                str += '<option value="' + arr[i] + '">' + arr[i] + '</option>';
            }
        }
        $("#ddl_Prabhu_Amount").html("");
        $("#ddl_Prabhu_Amount").append(str);
    }
    //Set PG TV form
    else if (objID == 26) {
        $("#dv_PG").show(200);
        $("#dv_button").show(200);
        var jsonData;
        try {
            jsonData = $.parseJSON(response);
        }
        catch (err) {

        }
        var str = "";
        var arr = jsonData.png_network_tv_details.packages;
        if (arr.length > 0) {
            for (var i = 0; i < arr.length; ++i) {
                str += '<option value="' + arr[i].amount + '">' + arr[i].package_name + '-' + arr[i].amount + '</option>';
            }
        }
        $("#ddl_PG_Package").html("");
        $("#ddl_PG_Package").append(str);
    }
    //Set Jagriti TV form
    else if (objID == 27) {
        $("#dv_Jagriti").show(200);
        $("#dv_button").show(200);
        var jsonData;
        try {
            jsonData = $.parseJSON(response);
        }
        catch (err) {

        }
        var str = "";
        var arr = jsonData.jagriti_tv_details.packages;
        if (arr.length > 0) {
            for (var i = 0; i < arr.length; ++i) {
                str += '<option value="' + arr[i].amount + '">' + arr[i].package_name + '-' + arr[i].amount + '</option>';
            }
        }
        $("#ddl_Jagriti_Package").html("");
        $("#ddl_Jagriti_Package").append(str);
    }
}
$("#btnDetailBack").click(function () {
    $("html, body").animate({ scrollTop: "0" });
    $("#DivTVStep2").show();
    $("#DivTV").hide();
    $("#DivTVDetail").hide();
    $("#DivProceedToPay").hide();

});

$("#btnPayBack").click(function () {
    $("#dvMessage").html(""); $("html, body").animate({ scrollTop: "0" });

    $("#dv_DishHome").hide();
    $("#dv_Sim").hide();
    $("#dv_Mero").hide();
    $("#dv_Clear").hide();
    $("#dv_Max").hide();
    $("#dv_Prabhu").hide();
    $("#dv_PG").hide();
    $("#dv_Jagriti").hide();

    $("#DivTVStep2").hide();
    $("#DivTV").show();
    $("#DivProceedToPay").hide();
    $("#DivTVDetail").hide();
    $("#dv_button").hide();
    $("#lblProviderName").html("TV");
});

$("#btnPay").click(function () {
    debugger;
    $('#btnapplycoupon').show();
    $('#btnremovecoupon').hide();
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = "";
    var Amount = "";
    /* DISHHOME TV */
    if (ServiceID == "14") {
        Number = $("#txt_Dish_Customer").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
            return false;
        }

        Amount = $("#ddl_Dish_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }


    }
    /* SIM TV */
    else if (ServiceID == "15") {
        Number = $("#txt_Sim_Customer").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Customer ID/Chip ID");
            return false;
        }

        Amount = $("#ddl_Sim_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }


    }
    /* MERO TV */
    else if (ServiceID == "16") {
        Number = $("#Account").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
            return false;
        }

        var selectedAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
            Amount = selectedAmount[1];
            $("#lblAmountTV").html(selectedAmount[1]);
            $("#Amount").val(selectedAmount[1]);
        }
        $("#hdnPackageId").val($("#ddlPackage :selected").val());
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
    }


    $("#DivTV").hide();
    $("#DivTVStep2").hide();
    $("#DivTVDetail").hide();
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    if (ServiceID == "16") {
        var selectedAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
            Amount = selectedAmount[1];
            $("#lblPayAmount").text(selectedAmount[1]);
        }
    }
    else {
        $("#lblPayAmount").text($("#ddlAmount :selected").val());
    }

    $("#DivProceedToPay").show();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
    $('#DivWallet')[0].click();
});

$("#btnProceedToPay").click(function () {
    debugger;
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
        var ServiceID = $("#hdnServiceID").val();
        $("#MobilePopup").closest("tr").hide();
        var CustomerID = "";
        /* DISHHOME TV */
        if (ServiceID == "14") {
            CustomerID = $("#txt_Dish_Customer").val();
            $("#CouponDiscountPopup").text(discount);

        }
        /* SIM TV */
        else if (ServiceID == "15") {
            CustomerID = $("#txt_Sim_Customer").val();
            $("#CouponDiscountPopup").text(discount);
        }
        /* MERO TV */
        else if (ServiceID == "16") {
            CustomerID = $("#hdnSessionId").val();
            $("#CouponDiscountPopup").text(discount);
        }
        /* CLEAR TV*/
        else if (ServiceID == "23") {
            CustomerID = $("#txt_Clear_Subscriber").val();
            $("#MobilePopup").text($("#txt_Clear_Mobile_Number").val());
            $("#MobilePopup").closest("tr").show();
            $("#CouponDiscountPopup").text(discount);
        }
        else if (ServiceID == "24") {
            CustomerID = $("#txt_Max_Customer").val();
            $("#CouponDiscountPopup").text(discount);
        }
        else if (ServiceID == "25") {
            CustomerID = $("#txt_Prabhu_Customer").val();
            $("#CouponDiscountPopup").text(discount);
        }
        /* PG TV*/
        else if (ServiceID == "26") {
            CustomerID = $("#txt_PG_FullName").val();
            $("#MobilePopup").text($("#txt_PG_Contact").val());
            $("#MobilePopup").closest("tr").show();
            $("#CouponDiscountPopup").text(discount);
        }
        /*Jagriti TV*/
        else if (ServiceID == "27") {
            CustomerID = $("#txt_Jagriti_CustomerID").val();
            $("#MobilePopup").text($("#txt_Jagriti_Contact").val());
            $("#MobilePopup").closest("tr").show();
            $("#CouponDiscountPopup").text(discount);
        }
        //$("#CustomerIdPopup").text(CustomerID);
        /*  $("#CustomerIdPopup").closest("tr").show();*/
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

    var ServiceID = $("#hdnServiceID").val();
    var Amount = "";

    /* DISHHOME TV */
    if (ServiceID == "14") {
        Amount = $("#ddl_Dish_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }
    }
    /* SIM TV */
    else if (ServiceID == "15") {
        Amount = $("#ddl_Sim_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }
    }
    /* MERO TV */
    else if (ServiceID == "16") {
        var selectedAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
            $("#lblAmountTV").html(selectedAmount[1]);
            $("#Amount").html(selectedAmount[1]);
        }
        Amount = selectedAmount[1];
    }
    /* CLEAR TV*/
    else if (ServiceID == "23") {
        Amount = $("#txt_Clear_Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
    }
    /* Max TV*/
    else if (ServiceID == "24") {
        Amount = $("#hdnMaxTvAmount").val();
    }
    /* Prabhu TV*/
    else if (ServiceID == "25") {
        Amount = $("#ddl_Prabhu_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
    }
    /* P&G TV*/
    else if (ServiceID == "26") {
        Amount = $("#ddl_PG_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Package");
            return false;
        }

    }
    /* Jagriti TV*/
    else if (ServiceID == "27") {
        Amount = $("#ddl_Jagriti_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please Select Package");
            return false;
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }
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
                data: '{"Amount":"' + Amount + '","ServiceId":"' + parseInt(ServiceID) + '"}',
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
                        $("#MobilePopup").text($("#Account").val());
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
                            var Totalamount = (DeductAmount + DeductServiceCharge) - discount;
                            $("#TotalAmountPopup").text(parseFloat(Totalamount).toFixed(2));
                        }
                        else {
                            $("#AmountPopup").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var Totalamount = (Amount + ServiceCharge) - discount;
                            $("#TotalAmountPopup").text(parseFloat(Totalamount).toFixed(2));

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
    debugger;
    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var CustomerID = "";
    var SessionId = "";
    var PackageId = "";
    var UserName = "";
    var ContactNumber = "";
    var Amount = "";
    var Ward = "";
    var STD_ID = "";
    /* DISHHOME TV */
    if (ServiceID == "14") {
        CustomerID = $("#txt_Dish_Customer").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
            return false;
        }

        Amount = $("#ddl_Dish_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }
    }
    /* SIM TV */
    else if (ServiceID == "15") {
        CustomerID = $("#txt_Sim_Customer").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter Customer ID/Chip ID");
            return false;
        }

        Amount = $("#ddl_Sim_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }


    }
    /* MERO TV */
    else if (ServiceID == "16") {
        var selectedAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
            Amount = selectedAmount[1];
            $("#lblAmountTV").html(selectedAmount[1]);
            $("#Amount").html(selectedAmount[1]);
        }
        $("#hdnPackageId").val($("#ddlPackage :selected").val());
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        SessionId = $("#hdnSessionId").val();
        if (SessionId == "") {
            $("#dvMessage").html("Please enter SessionId");
            return false;
        }
        PackageId = $("#hdnPackageId").val();
        if (PackageId == "" || PackageId == "0") {
            $("#dvMessage").html("Please select a valid Package");
            return false;
        }
    }
    /* CLEAR TV*/
    else if (ServiceID == "23") {
        CustomerID = $("#txt_Clear_Subscriber").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter Subscriber Id");
            return false;
        }

        Amount = $("#txt_Clear_Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        PackageId = $("#txt_Clear_Mobile_Number").val();
        if (PackageId == "" || PackageId == "0") {
            $("#dvMessage").html("Please enter mobile number");
            return false;
        }
    }
    else if (ServiceID == "24") {
        CustomerID = $("#txt_Max_Customer").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter Customer Id");
            return false;
        }

        Amount = $("#hdnMaxTvAmount").val();
        SessionId = $("#hdnSessionId").val();
        if (SessionId == "") {
            $("#dvMessage").html("Please enter SessionId");
            return false;
        }
    }
    else if (ServiceID == "25") {
        CustomerID = $("#txt_Prabhu_Customer").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter Customer Id");
            return false;
        }

        Amount = $("#ddl_Prabhu_Amount :selected").val();
        SessionId = $("#hdnSessionId").val();
        if (SessionId == "") {
            $("#dvMessage").html("Please enter SessionId");
            return false;
        }
    }
    /* PG TV*/
    else if (ServiceID == "26") {
        Amount = $("#ddl_PG_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Package");
            return false;
        }
        PackageId = $("#ddl_PG_Package :selected").text();
        UserName = $("#txt_PG_UserName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter user name");
            return false;
        }
        ContactNumber = $("#txt_PG_Contact").val();
        if (ContactNumber == "") {
            $("#dvMessage").html("Please enter contact number");
            return false;
        }
        CustomerID = $("#txt_PG_FullName").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter full name");
            return false;
        }
    }
    /*Jagriti TV*/
    else if (ServiceID == "27") {
        Amount = $("#ddl_Jagriti_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Package");
            return false;
        }
        PackageId = $("#ddl_Jagriti_Package :selected").text();

        CustomerID = $("#txt_Jagriti_CustomerID").val();
        if (CustomerID == "") {
            $("#dvMessage").html("Please enter customer ID");
            return false;
        }
        UserName = $("#txt_Jagriti_CustomerName").val();
        if (UserName == "") {
            $("#dvMessage").html("Please enter customer name");
            return false;
        }
        Ward = $("#txt_Jagriti_Ward").val();
        if (Ward == "") {
            $("#dvMessage").html("Please enter old ward number");
            return false;
        }
        ContactNumber = $("#txt_Jagriti_Contact").val();
        if (ContactNumber == "") {
            $("#dvMessage").html("Please enter mobile number");
            return false;
        }
        STD_ID = $("#txt_Jagriti_CASID").val();
        if (STD_ID == "") {
            $("#dvMessage").html("Please enter CAS ID/STD ID");
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
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserTV",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","CasId":"' + CustomerID + '","Amount":"' + Amount + '","Mpin":"' +
                        Mpin + '","PaymentMode":"' + PaymentMode + '","SessionId":"' + SessionId + '","PackageId":"' +
                        PackageId + '","UserName":"' + UserName + '","ContactNumber":"' + ContactNumber + '","STB_OR_CAS_ID":"' + STD_ID + '","WardNumber":"' + Ward + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
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
    debugger;
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var Number = "";
    var Amount = "";
    var SubscriberId = "";
    var CustomerName = "";
    var Package = "";
    var Ward = "";
    var MobileNumber = "";
    /* DISHHOME TV */
    if (ServiceID == "14") {
        Number = $("#txt_Dish_Customer").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
            return false;
        }

        Amount = $("#ddl_Dish_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }

        if (Number.length <= 3) {
            $("#dvMessage").html("Please enter valid CAS ID/Chip ID/Account");
            return false;
        }
    }
    /* SIM TV */
    else if (ServiceID == "15") {
        Number = $("#txt_Sim_Customer").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Customer ID/Chip ID");
            return false;
        }

        Amount = $("#ddl_Sim_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }


    }
    /* MERO TV */
    else if (ServiceID == "16") {
        Number = $("#txt_Mero_Stb").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter STB");
            return false;
        }
        if (Number.length <= 4) {
            $("#dvMessage").html("Please enter valid STB");
            return false;
        }
    }
    /* CLEAR TV*/
    else if (ServiceID == "23") {
        SubscriberId = $("#txt_Clear_Subscriber").val();
        if (SubscriberId == "") {
            $("#dvMessage").html("Please enter Subscriber id");
            return false;
        }
        Number = $("#txt_Clear_Mobile_Number").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter Mobile Number");
            return false;
        }

        Amount = $("#txt_Clear_Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        if (Number.length < 10) {
            $("#dvMessage").html("Please enter valid Mobile Number");
            return false;
        }
    }
    /* Max TV */
    else if (ServiceID == "24") {
        Number = $("#txt_Max_Customer").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter cust ID(10 digit)/Card no.(16 digit)");
            return false;
        }
    }
    /* Max TV */
    else if (ServiceID == "25") {
        Number = $("#txt_Prabhu_Customer").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
            return false;
        }
        Amount = $("#ddl_Prabhu_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }

        if (Number.length !== 11) {
            $("#dvMessage").html("Please enter valid CAS ID/Chip ID/Account");
            return false;
        }
    }
    /* PG TV */
    else if (ServiceID == "26") {
        Amount = $("#ddl_PG_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Package");
            return false;
        }
        Package = $("#ddl_PG_Package :selected").text();
        SubscriberId = $("#txt_PG_UserName").val();
        if (SubscriberId == "") {
            $("#dvMessage").html("Please enter user name");
            return false;
        }
        Number = $("#txt_PG_Contact").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter contact number");
            return false;
        }
        CustomerName = $("#txt_PG_FullName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter full name");
            return false;
        }
        if (Number.length < 10) {
            $("#dvMessage").html("Please enter valid Mobile Number");
            return false;
        }
    }
    /* Jagriti TV */
    else if (ServiceID == "27") {
        Number = $("#txt_Jagriti_CASID").val();
        if (Number == "") {
            $("#dvMessage").html("Please enter CAS ID/Chip ID/Account");
            return false;
        }
        if (Number.length <= 3) {
            $("#dvMessage").html("Please enter valid CAS ID/Chip ID/Account");
            return false;
        }
        if (Number.length != 11 && Number.length != 16) {
            $("#dvMessage").html(" CAS ID/Chip ID/Account Number Sould be 11 Numbers or 16 Numbers");
            return false;

        }
        Amount = $("#ddl_Jagriti_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }
        Package = $("#ddl_Jagriti_Package :selected").text();

        SubscriberId = $("#txt_Jagriti_CustomerID").val();
        if (SubscriberId == "") {
            $("#dvMessage").html("Please enter customer ID");
            return false;
        }
        CustomerName = $("#txt_Jagriti_CustomerName").val();
        if (CustomerName == "") {
            $("#dvMessage").html("Please enter customer name");
            return false;
        }
        Ward = $("#txt_Jagriti_Ward").val();
        if (Ward == "") {
            $("#dvMessage").html("Please enter old ward number");
            return false;
        }
        MobileNumber = $("#txt_Jagriti_Contact").val();
        if (MobileNumber == "") {
            $("#dvMessage").html("Please enter mobile number");
            return false;
        }
        if (MobileNumber.length < 10) {
            $("#dvMessage").html("Please enter valid Mobile Number");
            return false;
        }
    }
    $('#AjaxLoader').show();
    if (ServiceID == "14" || ServiceID == "15" || ServiceID == "16" || ServiceID == "24" || ServiceID == "25") {
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserTVDetails",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{"ServiceId":"' + parseInt(ServiceID) + '","CasId":"' + Number + '"}',
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

                        $("#tblTVDetail").html("");
                        if (IsValidJson) {

                            if (arr['Message'].toLowerCase() == "success") {
                                /* DISHHOME TV */
                                if (ServiceID == "14") {
                                    if (jsonData != null && jsonData.data_dishhome != null) {
                                        $("#hdnAccount").val(jsonData.data_dishhome.mobile_no);
                                        str += '<div class="card">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">CAS ID/Chip ID/Account</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Package</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_dishhome.package + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Status</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_dishhome.customer_status + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Type</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_dishhome.customer_type + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Expiry Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_dishhome.expiry_date + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-12 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Address</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_dishhome.customer_address + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                    }
                                }
                                /* SIM TV */
                                else if (ServiceID == "15") {
                                    if (jsonData != null && jsonData.data_simtv != null) {
                                        $("#hdnAccount").val(jsonData.data_simtv.mobile_no);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">CAS ID/Chip ID/Account</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Due Amount</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_simtv.due_amount + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.data_simtv.customer_name + '</span>';
                                        str += '</div>';
                                        //str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        //str += '<label class="mb-0 text-soft w-100">Customer Type</label>';
                                        //str += '<span class="fw-medium">' + jsonData.data_simtv.customer_type + '</span>';
                                        //str += '</div>';
                                        //str += '<div class="col-md-6 mb-3">';
                                        //str += '<label class="mb-0 text-soft w-100">Expiry Date</label>';
                                        //str += '<span class="fw-medium">' + jsonData.data_simtv.expiry_date + '</span>';
                                        //str += '</div>';
                                        //str += '<div class="col-md-12 mb-3">';
                                        //str += '<label class="mb-0 text-soft w-100">Address</label>';
                                        //str += '<span class="fw-medium">' + jsonData.data_simtv.customer_address + '</span>';
                                        //str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                    }
                                }
                                /* MERO TV */
                                else if (ServiceID == "16") {
                                    if (jsonData != null && jsonData.connection != null) {

                                        $("#hdnSessionId").val(jsonData.session_id);
                                        //$("#hdnAccount").val(jsonData.data_merotv.mobile_no);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Id</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer.first_name + ' ' + jsonData.customer.last_name + '</span>';
                                        str += '</div>';
                                        //SetupBoxlist
                                        str += '<div class="col-md-12 mb-6 meroTvRadio">';
                                        str += '<label class="mb-0 text-soft w-100">Select Setup box</label>';
                                        //for (var i = 0; i < jsonData.connection.length; ++i) {
                                        //    for (var j = 0; j < jsonData.connection[i].package_list.length; ++j) {
                                        //        str += '<option value="' + jsonData.connection[i].package_list[j].id + '">' + jsonData.connection[i].package_list[j].name + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.connection[i].package_list[j].price + '</option>';
                                        //    }
                                        //}
                                        for (var i = 0; i < jsonData.connection.length; ++i) {

                                            str += '<input type="radio" name="stb" value="' + jsonData.connection[i].stb + '">';
                                            str += jsonData.connection[i].stb + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                                        }
                                        str += '</select>';
                                        str += '</div>';

                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                    }
                                }
                                /* MAX TV */
                                else if (ServiceID == "24") {
                                    if (jsonData != null) {

                                        $("#hdnSessionId").val(jsonData.session_id);
                                        $("#hdnMaxTvAmount").val(jsonData.amount);

                                        //$("#hdnAccount").val(jsonData.data_merotv.mobile_no);
                                        str += '<div class="card mt-4">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer ID</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Smart Card No</label>';
                                        str += '<span class="fw-medium">' + jsonData.smartcard_no + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">STB No</label>';
                                        str += '<span class="fw-medium">' + jsonData.stb_no + '</span>';
                                        str += '</div>';

                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        $("#lblAmountTV").html(jsonData.amount);
                                        $("#Amount").val(jsonData.amount);
                                    }
                                }
                                if (ServiceID == "25") {
                                    if (jsonData != null) {
                                        $("#hdnSessionId").val(jsonData.session_id);
                                        str += '<div class="card">';
                                        str += '<div class="card-inner p-0">';
                                        str += '<div class="row">';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">CAS ID/Chip ID/Account</label>';
                                        str += '<span class="fw-medium">' + Number + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">STB Count</label>';
                                        str += '<span class="fw-medium">' + jsonData.stb_count + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer_name + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                                        str += '<label class="mb-0 text-soft w-100">Customer ID</label>';
                                        str += '<span class="fw-medium">' + jsonData.customer_id + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-12 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Expiry Date</label>';
                                        str += '<span class="fw-medium">' + jsonData.expiry_date + '</span>';
                                        str += '</div>';
                                        str += '<div class="col-md-12 mb-3">';
                                        str += '<label class="mb-0 text-soft w-100">Address</label>';
                                        str += '<span class="fw-medium">' + jsonData.product_name + '</span>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';
                                        str += '</div>';

                                        $("#lblAmountTV").html($("#ddl_Prabhu_Amount :selected").val());
                                        $("#Amount").val($("#ddl_Prabhu_Amount :selected").val());

                                    }
                                }
                                $("#tblTVDetail").append(str);
                                $("#DivTV").hide();
                                $("#DivTVStep2").hide();
                                $("#DivTVDetail").show();
                                if (ServiceID == "16") {
                                    $(".clsamt").css("display", "none");
                                    $("#btnPay").css("display", "none");
                                    $("#meroTvProceed").css("display", "block");
                                    $("#TvServiceId").val(jsonData.session_id);
                                }
                                else {
                                    $(".clsamt").css("display", "block");
                                    $("#btnPay").css("display", "block");
                                    $("#meroTvProceed").css("display", "none");

                                    ServiceCharge();

                                }
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html("");
                                $("#txnMsg").html("");

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
    else {
        var str = "";
        $("#tblTVDetail").html("");
        /* CLEAR TV*/
        if (ServiceID == "23") {
            str += '<div class="card mt-4">';
            str += '<div class="card-inner p-0">';
            str += '<div class="row">';
            str += '<div class="col-md-6 mb-3">';
            str += '<label class="mb-0 text-soft w-100">Mobile Number</label>';
            str += '<span class="fw-medium">' + Number + '</span>';
            str += '</div>';
            str += '<div class="col-md-6 mb-3 text-sm-right">';
            str += '<label class="mb-0 text-soft w-100">Amount</label>';
            str += '<span class="fw-medium">' + Amount + '</span>';
            str += '</div>';
            str += '<div class="col-md-6 mb-3">';
            str += '<label class="mb-0 text-soft w-100">Subscriber ID</label>';
            str += '<span class="fw-medium">' + SubscriberId + '</span>';
            str += '</div>';
            str += '</div>';
            str += '</div>';
            str += '</div>';


        }
        else if (ServiceID == "26") {
            str += '<div class="card mt-4">';
            str += '<div class="card-inner p-0">';
            str += '<div class="row">';
            str += '<div class="col-md-6 mb-3">';
            str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
            str += '<span class="fw-medium">' + CustomerName + '</span>';
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
            str += '<label class="mb-0 text-soft w-100">User Name</label>';
            str += '<span class="fw-medium">' + SubscriberId + '</span>';
            str += '</div>';
            str += '</div>';
            str += '</div>';
            str += '</div>';
        }
        else if (ServiceID == "27") {
            str += '<div class="card mt-4">';
            str += '<div class="card-inner p-0">';
            str += '<div class="row">';
            str += '<div class="col-md-6 mb-3">';
            str += '<label class="mb-0 text-soft w-100">Customer Name</label>';
            str += '<span class="fw-medium">' + CustomerName + '</span>';
            str += '</div>';
            str += '<div class="col-md-6 mb-3 text-sm-right">';
            str += '<label class="mb-0 text-soft w-100">Customer ID</label>';
            str += '<span class="fw-medium">' + SubscriberId + '</span>';
            str += '</div>';
            str += '<div class="col-md-6 mb-3">';
            str += '<label class="mb-0 text-soft w-100">Contact Number</label>';
            str += '<span class="fw-medium">' + MobileNumber + '</span>';
            str += '</div>';
            str += '<div class="col-md-6 mb-3 text-sm-right">';
            str += '<label class="mb-0 text-soft w-100">CAS ID</label>';
            str += '<span class="fw-medium">' + Number + '</span>';
            str += '</div>';
            str += '<div class="col-md-12 mb-6 ">';
            str += '<label class="mb-0 text-soft w-100">Package</label>';
            str += '<span class="fw-medium">' + Package + '</span>';
            str += '</div>';
            str += '<div class="col-md-12 mb-6 ">';
            str += '<label class="mb-0 text-soft w-100">Old ward no.</label>';
            str += '<span class="fw-medium">' + Ward + '</span>';
            str += '</div>';
            str += '</div>';
            str += '</div>';
            str += '</div>';


        }
        $("#tblTVDetail").append(str);
        ServiceCharge();
        $("#DivTV").hide();
        $("#DivTVStep2").hide();
        $("#DivTVDetail").show();
        $('#AjaxLoader').hide();
        $("#dvMessage").html("");
        $("#txnMsg").html("");

    }

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);

    var Amount = "";
    /* DISHHOME TV */
    if (ServiceID == "14") {
        Amount = $("#ddl_Dish_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }
    /* SIM TV */
    else if (ServiceID == "15") {
        Amount = $("#ddl_Sim_Amount :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please select Amount");
            return false;
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }
    /* MERO TV */
    else if (ServiceID == "16") {
        var selectedAmount = $("#ddlPackage :selected").text().split("रु");
        if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
            Amount = selectedAmount[1];
            $("#lblAmountTV").html(selectedAmount[1]);
            $("#Amount").val(selectedAmount[1]);
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }
    /* CLEAR TV*/
    else if (ServiceID == "23") {
        Amount = $("#txt_Clear_Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }

    /* PG TV*/
    else if (ServiceID == "26") {
        Amount = $("#ddl_PG_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please Select Package");
            return false;
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }
    /* Jagriti TV*/
    else if (ServiceID == "27") {
        Amount = $("#ddl_Jagriti_Package :selected").val();
        if (Amount == "") {
            $("#dvMessage").html("Please Select Package");
            return false;
        }
        $("#lblAmountTV").html(Amount);
        $("#Amount").val(Amount);
    }


    $("#txnMsg").html("");
});
$("#meroTvProceed").click(function () {
    debugger;
    var stbRadioButtons = document.getElementsByName('stb');
    var isChecked = false;
    var stb = "";
    // Check if any radio button is checked
    for (var i = 0; i < stbRadioButtons.length; i++) {
        if (stbRadioButtons[i].checked) {
            isChecked = true;
            stb = stbRadioButtons[i].value;
            break;
        }
    }
    if (!isChecked) {
        $("#dvMessage").html('Please select an option for STB.');
        return false;
    }
    var TvsessionId = $("#TvServiceId").val();
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserMeroTvPackage",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"ServiceId":"' + parseInt(ServiceID) + '","stb":"' + stb + '", "sessionId":"' + TvsessionId + '"}',
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
            if (arr.Message.toLowerCase() == "success") {
                if (ServiceID == "16") {
                    if (jsonData != null && jsonData.packages != null) {
                        $("#hdnSessionId").val(jsonData.session_id);
                        str += '<div class="card mt-4">';
                        str += '<div class="card-inner p-0">';
                        str += '<div class="row">';
                        
                        str += '<div class="col-md-6 mb-3">';
                        str += '<label class="mb-0 text-soft w-100">Select Package</label>';


                        //packagelist
                        str += '<span class="fw-medium">';
                        str += '<select id="ddlPackage" class="form-control form-control-lg" onchange="GetAmount();">';
                        str += '<option value="0">Select Packages</option>';
                        for (var i = 0; i < jsonData.packages.length; ++i) {
                            str += '<option value="' + jsonData.packages[i].package_id + '">' + jsonData.packages[i].package_name + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;रु' + jsonData.packages[i].amount + '</option>';
                        }

                        str += '</select>';
                        str += '</span>';
                        str += '</div>';
                        str += '<div class="col-md-6 mb-3 text-sm-right">';
                        str += '<label class="mb-0 text-soft w-100">STB Number</label>';
                        str += '<span class="fw-medium">' + stb + '</span>';
                        str += '</div>';
                        str += '</div>';
                        str += '</div>';
                        str += '</div>';

                    }
                }
            }
            $(".clsamt").css("display", "block");
            $(".meroTvRadio").css("display", "none");
            $("#tblTVDetail").append(str);
            $("#DivTV").hide();
            $("#DivTVStep2").hide();
            $("#DivTVDetail").show();
            $("#btnPay").css("display", "block");
            $("#meroTvProceed").css("display", "none");
            $('#AjaxLoader').hide();
            $("#dvMessage").html("");
            $("#txnMsg").html("");
            $("#DivProceedToPay").hide();
            $("#lblProviderName").html(providername + " TV");

        }

    })

});
function GetAmount() {

    var selectedAmount = $("#ddlPackage :selected").text().split("रु");
    if (selectedAmount[1] != "0" || selectedAmount[1] != "") {
        $("#lblAmountTV").html(selectedAmount[1]);
        $("#Amount").val(selectedAmount[1]);
        ServiceCharge();
    }

}

var specialKeys = new Array();
specialKeys.push(8);  //Backspace
specialKeys.push(9);  //Tab
specialKeys.push(46); //Delete
specialKeys.push(36); //Home
specialKeys.push(35); //End
specialKeys.push(37); //Left
specialKeys.push(39); //Right
function IsAlphaNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    return ret;
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function IsAlphabetOnly(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1));
    return ret;
}