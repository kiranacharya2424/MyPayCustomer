///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////
///////////////////////////////////////////////////

var table;

function BindMerchantDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MerchantUniqueId = $("#MerchantUniqueId").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#ContactName").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();


            table = $('#tblmerlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Merchant"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetMerchantLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantUniqueId = MerchantUniqueId;
                        data.ContactNumber = Mobile;
                        data.Email = Email;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Name = Name;
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
                        sTitle: "Srno"
                    },
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            $('#merchanttotalcount').html(data.TotalUserCount);

                            return '<span>' + data.ContactName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Name"
                    },
                    { "data": "UserName", "name": "User Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNo", "name": "Contact Number", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.EmailID == "" || data.EmailID == null) {
                                return '<span>--</span>';
                            }
                            else {
                                return '<span>' + data.EmailID + '</span>';

                            }
                        },
                        bSortable: false,
                        sTitle: "Email"
                    },
                    { "data": "MerchantUniqueId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "OrganizationName", "name": "Organization Name", "autoWidth": true, "bSortable": false },
                    //{ "data": "MerchantTotalAmount", "name": "Wallet Amount", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.IsActive) {
                                return '<span class="tb-status text-success">Active</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">Suspended</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Status"
                    },
                    { "data": "FeeAccountBalance", "name": "Fee Balance", "autoWidth": true, "bSortable": false },
                    { "data": "FeeTypeName", "name": "Fee Type", "autoWidth": true, "bSortable": false },
                    { "data": "MarginConversionRate", "name": "Margin Conversion Rate", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" onclick="return ShowIpModal(&apos;' + data.MerchantUniqueId + '&apos;)">' + data.MerchantIpAddress + '</a>';
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    //{ "data": "MerchantIpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    //{ "data": "apikey", "name": "Key", "autoWidth": true, "bSortable": false },
                    //{ "data": "API_Password", "name": "API Password", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<a href="javascript:void(0);" onclick="return ShowAPIKeyPassword(&apos;' + data.apikey + '&apos;,&apos;' + data.API_Password + '&apos;,event)" title="Show">Show</a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "API Key/Password"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<a href="javascript:void(0);" onclick="return ShowKeys(&apos;' + data.MerchantUniqueId + '&apos;,event)" title="Show">Show</a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Public Key/Private Key"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="Remittance/Index?MerchantUniqueId=' + data.MerchantUniqueId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            str += '<em class="icon ni ni-eye"></em>';
                            str += '</a>';
                            str += '</li>';
                            if (data.IsActive) {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.MerchantUniqueId + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                str += '<em class="icon ni ni-user-cross-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }
                            else {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.MerchantUniqueId + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                str += '<em class="icon ni ni-user-check-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }

                            str += '<li>';
                            str += '<div class="drodown">';
                            str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                            str += '<div class="dropdown-menu dropdown-menu-right">';
                            str += '<ul class="link-list-opt no-bdr">';
                            str += '<li><a href="Remittance/Index?MerchantUniqueId=' + data.MerchantUniqueId + '""><em class="icon ni ni-eye"></em><span>View Details</span></a></li>';

                            str += '<li class="divider"></li>';
                            if (data.IsActive) {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-user-cross-fill"></em><span>Disable User</span></a></li>';
                            }
                            else {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-user-check-fill"></em><span>Enable User</span></a></li>';
                            }
                            //str += '<li><a href="Remittance/AddRemittanceBankDetail?MerchantId=' + data.MerchantUniqueId + '""><em class="icon ni ni-list"></em><span>Add BankDetail</span></a></li>';

                            //str += '<li><a href="Remittance/RemittanceBankList?MerchantId=' + data.MerchantUniqueId + '""><em class="icon ni ni-list"></em><span>Bank Detail</span></a></li>';

                            str += '<li><a href="Remittance/RemittanceWallet?MerchantUniqueId=' + data.MerchantUniqueId + '""><em class="icon ni ni-list"></em><span>Wallet Currency</span></a></li>';
                            //str += '<li><a href="Remittance/AssignCurrency?MerchantUniqueId=' + data.MerchantUniqueId + '""><em class="icon ni ni-list"></em><span>Assign Currency</span></a></li>';
                            //str += '<li><a href="Remittance/MerchantOrdersList?MerchantUniqueId=' + data.MerchantUniqueId + '""><em class="icon ni ni-list"></em><span>Orders List</span></a></li>';
                            str += '<li><a href="javascript:void(0)" onclick="return ResetMerchantKeys(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-setting-alt"></em><span>Reset Key</span></a></li>';
                            str += '<li><a href="javascript:void(0)" onclick="return ResetMerchantAPIPassword(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-setting-alt"></em><span>Reset API Password</span></a></li>';
                            //str += '<li><a href="Remittance/MerchantBankList?MerchantId=' + data.MerchantUniqueId + '" title="Add BankDetail"><em class="icon ni ni-wallet"></em><span>BankDetail</span></a></li>'; str += '</ul>';
                            str += '<li><a href="Remittance/RemittanceIPList?RemittanceId=' + data.MerchantUniqueId + '" title="Show Whitelist IPs"><em class="icon ni ni-wallet"></em><span>Whitelist IPs</span></a></li>';
                            str += '<li><a href="javascript:void(0)" onclick="return AddFeeBalance(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-edit-fill"></em><span>Update Fee Account</span></a></li>';
                            str += '</div>';
                            str += '</div>';
                            str += '</li>';
                            str += '</ul>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: ""
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblmerlist").deleteTFoot();

            $("#tblmerlist").append(
                $('<tfoot/>').append($("#tblmerlist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}



function ResetMerchantKeys(merchantid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm('This Action Will Reset Merchant Keys. Do You Really Want To Continue ??'))
        $.ajax({
            type: "POST",
            url: "/Remittance/ResetMerchantKeys",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"MerchantUniqueId":"' + merchantid + '"}',
            success: function (response) {
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMsg").html("Records not updated.");
                        return false;
                    }
                    else {
                        $("#dvMsgSuccess").html("successfully updated");
                        var tableId = $(this).data("table");
                        BindMerchantDataTable(tableId);
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

function ResetMerchantAPIPassword(merchantid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm('This Action Will Reset Merchant API Password. Do You Really Want To Continue ??'))
        $.ajax({
            type: "POST",
            url: "/Remittance/ResetMerchantAPIPassword",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"MerchantUniqueId":"' + merchantid + '"}',
            success: function (response) {
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMsg").html("Records not updated.");
                        return false;
                    }
                    else {
                        $("#dvMsgSuccess").html("successfully updated");
                        var tableId = $(this).data("table");
                        BindMerchantDataTable(tableId);
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

function ShowKeys(merchantid, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Remittance/ShowPublicPrivateKeys",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MerchantUniqueId":"' + merchantid + '"}',
        success: function (response) {
            debugger;
            if (response != null) {
                var jsondata = response.split("(");
                if (jsondata[0] != null || jsondata[0] != "" || jsondata[1] != null || jsondata[1] != "") {
                    $("#PublicPrivateKeys").modal('show');
                    $("#lblPublicKey").html(jsondata[1].replace(/\\n/g, "<br />"));
                    $("#lblPrivateKey").html(jsondata[0].replace(/\\n/g, "<br />"));
                    //$("#dvMsgSuccess").html("successfully updated");
                    //var tableId = $(this).data("table");
                    //BindMerchantDataTable(tableId);
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


function BlockUnblock(merchantid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Remittance/MerchantBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MerchantUniqueId":"' + merchantid + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");
                    var tableId = $(this).data("table");
                    BindMerchantDataTable(tableId);
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

function BindMerchantOrderDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            var MerchantId = $("#MerchantId").val();
            var OrderId = $("#OrderId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var TrackerId = $("#TrackerId").val();
            var MemberName = $("#MemberName").val();
            var MemberContactNumber = $("#MemberContactNumber").val();
            var Status = $("#status :selected").val();
            var Sign = $("#Sign :selected").val();
            var TransactionId = $("#TransactionId").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            table = $('#tbllist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No MerchantOrder"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetMerchantOrdersLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantId = MerchantId;
                        data.OrderId = OrderId;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.TrackerId = TrackerId;
                        data.MemberName = MemberName;
                        data.MemberContactNumber = MemberContactNumber;
                        data.Status = Status;
                        data.TransactionId = TransactionId;
                        data.Sign = Sign;
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    { "data": "TrackerId", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "OrderId", "name": "Order Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.MemberId == 0) {
                                return '<span><strong></strong></span>';
                            }
                            else {
                                return '<span><strong>' + data.MemberId + '</strong></span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    { "data": "MemberContactNumber", "name": "Member ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Sign == 1) {
                                return '<span class="tb-status text-success">' + data.SignName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.SignName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Sign"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                                TotalCredit = data.TotalCredit;
                                TotalDebit = data.TotalDebit;
                            }
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 2) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "CurrentBalance", "name": "Available Balance", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderDetail(&apos;' + data.CreatedDateDt + '&apos;,&apos;' + data.TransactionId + '&apos;,&apos;' + data.TrackerId + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos; ' + data.OrderId + ' &apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.MemberContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos; ' + data.ServiceCharges + ' &apos;,&apos; ' + data.TypeName + ' &apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    }
                    //{
                    //    data: null,
                    //    render: function (data, type, row) {
                    //        return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderStatusCheck(&apos;' + data.TransactionId + '&apos;)" title="" data-original-title="Status"><em class="icon ni ni-activity-alt"></em></a>';
                    //    },
                    //    bSortable: false,
                    //    sTitle: "Status"
                    //}
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            document.getElementById("tbllist").deleteTFoot();
            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindMerchantTxnDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var ContactNumber = $("#ContactNumber").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var GatewayTransactionId = $("#GatewayTransactionId").val();
            var ParentTransactionId = $("#ParentTransactionId").val();
            var Reference = $("#Reference").val();
            var SubscriberId = $("#SubscriberId").val();
            var Status = $("#status :selected").val();
            var Sign = $("#Sign :selected").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
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
                    "url": "/Remittance/GetMerchantTransactionsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = ContactNumber;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.GatewayTransactionId = GatewayTransactionId;
                        data.Status = Status;
                        data.Sign = Sign;
                        data.ParentTransactionId = ParentTransactionId;
                        data.Reference = Reference;
                        data.CustomerID = SubscriberId;
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDatedt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.VendorTransactionId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Gateway Txn Id"
                    },
                    { "data": "ParentTransactionId", "name": "Parent TransactionId", "autoWidth": true, "bSortable": true },
                    //{ "data": "Reference", "name": "Reference", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.TypeName == "BANK TRANSFER") {
                                return '<a href="/DepositOrder?TransactionId=' + data.Reference + '">' + data.Reference + '</a>';
                            }
                            else {
                                return '<span>' + data.Reference + '</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Tracker Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/AdminTransactions?memberid=' + data.MemberId + '" class="tb-lead">' + data.MemberName + '</a>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact No", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + (data.Type == "22" ? data.RecieverContactNumber : data.CustomerID) + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Subscriber Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-sub tb-amount">' + data.Amount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Amount (Rs)"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Sign == 1) {
                                return '<span class="tb-status text-success">' + data.SignName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.SignName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Sign"
                    },
                    { "data": "TypeName", "name": "Service", "autoWidth": true, "bSortable": false },
                    { "data": "TransactionAmount", "name": "Txn Amount", "autoWidth": true, "bSortable": false },
                    { "data": "RewardPoint", "name": "MPCoins Credit", "autoWidth": true, "bSortable": false },
                    { "data": "MPCoinsDebit", "name": "MPCoins Debit", "autoWidth": true, "bSortable": false },
                    { "data": "RewardPointBalance", "name": "MPCoins Balance", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousRewardPointBalance", "name": "Prev MPCoins Balance", "autoWidth": true, "bSortable": false },
                    { "data": "WalletTypeName", "name": "Txn Mode", "autoWidth": true, "bSortable": false },
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashBack", "name": "Cashback", "autoWidth": true, "bSortable": false },
                    { "data": "SenderBankName", "name": "Sender Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "SenderAccountNo", "name": "Sender AccountNo", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverBankName", "name": "Receiver Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverAccountNo", "name": "Receiver AccountNo", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "VendorTypeName", "name": "Gateway Name", "autoWidth": true, "bSortable": false },
                    { "data": "GatewayStatus", "name": "Gateway Status", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                                TotalCredit = data.TotalCredit;
                                TotalDebit = data.TotalDebit;
                            }
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "My Pay Status"
                    },
                    { "data": "UpdateByName", "name": "Update By", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.CustomerID + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.SenderBankName + '&apos;,&apos;' + data.SenderAccountNo + '&apos;,&apos;' + data.RecieverBankName + '&apos;,&apos;' + data.RecieverAccountNo + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.CashBack + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';

                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/TransactionReceipt/Index?transactionid=' + data.TransactionUniqueId + '"  class="btn btn-primary btn-sm btn-icon btn-tooltip"><em class="icon ni ni-wallet-out"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Download"
                    },
                    //{
                    //    data: null,
                    //    render: function (data, type, row) {
                    //        if (hdnrole == "8") {
                    //            return '';
                    //        }
                    //        else {
                    //            if (data.Status == 2) {
                    //                return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return ChangeTxnStatus(&apos;' + data.TransactionUniqueId + '&apos;)" title="" data-original-title="Change Status"><em class="icon ni ni-pen2"></em></a>';
                    //            }
                    //            else {
                    //                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled" title="Change Status" style="display:none" data-original-title="Change Status"><em class="icon ni ni-pen2"></em></a>';
                    //            }
                    //        }
                    //    },
                    //    bSortable: false,
                    //    sTitle: "Change Status"
                    //},
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindRemittanceBankDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Bank Detail"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetRemittanceBankLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantId = $("#MerchantId").val();
                        data.Name = $("#Name").val();
                        data.BankCode = $("#BankCode").val();
                        data.BankName = $("#BankName").val();
                        data.BranchName = $("#BranchName").val();
                        data.AccountNumber = $("#AccountNumber").val();
                        data.StartDate = $("#fromdate").val();
                        data.EndDate = $("#todate").val();
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
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "Name", "name": "Name", "autoWidth": true, "bSortable": false },
                    { "data": "BankCode", "name": "Bank Code", "autoWidth": true, "bSortable": false },
                    { "data": "BankName", "name": "Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "BranchName", "name": "Token", "autoWidth": true, "bSortable": false },
                    { "data": "AccountNumber", "name": "Account Number", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str += '<a href="Remittance/AddRemittanceBankDetail?MerchantId=' + data.MerchantId + '"  class="btn btn-primary btn-sm btn-icon" title="Edit">';
                            str += '<em class="icon ni ni-edit"></em>';
                            str += '</a>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: "Edit"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    //var oSettings = this.fnSettings();
                    //$("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
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

function BindLoginMerchantOrderDataTable(pagetype) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var OrderId = $("#OrderId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var TrackerId = $("#TrackerId").val();
            var MemberName = $("#MemberName").val();
            var MemberContactNumber = $("#MemberContactNumber").val();
            var Status = $("#status :selected").val();
            /*var Sign = $("#Sign :selected").val();*/
            var TransactionId = $("#TransactionId").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            var pgtype = pagetype;
            table = $('#tbllist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No MerchantOrder"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetLoginMerchantOrdersLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.OrderId = OrderId;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.TrackerId = TrackerId;
                        data.MemberName = MemberName;
                        data.MemberContactNumber = MemberContactNumber;
                        data.Status = Status;
                        data.TransactionId = TransactionId;
                        /*data.Sign = Sign;*/
                        data.PageType = pgtype;
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    { "data": "TrackerId", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "OrderId", "name": "Order Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.MemberId == 0) {
                                return '<span><strong></strong></span>';
                            }
                            else {
                                return '<span><strong>' + data.MemberId + '</strong></span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    { "data": "MemberContactNumber", "name": "Member ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Sign == 1) {
                                return '<span class="tb-status text-success">' + data.SignName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.SignName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Sign"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                                TotalCredit = data.TotalCredit;
                                TotalDebit = data.TotalDebit;
                            }
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 2) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "CurrentBalance", "name": "Available Balance", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderDetail(&apos;' + data.CreatedDateDt + '&apos;,&apos;' + data.TransactionId + '&apos;,&apos;' + data.TrackerId + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos; ' + data.OrderId + ' &apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.MemberContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos; ' + data.ServiceCharges + ' &apos;,&apos; ' + data.TypeName + ' &apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    }
                    //{
                    //    data: null,
                    //    render: function (data, type, row) {
                    //        return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderStatusCheck(&apos;' + data.TransactionId + '&apos;)" title="" data-original-title="Status"><em class="icon ni ni-activity-alt"></em></a>';
                    //    },
                    //    bSortable: false,
                    //    sTitle: "Status"
                    //}
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            document.getElementById("tbllist").deleteTFoot();
            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindLoginMerchantWithdrawalReqDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#status :selected").val();
            var FilterTotalCount = 0;
            table = $('#tbllist').DataTable({
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/RemittanceOrders/GetMerchantWithdrawalRequestsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Status = Status;
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
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },

                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                            }
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 2) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderDetail(&apos;' + data.CreatedDateDt + '&apos;,&apos;' + data.UpdatedDateDt + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.Remarks + '&apos;,&apos;' + data.Status + '&apos;,&apos; ' + data.StatusName + ' &apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
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
            document.getElementById("tbllist").deleteTFoot();
            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindMerchantWithdrawalReqDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MerchantId = $("#MerchantId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#status :selected").val();
            var FilterTotalCount = 0;
            table = $('#tbllist').DataTable({
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetMerchantWithdrawalRequestsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Status = Status;
                        data.MerchantId = MerchantId;
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
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                            }
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 2) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderDetail(&apos;' + data.CreatedDateDt + '&apos;,&apos;' + data.UpdatedDateDt + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.Remarks + '&apos;,&apos;' + data.Status + '&apos;,&apos; ' + data.StatusName + ' &apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Status == 4 && !data.IsApprovedByAdmin) {
                                var str = "";
                                str = '<a  href="javascript:void(0);" id="Accept' + data.Id + '"   class="btn btn-sm btn-success" style="white-space:nowrap;"  title="Accept" onclick="return Approve(&apos;' + data.Id + '&apos;,&apos;approve&apos;)">Accept</a>';
                                str += '<a  href="javascript:void(0);" id="Reject' + data.Id + '"   class="btn btn-sm btn-danger" style="white-space:nowrap;"  title="Reject" onclick="return DisApprove(&apos;' + data.Id + '&apos;,&apos;disapprove&apos;)">Reject</a>';
                                return str;
                            }
                            else {
                                return '';
                            }
                        },
                        bSortable: false,
                        sTitle: "Action"
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
            document.getElementById("tbllist").deleteTFoot();
            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function Approve(id, requesttype) {
    $.ajax({
        type: "POST",
        url: "/Remittance/ApproveWithdrawalRequest",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Id":"' + id + '","RequestType":"' + requesttype + '"}',
        success: function (response) {
            if (response != null) {
                if (response == "success") {
                    $("#dvPopupMsg").html(response);
                    BindMerchantWithdrawalReqDataTable();
                }
                else {
                    $("#dvPopupMsg").html(response);
                    //BindMerchantWithdrawalReqDataTable();
                }
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

function ShowAPIKeyPassword(APIKey, APIPassword, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#APIKey").modal('show');
    if (APIKey != "" || APIPassword != "") {
        $("#lblAPIKey").html(APIKey);
        $("#lblAPIPassword").html(APIPassword);
    }
    else {
        $("#lblAPIKey").html("Something went wrong");
    }
}

function ShowIpModal(RemittanceIdUniqueId) {
    $('#hdnRemittanceIdUniqueId').val(RemittanceIdUniqueId);
    $("#RemittanceIpAddress").val("");
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#RemittanceIp').modal('show');
}
function BindRemittanceIpDataTable() {

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Remittance Ip"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetRemittanceIpLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.RemittanceId = $("#RemittanceId").val();
                        data.Name = $("#Name").val();
                        data.Organization = $("#Organization").val();
                        data.IpAddress = $("#IpAddress").val();
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

                    { "data": "RemittanceName", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "RemittanceUniqueId", "name": "Remittance Id", "autoWidth": true, "bSortable": true },
                    { "data": "RemittanceName", "name": "Name", "autoWidth": true, "bSortable": true },
                    { "data": "RemittanceOrganization", "name": "Organization", "autoWidth": true, "bSortable": false },
                    { "data": "IPAddress", "name": "Ip Address", "autoWidth": true, "bSortable": true },


                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<label class="tb-status text-success">Active</label>';
                            }
                            else {
                                return '<label class="tb-status text-danger">In-Active</label>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },

                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';

                            if (data.IsActive) {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblockIp(&apos;' + data.Id + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                str += '<em class="icon ni ni-user-cross-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }
                            else {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblockIp(&apos;' + data.Id + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                str += '<em class="icon ni ni-user-check-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }

                            str += '<li>';
                            str += '<div class="drodown">';
                            str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                            str += '<div class="dropdown-menu dropdown-menu-right">';
                            str += '<ul class="link-list-opt no-bdr">';
                            str += '<li class="divider"></li>';
                            if (data.IsActive) {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblockIp(&apos;' + data.Id + '&apos;,event)"><em class="icon ni ni-user-cross-fill"></em><span>Disable Ip</span></a></li>';
                            }
                            else {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblockIp(&apos;' + data.Id + '&apos;,event)"><em class="icon ni ni-user-check-fill"></em><span>Enable Ip</span></a></li>';
                            }

                            str += '</ul>';
                            str += '</div>';
                            str += '</div>';
                            str += '</li>';
                            str += '</ul>';
                            return str;
                        },
                        bSortable: false,
                        sTitle: ""
                    }


                    //{
                    //    bSortable: false,
                    //    sTitle: ""
                    //}
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
function AddIp(e) {
    debugger;
    e.stopPropagation();
    $('#RemittanceIp').modal('toggle');
    $('#AjaxLoader').show();
    var RemittanceIpAddress = $("#RemittanceIpAddress").val();
    var RemittanceIdUniqueId = $("#hdnRemittanceIdUniqueId").val();

    if (RemittanceIpAddress == "") {
        $("#dvMsg").html("Please enter Remittance Ip Address.");
        $('#AjaxLoader').hide();
        return false;
    }
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Remittance/AddRemittanceIp",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MerchantUniqueId":"' + RemittanceIdUniqueId + '","MerchantIpAddress":"' + RemittanceIpAddress + '"}',
        success: function (response) {
            debugger;
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Remittance Ip not saved.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("Remittance Ip successfully added");
                    $('#AjaxLoader').hide();

                    return false;
                }
            }
            else {
                $("#dvMsg").html("Something went wrong. Please try again later.");
                $('#AjaxLoader').hide();
                return false;
            }
        },
        failure: function (response) {
            $("#dvMsg").html(response.responseText);
            $('#AjaxLoader').hide();
            return false;
        },
        error: function (response) {
            $("#dvMsg").html(response.responseText);
            $('#AjaxLoader').hide();
            return false;
        }
    });
}

function BlockUnblockIp(id, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Remittance/RemittanceIpBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Id":"' + id + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("Successfully Updated Remittance Ip");
                    var tableId = $(this).data("table");
                    BindRemittanceIpDataTable(tableId)
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

} function AddFeeBalance(merchantid, e) {
    $('#AddFeeBalance').modal('show');
    $("#txtFeeTxnId").val("");
    $("#txtFeeRemarks").val("");
    $("#txtFeeAmount").val("");
    $("#dvFeePopupMsg").html("");
    $(".custom-file-label").html("");
    $("#targetReceiptImage").attr("src", "Content/assets/Images/noimageblank.png");
    $("#hdnimage").val("");
    $("#hdnMerchantId").val(merchantid);
}

function SubmitAddFee() {
    debugger;
    $("#dvSuccessMsg").html("");
    $("#dvFailedMsg").html("");
    var Sign = $("#drpFeeSign option:selected").val();
    var TxnId = $("#txtFeeTxnId").val();
    //var TxnFile = $("#TxnReceiptFile").val();
    var Remarks = $("#txtFeeRemarks").val();
    var Amount = $("#txtFeeAmount").val();
    var filename = $("#hdnimage").val();
    var Type = "FeeAccountBalance";
    if (Sign == "1") {
        if (TxnId == "") {
            $("#dvFeePopupMsg").html("Please enter Bank Txn. Id for Credit Fee Balance.");
            return false;
        }
        if (filename == "") {
            $("#dvFeePopupMsg").html("Please upload Receipt for Credit Fee Balance.");
            return false;
        }
    }
    if (Amount == "" || Amount == "0") {
        $("#dvFeePopupMsg").html("Please enter valid Amount.");
        return false;
    }
    if (Remarks == "") {
        $("#dvFeePopupMsg").html("Please enter Remarks.");
        return false;
    }
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/Remittance/AddFeeBalance",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"MerchantUniqueId":"' + $("#hdnMerchantId").val() + '","Type":"' + Type + '","Sign":"' + Sign + '","TxnId":"' + TxnId + '","Remarks":"' + Remarks + '","Amount":"' + Amount + '","ReceiptFileName":"' + filename + '"}',
                success: function (response) {
                    if (response == "success") {
                        if (Sign == "1") {
                            alert("Successfully Credit Fee Account Balance.");
                            $("#dvFeePopupMsg").html("Successfully Credit Fee Account Balance");
                        }
                        else if (Sign == "2") {
                            alert("Successfully Debit Fee Account Balance from Selected Wallet");
                            $("#dvFeePopupMsg").html("Successfully Debit Fee Account Balance");
                        }
                        $('#AddFeeBalance').modal('hide');
                        $("#txtFeeTxnId").val("");
                        $("#txtFeeRemarks").val("");
                        $("#txtFeeAmount").val("");
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