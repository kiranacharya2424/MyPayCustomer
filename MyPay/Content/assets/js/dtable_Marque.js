///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindMarqueDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Marques Added"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/Marque/GetMarqueLists",
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
                    { "data": "Sno", "name": "SNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true, "bSortable": true },
                    { "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    { "data": "Description", "name": "Amount", "autoWidth": true, "bSortable": true },
                    { "data": "Link", "name": "Points", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedByName", "name": "Updated By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/Marque/Index?Id=' + data.Id + '"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Edit"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<button onclick="return BlockUnblock(\'' + data.Id + '\', \'true\', event);" class="btn btn-success btn-sm btn-icon" style="color:white;"  Title="UnPublish"><em class="icon ni ni-check-circle"></em></button>';
                            }
                            else {
                                return '<button onclick="return BlockUnblock(\'' + data.Id + '\', \'false\', event);" class="btn btn-danger btn-sm btn-icon" style="color:white;" Title="Publish"><em class="icon ni ni-na"></em></button>';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
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
    debugger;
    $("#EnumMarqueFor").val($("#MarqueFor").val());
    $("#btnSearch").click(function () {

    });
});

function BlockUnblock(id, activestatus, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var MarqueData = new Object();
    MarqueData.Id = id;
    MarqueData.IsActive = activestatus;
    $.ajax({
        type: "POST",
        url: "/Marque/MarqueBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify(MarqueData),
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    e.preventDefault();
                    e.stopPropagation();
                    return false;
                }
                else {
                    debugger;
                    if (activestatus == "true") {
                        $("#dvMsgSuccess").html("Successfully Published");
                    }
                    else {
                        $("#dvMsgSuccess").html("Successfully UnPublished");
                    }
                    BindMarqueDataTable();
                    e.preventDefault();
                    e.stopPropagation();
                    debugger;
                    return false;
                }
            }
            else {
                $("#dvMsg").html("Something went wrong. Please try again later.");
                e.preventDefault();
                e.stopPropagation();
                return false;
            }
        },
        failure: function (response) {
            $("#dvMsg").html(response.responseText);
            e.preventDefault();
            e.stopPropagation();
            return false;
        },
        error: function (response) {
            $("#dvMsg").html(response.responseText);
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
    });

}