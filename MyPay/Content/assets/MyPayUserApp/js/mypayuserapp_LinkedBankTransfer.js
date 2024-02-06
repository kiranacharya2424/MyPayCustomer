$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            GetBankDetail();
        }, 10);
});

function GetBankDetail() {
    var objBank = '';
    $("#dvMsg").html("");
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
                            if (jsonData.data[i].IsPrimary = true) {
                                objBank += '<div class="paymentoption">';
                                objBank += '<img width="31" src=' + jsonData.data[i].ICON_NAME + ' >' + jsonData.data[i].BankName + ' <br />';
                                objBank += jsonData.data[i].Name + '<input type="hidden" id="hfBankId" value=' + jsonData.data[i].Id+' /><br />';
                                objBank += jsonData.data[i].AccountNumber + '<br />';
                                objBank += '</div>';
                            }
                        }
                        $("#DivInfo").css("display", "block");
                    }
                    else {
                        $("#DivInfo").css("display", "none");
                        $("#dvMsg").html("Add one Account to Add Balance");
                    }
                    $("#dvRecordsDisplay").append(objBank);
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

$("#btnProceed").click(function () {
    var Amount = $("#Amount").val();
    if (Amount == "") {
        $("#dvMsg").html("Please enter Amount");
        return false;
    }
    var Remarks = $("#Remarks").val();
    if (Remarks == "") {
        $("#dvMsg").html("Please enter Remarks");
        return false;
    }
    var BankId = $("#hfBankId").val();
    if (BankId == "") {
        $("#dvMsg").html("Please Select Bank");
        return false;
    }
    $("#dvMsg").html("");
    $("#DivPin").show(200);
    $("#DivInfo").hide(200);
});

$("#btnPin").click(function () {
    LinkBankTransfer();
})

function LinkBankTransfer() {
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
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserLinkedBankTransfer",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + $("#Amount").val() + '","Remarks":"' + $("#Remarks").val() + '","BankId":"' + $("#hfBankId").val() + '","Mpin":"' + Pin + '"}',
                success: function (response) {
                    var jsonData = $.parseJSON(response);
                    if (jsonData['ReponseCode'] == '1') {
                        alert(jsonData['Message']);
                        window.location.href = '/MyPayUser/MyPayUserTransactions';
                        $('#AjaxLoader').hide();
                    }
                    else {
                        $("#dvMsg").html(jsonData['Message']);
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

function BackClick() {
    $("#DivInfo").show(300);
    $("#DivPin").hide(300);
}