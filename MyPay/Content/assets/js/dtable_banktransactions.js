///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var MemberName = $("#MemberName").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var GatewayTransactionId = $("#GatewayTransactionId").val();
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var Reference = $("#Reference").val();
            var VendorType = $("#VendoTypes :selected").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Bank Transactions"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "searching": false,
                "sorting": false,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/BankTransactions/GetBankTransactionsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.MemberName = MemberName;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.GatewayTransactionId = GatewayTransactionId;
                        data.Status = Status;
                        data.Today = DayWise;
                        data.Reference = Reference;
                        data.VendorType = VendorType;
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
                    { "data": "UpdatedDatedt", "name": "Updated Date", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    //{ "data": "TransactionUniqueId", "name": "Transaction Id", "autoWidth": true, "bSortable": false },
                    { "data": "VendorTransactionId", "name": "Gateway Txn Id", "autoWidth": true, "bSortable": false },
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "BatchId", "name": "Batch Id", "autoWidth": true, "bSortable": false },
                    { "data": "InstructionId", "name": "Instruction Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Member Id"
                    },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.Amount + '</span>';
                        },
                        bSortable: false,
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
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "CreditStatus", "name": "Credit Status", "autoWidth": true, "bSortable": false },
                    { "data": "DebitStatus", "name": "Debit Status", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.VendorType == "1") {
                                return '<span class="tb-status text-success">' + data.VendorTypeName + '</span>';
                            }
                            else if (data.VendorType == "2") {
                                return '<span class="tb-status text-danger">' + data.VendorTypeName + '</span>';
                            }
                            else if (data.VendorType == "3") {
                                return '<span class="tb-status text-purple">' + data.VendorTypeName + '</span>';
                            }
                            else if (data.VendorType == "4") {
                                return '<span class="tb-status text-orange">' + data.VendorTypeName + '</span>';
                            }
                            else if (data.VendorType == "5") {
                                return '<span class="tb-status text-pink">' + data.VendorTypeName + '</span>';
                            }
                            else if (data.VendorType == "6") {
                                return '<span class="tb-status text-warning">' + data.VendorTypeName + '</span>';
                            }
                            else if (data.VendorType == "7") {
                                return '<span class="tb-status text-success">' + data.VendorTypeName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.VendorTypeName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Gateway"
                    },
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
                        bSortable: true,
                        sTitle: "My Pay Status"
                    },
                    { "data": "Purpose", "name": "Purpose", "autoWidth": true, "bSortable": false },
                    { "data": "SenderAccountNo", "name": "Sender AccountNo", "autoWidth": true, "bSortable": false },
                    { "data": "SenderBankName", "name": "Sender Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "SenderBranchName", "name": "Sender Branch Name", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "NetAmount", "name": "Net Amount (Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="#" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.TransactionUniqueId + ' &apos;,&apos; ' + data.VendorTransactionId + ' &apos;,&apos; ' + data.Reference + ' &apos;,&apos; ' + data.BatchId + ' &apos;,&apos; ' + data.InstructionId + ' &apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SignName + ' &apos;,&apos; ' + data.TypeName + ' &apos;,&apos; ' + data.CreditStatus + ' &apos;,&apos; ' + data.DebitStatus + ' &apos;,&apos; ' + data.GatewayStatus + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos; ' + data.Purpose + ' &apos;,&apos; ' + data.SenderAccountNo + ' &apos;,&apos; ' + data.SenderBankName + ' &apos;,&apos; ' + data.SenderBranchName + ' &apos;,&apos; ' + data.ServiceCharge + ' &apos;,&apos; ' + data.NetAmount + ' &apos;,&apos; ' + data.Remarks + ' &apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            
                                if (data.Status == 2) {
                                    return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return ChangeTxnStatus(&apos;' + data.TransactionUniqueId + '&apos;)" title="" data-original-title="Change Status"><em class="icon ni ni-pen2"></em></a>';
                                }
                                else {
                                    return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled" title="Change Status" style="display:none" data-original-title="Change Status"><em class="icon ni ni-pen2"></em></a>';
                                }
                            
                        },
                        bSortable: false,
                        sTitle: "Change Status"
                    },
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });

            if (document.getElementById("tblist") != undefined) {
                document.getElementById("tblist").deleteTFoot();
            }
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );

            $('#AjaxLoader').hide();
        }, 100);
}
function ChangeTxnStatus(TxnId) {
    $("#hdntxnid").val(TxnId);
    $('#TxnStatus').modal('show');
    $('#txtRemarks').val("");
    $("#dvPopupMsg").html("");
    return false;
}

function getNewVal(item) {
    if (item.value == 3) {
       $("#gatewaytxnid").css("display", "none");

    }
    else if (item.value == 1) {
        $("#checkrefund").css("display", "none");
        $("#gatewaytxnid").removeAttr("style");
    }
    else {
        $("#checkrefund").css("display", "none");
        $("#gatewaytxnid").css("display", "none");

    }
}
function SubmitTxnStatus() {
    var txnid = $("#hdntxnid").val();
    var selectedstatus = $("#drptxnstatus").val();
    var hdnChkRefund = $("#hdnChkRefund").val();
    var txtRemarks = $("#txtRemarks").val();
    var txtGatewayID = $("#txtGatewayID").val();

    if (selectedstatus == "" || selectedstatus == "0") {
        $("#dvPopupMsg").html('Please select status');
    }
    else if ($("#txtRemarks").val() == "") {
        $("#dvPopupMsg").html('Please enter remarks');
    }
    else {
        $.ajax({
            type: "POST",
            url: "/BankTransactions/ChangeTxnStatus",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"Status":"' + selectedstatus + '","TxnId":"' + txnid + '","hdnChkRefund":"' + hdnChkRefund + '","txtRemarks":"' + txtRemarks + '","txtGatewayID":"' + txtGatewayID + '"}',
            success: function (response) {
                debugger;
                if (response != null) {
                    debugger;
                    $("#dvPopupMsg").html(response);
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
}