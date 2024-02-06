
function DeleteWalletCurrency(Id) {
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    if (confirm('This Action Will Delete your selected wallet. Do You Really Want To Continue ??')) {
        $('#AjaxLoader').show();
        setTimeout(
            function () {

                $.ajax({
                    type: "POST",
                    url: "/Remittance/DeleteWalletCurrencies",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Id":"' + Id + '"}',
                    success: function (response) {
                        if (response == "success") {
                            alert("Successfully Delete Selected Wallet");
                            location.reload();
                            //GetWalletList();
                            $('#AjaxLoader').hide();
                        }
                        else {
                            alert(response);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    }
                });

                //$('#AjaxLoader').hide();
            }, 100);
    }
}

function AddWalletCurrency() {
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    var CurrencyId = $("#CurrencyId option:selected").val();
    var Type = $("#hdnType").val();
    var CurrencyName = $("#CurrencyId option:selected").text();
    var MerchantUniqueId = $("#hdnMerchantId").val();
    if (CurrencyId != "" || CurrencyId != "0") {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/Remittance/AssignWalletCurrencies",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"CurrencyId":"' + CurrencyId + '","CurrencyName":"' + CurrencyName + '","MerchantUniqueId":"' + MerchantUniqueId + '","Type":"' + Type + '"}',
                    success: function (response) {
                        if (response == "success") {
                            //GetWalletList();
                            alert("Successfully Assign Selected Wallet");

                            location.reload();
                            //$("#dvSuccessMsg").html("Successfully Assign Selected Wallet");
                            $("#CurrencyId").val("0");
                            $('#AjaxLoader').hide();
                            $('#AddWallet').modal('hide');
                        }
                        else {
                            alert(response);
                            $('#AjaxLoader').hide();
                            return false;
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $('#AjaxLoader').hide();
                    }
                });
                //$('#AjaxLoader').hide();
            }, 100);
    }
    else {
        $("dvFailedMsg").html("Please select currency");
    }
}

function AddWalletFund(Id, CurrencyId, MerchantUniqueId, Type, TypeName, CurrencyName) {
    $('#AddFund').modal('show');
    $("#hdnCurrencyName").val(CurrencyName);
    $("#hdnCurrencyId").val(CurrencyId)
    $("#txtTxnId").val("");
    $("#txtRemarks").val("");
    $("#txtAmount").val("");
    $("#dvPopupMsg").html("");
    $(".custom-file-label").html("");
    $("#targetReceiptImage").attr("src", "Content/assets/Images/noimageblank.png");
}


function isNumberKey(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1) {
        var range = document.selection.createRange();

        if (range.text != "") {
        }
        else {
            var number = el.value.split('.');
            if (number.length == 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
}


function SubmitAddFund() {
    debugger;
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    var Sign = $("#drpSign option:selected").val();
    var TxnId = $("#txtTxnId").val();
    //var TxnFile = $("#TxnReceiptFile").val();
    var Remarks = $("#txtRemarks").val();
    var Amount = $("#txtAmount").val();
    var filename = $("#hdnimage").val();
    if (Sign == "1") {
        if (TxnId == "") {
            $("#dvPopupMsg").html("Please enter Bank Txn. Id for Credit Fund.");
            return false;
        }
        if (filename == "") {
            $("#dvPopupMsg").html("Please upload Receipt for Credit Fund.");
            return false;
        }
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/Remittance/AddWalletFund",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"CurrencyId":"' + $("#hdnCurrencyId").val() + '","MerchantUniqueId":"' + $("#hdnMerchantId").val() + '","Type":"' + $("#hdnType").val() + '","Sign":"' + Sign + '","TxnId":"' + TxnId + '","Remarks":"' + Remarks + '","Amount":"' + Amount + '","CurrencyName":"' + $("#hdnCurrencyName").val() + '","ReceiptFileName":"' + filename + '"}',
                success: function (response) {
                    if (response == "success") {
                        if (Sign == "1") {
                            alert("Successfully Credit Fund in Selected Wallet");
                            $("#dvPopupMsg").html("Successfully Credit Fund in Selected Wallet");
                        }
                        else if (Sign == "2") {
                            alert("Successfully Debit Fund from Selected Wallet");
                            $("#dvPopupMsg").html("Successfully Debit Fund from Selected Wallet");
                        }
                        GetWalletList();
                        $('#AddFund').modal('hide');
                        $("#txtTxnId").val("");
                        $("#txtRemarks").val("");
                        $("#txtAmount").val("");
                        $("#CurrencyId").val("0");
                        $('#AjaxLoader').hide();
                    }
                    else {
                        alert(response);
                        $('#AjaxLoader').hide();
                        return false;
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                    $('#AjaxLoader').hide();
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                    $('#AjaxLoader').hide();
                }
            });
            //$('#AjaxLoader').hide();
        }, 100);
}

function AddWalletPopup() {
    $('#AddWallet').modal('show');
}

function UploadAjaxImage(obj, foldername, divid, size, nocheckfile) {
    debugger;
    var hdnimage = $("#hdnimage").val();
    if (typeof (hdnimage) == "undefined") {
        hdnimage = $("#ContentPlaceHolder1_hdnimage").val();
    }
    var hdnfilename = $("#hdnfilename").val();
    if (typeof (hdnfilename) == "undefined") {
        hdnfilename = $("#ContentPlaceHolder1_hdnfilename").val();
    }
    var splitimage;
    if (hdnimage.indexOf(',') > -1) {
        splitimage = hdnimage.toString().split(',');
        if (splitimage.length == 1) {
            alert("Maximum limit of images is 1 and you have already uploaded 1 image");
            $(obj).replaceWith($(obj).val('').clone(true));
            $('#target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
        }
    }

    var fd = new FormData();
    var file = $(obj)[0].files[0];
    var filename = file.name;
    if (file) {
        if (nocheckfile == 1) {
            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray(file.name.split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(obj).replaceWith($(obj).val('').clone(true));
                $('#target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
                return false;
            }
        }
        if (parseInt(file.size / 1000) > size) {
            alert("Allowed file size exceeded. (Max. " + size + " KB)");
            $(obj).replaceWith($(obj).val('').clone(true));
            $('#target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
            return false;
        }
    }
    fd.append("file", file);
    fd.append("foldername", foldername);
    fd.append("Method", "UploadImage");
    $.ajax({
        url: '/UploadImage.ashx',
        type: "POST",
        processData: false,
        contentType: false,
        data: fd,
        success: function (result) {

            var splitstr = result.toString().split('!@!');
            if (result.indexOf('Success') > -1) {

                if (splitstr[0] == "Success") {
                    if (hdnimage == "") {
                        hdnimage = splitstr[1];
                        hdnfilename = filename;
                    }
                    else {
                        hdnimage = splitstr[1];
                        hdnfilename = filename;
                    }
                    debugger;
                    $(".uploadimagetext").html(hdnfilename);
                    $("#hdnfilename").val(hdnfilename);
                    $("#hdnimage").val(hdnimage);
                    $("#ContentPlaceHolder1_hdnimage").val(hdnimage);
                    $("#ContentPlaceHolder1_hdnfilename").val(hdnfilename);
                    $("progress").hide();
                    $('#target' + divid).attr("src", "Images/UploadReceiptRemittance/" + hdnimage);
                    $('#ContentPlaceHolder1_target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
                    //PreviewImage(obj, divid, 1000);
                }
                else {
                    $("progress").hide();
                    //$("#hdnimage").val("");
                    //$("#ContentPlaceHolder1_hdnimage").val("");
                    alert(splitstr[0]);
                    $(obj).replaceWith($(obj).val('').clone(true));
                    $('#target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
                    $('#ContentPlaceHolder1_target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
                }
            }
            else {
                //$("#hdnimage").val("");
                //$("#ContentPlaceHolder1_hdnimage").val("");
                $("progress").hide();
                alert(splitstr[0]);
                $(obj).replaceWith($(obj).val('').clone(true));
                $('#target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "Content/assets/Images/noimageblank.png");
            }
            return false;
        },
        xhr: function () {
            var fileXhr = $.ajaxSettings.xhr();
            if (fileXhr.upload) {
                $("progress").show();
                fileXhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        $(".fileProgress").attr({
                            value: e.loaded,
                            max: e.total
                        });
                    }
                }, false);
            }
            return fileXhr;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText);
            return false;
        }
    });
}


