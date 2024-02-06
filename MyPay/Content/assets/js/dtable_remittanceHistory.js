///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////
///////////////////////////////////////////////////

var table;

function BindRemittanceHistoryDataTable() {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var SourceCurrencyName = $("#SourceCurrencyName").val();
            var DestinationCurrencyName = $("#DestinationCurrencyName").val();
            var BankConversionRateCode = $("#BankConversionRateCode").val();
            var InverseRate = $("#InverseRate").val();
            var Markup = $("#Markup").val();

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
                    "url": "/Remittance/GetCurrencyConversionHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.SourceCurrencyName = SourceCurrencyName;
                        data.DestinationCurrencyName = DestinationCurrencyName;
                        data.BankConversionRateCode = BankConversionRateCode;
                        data.InverseRate = InverseRate;
                        data.Markup = Markup;

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
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "SourceCurrencyName", "name": "Source Currency", "autoWidth": true, "bSortable": false },
                    { "data": "DestinationCurrencyName", "name": "Destination Currency", "autoWidth": true, "bSortable": false },
                    { "data": "ConversionRate", "name": "Conversion Rate", "autoWidth": true, "bSortable": false },
                    { "data": "InverseRate", "name": "Inverse Rate", "autoWidth": true, "bSortable": false },
                    { "data": "Markup", "name": "Markup", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
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




