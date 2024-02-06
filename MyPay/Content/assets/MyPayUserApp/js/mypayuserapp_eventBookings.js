var WebResponse = '';
var EventsJson = '';


$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvSingleBookings").css("display", "none");
            BookingsLoad();
        }, 10);

    $("html, body").animate({ scrollTop: "0" });
});

$(document).ready(function () {
  

});


function BookingsLoad() {
    $('#AjaxLoader').show();
    $("#dvMessage").html('');
    $("#dvSingleBookings").css("display", "none");
    $("#dvEventBookings").css("display", "block");
    setTimeout(
        function () {
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserEventBookingsList",
        data: '{"Take":"' + $("#hdnTake").val() + '","Skip":"' + $("#hdnSkip").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                var jsonData;
                var objTrans = '';
                var IsValidJson = false;
                try {
                    jsonData = $.parseJSON(response);
                    EventsJson = jsonData;
                  
                    var IsValidJson = true;
                    if (jsonData.Bookings.length < 10) {

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
                
                if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                    for (var i = 0; i < jsonData.data.length; i++) {
                        objTrans += '<li class="nk-support-item px-0 cursor-pointer" onclick="SingleTicketLoad(&apos;' + jsonData.data[i].Id + '&apos;);">';
                        objTrans += '<div class="serv-icon">';
                        objTrans += '<img src="' + jsonData.data[i].bannerImagePath + '" width="35" alt="" class="bg-light rounded-circle p-1">';
                        objTrans += '</div>';
                        objTrans += '<div class="nk-support-content">';
                        objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.data[i].eventDateString + '</span>';
                        objTrans += '<div class="title">';
                        objTrans += '<span class="timeline-title mb-0">' + jsonData.data[i].eventName + '';
                        objTrans += '<div class="d-flex align-items-center fw-normal">';
                        objTrans += '<span class="fs-12px">' + jsonData.data[i].venueName + ', ' + jsonData.data[i].venueAddress+'</span>';
                        objTrans += '</div>';
                        objTrans += '</span>';
                        objTrans += '<span class="amount fw-medium fs-14px m-0 text-right">';
                        objTrans += '<small>Rs.</small>' + jsonData.data[i].totalPrice + '';
                        objTrans += '<br/><label class="m-0 ' + (jsonData.data[i].IsBooked == true ? "text-green" : "") + ' fs-13px">' + (jsonData.data[i].IsBooked == true ? "success" : "booked") + '</label>';
                        objTrans += '</span>';
                        objTrans += '</div>';
                        objTrans += '<div class="d-flex mt-1">';
                        objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0 mr-2">';
                        objTrans += '<img src="/Content/assets/MyPayUserApp/images/user-group-solid.svg" class="mr-1">' + jsonData.data[i].noOfTicket + ' Tickets';
                        objTrans += '</p>';
                        objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0">';
                        objTrans += '<img src="/Content/assets/MyPayUserApp/images/clock-solid.svg" class="mr-1">' + jsonData.data[i].eventStartTime + '';
                        objTrans += '</p>';
                        objTrans += '</div>';
                        objTrans += '</div>';
                        objTrans += '</li>';


                    }
                    $('#AjaxLoader').hide();
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                        $('#AjaxLoader').hide();
                    }
                    else {
                        objTrans += '<div class="col-md-12 mb-6">';
                        objTrans += '<div style="color:red;text-align:center">No Bookings Found</div>';
                        objTrans += '</div>';
                        $('#AjaxLoader').hide();
                    }
                    $("#showmore").css("display", "none");
                }
                var skipTrans = parseInt($("#hdnSkip").val()) + 1;
                $("#table").append(objTrans);
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

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvSingleBookings").css("display", "none");
            BookingsLoad();
        }, 10);
}
function SingleTicketLoad(BookingId) {
    $("#dvMessage").html('');
    var selectedTicket = EventsJson.data.find(x => x.Id == BookingId);
   
    $("#dvEventBookings").hide();
    var objEvents = '';
    objEvents += '<div class="post_img p-3 round bg-lighter">';
    objEvents += '<img src="' + selectedTicket.bannerImagePath + '" class="w-100">';
    objEvents += '</div><!--post_img-->';
    objEvents += '<div class="d-lg-flex mt-1 align-items-center justify-content-between">';
    objEvents += '<span>' + selectedTicket.eventDateString + ' | ' + selectedTicket.venueAddress + '</span>';
    objEvents += '</div>';
    objEvents += '<div class="card-title d-flex w-100 mt-4 mb-4 align-items-center big-card-title">';
    objEvents += '<h4 class="card-title mb-0">' + selectedTicket.eventName + '</h4><!--card-title-->';
    objEvents += '</div>';
    objEvents += '<p class="text-base">' + selectedTicket.eventDescription + '</p >';
    objEvents += '<a href="javascript:void(0);" class="btn btn-primary btn-lg d-block" onclick="DownloadTicket(&apos;' + selectedTicket.merchantCode + '&apos;,&apos;' + selectedTicket.OrderId + '&apos;)">Download Ticket</a>';
    objEvents += '<a href="javascript:void(0);" class="btn btn-primary btn-lg d-block mt-2" onclick="EmailTicket(&apos;' + selectedTicket.merchantCode + '&apos;,&apos;' + selectedTicket.OrderId + '&apos;)">Email Ticket</a>';
    objEvents += '<div class="text-soft fs-13px px-3 pb-3 mt-1">';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Event Start Time: <label class="fw-light m-0 pr-4">' + selectedTicket.eventStartTime + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Event End Time: <label class="fw-light m-0 pr-4">' + selectedTicket.eventEndTime + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Venue Name: <label class="fw-light m-0 pr-4">' + selectedTicket.venueName + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Venue Address: <label class="fw-light m-0 pr-4">' + selectedTicket.venueAddress + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Parking Available: <label class="fw-light m-0 pr-4">' + selectedTicket.parkingAvailable + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Total Ticket: <label class="fw-light m-0 pr-4">' + selectedTicket.noOfTicket + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Total Price: <label class="fw-light m-0 pr-4">Rs.' + selectedTicket.totalPrice + '</label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Transaction ID: <label class="fw-light m-0">' + selectedTicket.TransactionUniqueId + '<a href="javascript:void(0);" class="copy-order ml-1" onclick=Copy(&apos;' + selectedTicket.TransactionUniqueId + '&apos;)><em class="clipboard-icon icon ni ni-copy"></em></a></label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Order ID: <label class="fw-light m-0">' + selectedTicket.OrderId + '<a href="javascript:void(0);" class="copy-order ml-1" onclick=Copy(&apos;' + selectedTicket.OrderId + '&apos;)><em class="clipboard-icon icon ni ni-copy"></em></a></label></span>';
    objEvents += '<span class="d-flex justify-content-between fw-medium">Event Organizers: <label class="fw-light m-0 pr-4">' + selectedTicket.organizerName + '</label></span>';
    objEvents += '</div>';
    $("#dvSingleBookings").show();
    $("#dvBookingDetail").html('');
    $("#dvBookingDetail").append(objEvents);
}


function BackToTxns() {
    $("#dvSingleBookings").css("display", "none");
    $("#dvEventBookings").css("display", "block");
}

$("#btnBackFlightBookings").click(function () {
    BackToTxns();
});
function EmailTicket(MerchantCode, OrderId) {
    debugger;
    $("#dvMessage").html('');
    $('#AjaxLoader').show();
    setTimeout(
        function () {
   $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserEventTicketEmail",
       data: '{"MerchantCode":"' + MerchantCode + '","OrderId":"' + OrderId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            if (response != null) {
                debugger;
                var jsonData;
                var objTrans = '';
                var IsValidJson = false;
                try {
                    jsonData = $.parseJSON(response);
                    EventsJson = jsonData;
                    var IsValidJson = true;
                   
                }
                catch (err) {
                   
                }
                var arr = jsonData;
                $("#dvMessage").html("");
                if (IsValidJson) {
                    if (arr['message'] == "success") {
                        $('#dvMessage').css('color', 'green');
                        $("#dvMessage").html(jsonData.Message);
                    }
                    else {
                        $('#dvMessage').css('color', 'red');
                        $("#dvMessage").html(jsonData.Message);
                    }
                    $('#AjaxLoader').hide();
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                        $('#dvMessage').css('color', 'red');
                    }
                    else {
                        $("#dvMessage").html("Email not found");
                        $('#dvMessage').css('color', 'red');
                    }
                    $('#AjaxLoader').hide();
                }
               
            }
            else if (response == "Invalid Request") {
                window.location.href = "/MyPayUserLogin";
            }
            else {
                $("#dvMessage").html("Something went wrong. Please try again later.");
                $('#dvMessage').css('color', 'red');
                $('#AjaxLoader').hide();
                return false;
            }
        },
        failure: function (response) {
            $("#dvMessage").html(response.responseText);
            $('#dvMessage').css('color', 'red');
            $('#AjaxLoader').hide();
            return false;
        },
        error: function (response) {
            $("#dvMessage").html(response.responseText);
            $('#dvMessage').css('color', 'red');
            $('#AjaxLoader').hide();
            return false;
        }
   });
        }, 100);
}
function Copy(id) {
    navigator.clipboard.writeText(id);
}







