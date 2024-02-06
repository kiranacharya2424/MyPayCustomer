

///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;
$(document).ready(function () {
    BindDataTable("tbllist");
});

$(function () {
    $(".nav-item").click(function () {
        debugger;
        var tableId = $(this).data("table");
        BindDataTable(tableId);
    });
});

function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
function FormatDate(date) {
    debugger;
    var d = new Date(date);
    var day = addZero(d.getDate());
    var month = addZero(d.getMonth() + 1);
    var year = addZero(d.getFullYear());
    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());
    var s = addZero(d.getSeconds());
    return year + "-" + month + "-" + day + "T" + h + ":" + m + "";
}
function BindDataTable(tableId) {
    $('#AjaxLoader').show();

    setTimeout(
        function () {
            $("#tbllist").show();
            table = $('#tbllist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Active Notification Campaigns  "
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "searching": false,
                "sorting": false,
                "order": [[0, "desc"]],
                "ajax": {
                    "url": "/NotificationCampaign/GetAdminNotificationCampaignLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.IsDeleted = 0;
                        data.GenderStatus = $("#GenderStatusEnum").val();
                        data.KycStatus = $("#KycStatusEnum").val();
                        debugger;
                        data.Geography = $("#GeographyTypeEnum").val();
                        data.UserType = $("#OldUserStatusEnum").val();
                        data.DeviceTypeStatus = $("#DeviceTypeEnum").val();
                        data.RedirectionType = $("#NotificationRedirectType").val();
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
                    { "data": "Sno", "name": "Sno", "autoWidth": true, "bSortable": false },
                    { "data": "CreatedDatedt", "name": "CreatedDate", "autoWidth": true, "bSortable": false },
                    { "data": "UpdatedDatedt", "name": "UpdatedDate", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<textarea id="title' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;" >' + data.Title + '</textarea>';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<textarea  id="message' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;">' + data.NotificationMessage + '</textarea>';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {

                            return '<textarea id="description' + data.Id + '"  class="form-control form-control-md form-control-outlined dttxtcontrol_inGrid"  style="width:250px;height:60px;min-width:250px;min-height:60px; resize: none;">' + data.NotificationDescription + '</textarea>';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<input id="ScheduleDateDT' + data.Id + '" type="datetime-local" style="width:180px;" class="form-control form-control-md form-control-outlined dtdatecontrol_inGrid" value="' + FormatDate(data.ScheduleDateTimeDT) + '">';
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var str = "";
                            str = '<a class="gallery-image popup-image" href="/Images/NotificationImages/' + data.NotificationImage + '" style="min-height:auto; height:auto;">';
                            if (data.NotificationImage == null || data.NotificationImage == "") {
                                str += '<img ID="targetNotificationImage' + data.Id + '" class="custom-file-inputrounded-top" width="120" src="/Content/assets/Images/noimageblank.png" alt="">';
                            }
                            else {
                                str += '<img ID="targetNotificationImage' + data.Id + '" class="custom-file-inputrounded-top" width="200" src="/Images/NotificationImages/' + data.NotificationImage + '" alt="">';
                            }
                            str += '</a>';
                            return str;
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var RedirectionTypeName = '<input type="hidden" id="RedirectionTypeId' + data.Id + '" value="' + data.RedirectionType + '"/>';
                            RedirectionTypeName = RedirectionTypeName + '<span  class="tb-status text-' + (data.NotificationRedirectTypeName != "Select Type" ? "success" : "") + '">' + data.NotificationRedirectTypeName + '</span>';
                            return RedirectionTypeName;
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    { "data": "URL", "name": "URL", "autoWidth": true, "bSortable": false },
                   
                   /* {
                        data: null,
                        render: function (data, type, row) {
                            var URL = '<input type="hidden" id="URL' + data.Id + '" value="' + data.URL + '"/>';
                            RedirectionTypeName = RedirectionTypeName + '<span  class="tb-status text-' + (data.NotificationRedirectTypeName != "Select Type" ? "success" : "") + '">' + data.NotificationRedirectTypeName + '</span>';
                            return RedirectionTypeName;
                        },
                        autoWidth: true,
                        bSortable: false
                    },*/
                    {
                        data: null,
                        render: function (data, type, row) {
                            var UserTypeName = '<input type="hidden" id="UserTypeId' + data.Id + '" value="' + data.UserTypeName + '"/>';
                            UserTypeName = UserTypeName + '<span  class="tb-status text-' + (data.IsOldUserStatusName == "All" ? "success" : "") + '">' + data.IsOldUserStatusName + '</span>';
                            return UserTypeName;
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var objGenderStatus = '<input type="hidden" id="GenderStatusId' + data.Id + '" value="' + data.GenderStatus + '"/>';
                            objGenderStatus = objGenderStatus + '<span  class="tb-status text-' + (data.GenderStatusName == "All" ? "success" : "") + '">' + data.GenderStatusName + '</span>';
                            return objGenderStatus;
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var objKycStatus = '<input type="hidden" id="KycStatusId' + data.Id + '" value="' + data.KycStatus + '"/>';
                            objKycStatus = objKycStatus + '<span  class="tb-status text-' + (data.KycStatusName == "NotVerified" ? "danger" : "success") + '">' + data.KycStatusName + '</span>';
                            return objKycStatus;
                        },
                        autoWidth: true,
                        bSortable: false
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var objDeviceTypeStatus = '<input type="hidden" id="DeviceTypeStatusId' + data.Id + '" value="' + data.DeviceTypeStatus + '"/>';
                            objDeviceTypeStatus = objDeviceTypeStatus + '<span  class="tb-status text-' + (data.DeviceTypeStatusName != "All" ? "danger" : "success") + '">' + data.DeviceTypeStatusName + '</span>';
                            return objDeviceTypeStatus;
                        },
                        autoWidth: true,
                        bSortable: false
                    },

                    {
                        data: null,
                        render: function (data, type, row) {
                            var objKycStatus = '<input type="hidden" id="SentStatusId' + data.Id + '" value="' + data.SentStatus + '"/>';
                            objKycStatus = objKycStatus + '<span  class="tb-status text-' + (data.SentStatusName != "Sent" ? "danger" : "success") + '">' + data.SentStatusName + '</span>';
                            return objKycStatus;
                        },
                        autoWidth: true,
                        bSortable: false
                    },


                    { "data": "TotalNotificationSent", "name": "Total Sent", "autoWidth": true, "bSortable": false },

                    { "data": "Geography", "name": "Geography", "autoWidth": true, "bSortable": false },
                    { "data": "Province", "name": "Province", "autoWidth": true, "bSortable": false },
                    { "data": "District", "name": "District", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var actioncolumn = '<table><tr>';
                            if (data.SentStatus == "0") {
                                actioncolumn = actioncolumn + '<td><a  href="/NotificationCampaign/AddNotificationImage?Id=' + data.Id + '" id="UpdateNotificationCampaign' + data.Id + '"   class="btn btn-sm btn-info btnNotificationCampaignAction" style="white-space:nowrap;"  title="Add Image"> + Image</a></td>';

                            }
                            else {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateNotificationCampaign' + data.Id + '"   class="btn btn-sm btn-info btnNotificationCampaignAction" style="white-space:nowrap;"  title="Add Image"  onclick=\'alert("Image Will Not Update As Notification Campaign Already Published.")\' > + Image</a></td>';
                            }
                            if (data.SentStatus == "0") {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateNotificationCampaign' + data.Id + '"  class="btn btn-sm btn-success btnNotificationCampaignAction"  title="Update NotificationCampaign" onclick=\'return updateNotificationCampaign(\"' + data.Id + '\");\'  class="btn btn-primary "> Save </a></td>';
                            }
                            else {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" id="UpdateNotificationCampaign' + data.Id + '"  class="btn btn-sm btn-success btnNotificationCampaignAction"  title="Update NotificationCampaign"   onclick=\'alert("Published Notification Campaign are readonly.")\'   class="btn btn-primary "> Save </a></td>';
                            }
                            if (data.SentStatus == "0") {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-success btnNotificationCampaignAction btnpublish"   title="Broadcast NotificationCampaign" onclick=\'return BroadcastNotificationCampaign(\"' + data.Id + '\",\"' + "true" + '\"   ,\"' + data.Province + '\"  ,\"' + data.District + '\");\' "> ' + " Broadcast " + ' </a></td>';
                            }
                            else {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);"  class="btn btn-sm btn-danger btnNotificationCampaignAction btnpublish"  title="Published" onclick=\'alert("Notification Campaign already published .")\' > ' + "Published" + ' </a></td>';
                            }
                            if (data.SentStatus == "0") {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'return DeleteNotificationCampaign(\"' + data.Id + '\");\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btnNotificationCampaignAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
                            }
                            else {
                                actioncolumn = actioncolumn + '<td><a href="javascript:void(0);" onclick=\'alert("Cannot perform delete operation on already published Notification Campaign.")\' class="ml-2 btn btn-sm btn-outline-danger btn-icon btn-tooltip btnNotificationCampaignAction" title="Delete"><em class="icon ni ni-trash"></em></a></td>';
                            }
                            actioncolumn = actioncolumn + '</tr></table>'
                            return actioncolumn;
                        },
                        bSortable: false,
                        sTitle: "Action"
                    }


                ]
            });

            if (document.getElementById("tbllist") != undefined) {
                document.getElementById("tbllist").deleteTFoot();
            }

            $("#tbllist").append(
                $('<tfoot/>').append($("#tbllist thead tr").clone())
            );
            //}
            //else {
            //    $("#tbllist").hide();
            //}
            $("#anchor_addNotificationCampaignRow").show();
            $("#dvMsgSuccess").html("");
            $("#dvMsg").html("");
            $("#tbllist_length").hide();
            $(".dt-buttons btn-group flex-wrap").hide();
            //$('.datePicker').datepicker({
            //    format: 'd M yyyy',
            //    startDate: '+0d'
            //});
            //$(".datetimepicker").each(function () {
            //    $(this).datetimepicker();
            //});
            //setTimeout(function () {
            //    /*$(".datetimepicker").each(function () {*/
            //    $("#description162").datetimepicker();
            //   /* });*/
            //}, 1000);
            //        $('body').on('focus', ".datepicker", function () {
            //    $(this).datepicker();
            //});
            $('#AjaxLoader').hide();
        }, 100);
    //(".btnNotificationCampaignAction").tooltip();
}
$('[id*=btnsearch]').on('click', function () {
    table.draw();
});


function CheckNotificationCampaignRowValidation() {
    debugger;
    var URL = $("#URL").val();
    var RedirectionType = $("#NotificationRedirectType").val();

    if (RedirectionType !== "0" && RedirectionType !== "") {
        if (RedirectionType === "1001") {
            if (URL === "0" || URL === "") {
                $("#dvMsg").html("Please enter URL");
            } else
            {
                AddNotificationCampaignRow();
            }
        }
        else
        {
            AddNotificationCampaignRow();
        }
    }
    else
    {
        $("#dvMsg").html("Please select Redirection Type");
    }
}

//function AddNotificationCampaignRow() {
//    debugger;
//    $("#dvMsgSuccess").html("");
//    $("#dvMsg").html("");
//    var GenderStatus = $("#GenderStatusEnum").val();
//    var KycStatus = $("#KycStatusEnum").val();
//    var UserType = $("#OldUserStatusEnum").val();
//    var DeviceType = $("#DeviceTypeEnum").val();
//    var URL = $("#URL").val();
//    var RedirectionType = $("#NotificationRedirectType").val();
//    if (URL == "0" || URL == "") {
//        $("#dvMsg").html("Please select URL Type");
//    }
//    if (RedirectionType == "0" || RedirectionType == "") {
//        $("#dvMsg").html("Please select Redirection Type");
//    }
//    else {

//        var NotificationCampaign = new Object();
//        NotificationCampaign.Id = 0;
//        NotificationCampaign.GenderStatus = GenderStatus;
//        NotificationCampaign.KycStatus = KycStatus;
//        NotificationCampaign.IsOldUserStatus = UserType;
//        NotificationCampaign.DeviceTypeStatus = DeviceType;
//        NotificationCampaign.URL = URL;
//        NotificationCampaign.NotificationRedirectType = RedirectionType;
//        NotificationCampaign.ScheduleDateTime = GetTodayDate();
//        if (NotificationCampaign != null) {
//            $.ajax({
//                type: "POST",
//                url: "/NotificationCampaign/AddNewNotificationCampaignDataRow",
//                data: JSON.stringify(NotificationCampaign),
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (response) {
//                    if (response != null) {
//                        BindDataTable("tbllist");
//                        $("#dvMsgSuccess").html("Add Notification Campaigns Values and click Update button in new row.");
//                        // $("#anchor_addNotificationCampaignRow").hide();
//                        return true;
//                    } else {
//                        $("#dvMsg").html("Something went wrong");
//                        return false;
//                    }
//                },
//                failure: function (response) {
//                    $("#dvMsg").html(response.responseText);
//                    return false;
//                },
//                error: function (response) {
//                    $("#dvMsg").html(response.responseText);
//                    return false;
//                }
//            });
//        }
//    }
//}


function CheckNotificationCampaignRowValidation() {
    debugger;
    var URL = $("#URL").val();
    var RedirectionType = $("#NotificationRedirectType").val();

    if (RedirectionType !== "0" && RedirectionType !== "") {
        if (RedirectionType === "1001") {
            if (URL === "0" || URL === "") {
                $("#dvMsg").html("Please enter URL");
            } else {
                AddNotificationCampaignRow();
            }
        } else {
            AddNotificationCampaignRow();
        }
    } else {
        $("#dvMsg").html("Please select Redirection Type");
    }
}

function AddNotificationCampaignRow() {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var GenderStatus = $("#GenderStatusEnum").val();
    var KycStatus = $("#KycStatusEnum").val();
    var UserType = $("#OldUserStatusEnum").val();
    var GeographyType = $("#GeographyTypeEnum").val();
    var Province = $("#Province").val();
    var District = $("#District").val();
    var DeviceType = $("#DeviceTypeEnum").val();
    var URL = $("#URL").val();
    var RedirectionType = $("#NotificationRedirectType").val();

        var NotificationCampaign = new Object();
        NotificationCampaign.Id = 0;
        NotificationCampaign.GenderStatus = GenderStatus;
        NotificationCampaign.KycStatus = KycStatus;
    NotificationCampaign.IsOldUserStatus = UserType;
    
    NotificationCampaign.Geography = GeographyType;
    debugger;
    NotificationCampaign.Province = Province;
    NotificationCampaign.District = District;
        NotificationCampaign.DeviceTypeStatus = DeviceType;
        NotificationCampaign.URL = URL;
        NotificationCampaign.NotificationRedirectType = RedirectionType;
        NotificationCampaign.ScheduleDateTime = GetTodayDate();
        if (NotificationCampaign != null) {
            $.ajax({
                type: "POST",
                url: "/NotificationCampaign/AddNewNotificationCampaignDataRow",
                data: JSON.stringify(NotificationCampaign),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Add Notification Campaigns Values and click Update button in new row.");
                        // $("#anchor_addNotificationCampaignRow").hide();
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
    }



function updateNotificationCampaign(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $("#UpdateNotificationCampaign" + id).attr("disabled", "disabled");
    var Title = $("#title" + id).val(), Message = $("#message" + id).val(), Description = $("#description" + id).val(), ScheduleDateDT = $("#ScheduleDateDT" + id).val();
    var UserType = $("#UserType" + id).val(), GenderStatus = $("#GenderStatusId" + id).val(), KycStatus = $("#KycStatusId" + id).val(), RedirectionType = $("#NotificationRedirectType" + id).val();

    if (Title == "") {
        $("#dvMsg").html("Please enter Title");
        return false;
    }
    else if (Message == "") {
        $("#dvMsg").html("Please enter Message");
        return false;
    }
    else if (Description == "") {
        $("#dvMsg").html("Please enter Description");
        return false;
    }
    else if (UserType == "") {
        $("#dvMsg").html("Please enter UserType");
        return false;
    }
    else if (RedirectionType == "") {
        $("#dvMsg").html("Please enter RedirectionType");
        return false;
    }
    else if (ScheduleDateDT == "") {
        $("#dvMsg").html("Please enter Schedule Date");
        return false;
    }
    var NotificationCampaign = new Object();
    NotificationCampaign.Id = parseInt(id);
    NotificationCampaign.IsOldUserStatus = UserType;
    NotificationCampaign.GenderStatus = GenderStatus;
    NotificationCampaign.KycStatus = KycStatus;
    NotificationCampaign.Title = Title;
    NotificationCampaign.NotificationMessage = Message;
    NotificationCampaign.NotificationDescription = Description;
    NotificationCampaign.NotificationRedirectType = RedirectionType;
    NotificationCampaign.ScheduleDateTime = ScheduleDateDT;
    var dd = JSON.stringify(NotificationCampaign);
    if (NotificationCampaign != null) {
        $.ajax({
            type: "POST",
            url: "/NotificationCampaign/NotificationCampaignUpdateCall",
            data: JSON.stringify(NotificationCampaign),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                if (response != null) {
                    if (response.Id == "0") {
                        $("#dvMsg").html("Records not updated. Please check Title and Message.");
                        return false;
                    }
                    else {
                        $("#UpdateNotificationCampaign" + id).text("Processing..");
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Notification Campaigns are successfully updated");
                        $("#anchor_addNotificationCampaignRow").show();
                        return true;
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
    else {
        return false;
    }
}




function BroadcastNotificationCampaign(id, activestatus,prv,dst) {
    debugger;
    if (confirm('Do you really want to broadcast this notification campaign ?')) {
        $("#dvMsgSuccess").html("");
        $("#dvMsg").html("");
        var NotificationCampaign = new Object();
        NotificationCampaign.Id = parseInt(id);
        debugger;
        NotificationCampaign.Province = prv;
        NotificationCampaign.District = dst;
        var dd = JSON.stringify(NotificationCampaign);
        if (NotificationCampaign != null) {
            $.ajax({
                type: "POST",
                url: "/NotificationCampaign/BroadcastNotificationCampaignCall",
                data: JSON.stringify(NotificationCampaign),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    debugger;
                    if (response != null) {
                        if (response.Id != "0") {
                            BindDataTable("tbllist");
                            if (activestatus == "true")
                                $("#dvMsgSuccess").html("Notification Campaigns are successfully Published");
                            else
                                $("#dvMsgSuccess").html("Notification Campaigns are successfully Unpublished");
                            $("#anchor_addNotificationCampaignRow").show();
                            return true;
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
            BindDataTable("tbllist");
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function DeleteNotificationCampaign(id) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    if (confirm("Do you really want to delete Notification Campaign ?")) {
        var NotificationCampaign = new Object();
        NotificationCampaign.Id = id;
        if (NotificationCampaign != null) {
            $.ajax({
                type: "POST",
                url: "/NotificationCampaign/DeleteNotificationCampaignDataRow",
                data: JSON.stringify(NotificationCampaign),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        BindDataTable("tbllist");
                        $("#dvMsgSuccess").html("Notification Campaigns are successfully deleted");
                        return true;
                    } else {
                        $("#dvMsg").html("Something went wrong");
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
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function isNumberKey(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1) {
        var range = document.selection.createRange();

        if (range.text != "") {
        }
        else {
            var number = el.value.split('.');
            if (number.length == 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
}

function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "-" + (MM + 1) + "-" + yyyy;

    return currentDate;
}

$(document).ready(function () {

    $("#anchor_SearchNotificationCampaign").on("click", function () {
        debugger;
        BindDataTable("tbllist");
    });
    $("#OldUserStatusEnum").on("change", function () {
        debugger;
        $("#IsOldUserStatus").val($("#OldUserStatusEnum").val());
    });

});

//NotificationCampaignUpdateHistory DataTable
function BindNotificationCampaignUpdateHistoryDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Request"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/NotificationCampaign/GetNotificationCampaignUpdateHistoryLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.GenderStatus = $("#GenderStatusEnum").val();
                        data.KycStatus = $("#KycStatusEnum").val();
                        data.Status = $("#Status").val();
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
                    { "data": "Sno", "name": "Sno", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDateDt", "name": "Created Date", "autoWidth": true },
                    { "data": "UpdatedDateDt", "name": "Updated Date", "autoWidth": true },
                    { "data": "CreatedByName", "name": "Update By", "autoWidth": true },
                    { "data": "IpAddress", "name": "Ip Address", "autoWidth": true },
                    { "data": "StatusName", "name": "Status", "autoWidth": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var objScheduleStatusType = '<span  class="tb-status text-' + (data.ScheduleStatus != "Running" ? "danger" : "success") + '">' + data.ScheduleStatus + '</span>';
                            return objScheduleStatusType;
                        },
                        autoWidth: true,
                        bSortable: false
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

function updateproviderNotificationCampaign(id) {
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    var NotificationCampaign = $("#NotificationCampaign" + id).val();


    if (NotificationCampaign == "") {
        $("#dvMsg").html("Please enter Cashback");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/NotificationCampaign/ProviderNotificationCampaignUpdate",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: '{"NotificationCampaign":"' + NotificationCampaign + '","Id":"' + id + '"}',
        success: function (response) {
            if (response != null) {
                if (response.Id == "0") {
                    $("#dvMsg").html("Records not updated.");
                    return false;
                }
                else {
                    BindProviderServiceCategoryDataTable();
                    $("#dvMsgSuccess").html("Notification Campaign successfully updated");
                    return true;
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

function BlockUnblock(id, url, e) {
    debugger;
    $("#dvMsgSuccess").html("");
    $("#dvMsg").html("");
    $.ajax({
        type: "POST",
        url: "" + url + "",
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
                    BindDataTable("tbllist");
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