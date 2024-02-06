///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No KYC Remarks"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/KYCRemarks/GetKYCRemarksList",
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
                    //{ "data": "Title", "name": "Title", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" onclick = "return GetTxnDetail(&apos; ' + data.CreatedDateDt + '&apos;,&apos; ' + data.Title + ' &apos;,&apos; ' + data.Description + ' &apos;,&apos; ' + data.CreatedByName + ' &apos;) " >' + data.Title + '</a>';
                        },
                        bSortable: false,
                        sTitle: "Title"
                    },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;KYCRemarks&#47;KYCRemarksBlockUnblock&apos;,event)" class="btn btn-success btn-sm btn-icon" style="color:white;"  Title="Enable"><em class="icon ni ni-check-circle"></em></a>';
                            }
                            else {
                                return '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.Id + ',&apos;&#47;KYCRemarks&#47;KYCRemarksBlockUnblock&apos;,event)" class="btn btn-danger btn-sm btn-icon" style="color:white;" Title="Disable"><em class="icon ni ni-na"></em></a>';
                            }

                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/KYCRemarks/Index?Id=' + data.Id + '"  class="btn btn-primary btn-sm btn-icon"><em class="icon ni ni-edit"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Edit"
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

function BlockUnblock(id, url, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"Id":"' + id + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");
                    BindDataTable();
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