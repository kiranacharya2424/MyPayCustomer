///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindEmpDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
           // var MemberId = $("#MemberId").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#FirstName").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var RoleId = $("#RoleId :selected").val();
            if (Mobile == "" && Name == "" && Email == "" && RoleId == "0" && fromdate == "" && todate == "") {
                alert("Please select field.");
            }
            else {
                table = $('#tblemplist').DataTable({
                    "dom": 'lBfrtip',
                    bFilter: false,
                    "oLanguage": {
                        "sEmptyTable": "No Employee"
                    },
                    "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                    "pageLength": 50,
                    "processing": true,
                    "serverSide": true,
                    "destroy": true,
                    "order": [[2, "desc"]],
                    "ajax": {
                        "url": "/Employee/GetEmployeeLists",
                        "type": "POST",
                        "async": false,
                        data: function (data) {
                            //data.MemberId = MemberId;
                            data.ContactNumber = Mobile;
                            data.Email = Email;
                            data.StartDate = fromdate;
                            data.ToDate = todate;
                            data.Name = Name;
                            data.RoleId = RoleId;
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
                        {
                            data: null,
                            render: function (data, type, row) {
                                $('#employeetotalcount').html(data.TotalUserCount);
                                var str = "";
                                str = '<div class="user-card">';
                                str += '<div class="user-avatar xs">';
                                if (data.FirstName.length > 0 && data.LastName.length > 0) {
                                    str += '<span>' + data.FirstName.charAt(0) + '' + data.LastName.charAt(0) + '</span>';
                                }
                                else {
                                    str += '<span>U</span>';
                                }
                                str += '</div>';
                                str += '<div class="user-info">';
                                str += '<span class="tb-lead">' + data.FirstName + '' + data.LastName + '';
                                str += '<span class="dot dot-danger d-md-none ml-1"></span>';
                                str += '</span><span>';

                                str += '' + data.ContactNumber + '</span></div></div>';
                                return str;
                            },
                            bSortable: true,
                            sTitle: "Full Name"
                        },
                        { "data": "CreatedDatedt", "name": "Joining Date", "autoWidth": true, "bSortable": true },
                        { "data": "UserId", "name": "User Id", "autoWidth": true, "bSortable": false },
                        { "data": "ContactNumber", "name": "Contact", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {

                                if (data.Email == "" || data.Email == null) {
                                    return '<span>--</span>';
                                }
                                else {
                                    return '<span>' + data.Email + '</span>';

                                }
                            },
                            bSortable: false,
                            sTitle: "Email"
                        },
                        { "data": "RoleName", "name": "Role", "autoWidth": true, "bSortable": false },
                        {
                            data: null,
                            render: function (data, type, row) {

                                if (data.IsActive) {
                                    return '<span class="tb-status text-success">Active</span>';
                                }
                                else {
                                    return '<span class="tb-status text-danger">Suspended</span>';
                                }
                            },
                            bSortable: true,
                            sTitle: "Status"
                        },
                        {
                            data: null,
                            render: function (data, type, row) {

                                if (data.IsPasswordExpired) {
                                    return '<span class="tb-status text-danger">Expired</span>';
                                }
                                else {
                                    return '<span class="tb-status text-success">Active</span>';
                                }
                            },
                            bSortable: true,
                            sTitle: "Password Status"
                        },

                        {
                            data: null,
                            render: function (data, type, row) {
                                var str = "";
                                str = '<ul class="nk-tb-actions gx-1">';
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="Employee/Index?MemberId=' + data.MemberId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                                str += '<em class="icon ni ni-eye"></em>';
                                str += '</a>';
                                str += '</li>';
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="/Employee/ResetPassword?MemberId=' + data.MemberId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Reset Password">';
                                str += '<em class="icon ni ni-shield-star"></em>';
                                str += '</a>';
                                str += '</li>';
                                if (data.IsActive) {
                                    str += '<li class="nk-tb-action-hidden">';
                                    str += '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.MemberId + ',event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                    str += '<em class="icon ni ni-user-cross-fill"></em>';
                                    str += '</a>';
                                    str += '</li>';
                                }
                                else {
                                    str += '<li class="nk-tb-action-hidden">';
                                    str += '<a href="javascript:void(0);" onclick="return BlockUnblock(' + data.MemberId + ',event)" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                    str += '<em class="icon ni ni-user-check-fill"></em>';
                                    str += '</a>';
                                    str += '</li>';
                                }

                                str += '<li>';
                                str += '<div class="drodown">';
                                str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                                str += '<div class="dropdown-menu dropdown-menu-right">';
                                str += '<ul class="link-list-opt no-bdr">';
                                str += '<li><a href="Employee/Index?MemberId=' + data.MemberId + '"><em class="icon ni ni-eye"></em><span>View Details</span></a></li>';
                                str += '<li class="divider"></li>';
                                str += '<li><a href="/Employee/ResetPassword?MemberId=' + data.MemberId + '"><em class="icon ni ni-shield-star"></em><span>Reset Pass</span></a></li>';
                                if (data.IsActive) {
                                    str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(' + data.MemberId + ',event)"><em class="icon ni ni-user-cross-fill"></em><span>Disable User</span></a></li>';
                                }
                                else {
                                    str += '<li><a href="javascript:void(0);" onclick="return BlockUnblock(' + data.MemberId + ',event)"><em class="icon ni ni-user-check-fill"></em><span>Enable User</span></a></li>';
                                }
                                if (!data.IsPasswordExpired) {
                                    str += '<li><a href="javascript:void(0);" onclick="return PasswordExpired(' + data.MemberId + ',event)"><em class="icon ni ni-na"></em><span>Password Expire</span></a></li>';
                                }
                                str += '</ul>';
                                str += '</div>';
                                str += '</div>';
                                str += '</li>';
                                str += '</ul>';
                                return str;
                            },
                            bSortable: false,
                            sTitle: ""
                        }
                    ],
                    "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                        $(nRow).addClass('nk-tb-item');

                        $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    }
                });
                document.getElementById("tblemplist").deleteTFoot();

                $("#tblemplist").append(
                    $('<tfoot/>').append($("#tblemplist thead tr").clone())
                );
            }
            $('#AjaxLoader').hide();
        }, 100);
}

function BlockUnblock(memberid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Employee/EmpBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MemberId":"' + memberid + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");

                    var tableId = $(this).data("table");
                    BindEmpDataTable(tableId);
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

function PasswordExpired(memberid, e) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/Employee/EmpPasswordExpired",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MemberId":"' + memberid + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    $("#dvMsgSuccess").html("successfully updated");

                    var tableId = $(this).data("table");
                    BindEmpDataTable(tableId);
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