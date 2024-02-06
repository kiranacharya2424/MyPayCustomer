
$('.nk-msg-item').on('click', function () {
    $(".ticketmessage").html('');
    $('.nk-msg-item').removeClass('current');
    $(this).addClass('current');
});
$('.nk-msg-item').on('click', function () {
    $(".ticketmessage").html('');
    $('.nk-msg-body').addClass('show-message');
});
$('.profile-toggle').on('click', function () {
    $('.nk-msg-profile').toggleClass('visible');
    $('.nk-msg-body').toggleClass('show-message, profile-shown');
});
$('.nk-msg-head-meta a.btn-trigger').on('click', function () {
    $('.nk-msg-body').removeClass('show-message');
});

$(document).ready(function () {
    $('#hdnSingleTake').val(10);
    $('#hdnSingleSkip').val(-1);
    $('#hdnTabType').val('');
    $(".ticketmessage").html('');
    var querystring = $("#hdnquerystring").val();
    if (querystring == "1") {
        $("#act").removeClass("active");
        $("#open").addClass("active");
        GetTickets(2, 2, 2, 1, 10, 0, "", 0, 0, 0, "", "", "");
        $('#hdnTabType').val(6);

    }
    else {
        GetTickets(1, 2, 2, 1, 10, 0, "", 0, 0, 0, "", "", "");
        $('#hdnTabType').val(1);
    }

});

function GetTickets(isSeen, isAttached, isFavourite, isClosed, take, skip, TicketId, Category, Priority, usertype, fromdate, todate, contactno) {
    $('#hdnSingleTake').val(10);
    $('#hdnSingleSkip').val(-1);
    $(".ticketmessage").html('');
    skip = $('#hdnSkip').val();
    take = $("#hdnTake").val();
    $.ajax({
        url: "/Support/GetTickets",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{ "isSeen":"' + parseInt(isSeen) + '","isFavourite":"' + parseInt(isFavourite) + '","isAttached":"' + parseInt(isAttached) + '","isClosed":"' + parseInt(isClosed) + '","TicketId":"' + TicketId + '","Category":"' + Category + '", "Priority":"' + Priority + '","fromdate":"' + fromdate + '","todate":"' + todate + '","contactno":"' + contactno + '","usertype":"' + parseInt(usertype) + '","take":"' + parseInt(take) + '","skip":"' + parseInt(skip) + '"}',
        success: function (data) {
            if (data.data.length < 10) {

                if ($("#hdnSkip").val() > 0) {
                    $("#nomorerecords").css("display", "block");
                    $("#showmore").css("display", "none");
                }
                else {
                    $("#showmore").css("display", "block");
                }
            }
            else {
                $("#showmore").css("display", "block");
            }
            SetUpTickets(data);
            var skipTrans = parseInt($("#hdnSkip").val()) + 1;
            $("#hdnSkip").val(skipTrans);
            if (data.data != "") {
                $(".ticketmessage").html('');
                $(".nk-msg-item[data-ticketid*='" + data.data[0].TicketId + "']").click();
            }

            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".ticketssidebar").html('');
            alert(XMLHttpRequest.responseText, "danger")
            return false;
        }
    });

    return false;
}

function GetTickets_Reply(TicketId) {

    debugger;
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/Support/SupportReplyCount",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Id":"' + TicketId + '"}',
                success: function (response) {
                    if (response != null) {
                        debugger;
                        if ($('#hdnSingleSkip').val() == "-1") {
                            $(".ticketmessage").html('');
                        }
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
                                $(".ticketmessage").html('');
                            }
                        }
                        else {
                            $('#hdnSingleSkip').val(0);
                        }
                        debugger;
                        $.ajax({
                            url: "/Support/GetTickets_Reply",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: '{"TicketId":"' + TicketId + '","Take":"' + $('#hdnSingleTake').val() + '","Skip":"' + $('#hdnSingleSkip').val() + '"}',
                            success: function (data) {
                                // if (data.d.indexOf('Success') > -1) {
                                if (data != "") {
                                    debugger;
                                    if ($("#hdnSingleSkip").val() > 0) {
                                        $("#showmoreReply").css("display", "block");
                                        $("#nomoreSinglerecords").css("display", "none");
                                    }
                                    else if ($("#hdnSingleSkip").val() == "-1") {
                                        $("#showmoreReply").css("display", "none");
                                        $("#nomoreSinglerecords").css("display", "none");
                                    }
                                    else {
                                        $("#nomoreSinglerecords").css("display", "block");
                                        $("#showmoreReply").css("display", "none");
                                    }
                                    SetupMessage(data);
                                    var skipTrans = parseInt($("#hdnSingleSkip").val()) - 1;
                                    $("#hdnSingleSkip").val(skipTrans);
                                }
                                else {
                                    alert(data.d, "warning");
                                }
                                $('#AjaxLoader').hide();
                                return false;
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(XMLHttpRequest.responseText, "danger")
                                return false;
                            }
                        });
                    }
                    else if (response == "Invalid Request") {
                        window.location.href = "/AdminLogin";
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
            $('#AjaxLoader').hide();
            return false;
        }, 100);
}

function UpdateFavourite(TicketId, status) {
    $.ajax({
        url: "/Support/UpdateFavourite",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{"TicketId":"' + TicketId + '","Status":"' + parseInt(status) + '"}',
        success: function (data) {
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText, "danger")
            return false;
        }
    });

    return false;
}

function MarkClosed() {
    if ($("#hdnStatus").val() == "2") {
        return;
    }
    var TicketId = $("#hdnticketid").val();
    if (TicketId != "") {
        $.ajax({
            url: "/Support/MarkClosed",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{"TicketId":"' + TicketId + '"}',
            success: function (data) {
                //if (data.d.indexOf('Success') > -1) {
                if (data != "") {
                    $('#status').html('CLOSE').addClass('text-danger').removeClass('text-success');
                    alert("Succesfully Closed");
                }
                else {
                    alert(data.d);
                }

                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.responseText, "danger")
                return false;
            }
        });
    }
    else {
        alert("Ticket Id Not Found");
    }
    return false;
}

function SetPriority(priority) {
    var TicketId = $("#hdnticketid").val();
    if (TicketId != "") {
        $.ajax({
            url: "/Support/SetPriority",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{"TicketId":"' + TicketId + '","Priority":"' + parseInt(priority) + '"}',
            success: function (data) {
                //if (data.d.indexOf('Success') > -1) {
                if (data != "") {
                    if (priority == 1) {
                        $('#priority').html('LOW').addClass('text-primary').removeClass('text-success').removeClass('text-danger');
                    }
                    else if (priority == 2) {
                        $('#priority').html('MEDIUM').addClass('text-success').removeClass('text-danger').removeClass('text-primary');
                    }
                    else if (priority == 3) {
                        $('#priority').html('HIGH').addClass('text-danger').removeClass('text-success').removeClass('text-primary');
                    }

                    alert("Priority Successfully Updated");
                }
                else {
                    alert(data.d);
                }

                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.responseText, "danger")
                return false;
            }
        });
    }
    else {
        alert("Ticket Id Not Found");
    }
    return false;
}

function AddTicket(isnote) {
    debugger;
    $('#hdnSingleTake').val(10);
    $('#hdnSingleSkip').val(-1);
    var message = "";
    var TicketId = $("#hdnticketid").val();
    var filename = $("#hdnimage").val();
    if (isnote == "1") {
        message = $("#txtnote").val().replaceAll('"', ' ');
    }
    else {
        message = $("#txtdescription").val().replaceAll('"', ' ');
    }
    if (message == "") {
        alert("Please enter Message", "warning");
    }
    if (TicketId != "") {
        $.ajax({
            url: "/Support/AddTicketDetail",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: '{"TicketId":"' + TicketId + '","message":"' + message + '","filename":"' + filename + '","isnote":"' + isnote + '"}',
            success: function (data) {
                debugger;
                if (data == "success") {
                    alert("Reply Successfully Posted", "success");
                    $("#hdnimage").val("");
                    $("#hdnfilename").val("");
                    $(".uploadimagetext").html("");
                    $("#txtnote").val('');
                    $("#txtdescription").val('');
                    $(".ticketmessage").html('');
                    GetTickets_Reply(TicketId);
                }
                else {
                    alert(data);
                }

                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.responseText, "danger")
                return false;
            }
        });
    }
    else {
        alert("Ticket Id Not Found", "warning");
    }
    return false;
}

function GetUserDetail(ContactNo) {
    $.ajax({
        url: "/Support/GetUserDetail",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: '{"ContactNo":"' + ContactNo + '"}',
        success: function (data) {
            debugger;
            //var json = $.parseJSON(data.d);
            if (data != "") {
                //$.each(json, function (i, categories) {
                $("#viewprofile").attr("href", "/User/UserDetails?MemberId=" + data[0].MemberId);
                $("#vieworders").attr("href", "/AdminTransactions?MemberId=" + data[0].MemberId);
                $("#refid").html(data[0].RefId);
                $(".username").html(data[0].UserName);
                $("#address").html(data[0].Address + "," + data[0].State);
                //$("#userstate").html(json[i].State);
                $("#email").html(data[0].Email);
                $("#phone").html(data[0].Phone);
                $("#platform").html(data[0].Platform);
                $("#Totalticket").html(data[0].TotalTickets);
                $("#CloseTicket").html(data[0].CloseTickets);
                $("#PendingTicket").html(data[0].PendingTickets);
                var name = data[0].UserName.split(' ');
                var abr = "";
                if (name.length >= 2) {
                    abr = name[0].charAt(0) + name[1].charAt(0);
                }
                else {
                    abr = name[0].charAt(0) + name[0].charAt(1);
                }
                $(".usrabr").html(abr.toUpperCase());
                /*});*/
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText, "danger")
            return false;
        }
    });

    return false;
}

function SetupMessage(result) {
    debugger;
    if (result != "") {
        var str = '';
        //$(".ticketmessage").html('');
        for (var i = 0; i < result.data.length; i++) {
            debugger;
            var name = result.data[i].Name.split(' ');
            var abr = "";
            if (name.length >= 2) {
                abr = name[0].charAt(0) + name[1].charAt(0);
            }
            else {
                abr = name[0].charAt(0) + name[0].charAt(1);
            }

            /*str += '<div class="nk-reply-item">';*/
            if (result.data[i].IsAdmin) {
                str += '<div class="admin-nk-reply-item nk-reply-item">';
            }
            else {
                str += '<div class="nk-reply-item">';
            }
            str += '<div class="nk-reply-header">';
            str += '<div class="user-card">';
            str += '<div class="user-avatar sm bg-blue">';
            str += '<span>' + abr.toUpperCase() + '</span>';
            str += '</div>';
            if (result.data[i].IsNote) {
                str += '<div class="user-name">' + result.data[i].Name + '<span> added a note</span></div>';
            }
            else {
                str += '<div class="user-name">' + result.data[i].Name + '</div>';
            }
            str += '</div>';
            str += '<div class="date-time">' + result.data[i].CreatedDateDt + '</div>';
            str += '</div>';
            str += '<div class="nk-reply-body">';
            if (result.data[i].Message == "") {
                str += '<div class="nk-reply-entry">';
            }
            else {
                str += '<div class="nk-reply-entry entry">';
            }
            str += '<p>' + result.data[i].Message + '</p>';
            str += '</div>';
            if (result.data[i].AttachFile != "") {
                var splitimage = result.data[i].AttachFile.toString().split(',');
                $.each(splitimage, function (i, categories) {
                    str += '<div class="attach-files">';
                    str += '<ul class="attach-list">';
                    str += '<li class="attach-item"><a target="_blank" href="/Images/TicketsImages/' + splitimage[i] + '"><em class="icon ni ni-img"></em><span>' + splitimage[i] + '</span></a>';
                    str += '</li>';
                    str += '</ul>';
                    str += '<div class="attach-foot">';
                    str += '<span class="attach-info">1 file attached</span>';
                    str += '</div>';
                    str += '</div>';
                });
            }
            str += '</div>';
            str += '</div>';
            str += '<div class="nk-reply-meta">';
            str += '<div class="nk-reply-meta-info">';
            str += '<strong>' + result.data[i].CreatedDateDt + '</strong>';
            str += ' </div>';
            str += ' </div>';



            $("#ticketid").html(result.data[i].TicketId);
            if (i === result.data.length - 1) {
                $("#lastreply").html(result.data[i].Name);
            }
        }
        var refreshTicketReplyData = str + $(".ticketmessage").html();
        $(".ticketmessage").html(refreshTicketReplyData);
        $('#AjaxLoader').hide();
    }
    else {
        alert("Error! Try Again Later");
    }
}

function SetUpTickets(result) {
    $(".ticketmessage").html('');
    if (result != "") {
        //$(".ticketssidebar").html('');
        for (var i = 0; i < result.data.length; i++) {
            var name = result.data[i].Name.split(" ");
            var abr = "";
            if (name.length >= 2) {
                abr = name[0].charAt(0) + name[1].charAt(0);
            }
            else {
                abr = name[0].charAt(0) + name[0].charAt(1);
            }
            var str = '<div class="nk-msg-item" data-priority="' + result.data[i].Priority + '" data-status="' + result.data[i].Status + '" data-contactno="' + result.data[i].ContactNumber + '" data-id="' + result.data[i].Id + '" data-categoryname="' + result.data[i].CategoryName + '" data-tickettitle="' + result.data[i].TicketTitle + '" data-ticketid="' + result.data[i].TicketId + '" data-txnid="' + result.data[i].TransactionId + '">';
            str += '<div class="nk-msg-media user-avatar">';
            str += '<span>' + abr.toUpperCase() + '</span>';
            str += '</div>';
            str += '<div class="nk-msg-info">';
            str += '<div class="nk-msg-from">';
            str += '<div class="nk-msg-sender">';
            if (result.data[i].Type == 3) {
                str += '<div class="name">' + result.data[i].Name + ' (Agent)</div>';
            }
            else {
                str += '<div class="name">' + result.data[i].Name + ' (User)</div>';
            }

            if (!result.data[i].IsSeen) {
                str += '<div class="lable-tag dot bg-pink"></div>';
            }
            str += '</div>';
            str += '<div class="nk-msg-meta">';
            if (result.data[i].IsAttached) {
                str += '<div class="attchment">';
                str += '<em class="icon ni ni-clip-h"></em>';
                str += '</div>';
            }
            str += '<div class="date">' + result.data[i].CreatedDateDt + '</div>';
            str += '</div>';
            str += '</div>';
            str += '<div class="nk-msg-context">';
            str += '<div class="nk-msg-text">';
            str += '<h6 class="title">' + result.data[i].TicketTitle + '</h6>';
            str += '<p>' + result.data[i].UpdatedMessage + '</p>';
            str += '</div>';
            str += '<div class="nk-msg-lables">';
            if (!result.data[i].IsFavourite) {
                str += '<div class="asterisk" data-ticketid="' + result.data[i].TicketId + '">';
                str += '<a href="javascript:void(0);"><em class="asterisk-off icon ni ni-star"></em><em class="asterisk-on icon ni ni-star-fill"></em></a>';
                str += '</div>';
            }
            else {
                str += '<div class="asterisk" data-ticketid="' + result.data[i].TicketId + '">';
                str += '<a class="active" href="javascript:void(0);" data-id="' + result.data[i].Id + '"><em class="asterisk-off icon ni ni-star"></em><em class="asterisk-on icon ni ni-star-fill"></em></a>';
                str += '</div>';
            }

            str += '</div>';
            str += '</div>';
            str += '</div>';
            str += '</div>';
            $(".ticketssidebar").append(str);

        }
        $(".nk-msg-item").click(function () {
            debugger;
            $(".ticketmessage").html('');
            $('.tickettitle').html($(this).attr("data-tickettitle") + "(" + $(this).attr("data-ticketid") + ")");
            $('.category').html($(this).attr("data-categoryname"));
            var txnid = $(this).attr("data-txnid");
            $('.ticketTxnid').html("");
            if (txnid != "") {
                $('.ticketTxnid').html("Transaction Id/Reference Number: " + $(this).attr("data-txnid"));
            }
            $(this).find('.bg-pink').remove();
            if ($(this).attr("data-status") == "2") {
                $('#status').html('CLOSE').addClass('text-danger').removeClass('text-success');
                $('#reply_form_detail').hide();
                $('#btn_Close').text("Closed");

            }
            else {
                $('#status').html('OPEN').addClass('text-success').removeClass('text-danger');
                $('#reply_form_detail').show();
                $('#btn_Close').text("Mark as Closed");
            }

            if ($(this).attr("data-priority") == "1") {
                $('#priority').html('LOW').addClass('text-primary').removeClass('text-success').removeClass('text-danger');
            }
            else if ($(this).attr("data-priority") == "2") {
                $('#priority').html('MEDIUM').addClass('text-success').removeClass('text-danger').removeClass('text-primary');
            }
            else if ($(this).attr("data-priority") == "3") {
                $('#priority').html('HIGH').addClass('text-danger').removeClass('text-success').removeClass('text-primary');
            }
            debugger;
            $("#hdnticketid").val($(this).attr("data-ticketid"));
            $("#hdnStatus").val($(this).attr("data-status"));
            GetTickets_Reply($(this).attr("data-ticketid"));
            GetUserDetail($(this).attr("data-contactno"));
            $("#ticketid").html($("#hdnticketid").val());
        });
        $(".asterisk").click(function () {
            if ($(this).find('a').hasClass('active')) {
                $(this).find('a').removeClass('active');
                UpdateFavourite($(this).attr("data-ticketid"), 0);
            }
            else {
                $(this).find('a').addClass('active');
                UpdateFavourite($(this).attr("data-ticketid"), 1);
            }

        });
    }
    else {
        alert("Error! Try Again Later");
    }
}

function CheckMethod(obj, type) {
    $('.nk-msg-menu-item').removeClass('active');
    $(obj).parent().addClass('active');
    $('#hdnTake').val(10);
    $('#hdnSkip').val(0);
    $(".ticketssidebar").html('');
    $("#nomorerecords").css("display", "none");
    $("#showmore").css("display", "none");
    $('#hdnTabType').val(type);
    if (type == 1) {
        GetTickets(1, 2, 2, 1, 10, 0, "", 0, 0, 0, "", "", "");
    }
    else if (type == 2) {
        GetTickets(2, 2, 2, 2, 10, 0, "", 0, 0, 0, "", "", "");
    }
    else if (type == 3) {
        GetTickets(2, 2, 1, 0, 10, 0, "", 0, 0, 0, "", "", "");
    }
    else if (type == 4) {
        GetTickets(2, 2, 2, 0, 10, 0, "", 0, 0, 0, "", "", "");
    }
    //else if (type == 5) {
    //    GetTickets(2, 2, 2, 0, 10, 0, "", 1);
    //}
    else if (type == 6) {
        GetTickets(2, 2, 2, 1, 10, 0, "", 0, 0, 0, "", "", "");
    }
    return false;
}


function PreviewImage(input, divid, size) {

    if (input.files && input.files[0]) {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray(input.files[0].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
            //ViewMessage("Only formats are allowed : " + fileExtension.join(', '), "warning", '');
            alert("Only formats are allowed : " + fileExtension.join(', '));
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Content/assets/Images/noimageblank.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Content/assets/Images/noimageblank.png");
            return false;
        }
        if (parseInt(input.files[0].size / 1000) > size) {
            //ViewMessage("Allowed file size exceeded. (Max. " + size + " KB)", "warning", '');
            alert("Allowed file size exceeded. (Max. " + size + " KB)");
            $(input).replaceWith($(input).val('').clone(true));
            $('#target' + divid).attr("src", "/Content/assets/Images/noimageblank.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Content/assets/Images/noimageblank.png");
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            debugger;
            if (input.files[0].name.split('.').pop().toLowerCase() != "pdf" && input.files[0].name.split('.').pop().toLowerCase() != "txt") {
                $('#target' + divid).attr('src', e.target.result);
                $('#ContentPlaceHolder1_target' + divid).attr("src", e.target.result);
            }
            else {
                $('#target' + divid).attr("src", "/Content/assets/Images/noimageblank.png");
                $('#ContentPlaceHolder1_target' + divid).attr("src", "/Content/assets/Images/noimageblank.png");
            }
        }
        reader.readAsDataURL(input.files[0]);
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
    var splitimage;
    if (hdnimage.indexOf(',') > -1) {
        splitimage = hdnimage.toString().split(',');
        if (splitimage.length == 5) {
            alert("Maximum limit of images is 5 and you have already uploaded 5 images");
            $(obj).replaceWith($(obj).val('').clone(true));
            $('#target' + divid).attr("src", "/Admin/Images/no-image.png");
            $('#ContentPlaceHolder1_target' + divid).attr("src", "/Admin/Images/no-image.png");
        }
    }

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

            var splitstr = result.toString().split('!@!');
            if (result.indexOf('Success') > -1) {

                if (splitstr[0] == "Success") {
                    if (hdnimage == "") {
                        hdnimage = splitstr[1];
                        hdnfilename = filename;
                    }
                    else {
                        hdnimage = hdnimage + "," + splitstr[1];
                        hdnfilename = hdnfilename + "|" + filename;
                    }
                    $(".uploadimagetext").html(hdnfilename);
                    $("#hdnfilename").val(hdnfilename);
                    $("#hdnimage").val(hdnimage);
                    $("#ContentPlaceHolder1_hdnimage").val(hdnimage);
                    $("#ContentPlaceHolder1_hdnfilename").val(hdnfilename);
                    $("progress").hide();
                    //PreviewImage(obj, divid, 1000);
                }
                else {
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

function ShowMoreReply() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var TicketId = $("#hdnticketid").val();
            GetTickets_Reply(TicketId);
        }, 10);
}

function ShowMore() {
    debugger;
    var type = $('#hdnTabType').val();
    if (type == 1) {
        GetTickets(1, 2, 2, 1, 10, 0, "", 0, 0, 0, "", "", "");
    }
    else if (type == 2) {
        GetTickets(2, 2, 2, 2, 10, 0, "", 0, 0, 0, "", "", "");
    }
    else if (type == 3) {
        GetTickets(2, 2, 1, 0, 10, 0, "", 0, 0, 0, "", "", "");
    }
    else if (type == 4) {
        GetTickets(2, 2, 2, 0, 10, 0, "", 0, 0, 0, "", "", "");
    }
    //else if (type == 5) {
    //    GetTickets(2, 2, 2, 0, 10, 0, "", 1);
    //}
    else if (type == 6) {
        GetTickets(2, 2, 2, 1, 10, 0, "", 0, 0, 0, "", "", "");
    }
}