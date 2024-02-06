

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

const { data } = require("jquery");

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
                            var jsonString = JSON.stringify(data.AdditionalInfo1);
                            var AdditionalInfo = jsonString.replace(/'/g, "\\'").replace(/"/g, '&quot;');
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.CustomerID + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.SenderBankName + '&apos;,&apos;' + data.SenderAccountNo + '&apos;,&apos;' + data.RecieverBankName + '&apos;,&apos;' + data.RecieverAccountNo + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.CashBack + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + AdditionalInfo + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';

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
                                if (data.VendorType == "1") {
                                    return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnStatusCheck(&apos;' + data.TransactionUniqueId + '&apos;)" title="" data-original-title="Status"><em class="icon ni ni-activity-alt"></em></a>';
                                }
                                else if (data.VendorType == "10" && data.Status == 2) {
                                    return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetNepalPayQRStatus(&apos;' + data.TransactionUniqueId + '&apos;)" title="" data-original-title="Status"><em class="icon ni ni-activity-alt"></em></a>';
                                }
                                else {
                                    return '';
                                }
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (hdnrole == "8") {
                                return '';
                            }
                            else {
                                if (data.Status == 2) {
                                    return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return ChangeTxnStatus(&apos;' + data.TransactionUniqueId + '&apos;)" title="" data-original-title="Change Status"><em class="icon ni ni-pen2"></em></a>';
                                }
                                else {
                                    return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled" title="Change Status" style="display:none" data-original-title="Change Status"><em class="icon ni ni-pen2"></em></a>';
                                }
                            }
                        },
                        bSortable: false,
                        sTitle: "Change Status"
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

function BindLoadCardRequestDataTable() {
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
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Reference = $("#Reference").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetLoadCardRequestTransactionLists",
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
                        data.Reference = Reference;
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
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
                    //{ "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": true },
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
                        bSortable: true,
                        sTitle: "Sign"
                    },
                    { "data": "CardNumber", "name": "Card Number", "autoWidth": true, "bSortable": false },
                    { "data": "CardType", "name": "Card Type", "autoWidth": true, "bSortable": false },
                    { "data": "ExpiryDate", "name": "Expiry Date", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Source Type", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "VendorTypeName", "name": "Gateway Name", "autoWidth": true, "bSortable": false },
                    { "data": "GatewayStatus", "name": "Gateway Status", "autoWidth": true, "bSortable": false },
                    //{ "data": "StatusName", "name": "MyPay Status", "autoWidth": true, "bSortable": false },
                    //{ "data": "UpdateByName", "name": "UpdateBy", "autoWidth": true }
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
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + '&apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.TransactionUniqueId + ' &apos;,&apos; ' + data.VendorTransactionId + ' &apos;,&apos; ' + data.Reference + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.ContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SignName + ' &apos;,&apos; ' + data.CardNumber + ' &apos;,&apos; ' + data.CardType + ' &apos;,&apos; ' + data.ExpiryDate + ' &apos;,&apos; ' + data.TypeName + ' &apos;,&apos; ' + data.ServiceCharge + ' &apos;,&apos; ' + data.GatewayStatus + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindP2PTransferDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var ContactNumber = $("#ContactNumber").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#status :selected").val();
            var ReceiverContactNumber = $("#ReceiverContactNumber").val();
            var ReceiverName = $("#ReceiverName").val();
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Reference = $("#Reference").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetP2PTransactionLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = ContactNumber;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Status = Status;
                        data.ReceiverContactNumber = ReceiverContactNumber;
                        data.ReceiverName = ReceiverName;
                        data.Today = DayWise;
                        data.Sign = Sign;
                        data.Reference = Reference;
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
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.RecieverName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Receiver Name"
                    },
                    { "data": "RecieverContactNumber", "name": "Receiver Wallet Number", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Sender Name"
                    },
                    { "data": "ContactNumber", "name": "Sender Wallet Number", "autoWidth": true, "bSortable": false },

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
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + '&apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.TransactionUniqueId + ' &apos;,&apos; ' + data.VendorTransactionId + ' &apos;,&apos; ' + data.Reference + ' &apos;,&apos; ' + data.RecieverName + ' &apos;,&apos; ' + data.RecieverContactNumber + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.ContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SignName + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    }
                    // { "data": "UpdateByName", "name": "UpdateBy", "autoWidth": true }
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

function BindLoadIPSRequestDataTable() {
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
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Reference = $("#Reference").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetLoadIPSRequestTransactionLists",
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
                        data.Reference = Reference;
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
                        bSortable: false,
                        sTitle: "Gateway Txn Id"
                    },
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
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
                        bSortable: true,
                        sTitle: "Sign"
                    },
                    { "data": "TypeName", "name": "Source Type", "autoWidth": true, "bSortable": false },
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
                        bSortable: true,
                        sTitle: "My Pay Status"
                    },
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + '&apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.TransactionUniqueId + ' &apos;,&apos; ' + data.VendorTransactionId + ' &apos;,&apos; ' + data.Reference + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.ContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SignName + ' &apos;,&apos; ' + data.TypeName + ' &apos;,&apos; ' + data.GatewayStatus + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    }
                    //{ "data": "UpdateByName", "name": "UpdateBy", "autoWidth": true }
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

function BindCashbackDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var ContactNumber = $("#ContactNumber").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#status :selected").val();
            var Service = $("#servicelist :selected").val();
            var DayWise = $("#DayWise :selected").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetCashbackTransactionLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = ContactNumber;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Status = Status;
                        data.Type = Service;
                        data.Today = DayWise;
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
                            if (data.Type == "20" && data.VendorType == "1") {
                                return '<a href="/AdminTransactions?TransactionId=' + data.ParentTransactionId + '" style="color: #f98c45"> <span><strong>#' + data.TransactionUniqueId + '</strong></span></a>';
                            }
                            else {
                                return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                            }
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
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
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos; ' + data.Status + ' &apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + '&apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.TransactionUniqueId + ' &apos;,&apos; ' + data.VendorTransactionId + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.ContactNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SignName + ' &apos;,&apos; ' + data.TypeName + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos; ' + data.CurrentBalance + ' &apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindAllServiceDataTable() {
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
            var Status = $("#status :selected").val();
            var Service = $("#servicelist :selected").val();
            var VendorTypes = $("#VendorTypes :selected").val();
            var ServiceMultiple = $("#selectedProvider").val();
            var DayWise = $("#DayWise :selected").val();
            var Reference = $("#Reference").val();
            var FilterTotalCount = 0;
            var TotalCredit = 0;
            var TotalDebit = 0;
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetAllServiceTransactionLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.Type = Service;
                        data.TypeMultiple = ServiceMultiple;
                        data.ContactNumber = ContactNumber;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.GatewayTransactionId = GatewayTransactionId;
                        data.Status = Status;
                        data.Today = DayWise;
                        data.Reference = Reference;
                        data.VendorTypes = VendorTypes;
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
                    { "data": "VendorTransactionId", "name": "Gateway TxnId", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span>' + data.Reference + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Tracker Id"
                    },
                    { "data": "CustomerID", "name": "Subscriber", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.Amount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Amount (Rs)"
                    },
                    { "data": "CashBack", "name": "Cashback (Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "RewardPoint", "name": "MPCoins Credit", "autoWidth": true, "bSortable": false },
                    { "data": "MPCoinsDebit", "name": "MPCoins Debit", "autoWidth": true, "bSortable": false },
                    { "data": "RewardPointBalance", "name": "MPCoins Balance", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousRewardPointBalance", "name": "Prev MPCoins Balance", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge (Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "CouponDiscount", "name": "Discount Coupon (Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Service", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                                TotalCredit = data.TotalCredit;
                                TotalDebit = data.TotalDebit;
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
                        sTitle: "Txn Status"
                    },
                    { "data": "VendorTypeName", "name": "Gateway Name", "autoWidth": true, "bSortable": false },
                    { "data": "GatewayStatus", "name": "Gateway Status", "autoWidth": true, "bSortable": false },
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "Platform", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.CustomerID + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Reference + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            $("#totalCoinsCredit").html(parseFloat(TotalCoinsCredit).toFixed(2));
            $("#totalCoinsDebit").html(parseFloat(TotalCoinsDebit).toFixed(2));
            $("#totalCouponDiscount").html(" Rs." + parseFloat(DiscountCoupon).toFixed(2))
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function BindBankTransferDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var GatewayTransactionId = $("#GatewayTransactionId").val();
            var Status = $("#status :selected").val();
            var ContactNumber = $("#ContactNumber").val();
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Reference = $("#Reference").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetBankTransferLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.GatewayTransactionId = GatewayTransactionId;
                        data.Status = Status;
                        data.ContactNumber = ContactNumber;
                        data.Today = DayWise;
                        data.Sign = Sign;
                        data.Reference = Reference;
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
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    { "data": "TxnInstructionId", "name": "Instruction Id", "autoWidth": true, "bSortable": false },
                    { "data": "BatchTransactionId", "name": "Batch Id", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverAccountNo", "name": "Receiver AccountNo", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.RecieverName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Receiver Name"
                    },
                    { "data": "RecieverBankCode", "name": "Receiver Bank Code", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.RecieverBankName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Receiver Bank Name"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Sender Name"
                    },
                    { "data": "ContactNumber", "name": "Sender ContactNo", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.SenderBankName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Sender Bank Name"
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
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
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
                        bSortable: false,
                        sTitle: "My Pay Status"
                    },
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.Reference + '&apos;,&apos;' + data.TxnInstructionId + '&apos;,&apos;' + data.BatchTransactionId + '&apos;,&apos;' + data.RecieverAccountNo + '&apos;,&apos;' + data.RecieverName + '&apos;,&apos;' + data.RecieverBankCode + '&apos;,&apos;' + data.RecieverBankName + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.SenderBankName + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    }
                    //{ "data": "UpdateByName", "name": "UpdateBy", "autoWidth": true }
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

function BindVendorApiRequestDataTable() {
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
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Name = $("#Name").val();
            var Res_Khalti_Id = $("#Res_Khalti_Id").val();
            var TransactionUniqueId = $("#TransactionUniqueId").val();
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
                    "url": "/VendorApiRequestReport/GetVendorApiRequestLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Name = Name;
                        data.Res_Khalti_Id = Res_Khalti_Id;
                        data.TransactionUniqueId = TransactionUniqueId;
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
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    //{ "data": "TransactionUniqueId", "name": "TransactionId", "autoWidth": true, "bSortable": true },
                    {

                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"   onclick="return GetMyPayApiDetail(&apos;' + data.TransactionUniqueId + '&apos;)">' + data.TransactionUniqueId + '</a>';
                        },
                        bSortable: false,
                        sTitle: "TransactionId"

                    },
                    { "data": "Req_ReferenceNo", "name": "Reference Number", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  onclick="return GetVendorApiDetail(&apos;' + data.TransactionUniqueId + '&apos;)">' + (data.Res_Khalti_Id == "" ? " NA " : data.Res_Khalti_Id) + '</a>';
                        },
                        bSortable: false,
                        sTitle: "Vendor TxnId"

                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    //{ "data": "MemberId", "name": "MemberId", "autoWidth": true, "bSortable": true },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": true },
                    { "data": "TypeName", "name": "Vendor Type", "autoWidth": true, "bSortable": false },
                    { "data": "Res_Khalti_State", "name": "Vendor Status", "autoWidth": true, "bSortable": false },
                    { "data": "PlatForm", "name": "PlatForm", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.Res_Khalti_Id + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.Res_Khalti_State + '&apos;,&apos;' + data.PlatForm + '&apos;,&apos;' + data.DeviceCode + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Req_ReferenceNo + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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

function BindRewardPointsDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Sign = $("#Sign :selected").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/RewardPoints/GetRewardPointLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
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

                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
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
                    { "data": "MemberName", "name": "Name", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.Amount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Reward Points"
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
                    { "data": "TypeName", "name": "Reward Type", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.ParentTransactionId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Parent Txn Id"
                    },
                    { "data": "VendorServiceName", "name": "Service", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.TransactionUniqueId + ' &apos;,&apos; ' + data.VendorTransactionId + ' &apos;,&apos; ' + data.MemberName + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SignName + ' &apos;,&apos; ' + data.TypeName + ' &apos;,&apos; ' + data.Description + ' &apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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

function BindDepositOrdersDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var TransactionId = $("#TransactionId").val();
            var RefferalsId = $("#RefferalsId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#Status :selected").val();
            var Type = $("#Type :selected").val();
            var TypeMultiple = $("#selectedType").val();
            var DayWise = $("#DayWise :selected").val();
            var FilterTotalCount = 0;
            var TotalSuccess = 0;
            var TotalPending = 0;
            var TotalFailed = 0;
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/DepositOrder/GetDepositOrdersLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Status = Status;
                        data.Type = Type;
                        data.TypeMultiple = TypeMultiple;
                        data.Today = DayWise;
                        data.RefferalsId = RefferalsId;
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
                    //{ "data": "TransactionId", "name": "TransactionId", "autoWidth": true, "bSortable": true },

                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Status == 1) {
                                return '<a href="/AdminTransactions/Index?Reference=' + data.TransactionId + '" style="color: #f98c45"><strong>#' + data.TransactionId + '</strong></a>';
                            }
                            else if (data.Status == 6) {
                                return '<a href="/AdminTransactions?ParentTransactionId=' + data.TransactionId + '"> <span><strong>#' + data.TransactionId + '</strong></span></a>';
                            }
                            else {
                                return '<span><strong style="color: #f98c45">#' + data.TransactionId + '</strong></span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Tracker Id"
                    },
                    { "data": "CreatedByName", "name": "CreatedByName", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/AdminTransactions/Index?MemberId=' + data.MemberId + '" class="tb-lead"><strong>' + data.MemberId + '</strong></a>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    //{ "data": "MemberId", "name": "MemberId", "autoWidth": true, "bSortable": true },
                    //{ "data": "StatusName", "name": "Status", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                debugger;
                                FilterTotalCount = data.FilterTotalCount;
                                TotalSuccess = data.TotalSuccess;
                                TotalPending = data.TotalPending;
                                TotalFailed = data.TotalFailed;
                            }

                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 4) {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-sub tb-amount">' + data.Amount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Amount (Rs)"
                    },
                    //{ "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "Particulars", "name": "Particulars", "autoWidth": true, "bSortable": false },
                    //{ "data": "RefferalsId", "name": "RefferalsId", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Status == 1) {
                                return '<a href="/AdminTransactions?GatewayTransactionId=' + data.RefferalsId + '">' + data.RefferalsId + '</a>';
                            }
                            else {
                                return '<span class="tb-lead">' + data.RefferalsId + '</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "RefferalsId"
                    },
                    { "data": "ResponseCode", "name": "Response Code", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.GatewayType == "1") {
                                return '<span class="tb-status text-success">' + data.GatewayName + '</span>';
                            }
                            else if (data.GatewayType == "2") {
                                return '<span class="tb-status text-danger">' + data.GatewayName + '</span>';
                            }
                            else if (data.GatewayType == "3") {
                                return '<span class="tb-status text-purple">' + data.GatewayName + '</span>';
                            }
                            else if (data.GatewayType == "4") {
                                return '<span class="tb-status text-orange">' + data.GatewayName + '</span>';
                            }
                            else if (data.GatewayType == "5") {
                                return '<span class="tb-status text-pink">' + data.GatewayName + '</span>';
                            }
                            else if (data.GatewayType == "6") {
                                return '<span class="tb-status text-warning">' + data.GatewayName + '</span>';
                            }
                            else if (data.GatewayType == "7") {
                                return '<span class="tb-status text-success">' + data.GatewayName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.GatewayName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Gateway"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.TypeName == "Card") {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled"  onclick="return GetDepositOrderDetail(&apos;' + data.TransactionId + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';

                            }
                            else {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetDepositOrderDetail(&apos;' + data.TransactionId + '&apos;,event)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                            }
                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var IsDisabled = "";
                            if (data.TypeName == "ConnectIPS") {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip ' + IsDisabled + ' "  onclick="return GetConnectIpsStatus(&apos;' + data.TransactionId + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-activity"></em></a>';
                            }
                            else if (data.TypeName == "Mobile Banking" || data.TypeName == "Internet Banking") {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip ' + IsDisabled + ' "  onclick="return GetBankingStatus(&apos;' + data.TransactionId + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-activity"></em></a>';
                            }
                            else if ((data.Type == "6") || (data.Type == "8")) {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip ' + IsDisabled + ' "  onclick="return GetLinkBankStatus(&apos;' + data.TransactionId + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-activity"></em></a>';
                            }
                            else if (data.TypeName == "Bank Transfer") {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip ' + IsDisabled + ' "  onclick="return GetBankTransferDepositOrderTxnStatus(&apos;' + data.TransactionId + '&apos;,&apos;' + data.MemberId + '&apos;,event)" title="" data-original-title="View"><em class="icon ni ni-activity"></em></a>';

                            }
                            else {
                                return '';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
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
            $("#totaltra").html(FilterTotalCount);
            $("#totalsuccess").html(" Rs." + parseFloat(TotalSuccess).toFixed(2));
            $("#totalpending").html(" Rs." + parseFloat(TotalPending).toFixed(2));
            $("#totalfailed").html(" Rs." + parseFloat(TotalFailed).toFixed(2));
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

function BindRequestFundDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var SenderMemberId = $("#SenderMemberId").val();
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var FilterTotalCount = 0;
            var TotalAccepted = 0;
            var TotalPending = 0;
            var TotalRejected = 0;
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/RequestFundReport/GetRequestFundLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.SenderMemberId = SenderMemberId;
                        data.Status = Status;
                        data.Today = DayWise;
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
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Created Date", "autoWidth": true, "bSortable": true },
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
                            return '<span class="tb-lead">' + data.ReceiverMemberName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Receiver Name"
                    },
                    { "data": "ReceiverPhoneNumber", "name": "Receiver ContactNo", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.Amount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Amount (Rs)"
                    },
                    { "data": "SenderMemberId", "name": "Sender MemberId", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.SenderMemberName + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Sender Name"
                    },
                    { "data": "SenderPhoneNumber", "name": "Sender ContactNo", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (FilterTotalCount == 0) {
                                FilterTotalCount = data.FilterTotalCount;
                                TotalAccepted = data.TotalAccepted;
                                TotalPending = data.TotalPending;
                                TotalRejected = data.TotalRejected;
                            }

                            /*$("#totalrejected").html(parseFloat(data.TotalRejected).toFixed(2) + " Rs.");*/
                            if (data.RequestStatus == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.RequestStatus == 0) {
                                return '<span class="tb-status text-orange">' + data.StatusName + '</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip" onclick = "return GetTxnDetail(&apos; ' + data.Status + ' &apos;,&apos; ' + data.CreatedDatedt + '&apos;,&apos; ' + data.UpdatedDatedt + ' &apos;,&apos; ' + data.MemberId + ' &apos;,&apos; ' + data.ReceiverMemberName + ' &apos;,&apos; ' + data.ReceiverPhoneNumber + ' &apos;,&apos; ' + data.Amount + ' &apos;,&apos; ' + data.SenderMemberId + ' &apos;,&apos; ' + data.SenderMemberName + ' &apos;,&apos; ' + data.SenderPhoneNumber + ' &apos;,&apos; ' + data.StatusName + ' &apos;,&apos;' + data.IpAddress + '&apos;) " title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            $("#totaltra").html(FilterTotalCount);
            $("#totalaccepted").html(" Rs." + parseFloat(TotalAccepted).toFixed(2));
            $("#totalpending").html(" Rs." + parseFloat(TotalPending).toFixed(2));
            $("#totalrejected").html(" Rs." + parseFloat(TotalRejected).toFixed(2));
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function GetTxnStatusCheck(TransactionUniqueId) {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#singleTxn").html("");
            var JsonOutput = "";
            var jsonPretty = "";
            $.ajax({
                type: "POST",
                url: "/VendorApiRequestReport/GetVendorTransactionStatus",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"TransactionUniqueId":"' + TransactionUniqueId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        debugger;
                        jsonPretty = JSON.stringify(response.message, null, '\t');
                        JsonOutput = response;
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                }
            });
            $('#AjaxLoader').hide();
            var str = '<div class="table-responsive"><table class="nowrap nk-tb-list nk-tb-ulist table-bordered h-100">';
            str += '<tr class="nk-tb-item"><td colspan="2" class="nk-tb-col"><span class="tb-lead"><b>' + jsonPretty + '</b></span></td></tr>';
            if (JsonOutput.jsonData != undefined && JsonOutput.jsonData != "") {
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Amount :</span></td><td class="nk-tb-col"><span>' + JsonOutput.amount.toFixed(2) + '</span></td></tr>';
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Details :</span></td><td class="nk-tb-col"><span>' + JsonOutput.details + '</span></td></tr>';
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>State :</span></td><td class="nk-tb-col"><span>' + JsonOutput.state + '</span></td></tr>';
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Tracker Id :</span></td><td class="nk-tb-col"><span>' + JsonOutput.reference + '</span></td></tr>';
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Message :</span></td><td class="nk-tb-col"><span>' + JsonOutput.message + '</span></td></tr>';
                str += '</table></div>';
                str += '<span class="tb-lead"><b>Vendor JSON Output</b></span><br>';


                var jsonObj = JSON.parse(JsonOutput.jsonData);
                var jsonPretty = JSON.stringify(jsonObj, null, '\t');
                str += '<span class="tb-lead"> ' + jsonPretty + ' </span>';
            }
            $("#singleTxn").append(str);
            $('#singleTransaction').modal('show');
            return false;
        }, 100);
}
function GetNepalPayQRStatus(TransactionUniqueId) {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#singleTxn").html("");
            var JsonOutput = "";
            var jsonPretty = "";
            var nQrTxnId = "";
            var instructionId = "";            
            var amt = "";

            $.ajax({
                type: "POST",
                url: "/VendorApiRequestReport/GetNepalPayQRStatus",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"TransactionUniqueId":"' + TransactionUniqueId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        debugger;
                        jsonPretty = JSON.stringify(response.message, null, '\t');
                        JsonOutput = response.result;
                        nQrTxnId = response.nQrTxnId;
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                    }

                    instructionId = response.instructionId;
                    amt = response.amt;
                },
                failure: function (response) {
                    JsonOutput = (response.result);
                },
                error: function (response) {
                    JsonOutput = (response.result);
                }
            });
            $('#AjaxLoader').hide();
            
            var str = '<div class="table-responsive"><table class="nowrap nk-tb-list nk-tb-ulist table-bordered h-100">';
            str += '<tr class="nk-tb-item"><td colspan="2" class="nk-tb-col"><span class="tb-lead"><b>' + jsonPretty + '</b></span></td></tr>';
           
            /*    if (JsonOutput.jsonData != undefined && JsonOutput.jsonData != "") {*/
            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Instruction Id:</span></td><td class="nk-tb-col"><span>' + instructionId + '</span></td></tr>';
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Amount :</span></td><td class="nk-tb-col"><span>' + amt + '</span></td></tr>';
            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Details :</span></td><td class="nk-tb-col"><span>' + JsonOutput + '</span></td></tr>';
            if (jsonPretty == "success") {
                str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>QR Txn Id :</span></td><td class="nk-tb-col"><span>' + nQrTxnId + '</span></td></tr>';
               
            }
            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Message :</span></td><td class="nk-tb-col"><span>' + jsonPretty + '</span></td></tr>';
                str += '</table></div>';
                

            if (jsonPretty == "success") {
                str += '<span class="tb-lead"><b>Vendor JSON Output</b></span><br>';
                var jsonObj = JSON.parse(JsonOutput);
                var jsonPretty = JSON.stringify(jsonObj, null, '\t');
                str += '<span class="tb-lead"> ' + jsonPretty + ' </span>';
            }
               
      /*      }*/
            $("#singleTxn").append(str);
            $('#singleTransaction').modal('show');
            return false;
        }, 100);
}

function BindInternetBankingDataTable() {
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
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Reference = $("#Reference").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetInternetBankingLists",
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
                        data.Reference = Reference;
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
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact No", "autoWidth": true, "bSortable": false },
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
                    { "data": "SenderBankName", "name": "Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Service", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashBack", "name": "Cashback", "autoWidth": true, "bSortable": false },
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
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.Reference + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.SenderBankName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.CashBack + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );

            $('#AjaxLoader').hide();
        }, 100);
}


function BindReferAndEarnDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var RefCode = $("#RefCode").val();
            var ReferTo = $("#ReferTo").val();
            var Referby = $("#Referby").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var ParentTransactionId = $("#ParentTransactionId").val();
            //var Sign = $("#Sign :selected").val();
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
                "order": [[8, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetReferAndEarnLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.RefCode = RefCode;
                        data.RecieverName = ReferTo;
                        data.MemberName = Referby;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Status = Status;
                        data.Today = DayWise;
                        data.ParentTransactionId = ParentTransactionId;
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span><strong style="color: #f98c45">#' + data.TransactionUniqueId + '</strong></span>';
                        },
                        bSortable: true,
                        sTitle: "Transaction Id"
                    },
                    { "data": "ParentTransactionId", "name": "Parent Txn Id", "autoWidth": true, "bSortable": false },
                    { "data": "RefCode", "name": "Refer code", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Referred by Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNumber", "name": "Referred by ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Referree earning cash", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverName", "name": "Referred to Name", "autoWidth": true, "bSortable": false },
                    { "data": "RecieverContactNumber", "name": "Referred to ContactNo", "autoWidth": true, "bSortable": false },
                    { "data": "ReceiverAmount", "name": "Referal earning cash", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDatedt", "name": "Refer and earn initiated date", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Cashback applied date", "autoWidth": true, "bSortable": true },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    { "data": "DeviceCode", "name": "Device Id", "autoWidth": true, "bSortable": false }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });

            //$("#totaltra").html(FilterTotalCount);
            //$("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            //$("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );

            $('#AjaxLoader').hide();
        }, 100);
}

function BindMobileBankingDataTable() {
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
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
            var Sign = $("#Sign :selected").val();
            var Reference = $("#Reference").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetMobileBankingLists",
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
                        data.Reference = Reference;
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
                    { "data": "Reference", "name": "Tracker Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact No", "autoWidth": true, "bSortable": false },
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
                    { "data": "SenderBankName", "name": "Bank Name", "autoWidth": true, "bSortable": false },
                    { "data": "TypeName", "name": "Service", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashBack", "name": "Cashback", "autoWidth": true, "bSortable": false },
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
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.Reference + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.SenderBankName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.CashBack + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}


function GetBankTransferDepositOrderTxnStatus(TransactionId, MemberId, e) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#singleTxn").html("");
            var JsonOutput = "";
            var jsonPretty = "";
            $.ajax({
                type: "POST",
                url: "/BankTransactions/GetBankTransferDepositOrderTxnStatusCheck",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"TransactionId":"' + TransactionId + '","MemberId":"' + MemberId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        debugger;
                        jsonPretty = JSON.stringify(response, null, '\t');
                        JsonOutput = response;
                        table.draw();
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                }
            });
            $('#AjaxLoader').hide();
            var str = '';
            //str += '<div class="table-responsive">';
            //str += '<table class="nowrap nk-tb-list nk-tb-ulist table-bordered h-100">';
            //str += '<tr class="nk-tb-item"><td colspan="2" class="nk-tb-col"><span class="tb-lead"><b>' + jsonPretty + '</b></span></td></tr>';
            //if (JsonOutput.jsonData != undefined && JsonOutput.jsonData != "") {
            //    str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Amount :</span></td><td class="nk-tb-col"><span>' + JsonOutput.amount.toFixed(2) + '</span></td></tr>';
            //    str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Details :</span></td><td class="nk-tb-col"><span>' + JsonOutput.details + '</span></td></tr>';
            //    str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>State :</span></td><td class="nk-tb-col"><span>' + JsonOutput.state + '</span></td></tr>';
            //    str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Reference :</span></td><td class="nk-tb-col"><span>' + JsonOutput.reference + '</span></td></tr>';
            //    str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Message :</span></td><td class="nk-tb-col"><span>' + JsonOutput.message + '</span></td></tr>';
            //    str += '</table>';
            //    str += '<span class="tb-lead"><b>' + jsonPretty + '</b></span>';
            //    str += '</div > ';
            //    str += '<span class="tb-lead"><b>JSON Output</b></span><br>';
            //}
            //str += '<span class="tb-lead"> ' + JsonOutput.jsonData + ' </span>';
            $("#Resapijson").text(jsonPretty);
            $('#depositapijson').modal('show');
            return false;
        });
}

function GetConnectIpsStatus(TransactionId) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#singleTxn").html("");
            var JsonOutput = "";
            var jsonPretty = "";
            $.ajax({
                type: "POST",
                url: "/BankTransactions/GetConnectIpsStatus",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"TransactionId":"' + TransactionId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        debugger;
                        jsonPretty = JSON.stringify(response, null, '\t');
                        JsonOutput = response;
                        table.draw();
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                }
            });
            $('#AjaxLoader').hide();
            var str = '';
            $("#Resapijson").text(jsonPretty);
            $('#depositapijson').modal('show');
            return false;
        });
}

function GetBankingStatus(TransactionId) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#singleTxn").html("");
            var JsonOutput = "";
            var jsonPretty = "";
            $.ajax({
                type: "POST",
                url: "/BankTransactions/GetBankingStatus",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"TransactionId":"' + TransactionId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        debugger;
                        jsonPretty = JSON.stringify(response, null, '\t');
                        JsonOutput = response;
                        table.draw();
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                }
            });
            $('#AjaxLoader').hide();
            var str = '';
            $("#Resapijson").text(jsonPretty);
            $('#depositapijson').modal('show');
            return false;
        });
}


function GetLinkBankStatus(TransactionId) {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#singleTxn").html("");
            var JsonOutput = "";
            var jsonPretty = "";
            $.ajax({
                type: "POST",
                url: "/BankTransactions/GetLinkBankStatus",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"TransactionId":"' + TransactionId + '"}',
                success: function (response) {
                    debugger;
                    if (response != null) {
                        debugger;
                        jsonPretty = JSON.stringify(response, null, '\t');
                        JsonOutput = response;
                        table.draw();
                    }
                    else {
                        JsonOutput = "Something went wrong. Please try again later.";
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                }
            });
            $('#AjaxLoader').hide();
            var str = '';
            $("#Resapijson").text(jsonPretty);
            $('#depositapijson').modal('show');
            return false;
        });
}

function BindAdminLoadedWalletDataTable() {
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
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetAdminLoadedWalletLists",
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact No", "autoWidth": true, "bSortable": false },
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
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashBack", "name": "Cashback", "autoWidth": true, "bSortable": false },
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
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.CashBack + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);

}


function BindAdminHoldWalletDataTable() {
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
            var Status = $("#status :selected").val();
            var DayWise = $("#DayWise :selected").val();
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
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AdminTransactions/GetAdminHoldWalletReportLists",
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberName + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Name"
                    },
                    { "data": "ContactNumber", "name": "Contact No", "autoWidth": true, "bSortable": false },
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
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "CashBack", "name": "Cashback", "autoWidth": true, "bSortable": false },
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
                        bSortable: false,
                        sTitle: "My Pay Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.ResponseCode == "1") {
                                return '<span class="tb-status text-success">Released</span>';
                            }
                            else {
                                return '<span class="tb-status text-orange">Holded</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Released Status"
                    },
                    { "data": "UpdateByName", "name": "Update By", "autoWidth": true, "bSortable": false },
                    { "data": "CurrentBalance", "name": "Available Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "PreviousBalance", "name": "Previous Balance(Rs)", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var strReturn = '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.Sign + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.UpdatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.MemberName + '&apos;,&apos;' + data.ContactNumber + '&apos;,&apos;' + data.Amount + '&apos;,&apos;' + data.SignName + '&apos;,&apos;' + data.TypeName + '&apos;,&apos;' + data.ServiceCharge + '&apos;,&apos;' + data.CashBack + '&apos;,&apos;' + data.VendorTypeName + '&apos;,&apos;' + data.GatewayStatus + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.CurrentBalance + '&apos;,&apos;' + data.PreviousBalance + '&apos;,&apos;' + data.IpAddress + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a> ';
                            return strReturn;
                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var strReturn = '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return ReleaseAmount(&apos;' + data.MemberId + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.Type + '&apos;)" title="" data-original-title="Release"><em class="icon ni ni-check-circle"></em></a>';
                            return strReturn;
                        },
                        bSortable: false,
                        sTitle: "Release"
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
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);

}

function ReleaseAmount(MemberId, TxnId, Type) {
    if (confirm("Do you really want to release the amount ?") == true) {
        if (MemberId != "" && TxnId != "" && Type != "") {
            $.ajax({
                type: "POST",
                url: "/AdminTransactions/AmountReleaseByAdmin",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"MemberId":"' + MemberId + '","TxnId":"' + TxnId + '","Type":"' + Type + '"}',
                success: function (response) {
                    if (response == "success") {
                        $("#dvMsgSuccess").html("successfully Release Amount");
                        BindAdminHoldWalletDataTable();
                    }
                    else {
                        $("#dvMsg").html(response);
                        return false;
                    }
                },
                failure: function (response) {
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    JsonOutput = (response.responseText);
                }
            });
        }
        else {
            alert("Please enter memberid,Transactionid and type.")
        }
    }
    else {
        return false;
    }
}
function toTitleCase(str) {
    return str.replace(/(?:^|\s)\w/g, function (match) {
        return match.toUpperCase();
    });
}