$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            GetBankDetail();
        }, 10);
});

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
                            if (jsonData.data[i].IsPrimary = true) {
                                //objBank += '<div class="paymentoption">';
                                //objBank += '<img width="31" src=' + jsonData.data[i].ICON_NAME + ' >' + jsonData.data[i].BankName + ' <br />';
                                //objBank += '<input type="hidden" id="BankCode" value=' + jsonData.data[i].BankCode + '>';
                                //objBank += '<div class="content-light">';
                                //objBank += '<div style="text-align:left;">';
                                //if (jsonData.data[i].IsPrimary = 'true') {
                                //    objBank += '<b><span style="color: green;font-size:19px;">Active</span></b>';
                                //}
                                //objBank += '</div>';
                                //objBank += '</div>';
                                //objBank += '</div>';

                                //
                                objBank += '<div class="paymentoption">';
                                objBank += '<div class="banklinked mb-4">';
                                objBank += '<span>';
                                objBank += '<img src="' + jsonData.data[i].ICON_NAME + '" width="30" alt=""> ' + jsonData.data[i].BankName;
                                objBank += '</span>';
                                objBank += '<div class="">';
                                objBank += '<div class="d-block"><small class="d-block text-soft">' + jsonData.data[i].Name + ' </small>' + jsonData.data[i].AccountNumber+' </div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '<input type="hidden" id="BankCode" value=' + jsonData.data[i].BankCode + '>';
                                objBank += '<input type="hidden" id="BankId" value=' + jsonData.data[i].Id + '>';
                            }

                        }
                        $("#DivPrimaryAccount").css("display", "block");
                    }
                    else {
                        $("#DivNoAccount").css("display", "block");
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
        $("#txnMsg").html("Please enter Amount");
        $("#DivErrMessage").modal("show");
        return false;
    }
    if (parseFloat(Amount) < 10) {
        $("#txnMsg").html("Minimum amount should be  Rs. 10 ");
        $("#DivErrMessage").modal("show");
        return false;
    }
    var Remarks = $("#Remarks").val();
    if (Remarks == "") {
        $("#txnMsg").html("Please enter Remarks");
        $("#DivErrMessage").modal("show");
        return false;
    }
    var BankId = $("#BankId").val();
    if (BankId == "") {
        $("#txnMsg").html("Please select Bank");
        $("#DivErrMessage").modal("show");
        return false;
    }
    $("#DivPin").modal('show'); 
    $("#dvMessage").html("");
});

$("#btnPin").click(function () {
    var Amount = $("#Amount").val();
    if (Amount == "") {
        $("#txnMsg").html("Please enter Amount");
        $("#DivErrMessage").modal("show");

        return false;
    }
    var Remarks = $("#Remarks").val();
    if (Remarks == "") {
        $("#txnMsg").html("Please enter Remarks");
        $("#DivErrMessage").modal("show");

        return false;
    }
    var BankId = $("#BankId").val();
    if (BankId == "") {
        $("#txnMsg").html("Please select Bank");
        $("#DivErrMessage").modal("show");

        return false;
    }
    var Pin = $("#Pin").val();
    if (Pin == "") {
        $("#txnMsg").html("Please enter Pin");
        $("#DivErrMessage").modal("show");

        return false;
    }
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserLoadWallet",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + Amount + '","Remarks":"' + Remarks + '","BankId":"' + BankId + '","Mpin":"' + Pin + '"}',
                success: function (response) {
                    debugger;
                    var jsonData;
                    try {
                        jsonData = $.parseJSON(response);
                        if (jsonData != null) {
                            if (jsonData['ReponseCode'] == '1') {
                                alert(jsonData['Message']);
                                window.location.href = "/MyPayUser/MyPayUserLoadWallet";
                            }
                            else {
                                $("#txnMsg").html(jsonData['Message']);
                                $("#DivErrMessage").modal("show");

                            }
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $("#dvMessage").html(jsonData['Message']);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                    } catch (e) {
                        $("#dvMessage").html(response);
                        $('#AjaxLoader').hide();
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
});