
var table;

function BindServiceDataTable() {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var ProviderId = $("#drpProvider").val();
    if (ProviderId == "" || ProviderId == "0") {
        alert("Please select Category");
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                table = $('#tbllist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Service"
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "order": [[0, "desc"]],
                    "ajax": {
                        "url": "/ServiceInactive/GetProviderServicesList",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            data.Provider = $("#drpProvider").val();
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
                        { "data": "ServiceAPIName", "name": "Service Name", "autoWidth": true, "bSortable": true },
                        {
                            data: null,
                            render: function (data, type, row) {

                                if (data.IsServiceDown) {
                                    return '<span class="tb-status text-danger">InActive</span>';
                                }
                                else {
                                    return '<span class="tb-status text-success">Active</span>';
                                }
                            },
                            bSortable: true,
                            sTitle: "Service Status"
                        },
                        {
                            data: null,
                            render: function (data, type, row) {
                                var str = "";
                                str = '<ul class="nk-tb-actions gx-1">';
                                if (data.IsServiceDown) {
                                    str += '<li class="nk-tb-action">';
                                    str += '<a href="javascript:void(0);" onclick="return ServiceStatusChange(&apos;' + data.Id + '&apos;)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                    str += '<em class="icon ni ni-arrow-up-round"></em>Service Up';
                                    str += '</a>';
                                    str += '</li>';
                                }
                                else {
                                    str += '<li class="nk-tb-action">';
                                    str += '<a href="javascript:void(0);" onclick="return ServiceStatusChange(&apos;' + data.Id + '&apos;)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                    str += '<em class="icon ni ni-arrow-down-round"></em>Service Down';
                                    str += '</a>';
                                    str += '</li>';
                                }
                                str += '</ul>';
                                return str;
                            },
                            bSortable: false,
                            sTitle: "Action"
                        }
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
}

function ServiceStatusChange(id) {
    $("#txtRemarks").val("");
    $("#dvPopupMsg").html("");
    $("#hdnid").val("");
    if (id != "" || id != "0") {
        $("#hdnid").val(id);
        $('#ServiceStatus').modal('show');
    }
    else {
        $("#dvMsg").html("Please select service first.");
    }
}

function ChangeStatus(id, e) {
    var Id = id;
    if (Id == "" || Id == "0") {
        Id = $("#hdnid").val();
    }
    var txtRemarks = $("#txtRemarks").val();
    if (txtRemarks == "") {
        $("#dvPopupMsg").html("Please enter Remarks.");
        return false;
    }
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/ServiceInactive/ServiceBlockUnblock",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Id":"' + Id + '","Remarks":"' + txtRemarks + '"}',
                success: function (response) {
                    if (response != null) {
                        $("#dvPopupMsg").html(response);
                        //$("#dvMsgSuccess").html("successfully updated");
                        $('#ServiceStatus').modal('hide');
                        BindServiceDataTable();
                        e.preventDefault();
                        e.stopPropagation();
                        $('#AjaxLoader').hide();
                        return false;
                    }
                    else {
                        $("#dvPopupMsg").html("Something went wrong. Please try again later.");
                        //$("#dvMsg").html("Something went wrong. Please try again later.");
                        return false;
                    }
                },
                failure: function (response) {
                    $("#dvPopupMsg").html(response.responseText);
                    /*$("#dvMsg").html(response.responseText);*/
                    return false;
                },
                error: function (response) {
                    $("#dvPopupMsg").html(response.responseText);
                    //$("#dvMsg").html(response.responseText);
                    return false;
                }
            });
            $('#AjaxLoader').hide();
        }, 100);
}

function BindServiceRemarksDataTable() {
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
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/ServiceInactive/GetServiceInactiveRemarksList",
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
                    { "data": "CreatedDateDt", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "ServiceName", "name": "Service Name", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    { "data": "Action", "name": "Action", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedByName", "name": "Created By", "autoWidth": true, "bSortable": false }
                   
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