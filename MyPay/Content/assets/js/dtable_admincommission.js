

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
    var ServiceId = $("#drpProviderServices").val()
    if (ServiceId != "0") {
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Commissions  "
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
                        "url": "/Commission/GetAdminCommissionLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.ServiceId = $("#drpProviderServices").val();
                            data.GenderType = $("#GenderTypeEnum").val();
                            data.KycType = $("#KycTypeEnum").val();
                            data.ScheduleStatus = $("#EnumScheduleStatus").val();
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
                                return '<input type="hidden" id="ServiceId' + data.Id + '" value="' + data.ServiceId + '"/><span>' + data.ServiceName.toUpperCase() + '</span> ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="MinimumAmount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAmount + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="MaximumAmount' + data.Id + '"  maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAmount + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        //{
                        //    data: null,
                        //    render: function (data, type, row) {
                        //        return '<input type="text"  id="FixedCommission' + data.Id + '"  maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.FixedCommission + '" onkeypress="return isNumberKey(this, event);">';
                        //    },
                        //    autoWidth: true,
                        //    bSortable: false
                        //},
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageCommission' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PercentageCommission + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageRewardPoints' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PercentageRewardPoints + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageRewardPointsDebit' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.PercentageRewardPointsDebit + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MinimumAllowed' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAllowed + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MaximumAllowed' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAllowed + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                //var objIsDisables = (data.GenderTypeName.toUpperCase() == "ALL" && data.KycTypeName.toUpperCase() == "ALL") ? "" : "disabled";
                                var objIsDisables = ""; 
                                return '<input type="text" id="ServiceCharge' + data.Id + '" ' + objIsDisables + ' maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ServiceCharge + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MinimumAllowedSC' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumAllowedSC + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MaximumAllowedSC' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumAllowedSC + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objGenderType = '<input type="hidden" id="GenderTypeId' + data.Id + '" value="' + data.GenderType + '"/>';
                                objGenderType = objGenderType + '<span  class="tb-status text-' + (data.GenderTypeName == "All" ? "success" : "") + '">' + data.GenderTypeName + '</span>';
                                return objGenderType;
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objKycType = '<input type="hidden" id="KycTypeId' + data.Id + '" value="' + data.KycType + '"/>';
                                objKycType = objKycType + '<span  class="tb-status text-' + (data.KycTypeName == "NotVerified" ? "danger" : "success") + '">' + data.KycTypeName + '</span>';
                                return objKycType;
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
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCommission' + data.Id + '"  class="btn btn-sm btn-success btncommissionAction"  title="Update Commission" onclick=\'return updatecommission(\"' + data.Id + '\");\'  class="btn btn-primary "> Save </a></td>';
                                if (data.IsActive == true) {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-success btncommissionAction"   title="UnPublish Commission" onclick=\'return statusupdatecommission(\"' + data.Id + '\",\"' + "false" + '\",);\' "> ' + "UnPublish" + ' </a></td>';
                                }
                                else {
                                    actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-danger btncommissionAction"  title="Publish Commission" onclick=\'return statusupdatecommission(\"' + data.Id + '\",\"' + "true" + '\",);\' "> ' + "Publish" + ' </a></td>';
                                }
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'return DeleteCommission(\"' + data.Id + '\");\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btncommissionAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
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
    $("#anchor_addCommissionRow").show();
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

    //$('.datePicker').datepicker({
    //    format: 'd M yyyy',
    //    startDate: '+0d'
    //});

    //(".btncommissionAction").tooltip();
}
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});



function AddCommissionRow() {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var ProviderId = $("#drpProvider").val();
    var ServiceId = $("#drpProviderServices").val();
    var GenderType = $("#GenderTypeEnum").val();
    var KycType = $("#KycTypeEnum").val();
    if (ProviderId == "0") {
        $("#dvMsg").html("Please select Provider Type");
    }
    else if (ServiceId == "0") {
        $("#dvMsg").html("Please select Provider Service");
    }
    else {
        var commission = new Object();
        commission.Id = 0;
        commission.ServiceId = $("#drpProviderServices").val();
        commission.GenderType = GenderType;
        commission.KycType = KycType;
        commission.MinimumAmount = 0;
        commission.MaximumAmount = 0;
        commission.FixedCommission = 0;
        commission.PercentageCommission = 0;
        commission.PercentageRewardPoints = 0;
        commission.PercentageRewardPointsDebit = 0;
        commission.ServiceCharge = 0;
        commission.FromDate = GetTodayDate();
        commission.ToDate = GetTodayDate();
        commission.MinimumAllowed = 0;
        commission.MaximumAllowed = 0;
        if (commission != null) {
            $.ajax({
                type: "POST",
                url: "/Commission/AddNewCommissionDataRow",
                data: JSON.stringify(commission),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Add Commissions Values and click Update button in new row.");
                        // $("#anchor_addCommissionRow").hide();
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
}


function updatecommission(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateCommission" + id).attr("disabled", "disabled");
    $("#UpdateCommission" + id).text("Processing..");
    var min = $("#MinimumAmount" + id).val(), max = $("#MaximumAmount" + id).val(), fixed = $("#FixedCommission" + id).val(), perc = $("#PercentageCommission" + id).val(), percRewards = $("#PercentageRewardPoints" + id).val(), percRewardsDebit = $("#PercentageRewardPointsDebit" + id).val(), fromdt = $("#FromDateDT" + id).val(), todt = $("#ToDateDT" + id).val(), minallowed = $("#MinimumAllowed" + id).val(), maxallowed = $("#MaximumAllowed" + id).val(), minallowedSC = $("#MinimumAllowedSC" + id).val(), maxallowedSC = $("#MaximumAllowedSC" + id).val();
    var ServiceId = $("#ServiceId" + id).val(), ServiceCharge = $("#ServiceCharge" + id).val(), GenderType = $("#GenderTypeId" + id).val(), KycType = $("#KycTypeId" + id).val();

    if (min == "") {
        $("#dvMsg").html("Please enter Minimum Slab Amount");
        return false;
    }
    else if (max == "") {
        $("#dvMsg").html("Please enter Maximum Slab Amount");
        return false;
    }
    else if (fixed == "") {
        $("#dvMsg").html("Please enter Fixed Amount");
        return false;
    }
    else if (perc == "") {
        $("#dvMsg").html("Please enter Percent Amount");
        return false;
    }
    else if (percRewards == "") {
        $("#dvMsg").html("Please enter MP-Coins Credit.");
        return false;
    }
    else if (percRewardsDebit == "") {
        $("#dvMsg").html("Please enter MP-Coins Debit.");
        return false;
    }
    else if (maxallowed == "") {
        $("#dvMsg").html("Please enter Maximum Amount Allowed");
        return false;
    }
    else if (parseInt(min) > parseInt(max)) {
        $("#dvMsg").html("Please enter Minimum value less than Maximum value");
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
        $("#dvMsg").html("Cashback(%)  can't be greater than equal to 100");
        return false;
    }
    else if (parseFloat(percRewards) >= "100") {
        $("#dvMsg").html("MP-Coins Credit(%)  can't be greater than equal to 100");
        return false;
    }
    else if (parseFloat(percRewardsDebit) >= "100") {
        $("#dvMsg").html("MP-Coins Debit(%)  can't be greater than equal to 100");
        return false;
    }
    else if (parseFloat(ServiceCharge) >= "100") {
        $("#dvMsg").html("ServiceCharge(%)  can't be greater than equal to 100");
        return false;
    }
    var commission = new Object();
    commission.Id = parseInt(id);
    commission.ServiceId = ServiceId;
    commission.GenderType = GenderType;
    commission.KycType = KycType;
    commission.MinimumAmount = min;
    commission.MaximumAmount = max;
    commission.FixedCommission = fixed;
    commission.PercentageCommission = perc;
    commission.PercentageRewardPoints = percRewards;
    commission.PercentageRewardPointsDebit = percRewardsDebit;
    commission.FromDate = fromdt;
    commission.ToDate = todt;
    commission.MinimumAllowed = minallowed;
    commission.MaximumAllowed = maxallowed;
    commission.MinimumAllowedSC = minallowedSC;
    commission.MaximumAllowedSC = maxallowedSC;
    commission.ServiceCharge = ServiceCharge;
    var dd = JSON.stringify(commission);
    if (commission != null) {
        $.ajax({
            type: "POST",
            url: "/Commission/CommissionUpdateCall",
            data: JSON.stringify(commission),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMsg").html("Records not updated. Please check that minimum and maximum values are unique and never defined previously.");
                        return false;
                    }
                    else {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Commissions are successfully updated");
                        $("#anchor_addCommissionRow").show();
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




function statusupdatecommission(id, activestatus) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var commission = new Object();
    commission.Id = parseInt(id);
    commission.IsActive = activestatus;
    var dd = JSON.stringify(commission);
    if (commission != null) {
        $.ajax({
            type: "POST",
            url: "/Commission/StatusUpdateCommissionCall",
            data: JSON.stringify(commission),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id != "0") {
                        BindDataTable("tbllist");
                        if (activestatus == "true")
                            $("#dvMsgSuccess").html("Commissions are successfully Published");
                        else
                            $("#dvMsgSuccess").html("Commissions are successfully Unpublished");
                        $("#anchor_addCommissionRow").show();
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


function DeleteCommission(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete commission ?")) {
        var commission = new Object();
        commission.Id = id;
        if (commission != null) {
            $.ajax({
                type: "POST",
                url: "/Commission/DeleteCommissionDataRow",
                data: JSON.stringify(commission),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Commissions are successfully deleted");
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
    $("#drpProvider").on("change", function () {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        debugger;
        var ProviderId = $("#drpProvider").val();
        var value = $("#drpProvider option:selected");
        if (ProviderId != 0) {
            $.ajax({
                url: "/Commission/GetProviderServiceList",
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
            return;
           // alert("Please select Provider ");
        }
    });

    $("#drpProviderServices").on("change", function () {

        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
    });
    $("#anchor_SearchCommission").on("click", function () {
        debugger;
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var ProviderServicesId = $('#drpProviderServices').val();
        if (ProviderServicesId == "0") {
            $("#dvMsg").html("Please select Provider Service");
        }
        else {
            BindDataTable("tbllist");
        }
    });
});

//CommissionUpdateHistory DataTable
function BindCommissionUpdateHistoryDataTable() {
    var loginUserId = $("#hdn_LoginUserId").val();
    var roleid = $("#hdnroleid").val();
   $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Request"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[20, "asc"]],
                "ajax": {
                    "url": "/Commission/GetCommissionUpdateHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.ServiceId = $("#drpProviderServices").val();
                        data.GenderType = $("#GenderTypeEnum").val();
                        data.KycType = $("#KycTypeEnum").val();
                        data.Status = $("#Status").val();
                        data.ScheduleStatus = $("#EnumScheduleStatus").val();
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
                    { "data": "Sno", "name": "Sno", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceName", "name": "Service Name", "autoWidth": true, "bSortable": false },
                    { "data": "MinimumAmount", "name": "Min Amount", "autoWidth": true, "bSortable": false },
                    { "data": "MaximumAmount", "name": "Max Amount", "autoWidth": true, "bSortable": false},
                    //{ "data": "FixedCommission", "name": "FixedCommission", "autoWidth": true },
                    { "data": "PercentageCommission", "name": "Cashback Amount(%)", "autoWidth": true, "bSortable": false},
                    { "data": "PercentageRewardPoints", "name": "MP-Coins Credit(%)", "autoWidth": true, "bSortable": false },
                    { "data": "PercentageRewardPointsDebit", "name": "MP-Coins Debit(%)", "autoWidth": true, "bSortable": false },
                    { "data": "MinimumAllowed", "name": "Min Allowed", "autoWidth": true, "bSortable": false },
                    { "data": "MaximumAllowed", "name": "Max Allowed", "autoWidth": true, "bSortable": false },
                    { "data": "MinimumAllowedSC", "name": "Min Allowed SC", "autoWidth": true, "bSortable": false },
                    { "data": "MaximumAllowedSC", "name": "Max Allowed SC", "autoWidth": true, "bSortable": false},
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "GenderTypeName", "name": "Gender Type", "autoWidth": true, "bSortable": false },
                    { "data": "KycTypeName", "name": "Kyc Type", "autoWidth": true, "bSortable": false},
                    { "data": "FromDateDT", "name": "Start Date", "autoWidth": true, "bSortable": false },
                    { "data": "ToDateDT", "name": "End Date", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Update By", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            
                            if (data.IsActive == true) {
                                var objPublishStatus = '<span>Publish</span>';
                                return objPublishStatus;
                            }
                            else {
                                var objPublishStatus = '<span>Un-Publish</span>';
                                return objPublishStatus;
                            }
                           
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    //{ "data": "IsActive", "name": "Publish Status", "autoWidth": true, "bSortable": false },
                    { "data": "StatusName", "name": "Status", "autoWidth": true, "bSortable": false },
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
                            if (data.IsApprovedByAdmin) {
                                var objApprovalStatus = '<span  class="tb-status text-success">' +  "Approved" + '</span>';
                                return objApprovalStatus;
                            }
                            else {
                                var objApprovalStatus = '<span  class="tb-status text-danger">' + "Pending" + '</span>';
                                return objApprovalStatus;
                            }
                            
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var actioncolumn = '<table>';
                            if (data.IsApprovedByAdmin) {
                                var actioncolumn = '<span>' + data.UpdatedByName + '</span>';
                            }
                            else {
                                if (roleid.toString() == "1") {
                                    actioncolumn = actioncolumn + '<a href="javascript:void(0);" id="ApproveCommission' + data.Id + '"  title="Approve"   onclick="ApproveCommission(' + data.Id + ')"  class="btn btn-sm btn-success btnCouponAction">  Approve </a>';
                                    actioncolumn = actioncolumn + '</table>'
                                }
                                else if (roleid.toString() == "5" && data.CreatedBy != loginUserId){
                                    actioncolumn = actioncolumn + '<a href="javascript:void(0);" id="ApproveCommission' + data.Id + '"  title="Approve"   onclick="ApproveCommission(' + data.Id + ')"  class="btn btn-sm btn-success btnCouponAction">  Approve </a>';
                                    actioncolumn = actioncolumn + '</table>'
                                 }

                            }
                            
                            return actioncolumn;
                        },
                        autoWidth: true,
                        bSortable: false
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindProviderServiceCategoryDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Request"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/Commission/GetProviderServiceCategoriesLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
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
                    { "data": "Id", "name": "SrNo", "autoWidth": true },
                    { "data": "ProviderCategoryName", "name": "Provider Services", "autoWidth": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input type="text"  id="Commission' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.Commission + '" ">';
                        },
                        autoWidth: true,
                        bSortable: false,
                        sTitle: "Commission %"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input type="text"  id="MPCoins' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MPCoinsCashback + '" ">';
                        },
                        autoWidth: true,
                        bSortable: false,
                        sTitle: "MP Coins Cashback %"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var actioncolumn = '<table><tr><td><a href="javascript:void(0);"  class="btn btn-sm btn-success" onclick=\'return updateprovidercommission(\"' + data.Id + '\");\'  class="btn btn-primary "> Update </a></td></tr></table>';
                            return actioncolumn;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }
                ]
            });

            $("#dvMsgSuccess").html("");
            $("#dvMsg").html("");

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function updateprovidercommission(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var commission = $("#Commission" + id).val();
    var MPCoins = $("#MPCoins" + id).val();


    if (commission == "") {
        $("#dvMsg").html("Please enter Cashback");
        return false;
    }
    if (MPCoins == "") {
        $("#dvMsg").html("Please enter MP Coins Cashback");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/Commission/ProviderCommissionUpdate",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Commission":"' + commission + '","Id":"' + id + '","MPCoinsCashback":"' + MPCoins + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    BindProviderServiceCategoryDataTable();
                    $("#dvMsgSuccess").html("Commission successfully updated");
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

function BlockUnblock(id, url, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "" + url + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Id":"' + id + '"}',
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
                    BindDataTable("tbllist");
                    e.preventDefault();
                    e.stopPropagation();
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
function ApproveCommission(id) {
    if (confirm("Do you really want to approve Commission ?")) {
        $.ajax({
            type: "POST",
            url: "/Commission/ApproveCommissionMaker",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"Id":"' + id + '"}',
            success: function (response) {
                debugger;
                if (response != null) {
                    debugger;
                    if (response.IsApprovedByAdmin) {
                        $("#dvMsgSuccess").html("Request approved.");
                        BindCommissionUpdateHistoryDataTable();
                        e.preventDefault();
                        e.stopPropagation();
                        return false;
                    }
                    else {
                        debugger;
                        $("#dvMsg").html("Something went wrong");

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

    else {
        return false;
    }
}