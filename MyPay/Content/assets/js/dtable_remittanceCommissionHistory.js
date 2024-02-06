///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////
///////////////////////////////////////////////////

var table;

function BindRemittanceCommissionUpdateHistoryDataTable () {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var SourceCurrencyId = $("#drpSourceCurrency").val();
            var DestinationCurrencyId = $("#drpDestinationCurrency").val();
            var Status = $("#Status").val();
            var ScheduleStatus = $("#ScheduleStatus").val();
            

            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No History"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/RemittanceCommission/GetRemittanceCommissionUpdateHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.SourceCurrencyId = SourceCurrencyId;
                        data.DestinationCurrencyId = DestinationCurrencyId;
                        data.Status = Status;
                        data.ScheduleStatus = ScheduleStatus;
                        

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
                    { "data": "Sno", "name": "Sno", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "SourceCurrencyName", "name": "Source Currency", "autoWidth": true, "bSortable": true },
                    { "data": "DestinationCurrencyName", "name": "Destination Currency", "autoWidth": true, "bSortable": true },
                    { "data": "MinimumAmount", "name": "Minimum Amount", "autoWidth": true, "bSortable": false },
                    { "data": "MaximumAmount", "name": "Maximum Amount", "autoWidth": true, "bSortable": false },
                    { "data": "MinimumAllowedSC", "name": "Min Allowed SC", "autoWidth": true, "bSortable": false },
                    { "data": "MaximumAllowedSC", "name": "Max Allowed SC", "autoWidth": true, "bSortable": false },
                    { "data": "ServiceCharge", "name": "Service Charge", "autoWidth": true, "bSortable": false },
                    { "data": "FromDateDT", "name": "From Date", "autoWidth": true, "bSortable": false },
                    { "data": "ToDateDT", "name": "To Date", "autoWidth": true, "bSortable": false },
                    { "data": "IPAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    { "data": "StatusName", "name": "Status", "autoWidth": true, "bSortable": false },
                    
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            // document.getElementById("tblmerlist").deleteTFoot();

            //$("#tblmerlist").append(
            //    $('<tfoot/>').append($("#tblmerlist thead tr").clone())
            //);
            $('#AjaxLoader').hide();
        }, 100);
}




