///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
var take = 50;
var skip = 0;
var sort = "";
var sortdir = "";
function BindDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var ContactNumber = $("#ContactNumber").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Status = $("#StatusEnum :selected").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Data"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/CashbackImport/GetGiftCashbackHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = ContactNumber;
                        data.MemberName = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.Status = Status;
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
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
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.ContactNumber + '</span>';
                            }
                            else if (data.Status == 2) {
                                return '<span class="tb-status text-danger">' + data.ContactNumber + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Contact No"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.TransactionId != "") {
                                return '<span><strong style="color: #f98c45">#' + data.TransactionId + '</strong></span>';
                            }
                            else {
                                return '<span>--</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Transaction Id"
                    },
                    { "data": "Prize", "name": "Prize", "autoWidth": true, "bSortable": false },
                    { "data": "MemberId", "name": "Member Id", "autoWidth": true, "bSortable": false },
                    { "data": "MemberName", "name": "Member Name", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Status == 1) {
                                return '<span class="tb-status text-success">' + data.StatusName + '</span>';
                            }
                            else if (data.Status == 2) {
                                return '<span class="tb-status text-danger">' + data.StatusName + '</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "CreatedByName", "name": "CreatedBy", "autoWidth": true, "bSortable": false },
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
