var ErrorMessage = false;
var BankList = '';
var TransferType = '';
function BankingTabClick() {
    $("#DivMobileInfoTab").hide(200);
    $("#BankingTab").show(200);
}

function DivMobileInfoTabClick() {
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
                $("#DivMobileInfoTab").show();
                $("#BankingTab").hide();
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

function LoadBank(objTransferType) {
    TransferType = objTransferType;
    var objBank = '';
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserBankListAll",
        data: '{"TransferType":"' + objTransferType + '"}',
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
                        if ((objTransferType.toLowerCase() == "mbanking") || (objTransferType.toLowerCase() == "ebanking")) {
                            if ((objTransferType.toLowerCase() == "mbanking")) {
                                $("#banktitle").html("Mobile Banking");
                            }
                            else {
                                $("#banktitle").html("E-Banking");
                            }
                            objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData ">';
                            objBank += '<div class="text-center p-3 border round">';
                            objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + jsonData.data[i].LogoUrl + ' width="100" style="width:100px;" /></a>';
                            objBank += '<div class="text-base mt-1">' + jsonData.data[i].InstitutionName + ' </div>';
                            objBank += "<input type='hidden' id='BankName' value='" + jsonData.data[i].InstitutionName + "'>";
                            objBank += "<input type='hidden' id='BranchName' value='" + jsonData.data[i].InstrumentName + "'>";
                            objBank += "<input type='hidden' id='BranchId' value='" + jsonData.data[i].InstrumentCode + "'>";
                            objBank += "<input type='hidden' id='BankId' value='" + jsonData.data[i].InstrumentCode + "'>";
                            objBank += "<input type='hidden' id='BankCode' value='" + jsonData.data[i].InstrumentCode + "'><hr>";
                            objBank += '</div>';
                            objBank += '</div>';

                        }
                        else {

                            objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData">';
                            objBank += '<div class="text-center p-3 border round">';
                            objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + jsonData.data[i].ICON_NAME + ' width="100" style="width:100px;" /></a>';
                            objBank += '<div class="text-base mt-1">' + jsonData.data[i].BANK_NAME + ' </div>';
                            objBank += "<input type='hidden' id='BankName' value='" + jsonData.data[i].BANK_NAME + "'>";
                            objBank += "<input type='hidden' id='BranchName' value='" + jsonData.data[i].BRANCH_NAME + "'>";
                            objBank += "<input type='hidden' id='BranchId' value='" + jsonData.data[i].BRANCH_CD + "'>";
                            objBank += "<input type='hidden' id='BankId' value='" + jsonData.data[i].BANK_CD + "'>";
                            objBank += "<input type='hidden' id='BankCode' value='" + jsonData.data[i].SHORTCODE + "'><hr>";
                            objBank += '</div>';
                            objBank += '</div>';
                        }
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
        $("#paymenttype").text("Banking");
        $("#PaymentPopup").modal("show");
        $("#SpanBankName").text($(this).find('#BankName').val());
        $('#ImgBankLogo').attr('src', $(this).find('#BankLogo').attr('src'));
        $("#SpanBankName").show();
        $('#ImgBankLogo').show();
        $('#hfBankCode').val($(this).find('#BankCode').val());
        $('#hfBankId').val($(this).find('#BankId').val());
        $('#hfBankName').val($(this).find('#BankName').val());
        $('#hfBranchId').val($(this).find('#BranchId').val());
        $('#hfBranchName').val($(this).find('#BranchName').val());
        $("html, body").animate({ scrollTop: "0" });
    });
}


$("#search").keyup(function () {
    $("#dvRecordsDisplay").html('');
    $("#dvMessage").html('');
    var searchVal = $("#search").val().toLowerCase();
    var filteredBank = BankList.data.filter((item) => item.InstitutionName.toLowerCase().includes(searchVal));

    var objBank = '';
    if (filteredBank != null && filteredBank.length > 0) {
        for (var i = 0; i < filteredBank.length; ++i) {
            if ((TransferType.toLowerCase() == "mbanking") || (TransferType.toLowerCase() == "ebanking")) {
                if ((TransferType.toLowerCase() == "mbanking")) {
                    $("#banktitle").html("Mobile Banking");
                }
                else {
                    $("#banktitle").html("E-Banking");
                }
                objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData ">';
                objBank += '<div class="text-center p-3 border round">';
                objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + filteredBank[i].LogoUrl + ' width="100" style="width:100px;" /></a>';
                objBank += '<div class="text-base mt-1">' + filteredBank[i].InstitutionName + ' </div>';
                objBank += "<input type='hidden' id='BankName' value='" + filteredBank[i].InstitutionName + "'>";
                objBank += "<input type='hidden' id='BranchName' value='" + filteredBank[i].InstrumentName + "'>";
                objBank += "<input type='hidden' id='BranchId' value='" + filteredBank[i].InstrumentCode + "'>";
                objBank += "<input type='hidden' id='BankId' value='" + filteredBank[i].InstrumentCode + "'>";
                objBank += "<input type='hidden' id='BankCode' value='" + filteredBank[i].InstrumentCode + "'><hr>";
                objBank += '</div>';
                objBank += '</div>';

            }
            else {

                objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData">';
                objBank += '<div class="text-center p-3 border round">';
                objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + filteredBank[i].ICON_NAME + ' width="100" style="width:100px;" /></a>';
                objBank += '<div class="text-base mt-1">' + filteredBank[i].BANK_NAME + ' </div>';
                objBank += "<input type='hidden' id='BankName' value='" + filteredBank[i].BANK_NAME + "'>";
                objBank += "<input type='hidden' id='BranchName' value='" + filteredBank[i].BRANCH_NAME + "'>";
                objBank += "<input type='hidden' id='BranchId' value='" + filteredBank[i].BRANCH_CD + "'>";
                objBank += "<input type='hidden' id='BankId' value='" + filteredBank[i].BANK_CD + "'>";
                objBank += "<input type='hidden' id='BankCode' value='" + filteredBank[i].SHORTCODE + "'><hr>";
                objBank += '</div>';
                objBank += '</div>';
            }
        }
        $("#dvRecordsDisplay").html(objBank);
        $(".DivBankData").click(function () {
            $("#paymenttype").text("Banking");
            $("#PaymentPopup").modal("show");
            $("#SpanBankName").text($(this).find('#BankName').val());
            $('#ImgBankLogo').attr('src', $(this).find('#BankLogo').attr('src'));
            $("#SpanBankName").show();
            $('#ImgBankLogo').show();
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
                        $('#selectPurpose').empty().append($("<option></option>").val('0').html('Select Purpose'));
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            $("#ddlPurpose").append($("<option></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
                            $("#ddlMobilePurpose").append($("<option></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
                            $("#selectPurpose").append($("<option></option>").val(jsonData.data[i].Id).html(jsonData.data[i].CategoryName));
                        }

                        $('#ddlPurpose option[value="0"]').attr("selected", true);
                        $('#ddlMobilePurpose option[value="0"]').attr("selected", true);
                        $('#selectPurpose option[value="0"]').attr("selected", true);

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


function Back(HideDiv, ShowDiv) {
    $("#" + ShowDiv).show(100);
    $("#" + HideDiv).hide(100);
    $("#dvMessage").html("");
    $('#txnMsg').html("");
    $("html, body").animate({ scrollTop: "0" });
}

function CheckRecipientDetail() {
    debugger;
    ErrorMessage = false;
    $('#dvMessage').html("");
    $('#txnMsg').html("");
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

$(document).ready(function () {
    $('#AjaxLoader').show();

    setTimeout(
        function () {
            Purpose();
            $('#AjaxLoader').hide();
        }, 10);

    $("#dvpaymentback").click(function () {

        $("#DivPaymentGateway").hide();
        $("#DivMobileInfoTab").hide();
        $("#DivBankAll").hide();
        $("#DivAccount").show();
        $('#AjaxLoader').hide();
    });



    $("#DivMobileBanking").click(function () {
        debugger;
        var ServiceId = 59;
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
                    setTimeout(
                        function () {
                            $("#DivBankAll").show(200);
                            $("#DivAccount").hide(200);
                            LoadBank("mbanking");
                            Purpose();
                            $('#AjaxLoader').hide();
                        }, 100);
                    $("#txtAmount").val("");
                    $("#selectPurpose").val("");
                    $("#dvMessageSpan").html("");
                    $('#txnMsg').html("");
                    $("#hfTransferType").val("mbanking");
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


    $("#DiveBanking").click(function () {
        debugger;
        var ServiceId = 57;
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
                    setTimeout(
                        function () {
                            $("#DivBankAll").show(200);
                            $("#DivAccount").hide(200);
                            LoadBank("ebanking");
                            Purpose();
                            $('#AjaxLoader').hide();
                        }, 100);
                    $("#txtAmount").val("");
                    $("#selectPurpose").val("");
                    $("#dvMessageSpan").html("");
                    $('#txnMsg').html("");
                    $("#hfTransferType").val("ebanking");
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



    $("#DivDebitCards").click(function () {
        debugger;
        var ServiceId = 21;
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
                    $("#paymenttype").html("Debit / Credit Cards");
                    $("#PaymentPopup").modal("show");
                    $("#hfTransferType").val("cards");
                    $("#txtAmount").val("");
                    $("#selectPurpose").val("");
                    $("#dvMessageSpan").html("");
                    $('#txnMsg').html("");
                    $("#SpanBankName").hide();
                    $('#ImgBankLogo').hide();
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


    $("#DivConnectIPS").click(function () {
        debugger;
        var ServiceId = 19;
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
                    $("#paymenttype").html("Connect IPS");
                    $("#PaymentPopup").modal("show");
                    $("#hfTransferType").val("cips");
                    $("#txtAmount").val("");
                    $("#dvMessageSpan").html("");
                    $('#txnMsg').html("");
                    $("#SpanBankName").hide();
                    $('#ImgBankLogo').hide();
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


    $("#DivLinkBankAccount").click(function () {
        debugger;
        var ServiceId = 49;
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
                    setTimeout(
                        function () {
                            window.location.href = "/MyPayUser/MyPayUserLoadWallet";
                            $('#AjaxLoader').hide();
                        }, 100);
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



    $("#btnOkPopup").click(function (e) {
        debugger;
        var Amount = $("#txtAmount").val();
        if (Amount == "") {
            $("#txnMsg").html("Please enter Amount");
            $("#DivErrMessage").modal("show");
            return false;
        }
        var isnum = /^\d+$/.test(Amount);
        if (!isnum) {
            $("#txnMsg").html("Invalid Amount");
            $("#DivErrMessage").modal("show");
            return false;
        }
        if (parseFloat(Amount) < 10) {
            $("#txnMsg").html("Minimum amount should be  Rs. 10 ");
            $("#DivErrMessage").modal("show");
            return false;
        }

        var Purpose = $("#selectPurpose option:selected").text();
        if (Purpose == "0") {
            $("#txnMsg").html("Please select Purpose");
            $("#DivErrMessage").modal("show");
            return false;
        }
        var BankCode = $("#hfBankCode").val();
        if (BankCode == "" && (BankCode == "mbanking" || BankCode == "ebanking")) {
            $("#txnMsg").html("Please select Bank");
            $("#DivErrMessage").modal("show");
            return false;
        }
        var Remarks = Purpose;
        $("#dvMessageSpan").html("");
        $('#txnMsg').html("");
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                var TransferType = $("#hfTransferType").val();
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/GetLoadFundsURL",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"TransferType":"' + TransferType + '","amount":"' + Amount + '","remarks":"' + Remarks + '","particulars":"' + Purpose + '","code":"' + BankCode + '"}',
                    success: function (response) {
                        debugger;
                        try {
                            jsonData = $.parseJSON(response);
                            window.location.href = jsonData.Details;
                            // $("#ifrm").attr("src", jsonData.Details);
                            //$("#DivPaymentGateway").show();
                            //$("#DivMobileInfoTab").hide();
                            //$("#DivBankAll").hide();
                            //$("#DivAccount").hide();
                            //$('#AjaxLoader').hide();

                        } catch (e) {

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
        e.preventDefault();
        e.stopPropagation();
        return false;
    });

    $("#btnMobileProceed").click(function () {
        debugger;

        if (ErrorMessage == true) {
            return false;
        }


        var MobileNo = $("#MobileNo").val();
        if (MobileNo == "") {
            $("#txnMsg").html("Please enter MobileNo");
            $("#DivErrMessage").modal("show");
            return false;
        }
        var Amount = $("#MobileAmount").val();
        if (Amount == "") {
            $("#txnMsg").html("Please enter Amount");
            $("#DivErrMessage").modal("show");
            return false;
        }

        if (parseFloat(Amount) < 10) {
            $("#txnMsg").html("Minimum amount should be  Rs. 10 ");
            $("#DivErrMessage").modal("show");
            return false;
        }

        var Purpose = $("#ddlMobilePurpose").val();
        if (Purpose == "0") {
            $("#txnMsg").html("Please select Purpose");
            $("#DivErrMessage").modal("show");
            return false;
        }
        var Remarks = $("#MobileRemarks").val();
        if (Remarks == "") {
            $("#txnMsg").html("Please enter Remarks");
            $("#DivErrMessage").modal("show");
            return false;
        }
        $("#dvMessage").html("");
        $('#txnMsg').html("");
        $("#FundRequestConfirmPopup").modal("show");

        var purposeTxt = $("#ddlMobilePurpose option:selected").text();
        $("#MobileRequestName").html($("#MobileAccountHolderName").val());
        $("#MobileRequestMobilePopup").html($("#MobileNo").val());
        $("#MobileRequestAmountPopup").html($("#MobileAmount").val());
        $("#MobileRequestPurposePopup").html(purposeTxt);
        $("#MobileRequestRemarksPopup").html($("#MobileRemarks").val());
    });



    $("#btnOkFundRequestPopup").click(function () {
        $("#FundRequestConfirmPopup").modal("hide");
        $('#DivPin').modal('show');
        $("#btnPinSubmit").html("Proceed");
    });

    $("#btnPinSubmit").click(function () {
        $('#AjaxLoader').show();
        var MobileAmount = $("#MobileAmount").val()
        var MobileNo = $("#MobileNo").val()
        var MobileRemarks = $("#MobileRemarks").val()
        var Pin = $("#Pin").val()
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/RequestTransferByphone",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Amount":"' + MobileAmount + '","RecipientPhone":"' + MobileNo + '","Remarks":"' + MobileRemarks + '","Mpin":"' + Pin + '"}',
                    success: function (response) {
                        debugger;
                        var arr = $.parseJSON(response);
                        if (arr['responseMessage'] == "success") {
                            $("#Pin").val("");
                            $("#DivPin").modal("hide");
                            $('#PaymentSuccess').modal('show');
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $("#DivPin").modal("hide");
                            $("#Pin").val("");
                            $('#AjaxLoader').hide();
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
    });

});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});


