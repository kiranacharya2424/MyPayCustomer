///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindUserBankDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Bank Detail"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/UserBankDetail/GetUserBankLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = $("#MemberId").val();
                        data.Name = $("#Name").val();
                        data.BankCode = $("#BankCode").val();
                        data.BankName = $("#BankName").val();
                        data.BranchName = $("#BranchName").val();
                        data.AccountNumber = $("#AccountNumber").val();
                        data.Today = $("#DayWise :selected").val();
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
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "MemberId", "name": "Member Id", "autoWidth": true, "bSortable": true },
                    { "data": "Name", "name": "Name", "autoWidth": true, "bSortable": true },
                    { "data": "BankCode", "name": "Bank Code", "autoWidth": true, "bSortable": false },
                    { "data": "BankName", "name": "Bank Name", "autoWidth": true, "bSortable": true },
                    { "data": "BranchName", "name": "Token", "autoWidth": true, "bSortable": true },
                    { "data": "AccountNumber", "name": "Account Number", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsPrimary) {
                                return '<label style="color:green;">Primary</label>';
                            }
                            else {
                                return '<label style="color:red;">Not Primary</label>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Is Primary"
                    }
                    //{ "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
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