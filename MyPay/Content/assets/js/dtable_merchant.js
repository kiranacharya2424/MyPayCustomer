///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////
///////////////////////////////////////////////////

var table;

function BindMerchantDataTable() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MerchantUniqueId = $("#MerchantUniqueId").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#FirstName").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            var MerchantType = $("#selectedMerchantType").val();
            var roleid = $("#hdnroleid").val();
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
                    "url": "/Merchant/GetMerchantLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantUniqueId = MerchantUniqueId;
                        data.ContactNumber = Mobile;
                        data.Email = Email;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Name = Name;
                        data.MerchantType = MerchantType;
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
                            $('#merchanttotalcount').html(data.TotalUserCount);

                            return '<span>' + data.FirstName + " " + data.LastName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Full Name"
                    },
                    { "data": "UserName", "name": "User Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNo", "name": "Contact", "autoWidth": true, "bSortable": false },
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="Merchant/MerchantOrdersList?MerchantUniqueId=' + data.MerchantUniqueId + '">' + data.MerchantUniqueId + '</a>';
                        },
                        bSortable: false,
                        sTitle: "Merchant Id"
                    },
                    //{ "data": "MerchantUniqueId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "OrganizationName", "name": "Organization Name", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantTypeName", "name": "Merchant Type", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantTotalAmount", "name": "Merchant Balance", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (roleid == "1") {
                                return '<a id="WalletBalance' + data.UserMemberId + '" href="javascript:void(0);" onclick="return ShowMerchantWalletBalance(&apos;' + data.UserMemberId + '&apos;, event)">Show</a><span id="spanWalletBalance' + data.UserMemberId + '"></span>';
                            }
                            else {
                                return '';
                            }
                        },
                        bSortable: false,
                        sTitle: "Wallet Balance"
                    },
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
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (roleid == "1") {
                                return '<a href="javascript:void(0);" onclick="return ShowIpModal(&apos;' + data.MerchantUniqueId + '&apos;)">' + data.MerchantIpAddress + '</a>';
                            }
                            else {
                                return '';
                            }
                        },
                        bSortable: false,
                        sTitle: "Ip Address"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (roleid == "1") {
                                return data.apikey;
                            }
                            else {
                                return '';
                            }
                        },
                        bSortable: false,
                        sTitle: "Key"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (roleid == "1") {
                                return data.API_Password;
                            }
                            else {
                                return '';
                            }
                        },
                        bSortable: false,
                        sTitle: "API Password"
                    },
                    //{ "data": "MerchantIpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    //{ "data": "apikey", "name": "Key", "autoWidth": true, "bSortable": false },
                    //{ "data": "API_Password", "name": "API Password", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            if (roleid == "1") {
                                str = '<a href="javascript:void(0);" onclick="return ShowKeys(&apos;' + data.MerchantUniqueId + '&apos;,event)" title="Show">Show</a>';
                            }
                            else {
                                str = '';
                            }
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
                            str += '<a href="Merchant/Index?MerchantUniqueId=' + data.MerchantUniqueId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
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
                            str += '<li><a href="Merchant/Index?MerchantUniqueId=' + data.MerchantUniqueId + '""><em class="icon ni ni-eye"></em><span>View Details</span></a></li>';
                            str += '<li class="divider"></li>';
                            str += '<li><a href="MerchantCommissionSetup?MerchantUniqueId=' + data.MerchantUniqueId + '" title="Setup Commission"><em class="icon ni ni-plus-circle-fill"></em><span>Setup Commission</span></a></li>';
                            if (data.IsActive) {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-user-cross-fill"></em><span>Disable User</span></a></li>';
                            }
                            else {
                                str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-user-check-fill"></em><span>Enable User</span></a></li>';
                            }
                            str += '<li><a href="Merchant/MerchantOrdersList?MerchantUniqueId=' + data.MerchantUniqueId + '""><em class="icon ni ni-list"></em><span>Orders List</span></a></li>';
                            str += '<li><a href="Merchant/CreditDebit?MerchantId=' + data.MerchantUniqueId + '"><em class="icon ni ni-wallet-alt"></em><span>Update Wallet</span></a></li>';
                            str += '<li><a href="javascript:void(0)" onclick="return ResetMerchantKeys(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-setting-alt"></em><span>Reset Key</span></a></li>';
                            str += '<li><a href="javascript:void(0)" onclick="return ResetMerchantAPIPassword(&apos;' + data.MerchantUniqueId + '&apos;,event)"><em class="icon ni ni-setting-alt"></em><span>Reset API Password</span></a></li>';
                            str += '<li><a href="Merchant/MerchantBankList?MerchantId=' + data.MerchantUniqueId + '" title="Add BankDetail"><em class="icon ni ni-wallet"></em><span>BankDetail</span></a></li>';
                            str += '<li><a href="Merchant/MerchantWithdrawalRequests?MerchantId=' + data.MerchantUniqueId + '" title="Show Withdrawal Requests"><em class="icon ni ni-list"></em><span>Withdrawal Requests</span></a></li>';
                            str += '<li><a href="Merchant/MerchantIPList?MerchantId=' + data.MerchantUniqueId + '" title="Show Whitelist IPs"><em class="icon ni ni-wallet"></em><span>Whitelist IPs</span></a></li>';
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



function ShowMerchantWalletBalance(UserMemberId, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/Merchant/ShowMerchantWalletBalance",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"UserMemberId":"' + UserMemberId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        var jsondata = response;
                        $("#WalletBalance" + UserMemberId).hide();
                        $("#spanWalletBalance" + UserMemberId).html(jsondata);
                        $("#spanWalletBalance" + UserMemberId).show();
                        e.preventDefault();
                        e.stopPropagation();
                        return false;
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
            $('#AjaxLoader').hide();
        }, 100);

}


function BindMerchantIpDataTable() {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Merchant Ip"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Merchant/GetMerchantIpLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MerchantId = $("#MerchantId").val();
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

                    { "data": "MechantName", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "MerchantUniqueId", "name": "Merchant Id", "autoWidth": true, "bSortable": true },
                    { "data": "MechantName", "name": "Name", "autoWidth": true, "bSortable": true },
                    { "data": "MerchantOrganization", "name": "Organization", "autoWidth": true, "bSortable": false },
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
function ShowIpModal(MerchantId) {
    $('#hdnMerchantId').val(MerchantId);
    $("#MerchantIpAddress").val("");
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#merchantIp').modal('show');
}
function AddIp() {
    debugger;
    var MerchantIpAddress = $("#MerchantIpAddress").val();
    var merchantid = $("#hdnMerchantId").val();

    if (MerchantIpAddress == "") {
        $("#dvMsg").html("Please enter Merchant Ip Address.");
        return false;
    }
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Merchant/AddMerchantIp",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MerchantUniqueId":"' + merchantid + '","MerchantIpAddress":"' + MerchantIpAddress + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Merchant Ip not saved.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("Merchant Ip successfully added");

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
function ResetMerchantKeys(merchantid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm('This Action Will Reset Merchant Keys. Do You Really Want To Continue ??'))
        $.ajax({
            type: "POST",
            url: "/Merchant/ResetMerchantKeys",
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
            url: "/MerchantLogin/ResetMerchantAPIPassword",
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
        url: "/Merchant/ShowPublicPrivateKeys",
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
        url: "/Merchant/MerchantBlockUnblock",
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


function BlockUnblockBank(merchantid, id, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Merchant/MerchantBankBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MerchantUniqueId":"' + merchantid + '","Id":"' + id + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");
                    var tableId = $(this).data("table");
                    BindMerchantBankDataTable(tableId);
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
function BlockUnblockIp(id, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Merchant/MerchantIpBlockUnblock",
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
                    $("#dvMsgSuccess").html("Successfully Updated Merchant Ip");
                    var tableId = $(this).data("table");
                    BindMerchantIpDataTable(tableId);
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
            var Type = $("#Type :selected").val();
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
                    "url": "/Merchant/GetMerchantOrdersLists",
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
                        data.Type = Type;
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
                    { "data": "OrganizationName", "name": "Organization Name", "autoWidth": true, "bSortable": false },
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
                    { "data": "ServiceCharges", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashbackAmount", "name": "Cashback Amount", "autoWidth": true, "bSortable": false },
                    { "data": "DiscountAmount", "name": "Discount", "autoWidth": true, "bSortable": false },
                    { "data": "CommissionAmount", "name": "Commission", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantContributionPercentage", "name": "Contribution", "autoWidth": true, "bSortable": false },
                    //{ "data": "NetAmount", "name": "PaidAmount", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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


function BindMerchantWithdrawOrderDataTable() {
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
                    "url": "/Merchant/GetMerchantWithdrawOrdersLists",
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
                    { "data": "OrganizationName", "name": "Organization Name", "autoWidth": true, "bSortable": false },
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
                    { "data": "ServiceCharges", "name": "Service Charge", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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
                    //{ "data": "PreviousBalance", "name": "Previous Balance", "autoWidth": true, "bSortable": false },
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
            var Type = $("#Type :selected").val();
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
                    "url": "/Merchant/GetMerchantTransactionsLists",
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
                        data.Type = Type;
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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

function BindMerchantBankDataTable() {
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Merchant/GetMerchantBankLists",
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
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": true },
                    { "data": "Name", "name": "Name", "autoWidth": true, "bSortable": true },
                    { "data": "BankCode", "name": "Bank Code", "autoWidth": true, "bSortable": false },
                    { "data": "BankName", "name": "Bank Name", "autoWidth": true, "bSortable": true },
                    { "data": "BranchName", "name": "Token", "autoWidth": true, "bSortable": true },
                    { "data": "AccountNumber", "name": "Account Number", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsVerified) {
                                return '<label class="tb-status text-success">Verified</label>';
                            }
                            else {
                                return '<label class="tb-status text-danger">Not Verified</label>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Verified"
                    },
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
                            if (data.IsActive) {
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblockBank(&apos;' + data.MerchantId + '&apos;,&apos;' + data.Id + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                str += '<em class="icon ni ni-user-cross-fill"></em>';
                                str += '</a>';
                            }
                            else {
                                str += '<a href="javascript:void(0);" onclick="return BlockUnblockBank(&apos;' + data.MerchantId + '&apos;,&apos;' + data.Id + '&apos;,event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                str += '<em class="icon ni ni-user-check-fill"></em>';
                                str += '</a>';
                            }
                            return str;
                        },
                        bSortable: false,
                        sTitle: ""
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str += '<em class="icon ni ni-edit"></em>';
                            str += '</a>';

                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li>';
                            str += '<div class="drodown">';
                            str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                            str += '<div class="dropdown-menu dropdown-menu-right">';
                            str += '<ul class="link-list-opt no-bdr">';
                            str += '<li><a href="Merchant/MerchantBankDetail?MerchantId=' + data.MerchantId + '" onclick="showloader();"  title="Edit"><em class="icon ni ni-edit"></em><span>Edit</span></a></li>';
                            //str += '<li><a href="javascript:void(0)" title="Verify" onclick="showVerificationPopup(\'' + data.Id + '\',\'' + data.MerchantId + '\',\'' + data.AccountNumber + '\')" ><em class="icon ni ni-shield-star"></em><span>Verify</span></a></li>';
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
function DeleteMerchantBank(Id) {

}
function showVerificationPopup(BankId, MerchantId, AccountNo) {
    $("#hdnBankId").val(BankId);
    $("#hdnMerchantId").val(MerchantId);
    $("#hdnAccountNo").val(AccountNo);

    $("#spnMerchantId").html(MerchantId);
    $("#spnBankName").html(BankId);
    $("#spnAccountNo").html(AccountNo);
    $("#verificationrequestOTP").modal("show");
}

function showloader() {
    $('#AjaxLoader').show();
    setTimeout(function () {
        $('#AjaxLoader').hide();
    }, 2000);
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
                    "url": "/MerchantOrders/GetLoginMerchantOrdersLists",
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
                    { "data": "ServiceCharges", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "DiscountAmount", "name": "Discount", "autoWidth": true, "bSortable": false },
                    { "data": "CommissionAmount", "name": "Commission", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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



function BindLoginMerchantOrderDataTableWithdraw(pagetype) {
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
                    "url": "/MerchantOrders/GetLoginMerchantOrdersLists",
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
                    { "data": "ServiceCharges", "name": "Service Charge", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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
                    //{ "data": "PreviousBalance", "name": "Previous Balance", "autoWidth": true, "bSortable": false },
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
            debugger;
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#status :selected").val();
            var Type = $("#Type :selected").val();
            var TypeName = $("#Type :selected").text();
            if (TypeName == "Select Type") {
                Type = -1;
            }
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
                    "url": "/MerchantOrders/GetMerchantWithdrawalRequestsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Status = Status;
                        data.Type = Type;
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
                    { "data": "WithdrawalRequestTypeName", "name": "Type", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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
                    "url": "/Merchant/GetMerchantWithdrawalRequestsLists",
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
                    { "data": "MerchantName", "name": "Name", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantContactNumber", "name": "ContactNumber", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantOrganization", "name": "Organization", "autoWidth": true, "bSortable": false },
                    { "data": "OrderId", "name": "OrderId", "autoWidth": true, "bSortable": false },
                    { "data": "TrackerId", "name": "TrackerId", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "BankName", "name": "BankName", "autoWidth": true, "bSortable": false },
                    { "data": "AccountNumber", "name": "AccountNumber", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "WithdrawalRequestTypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                            }
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderDetail(&apos;' + data.CreatedDateDt + '&apos;,&apos;' + data.UpdatedDateDt + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos;' + data.MerchantName + '&apos;,&apos;' + data.MerchantContactNumber + '&apos;,&apos;' + data.MerchantOrganization + '&apos;,&apos;' + data.OrderId + '&apos;,&apos;' + data.TrackerId + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.BankName + '&apos;,&apos;' + data.AccountNumber + '&apos;,&apos;' + data.Remarks + '&apos;,&apos;' + data.WithdrawalRequestTypeName + '&apos;,&apos; ' + data.StatusName + ' &apos;,&apos; ' + data.Status + ' &apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Status == 7 && !data.IsWithdrawalApproveByAdmin) {
                                var str = "";
                                str = '<a  href="javascript:void(0);" id="Accept' + data.Id + '"   class="btn btn-sm btn-success" style="white-space:nowrap;"  title="Accept" onclick="return Approve(&apos;' + data.Id + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos;approve&apos;)">Accept</a> ';
                                str += '<a  href="javascript:void(0);" id="Reject' + data.Id + '"   class="btn btn-sm btn-danger" style="white-space:nowrap;"  title="Reject" onclick="return Approve(&apos;' + data.Id + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos;disapprove&apos;)">Reject</a>';
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

function Approve(id, MerchantUniqueId, requesttype) {
    if (confirm('Do you want to ' + requesttype + ' the withdrawal Request ?')) {


        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/Merchant/ApproveWithdrawalRequest",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Id":"' + id + '","MerchantUniqueId":"' + MerchantUniqueId + '","RequestType":"' + requesttype + '"}',
                    success: function (response) {
                        if (response != null) {
                            if (response == "success") {
                                alert("Withdrawal Request " + requesttype + "  Successfully.");
                                BindMerchantWithdrawalReqDataTable();
                            }
                            else {
                                alert(response);
                                BindMerchantWithdrawalReqDataTable();
                            }
                        }
                        else {
                            JsonOutput = "Something went wrong. Please try again later.";
                        }
                    },
                    failure: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvMsg").html(JsonOutput);
                    },
                    error: function (response) {
                        JsonOutput = (response.responseText);
                        $("#dvMsg").html(JsonOutput);
                    }
                });
                $('#AjaxLoader').hide();
            }, 100);
    }
}

function BindLoginMerchantSettlementReportDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            //var OrderId = $("#OrderId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var TrackerId = $("#TrackerId").val();
            var MemberName = $("#MemberName").val();
            var MemberContactNumber = $("#MemberContactNumber").val();
            var Status = $("#Status :selected").val();
            var Type = $("#Type :selected").val();
            var Sign = $("#Sign :selected").val();
            var TransactionId = $("#TransactionId").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            //var pgtype = pagetype;
            table = $('#tbllist').DataTable({
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/MerchantOrders/GetMerchantSettlementReportLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        //data.OrderId = OrderId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Reference = TrackerId;
                        data.MemberName = MemberName;
                        data.MemberContactNumber = MemberContactNumber;
                        data.Status = Status;
                        data.TransactionId = TransactionId;
                        data.Type = Type;
                        data.Sign = Sign;
                        //data.PageType = pgtype;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverName", "name": "Reciever Name", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverContactNumber", "name": "Reciever ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
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
                    //{
                    //    data: null,
                    //    render: function (data, type, row) {
                    //        return '';//'<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetOrderDetail(&apos;' + data.CreatedDateDt + '&apos;,&apos;' + data.TransactionId + '&apos;,&apos;' + data.TrackerId + '&apos;,&apos;' + data.MerchantId + '&apos;,&apos; ' + data.OrderId + ' &apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.MemberContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos; ' + data.ServiceCharges + ' &apos;,&apos; ' + data.TypeName + ' &apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                    //    },
                    //    bSortable: false,
                    //    sTitle: "View"
                    //}
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
$("#btnSubmitMerchantTransfer").click(function () {
    $("#dvMessage").html("");
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
            return confirm('This will update merhcnat wallet. Do you want to continue ?');
        }
        else if ($("#Type").val() == "1") {
            return confirm('This will update merchant MP Coins. Do you want to continue ?');
        }
    }

});

function BindLoginMerchantDirectwalletReportDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var TrackerId = $("#TrackerId").val();
            var Status = $("#Status :selected").val();
            var Sign = $("#Sign :selected").val();
            var TransactionId = $("#TransactionId").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            table = $('#tbllist').DataTable({
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/MerchantOrders/GetMerchantDirectWalletLoadReportLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Reference = TrackerId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantOrganization", "name": "Organization", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Reciever Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNumber", "name": "Reciever ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false }
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


function BindMerchantDirectwalletReportDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var TrackerId = $("#TrackerId").val();
            var Status = $("#Status :selected").val();
            var Sign = $("#Sign :selected").val();
            var TransactionId = $("#TransactionId").val();
            var MerchantId = $("#MerchantId").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            table = $('#tbllist').DataTable({
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Merchant/GetMerchantDirectWalletLoadReportLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Reference = TrackerId;
                        data.Status = Status;
                        data.TransactionId = TransactionId;
                        data.MerchantId = MerchantId;
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
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantId", "name": "Merchant Id", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantOrganization", "name": "Organization", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Reciever Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNumber", "name": "Reciever ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
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
                            else if (data.Status == 2 || data.Status == 3) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 7) {
                                return '<span class="tb-status text-pink">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false }
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