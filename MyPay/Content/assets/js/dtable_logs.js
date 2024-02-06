
///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////
var table;

function BindLogsDataTable() {
    $("#dvMessage").html("");
    if ($("#fromdate").val() === "") {
        $("#dvMessage").html("Please select from Date.");
        return;
    }
    if ($("#todate").val() === "") {
        $("#dvMessage").html("Please select to Date.");
        return;
    }
    if (new Date($("#fromdate").val()) > new Date($("#todate").val())) {
        $("#dvMessage").html("To date should  be greater than From date.");
        return;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var ContactNumber = $("#ContactNumber").val();
            var LogActivity = $("#logactivitylist option:selected").val();
            var OldUserStatus = $("#OldUserStatus option:selected").val();
            var UserType = $("#UserType option:selected").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Transactions"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.LogActivity = LogActivity;
                        data.ContactNumber = ContactNumber;
                        data.OldUserStatus = OldUserStatus;
                        data.UserType = UserType;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "Name", "autoWidth": true, "bSortable": false },
                    { "data": "UserTypeName", "name": "User Type Name", "autoWidth": true, "bSortable": false },
                    { "data": "OldUserStatusName", "name": "User Status", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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

function BindBankTrasferLogsDataTable() {
    $("#dvMessage").html("");
    if ($("#fromdate").val() === "") {
        $("#dvMessage").html("Please select from Date.");
        return;
    }
    if ($("#todate").val() === "") {
        $("#dvMessage").html("Please select to Date.");
        return;
    }
    if (new Date($("#fromdate").val()) > new Date($("#todate").val())) {
        $("#dvMessage").html("To date should  be greater than From date.");
        return;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Bank Trasfer Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetBankTransferLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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

function BindBankTransctionsLogsDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var searchtext = $("#searchtext").val();
            var status = $("#status :selected").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "bPaginate": false,
                "oLanguage": {
                    "sEmptyTable": "No Bank Trasfer Logs"
                },
                /*"lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],*/
                /*"pageLength": 50,*/
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetBankTransactionsLogLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.searchtext = searchtext;
                        data.status = status;
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
                    { "data": "cipsBatchDetail.rcreTime", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "cipsBatchDetail.id", "name": "TransactionId", "autoWidth": true, "bSortable": true },
                    { "data": "cipsBatchDetail.batchId", "name": "BatchId", "autoWidth": true, "bSortable": false },
                    { "data": "cipsTransactionDetailList[0].instructionId", "name": "InstructionId", "autoWidth": true, "bSortable": false },
                    { "data": "cipsBatchDetail.debitStatus", "name": "DebitStatus", "autoWidth": true, "bSortable": false },
                    { "data": "cipsTransactionDetailList[0].creditStatus", "name": "CreditStatus", "autoWidth": true, "bSortable": false },
                    { "data": "cipsTransactionDetailList[0].amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "cipsTransactionDetailList[0].reasonDesc", "name": "Description", "autoWidth": true, "bSortable": false },

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

function BindConnectIPSLogsDataTable() {
    $("#dvMessage").html("");
    if ($("#fromdate").val() === "") {
        $("#dvMessage").html("Please select from Date.");
        return;
    }
    if ($("#todate").val() === "") {
        $("#dvMessage").html("Please select to Date.");
        return;
    }
    if (new Date($("#fromdate").val()) > new Date($("#todate").val())) {
        $("#dvMessage").html("To date should  be greater than From date.");
        return;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Connect-IPS Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetConnectIPSLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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

function BindCardLogsDataTable() {
    $("#dvMessage").html("");
    if ($("#fromdate").val() === "") {
        $("#dvMessage").html("Please select from Date.");
        return;
    }
    if ($("#todate").val() === "") {
        $("#dvMessage").html("Please select to Date.");
        return;
    }
    if (new Date($("#fromdate").val()) > new Date($("#todate").val())) {
        $("#dvMessage").html("To date should  be greater than From date.");
        return;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Card Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetCardPaymentLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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

function BindInternetBankingDataTable() {
    $("#dvMessage").html("");
    if ($("#fromdate").val() === "") {
        $("#dvMessage").html("Please select from Date.");
        return;
    }
    if ($("#todate").val() === "") {
        $("#dvMessage").html("Please select to Date.");
        return;
    }
    if (new Date($("#fromdate").val()) > new Date($("#todate").val())) {
        $("#dvMessage").html("To date should  be greater than From date.");
        return;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No InternetBanking Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetInternetBankingLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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

function BindP2PLogsDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No P2P Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetP2PLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }alert('Session Timeout. Please login again to continue.');
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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

function BindMobileBankingLogsDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var UserId = $("#UserId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No MobileBanking Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ActivityLogs/GetMobileBankingLogsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.UserId = UserId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.IpAddress + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm" onclick="return GetLogDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.UserId + '&apos;,&apos;' + data.Action + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Platform + '&apos;,&apos;' + data.DeviceCode + '&apos;)"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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