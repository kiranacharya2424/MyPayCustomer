
var BankJsonData = '';
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            GetBankDetail();
            GetBankDetailOthers();

        }, 10);
});

function Back(HideDiv, ShowDiv) {
    $("#" + ShowDiv).show(100);
    $("#" + HideDiv).hide(100);
    $("#dvMessage").html("");
    $("html, body").animate({ scrollTop: "0" });
}

function DisplayLinkedBankDetails(Id) {
   

    var SelectedBank = BankJsonData.data.find(x => x.Id == Id);

    $("#lblUserName").text(SelectedBank.Name);
    $("#lblAccountNumber").text(SelectedBank.AccountNumber);
    $("#lblMobileNumber").html($("#hfContactNumber").val());
    $("#ImgBankLogo").attr("src", (SelectedBank.ICON_NAME));
    $("#SpanBankName").text(SelectedBank.BankName);
    $("#spnAccountNumber").text(SelectedBank.AccountNumber);
    $("#spnBankName").text(SelectedBank.BankName);
    $("#spnMobileNumber").html($("#hfContactNumber").val());
    $("#BankCode").html(SelectedBank.BankCode);
    $("#BranchId").html(SelectedBank.BranchId);
    $("#BranchName").html(SelectedBank.BranchName);
    $("#BankId").html(SelectedBank.Id);
    Back('MyLinkedAccounts', 'MyLinkedAccountsDetails');
}

$("#btnProceedRemoveConfirm").click(function () {
    $("#dvRemoveBankConfirm").modal("show");
});
$("#btnProceedRemove").click(function () {
    RemoveBankAccount();
});
$("#btnProceedPrimaryConfirm").click(function () {
    $("#dvProceedPrimaryConfirm").modal("show");
});
$("#btnProceedPrimary").click(function () {
    BankAccountConvertPrimary();
});
function BankAccountConvertPrimary() {
    debugger;
    $('#AjaxLoader').show();
    $('#dvMsg').html("");
    $("#dvMessageModal").modal('hide');
    var BankCode = $("#BankCode").html();
    var BankName = $("#spnBankName").html();
    var BranchId = $("#BranchId").html();
    var BranchName = $("#BranchName").html();
    var AccountNumber = $("#spnAccountNumber").html();
    debugger;
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/BankAccountConvertPrimary",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"BankCode":"' + BankCode + '","BankName":"' + BankName + '","BranchId":"' + BranchId + '","BranchName":"' + BranchName + '","AccountNumber":"' + AccountNumber + '"}',
                success: function (response) {
                    try {
                        debugger;
                        var arr = $.parseJSON(response);
                        if (arr['ReponseCode'] == "1") {
                            $('#AjaxLoader').hide();
                            $('#txnMsgSuccess').html(arr['Message']);
                            $("#PaymentSuccess").modal('show');
                            $("#dvProceedPrimaryConfirm").modal("hide");
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $('#txnMsg').html(arr['Message']);
                            $("#dvMessageModal").modal('show');
                            $("#dvProceedPrimaryConfirm").modal("hide");
                        }
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
}

function RemoveBankAccount() {
    $('#AjaxLoader').show();
    $('#dvMsg').html("");
    $("#dvMessageModal").modal('hide');
    var BankId = $("#BankId").html();
    debugger;
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/RemoveBankAccount",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"BankId":"' + BankId + '"}',
                success: function (response) {
                    try {
                        debugger;
                        var arr = $.parseJSON(response);
                        if (arr['ReponseCode'] == "1") {
                            $('#AjaxLoader').hide();
                            $('#txnMsgSuccess').html(arr['Message']);
                            $("#PaymentSuccess").modal('show');
                            $("#dvRemoveBankConfirm").modal("hide");
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $('#txnMsg').html(arr['Message']);
                            $("#dvMessageModal").modal('show');
                            $("#dvRemoveBankConfirm").modal("hide");
                        }
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
}

function GetBankDetail() {

    var objBank = '';
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

                
                    var jsonData = $.parseJSON(response);
             

                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            if (jsonData.data[i].IsPrimary == true) {

                                objBank += '<div class="paymentoption" onclick="DisplayLinkedBankDetails(' + jsonData.data[i].Id + ');">';
                                objBank += '<div class="banklinked mb-4">';
                                objBank += '<span>';
                                objBank += '<img id="imgLogo" src="' + jsonData.data[i].ICON_NAME + '" width="30" alt=""> ' + '<span id="UserBankName"> ' + jsonData.data[i].BankName + '</span>';
                                objBank += '</span>';
                                if (jsonData.data[i].IsPrimary = 'true') {
                                    objBank += ' <br><span style="color: green;font-size:15px;">Active</span> ';
                                }

                                objBank += '<div class="">';
                                objBank += '<div class="d-block"><small id="UserName" class="d-block text-soft">' + jsonData.data[i].Name + ' </small>' + jsonData.data[i].AccountNumber + ' </div>';


                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '<span id="BankCode" style="display:none"> ' + jsonData.data[i].BankCode + '</span>';
                                objBank += '<span id="BankName" style="display:none"> ' + jsonData.data[i].BankName + '</span>';
                                objBank += '<span id="BranchId" style="display:none"> ' + jsonData.data[i].BranchId + '</span>';
                                objBank += '<span id="BranchName" style="display:none"> ' + jsonData.data[i].BranchName + '</span>';
                                objBank += '<span id="AccountNumber" style="display:none"> ' + jsonData.data[i].AccountNumber + '</span>';
                                objBank += '<span id="UserAccountName" style="display:none"> ' + jsonData.data[i].Name + '</span>';
                                objBank += '<span id="BankId" style="display:none"> ' + jsonData.data[i].Id + '</span>';
                            }
                        }
                        $("#DivPrimaryAccount").css("display", "block");


                    }
                    else {
                        $("#DivNoAccount").css("display", "block");

                    }
                    $("#dvRecordsDisplay").html(objBank);
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
function GetBankDetailOthers() {
   
    var objBank = '';
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetBankDetailOthers",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {
                 
                    var jsonData = $.parseJSON(response);

                    BankJsonData = jsonData;
                    console.log("other banks==", jsonData);
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            if (jsonData.data[i].IsPrimary == false) {


                                objBank += '<div class="paymentoption" onclick="DisplayLinkedBankDetails(' + jsonData.data[i].Id + ');">';
                                objBank += '<div class="banklinked mb-4">';
                                objBank += '<span>';
                                objBank += '<img id="imgLogo" src="' + jsonData.data[i].ICON_NAME + '" width="30" alt=""> ' + '<span id="UserBankName"> ' + jsonData.data[i].BankName + '</span>';
                                objBank += '</span>';
                                if (jsonData.data[i].IsPrimary = 'true') {
                                    objBank += ' <br><span style="color: green;font-size:15px;">Active</span> ';
                                }

                                objBank += '<div class="">';
                                objBank += '<div class="d-block"><small id="UserName" class="d-block text-soft">' + jsonData.data[i].Name + ' </small>' + jsonData.data[i].AccountNumber + ' </div>';


                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '<span id="BankCode" style="display:none"> ' + jsonData.data[i].BankCode + '</span>';
                                objBank += '<span id="BankName" style="display:none"> ' + jsonData.data[i].BankName + '</span>';
                                objBank += '<span id="BranchId" style="display:none"> ' + jsonData.data[i].BranchId + '</span>';
                                objBank += '<span id="BranchName" style="display:none"> ' + jsonData.data[i].BranchName + '</span>';
                                objBank += '<span id="AccountNumber" style="display:none"> ' + jsonData.data[i].AccountNumber + '</span>';
                                objBank += '<span id="UserAccountName" style="display:none"> ' + jsonData.data[i].Name + '</span>';
                                objBank += '<span id="BankId" style="display:none"> ' + jsonData.data[i].Id + '</span>';
                            }
                        }
                        $("#DivOtherAccount").css("display", "block");


                    }

                    $("#DivOtherAccountList").html(objBank);
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

$("#btnLinkBankAccount").click(function () {
    if (($("#hfKYCStatus").val()) == '3') {
        window.location.href = '/MyPayUser/MyPayUserBankListAll'
    }
    else {
        $('#KYCPopup').modal('show');
    }
});


$("#btnLinkIPSBankAccount").click(function () {
    var MemberId = $("#hfMemberId").val();
    var ContactNumber = $("#hfContactNumber").val();
    if (($("#hfKYCStatus").val()) == '3') {
        window.location.href = '/UserAuthorizations/IPSLinkBank?MemberId=' + MemberId + "&ContactNumber=" + ContactNumber;
    }
    else {
        $('#KYCPopup').modal('show');
    }
});



$("#btnsubmit").click(function () {
    $("#errormsg").html('');
    UserEmail = $("#emailid").val();
    if (UserEmail == "") {
        $("#errormsg").html("Please Enter your Email");
        return false;
    }
    else if (!isEmail(UserEmail)) {
        $("#errormsg").html("Please enter a valid Email ID");
        return false;
    }
    $('#AjaxLoader').show();
    debugger;
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/EmailVerify",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Email":"' + UserEmail + '"}',
                success: function (response) {

                    debugger;
                    var jsonData;
                    jsonData = response;
                    if (jsonData == "success") {
                        $("#div_verify_Email").hide();

                        $("#div_verify_otp").show();
                        $("#sendotp").hide();

                        var counter = 10;
                        setInterval(function () {
                            counter--;
                            if (counter >= 0) {
                                span = document.getElementById("clock");
                                span.innerHTML = counter;

                            }
                            if (counter === 0) {

                                $("#sendotp").show();
                                $(".resend_otp").hide();
                                clearInterval(counter);
                            }
                        }, 1000);
                    }
                    else {
                        $("#errormsg").html(jsonData);
                    }

                    $('#AjaxLoader').hide();

                },
                failure: function (response) {
                    $('#AjaxLoader').hide();

                },
                error: function (response) {
                    $('#AjaxLoader').hide();

                }
            });
        }, 100);

});

$("#sendotp").click(function () {
    debugger;

    $("#errormsg").html('');
    UserEmail = $("#emailid").val();
    if (UserEmail == "") {
        $("#errormsg").html("Please Enter your Email");
        return false;
    }
    else if (!isEmail(UserEmail)) {
        $("#errormsg").html("Please enter a valid Email ID");
        return false;
    }
    $('#AjaxLoader').show();
    debugger;
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/EmailVerify",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Email":"' + UserEmail + '"}',
                success: function (response) {

                    debugger;
                    var jsonData;
                    jsonData = response;
                    if (jsonData == "success") {
                        $("#div_verify_Email").hide();

                        $("#div_verify_otp").show();
                        $("#sendotp").hide();
                        $(".resend_otp").show();

                        var counter = 10;
                        setInterval(function () {
                            counter--;
                            if (counter >= 0) {
                                span = document.getElementById("clock");
                                span.innerHTML = counter;

                            }
                            if (counter === 0) {

                                $("#sendotp").show();
                                $(".resend_otp").hide();
                                clearInterval(counter);
                            }
                        }, 1000);
                    }
                    else {
                        $("#errormsg").html(jsonData);
                    }

                    $('#AjaxLoader').hide();

                },
                failure: function (response) {
                    $('#AjaxLoader').hide();

                },
                error: function (response) {
                    $('#AjaxLoader').hide();

                }
            });
        }, 100);
});

$("#verifyotp").click(function () {
    $("#errormsg").html('');
    var OTP = $("#otp").val();
    if (OTP == "") {
        $("#errormsg").html("Please Enter otp");
        return false;
    }
    $('#AjaxLoader').show();
    debugger;
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MypayUseOTPVerify",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"OTP":"' + OTP + '","Email":"' + UserEmail + '"}',
                success: function (response) {

                    debugger;
                    var jsonData;
                    jsonData = response;

                    if (jsonData == "success") {
                        $('#verifySuccess').html("Success");
                        $("#dvVerifyModalSuccess").modal('show');
                    }
                    else {
                        $("#errormsg").html(jsonData);
                    }
                    $('#AjaxLoader').hide();
                },
                failure: function (response) {
                    $('#AjaxLoader').hide();

                },
                error: function (response) {
                    $('#AjaxLoader').hide();

                }
            });
        }, 100);
});

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
$("#dvVerifyModalSuccessClose").click(function () {
    location.reload(true);
})