///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var hdnrole = $("#hdnroleid").val();
            var MemberId = $("#MemberId").val();
            var MerchantUniqueId = $("#MerchantUniqueId").val();
            var ContactNumber = $("#ContactNumber").val();
            var TransactionId = $("#TransactionId").val();
            var ParentTransactionId = $("#ParentTransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var GatewayTransactionId = $("#GatewayTransactionId").val();
            var Reference = $("#Reference").val();
            var Status = $("#status :selected").val();
            var Sign = $("#Sign :selected").val();
            var WalletType = $("#WalletType :selected").val();
            var SourceCurrency = $("#SourceCurrency :selected").text();
            var DestinationCurrency = $("#DestinationCurrency :selected").text();
            var SourceCurrencyId = $("#SourceCurrency :selected").val();
            var DestinationCurrencyId = $("#DestinationCurrency :selected").val();
            var IsFeeAccountTransaction = $("#IsFeeAccountTransaction").val();

            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            var TotalServiceCharge = 0;
            var TotalCoinsCredit = 0;
            var TotalCoinsDebit = 0;
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
                    "url": "/Remittance/GetRemittanceSettlementReport",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.MerchantUniqueId = MerchantUniqueId;
                        data.ContactNumber = ContactNumber;
                        data.TransactionId = TransactionId;
                        data.ParentTransactionId = ParentTransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.GatewayTransactionId = GatewayTransactionId;
                        data.Status = Status;
                        data.Sign = Sign;
                        data.Reference = Reference;
                        data.WalletType = WalletType;
                        data.SourceCurrency = SourceCurrency;
                        data.DestinationCurrency = DestinationCurrency;
                        data.SourceCurrencyId = SourceCurrencyId;
                        data.DestinationCurrencyId = DestinationCurrencyId;
                        data.IsFeeAccountTransaction = IsFeeAccountTransaction;
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
                        sTitle: "SrNo"
                    },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MerchantUniqueId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "MerchantUniqueId"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: false,
                        sTitle: "Transaction Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.GatewayTransactionId + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Gateway Txn Id"
                    },
                    { "data": "ParentTransactionId", "name": "Parent Txn Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span>' + data.Reference + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Tracker Id"
                    },
                    { "data": "MerchantName", "name": "Merchant Name", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantContactNumber", "name": "Merchant ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "FromCurrency", "name": "From Currency", "autoWidth": true, "bSortable": false },
                    { "data": "FromAmount", "name": "From Amount", "autoWidth": true, "bSortable": false },
                    { "data": "ToCurrency", "name": "To Currency", "autoWidth": true, "bSortable": false },
                    { "data": "ToAmount", "name": "To Amount", "autoWidth": true, "bSortable": false },
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
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "ConversionRate", "name": "Conversion Rate", "autoWidth": true, "bSortable": false },
                    { "data": "ConvertedAmount", "name": "Converted Amount", "autoWidth": true, "bSortable": false },
                    { "data": "NetAmount", "name": "Net Amount", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "WalletTypeName", "name": "Wallet Type", "autoWidth": true, "bSortable": false },
                    { "data": "CurrentBalance", "name": "Current Balance", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance", "autoWidth": true, "bSortable": false },
                    { "data": "FeeTypeName", "name": "FeeType", "autoWidth": true, "bSortable": false },
                    { "data": "BeneficiaryName", "name": "Beneficiary Name", "autoWidth": true, "bSortable": false },
                    { "data": "BeneficiaryAccountNo", "name": "Beneficiary AccountNo", "autoWidth": true, "bSortable": false },
                    { "data": "BeneficiaryBankName", "name": "Beneficiary Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "BeneficiaryBranchName", "name": "Beneficiary Branch Name", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
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
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MerchantMemberId + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.GatewayTransactionId + '&apos;,&apos;' + data.ParentTransactionId + '&apos;,&apos;' + data.MerchantName + '&apos;,&apos;' + data.MerchantContactNumber + '&apos;,&apos;' + data.FromAmount + '&apos;,&apos;' + data.ToAmount + '&apos;,&apos;' + data.FromCurrency + '&apos;,&apos;' + data.ToCurrency + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.BeneficiaryBankName + '&apos;,&apos;' + data.BeneficiaryAccountNo + '&apos;,&apos;' + data.BeneficiaryBranchName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.ConversionRate + '&apos;,&apos;' + data.ConvertedAmount + '&apos;,&apos;' + data.WalletTypeName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Remarks + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            var totalTotalBalance = parseFloat(TotalCredit).toFixed(2) - parseFloat(TotalDebit).toFixed(2) - parseFloat(TotalServiceCharge).toFixed(2);
            $("#totalTotalBalance").html(" Rs." + parseFloat(totalTotalBalance).toFixed(2));


            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}


function BindDataTableFeeAccount() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var hdnrole = $("#hdnroleid").val();
            var MemberId = $("#MemberId").val();
            var MerchantUniqueId = $("#MerchantUniqueId").val();
            var ContactNumber = $("#ContactNumber").val();
            var TransactionId = $("#TransactionId").val();
            var ParentTransactionId = $("#ParentTransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var GatewayTransactionId = $("#GatewayTransactionId").val();
            var Reference = $("#Reference").val();
            var Status = $("#status :selected").val();
            var Sign = $("#Sign :selected").val();
            var WalletType = $("#WalletType :selected").val();
            var SourceCurrency = $("#SourceCurrency :selected").text();
            var DestinationCurrency = $("#DestinationCurrency :selected").text();
            var SourceCurrencyId = $("#SourceCurrency :selected").val();
            var DestinationCurrencyId = $("#DestinationCurrency :selected").val();
            var IsFeeAccountTransaction = $("#IsFeeAccountTransaction").val();

            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            var TotalServiceCharge = 0;
            var TotalCoinsCredit = 0;
            var TotalCoinsDebit = 0;
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
                    "url": "/Remittance/GetRemittanceSettlementReport",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.MerchantUniqueId = MerchantUniqueId;
                        data.ContactNumber = ContactNumber;
                        data.TransactionId = TransactionId;
                        data.ParentTransactionId = ParentTransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.GatewayTransactionId = GatewayTransactionId;
                        data.Status = Status;
                        data.Sign = Sign;
                        data.Reference = Reference;
                        data.WalletType = WalletType;
                        data.SourceCurrency = SourceCurrency;
                        data.DestinationCurrency = DestinationCurrency;
                        data.SourceCurrencyId = SourceCurrencyId;
                        data.DestinationCurrencyId = DestinationCurrencyId;
                        data.IsFeeAccountTransaction = IsFeeAccountTransaction;
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
                        sTitle: "SrNo"
                    },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MerchantUniqueId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "MerchantUniqueId"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: false,
                        sTitle: "Transaction Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.GatewayTransactionId + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Gateway Txn Id"
                    }, 
                    { "data": "MerchantName", "name": "Merchant Name", "autoWidth": true, "bSortable": false },
                    { "data": "MerchantContactNumber", "name": "Merchant ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "FromCurrency", "name": "From Currency", "autoWidth": true, "bSortable": false },
                    { "data": "FromAmount", "name": "From Amount", "autoWidth": true, "bSortable": false },
                    { "data": "ToCurrency", "name": "To Currency", "autoWidth": true, "bSortable": false },
                    { "data": "ToAmount", "name": "To Amount", "autoWidth": true, "bSortable": false },
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
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "ConversionRate", "name": "Conversion Rate", "autoWidth": true, "bSortable": false },
                    { "data": "ConvertedAmount", "name": "Converted Amount", "autoWidth": true, "bSortable": false },
                    { "data": "NetAmount", "name": "Net Amount", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false }, 
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
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
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MerchantMemberId + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.GatewayTransactionId + '&apos;,&apos;' + data.ParentTransactionId + '&apos;,&apos;' + data.MerchantName + '&apos;,&apos;' + data.MerchantContactNumber + '&apos;,&apos;' + data.FromAmount + '&apos;,&apos;' + data.ToAmount + '&apos;,&apos;' + data.FromCurrency + '&apos;,&apos;' + data.ToCurrency + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.BeneficiaryBankName + '&apos;,&apos;' + data.BeneficiaryAccountNo + '&apos;,&apos;' + data.BeneficiaryBranchName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.ConversionRate + '&apos;,&apos;' + data.ConvertedAmount + '&apos;,&apos;' + data.WalletTypeName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Remarks + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            var totalTotalBalance = parseFloat(TotalCredit).toFixed(2) - parseFloat(TotalDebit).toFixed(2) - parseFloat(TotalServiceCharge).toFixed(2);
            $("#totalTotalBalance").html(" Rs." + parseFloat(totalTotalBalance).toFixed(2));


            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}