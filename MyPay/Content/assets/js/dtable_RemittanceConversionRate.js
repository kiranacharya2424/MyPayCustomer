

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
$(document).ready(function () {
    //BindDataTable("tbllist");
    $('#tbllist').hide();
});

$(function () {
    $(".nav-item").click(function () {
        debugger;
        var tableId = $(this).data("table");
        BindDataTable(tableId);
    });
});

function BindDataTable(tableId) {
    var SourceCurrency = $("#drpSourceCurrency").val()
    var DestinationCurrency = $("#drpDestinationCurrency").val()
    if (SourceCurrency != "0" && DestinationCurrency != "0") {
        $("#tbllist").show();
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Active Conversion Rate  "
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
                        "url": "/RemittanceConversionRateSettings/GetAdminRemittanceConversionLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.IsDeleted = 0;
                            data.SourceCurrencyId = $("#drpSourceCurrency").val();
                            data.DestinationCurrencyId = $("#drpDestinationCurrency").val();
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
                        {
                            data: null,
                            render: function (data, type, full, meta) {
                                return meta.row + 1;
                            },
                            bSortable: true,
                            sTitle: "Sno"
                        },
                        { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": false },
                        { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                        { "data": "SourceCurrency", "name": "Source Currency", "autoWidth": true, "bSortable": false },
                        { "data": "DestinationCurrency", "name": "Destination Currency", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {
                                return '<span class="tb-status text-danger" > * </span><input type="text" id="ConversionRate' + data.Id + '" maxlength="10" class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid" value="' + data.ConversionRate + '" onkeypress="return isNumberKey(this, event);">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                //return '<input type="text" id="FromDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.FromDateDT + '">';
                                return '<input type="time" name="time" id="FromDateDT' + data.Id + '"   class="form-control form-control-md form-control-outlined timepicker" value="' + data.FromDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                //return '<input type="text" id="ToDateDT' + data.Id + '"  readonly=readonly class="form-control form-control-md form-control-outlined datePicker dtdatecontrol_inGrid" value="' + data.ToDateDT + '">';
                                return '<input type="time" name="time" id="ToDateDT' + data.Id + '"  class="form-control form-control-md form-control-outlined timepicker" value="' + data.ToDateDT + '">';
                            },
                            autoWidth: true,
                            bSortable: false
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var actioncolumn = '<table><tr>';
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateCommission' + data.Id + '"  class="btn btn-sm btn-success btncommissionAction"  title="Update Conversion Rate" onclick=\'return updatecommission(\"' + data.Id + '\",\"' + data.SourceCurrencyId + '\",\"' + data.DestinationCurrencyId + '\");\'  class="btn btn-primary "> Save </a></td>';

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
                $('#tbllist').show();
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



function AddRemittanceConversionRow() {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var SourceCurrencyId = $("#drpSourceCurrency").val();
    var DestinationCurrencyId = $("#drpDestinationCurrency").val();
    var SourceCurrencyName = $("#drpSourceCurrency option:selected").text();
    var DestinationCurrencyName = $("#drpDestinationCurrency option:selected").text();
    if (SourceCurrencyId == "0") {
        $("#dvMsg").html("Please select Source Currency");
    }
    else if (DestinationCurrencyId == "0") {
        $("#dvMsg").html("Please select Destination Currency");
    }
    else if (SourceCurrencyName == DestinationCurrencyName) {
        $("#dvMsg").html("Source and Destination Currency cannot be same.");
    }
    else {
        var commission = new Object();
        commission.Id = 0;
        commission.SourceCurrencyId = SourceCurrencyId;
        commission.SourceCurrency = SourceCurrencyName;
        commission.DestinationCurrencyId = DestinationCurrencyId;
        commission.DestinationCurrency = DestinationCurrencyName;
        commission.ConversionRate = 0;
        commission.FromDate = GetTodayDate();
        commission.ToDate = GetTodayDate();
        if (commission != null) {
            $.ajax({
                type: "POST",
                url: "/RemittanceConversionRateSettings/AddNewRemittanceConversionDataRow",
                data: JSON.stringify(commission),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Add Conversion Values and click save button in new row.");
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


function updatecommission(id, sourcecurrencyid, destcurrencyid) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateCommission" + id).attr("disabled", "disabled");
    $("#UpdateCommission" + id).text("Processing..");
    var conversionrate = $("#ConversionRate" + id).val(), fromdt = $("#FromDateDT" + id).val(), todt = $("#ToDateDT" + id).val();
    var SourceCurrencyId = sourcecurrencyid;
    var DestinationCurrencyId = destcurrencyid;
    if (conversionrate == "") {
        $("#dvMsg").html("Please enter Conversion Rate");
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
    else if (DestinationCurrencyId == "0") {
        $("#dvMsg").html("Please select destination currency");
        return false;
    }
    var commission = new Object();
    commission.Id = parseInt(id);
    commission.SourceCurrencyId = SourceCurrencyId;
    commission.DestinationCurrencyId = DestinationCurrencyId;
    commission.ConversionRate = conversionrate;
    commission.FromDate = fromdt;
    commission.ToDate = todt;
    var dd = JSON.stringify(commission);
    if (commission != null) {
        $.ajax({
            type: "POST",
            url: "/RemittanceConversionRateSettings/AddRemittanceConversionUpdateCall",
            data: JSON.stringify(commission),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id == "0") {
                        BindDataTable("tbllist");
                        $("#dvMsg").html("Records not updated. Please check FromDate and ToDate with Source Currency and DestinationCurrency values are unique and never defined previously.");
                        return false;
                    }
                    else {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Conversion Rates are successfully updated");
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
            url: "/RemittanceConversionRateSettings/StatusUpdateRemittanceConversionCall",
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
                url: "/RemittanceConversionRateSettings/DeleteRemittanceConversionDataRow",
                data: JSON.stringify(commission),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Conversion Rate is successfully deleted");
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

    $("#anchor_SearchRemittanceCommission").on("click", function () {
        debugger;
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var SourceCurrency = $("#drpSourceCurrency").val();
        var DestinationCurrency = $("#drpDestinationCurrency").val();
        if (SourceCurrency != "0" && DestinationCurrency != "0") {
            BindDataTable("tbllist");
        }
        else {
            $("#dvMsg").html("Please select Source and Destination Currency.");
        }
    });
});

function BindRemittanceConversionUpdateHistoryDataTable() {
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
                "order": [[0, "asc"]],
                "ajax": {
                    "url": "/RemittanceConversionRateSettings/GetRemittanceCommissionUpdateHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.SourceCurrencyId = $("#drpSourceCurrency").val();
                        data.DestinationCurrencyId = $("#drpDestinationCurrency").val();
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
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "SourceCurrencyName", "name": "Source Currency", "autoWidth": true, "bSortable": false },
                    { "data": "DestinationCurrencyName", "name": "Destination Currency", "autoWidth": true, "bSortable": false },
                    { "data": "ConversionRate", "name": "Conversion Rate", "autoWidth": true, "bSortable": false },
                    { "data": "FromDateDT", "name": "Start Date", "autoWidth": true, "bSortable": true },
                    { "data": "ToDateDT", "name": "End Date", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedByName", "name": "Update By", "autoWidth": true, "bSortable": false },
                    { "data": "IPAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false }
                ]
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}