

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
var datacheckval = "";
function BindDataTable(tableId) {
    var AirlineId = $("#AirlineName").val()
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Commissions"
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
                        "url": "/Commission/GetAirlineCommissionLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.AirlineId = AirlineId;
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
                        {
                            data: null,
                            render: function (data, type, row) {

                                return '<input type="hidden" id="AirlineId' + data.Id + '" value="' + data.AirlineId + '"/><span>' + data.AirlineName + '</span> ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="hidden" id="FromSectorId' + data.Id + '" value="' + data.FromSectorId + '"/><span>' + data.FromSectorName + '</span> ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="hidden" id="ToSectorId' + data.Id + '" value="' + data.ToSectorId + '"/><span>' + data.ToSectorName + '</span> ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="hidden" id="ClassId' + data.Id + '" value="' + data.AirlineClassId + '"/><span>' + data.ClassName + '</span> ';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="CashbackPercentage' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.Cashback_Percentage + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageRewardPoints' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MPCoinsCredit + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"   id="PercentageRewardPointsDebit' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MPCoinsDebit + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MinimumAllowed' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinimumCashbackAllowed + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MaximumAllowed' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaximumCashbackAllowed + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var objIsDisables = "";
                                return '<input type="text" id="ServiceCharge' + data.Id + '" ' + objIsDisables + ' maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ServiceCharge + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {

                                var objIsDisables = "";
                                var isChecked = data.IsCashbackPerTicket ? 'checked' : '';
                                return '<input type="checkbox" id="IsCashbackPerTicket' + data.Id + '" class="form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.IsCashbackPerTicket + '" onchange="toggleButtonClick(this);" ' + isChecked + '>';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MinServiceCharge' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MinServiceCharge + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<input type="text"  id="MaxServiceCharge' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.MaxServiceCharge + '" onkeypress="return isNumberKey(this, event);">';
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

    $("#addAirlineCommissionRow").show();
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
    var AirlineId = $("#AirlineName").val();
    var FromSectorId = $("#fromSector").val();
    var ToSectorId = $("#toSector").val();
    var GenderType = $("#GenderTypeEnum").val();
    var KycType = $("#KycTypeEnum").val();
    if (AirlineId == "0") {
        $("#dvMsg").html("Please select Airline Type");
    }
    else if (FromSectorId == "0") {
        $("#dvMsg").html("Please select destination from Sector");
    }
    else if (ToSectorId == "0") {
        $("#dvMsg").html("Please select destination to Sector");
    }
    else {
        var commission = new Object();
        commission.Id = 0;
        commission.AirlineId = $("#AirlineName").val();
        commission.FromSectorId = $("#fromSector").val();
        commission.ToSectorId = $("#toSector").val();
        commission.GenderType = GenderType;
        commission.KycType = KycType;
        commission.PercentageCommission = 0;
        commission.PercentageRewardPoints = 0;
        commission.PercentageRewardPointsDebit = 0;
        commission.ServiceCharge = 0;
        commission.IsCashbackPerTicket = false;
        commission.MinimumCashbackAllowed = 0;
        commission.MaximumCashbackAllowed = 0;
        commission.FromDate = GetTodayDate();
        commission.ToDate = GetTodayDate();
        commission.MinimumAllowedSC = 0;
        commission.MaximumAllowedSC = 0;

        if (commission != null) {
            $.ajax({
                type: "POST",
                url: "/Commission/AddAirlineCommissionDataRow",
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
    var cashback_percentage = $("#CashbackPercentage" + id).val(), percRewards = $("#PercentageRewardPoints" + id).val(), percRewardsDebit = $("#PercentageRewardPointsDebit" + id).val(), MinServiceCharge = $("#MinServiceCharge" + id).val(), MaxServiceCharge = $("#MaxServiceCharge" + id).val(), IsCashbackPerTicket = ($("#IsCashbackPerTicket" + id).prop("checked")), fromdate = $("#FromDateDT" + id).val(), todate = $("#ToDateDT" + id).val(), MinimumCashbackAllowed = $("#MinimumAllowed" + id).val(), MaximumCashbackAllowed = $("#MaximumAllowed" + id).val();
    var ServiceCharge = $("#ServiceCharge" + id).val(), GenderType = $("#GenderTypeId" + id).val(), KycType = $("#KycTypeId" + id).val();
    var AirlineId = $("#AirlineId" + id).val();
    var FromSectorId = $("#FromSectorId" + id).val();
    var ToSectorId = $("#ToSectorId" + id).val();
    var AirlineClassId = $("#ClassId" + id).val();

    if (cashback_percentage == "") {
        $("#dvMsg").html("Please enter cashback percentage");
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
    else if (ServiceCharge == "") {
        $("#dvMsg").html("Please enter service charge Amount");
        return false;
    }
    else if (MinServiceCharge == "") {
        $("#dvMsg").html("Please enter Minimum Service Charge Allowed");
        return false;
    }
    else if (MaxServiceCharge == "") {
        $("#dvMsg").html("Please enter Maximum service charge Amount");
        return false;
    }
    else if (MinimumCashbackAllowed == "") {
        $("#dvMsg").html("Please enter Minimum cashback Allowed");
        return false;
    }
    else if (MaximumCashbackAllowed == "") {
        $("#dvMsg").html("Please enter Maximum cashback Allowed");
        return false;
    }
    else if (parseInt(MinimumCashbackAllowed) > parseInt(MaximumCashbackAllowed)) {
        $("#dvMsg").html("Please enter Minimum cashback value less than Maximum value");
        return false;
    }
    else if (fromdate == "") {
        $("#dvMsg").html("Please enter From Date");
        return false;
    }
    else if (todate == "") {
        $("#dvMsg").html("Please enter To Date");
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
    commission.AirlineId = AirlineId;
    commission.FromSectorId = FromSectorId;
    commission.ToSectorId = ToSectorId;
    commission.AirlineClassId = AirlineClassId;
    commission.GenderType = GenderType;
    commission.KycType = KycType;
    commission.Cashback_Percentage = cashback_percentage;
    commission.MPCoinsCredit = percRewards;
    commission.MPCoinsDebit = percRewardsDebit;
    commission.MinServiceCharge = MinServiceCharge;
    commission.MaxServiceCharge = MaxServiceCharge;
    commission.IsCashbackPerTicket = IsCashbackPerTicket;
    commission.ServiceCharge = ServiceCharge;
    commission.MinimumCashbackAllowed = MinimumCashbackAllowed;
    commission.MaximumCashbackAllowed = MaximumCashbackAllowed;
    commission.FromDate = fromdate;
    commission.ToDate = todate;
    var dd = JSON.stringify(commission);
    if (commission != null) {
        $.ajax({
            type: "POST",
            url: "/Commission/AirlineCommissionUpdateCall",
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
                url: "/Commission/DeleteAirlineCommission",
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
            url: "/Commission/AirlineStatusUpdateCommissionCall",
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
    $("#SearchAirlineCommission").on("click", function () {
        debugger;
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        BindDataTable("tbllist");
    });
});

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