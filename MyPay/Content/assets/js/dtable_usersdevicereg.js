///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var DeviceId = $("#DeviceId").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Record Found"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/UsersDeviceRegistrationReport/GetUsersDeviceRegistrationLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.DeviceId = DeviceId;
                    },
                    error: function (xhr, error, code) {
                        if (xhr.status == 200) {
                            alert('Session Timeout. Please login again to continue.');
                        }
                        else {
                            alert("Something went wrong try again later");
                        }alert('Session Timeout. Please login again to continue.');
                        location.reload();
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    { "data": "PlatForm", "name": "Platform", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsActive) {
                                return "<span class='tb-status text-success'>Active</span>";
                            }
                            else {
                                return "<span class='tb-status text-danger'>Suspended</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },
                    { "data": "DeviceCode", "name": "DeviceCode", "autoWidth": true, "bSortable": true },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.PlatForm + '&apos;,&apos;' + data.DeviceCode + '&apos;,&apos;' + data.DeviceCode + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.SequenceNo + '&apos;,&apos;' + data.PreviousDeviceCode + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "View"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.CreatedDatedt + '&apos;,&apos;' + data.MemberId + '&apos;,&apos;' + data.PlatForm + '&apos;,&apos;' + data.DeviceCode + '&apos;,&apos;' + data.DeviceCode + '&apos;,&apos;' + data.IpAddress + '&apos;,&apos;' + data.SequenceNo + '&apos;,&apos;' + data.PreviousDeviceCode + '&apos;)" title="" data-original-title="View"><em class="icon ni ni-eye"></em></a>';
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
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}