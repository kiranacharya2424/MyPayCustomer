

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;


function BindExcelImportedDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No User Found"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/NotificationCampaignExcelImport/GetNotificationExcelDataLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.Id = $("#hdnid").val();
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
                    { "data": "ContactNumber", "name": "Contact Number", "autoWidth": true, "bSortable": false },
                    { "data": "MemberId", "name": "MemberId", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": false, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.SentStatus == "1") {
                                return "<span class='tb-status text-success'>"+data.SentStatusName+"</span>";
                            }
                            else {
                                return "<span class='tb-status text-danger'>" + data.SentStatusName +"</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "Sent Status"
                    },
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

