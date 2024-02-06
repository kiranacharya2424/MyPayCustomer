///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindRedeemPointsDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var Title = $("#Title").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();

            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Redeem Points"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/RedeemPoints/GetRedeemPointsLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.Title = Title;
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
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    { "data": "Amount", "name": "Amount", "autoWidth": true, "bSortable": true },
                    { "data": "Points", "name": "Points", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',event)" class="btn btn-success btn-sm btn-icon" style="color:white;"  Title="Enable"><em class="icon ni ni-check-circle"></em></a>';
                            }
                            else {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',event)" class="btn btn-danger btn-sm btn-icon" style="color:white;" Title="Disable"><em class="icon ni ni-na"></em></a>';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/RedeemPoints/Index?Id=' + data.Id + '"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Edit"
                    }
                ]
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

$(document).ready(function () {

    $("#btnSearch").click(function () {

    });
});

function BlockUnblock(id, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/RedeemPoints/RedeemPointsBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Id":"' + id + '"}',
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    debugger;
                    $("#dvMsgSuccess").html("successfully updated");
                    BindRedeemPointsDataTable();
                    e.preventDefault();
                    e.stopPropagation();
                    return false;
                }
            }
            else {
                $("#dvMsg").html("Something went wrong. Please try again later.");
                return false;
            }
        },
        failure: function (response) {
            $("#dvMsg").html(response.responseText);
            return false;
        },
        error: function (response) {
            $("#dvMsg").html(response.responseText);
            return false;
        }
    });

}