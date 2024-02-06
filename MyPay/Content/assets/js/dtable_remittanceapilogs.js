
///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});
function BindRemittanceApiLogsDataTable() {
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
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Name = $("#Name").val();520
            var Res_Khalti_Id = $("#VendorTransactionId").val();
            var TransactionUniqueId = $("#TransactionUniqueId").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No API Logs"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/Remittance/GetRemittanceApiLogsLists",
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
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"   onclick="return GetMyPayApiDetail(&apos;' + data.TransactionUniqueId + '&apos;)">' + data.TransactionUniqueId + '</a>';
                        },
                        bSortable: false,
                        sTitle: "TransactionId"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);"  onclick="return GetVendorApiDetail(&apos;' + data.TransactionUniqueId + '&apos;)">' + data.VendorTransactionId + '</a>';
                        },
                        bSortable: false,
                        sTitle: "Vendor TxnId"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MerchantUniqueId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Merchant UniqueId"
                    },
                    { "data": "MerchantName", "name": "Merchant Name", "autoWidth": true, "bSortable": false },
                    { "data": "OrganizationName", "name": "Organization Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactNo", "name": "Contact No", "autoWidth": true, "bSortable": false },
                    { "data": "StatusName", "name": "Status", "autoWidth": true, "bSortable": false },
                    { "data": "PlatForm", "name": "PlatForm", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    { "data": "Req_ReferenceNo", "name": "Req Reference No", "autoWidth": true, "bSortable": false },
                    { "data": "RemittanceApiTypeName", "name": "API Type", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.CreatedByName + '&apos;,&apos;' + data.TransactionUniqueId + '&apos;,&apos;' + data.VendorTransactionId + '&apos;,&apos;' + data.MerchantUniqueId + '&apos;,&apos;' + data.MerchantName + '&apos;,&apos;' + data.OrganizationName + '&apos;,&apos;' + data.ContactNo + '&apos;,&apos;' + data.StatusName + '&apos;,&apos;' + data.Status + '&apos;,&apos;' + data.RemittanceApiTypeName + '&apos;,&apos;' + data.DeviceId + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.Req_ReferenceNo + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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