var table;
function BindAppVersionHistoryDataTable() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tbllist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Data Found"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/AppVersionUpdate/GetHistoryList",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
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
                    { "data": "Sno", "name": "SNO", "autoWidth": true, "bSortable": false },
                    { "data": "IndiaDate", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "IOS", "name": "IOS", "autoWidth": true, "bSortable": false },
                    { "data": "Android", "name": "Android", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false }

                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tbllist").deleteTFoot();

            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}