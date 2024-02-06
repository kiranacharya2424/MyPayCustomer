var ErrorMessage = false;
var BankList = '';
$(document).ready(function () {
    debugger;
    var param = getUrlVars();
    if (param["mode"] != undefined && param["mode"] == "banktransfer") {
        $("#DivAnyBankAccount").trigger("click");
    }

    $("html, body").animate({ scrollTop: "0" });
});


$("#DivAnyBankAccount").click(function () {
    debugger;
    var ServiceId = 34;
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
                $('#AjaxLoader').show();
                $("#DivBankAll").show();
                $("#DivAccount").hide();
                setTimeout(
                    function () {
                        LoadBank();
                        Purpose();
                        $('#AjaxLoader').hide();
                    }, 10);
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

$("#DivMyPayUser").click(function () {
    debugger;
    var ServiceId = 22;
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
                $('#AjaxLoader').show();
                $("#DivMobileInfo").show(0);
                $("#DivAccount").hide(0);
                setTimeout(
                    function () {
                        Purpose();
                        $('#AjaxLoader').hide();
                    }, 10);
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

function LoadBank() {
    var objBank = '';
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserBankListAll",
        data: null,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                var jsonData;
                var IsValidJson = false;
                try {
                    jsonData = $.parseJSON(response);
                    var IsValidJson = true;
                    BankList = jsonData;

                }
                catch (err) {

                }
                if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                    for (var i = 0; i < jsonData.data.length; ++i) {

                        objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData ">';
                        objBank += '<div class="text-center p-3 border round">';
                        objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + jsonData.data[i].ICON_NAME + ' width="100" style="width:100px;" /></a>';
                        objBank += "<input type='hidden' id='BankName' value='" + jsonData.data[i].BANK_NAME + "'>";
                        objBank += "<input type='hidden' id='BranchName' value='" + jsonData.data[i].BRANCH_NAME + "'>";
                        objBank += "<input type='hidden' id='BranchId' value='" + jsonData.data[i].BRANCH_CD + "'>";
                        objBank += jsonData.data[i].BANK_NAME;
                        objBank += "<input type='hidden' id='BankId' value='" + jsonData.data[i].BANK_CD + "'>";
                        objBank += "<input type='hidden' id='BankCode' value='" + jsonData.data[i].SHORTCODE + "'><hr>";

                        objBank += '</div>';
                        objBank += '</div>';
                    }
                    $("#dvRecordsDisplay").html(objBank);
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                    }
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

    $('#AjaxLoader').hide();

    $(".DivBankData").click(function () {
        $("#DivBankAll").hide(300);
        $("#DivAccountInfo").show(300);
        $("#SpanBankName").text($(this).find('#BankName').val());
        $('#ImgBankLogo').attr('src', $(this).find('#BankLogo').attr('src'));
        $('#hfBankCode').val($(this).find('#BankCode').val());
        $('#hfBankId').val($(this).find('#BankId').val());
        $('#hfBankName').val($(this).find('#BankName').val());
        $('#hfBranchId').val($(this).find('#BranchId').val());
        $('#hfBranchName').val($(this).find('#BranchName').val());
        $("html, body").animate({ scrollTop: "0" });
    });
}

$("#search").keyup(function () {
    debugger;
    $("#dvRecordsDisplay").html('');
    $("#dvMessage").html('');
    var searchVal = $("#search").val().toLowerCase();
    var filteredBank = BankList.data.filter((item) => item.BANK_NAME.toLowerCase().includes(searchVal));

    var objBank = '';
    if (filteredBank != null && filteredBank.length > 0) {
        for (var i = 0; i < filteredBank.length; ++i) {

            objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData ">';
            objBank += '<div class="text-center p-3 border round">';
            objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + filteredBank[i].ICON_NAME + ' width="100" style="width:100px;" /></a>';
            objBank += "<input type='hidden' id='BankName' value='" + filteredBank[i].BANK_NAME + "'>";
            objBank += "<input type='hidden' id='BranchName' value='" + filteredBank[i].BRANCH_NAME + "'>";
            objBank += "<input type='hidden' id='BranchId' value='" + filteredBank[i].BRANCH_CD + "'>";
            objBank += filteredBank[i].BANK_NAME;
            objBank += "<input type='hidden' id='BankId' value='" + filteredBank[i].BANK_CD + "'>";
            objBank += "<input type='hidden' id='BankCode' value='" + filteredBank[i].SHORTCODE + "'><hr>";

            objBank += '</div>';
            objBank += '</div>';
        }
        $("#dvRecordsDisplay").html(objBank);
        $(".DivBankData").click(function () {
            $("#DivBankAll").hide(300);
            $("#DivAccountInfo").show(300);
            $("#SpanBankName").text($(this).find('#BankName').val());
            $('#ImgBankLogo').attr('src', $(this).find('#BankLogo').attr('src'));
            $('#hfBankCode').val($(this).find('#BankCode').val());
            $('#hfBankId').val($(this).find('#BankId').val());
            $('#hfBankName').val($(this).find('#BankName').val());
            $('#hfBranchId').val($(this).find('#BranchId').val());
            $('#hfBranchName').val($(this).find('#BranchName').val());
            $("html, body").animate({ scrollTop: "0" });
        });
    }
    else {

        $("#dvMessage").html("No Data Found.");
    }

})

function Purpose() {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetPurpose",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {
                    var jsonData = $.parseJSON(response);
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        $('#ddlPurpose').empty().append($("<option></option>").val('0').html('Select Purpose'));
                        $('#ddlMobilePurpose').empty().append($("<option></option>").val('0').html('Select Purpose'));
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            $("#ddlPurpose").append($("<option></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
                            $("#ddlMobilePurpose").append($("<option></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
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
        }, 100);
}

$("#btnProceed").click(function () {
    var AccountNo = $("#AccountNo").val();
    if (AccountNo == "") {
        $("#dvMessage").html("Please enter AccountNo");
        return false;
    }
    var AccountHolderName = $("#AccountHolderName").val();
    if (AccountHolderName == "") {
        $("#dvMessage").html("Please enter Account Holder Name");
        return false;
    }
    var Amount = $("#Amount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
    }
    var Purpose = $("#ddlPurpose").val();
    if (Purpose == "0") {
        $("#dvMessage").html("Please select Purpose");
        return false;
    }
    var Remarks = $("#Remarks").val();
    if (Remarks == "") {
        $("#dvMessage").html("Please enter Remarks");
        return false;
    }
    var BankId = $("#hfBankId").val();
    if (BankId == "") {
        $("#dvMessage").html("Please select Bank");
        return false;
    }
    if (parseFloat($("#hfWalletBalance").val()) < parseFloat(Amount)) {
        $("#dvMessage").html("Insufficient Balance");
        return false;
    }
    if (Amount < 10) {
        $("#dvMessage").html("Minimum Transfer amount should be Rs. 10");
        return false;
    }
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/AccountValidation",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"BankId":"' + BankId + '","AccountId":"' + AccountNo + '","AccountName":"' + AccountHolderName + '"}',
                success: function (response) {
                    debugger;
                    try {
                        var arr = $.parseJSON(response);
                        if (arr['ReponseCode'] == "1") {
                            ServiceCharge();
                            $("#BankNamePopup").text($("#AccountHolderName").val());
                            $("#BankAccountPopup").text($("#AccountNo").val());
                            $("#BankAmountPopup").text($("#Amount").val());
                            $("#BankPurposePopup").text($("#ddlPurpose").find(":selected").text());
                            $("#BankRemarksPopup").text($("#Remarks").val());
                            //$("#BankCashbackPopup").text(arr['CashbackAmount']);
                            //$("#BankServiceChargesPopup").text(arr['ServiceChargeAmount']);
                            $('#AjaxLoader').hide();
                            $('#BankPaymentPopup').modal('show');
                            //$('#AjaxLoader').hide();
                            $("#hdnTransferMode").val("0");
                            if (arr['branchId'] != "") {
                                $('#hfBranchId').val(arr['branchId']);
                            }
                            return false;
                        }

                        else {
                            $('#AjaxLoader').hide();
                            $('#dvMessage').html(arr['responseMessage']);
                        }

                    } catch (e) {
                        $('#AjaxLoader').hide();
                        $('#dvMessage').html(response);
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
});

function Back(HideDiv, ShowDiv) {
    $("#" + ShowDiv).show(100);
    $("#" + HideDiv).hide(100);
    $("#dvMessage").html("");
    $("html, body").animate({ scrollTop: "0" });
}

function CheckRecipientDetail() {
    debugger;
    ErrorMessage = false;
    $('#dvMessage').html("");
    $("#MobileAccountHolderName").val('');
    var MobileLenght = $("#MobileNo").val().length;
    if (MobileLenght == '10') {
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/RecipientDetail",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"RecipientPhone":"' + $("#MobileNo").val() + '"}',
                    success: function (response) {
                        debugger;
                        var arr = $.parseJSON(response);

                        if (arr['ReponseCode'] == "1") {

                            var name = arr['Name'];

                            let index = name.lastIndexOf(" ");
                            var encryptedName = name.substring(0, 1) + '******* ' + name.substring(index + 1);
                            $("#MobileAccountHolderName").val(encryptedName);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $('#dvMessage').html(arr['responseMessage']);
                            ErrorMessage = true;
                            return false;
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

$("#btnMobileProceed").click(function () {
    debugger;
    if (ErrorMessage == true) {
        return false;
    }
    var MobileNo = $("#MobileNo").val();
    if (MobileNo == "") {
        $("#dvMessage").html("Please enter MobileNo");
        return false;
    }
    var Amount = $("#MobileAmount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please enter Amount");
        return false;
    }
    if (parseFloat(Amount) < 10) {
        $("#txnMsg").html("Amount cannot be less than Rs. 10");
        $("#DivErrMessage").modal("show");
        return false;
    }
    var Purpose = $("#ddlMobilePurpose").val();
    if (Purpose == "0") {
        $("#dvMessage").html("Please select Purpose");
        return false;
    }
    var Remarks = $("#MobileRemarks").val();
    if (Remarks == "") {
        $("#dvMessage").html("Please enter Remarks");
        return false;
    }
    if (parseFloat($("#MobileAmount").val()) > parseFloat($("#spnWalletDashboard").html())) {
        $("#txnMsg").html("Insufficient Balance");
        $("#DivErrMessage").modal("show");
        return false;
    }
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ServiceCharge",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + Amount + '","ServiceId":"' + 22 + '"}',
                success: function (response) {
                    debugger;
                    var arr = $.parseJSON(response);
                    if (arr['Message'] == "success") {
                        $("#NamePopup").text($("#MobileAccountHolderName").val());
                        $("#MobilePopup").text($("#MobileNo").val());
                        $("#AmountPopup").text(arr['Amount']);
                        $("#PurposePopup").text($("#ddlMobilePurpose").find(":selected").text());
                        $("#RemarksPopup").text($("#MobileRemarks").val());
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $('#AjaxLoader').hide();
                        $('#PaymentPopup').modal('show');
                        $("#hdnTransferMode").val("1");
                        return false;
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $('#dvMessage').html(arr['responseMessage']);
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
});
$("#btnOkPopup").click(function () {
    $('#PaymentPopup').modal('hide');
    $("#DivPin").modal('show');
});

$("#btnMobilePin").click(function () {
    if ($("#hdnTransferMode").val() == "1") {
        var MobileNo = $("#MobileNo").val();
        if (MobileNo == "") {
            $("#dvMessage").html("Please enter MobileNo");
            return false;
        }
        var Amount = $("#MobileAmount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        var Purpose = $("#ddlMobilePurpose").val();
        if (Purpose == "0") {
            $("#dvMessage").html("Please select Purpose");
            return false;
        }
        var Remarks = $("#MobileRemarks").val();
        if (Remarks == "") {
            $("#dvMessage").html("Please enter Remarks");
            return false;
        }
        var Pin = $("#Pin").val();
        if (Pin == "") {
            $("#dvMessage").html("Please enter Pin");
            return false;
        }
        var UniqueCustomerId = $("#hdnUniqueCustomerId").val();
        $("#dvMessage").html("");
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/TransferByPhone",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"RecipientPhone":"' + MobileNo + '","Amount":"' + Amount + '","Remarks":"' + Remarks + '","MPin":"' + Pin + '","UniqueCustomerId":"' + UniqueCustomerId + '"}',
                    success: function (response) {
                        debugger;
                        var arr = $.parseJSON(response);
                        if (arr['ReponseCode'] == "1") {
                            $("#DivPin").modal("hide");
                            $("#Pin").val("");
                            $("#PaymentSuccess").modal("show");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#Pin").val("");
                            $("#DivPin").modal("hide");
                            $('#txnMsg').html(arr['Message']);
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
    else {
        var AccountNo = $("#AccountNo").val();
        if (AccountNo == "") {
            $("#dvMessage").html("Please enter AccountNo");
            return false;
        }
        var AccountHolderName = $("#AccountHolderName").val();
        if (AccountHolderName == "") {
            $("#dvMessage").html("Please enter Account Holder Name");
            return false;
        }
        var Amount = $("#Amount").val();
        if (Amount == "") {
            $("#dvMessage").html("Please enter Amount");
            return false;
        }
        var Purpose = $("#ddlPurpose").val();
        if (Purpose == "0") {
            $("#dvMessage").html("Please select Purpose");
            return false;
        }
        var Remarks = $("#Remarks").val();
        if (Remarks == "") {
            $("#dvMessage").html("Please enter Remarks");
            return false;
        }
        var BankId = $("#hfBankId").val();
        if (BankId == "") {
            $("#dvMessage").html("Please select Bank");
            return false;
        }
        if (parseFloat($("#hfWalletBalance").val()) < parseFloat(Amount)) {
            $("#dvMessage").html("Insufficient Balance");
            return false;
        }
        if (Amount < 10) {
            $("#dvMessage").html("Minimum Transfer amount should be Rs. 10");
            return false;
        }
        var Pin = $("#Pin").val();
        if (Pin == "") {
            $("#dvMessage").html("Please enter Pin");
            return false;
        }
        $("#dvMessage").html("");
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/BankTransfer",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Name":"' + AccountHolderName + '","AccountNumber":"' + AccountNo + '","BranchName":"' + $("#hfBranchName").val() + '","BankName":"' + $("#hfBankName").val() + '","BankId":"' + BankId + '","BranchId":"' + $("#hfBranchId").val() + '","Amount":"' + Amount + '","Description":"' + Remarks + '","MPin":"' + Pin + '"}',
                    success: function (response) {
                        var arr = $.parseJSON(response);
                        debugger;
                        if (arr['Message'] == "Success") {
                            $("#DivPin").modal("hide");
                            $("#PaymentSuccess").modal("show");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $('#dvMessage').html(arr['responseMessage']);
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
});



$("#btnBankOkPopup").click(function () {
    $('#BankPaymentPopup').modal('hide');
    $("#DivPin").modal('show');
});

function ServiceCharge() {

    var ServiceID = "34";
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

                    if (arr['Message'] == "success") {
                        $("#BankCashbackPopup").text(arr['CashbackAmount']);
                        $("#BankServiceChargesPopup").text(arr['ServiceChargeAmount']);

                        //CheckCoinBalance(0);

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