var skip = 0;
var newsData = '';


function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            NotificationsLoad();
        }, 10);
}

function ShowMoreFund() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            FundLoad();
        }, 10);
}

function NotificationsLoad() {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserNotificationList",
            data: '{"Take":"' + $("#hdnNotificationTake").val() + '","Skip":"' + $("#hdnNotificationSkip").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                var objNotifications = '';
                if (response != null) {
                    debugger;
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        IsValidJson = true;
                        if (jsonData.data.length < 10) {
                            if ($("#hdnNotificationSkip").val() > 0) {
                                $("#dvMessage").html("No More Records.");
                                $("#showmore").css("display", "none");
                            }
                        }
                        else {
                            $("#showmore").css("display", "block");
                        }
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        for (var i = 0; i < jsonData.data.length; ++i) {

                            objNotifications += '<li class="timeline-item">';
                            objNotifications += '<div class="timeline-status bg-success"></div>';
                            objNotifications += '<div class="timeline-date">' + jsonData.data[i].CreatedDatedt.split(" ")[0] + ' <em class="icon ni ni-alarm-alt d-none d-sm-block"></em></div>';
                            objNotifications += '<div class="timeline-data">';
                            objNotifications += '<h6 class="timeline-title">' + jsonData.data[i].Title + ' </h6>';
                            objNotifications += '<div class="timeline-des">';
                            objNotifications += '<p>' + jsonData.data[i].NotificationMessage + '  </p>';
                            objNotifications += '<span class="time">' + jsonData.data[i].CreatedDatedt.split(" ")[1] + ' ' + jsonData.data[i].CreatedDatedt.split(" ")[2] + ' </span>';
                            objNotifications += '</div>';
                            objNotifications += '</div>';
                            objNotifications += '</li>';
                        }
                    }
                    else {
                        if (!IsValidJson) {
                            $("#dvMessage").html(response);
                        }
                        $("#showmore").css("display", "none");
                    }
                    $("#ulistNotifications").append(objNotifications);
                    var skipNotification = parseInt($("#hdnNotificationSkip").val()) + 1;
                    $("#hdnNotificationSkip").val(skipNotification);
                    $('#AjaxLoader').hide();
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
    }, 10);
}

function FundRequest() {
    $("#hdnFundSkip").val("0");
    FundLoad();
}

function FundLoad() {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserLoadFund",
            data: '{"Take":"' + $("#hdnFundTake").val() + '","Skip":"' + $("#hdnFundSkip").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                var objFund = '';
                if (response != null) {
                    debugger;
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                        $("#dvMessage").html("");
                        if (jsonData['ReponseCode'] == '3') {
                            $("#dvMessage").html(jsonData['Message']);
                            $("#showmorefund").css("display", "none");
                            $('#dvFundDisplay').html("");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        if (jsonData.RequestFundList.length < 10) {
                            if ($("#hdnFundSkip").val() > 0) {
                                $("#dvMessage").html("No More Records.");
                                $("#showmorefund").css("display", "none");
                            }
                            else {
                                //$("#showmorefund").css("display", "block");
                            }
                        }
                        else {
                            //$("#showmorefund").css("display", "block");
                        }
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.RequestFundList != null && jsonData.RequestFundList.length > 0) {
                        for (var i = 0; i < jsonData.RequestFundList.length; ++i) {

                            objFund += '<li class="timeline-item">';
                            var IsUserRequest = ($("#spnUserContactNumber").html().trim() != jsonData.RequestFundList[i].SenderPhoneNumber);
                            if (jsonData.RequestFundList[i].StatusName == 'Pending') {
                                objFund += '<div class="timeline-status bg-warning"></div>';
                                objFund += '<div class="timeline-date">' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[0] + ' <em class="icon ni ni-alarm-alt d-none d-sm-block"></em></div>';
                                objFund += '<div class="timeline-data">';
                                if (IsUserRequest) {
                                    objFund += '<h6 class="timeline-title">Funds Request Pending</h6>';
                                }
                                else {
                                    objFund += '<h6 class="timeline-title">Request Sent</h6>';
                                }
                                objFund += '<div class="timeline-des">';
                                if (IsUserRequest) {
                                    objFund += '<p>You got a fund request from ' + jsonData.RequestFundList[i].SenderMemberName + ' of amount Rs. ' + jsonData.RequestFundList[i].Amount + ' </p>';
                                }
                                else {
                                    objFund += '<p>You sent a fund request to ' + jsonData.RequestFundList[i].ReceiverMemberName + ' of Rs. ' + jsonData.RequestFundList[i].Amount + ' </p>';
                                }
                                objFund += '<span class="time">' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[1] + ' ' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[2] + ' </span>';

                                if (IsUserRequest) {
                                    objFund += '<div style="margin:10px 0px 10px 0px ;"><a href="javascript:void(0)" style="color:red; font-weight:bold; margin:5px;" onclick="TransferByPhoneReject(' + jsonData.RequestFundList[i].Id + ' )">DECLINE</a>   <a href="javascript:void(0)" style="color:green; font-weight:bold;margin:5px;" onclick="TransferByPhoneAccept(' + jsonData.RequestFundList[i].Id + ')">ACCEPT</a></div> ';
                                }
                            }
                            else if (jsonData.RequestFundList[i].StatusName == 'Rejected') {
                                objFund += '<div class="timeline-status bg-danger is-outline"></div>';
                                objFund += '<div class="timeline-date">' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[0] + ' <em class="icon ni ni-alarm-alt d-none d-sm-block"></em></div>';
                                objFund += '<div class="timeline-data">';
                                objFund += '<h6 class="timeline-title">Funds Request Rejected</h6>';
                                objFund += '<div class="timeline-des">';
                                objFund += '<p>Fund request of Rs. ' + jsonData.RequestFundList[i].Amount + ' has been ' + jsonData.RequestFundList[i].StatusName +'  by ' + jsonData.RequestFundList[i].ReceiverMemberName + '</p>';
                                objFund += '<span class="time">' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[1] + ' ' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[2] + ' </span>';

                            }
                            else if (jsonData.RequestFundList[i].StatusName == 'Accepted') {
                                objFund += '<div class="timeline-status bg-success"></div>';
                                objFund += '<div class="timeline-date">' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[0] + ' <em class="icon ni ni-alarm-alt d-none d-sm-block"></em></div>';
                                objFund += '<div class="timeline-data">';
                                objFund += '<h6 class="timeline-title">Funds Tranferred</h6>';
                                objFund += '<div class="timeline-des">';
                                if (IsUserRequest) {
                                    objFund += '<p>Fund request of Rs. ' + jsonData.RequestFundList[i].Amount + ' from ' + jsonData.RequestFundList[i].SenderMemberName + ' has been ' + jsonData.RequestFundList[i].StatusName + '  by you </p>';
                                }
                                else {
                                    objFund += '<p>Fund request of Rs. ' + jsonData.RequestFundList[i].Amount + ' from you has been ' + jsonData.RequestFundList[i].StatusName + '  by ' + jsonData.RequestFundList[i].ReceiverMemberName + '  </p>';
                                }
                                objFund += '<span class="time">' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[1] + ' ' + jsonData.RequestFundList[i].CreatedDatedt.split(" ")[2] + ' </span>';

                            }
                        }
                    }
                    else {
                        if (!IsValidJson) {
                            $("#dvMessage").html(response);
                        }
                        $("#showmorefund").css("display", "none");
                    }
                    var skipFund = parseInt($("#hdnFundSkip").val()) + 1;
                    $("#ulistFundRequest").html(objFund);
                    $("#hdnFundSkip").val(skipFund);
                    $('#AjaxLoader').hide();
                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            }
        });
    }, 20);
}
function loadNews() {
    $("#hdnNewsSkip").val("0");
    News();
}
function ShowMoreNews() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            News();
        }, 10);
}
function News() {
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserLoadNews",
            data: '{"Take":"' + $("#hdnNewsTake").val() + '","Skip":"' + $("#hdnNewsSkip").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                debugger;
                var objNews = '';
                if (response != null) {

                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        newsData = jsonData;
                        var IsValidJson = true;
                        $("#dvMessage").html("");
                        if (jsonData != null && jsonData.Notifications != null && jsonData.Notifications.length > 0) {
                            for (var i = 0; i < jsonData.Notifications.length; ++i) {
                                if (jsonData.Notifications[i].NotificationImage != null && jsonData.Notifications[i].NotificationImage != "") {

                                    objNews += '<div class="row"><a onclick="RedirectPage(&apos;' + jsonData.Notifications[i].NotificationRedirectType + '&apos;);"><img class="w320 mt-3" width="440" height="200" src="' + jsonData.Notifications[i].NotificationImage + '"></a></div>';
                                    objNews += '<div class="row font-weight-bold mt-1">' + jsonData.Notifications[i].Title + '<a href="javascript:void(0)" class="ml-auto" style="color:red; font-weight:bold;" onclick="ViewMore(' + jsonData.Notifications[i].Id + ')">View More</a></div>';
                                    objNews += '<div class="row mt-1">' + jsonData.Notifications[i].NotificationMessage.substr(0, 250) + '</div>';
                                    objNews += '<div class="row mt-1 pb-2 border-bottom">' + jsonData.Notifications[i].SentDate + '</div>';

                                }
                            }

                        }
                        if (jsonData.Notifications.length < 10) {
                            if ($("#hdnNewsSkip").val() > 0) {
                                $("#dvMessage").html("No More Records.");
                                $("#showMoreNews").css("display", "none");
                            }
                            else {
                                $("#showMoreNews").css("display", "block");
                            }
                        }
                        else {
                            $("#showMoreNews").css("display", "block");
                        }
                    }
                    catch (err) {

                    }

                    var skipNews = parseInt($("#hdnNewsSkip").val()) + 1;
                    $("#ulistNews").html(objNews);
                    $("#hdnNewsSkip").val(skipNews);
                    $('#AjaxLoader').hide();
                }
                else {
                    $("#dvMsg").html("Something went wrong. Please try again later.");
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMsg").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            },
            error: function (response) {
                $("#dvMsg").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            }
        });
    }, 20);
}

function RedirectPage(RedirectType) {
    $("#dvMessage").html("");
    debugger;
    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        $.ajax({
            type: "POST",
            url: "/MyPayUser/GetProvidersMyPayWebURL",
            data: '{"RedirectType":"' + RedirectType + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response != null && response!=="") {
                    window.location.href = '' + response + '';
                    $('#AjaxLoader').hide();
                }
                else {
                    alert("Redirection link not found for news.");
                    $('#AjaxLoader').hide();
                    return false;
                }
            },
            failure: function (response) {
                $("#dvMessage").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            },
            error: function (response) {
                $("#dvMessage").html(response.responseText);
                $('#AjaxLoader').hide();
                return false;
            }
        });
    }, 100);
}

function ViewMore(Id) {
    $("#showMoreNews").css("display", "none");
    $('#ulistNews').hide();
    $('#NewsDetail').show();
    var selectedNews = newsData.Notifications.filter((x) => { return x.Id === Id });
    var objNews = '';
    objNews += '<div class="row"><img class="w320 mt-3" width="440" height="250" src="' + selectedNews[0].NotificationImage + '"> <a href="javascript:void(0)" onclick="Back();" class="ml-auto color-black"><em class="icon ni ni-arrow-left"></em> Back</a></div>';
    objNews += '<div class="row font-weight-bold mt-1">' + selectedNews[0].Title + '</div>';
    objNews += '<div class="row mt-1">' + selectedNews[0].NotificationMessage + '</div>';
    objNews += '<div class="row mt-1 pb-2">' + selectedNews[0].SentDate + '</div>';
    $("#NewsDetail").html('');
    $("#NewsDetail").html(objNews);
    $("html, body").animate({ scrollTop: "0" });
}
function Back() {
    $("#showMoreNews").css("display", "block");
    $('#ulistNews').show();
    $('#NewsDetail').hide();
}
function TransferByPhoneReject(UniqueCustomerId) {
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/TransferByPhoneReject",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"UniqueCustomerId":"' + UniqueCustomerId + '"}',
                success: function (response) {
                    debugger;
                    var arr = $.parseJSON(response);
                    if (arr['ReponseCode'] == "1") {
                        alert(arr['Message']);
                        var skip = $("#hdnFundSkip").val();
                        if (skip !== "0") {
                            skip = parseInt(skip) - 1;
                            $("#hdnFundSkip").val(skip);
                        }
                       
                        FundLoad();
                        $('#AjaxLoader').hide();
                        return false;
                    }
                    else {
                        $("#dvMessage").html(arr['Message']);
                        $('#AjaxLoader').hide();
                        $('#dvMessage').html(arr['responseMessage']);
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
        }, 10);

}



function TransferByPhoneAccept(UniqueCustomerId) {
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/TransferByPhoneAccept",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"UniqueCustomerId":"' + UniqueCustomerId + '"}',
                success: function (response) {
                    debugger;
                    if (response == "success") {
                        window.location.href = "/MyPayUser/MyPayUserSendMoney";
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
        }, 10);

}