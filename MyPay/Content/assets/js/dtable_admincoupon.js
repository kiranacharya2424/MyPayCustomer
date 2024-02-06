

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
$(document).ready(function () {
    BindDataTable("tbllist");
});

$(function () {
    $(".nav-item").click(function () {
        debugger;
        var tableId = $(this).data("table");
        BindDataTable(tableId);
    });
});

function BindDataTable(tableId) {
    debugger;
    var SelectedCoupenType = $("#drpCoupenType").val();
    if (SelectedCoupenType !== "0") {
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Coupons  "
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "searching": false,
                    "sorting": false,
                    "order": [[0, "desc"]],
                    "ajax": {
                        "url": "/Coupon/GetAdminCouponLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.ServiceId = $("#drpProviderServices").val();
                            data.CouponType = $("#drpCoupenType").val();
                            data.KycStatus = $("#KycStatus").val();
                            data.GenderStatus = $("#GenderStatus").val();
                            data.ScheduleStatus = $("#drpScheduleStatus").val();

                        },
                        error: function (xhr, error, code) {
                            if (xhr.status == 200) {
                                alert('Session Timeout. Please login again to continue.');
                            }
                            else {
                                alert("Something went wrong try again later");
                            }
                            location.reload();
                        }

                    },

                    "columns": [
                        { "data": "Sno.", "name": "Sno", "autoWidth": true, "bSortable": false },
                        { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": false },
                        { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.ServiceName !== "0") {
                                    return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>' + data.ServiceName.toUpperCase() + '</span> ';
                                }
                                else {
                                    return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>-</span> ';
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<textarea id="Title' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;" >' + data.Title + '</textarea>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<textarea id="Description' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;" >' + data.Description + '</textarea>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.CouponType === 2 || data.CouponType === 3) {
                                    return '<input type="text" id="Amount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.Amount + '" onkeypress="return isNumberKey(this, event);">';
                                }
                                else {
                                    return '<input type="text" disabled   id="Amount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.Amount + '" onkeypress="return isNumberKey(this, event);">';
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                if (data.ApplyType === 2 || data.ApplyType === 3 || data.ApplyType === 5) {
                                    strDisabled = "disabled";
                                }
                                return '<input type="text"  ' + strDisabled + '  id="TxnCount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransactionCount + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                var strDisabled = "";
                                if (data.ApplyType === 2 || data.ApplyType === 3 || data.ApplyType === 5) {
                                    strDisabled = "disabled";
                                }
                                return '<input type="text"  ' + strDisabled + '    id="TxnVolume' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransactionVolume + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.CouponType === 1) {
                                    return '<input type="text"   id="CouponPercentage' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CouponPercentage + '" onkeypress="return isNumberKey(this, event);">';
                                }
                                else {
                                    return '<input type="text" disabled  id="CouponPercentage' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CouponPercentage + '" onkeypress="return isNumberKey(this, event);">';
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="MinimumAmount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAmount + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="MaximumAmount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAmount + '" onkeypress="return isNumberKey(this, event);">';

                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="CouponsCount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.CouponsCount + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },

                        { "data": "CouponsUsedCount", "name": "CouponsUsedCount", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objColor = "danger";
                                if (data.CouponTypeName === "Coupon") {
                                    objColor = "success";
                                }
                                else if (data.CouponTypeName === "Wallet") {
                                    objColor = "success";
                                }
                                else if (data.CouponTypeName === "MPCoins") {
                                    objColor = "success";
                                }
                                else {
                                    objColor = "danger";
                                }
                                var objCouponType = '<span  class="tb-status text-' + objColor + '">' + data.CouponTypeName + '</span>';
                                return objCouponType;
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objColor = "danger";
                                if (data.ApplyTypeName === "Transaction") {
                                    objColor = "danger";
                                }
                                else if (data.ApplyTypeName === "EmailVerify") {
                                    objColor = "success";
                                }
                                else {
                                    objColor = "orange";
                                }
                                var objApplyType = '<span  class="tb-status text-' + objColor + '">' + data.ApplyTypeName + '</span>';
                                return objApplyType;
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var isChecked = "";
                                if (data.IsOneTime) {
                                    isChecked = "checked";
                                }

                                var strDisabled = "";
                                if (data.ApplyType === 2 || data.ApplyType === 3 || data.ApplyType === 5) {
                                    isChecked = " checked onclick='alert(\"You can not change setting for this coupon.\");return false;' onkeydown='return false;'";
                                }

                                return '<div class="custom-control  custom-control-sm custom-switch">    <input type="checkbox" ' + isChecked + ' ' + strDisabled + '  class="custom-control-input" id="OneTimeSwitch' + data.Id + '">    <label class="custom-control-label" for="OneTimeSwitch' + data.Id + '"></label></div>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var isChecked = "";
                                if (data.IsOneTimePerDay) {
                                    isChecked = "checked";
                                }

                                var strDisabled = "";
                                if (data.ApplyType === 2 || data.ApplyType === 3 || data.ApplyType === 5) {
                                    isChecked = " checked onclick='alert(\"You can not change setting for this coupon.\");return false;' onkeydown='return false;'";
                                }
                                return '<div class="custom-control  custom-control-sm custom-switch">    <input type="checkbox" ' + isChecked + ' ' + strDisabled + '  class="custom-control-input" id="OneTimeSwitch24Hrs' + data.Id + '">    <label class="custom-control-label" for="OneTimeSwitch24Hrs' + data.Id + '"></label></div>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.CouponType === 4) {
                                    var objGenderType = '<input type="hidden" id="GenderTypeId' + data.Id + '" value="' + data.GenderType + '"/>';
                                    objGenderType = objGenderType + '<span  class="tb-status text-' + (data.GenderTypeName == "All" ? "success" : "") + '">-</span>';
                                    return objGenderType;
                                } else {
                                    var objGenderType = '<input type="hidden" id="GenderTypeId' + data.Id + '" value="' + data.GenderType + '"/>';
                                    objGenderType = objGenderType + '<span  class="tb-status text-' + (data.GenderTypeName == "All" ? "success" : "") + '">' + data.GenderStatusName + '</span>';
                                    return objGenderType;
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                if (data.CouponType === 4) {
                                    var objKycType = '<input type="hidden" id="KycTypeId' + data.Id + '" value="' + data.KycType + '"/>';
                                    objKycType = objKycType + '<span  class="tb-status text-' + (data.KycTypeName == "NotVerified" ? "danger" : "success") + '">-</span>';
                                    return objKycType;
                                }
                                else {
                                    var objKycType = '<input type="hidden" id="KycTypeId' + data.Id + '" value="' + data.KycType + '"/>';
                                    objKycType = objKycType + '<span  class="tb-status text-' + (data.KycTypeName == "NotVerified" ? "danger" : "success") + '">' + data.KycStatusName + '</span>';
                                    return objKycType;
                                }
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text" id="FromDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.FromDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text" id="ToDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.ToDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objScheduleStatusType = '<span  class="tb-status text-' + (data.ScheduleStatus != "Running" ? "danger" : "success") + '">' + data.ScheduleStatus + '</span>';
                                return objScheduleStatusType;
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var actioncolumn = '<table><tr>';
                                if (data.IsActive == true) {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCoupon' + data.Id + '"  title="Update Coupon"   onclick=\'alert("Published Coupons cannot be alter.")\'   class="btn btn-sm btn-success btnCouponAction">  Save </a></td>';
                                }
                                else {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCoupon' + data.Id + '"  class="btn btn-sm btn-success btnCouponAction"  title="Update Coupon" onclick=\'return updateCoupon(\"' + data.Id + '\");\'  class="btn btn-primary "> Save </a></td>';
                                }
                                if (data.IsActive == true) {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-success btnCouponAction"   title="UnPublish Coupon" onclick=\'return statusupdateCoupon(\"' + data.Id + '\",\"' + "false" + '\",);\' "> ' + "UnPublish" + ' </a></td>';
                                }
                                else {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-danger btnCouponAction"  title="Publish Coupon" onclick=\'return statusupdateCoupon(\"' + data.Id + '\",\"' + "true" + '\",);\' "> ' + "Publish" + ' </a></td>';
                                }
                                if (data.IsActive == true) {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="DeleteCoupon' + data.Id + '"  title="Delete"   onclick=\'alert("Published Coupons cannot be alter.")\'   class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btnCouponAction"> <em class="icon ni ni-trash"></em> </a></td>';
                                }
                                else {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'return DeleteCoupon(\"' + data.Id + '\");\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btnCouponAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
                                }
                                actioncolumn = actioncolumn + '</tr></table>'
                                return actioncolumn;
                            },
                            bSortable: false,
                            sTitle: "Action"
                        }


                    ]
                });

                if (document.getElementById("tbllist") != undefined) {
                    document.getElementById("tbllist").deleteTFoot();
                }

                $("#tbllist").append(
                    $('<tfoot/>').append($("#tbllist thead tr").clone())
                );
                $('#AjaxLoader').hide();
            }, 100);
    }
    else {
        $("#tbllist").hide();
    }
    $("#anchor_addCouponRow").show();
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#tbllist_length").hide();
    $(".dt-buttons btn-group flex-wrap").hide();

    setTimeout(function () {
        $('.datePicker').datepicker({
            format: 'd M yyyy',
            startDate: '+0d'
        });
    }, 1000);


}
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});



function AddCouponsRow() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var ProviderId = $("#drpProvider").val();
    var ServiceId = $("#drpProviderServices").val();
    var GenderStatus = $("#GenderStatus").val();
    var KycStatus = $("#KycStatus").val();
    var ScheduleStatus = $("#drpScheduleStatus").val();
    var ApplyType = $("#drpApplyType").val();

    var SelectedCoupenType = $("#drpCoupenType").val();
    if (SelectedCoupenType === "0" || SelectedCoupenType === "") {
        $("#dvMsg").html("Please select Coupon Type");
        return;
    }
    if (ApplyType === "0" || ApplyType === "") {
        $("#dvMsg").html("Please select Coupon ApplyType");
        return;
    }
    if (SelectedCoupenType === "1") {
        if (ProviderId == "0") {
            $("#dvMsg").html("Please select Category Type");
            return;
        }
        else if (ServiceId == "0") {
            $("#dvMsg").html("Please select Provider Service");
            return;
        }
    }

    var coupon = new Object();
    coupon.Id = 0;
    coupon.ServiceId = $("#drpProviderServices").val();
    coupon.CouponType = $("#drpCoupenType").val();
    coupon.GenderStatus = GenderStatus;
    coupon.KycStatus = KycStatus;
    coupon.FromDate = GetTodayDate();
    coupon.ToDate = GetTodayDate();
    coupon.Status = ScheduleStatus;
    coupon.ApplyType = ApplyType;
    if (SelectedCoupenType === "4") {
        coupon.GenderStatus = "";
        coupon.KycStatus = "";
    }
    if (coupon != null) {
        $.ajax({
            type: "POST",
            url: "/Coupon/AddNewCouponDataRow",
            data: JSON.stringify(coupon),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response != null) {
                    BindDataTable("tbllist");
                    $("#dvMsgSuccess").html("Add Coupons Values and click Update button in new row.");
                    // $("#anchor_addCouponRow").hide();
                    return true;
                } else {
                    $("#dvMsg").html("Something went wrong");
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
}


function updateCoupon(id) {
    debugger;

    var Title = $("#Title" + id).val(), Description = $("#Description" + id).val(), Amount = $("#Amount" + id).val(), perc = $("#CouponPercentage" + id).val(), CouponsCount = $("#CouponsCount" + id).val(), fromdt = $("#FromDateDT" + id).val(), todt = $("#ToDateDT" + id).val();
    var ServiceId = $("#ServiceId" + id).val();
    var TxnCount = $("#TxnCount" + id).val();
    var TxnVolume = $("#TxnVolume" + id).val();
    var MinimumAmount = $("#MinimumAmount" + id).val();
    var MaximumAmount = $("#MaximumAmount" + id).val();
    var OneTimeSwitch = $("#OneTimeSwitch" + id).is(":checked")
    var OneTimeSwitch24Hrs = $("#OneTimeSwitch24Hrs" + id).is(":checked")

    if (Title == "") {
        $("#dvMsg").html("Please enter title");
        return false;
    }
    else if (Description == "") {
        $("#dvMsg").html("Please enter Description");
        return false;
    }
    else if (Amount == "") {
        $("#dvMsg").html("Please enter  Amount");
        return false;
    }
    else if (perc == "") {
        $("#dvMsg").html("Please enter Coupon Percentage");
        return false;
    }
    else if (CouponsCount == "") {
        $("#dvMsg").html("Please enter Coupons Count.");
        return false;
    }

    else if (fromdt == "") {
        $("#dvMsg").html("Please enter From Date");
        return false;
    }
    else if (todt == "") {
        $("#dvMsg").html("Please enter To Date");
        return false;
    }
    else if (parseFloat(perc) >= "100") {
        $("#dvMsg").html("Coupon(%)  can't be greater than equal to 100");
        return false;
    }
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateCoupon" + id).attr("disabled", "disabled");
    $("#UpdateCoupon" + id).text("Processing..");
    var Coupon = new Object();
    Coupon.Id = parseInt(id);
    Coupon.ServiceId = ServiceId;
    Coupon.Title = Title;
    Coupon.Description = Description;
    Coupon.Amount = Amount;
    Coupon.CouponPercentage = perc;
    Coupon.CouponsCount = CouponsCount;
    Coupon.FromDate = fromdt;
    Coupon.ToDate = todt;
    Coupon.IsOneTime = OneTimeSwitch;
    Coupon.IsOneTimePerDay = OneTimeSwitch24Hrs;
    Coupon.TransactionCount = TxnCount;
    Coupon.TransactionVolume = TxnVolume;
    Coupon.MinimumAmount = MinimumAmount;
    Coupon.MaximumAmount = MaximumAmount;

    var dd = JSON.stringify(Coupon);
    if (Coupon != null) {
        $.ajax({
            type: "POST",
            url: "/Coupon/CouponUpdate",
            data: JSON.stringify(Coupon),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMsg").html("Records not updated.");
                        return false;
                    }
                    else {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Coupons are successfully updated");
                        $("#anchor_addCouponRow").show();
                        return true;
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
    else {
        return false;
    }
}




function statusupdateCoupon(id, activestatus) {

    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Published Coupons cannot be alter.")) {
        var Coupon = new Object();
        Coupon.Id = parseInt(id);
        Coupon.IsActive = activestatus;
        var dd = JSON.stringify(Coupon);
        if (Coupon != null) {
            $.ajax({
                type: "POST",
                url: "/Coupon/StatusUpdateCoupon",
                data: JSON.stringify(Coupon),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    debugger;
                    if (response != null) {
                        if (response.Id != "0") {
                            BindDataTable("tbllist");
                            if (activestatus == "true")
                                $("#dvMsgSuccess").html("Coupons are successfully Published");
                            else
                                $("#dvMsgSuccess").html("Coupons are successfully Unpublished");
                            $("#anchor_addCouponRow").show();
                            return true;
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
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function DeleteCoupon(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete Coupon ?")) {
        var Coupon = new Object();
        Coupon.Id = id;
        if (Coupon != null) {
            $.ajax({
                type: "POST",
                url: "/Coupon/DeleteCouponDataRow",
                data: JSON.stringify(Coupon),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Coupons are successfully deleted");
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
        else {
            return false;
        }
    }
    else {
        return false;
    }
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

function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "-" + (MM + 1) + "-" + yyyy;

    return currentDate;
}

$(document).ready(function () {

    $("#btnSearch").click(function () {

    });

    $("#drpApplyType").on("change", function () {
        debugger;
        var ApplyType = $("#drpApplyType").val();
        //if (ApplyType == "1") {
        //    $('#div_Provider').show();
        //    $('#div_ProviderServices').show();
        //}
        //else {
        //    $('#div_Provider').hide();
        //    $('#div_ProviderServices').hide();
        //    $("#drpProvider").prop('selectedIndex', -1)
        //    $("#drpProviderServices").empty();
        //}
    });
    $("#drpCoupenType").on("change", function () {
        debugger;

        $("#KycStatus").empty();
        KycSelect.forEach(function (kyc) {
            $("#KycStatus").append($("<option></option>").val(kyc.Value).html(kyc.Text));
        });
        $("#GenderStatus").empty();
        GenderSelect.forEach(function (Gender) {
            $("#GenderStatus").append($("<option></option>").val(Gender.Value).html(Gender.Text));
        });
        $("#drpProvider").empty();
        CategorySelect.forEach(function (Category) {
            $("#drpProvider").append($("<option></option>").val(Category.Value).html(Category.Text));
        });

        $("#drpProviderServices").empty();

        $('#div_Provider').hide();
        $('#div_ProviderServices').hide();
        $('#div_KycType').show();
        $('#div_GenderStatus').show();
        var SelectedCoupenType = $("#drpCoupenType").val();
        if (SelectedCoupenType === "1") {
            $('#div_Provider').show();
            $('#div_ProviderServices').show();
        } else if (SelectedCoupenType === "4") {
            $('#div_KycType').hide();
            $('#div_GenderStatus').hide();
        }

    })
    $("#drpProvider").on("change", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        debugger;
        var ProviderId = $("#drpProvider").val();
        var value = $("#drpProvider option:selected");
        if (ProviderId != 0) {
            $.ajax({
                url: "/Coupon/GetProviderServiceList",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"ProviderId":"' + ProviderId + '"}',
                success: function (data) {
                    debugger;

                    $('#drpProviderServices')
                        .find('option')
                        .remove()
                        .end();

                    var list = $("#drpProviderServices");
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
            alert("Please select Provider ");
        }
    });

    $("#drpProviderServices").on("change", function () {

        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
    });
    $("#anchor_SearchCoupons").on("click", function () {
        debugger;
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var SelectedCoupenType = $("#drpCoupenType").val();
        if (SelectedCoupenType == "0") {
            alert('Please select Coupon Type');
        }
        else {
            var ProviderServicesId = $('#drpProviderServices').val();
            if ((ProviderServicesId == "0" || ProviderServicesId == "") && (SelectedCoupenType === "1")) {
                $("#dvMsg").html("Please select Provider Service");
            }
            else {
                BindDataTable("tbllist");
            }
        }
    });
});

function BindScratchedCouponDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var ContactNumber = $("#ContactNumber").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Event Details"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Coupon/GetScratchedCouponLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = ContactNumber;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "UniqueTransactionId", "name": "Transaction Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Member Id"
                    },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNumber", "name": "Contact No", "autoWidth": true, "bSortable": false },
                    { "data": "CouponCode", "name": "Coupon Code", "autoWidth": true, "bSortable": false },
                    { "data": "CouponTypeName", "name": "Coupon Type", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceName", "name": "Service Name", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindCouponHistoryDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Records"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Coupon/GetCouponsHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.fromdate = fromdate;
                        data.todate = todate;
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    { "data": "StatusName", "name": "Status", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceName", "name": "Service Name", "autoWidth": true, "bSortable": false },
                    { "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "TransactionCount", "name": "Txn Count", "autoWidth": true, "bSortable": false },
                    { "data": "TransactionVolume", "name": "Txn Volume", "autoWidth": true, "bSortable": false },
                    { "data": "CouponPercentage", "name": "Coupon Percentage", "autoWidth": true, "bSortable": false },
                    { "data": "MinimumAmount", "name": "Min Amount", "autoWidth": true, "bSortable": false },
                    { "data": "MaximumAmount", "name": "Max Amount", "autoWidth": true, "bSortable": false },
                    { "data": "CouponsCount", "name": "Coupons Count", "autoWidth": true, "bSortable": false },
                    { "data": "CouponsUsedCount", "name": "Coupons Used Count", "autoWidth": true, "bSortable": false },
                    { "data": "CouponTypeName", "name": "Coupon Type", "autoWidth": true, "bSortable": false },
                    { "data": "ToDateDT", "name": "Exipry Date", "autoWidth": true, "bSortable": false }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}