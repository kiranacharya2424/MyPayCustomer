var WebResponse = '';
$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            UserDetail();
        }, 10);
});

function UserDetail() {
    var objBank = '';
    $("#dvMsg").html("");
    $("#dvMessageModal").modal('hide');

    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserProfileDetails",
        data: null,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            if (response != null) {

                var jsonData;
                var IsValidJson = false;
                try {
                    jsonData = $.parseJSON(response);
                    IsValidJson = true;
                }
                catch (err) {

                }
                if (jsonData['IsKycVerified'] == '3') {
                    $("#DivBankAll").css("display", "block");
                    $("#UserName").text(jsonData['Name']);
                    $("#MobileNumber").text(jsonData['ContactNumber']);
                    LoadBank();
                }
                else {
                    $('#KYCPopup').modal('show');
                    $("#DivBankAll").css("display", "none");
                    $("#dvMsg").html("Your KYC Should be approved to link a bank account");
                   
                }
                $("#table tbody").append(objBank);
            }
            else {
                $("#dvMsg").html("Something went wrong. Please try again later.");
                $("#dvMessageModal").modal('show');

                return false;
            }
        },
        failure: function (response) {
            $("#dvMsg").html(response.responseText);
            $("#dvMessageModal").modal('show');

            return false;
        },
        error: function (response) {
            $("#dvMsg").html(response.responseText);
            $("#dvMessageModal").modal('show');

            return false;
        }
    });

    $('#AjaxLoader').hide();
}
function LoadBank() {
    var objBank = '';
    $.ajax({
        type: "POST",
        url: "/MyPayUser/GetLinkBankList",
        data: null,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            if (response != null) {

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
                        //objBank += '<div class="DivBankData" style="cursor:pointer">';
                        //objBank += '<img id="BankLogo" src=' + jsonData.data[i].LogoUrl + ' width="30" style="margin-right:25px;">';
                        //objBank += "<input type='hidden' id='BankName' value='" + jsonData.data[i].InstitutionName + "'>";
                        //objBank += jsonData.data[i].InstitutionName;
                        //objBank += "<input type='hidden' id='BankCode' value='" + jsonData.data[i].InstrumentCode + "'><hr>";
                        //objBank += '</div>';


                        objBank += '<div class="col-md-6 col-lg-3 mb-3 DivBankData ">';
                        objBank += '<div class="text-center p-3 border round">';
                        objBank += '<a href="javascript:void(0)"><img id="BankLogo" src=' + jsonData.data[i].LogoUrl + ' width="100" style="width:100px;" /></a>';
                        objBank += "<input type='hidden' id='BankName' value='" + jsonData.data[i].InstitutionName + "'>";
                        objBank += "<input type='hidden' id='BranchName' value='" + jsonData.data[i].InstitutionName + "'>";
                        objBank += "<input type='hidden' id='BranchId' value='" + jsonData.data[i].InstrumentCode + "'>";
                        objBank += jsonData.data[i].InstitutionName;
                        objBank += "<input type='hidden' id='BankId' value='" + jsonData.data[i].InstrumentCode + "'>";
                        objBank += "<input type='hidden' id='BankCode' value='" + jsonData.data[i].InstrumentCode + "'><hr>";

                        objBank += '</div>';
                        objBank += '</div>';

                    }
                    $("#dvRecordsDisplay").html(objBank);
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                        $("#dvMessageModal").modal('show');

                    }
                }
            }
            else {
                $("#dvMsg").html("Something went wrong. Please try again later.");
                $("#dvMessageModal").modal('show');

                return false;
            }
        },
        failure: function (response) {
            $("#dvMsg").html(response.responseText);
            $("#dvMessageModal").modal('show');

            return false;
        },
        error: function (response) {
            $("#dvMsg").html(response.responseText);
            $("#dvMessageModal").modal('show');

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
    });
}

$("#btnProceed").click(function () {
    var AccountNo = $("#AccountNo").val();
    if (AccountNo == "") {
        $("#dvMsg").html("Please Enter AccountNo");
        $("#errormsg").html("Please Enter AccountNo");
        return false;
    }

    var BankCode = $("#hfBankCode").val();
    if (BankCode == "") {
        $("#dvMsg").html("Please Select Bank");
        $("#errormsg").html("Please Select Bank");
        return false;
    }
    $('#AjaxLoader').show();
    $('#dvMsg').html("");
    $("#dvMessageModal").modal('hide');


    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/LinkBankAccount",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"BankCode":"' + BankCode + '","AccountNumber":"' + AccountNo + '"}',
                success: function (response) {
                    try {

                        var arr = $.parseJSON(response);
                        if (arr['ReponseCode'] == "1") {
                            alert(arr['Details']);
                            window.location.href = '/MyPayUser/MyPayUserLinkedAccount';
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {

                            $('#AjaxLoader').hide();
                            $('#dvMsg').html(arr['Details']);
                            $("#dvMessageModal").modal('show');
                            $('#txnMsg').html(arr['Details']);
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
});

function BankAllBackClick() {
    $("#DivBankAll").show(300);
    $("#DivAccountInfo").hide(300);
    $("#dvMsg").html("");
}


function BankLinkBackClick() {
    history.go(-1);
    $("#dvMsg").html("");
}


