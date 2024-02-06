

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
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#tbllist").show();
            table = $('#tbllist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Active Transaction Limits  "
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "searching": false,
                "sorting": false,
                "order": [[8, "desc"]],
                "ajax": {
                    "url": "/TransactionLimit/GetAdminTransactionLimitLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.IsDeleted = 0;
                        data.KycStatus = $("#KycStatus").val();
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
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input type="hidden" id="TransactionTransferType' + data.Id + '" value="' + data.TransactionTransferType + '"/><span>' + data.TransactionTransferTypeName.toUpperCase() + '</span> ';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-status text-danger" > * </span><input type="text" id="TransferLimitPerTransaction' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransferLimitPerTransaction + '" onkeypress="return isNumberKey(this, event);">';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-status text-danger" > * </span><input type="text" id="TransferLimitPerDay' + data.Id + '"  maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransferLimitPerDay + '" onkeypress="return isNumberKey(this, event);">';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-status text-danger" > * </span><input type="text"   id="TransferLimitPerMonth' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransferLimitPerMonth + '" onkeypress="return isNumberKey(this, event);">';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input type="text"  id="TransferLimitPerDayTransactionCount' + data.Id + '" Minlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransferLimitPerDayTransactionCount + '" onkeypress="return isNumberKey(this, event);">';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input type="text"   id="TransferLimitPerMonthTransactionCount' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransferLimitPerMonthTransactionCount + '" onkeypress="return isNumberKey(this, event);">';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var objKycType = '<input type="hidden" id="KycTypeId' + data.Id + '" value="' + data.KycStatus + '"/>';
                            objKycType = objKycType + '<span  class="tb-status text-' + (data.KycStatusName == "Verified" ? "success" : "danger") + '">' + data.KycStatusName + '</span>';
                            return objKycType;
                        },
                        name: "KycStatus",
                        autoWidth: true,
                        bSortable: true
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var actioncolumn = '<table><tr>';
                            actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateTransactionLimit' + data.Id + '"  class="btn btn-sm btn-success btnTransactionLimitAction"  title="Update TransactionLimit" onclick=\'return updateTransactionLimit(\"' + data.Id + '\");\'  class="btn btn-primary "> Save </a></td>';
                            if (data.IsActive == true) {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-success btnTransactionLimitAction"   title="UnPublish TransactionLimit" onclick=\'return statusupdateTransactionLimit(\"' + data.Id + '\",\"' + "false" + '\",);\' "> ' + "UnPublish" + ' </a></td>';
                            }
                            else {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-danger btnTransactionLimitAction"  title="Publish TransactionLimit" onclick=\'return statusupdateTransactionLimit(\"' + data.Id + '\",\"' + "true" + '\",);\' "> ' + "Publish" + ' </a></td>';
                            }
                            //actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'return DeleteTransactionLimit(\"' + data.Id + '\");\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btnTransactionLimitAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
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
            $("#anchor_addTransactionLimitRow").show();
            $("#dvMsgSuccess").html("");
            $("#dvMsg").html("");
            $("#tbllist_length").hide();
            $(".dt-buttons btn-group flex-wrap").hide();
            $('.datePicker').datepicker({
                format: 'd M yyyy'
            });
            $('#AjaxLoader').hide();
        }, 100);
    //(".btnTransactionLimitAction").tooltip();
}
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});



function AddTransactionLimitRow() {
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
        var TransactionLimit = new Object();
        TransactionLimit.Id = 0;
        TransactionLimit.ServiceId = $("#drpProviderServices").val();
        TransactionLimit.GenderType = GenderType;
        TransactionLimit.KycType = KycType;
        TransactionLimit.MinimumAmount = 0;
        TransactionLimit.MaximumAmount = 0;
        TransactionLimit.FixedTransactionLimit = 0;
        TransactionLimit.TransactionLimit = 0;
        TransactionLimit.RewardPoints = 0;
        TransactionLimit.ServiceCharge = 0;
        TransactionLimit.FromDate = GetTodayDate();
        TransactionLimit.ToDate = GetTodayDate();
        TransactionLimit.MinimumAllowed = 0;
        TransactionLimit.MaximumAllowed = 0;
        if (TransactionLimit != null) {
            $.ajax({
                type: "POST",
                url: "/TransactionLimit/AddNewTransactionLimitDataRow",
                data: JSON.stringify(TransactionLimit),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Add TransactionLimits Values and click Update button in new row.");
                        // $("#anchor_addTransactionLimitRow").hide();
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


function updateTransactionLimit(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateTransactionLimit" + id).attr("disabled", "disabled");
    $("#UpdateTransactionLimit" + id).text("Processing..");

    var TransactionTransferType = $("#TransactionTransferType" + id).val(),
        TransferLimitPerTransaction = $("#TransferLimitPerTransaction" + id).val(),
        TransferLimitPerDay = $("#TransferLimitPerDay" + id).val(),
        TransferLimitPerMonth = $("#TransferLimitPerMonth" + id).val(),
        TransferLimitPerDayTransactionCount = $("#TransferLimitPerDayTransactionCount" + id).val(),
        TransferLimitPerMonthTransactionCount = $("#TransferLimitPerMonthTransactionCount" + id).val();

    if (TransferLimitPerTransaction == "") {
        $("#dvMsg").html("Please enter TransferLimitPerTransaction");
        return false;
    }
    else if (TransferLimitPerDay == "") {
        $("#dvMsg").html("Please enter TransferLimitPerDay");
        return false;
    }
    else if (TransferLimitPerMonth == "") {
        $("#dvMsg").html("Please enter TransferLimitPerMonth");
        return false;
    }
    else if (TransferLimitPerDayTransactionCount == "") {
        $("#dvMsg").html("Please enter TransferLimitPerDayTransactionCount");
        return false;
    }
    else if (TransferLimitPerMonthTransactionCount == "") {
        $("#dvMsg").html("Please enter TransferLimitPerMonthTransactionCount");
        return false;
    }
    var TransactionLimit = new Object();
    TransactionLimit.Id = parseInt(id);
    TransactionLimit.TransactionTransferType = TransactionTransferType;
    TransactionLimit.TransferLimitPerTransaction = TransferLimitPerTransaction;
    TransactionLimit.TransferLimitPerDay = TransferLimitPerDay;
    TransactionLimit.TransferLimitPerMonth = TransferLimitPerMonth;
    TransactionLimit.TransferLimitPerDayTransactionCount = TransferLimitPerDayTransactionCount;
    TransactionLimit.TransferLimitPerMonthTransactionCount = TransferLimitPerMonthTransactionCount;
    TransactionLimit.KycStatus = $("#KycTypeId" + id).val();
    var dd = JSON.stringify(TransactionLimit);
    if (TransactionLimit != null) {
        $.ajax({
            type: "POST",
            url: "/TransactionLimit/TransactionLimitUpdateCall",
            data: JSON.stringify(TransactionLimit),
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
                        $("#dvMsgSuccess").html("TransactionLimits are successfully updated");
                        $("#anchor_addTransactionLimitRow").show();
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




function statusupdateTransactionLimit(id, activestatus) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var TransactionLimit = new Object();
    TransactionLimit.Id = parseInt(id);
    TransactionLimit.IsActive = activestatus;
    var dd = JSON.stringify(TransactionLimit);
    if (TransactionLimit != null) {
        $.ajax({
            type: "POST",
            url: "/TransactionLimit/StatusUpdateTransactionLimitCall",
            data: JSON.stringify(TransactionLimit),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id != "0") {
                        BindDataTable("tbllist");
                        if (activestatus == "true")
                            $("#dvMsgSuccess").html("TransactionLimits are successfully Published");
                        else
                            $("#dvMsgSuccess").html("TransactionLimits are successfully Unpublished");
                        $("#anchor_addTransactionLimitRow").show();
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


function DeleteTransactionLimit(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete TransactionLimit ?")) {
        var TransactionLimit = new Object();
        TransactionLimit.Id = id;
        if (TransactionLimit != null) {
            $.ajax({
                type: "POST",
                url: "/TransactionLimit/DeleteTransactionLimitDataRow",
                data: JSON.stringify(TransactionLimit),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("TransactionLimits are successfully deleted");
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

    //$("#KycStatus").val($("#KycTypeEnum").val());
    //$("#KycTypeEnum").on("change", function () {
    //    debugger;
    //    $("#KycStatus").val($("#KycTypeEnum").val());
    //    BindDataTable("tbllist");
    //});

});

//BindTransactionLimitHistory DataTable
function BindTransactionLimitHistoryDataTable() {
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
                    "url": "/TransactionLimit/GetTransactionLimitHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        //data.KycStatus = $("#KycTypeEnum").val();
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
                    { "data": "Sno", "name": "Sno", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "TransferLimitTypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "TransferLimitPerTransaction", "name": "Limit/Transaction", "autoWidth": true, "bSortable": false },
                    { "data": "TransferLimitPerDay", "name": "Amount/Day", "autoWidth": true, "bSortable": false },
                    { "data": "TransferLimitPerMonth", "name": "Amount/Month", "autoWidth": true, "bSortable": false },
                    { "data": "TransferLimitPerMonthTransactionCount", "name": "Transaction/Month", "autoWidth": true, "bSortable": false },
                    { "data": "TransferLimitPerDayTransactionCount", "name": "Transaction/Day", "autoWidth": true, "bSortable": false },
                    { "data": "StatusName", "name": "KYC Type", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
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
                    "url": "/TransactionLimit/GetProviderServiceCategoriesLists",
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
                            return '<input type="text"  id="TransactionLimit' + data.Id + '" maxlength="10"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.TransactionLimit + '" ">';
                        },
                        autoWidth: true,
                        bSortable: false,
                        sTitle: "TransactionLimit %"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var actioncolumn = '<table><tr><td><a href="javascript:void(0);"  class="btn btn-sm btn-success" onclick=\'return updateproviderTransactionLimit(\"' + data.Id + '\");\'  class="btn btn-primary "> Update </a></td></tr></table>';
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

function updateproviderTransactionLimit(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var TransactionLimit = $("#TransactionLimit" + id).val();


    if (TransactionLimit == "") {
        $("#dvMsg").html("Please enter Cashback");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/TransactionLimit/ProviderTransactionLimitUpdate",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"TransactionLimit":"' + TransactionLimit + '","Id":"' + id + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    BindProviderServiceCategoryDataTable();
                    $("#dvMsgSuccess").html("TransactionLimit successfully updated");
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

