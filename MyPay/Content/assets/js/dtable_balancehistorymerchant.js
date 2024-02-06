///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
var take = 50;
var skip = 0;
var sort = "";
var sortdir = "";
function BindDataTable() {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Data"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/BalanceHistoryMerchantReport/GetBalanceHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
                    },
                    error: function (xhr, error, code) {
                        debugger;
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
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "TotalBalance", "name": "Total Balance", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantCount", "name": "Merchant Count", "autoWidth": true, "bSortable": false },
                    { "data": "ActiveMerchant", "name": "Active Merchant", "autoWidth": true, "bSortable": false },
                    { "data": "InActiveMerchant", "name": "InActive Merchant", "autoWidth": true, "bSortable": false },
                   /* { "data": "TotalCoinsBalance", "name": "Total Coins Balance", "autoWidth": true, "bSortable": false },*/
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" onclick="return EditBalance(&apos;' + data.Id + '&apos;,&apos;' + data.TotalBalance + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.MerchantCount + '&apos;,&apos;' + data.ActiveMerchant + '&apos;,&apos;' + data.InActiveMerchant + '&apos;)"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Edit"
                    }
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

function EditBalance(Id, TotalBalance, TypeName, MerchantCount, ActiveMerchant, InActiveMerchant) {
    $('#AjaxLoader').show();
    debugger;
    setTimeout(
        function () {
            if (Id != "" || Id != null) {
                $("#txtTotalBalance").val(TotalBalance);
                $("#spnType").html(TypeName);
                $("#spnUserCount").html(MerchantCount);
                $("#spnActiveUser").html(ActiveMerchant);
                $("#spnInActiveUser").html(InActiveMerchant);
                $("#hdnid").val(Id);
                $('#EditBalance').modal('show');
            }
            else {
                alert("Please enter Id");
            }
            $('#AjaxLoader').hide();
        }, 10);
}

function SubmitEditBalance() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var id = $("#hdnid").val();
            var totalbalance = $("#txtTotalBalance").val();

            if (id == "" || id == "0") {
                $("#dvPopupMsg").html('Please select id');
            }
            else if (totalbalance == "" || totalbalance == "0") {
                $("#dvPopupMsg").html('Please Enter Total Balance');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/BalanceHistoryMerchantReport/EditMerchantBalanceHistory",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Id":"' + parseInt(id) + '","TotalBalance":"' + totalbalance + '"}',
                    success: function (response) {
                        if (response != null) {
                            $("#dvPopupMsg").html(response);
                            BindDataTable();
                            setTimeout(function () {
                                $('#EditBalance').modal('hide');
                            },1000);
                        }
                        else {
                            JsonOutput = "Something went wrong. Please try again later.";
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvPopupMsg").html(JsonOutput);
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvPopupMsg").html(JsonOutput);
                    }
                });
            }
            $('#AjaxLoader').hide();
        }, 100);
}
function BindDataTableReadyOnly() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Data"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/BalanceHistoryMerchantReport/GetBalanceHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
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
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "TotalBalance", "name": "Total Balance", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantCount", "name": "Merchant Count", "autoWidth": true, "bSortable": false },
                    { "data": "ActiveMerchant", "name": "Active Merchant", "autoWidth": true, "bSortable": false },
                    { "data": "InActiveMerchant", "name": "InActive Merchant", "autoWidth": true, "bSortable": false },
                    
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
