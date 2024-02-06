

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
var take = 50;
var skip = 0;
var sort = "";
var sortdir = "";
var kycstatutsexport = -1;
//$(document).ready(function () {
//    BindDataTable("tblistpending");
//});

$(function () {
    $(".nav-item").click(function () {
        //var source = $(this).data("source");
        var tableId = $(this).data("table");
        BindDataTable(tableId);
    });
});

function BindDataTable(tableId) {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvMessage").html("");
            var MemberId = $("#MemberId").val();
            var Mobile = $("#ContactNumber").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Name = $("#FirstName").val();
            var IsActive = $("#IsActiveStatusEnum").val();
            var RefCode = $("#RefCode").val();
            var fromreviewdate = $("#fromreviewdate").val();
            var toreviewdate = $("#toreviewdate").val();

            if (MemberId === "" && Name === "" && Mobile === "" && Email === ""
                && IsActive === "2" && RefCode === "" && fromreviewdate === "" && toreviewdate === "") {
                if (fromdate == "") {
                    $("#dvMessage").html("Please select Start Date.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else if (todate == "") {
                    $("#dvMessage").html("Please select End Date.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else {
                    $("#dvMessage").html("");
                }
            }
            var KYCStatus = -1;
            if (tableId == "tblistpending") {
                KYCStatus = 1;
            }
            else if (tableId == "tblistapproved") {
                KYCStatus = 3;
            }
            else if (tableId == "tblistrejected") {
                KYCStatus = 4;
            }
            else if (tableId == "tblistnotfilled") {
                KYCStatus = 0;
            }
            else {
                KYCStatus = -1;
            }


            table = $('#tbllist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No KYC Request"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[5, "desc"]],
                "ajax": {
                    "url": "/UserKYC/GetUserKYCLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = Mobile;
                        data.Email = Email;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.IsKYCApproved = KYCStatus;
                        data.IsActive = IsActive;
                        data.RefCode = RefCode;
                        data.Name = Name;
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
                        kycstatutsexport = KYCStatus;
                        data.StartReviewDate = fromreviewdate;
                        data.ToReviewDate = toreviewdate;
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
                    { "data": "FirstName", "name": "Name", "autoWidth": true, "bSortable": true },
                    { "data": "ContactNumber", "name": "Mobile", "autoWidth": true, "bSortable": false },
                    { "data": "GenderName", "name": "Gender", "autoWidth": true, "bSortable": true },
                    { "data": "DateofBirthdt", "name": "Date Of Birth", "autoWidth": true, "bSortable": true },
                    { "data": "DOBType2", "name": "DOBType2", "autoWidth": true, "bSortable": true },
                    { "data": "KYCCreatedDatedt", "name": "KYC Created Time", "autoWidth": true, "bSortable": true },
                    { "data": "KYCReviewDateDt", "name": "Review Date", "autoWidth": true, "bSortable": false },
                    { "data": "TimeElapsed", "name": "Time Elapsed", "autoWidth": true, "bSortable": false },
                    { "data": "ApprovedorRejectedByName", "name": "Edit By", "autoWidth": true, "bSortable": false },
                    { "data": "Remarks", "name": "Remarks", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            $('#kyctotalcount').html(data.TotalUserCount);
                            if (data.IsKYCApproved == 0) {
                                return "<span class='tb-status text-danger'>Not Filled</span>";
                            }
                            if (data.IsKYCApproved == 1) {
                                return "<span class='tb-status text-warning'>Pending</span>";
                            }
                            else if (data.IsKYCApproved == 2) {
                                return "<span class='tb-status text-danger'>InComplete</span>";
                            }
                            else if (data.IsKYCApproved == 3) {
                                return "<span class='tb-status text-success'>Verified</span>";
                            }
                            else if (data.IsKYCApproved == 4) {
                                return "<span class='tb-status text-danger'>Rejected</span>";
                            }
                            else if (data.IsKYCApproved == 5) {
                                return "<span class='tb-status text-danger'>Proof_Rejected</span>";
                            }
                            else if (data.IsKYCApproved == 6) {
                                return "<span class='tb-status text-danger'>Risk_High</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "Status"
                    },

                    //{
                    //    data: null,
                    //    render: function (data, type, row) {
                    //        return '<a href="/User/UserEdit?MemberId=' + data.MemberId + '"  class="btn btn-primary "><em class="icon ni ni-edit"></em></a>';
                    //    },
                    //    bSortable: false,
                    //    sTitle: "Edit"
                    //}
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="User/UserDetails?MemberId=' + data.MemberId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            str += '<em class="icon ni ni-eye"></em>';
                            str += '</a>';
                            str += '</li>';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="/User/UserEdit?MemberId=' + data.MemberId + '"  class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Edit"><em class="icon ni ni-edit"></em></a>';
                            str += '</li>';
                            str += '<li>';
                            str += '<div class="drodown">';
                            str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                            str += '<div class="dropdown-menu dropdown-menu-right">';
                            str += '<ul class="link-list-opt no-bdr">';
                            str += '<li><a href="User/UserDetails?MemberId=' + data.MemberId + '"><em class="icon ni ni-eye"></em><span>View Details</span></a></li>';
                            str += '<li><a href="/User/UserEdit?MemberId=' + data.MemberId + '"><em class="icon ni ni-edit"></em><span>Edit</span></a></li>';
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
                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }
            });

            document.getElementById("tbllist").deleteTFoot();

            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );

            $('#AjaxLoader').hide();
        }, 100);
}


function BindUserDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {

            $("#dvMessage").html("");
            var MemberId = $("#MemberId").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#FirstName").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Kycstatus = $("#KycStatusEnum").val();
            var RoleId = $("#UserRoleEnumType").val();
            var IsActive = $("#IsActiveStatusEnum").val();
            var OldUserStatuses = $("#OldUserStatuses").val();
            var RefCode = $("#RefCode").val();
            var RefId = $("#RefId").val();
            var RefCodeAttempted = $("#RefCodeAttempted").val();
            var DocumentNumber = $("#DocumentNumber").val();
            var DeviceId = $("#DeviceId").val();
            if (MemberId === "" && Name === "" && Mobile === "" && Email === "" && Kycstatus === "-1" && RoleId === "0"
                && IsActive === "2" && OldUserStatuses === "0" && RefCode === "" && RefId === "" && RefCodeAttempted === "" && DocumentNumber === "" && DeviceId === "") {
                if (fromdate == "") {
                    $("#dvMessage").html("Please select Start Date.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else if (todate == "") {
                    $("#dvMessage").html("Please select End Date.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else {
                    $("#dvMessage").html("");
                }
            }
            table = $('#tbluserlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No User"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                //"scrollY": "500px" ,
                //"scrollX": true,
                "destroy": true,
                "order": [[3, "desc"]],
                "ajax": {
                    "url": "/User/GetUserLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = Mobile;
                        data.Email = Email;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Name = Name;
                        data.KycStatus = Kycstatus;
                        data.RoleId = RoleId;
                        data.IsActive = IsActive;
                        data.RefCode = RefCode;
                        data.RefId = RefId;
                        data.OldUserStatuses = OldUserStatuses;
                        data.RefCodeAttempted = RefCodeAttempted;
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
                        data.DocumentNumber = DocumentNumber;
                        data.DeviceId = DeviceId;
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
                            $('#kyctotalcount').html(data.TotalUserCount);
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
                            str += '<span class="tb-lead">' + data.FirstName + ' ' + data.MiddleName + ' ' + data.LastName + '';
                            str += '<span class="dot dot-danger d-md-none ml-1"></span>';
                            str += '</span><span>';
                            //if (data.IsPhoneVerified) {
                            //    str += '<em class="icon text-success ni ni-check-circle"></em>';
                            //}
                            //else {
                            //    str += '<em class="icon text-danger ni ni-cross-circle"></em>';
                            //}
                            //str += '' + data.ContactNumber + '';
                            str += '</span></div></div>';

                            return str;
                        },
                        bSortable: true,
                        sTitle: "Full Name"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsPhoneVerified) {
                                return '<span><em class="icon text-success ni ni-check-circle"></em>' + data.ContactNumber + '</span >';
                            }
                            else {
                                return '<span><em class="icon text-danger ni ni-cross-circle"></em>' + data.ContactNumber + '</span >';
                            }
                        },
                        bSortable: true,
                        sTitle: "Contact No"
                    },
                    { "data": "CreatedDatedt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.Email == "" || data.Email == null) {
                                return '<span>--</span>';
                            }
                            else {
                                if (data.IsEmailVerified) {
                                    return '<span><em class="icon text-success ni ni-check-circle"></em>' + data.Email + '</span>';
                                }
                                else {
                                    return '<span><em class="icon text-danger ni ni-cross-circle"></em>' + data.Email + '</span>';
                                }
                            }
                        },
                        bSortable: false,
                        sTitle: "Email"
                    },
                    { "data": "DateofBirthdt", "name": "DOB", "autoWidth": true, "bSortable": true },
                    { "data": "DOBType2", "name": "DOBType2", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.Gender === 0) {
                                return '<span>--</span>';
                            }
                            else {
                                return '<span>' + data.GenderName + '</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Gender"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-amount"> <span class="currency">Rs.</span> ' + data.TotalAmount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Available Balance"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-amount">' + data.TotalRewardPoints + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Reward Points"
                    },
                    //{ "data": "VerificationCode", "name": "OTP", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.RoleId == "1") {
                                return '<span class="tb-status text-danger">' + data.RoleName + ' </span>';
                            }
                            else if (data.RoleId == "3") {
                                return '<span class="tb-status text-warning">' + data.RoleName + ' </span>';
                            }
                            else if (data.RoleId == "4") {
                                return '<span class="tb-status text-success">' + data.RoleName + ' </span>';
                            }
                            else {
                                return '<span class="tb-status">' + data.RoleName + ' </span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "RoleName"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.IsActive) {
                                return '<span class="tb-status text-success">Active</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">Blocked</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsKYCApproved == 0) {
                                return "<span class='tb-status text-danger'>Not Filled</span>";
                            }
                            if (data.IsKYCApproved == 1) {
                                return "<span class='tb-status text-warning'>Pending</span>";
                            }
                            else if (data.IsKYCApproved == 2) {
                                return "<span class='tb-status text-danger'>InComplete</span>";
                            }
                            else if (data.IsKYCApproved == 3) {
                                return "<span class='tb-status text-success'>Verified</span>";
                            }
                            else if (data.IsKYCApproved == 4) {
                                return "<span class='tb-status text-danger'>Rejected</span>";
                            }
                            else if (data.IsKYCApproved == 5) {
                                return "<span class='tb-status text-danger'>Proof Rejected</span>";
                            }
                            else if (data.IsKYCApproved == 6) {
                                return "<span class='tb-status text-danger'>Risk High</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "KYC"
                    },
                    { "data": "RefCodeAttempted", "name": "RefCode Attempted", "autoWidth": true, "bSortable": true },
                    { "data": "DeviceId", "name": "DeviceId", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<ul class="nk-tb-actions gx-1">';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="User/UserDetails?MemberId=' + data.MemberId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="View Details">';
                            str += '<em class="icon ni ni-eye"></em>';
                            str += '</a>';
                            str += '</li>';
                            str += '<li class="nk-tb-action-hidden">';
                            str += '<a href="/User/ResetPassword?MemberId=' + data.MemberId + '" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Reset Password">';
                            str += '<em class="icon ni ni-shield-star"></em>';
                            str += '</a>';
                            str += '</li>';
                            if (data.IsActive) {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return OpenRemarksModal(' + data.MemberId + ')" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Disable">';
                                str += '<em class="icon ni ni-user-cross-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }
                            else {
                                str += '<li class="nk-tb-action-hidden">';
                                str += '<a href="javascript:void(0);" onclick="return OpenRemarksModal(' + data.MemberId + ')" class="btn btn-trigger btn-icon" data-toggle="tooltip" data-placement="top" title="Enable">';
                                str += '<em class="icon ni ni-user-check-fill"></em>';
                                str += '</a>';
                                str += '</li>';
                            }

                            str += '<li>';
                            str += '<div class="drodown">';
                            str += '<a href="javascript:void(0);" class="dropdown-toggle btn btn-icon btn-trigger" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>';
                            str += '<div class="dropdown-menu dropdown-menu-right">';
                            str += '<ul class="link-list-opt no-bdr">';
                            str += '<li><a href="User/UserDetails?MemberId=' + data.MemberId + '"><em class="icon ni ni-eye"></em><span>View Details</span></a></li>';
                            str += '<li><a href="AdminTransactions/Index?MemberId=' + data.MemberId + '"><em class="icon ni ni-repeat"></em><span>Transactions</span></a></li>';
                            str += '<li><a href="UserBankDetail/Index?MemberId=' + data.MemberId + '"><em class="icon ni ni-american-express"></em><span>Bank Detail</span></a></li>';
                            str += '<li><a href="User/CreditDebit?MemberId=' + data.MemberId + '"><em class="icon ni ni-wallet-alt"></em><span>Update Wallet</span></a></li>';
                            str += '<li><a href="User/RefCodeManage?MemberId=' + data.MemberId + '"><em class="icon ni ni-invest"></em><span>Update RefCode</span></a></li>';
                            //str += '<li><a href="UsersDeviceRegistrationReport?MemberId=' + data.MemberId + '"><em class="icon ni ni-activity-alt"></em><span>Devices</span></a></li>';
                            str += '<li class="divider"></li>';
                            str += '<li><a href="/User/ResetPassword?MemberId=' + data.MemberId + '"><em class="icon ni ni-shield-star"></em><span>Reset Pass</span></a></li>';
                            if (data.IsActive) {
                                str += '<li><a href="javascript:void(0);" onclick="return OpenRemarksModal(' + data.MemberId + ')"><em class="icon ni ni-user-cross-fill"></em><span>Disable User</span></a></li>';
                            }
                            else {
                                str += '<li><a href="javascript:void(0);" onclick="return OpenRemarksModal(' + data.MemberId + ')"><em class="icon ni ni-user-check-fill"></em><span>Enable User</span></a></li>';
                            }
                            str += '<li><a href="/UserInActiveStatus?MemberId=' + data.MemberId + '"><em class="icon ni ni-shield-star"></em><span>InActive Status</span></a></li>';

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
                    debugger;
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }


            });


            document.getElementById("tbluserlist").deleteTFoot();

            $("#tbluserlist").append(
                $('<tfoot/>').append($("#tbluserlist thead tr").clone())
            );

            $('#AjaxLoader').hide();
        }, 100);
}

function OpenRemarksModal(memberid) {
    $("#txtRemarks").val("");
    $("#dvPopupMsg").html("");
    $("#hdnmemberid").val("");
    if (memberid != "" || memberid != "0") {
        $("#hdnmemberid").val(memberid);
        $('#UserStatus').modal('show');
    }
    else {
        $("#dvMsg").html("Please select user first.");
    }
}

function BlockUnblock(memberid, e) {
    var MemberId = memberid;
    if (MemberId == "" || MemberId == "0") {
        MemberId = $("#hdnmemberid").val();
    }
    var txtRemarks = $("#txtRemarks").val();
    if (txtRemarks == "") {
        $("#dvPopupMsg").html("Please enter Remarks.");
    }
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "/User/UserBlockUnblock",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"MemberId":"' + MemberId + '","Remarks":"' + txtRemarks + '"}',
        success: function (response) {
            if (response != null) {
                $("#dvPopupMsg").html(response);
                //$("#dvMsgSuccess").html("successfully updated");

                var tableId = $(this).data("table");
                BindUserDataTable(tableId);
                e.preventDefault();
                e.stopPropagation();
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

}

function BindKYCStatusHistoryDataTable(memberid) {
    table = $('#tblkychistory').DataTable({
        "dom": 'lBfrtip',
        bFilter: false,
        "oLanguage": {
            "sEmptyTable": "No KYC Status History"
        },
        "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
        "pageLength": 50,
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "order": [[0, "desc"]],
        "ajax": {
            "url": "/UserKYC/GetKYCStatusHistoryLists",
            "type": "POST",
            "async": false,
            data: function (data) {
                data.MemberId = memberid;
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
            {
                data: null,
                render: function (data, type, row) {

                    if (data.IsAdmin) {
                        return '<span>Admin (' + data.CreatedByName + ')</span>';
                    }
                    else {
                        return '<span>User</span>';
                    }
                },
                bSortable: false,
                sTitle: "Created By"
            },
            { "data": "KYCStatusName", "name": "KYC", "autoWidth": true, "bSortable": false },
            { "data": "Remarks", "name": "Remarks", "autoWidth": false, "bSortable": false },
            { "data": "IpAddress", "name": "Ip Address", "autoWidth": true, "bSortable": false },
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            $(nRow).addClass('nk-tb-item');

            $('td', nRow).addClass('nk-tb-col tb-col-md');

        }
    });

    document.getElementById("tblkychistory").deleteTFoot();

    $("#tblkychistory").append(
        $('<tfoot/>').append($("#tblkychistory thead tr").clone())
    );
}

$('[id*=btnsearch]').on('click', function () {
    table.draw();
});
$(document).ready(function () {
    $("#Remarks").val("0");
    $("#dvKycRemarks").hide();
    $("#IsKYCApproved").change(function () {
        debugger;
        if (($("#IsKYCApproved").val() == "4") || ($("#IsKYCApproved").val() == "5") || ($("#IsKYCApproved").val() == "6")) {
            $("#dvKycRemarks").show();
            $("#Remarks").val("0");
        }
        else {
            $("#dvKycRemarks").hide();
            $("#Remarks").val("0");
        }
    });
    $("#KycSubmit").click(function (e) {
        debugger;
        if (($("#IsKYCApproved").val() == "4") || ($("#IsKYCApproved").val() == "5") || ($("#IsKYCApproved").val() == "6")) {
            if ($("#Remarks").val() == "0") {
                $("#dvMsg").html("Please select Kyc Remarks");
                e.preventDefault();
                e.stopPropagation();
                return;
            }
        }
        else if (($("#IsKYCApproved").val() == "3")) {
            $("#Remarks").val("1");
        }
        else {
            $("#Remarks").val("0");
        }
    });
    $("#KycStatusEnum").prepend("<option value='-1' selected='selected'>-- Select status --</option>");
    $("#IsActiveStatusEnum").prepend("<option value='2' selected='selected'>-- Select status --</option>");
});

function BindInActiveUserDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {

            $("#dvMessage").html("");
            var MemberId = $("#MemberId").val();
            var Mobile = $("#ContactNumber").val();
            var Name = $("#FirstName").val();
            var Email = $("#Email").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var Kycstatus = $("#KycStatusEnum").val();
            var RoleId = $("#UserRoleEnumType").val();
            //var IsActive = $("#IsActiveStatusEnum").val();
            var OldUserStatuses = $("#OldUserStatuses").val();
            var RefCode = $("#RefCode").val();
            var RefId = $("#RefId").val();
            var RefCodeAttempted = $("#RefCodeAttempted").val();
            var DocumentNumber = $("#DocumentNumber").val();
            var DeviceId = $("#DeviceId").val();
            if (MemberId === "" && Name === "" && Mobile === "" && Email === "" && Kycstatus === "-1" && RoleId === "0"
                && OldUserStatuses === "0" && RefCode === "" && RefId === "" && RefCodeAttempted === "" && DocumentNumber === "" && DeviceId === "") {
                if (fromdate == "") {
                    $("#dvMessage").html("Please select Start Date.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else if (todate == "") {
                    $("#dvMessage").html("Please select End Date.");
                    $('#AjaxLoader').hide();
                    return false;
                }
                else {
                    $("#dvMessage").html("");
                }
            }
            table = $('#tbluserlist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No InActive User"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                //"scrollY": "500px" ,
                //"scrollX": true,
                "destroy": true,
                "order": [[3, "desc"]],
                "ajax": {
                    "url": "/User/GetInactiveUsersLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = Mobile;
                        data.Email = Email;
                        data.StartDate = fromdate;
                        data.ToDate = todate;
                        data.Name = Name;
                        data.KycStatus = Kycstatus;
                        data.RoleId = RoleId;
                        //data.IsActive = IsActive;
                        data.RefCode = RefCode;
                        data.RefId = RefId;
                        data.OldUserStatuses = OldUserStatuses;
                        data.RefCodeAttempted = RefCodeAttempted;
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
                        data.DocumentNumber = DocumentNumber;
                        data.DeviceId = DeviceId;
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
                            $('#kyctotalcount').html(data.TotalUserCount);
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
                            str += '<span class="tb-lead">' + data.FirstName + ' ' + data.MiddleName + ' ' + data.LastName + '';
                            str += '<span class="dot dot-danger d-md-none ml-1"></span>';
                            str += '</span><span>';
                            //if (data.IsPhoneVerified) {
                            //    str += '<em class="icon text-success ni ni-check-circle"></em>';
                            //}
                            //else {
                            //    str += '<em class="icon text-danger ni ni-cross-circle"></em>';
                            //}
                            //str += '' + data.ContactNumber + '';
                            str += '</span></div></div>';

                            return str;
                        },
                        bSortable: true,
                        sTitle: "Full Name"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsPhoneVerified) {
                                return '<span><em class="icon text-success ni ni-check-circle"></em>' + data.ContactNumber + '</span >';
                            }
                            else {
                                return '<span><em class="icon text-danger ni ni-cross-circle"></em>' + data.ContactNumber + '</span >';
                            }
                        },
                        bSortable: true,
                        sTitle: "Contact No"
                    },
                    { "data": "CreatedDatedt", "name": "Created Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.Email == "" || data.Email == null) {
                                return '<span>--</span>';
                            }
                            else {
                                if (data.IsEmailVerified) {
                                    return '<span><em class="icon text-success ni ni-check-circle"></em>' + data.Email + '</span>';
                                }
                                else {
                                    return '<span><em class="icon text-danger ni ni-cross-circle"></em>' + data.Email + '</span>';
                                }
                            }
                        },
                        bSortable: false,
                        sTitle: "Email"
                    },
                    { "data": "DateofBirthdt", "name": "DOB", "autoWidth": true, "bSortable": true },

                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.Gender === 0) {
                                return '<span>--</span>';
                            }
                            else {
                                return '<span>' + data.GenderName + '</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Gender"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-amount"> <span class="currency">Rs.</span> ' + data.TotalAmount + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Available Balance"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-amount">' + data.TotalRewardPoints + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Reward Points"
                    },
                    //{ "data": "VerificationCode", "name": "OTP", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.RoleId == "1") {
                                return '<span class="tb-status text-danger">' + data.RoleName + ' </span>';
                            }
                            else if (data.RoleId == "3") {
                                return '<span class="tb-status text-warning">' + data.RoleName + ' </span>';
                            }
                            else if (data.RoleId == "4") {
                                return '<span class="tb-status text-success">' + data.RoleName + ' </span>';
                            }
                            else {
                                return '<span class="tb-status">' + data.RoleName + ' </span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "RoleName"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {

                            if (data.IsActive) {
                                return '<span class="tb-status text-success">Active</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">Blocked</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsKYCApproved == 0) {
                                return "<span class='tb-status text-danger'>Not Filled</span>";
                            }
                            if (data.IsKYCApproved == 1) {
                                return "<span class='tb-status text-warning'>Pending</span>";
                            }
                            else if (data.IsKYCApproved == 2) {
                                return "<span class='tb-status text-danger'>InComplete</span>";
                            }
                            else if (data.IsKYCApproved == 3) {
                                return "<span class='tb-status text-success'>Verified</span>";
                            }
                            else if (data.IsKYCApproved == 4) {
                                return "<span class='tb-status text-danger'>Rejected</span>";
                            }
                            else if (data.IsKYCApproved == 5) {
                                return "<span class='tb-status text-danger'>Proof Rejected</span>";
                            }
                            else if (data.IsKYCApproved == 6) {
                                return "<span class='tb-status text-danger'>Risk High</span>";
                            }
                        },
                        bSortable: false,
                        sTitle: "KYC"
                    },
                    { "data": "RefCodeAttempted", "name": "RefCode Attempted", "autoWidth": true, "bSortable": true },
                    { "data": "DeviceId", "name": "DeviceId", "autoWidth": true, "bSortable": true }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    debugger;
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;

                }


            });


            document.getElementById("tbluserlist").deleteTFoot();

            $("#tbluserlist").append(
                $('<tfoot/>').append($("#tbluserlist thead tr").clone())
            );

            $('#AjaxLoader').hide();
        }, 100);
}
