$(document).ready(function () {
    $("#successmsg").html("");
    $("#errormsg").html("");
    AllTicketTabClick(0);
});


function NewTicketTabClick() {
    $("#dvMessage").html("");
    $('#searchticks').val('');
    $("#hdnticketid").val("");
    $("#NewTicketTab").show();
    $("#AllTicketTab").hide();
    $("#SingleTicketTab").hide();
    $("#successmsg").html("");
    $("#errormsg").html("");
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    $('#hdnSingleTake').val(10);
    $('#hdnSingleSkip').val(-1);
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $("#ticketreply").html("");
    $("#AllTickets").html('');
}

function AllTicketTabClick(status) {
    $("#ticketreply").html("");
    $('#hdnSingleTake').val(10);
    $('#hdnSingleSkip').val(-1);
    $("#nomorerecords").hide();
    $("#dvMessage").html("");
    $("#nomoreSinglerecords").hide();
    $("#nav_All").attr("class", "nav-link");
    $("#nav_open").attr("class", "nav-link");
    $("#nav_Close").attr("class", "nav-link");
    if (status == 0) {
        $("#nav_All").attr("class", "active nav-link");
    }
    else if (status == 1) {
        $("#nav_open").attr("class", "active nav-link");
    }
    else {
        $("#nav_Close").attr("class", "active nav-link");
    }
    var TicketTitle = $("#searchticks").val();
    var ServiceId = 53;
    $("#hdnticketid").val("");
    $("#NewTicketTab").hide();
    $("#AllTicketTab").show();
    $("#SingleTicketTab").hide();
    $("#successmsg").html("");
    $("#errormsg").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserAllGrievance",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Take":"' + $("#hdnTake").val() + '","Skip":"' + $("#hdnSkip").val() + '","TicketTitle":"' + TicketTitle + '","Status":"' + status + '"}',
                success: function (response) {
                    if (response != null) {
                        debugger;
                        var jsonData;
                        var objTrans = '';
                        var IsValidJson = false;
                        try {
                            jsonData = $.parseJSON(response);
                            EventsJson = jsonData;

                            var IsValidJson = true;
                            if (jsonData.TicketList.length < 10) {

                                if ($("#hdnSkip").val() > 0) {
                                    $("#dvMessage").html("No More Records.");
                                    $("#showmore").css("display", "none");
                                }
                                else {
                                    $("#showmore").css("display", "block");
                                }
                            }
                            else {
                                $("#showmore").css("display", "block");
                            }
                        }
                        catch (err) {

                        }
                        //$("#AllTickets").html("");
                        if (jsonData != null && jsonData.TicketList != null && jsonData.TicketList.length > 0) {
                            debugger;
                            for (var i = 0; i < jsonData.TicketList.length; i++) {
                                if (status == 1 && jsonData.TicketList[i].Status == 1) {
                                    objTrans += '<li class="nk-support-item px-0 cursor-pointer" onclick="SingleTicketLoad(&apos;' + jsonData.TicketList[i].TicketId + '&apos;,&apos;' + jsonData.TicketList[i].Status + '&apos;);">';
                                    objTrans += '<div class="serv-icon">';
                                    objTrans += '<div class="user-avatar xs">' + jsonData.TicketList[i].TicketTitle.substring(0, 2).toLocaleUpperCase() + '</div>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="nk-support-content">';
                                    objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.TicketList[i].CreatedDateDt + '</span>';
                                    objTrans += '<div class="title">';
                                    objTrans += '<span class="timeline-title mb-0">' + jsonData.TicketList[i].TicketTitle + '<br/>';
                                    if (jsonData.TicketList[i].TransactionId != "") {
                                        objTrans += '<span class="fs-12px">Transaction Id/Reference No. : ' + jsonData.TicketList[i].TransactionId + '</span>';
                                    }
                                    objTrans += '<div class="d-flex align-items-center fw-normal">';
                                    objTrans += '<span class="fs-12px">' + jsonData.TicketList[i].CategoryName + '</span>';
                                    objTrans += '</div>';
                                    objTrans += '</span>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="d-flex mt-1">';
                                    objTrans += '<p class="d-flex align-items-center fs-12px line-height-1 mt-0 mr-2 text-success">Open</p>';
                                    objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0">' + jsonData.TicketList[i].Name + '</p>';
                                    objTrans += '</div>';
                                    objTrans += '</div>';
                                    objTrans += '</li>';
                                }
                                else if (status == 2 && jsonData.TicketList[i].Status == 2) {
                                    objTrans += '<li class="nk-support-item px-0 cursor-pointer" onclick="SingleTicketLoad(&apos;' + jsonData.TicketList[i].TicketId + '&apos;,&apos;' + jsonData.TicketList[i].Status + '&apos;);">';
                                    objTrans += '<div class="serv-icon">';
                                    objTrans += '<div class="user-avatar xs">' + jsonData.TicketList[i].TicketTitle.substring(0, 2).toLocaleUpperCase() + '</div>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="nk-support-content">';
                                    objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.TicketList[i].CreatedDateDt + '</span>';
                                    objTrans += '<div class="title">';
                                    objTrans += '<span class="timeline-title mb-0">' + jsonData.TicketList[i].TicketTitle + '<br/>';
                                    if (jsonData.TicketList[i].TransactionId != "") {
                                        objTrans += '<span class="fs-12px">Transaction Id/Reference No. : ' + jsonData.TicketList[i].TransactionId + '</span>';
                                    }
                                    objTrans += '<div class="d-flex align-items-center fw-normal">';
                                    objTrans += '<span class="fs-12px">' + jsonData.TicketList[i].CategoryName + '</span>';
                                    objTrans += '</div>';
                                    objTrans += '</span>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="d-flex mt-1">';
                                    objTrans += '<p class="d-flex align-items-center fs-12px line-height-1 mt-0 mr-2 text-danger">Closed</p>';
                                    objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0">' + jsonData.TicketList[i].Name + '</p>';
                                    objTrans += '</div>';
                                    objTrans += '</div>';
                                    objTrans += '</li>';
                                }
                                else if (status == 0) {
                                    objTrans += '<li class="nk-support-item px-0 cursor-pointer" onclick="SingleTicketLoad(&apos;' + jsonData.TicketList[i].TicketId + '&apos;,&apos;' + jsonData.TicketList[i].Status + '&apos;);">';
                                    objTrans += '<div class="serv-icon">';
                                    objTrans += '<div class="user-avatar xs">' + jsonData.TicketList[i].TicketTitle.substring(0, 2).toLocaleUpperCase() + '</div>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="nk-support-content">';
                                    objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.TicketList[i].CreatedDateDt + '</span>';
                                    objTrans += '<div class="title">';
                                    objTrans += '<span class="timeline-title mb-0">' + jsonData.TicketList[i].TicketTitle + '<br/>';
                                    if (jsonData.TicketList[i].TransactionId != "") {
                                        objTrans += '<span class="fs-12px">Transaction Id/Reference No. : ' + jsonData.TicketList[i].TransactionId + '</span>';
                                    }
                                    objTrans += '<div class="d-flex align-items-center fw-normal">';
                                    objTrans += '<span class="fs-12px">' + jsonData.TicketList[i].CategoryName + '</span>';
                                    objTrans += '</div>';
                                    objTrans += '</span>';
                                    objTrans += '</div>';
                                    objTrans += '<div class="d-flex mt-1">';
                                    if (jsonData.TicketList[i].Status == 1) {
                                        objTrans += '<p class="d-flex align-items-center fs-12px line-height-1 mt-0 mr-2 text-success">Open</p>';
                                    }
                                    else {
                                        objTrans += '<p class="d-flex align-items-center fs-12px line-height-1 mt-0 mr-2 text-danger">Closed</p>';
                                    }
                                    objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0">' + jsonData.TicketList[i].Name + '</p>';
                                    objTrans += '</div>';
                                    objTrans += '</div>';
                                    objTrans += '</li>';
                                }

                            }
                            $('#AjaxLoader').hide();
                        }
                        else {
                            if (!IsValidJson) {
                                $("#dvMessage").html(response);
                                $('#AjaxLoader').hide();
                            }
                            else {
                                //objTrans += '<div class="col-md-12 mb-6">';
                                //objTrans += '<div style="color:red;text-align:center">No Support Ticket Found</div>';
                                //objTrans += '</div>';
                                $('#AjaxLoader').hide();
                            }
                            $("#showmore").css("display", "none");
                        }
                        var skipTrans = parseInt($("#hdnSkip").val()) + 1;
                        $("#AllTickets").append(objTrans);
                        $("#hdnSkip").val(skipTrans);
                    }
                    else if (response == "Invalid Request") {
                        window.location.href = "/MyPayUserLogin";
                    }
                    else {
                        $("#dvMsg").html("Something went wrong. Please try again later.");
                        $('#AjaxLoader').hide();
                        return false;
                    }
                },
                failure: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                }
            });
        }, 100);
}

function AllTicketTabClickrecord() {
    $("#AllTickets").html('');
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    var status = $('#hdnselectedtab').val();
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    AllTicketTabClick(status);


}

function GetAllTickets() {
    $("#AllTickets").html('');
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    $('#searchticks').val('');
    AllTicketTabClick(0);
    $('#hdnselectedtab').val(0);

}

function AllTicketsClick() {
    $("#AllTickets").html('');
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    $('#searchticks').val('');
    AllTicketTabClick(0);
    $('#hdnselectedtab').val(0);

}

function OpenTicketsClick() {
    $("#AllTickets").html('');
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    $('#searchticks').val('');
    AllTicketTabClick(1);
    $('#hdnselectedtab').val(1);
    $('#searchticks').html('');
}

function ClosedTicketsClick() {
    $("#AllTickets").html('');
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    $('#searchticks').val('');
    AllTicketTabClick(2);

    $('#hdnselectedtab').val(2);

}

function SingleTicketLoad(Id, Status) {

    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $("#hdnstatus").val(Status);
    $('#txtMessage').val('');
    $(".uploadimagetext").html('');
    $("#hdnfilename").val('');
    $("#hdnimage").val('');
    $("#ContentPlaceHolder1_hdnimage").val('');
    $("#ContentPlaceHolder1_hdnfilename").val('');
    /* $('#imageupload').val('');*/
    //$('#imageupload').attr('value', '');
    $('#imageupload').val(null);
    $("#NewTicketTab").hide();
    $("#AllTicketTab").hide();
    $("#SingleTicketTab").show();
    $("#hdnticketid").val(Id);

    if (Status == 1) {
        $("#dvticketreply").show();
    }
    else {
        $("#dvticketreply").hide();
    }

    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserGrievanceReplyCount",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Id":"' + Id + '"}',
                success: function (response) {
                    if (response != null) {
                        debugger;
                        var replycount = response;
                        if (replycount > 10) {
                            if ($('#hdnSingleSkip').val() == "-1") {
                                if (replycount != 0 && replycount >= 10) {
                                    debugger;
                                    var remainder = replycount % 10;
                                    replycount = replycount / 10;
                                    var count = Math.floor(replycount);
                                    //var arr = count.split('.');
                                    if (remainder == 0) {
                                        count = count - 1;
                                    }
                                    $('#hdnSingleSkip').val(count);
                                }
                            }
                        }
                        else {
                            $('#hdnSingleSkip').val(0);
                        }
                        debugger;
                        $.ajax({
                            type: "POST",
                            url: "/MyPayUser/MyPayUserSingleGrievance",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            data: '{"Id":"' + Id + '","Take":"' + $("#hdnSingleTake").val() + '","Skip":"' + $("#hdnSingleSkip").val() + '"}',
                            success: function (response) {
                                if (response != null) {
                                    debugger;
                                    var jsonData;
                                    var objTrans = '';
                                    var IsValidJson = false;
                                    try {
                                        jsonData = $.parseJSON(response);
                                        EventsJson = jsonData;
                                        var IsValidJson = true;
                                        if ($("#hdnSingleSkip").val() > 0) {
                                            $("#showmoreReply").css("display", "block");
                                            //if ($("#hdnSingleSkip").val() > 0) {
                                            //    $("#nomoreSinglerecords").css("display", "block");
                                            //    $("#showmoreReply").css("display", "none");
                                            //}
                                            //else {
                                            //    $("#showmoreReply").css("display", "block");
                                            //}
                                        }
                                        else {
                                            $("#nomoreSinglerecords").css("display", "block");
                                            $("#showmoreReply").css("display", "none");
                                        }
                                    }
                                    catch (err) {

                                    }

                                    //$("#ticketreply").html("");
                                    if (jsonData != null && jsonData.TicketReplyList != null && jsonData.TicketReplyList.length > 0) {
                                        debugger;
                                        var objTicketDetail = '';

                                        objTicketDetail += '<li class="px-0 cursor-pointer Status">';
                                        objTicketDetail += '<div>';
                                        objTicketDetail += '<label> <b>Ticket ID: &nbsp</b></label>';
                                        objTicketDetail += '<span class="timeline-title mb-0"><b>' + jsonData.TicketReplyList[0].TicketId + '</b><br />';
                                        if (Status == 1) {
                                            objTicketDetail += '<p class="d-flex align-items-center fs-12px line-height-1 mt-0 mr-2 text-success">Open</p>';
                                            $("#dvticketreply").show();
                                        }
                                        else {
                                            objTicketDetail += '<p class="d-flex align-items-center fs-12px line-height-1 mt-0 mr-2 text-danger">Closed</p>';
                                            $("#dvticketreply").hide();
                                        }

                                        objTicketDetail += '<div class="d-flex align-items-right fw-normal">';
                                        objTicketDetail += '</div>';
                                        objTicketDetail += '</div>';
                                        objTicketDetail += '</li>';
                                        //objTicketDetail += '<a href="javacript:void(0);" id="showmoreReply" onclick="ShowMoreReply();" style="display: none;" class="btn btn-primary btn-inline-block w-auto">Show Older Msgs</a>';

                                        $("#dvticketdetail").html(objTicketDetail);

                                        for (var i = 0; i < jsonData.TicketReplyList.length; i++) {
                                            if (jsonData.TicketReplyList[i].IsAdmin) {
                                                objTrans += '<li class="nk-support-item px-0 cursor-pointer adminmsg">';
                                                objTrans += '<div class="nk-support-content">';
                                                objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.TicketReplyList[i].Name + '</span>';
                                                objTrans += '<div class="title">';
                                                objTrans += '<span class="timeline-title mb-0">' + jsonData.TicketReplyList[i].Message + '</br>';
                                                if (jsonData.TicketReplyList[i].AttachFile.includes(".jpeg") || jsonData.TicketReplyList[i].AttachFile.includes(".jpg") || jsonData.TicketReplyList[i].AttachFile.includes(".png") || jsonData.TicketReplyList[i].AttachFile.includes(".gif") || jsonData.TicketReplyList[i].AttachFile.includes(".bmp")) {
                                                    objTrans += '<a target="_blank" href="' + jsonData.TicketReplyList[i].AttachFile + '"><img style="width:40px; height:40px;" src="' + jsonData.TicketReplyList[i].AttachFile + '"/></a>';
                                                }
                                                else {
                                                    objTrans += '';
                                                }
                                                objTrans += '<div class="d-flex align-items-center fw-normal">';
                                                objTrans += '<span class="fs-12px">' + jsonData.TicketReplyList[i].CreatedDateDt + '</span>';
                                                objTrans += '</div>';
                                                objTrans += '</span>';
                                                objTrans += '</div>';
                                                objTrans += '</div>';
                                                objTrans += '</li>';
                                            }

                                            else if (!jsonData.TicketReplyList[i].IsAdmin) {

                                                objTrans += '<li class="nk-support-item px-0 cursor-pointer customermsg">';
                                                objTrans += '<div class="nk-support-content">';
                                                objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.TicketReplyList[i].Name + '</span>';
                                                objTrans += '<div class="title">';
                                                objTrans += '<span class="timeline-title mb-0">' + jsonData.TicketReplyList[i].Message + '</br>';
                                                if (jsonData.TicketReplyList[i].AttachFile.includes(".jpeg") || jsonData.TicketReplyList[i].AttachFile.includes(".jpg") || jsonData.TicketReplyList[i].AttachFile.includes(".png") || jsonData.TicketReplyList[i].AttachFile.includes(".gif") || jsonData.TicketReplyList[i].AttachFile.includes(".bmp")) {
                                                    objTrans += '<a target="_blank" href="' + jsonData.TicketReplyList[i].AttachFile + '"><img style="width:40px; height:40px;" src="' + jsonData.TicketReplyList[i].AttachFile + '"/></a>';

                                                }
                                                else {
                                                    objTrans += '';
                                                }
                                                objTrans += '<div class="d-flex align-items-center fw-normal">';
                                                objTrans += '<span class="fs-12px">' + jsonData.TicketReplyList[i].CreatedDateDt + '</span>';
                                                objTrans += '</div>';
                                                objTrans += '</span>';
                                                objTrans += '</div>';
                                                objTrans += '</div>';
                                                objTrans += '</li>';
                                            }
                                        }
                                        $('#AjaxLoader').hide();
                                    }
                                    else {
                                        if (!IsValidJson) {
                                            $("#dvMessage").html(response);
                                            $('#AjaxLoader').hide();
                                        }
                                        else {
                                            //objTrans += '<div class="col-md-12 mb-6">';
                                            //objTrans += '<div style="color:red;text-align:center">No Message Found</div>';
                                            //objTrans += '</div>';
                                            $('#AjaxLoader').hide();
                                        }
                                        $("#showmoreReply").css("display", "none");
                                    }
                                    debugger;
                                    var refreshTicketReplyData = objTrans + $("#ticketreply").html();

                                    $("#ticketreply").html(refreshTicketReplyData);
                                    var skipTrans = parseInt($("#hdnSingleSkip").val()) - 1;
                                    $("#hdnSingleSkip").val(skipTrans);

                                }
                                else if (response == "Invalid Request") {
                                    window.location.href = "/MyPayUserLogin";
                                }
                                else {
                                    $("#dvMsg").html("Something went wrong. Please try again later.");
                                    $('#AjaxLoader').hide();
                                    return false;
                                }
                            },
                            failure: function (response) {
                                $('#AjaxLoader').hide();
                                JsonOutput = (response.responseText);
                            },
                            error: function (response) {
                                $('#AjaxLoader').hide();
                                JsonOutput = (response.responseText);
                            }
                        });

                    }
                    else if (response == "Invalid Request") {
                        window.location.href = "/MyPayUserLogin";
                    }
                    else {
                        $("#dvMsg").html("Something went wrong. Please try again later.");
                        $('#AjaxLoader').hide();
                        return false;
                    }
                },
                failure: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                },
                error: function (response) {
                    $('#AjaxLoader').hide();
                    JsonOutput = (response.responseText);
                }
            });

        }, 100);
}

function GoBack() {
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    $("#NewTicketTab").hide();
    $("#AllTicketTab").show();
    $("#SingleTicketTab").hide();
    AllTicketsClick();
}

function SubmitGrievance() {
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    var CategoryId = $("#Category :selected").val();
    var CategoryName = $("#Category :selected").text();
    var Subject = $("#subject").val();
    var TxnId = $("#txnid").val();
    var Description = $("#txtIssueDescription").val();
    var filename = $("#hdnimage").val();
    $("#successmsg").html("");
    $("#errormsg").html("");
    if (CategoryId == "" || CategoryId == "0") {
        $("#errormsg").html("Please select Category");
        return false;
    }
    else if (Subject == "") {
        $("#errormsg").html("Please enter Subject");
        return false;
    }
    else if (Description == "") {
        $("#errormsg").html("Please enter Description");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserSubmitGrievance",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"CategoryId":"' + CategoryId + '","CategoryName":"' + CategoryName + '","Subject":"' + Subject + '","TxnId":"' + TxnId + '","Description":"' + Description + '","filename":"' + filename + '"}',
                    success: function (response) {
                        $("#successmsg").html("");
                        $("#errormsg").html("");
                        if (response == "success") {
                            /*$("#Grievance").modal("hide");*/
                            /* $("#successmsg").html("Your ticket has been submitted. Our team will get back to you within 12 working hour.");*/
                            /*  $('nav-link active').removeClass('active');*/

                            $("#subject").val("");
                            $("#txnid").val("");
                            $("#txtIssueDescription").val("");
                            $("#Category").val("0");
                            $("#hdnimage").val("");
                            $(".uploadimagetext").html("");
                            $('#AjaxLoader').hide();
                            $("#PopupMessage").modal();
                            return false;
                        }
                        else if (response == "Session Expired") {
                            alert('Logged in from another device.');
                            window.location.href = "/MyPayUserLogin/Index";
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(response);
                        }
                        else {

                            $('#AjaxLoader').hide();
                            $("#feedback").modal("hide");
                            $("#txnFeedbackMsg").html(response);
                            $("#DivFeedbackMessage").modal("show");
                        }
                    },

                    failure: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response.responseText);
                    },
                    error: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response.responseText);
                    }
                });
            }, 100);

    }

}
$("#btnMessageVerify").click(function () {
    location.reload();
});

function SubmitReply() {
    $("#ticketreply").html("");
    $('#hdnSingleTake').val(10);
    $('#hdnSingleSkip').val(-1);
    $("#nomorerecords").hide();
    $("#nomoreSinglerecords").hide();
    var Status = $("#hdnstatus").val();
    var Message = $("#txtMessage").val();
    var TicketId = $("#hdnticketid").val();
    var filename = $("#hdnimage").val();
    $("#successmsg").html("");
    $("#errormsg").html("");
    if (Message != "" || filename != "") {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserSubmitReply",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"Message":"' + Message + '","TicketId":"' + TicketId + '","filename":"' + filename + '"}',
                    success: function (response) {

                        $("#successmsg").html("");
                        $("#errormsg").html("");
                        if (response == "success") {
                            $("#successmsg").html("");
                            SingleTicketLoad(TicketId, Status);
                            $("#txtMessage").val("");
                            $("#hdnimage").val("");
                            $(".uploadimagetext").html("");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else if (response == "Session Expired") {
                            alert('Logged in from another device.');
                            window.location.href = "/MyPayUserLogin/Index";
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html(response);
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#feedback").modal("hide");
                            $("#txnFeedbackMsg").html(response);
                            $("#DivFeedbackMessage").modal("show");
                        }
                    },
                    failure: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response.responseText);
                    },
                    error: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response.responseText);
                    }
                });
            }, 100);
    }
    else {
        $("#errormsg").html("Please enter Message or upload attachment");
    }
}

function UploadAjaxImage(obj, foldername, divid, size, nocheckfile) {
    debugger;
    var hdnimage = $("#hdnimage").val();
    if (typeof (hdnimage) == "undefined") {
        hdnimage = $("#ContentPlaceHolder1_hdnimage").val();
    }
    var hdnfilename = $("#hdnfilename").val();
    if (typeof (hdnfilename) == "undefined") {
        hdnfilename = $("#ContentPlaceHolder1_hdnfilename").val();
    }
    //var splitimage;
    //if (hdnimage.indexOf(',') > -1) {
    //    splitimage = hdnimage.toString().split(',');
    //    if (splitimage.length == 1) {
    //        alert("Maximum limit of images is 1 and you have already uploaded 1 image");
    //        $(obj).replaceWith($(obj).val('').clone(true));
    //        $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
    //        $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
    //    }
    //}

    var fd = new FormData();
    var file = $(obj)[0].files[0];
    var filename = file.name;
    if (file) {
        if (nocheckfile == 1) {
            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray(file.name.split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(obj).replaceWith($(obj).val('').clone(true));
                $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
                return false;
            }
        }
        if (parseInt(file.size / 1000) > size) {
            alert("Allowed file size exceeded. (Max. " + size + " KB)");
            $(obj).replaceWith($(obj).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            return false;
        }
    }
    fd.append("file", file);
    fd.append("foldername", foldername);
    fd.append("Method", "UploadImage");
    $.ajax({
        url: '/UploadImage.ashx',
        type: "POST",
        processData: false,
        contentType: false,
        data: fd,
        success: function (result) {
            debugger;
            var splitstr = result.toString().split('!@!');
            if (result.indexOf('Success') > -1) {
                debugger;
                if (splitstr[0] == "Success") {
                    debugger;
                    /* if (hdnimage == "") {*/
                    hdnimage = splitstr[1];
                    hdnfilename = filename;
                    /* }*/
                    //else {
                    //    hdnimage = hdnimage + "," + splitstr[1];
                    //    hdnfilename = hdnfilename + "|" + filename;
                    //}
                    $(".uploadimagetext").html(hdnfilename);
                    $("#hdnfilename").val(hdnfilename);
                    $("#hdnimage").val(hdnimage);
                    $("#ContentPlaceHolder1_hdnimage").val(hdnimage);
                    $("#ContentPlaceHolder1_hdnfilename").val(hdnfilename);
                    $("progress").hide();
                    //PreviewImage(obj, divid, 1000);
                }
                else {
                    debugger;
                    $("progress").hide();
                    //$("#hdnimage").val("");
                    //$("#ContentPlaceHolder1_hdnimage").val("");
                    alert(splitstr[0]);
                    $(obj).replaceWith($(obj).val('').clone(true));
                    $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
                    $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
                }
            }
            else {
                debugger;
                //$("#hdnimage").val("");
                //$("#ContentPlaceHolder1_hdnimage").val("");
                $("progress").hide();
                alert(splitstr[0]);
                $(obj).replaceWith($(obj).val('').clone(true));
                $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
            }
            return false;
        },
        xhr: function () {
            var fileXhr = $.ajaxSettings.xhr();
            if (fileXhr.upload) {
                $("progress").show();
                fileXhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        $(".fileProgress").attr({
                            value: e.loaded,
                            max: e.total
                        });
                    }
                }, false);
            }
            return fileXhr;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText);
            return false;
        }
    });
}

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            $("#NewTicketTab").hide();
            $("#AllTicketTab").show();
            $("#SingleTicketTab").hide();
            var status = $('#hdnselectedtab').val();
            AllTicketTabClick(status);
        }, 10);
}

function ShowMoreReply() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            debugger;
            $("#NewTicketTab").hide();
            $("#AllTicketTab").hide();
            $("#SingleTicketTab").show();
            var TicketId = $("#hdnticketid").val();
            var Status = $("#hdnstatus").val();
            SingleTicketLoad(TicketId, Status);
        }, 10);
}

