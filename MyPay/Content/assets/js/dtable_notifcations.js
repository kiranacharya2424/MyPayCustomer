

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;


function BindNotificationDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var StartDate = $("#fromdate").val();
            var EndDate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Notification"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/Notification/GetNotificationLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.StartDate = StartDate;
                        data.EndDate = EndDate;
                        //data.SenderMemberId = SenderMemberId;
                        //data.Status = Status;
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
                    { "data": "CreatedDatedt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDatedt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    { "data": "MemberId", "name": "Receiver MemberId", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": false, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.SentStatusName == "Sent") {
                                return "<span class='tb-status text-success'>Sent</span>";
                            }
                            else {
                                return "<span class='tb-status text-danger'>Pending</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "Sent Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.ReadStatusName == "Read") {
                                return "<span class='tb-status text-success'>Read</span>";
                            }
                            else {
                                return "<span class='tb-status text-danger'>UnRead</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "Read Status"
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

