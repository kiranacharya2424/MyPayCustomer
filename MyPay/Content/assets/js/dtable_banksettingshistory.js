///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindBankSettingsHistoryDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var BankTransferType = $("#BankTransferType :selected").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();


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
                    "url": "/BankSettings/GetBankSettingsHistory",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.BankTransferType = BankTransferType;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.BankTransferType == 1) {
                                return '<span>NCHL</span>';
                            }
                            else if (data.BankTransferType == 2) {
                                return '<span>NPS</span>';
                            }
                            else {
                                return '<span>--</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Bank Transfer Type"
                    },
                    { "data": "UpdatedByName", "name": "UpdatedBy Name", "autoWidth": true, "bSortable": false }                    
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