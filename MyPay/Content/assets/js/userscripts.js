function PreviewImage(input, divid, size) {
    if (input.files && input.files[0]) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray(input.files[0].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
            //ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning", '');
            ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
        if (parseInt(input.files[0].size / 1000) > size) {
            //ViewMessage("Allowed file size exceeded. (Max. " + size + " KB)", "warning", '');
            alert("Allowed file size exceeded. (Max. " + size + " KB)", "warning");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            if (input.files[0].name.split('.').pop().toLowerCase() != "pdf" && input.files[0].name.split('.').pop().toLowerCase() != "txt") {
                $('#target' + divid).attr('src', e.target.result);
                $('#ContentPlaceHolder1_target' + divid).attr("src", e.target.result);
            }
            else {
                $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            }
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function PreviewImageOfflineBinaryData(input, divid, size) {
    debugger;
    if (input.files && input.files[0]) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray(input.files[0].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
            //ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning", '');
            ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
        if (parseInt(input.files[0].size / 1000) > size) {
            //ViewMessage("Allowed file size exceeded. (Max. " + size + " KB)", "warning", '');
            ViewMessage("Allowed file size exceeded. (Max. " + size + " KB)", "warning");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            if (input.files[0].name.split('.').pop().toLowerCase() != "pdf" && input.files[0].name.split('.').pop().toLowerCase() != "txt") {
                $('#target' + divid).attr('src', e.target.result);
                $('#ContentPlaceHolder1_target' + divid).attr("src", e.target.result);
                $("#hdnImageDataSeleted").attr("value", e.target.result);
            }
            else {
                $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            }
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(document).ready(function () {
    $("#previewImage").click(function () {
        debugger;
        setTimeout(
            function () {
                $(".mfp-img").attr("src", $("#hdnImageDataSeleted").val());
                $('.mfp-container.mfp-image-holder.mfp-s-error').removeClass('mfp-container mfp-image-holder mfp-s-error').addClass('mfp-container mfp-s-ready mfp-image-holder');
            }, 5);

    });
    $("#StateId").on("change", function () {
        var StateId = $("#StateId").val();
        var value = $("#StateId option:selected");
        $("#State").val(value.text());
        if (StateId != 0) {
            $.ajax({
                url: "/User/GetDistrictList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"StateId":"' + StateId + '"}',
                success: function (data) {

                    $('#DistrictId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#DistrictId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            alert("Please select State");
        }
    });

    $("#CurrentStateId").on("change", function () {
        var CurrentStateId = $("#CurrentStateId").val();
        var value = $("#CurrentStateId option:selected");
        $("#CurrentState").val(value.text());
        if (CurrentStateId != 0) {
            $.ajax({
                url: "/User/GetDistrictList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"StateId":"' + CurrentStateId + '"}',
                success: function (data) {
                    debugger;

                    $('#CurrentDistrictId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#CurrentDistrictId");
                    $.each(data, function (index, item) {
                        debugger;
                        list.append(new Option(item.Text, item.Value));
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            alert("Please select CurrentState");
        }
    });

    $("#DistrictId").on("change", function () {
        debugger;
        var DistrictId = $("#DistrictId").val();
        var value = $("#DistrictId option:selected");
        $("#City").val(value.text());
        if (DistrictId != 0) {
            $.ajax({
                url: "/User/GetMunicipalityList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"DistrictId":"' + DistrictId + '"}',
                success: function (data) {
                    debugger;

                    $('#MunicipalityId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#MunicipalityId");
                    $.each(data, function (index, item) {
                        debugger;
                        list.append(new Option(item.Text, item.Value));
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            alert("Please select District");
        }
    });

    $("#CurrentDistrictId").on("change", function () {
        var CurrentDistrictId = $("#CurrentDistrictId").val();
        var value = $("#CurrentDistrictId option:selected");
        $("#CurrentDistrict").val(value.text());
        if (CurrentDistrictId != 0) {
            $.ajax({
                url: "/User/GetMunicipalityList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"DistrictId":"' + CurrentDistrictId + '"}',
                success: function (data) {
                    debugger;

                    $('#CurrentMunicipalityId')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#CurrentMunicipalityId");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            alert("Please select District");
        }
    });

    $("#MunicipalityId").on("change", function () {
        var value = $("#MunicipalityId option:selected");
        $("#Municipality").val(value.text());
    });

    $("#CurrentMunicipalityId").on("change", function () {
        var value = $("#CurrentMunicipalityId option:selected");
        $("#CurrentMunicipality").val(value.text());
    });

    $("#IssueFromStateID").on("change", function () {
        debugger;
        var StateId = $("#IssueFromStateID").val();
        var value = $("#IssueFromStateID option:selected");
        $("#IssueFromStateName").val(value.text());
        //$("#IssueFromState").val(value.text());
        if (StateId != 0) {
            $.ajax({
                url: "/User/GetDistrictList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"StateId":"' + StateId + '"}',
                success: function (data) {

                    $('#IssueFromDistrictID')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#IssueFromDistrictID");
                    $.each(data, function (index, item) {
                        list.append(new Option(item.Text, item.Value));
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseTex);
                    return false;
                }
            });
        }
        else {
            alert("Please select State");
        }
    });

    $("#IssueFromDistrictID").on("change", function () {
        var value = $("#IssueFromDistrictID option:selected");
        $("#IssueFromDistrictName").val(value.text());
    });

    $("#btnSubmitResetUserPassword").click(function (e) {
        var txtpassword = $("#password").val();
        var passwordconfirm = $("#passwordconfirm").val();
        if (txtpassword == "") {
            $("#errormsg").html('Please enter password.');
            e.preventDefault();
            e.stopPropagation();
            return false;

        }
        else if (passwordconfirm == "") {
            $("#errormsg").html('Please confirm password');
            e.preventDefault();
            e.stopPropagation();
            return false;

        }
        else if (txtpassword != passwordconfirm) {
            $("#errormsg").html('Password donot match. Please try again.');
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
    });

    $("#btnSubmitAdminTransfer").click(function () {
        if ($("#TransactionAmount").val() == "" || $("#TransactionAmount").val() == "0" || parseFloat($('#TransactionAmount').val()) == 0) {
            $("#dvMessage").html("Please enter Amount.");
            return false;
        }
        else if ($("#TransactionType").val() == "" || $("#TransactionType").val() == "0") {
            $("#dvMessage").html("Please Select Transaction Type.");
            return false;
        }
        else {
            if ($("#Type").val() == "0") {
                return confirm('This will update user wallet. Do you want to continue ?');
            }
            else if ($("#Type").val() == "1") {
                return confirm('This will update user MP Coins. Do you want to continue ?');
            }
        }

    });
});

function BlockUnblockUser(memberid, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/User/UserBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MemberId":"' + memberid + '"}',
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    debugger;
                    $("#dvMsgSuccess").html("successfully updated");

                    var tableId = $(this).data("table");
                    e.preventDefault();
                    e.stopPropagation();
                    window.location.reload();
                    return false;
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

}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}