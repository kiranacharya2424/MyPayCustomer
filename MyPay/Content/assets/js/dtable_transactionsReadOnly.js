

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
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Type = $("#typelist:selected").val();
            var ServiceMultiple = $("#selectedProvider").val();
            var WalletType = $("#WalletType :selected").val();
            var VendorType = $("#VendoTypes :selected").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
            var TotalServiceCharge = 0;
            var TotalCoinsCredit = 0;
            var TotalCoinsDebit = 0;
            var DiscountCoupon = 0;
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
                    "url": "/AdminTransactions/GetTransactionLists",
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
                        data.Today = DayWise;
                        data.Sign = Sign;
                        data.ParentTransactionId = ParentTransactionId;
                        data.Reference = Reference;
                        data.CustomerID = SubscriberId;
                        data.Type = Type;
                        data.TypeMultiple = ServiceMultiple;
                        data.WalletType = WalletType;
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.WalletTypeName == "WALLET") {
                                return '<span class="tb-status text-success">' + data.WalletTypeName + '</span>';
                            }
                            else if (data.WalletTypeName == "BANK") {
                                return '<span class="tb-status text-danger">' + data.WalletTypeName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">' + data.WalletTypeName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Txn Mode"
                    },
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashBack", "name": "Cashback", "autoWidth": true, "bSortable": false },
                    { "data": "CouponDiscount", "name": "Coupon Discount", "autoWidth": true, "bSortable": false },
                    { "data": "CouponCode", "name": "Coupon Code", "autoWidth": true, "bSortable": false },
                    { "data": "SenderBankName", "name": "Sender Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "SenderAccountNo", "name": "Sender AccountNo", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverBankName", "name": "Receiver Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverAccountNo", "name": "Receiver AccountNo", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
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
                                TotalServiceCharge = data.TotalServiceCharge;
                                TotalCoinsCredit = data.TotalCoinsCredit;
                                TotalCoinsDebit = data.TotalCoinsDebit;
                                DiscountCoupon = data.TotalCouponDiscount;
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
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
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
                            if (hdnrole == "8") {
                                return '';
                            }
                            else {
                                var strReceipt = '<a href="/TransactionReceipt/Index?transactionid=' + data.TransactionUniqueId + '"  class="btn btn-primary btn-sm btn-icon btn-tooltip"><em class="icon ni ni-wallet-out"></em></a>';
                                if (data.Type == "12" && data.VendorJsonLookup.trim() != "") {
                                    strReceipt = strReceipt + '<a style="margin-left:5px;" href="/NEAElectricityTransactionReceipt/Index?transactionid=' + data.TransactionUniqueId + '"  class="btn btn-primary btn-sm btn-icon btn-tooltip"><em class="icon ni ni-setting-alt"></em></a>';
                                }
                                if (data.Type == "13" && data.VendorJsonLookup.trim() != "") {
                                    strReceipt = strReceipt + '<a style="margin-left:5px;" href="/KhanepaniTransactionReceipt/Index?transactionid=' + data.TransactionUniqueId + '"  class="btn btn-primary btn-sm btn-icon btn-tooltip"><em class="icon ni ni-setting-alt"></em></a>';
                                }
                                return strReceipt;
                            }

                        },
                        bSortable: false,
                        sTitle: "Download"
                    },
                   
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            $("#totaltra").html(FilterTotalCount);
            $("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            $("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            $("#totalServiceCharge").html(" Rs." + parseFloat(TotalServiceCharge).toFixed(2));
            $("#totalCoinsCredit").html(parseFloat(TotalCoinsCredit).toFixed(2));
            $("#totalCoinsDebit").html(parseFloat(TotalCoinsDebit).toFixed(2));
            var totalTotalBalance = parseFloat(TotalCredit).toFixed(2) - parseFloat(TotalDebit).toFixed(2) - parseFloat(TotalServiceCharge).toFixed(2) - parseFloat(DiscountCoupon).toFixed(2);
            $("#totalTotalBalance").html(" Rs." + parseFloat(totalTotalBalance).toFixed(2));
            $("#totalCouponDiscount").html(" Rs." + parseFloat(DiscountCoupon).toFixed(2))

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

$('[id*=btnsearch]').on('click', function () {
    table.draw();
});

function toTitleCase(str) {
    return str.replace(/(?:^|\s)\w/g, function (match) {
        return match.toUpperCase();
    });
}