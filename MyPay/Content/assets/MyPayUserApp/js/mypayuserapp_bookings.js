var WebResponse = '';
var TotalAdult = 0;
var TotalChildren = 0;
var PassengersDetails = [];
var Passangers = "";
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
    debugger;
    //$(".faresummary").click(function () {
    //    $(this).closest(".faresummary").toggleClass('active');
    //});
    //$(".faresummary").click(function () {
    //    $(this).next(".faresummarydet").toggleClass('d-none');
    //});

});

$(".faresummary").click(function () {
    $(this).closest(".faresummary").toggleClass('active');
});
$(".faresummary").click(function () {
    $(this).next(".faresummarydet").toggleClass('d-none');
});
function BookingsLoad() {
    $("#dvSingleBookings").css("display", "none");
    $("#dvFlightBookings").css("display", "block");
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserBookingsList",
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
                if (jsonData != null && jsonData.Bookings != null && jsonData.Bookings.length > 0) {
                    for (var i = 0; i < jsonData.Bookings.length; ++i) {
                        var totalpassanger = jsonData.Bookings[i].Adult + jsonData.Bookings[i].Child;
                        objTrans += '<li class="nk-support-item px-0 cursor-pointer" onclick="SingleBookingsLoad(&apos;' + jsonData.Bookings[i].BookingId + '&apos;);">';
                        objTrans += '<div class="serv-icon">';
                        objTrans += '<img src="' + jsonData.Bookings[i].Airlinelogo + '" width="35" alt="" class="bg-light rounded-circle p-1">';
                        objTrans += '</div>';
                        objTrans += '<div class="nk-support-content">';
                        objTrans += '<span class="d-block fs-12px fw-normal text-soft">' + jsonData.Bookings[i].Flightdatedt.split(' ')[0] + '</span>';
                        objTrans += '<div class="title">';
                        objTrans += '<span class="timeline-title mb-0">' + jsonData.Bookings[i].Airlinename + '';
                        objTrans += '<div class="d-flex align-items-center fw-normal">';
                        objTrans += '<span class="fs-12px">' + jsonData.Bookings[i].Departure + '</span>';
                        objTrans += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                        objTrans += '<span class="fs-12px">' + jsonData.Bookings[i].Arrival + '</span>';
                        objTrans += '</div>';
                        objTrans += '</span>';
                        objTrans += '<span class="amount fw-medium fs-14px m-0 text-right">';
                        objTrans += '<small>Rs.</small>' + jsonData.Bookings[i].Faretotal + '';
                        objTrans += '<br/><label class="m-0 ' + (jsonData.Bookings[i].IsFlightIssued == true ? "text-green" : "") + ' fs-13px">' + (jsonData.Bookings[i].IsFlightIssued == true ? "success" : "booked") + '</label>';
                        objTrans += '</span>';
                        objTrans += '</div>';
                        objTrans += '<div class="d-flex mt-1">';
                        objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0 mr-2">';
                        objTrans += '<img src="/Content/assets/MyPayUserApp/images/user-group-solid.svg" class="mr-1">' + totalpassanger + ' Passenger';
                        objTrans += '</p>';
                        objTrans += '<p class="text-soft d-flex align-items-center fs-12px line-height-1 mt-0">';
                        objTrans += '<img src="/Content/assets/MyPayUserApp/images/clock-solid.svg" class="mr-1">' + jsonData.Bookings[i].Departuretime + '';
                        objTrans += '</p>';
                        objTrans += '</div>';
                        objTrans += '</div>';
                        objTrans += '</li>';



                    }
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                    }
                    else {

                        $("#dvMessage").html("No Bookings Found");
                    }
                    $("#showmore").css("display", "none");
                }
                var skipTrans = parseInt($("#hdnSkip").val()) + 1;
                $("#table").append(objTrans);
                $("#hdnSkip").val(skipTrans);
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

    $('#AjaxLoader').hide();
}

function ShowMore() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $("#dvSingleBookings").css("display", "none");
            BookingsLoad();
        }, 10);
}
function SingleBookingsLoad(BookingId) {
    $('#AjaxLoader').show();
    $("#div_Payment").hide();
    $("#hdnBookingId").val(BookingId);
    $("#lblAmount").text('');
    $("#lblCashback").text('');
    $("#lblServiceCharge").text('');
    $("#lblDivAmount").text('');
    setTimeout(
        function () {
            debugger;
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserBookingDetail",
                data: '{"Take":"' + $("#hdnTake").val() + '","Skip":"' + $("#hdnSkip").val() + '","BookingId":"' + BookingId + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    var IsFlightIssued = false;
                    var BookingId = "";
                    var TransactionId = "";
                    $('#AjaxLoader').hide();
                    if (response != null) {
                        debugger;
                        $("#dvBookingDetail").html('');
                        $("#dvSingleBookings").css("display", "block");
                        $("#dvFlightBookings").css("display", "none");
                        var jsonData;
                        var objTrans = '';
                        var IsValidJson = false;
                        var InBoundAmout = 0;
                        var OutBoundAmout = 0;

                        try {
                            jsonData = $.parseJSON(response);
                            var IsValidJson = true;

                        }
                        catch (err) {

                        }
                        if (jsonData != null && jsonData.InBounds != null && jsonData.InBounds.length > 0) {
                            for (var i = 0; i < jsonData.InBounds.length; ++i) {
                                $("#hdnReturnFlightId").val(jsonData.InBounds[i].Flightid);
                                IsFlightIssued = jsonData.InBounds[i].IsFlightIssued;
                                InBoundAmout = jsonData.InBounds[i].Faretotal;
                                TotalAdult = jsonData.InBounds[i].Adult;
                                TotalChildren = jsonData.InBounds[i].Child;
                                var totalpassanger = jsonData.InBounds[i].Adult + jsonData.InBounds[i].Child;
                                var totaladultfare = jsonData.InBounds[i].Adult * jsonData.InBounds[i].Adultfare;
                                var totalchildfare = jsonData.InBounds[i].Child * jsonData.InBounds[i].Childfare;
                                BookingId = jsonData.InBounds[i].BookingId;
                                TransactionId = jsonData.InBounds[i].TransactionId;
                                objTrans += '<h4>InBound Ticket Detail</h4>';
                                objTrans += '<div class="d-flex align-items-center">';
                                objTrans += '<img src = "' + jsonData.InBounds[i].Airlinelogo + '" width = "26">';
                                objTrans += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.InBounds[i].Airlinename + '</span>';
                                objTrans += '</div >';
                                objTrans += '<div class="fs-15px text-uppercase fw-medium mt-3 mb-0">' + jsonData.InBounds[i].SectorFrom + ' - ' + jsonData.InBounds[i].SectorTo + '</div>';
                                objTrans += '<div class="d-flex align-items-center">';
                                objTrans += '<span class="fs-12px">' + jsonData.InBounds[i].Departure + '</span>';
                                objTrans += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                objTrans += '<span class="fs-12px">' + jsonData.InBounds[i].Arrival + '</span>';
                                objTrans += '</div>';
                                objTrans += '<div class="text-soft mt-2 fs-13px">';
                                objTrans += '<span class="w-100 display-inherit">' + jsonData.InBounds[i].Flightdatedt.split(' ')[0] + '</span>';
                                objTrans += '<span class="w-100 display-inherit">' + jsonData.InBounds[i].Departuretime + ' - ' + jsonData.InBounds[i].Arrivaltime + '</span>';
                                objTrans += '<span class="w-100 display-inherit">' + jsonData.InBounds[i].Flightno + ' | ' + jsonData.InBounds[i].FlightTypeName + ' | ' + (jsonData.InBounds[i].Refundable == true ? "Refundable" : "Non-Refundable") + '</span>';
                                objTrans += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.InBounds[i].Freebaggage + '</span>';
                                objTrans += '<span class="w-100 display-inherit"><label class="m-0 text-base fw-medium">PNR Number:</label> ' + jsonData.InBounds[i].PnrNumber + '</span>';
                                objTrans += '</div>';
                                objTrans += '<div class="mt-3 mb-4 bg-lighter line-height-1 text-soft">';
                                objTrans += '<a href="javascript:void(0)" class="px-3 pt-2 pb-2 fs-13px fw-medium text-soft d-flex align-items-center faresummary active">Fare Summary <em class="icon ni ni-downward-ios ml-1"></em></a>';
                                objTrans += '<div class="text-soft fs-13px px-3 pb-3 mt-1 faresummarydet">';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">Travellers <label class="fw-medium m-0">' + totalpassanger + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">' + jsonData.InBounds[i].Adult + ' Adult x Rs. ' + jsonData.InBounds[i].Adultfare + ' <label class="fw-medium m-0">' + totaladultfare + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">' + jsonData.InBounds[i].Child + ' child x Rs. ' + jsonData.InBounds[i].Childfare + ' <label class="fw-medium m-0">' + totalchildfare + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">Fuel Charges <label class="fw-medium m-0">' + jsonData.InBounds[i].Fuelsurcharge + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light">Tax <label class="fw-medium m-0">' + jsonData.InBounds[i].Tax + '</label></span>';
                                objTrans += '</div>';
                                objTrans += '</div>';
                                objTrans += '<div class="d-flex justify-content-between">';
                                objTrans += '<div class="line-height-1">';
                                //objTrans += '<label class="text-soft fs-12px mb-1 w-100">Cashback</label>';
                                //objTrans += '<span class="fw-medium fs-16px"><small>Rs.</small>110</span>';
                                objTrans += '</div>';
                                objTrans += '<div class="text-right line-height-1">';
                                objTrans += '<label class="text-soft fs-12px mb-1 w-100">Amount</label>';
                                objTrans += '<span class="fw-medium text-right fs-16px"><small>Rs.</small>' + jsonData.InBounds[i].Faretotal + '</span>';
                                objTrans += '</div>';
                                objTrans += '</div>';

                                objTrans += '<div class="mt-4 text-center">';

                                objTrans += '</div>';
                                objTrans += '<div class="p-sm-4 p-3 bg-lighter mt-4">';
                                objTrans += '<div class="badge badge-dim bg-outline-warning p-3 align-items-start text-left">';
                                objTrans += '<img src="/Content/assets/MyPayUserApp/images/info.svg" width="15">';
                                objTrans += '<p class="m-0 ml-2 fs-12px text-wrap">Please report to the airport 1 hour prior to the departure time</p>';
                                objTrans += '</div>';
                                objTrans += '<h6 class="title mb-3 mt-4">';
                                objTrans += 'Passenger Details';
                                objTrans += '</h6>';
                                objTrans += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                for (var j = 0; j < jsonData.InBounds[i].PassengersDetails.length; ++j) {
                                    objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.InBounds[i].PassengersDetails[j].Type + ' 1 : ' + jsonData.InBounds[i].PassengersDetails[j].Title + '. ' + jsonData.InBounds[i].PassengersDetails[j].Firstname + ' ' + jsonData.InBounds[i].PassengersDetails[j].Lastname + '</span>';
                                    objTrans += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Departure Ticket Number</span>';
                                    objTrans += '<span class="fs-14px w-100 d-inline-flex text-green fw-medium">' + jsonData.InBounds[i].PassengersDetails[j].TicketNo + '</span>';
                                }

                                objTrans += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.InBounds[i].ContactName + '</span>';
                                objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.InBounds[i].ContactPhone + '</span>';
                                objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.InBounds[i].ContactEmail + '</span>';
                                objTrans += '</div>';
                            }
                        }
                        else {
                            if (!IsValidJson) {
                                $("#dvMessage").html(response);
                            }

                            $("#showmore").css("display", "none");
                        }
                        if (jsonData != null && jsonData.OutBounds != null && jsonData.OutBounds.length > 0) {
                            for (var i = 0; i < jsonData.OutBounds.length; ++i) {
                                $("#hdnFlightId").val(jsonData.OutBounds[i].Flightid);
                                IsFlightIssued = jsonData.OutBounds[i].IsFlightIssued;
                                OutBoundAmout = jsonData.OutBounds[i].Faretotal;
                                totalpassanger = jsonData.OutBounds[i].Adult + jsonData.OutBounds[i].Child;
                                totaladultfare = jsonData.OutBounds[i].Adult * jsonData.OutBounds[i].Adultfare;
                                totalchildfare = jsonData.OutBounds[i].Child * jsonData.OutBounds[i].Childfare;
                                TotalAdult = jsonData.OutBounds[i].Adult;
                                TotalChildren = jsonData.OutBounds[i].Child;
                                BookingId = jsonData.OutBounds[i].BookingId;
                                PassengersDetails = jsonData.OutBounds[i].PassengersDetails;
                                TransactionId = jsonData.OutBounds[i].TransactionId;
                                objTrans += '<h4>OutBound Ticket Detail</h4>';
                                objTrans += '<div class="d-flex align-items-center">';
                                objTrans += '<img src = "' + jsonData.OutBounds[i].Airlinelogo + '" width = "26" >';
                                objTrans += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds[i].Airlinename + '</span>';
                                objTrans += '</div >';
                                objTrans += '<div class="fs-15px text-uppercase fw-medium mt-3 mb-0">' + jsonData.OutBounds[i].SectorFrom + ' - ' + jsonData.OutBounds[i].SectorTo + '</div>';
                                objTrans += '<div class="d-flex align-items-center">';
                                objTrans += '<span class="fs-12px">' + jsonData.OutBounds[i].Departure + '</span>';
                                objTrans += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                objTrans += '<span class="fs-12px">' + jsonData.OutBounds[i].Arrival + '</span>';
                                objTrans += '</div>';
                                objTrans += '<div class="text-soft mt-2 fs-13px">';
                                objTrans += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightdatedt.split(' ')[0] + '</span>';
                                objTrans += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Departuretime + ' - ' + jsonData.OutBounds[i].Arrivaltime + '</span>';
                                objTrans += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightno + ' | ' + jsonData.OutBounds[i].FlightTypeName + ' | ' + (jsonData.OutBounds[i].Refundable == true ? "Refundable" : "Non-Refundable") + '</span>';
                                objTrans += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds[i].Freebaggage + '</span>';
                                objTrans += '<span class="w-100 display-inherit"><label class="m-0 text-base fw-medium">PNR Number:</label> ' + jsonData.OutBounds[i].PnrNumber + '</span>';
                                objTrans += '</div>';
                                objTrans += '<div class="mt-3 mb-4 bg-lighter line-height-1 text-soft">';
                                objTrans += '<a href="javascript:void(0)" class="px-3 pt-2 pb-2 fs-13px fw-medium text-soft d-flex align-items-center faresummary active">Fare Summary <em class="icon ni ni-downward-ios ml-1"></em></a>';
                                objTrans += '<div class="text-soft fs-13px px-3 pb-3 mt-1 faresummarydet">';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">Travellers <label class="fw-medium m-0">' + totalpassanger + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">' + jsonData.OutBounds[i].Adult + ' Adult x Rs. ' + jsonData.OutBounds[i].Adultfare + ' <label class="fw-medium m-0">' + totaladultfare + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">' + jsonData.OutBounds[i].Child + ' child x Rs. ' + jsonData.OutBounds[i].Childfare + ' <label class="fw-medium m-0">' + totalchildfare + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light mb-1">Fuel Charges <label class="fw-medium m-0">' + jsonData.OutBounds[i].Fuelsurcharge + '</label></span>';
                                objTrans += '<span class="d-flex justify-content-between fw-light">Tax <label class="fw-medium m-0">' + jsonData.OutBounds[i].Tax + '</label></span>';
                                objTrans += '</div>';
                                objTrans += '</div>';
                                objTrans += '<div class="d-flex justify-content-between">';
                                objTrans += '<div class="line-height-1">';
                                //objTrans += '<label class="text-soft fs-12px mb-1 w-100">Cashback</label>';
                                //objTrans += '<span class="fw-medium fs-16px"><small>Rs.</small>110</span>';
                                objTrans += '</div>';
                                objTrans += '<div class="text-right line-height-1">';
                                objTrans += '<label class="text-soft fs-12px mb-1 w-100">Amount</label>';
                                objTrans += '<span class="fw-medium text-right fs-16px"><small>Rs.</small>' + jsonData.OutBounds[i].Faretotal + '</span>';
                                objTrans += '</div>';
                                objTrans += '</div>';

                                objTrans += '<div class="mt-4 text-center">';
                                //objTrans += '<a href="javascript:void(0)" class="d-flex align-items-center text-base justify-content-center fw-medium line-height-1">';
                                //objTrans += '<em class="icon ni ni-mail fs-20px text-orange mr-1"></em> Email Ticket';
                                //objTrans += '</a>';
                                objTrans += '</div>';
                                objTrans += '<div class="p-sm-4 p-3 bg-lighter mt-4">';
                                objTrans += '<div class="badge badge-dim bg-outline-warning p-3 align-items-start text-left">';
                                objTrans += '<img src="/Content/assets/MyPayUserApp/images/info.svg" width="15">';
                                objTrans += '<p class="m-0 ml-2 fs-12px text-wrap">Please report to the airport 1 hour prior to the departure time</p>';
                                objTrans += '</div>';
                                objTrans += '<h6 class="title mb-3 mt-4">';
                                objTrans += 'Passenger Details';
                                objTrans += '</h6>';
                                objTrans += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                for (var j = 0; j < jsonData.OutBounds[i].PassengersDetails.length; ++j) {
                                    objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.OutBounds[i].PassengersDetails[j].Type + ' 1 : ' + jsonData.OutBounds[i].PassengersDetails[j].Title + '. ' + jsonData.OutBounds[i].PassengersDetails[j].Firstname + ' ' + jsonData.OutBounds[i].PassengersDetails[j].Lastname + '</span>';
                                    objTrans += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Departure Ticket Number</span>';
                                    objTrans += '<span class="fs-14px w-100 d-inline-flex text-green fw-medium">' + jsonData.OutBounds[i].PassengersDetails[j].TicketNo + '</span>';
                                }

                                objTrans += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.OutBounds[i].ContactName + '</span>';
                                objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.OutBounds[i].ContactPhone + '</span>';
                                objTrans += '<span class="fs-14px w-100 fw-medium d-inline-flex">' + jsonData.OutBounds[i].ContactEmail + '</span>';
                                objTrans += '</div>';

                            }
                        }
                        else {
                            if (!IsValidJson) {
                                $("#dvMessage").html(response);
                            }

                            $("#showmore").css("display", "none");
                        }

                        var skipTrans = parseInt($("#hdnSkip").val()) + 1;

                        if (IsFlightIssued) {
                            objTrans += '<a href="/TransactionReceipt/Index?transactionid=' + TransactionId + '" "javascript:void(0)" class="btn btn-primary btn-lg d-block mt-4">Download Receipt</a>';
                            objTrans += '<a href="/FlightReport/GetFlightTicketDownload?bookingid=' + BookingId + '" "javascript:void(0)" class="btn btn-primary btn-lg d-block mt-4")">Download Ticket</a>';
                        }
                        else {
                            objTrans += '<a href="javascript:void(0)" class="btn btn-primary btn-lg d-block mt-4" id="" onclick="return BookFlight()">Book Now</a>';
                            var TotalAmout = Number(InBoundAmout) + Number(OutBoundAmout);
                        }

                        $("#hdnFinalAmount").val(TotalAmout);
                        $("#dvBookingDetail").append(objTrans);
                        $("#hdnSkip").val(skipTrans);
                        $('#AjaxLoader').hide();
                    }
                    else {
                        $('#AjaxLoader').hide();
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

        }, 10);
}

function BackToTxns() {
    $("#dvSingleBookings").css("display", "none");
    $("#dvFlightBookings").css("display", "block");
}

$("#btnBackFlightBookings").click(function () {
    BackToTxns();
});

function GetFlightTicketDownload(BookingId) {
    debugger;
    if (BookingId != "") {
        $.ajax({
            type: "POST",
            url: "/FlightReport/GetFlightTicketDownload",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"BookingId":"' + BookingId + '"}',
            success: function (response) {
                debugger;

                if (response != null) {
                }
                else {
                    JsonOutput = "Something went wrong. Please try again later.";
                }
            },
            failure: function (response) {
                JsonOutput = (response.responseText);
            },
            error: function (response) {
                JsonOutput = (response.responseText);
            }
        });
    }
}
function BookFlight() {
    if (PassengersDetails.length > 0) {
        $("#dvBookingDetail").hide();
        $("#div-ticket-detail").hide();
        $("#div_Payment").show();
        ServiceCharge($("#hdnFinalAmount").val());
    }
    else {
        $("#dvBookingDetail").hide();
        $("#div-ticket-detail").hide();
        $("#dvpassengerdetails").html("");
        var str = "";
        str = '<div class="row">';
        str += '<div class="col-md-12 col-lg-12 col-xl-12 col-xxl-12">';
        str += '<div class="card-title-group mb-4">';
        str += '<div class="card-title d-flex w-100 align-items-center pb-3 border-bottom">';
        str += '        <h6 class="title mb-0 d-flex align-items-center">';
        str += '            <span>Passenger Details</span>';
        str += '        </h6>';
        str += '        <a href="javascript:void(0)"  class="btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white" onclick="BackToBooking()"> Back</a>';
        str += '    </div>';
        str += '</div>';
        str += '<div class="badge badge-dim bg-outline-warning p-2">';
        str += '    <img src="/Content/assets/MyPayUserApp/images/info.svg" width="15">';
        str += '        <p class="m-0 ml-2 fs-12px">Please ensure spelling of passenger name and other details <br />match with government  approved IDs as these cannot be <br />changed later. Errors might lead to cancellation penalties.</p>';
        str += '                                        </div>';
        for (var i = 1; i <= TotalAdult; ++i) {
            str += '    <h6 class=" mt-4 mb-2">Adult ' + i + ' Details</h6>';
            str += '    <div class="row">';
            str += '        <div class="col-md-6">';
            str += '            <div class="form-group">';
            str += '                <div class="form-control-wrap ">';
            str += '                    <div class="form-control-select">';
            str += '                        <label class="form-label" for="title">Title</label>';
            str += '                        <select class="form-control form-control-lg" id="titleAdult' + i + '">';
            str += '                            <option value="0">Select Title</option>';
            str += '                            <option value="MR">Mr</option>';
            str += '                            <option value="MRS">Mrs</option>';
            str += '                            <option value="MS">Miss</option>';
            str += '                        </select>';
            str += '                    </div>';
            str += '                </div>';
            str += '            </div>';
            str += '        </div>';
            str += '        <div class="col-md-6">';
            str += '            <div class="form-group">';
            str += '                <div class="form-control-wrap ">';
            str += '                   <div class="form-control-select">';
            str += '                        <label class="form-label" for="country">Country</label>';
            str += '                        <select class="form-control form-control-lg" id="countryAdult' + i + '">';
            str += '                            <option value="0">Select Country</option>';
            str += '                            <option value="NP">Nepal</option>';
            str += '                            <option value="IN">India</option>';
            str += '                        </select>';
            str += '                    </div>';
            str += '                </div>';
            str += '            </div>';
            str += '        </div>';
            str += '        <div class="col-12 mt-3">';
            str += '            <div class="form-group">';
            str += '                <div class="form-control-wrap">';
            str += '                        <label class="form-label" for="fname">First Name <strong class="text-danger">*</strong></label>';
            str += '                    <input type="text" value="" class="form-control form-control-lg rounded" id="fnameAdult' + i + '">';
            str += '                                                    </div>';
            str += '                </div>';
            str += '            </div>';
            str += '            <div class="col-12 mt-3">';
            str += '                <div class="form-group">';
            str += '                    <div class="form-control-wrap">';
            str += '                            <label class="form-label" for="lname">Last Name <strong class="text-danger">*</strong></label>';
            str += '                           <input type="text" value="" class="form-control form-control-lg rounded" id="lnameAdult' + i + '">';
            str += '                                                </div>';
            str += '                    </div>';
            str += '                </div>';
            str += '                </div>';
        }
        for (var j = 1; j <= TotalChildren; ++j) {
            str += '    <h6 class=" mt-4 mb-2">Child ' + j + ' Details</h6>';
            str += '    <div class="row">';
            str += '        <div class="col-md-6">';
            str += '            <div class="form-group">';
            str += '                <div class="form-control-wrap ">';
            str += '                    <div class="form-control-select">';
            str += '                        <label class="form-label" for="title">Title</label>';
            str += '                        <select class="form-control form-control-lg" id="titlechild' + j + '">';
            str += '                            <option value="0">Select Title</option>';
            str += '                            <option value="MR">Mr</option>';
            str += '                            <option value="MRS">Mrs</option>';
            str += '                            <option value="MS">Miss</option>';
            str += '                        </select>';
            str += '                    </div>';
            str += '                </div>';
            str += '            </div>';
            str += '        </div>';
            str += '        <div class="col-md-6">';
            str += '            <div class="form-group">';
            str += '                <div class="form-control-wrap ">';
            str += '                   <div class="form-control-select">';
            str += '                        <label class="form-label" for="country">Country</label>';
            str += '                        <select class="form-control form-control-lg" id="countrychild' + j + '">';
            str += '                            <option value="0">Select Country</option>';
            str += '                            <option value="NP">Nepal</option>';
            str += '                            <option value="IN">India</option>';
            str += '                        </select>';
            str += '                    </div>';
            str += '                </div>';
            str += '            </div>';
            str += '        </div>';
            str += '        <div class="col-12 mt-3">';
            str += '            <div class="form-group">';
            str += '                <div class="form-control-wrap">';
            str += '                        <label class="form-label" for="fname">First Name <strong class="text-danger">*</strong></label>';
            str += '                    <input type="text" class="form-control form-control-lg rounded" id="fnamechild' + j + '">';
            str += '                                                    </div>';
            str += '                </div>';
            str += '            </div>';
            str += '            <div class="col-12 mt-3">';
            str += '                <div class="form-group">';
            str += '                    <div class="form-control-wrap">';
            str += '                            <label class="form-label" for="lname">Last Name <strong class="text-danger">*</strong></label>';
            str += '                           <input type="text" class="form-control form-control-lg rounded" id="lnamechild' + j + '">';
            str += '                                                </div>';
            str += '                    </div>';
            str += '                </div>';
            str += '                </div>';
        }
        str += '    <div class="row">';
        str += '                <div class="col-12 mt-5">';
        str += '                    <h6 class="fs-16px mb-0">Contact Person Details</h6>';
        str += '                    <p class="m-0 text-soft fs-12px">Booking details will be sent to below contact details</p>';
        str += '                </div>';
        str += '                <div class="col-12 mt-3">';
        str += '                    <div class="form-group">';
        str += '                        <div class="form-control-wrap">';
        str += '                            <input type="text" class="form-control form-control-lg rounded fw-medium text-base" id="txtContactNumber" maxlength="10" onkeypress="return isNumberKey(this, event);" value="' + $("#hdnContactNumber").val() + '">';
        str += '                                                    </div>';
        str += '                        </div>';
        str += '                    </div>';
        str += '                    <div class="col-12 mt-3">';
        str += '                        <div class="form-group">';
        str += '                            <div class="form-control-wrap">';
        str += '                                <input type="text" class="form-control form-control-lg rounded fw-medium text-base" id="txtContactName" value="' + $("#hdnContactName").val() + '">';
        str += '                                                    </div>';
        str += '                            </div>';
        str += '                        </div>';
        str += '                        <div class="col-12 mt-3">';
        str += '                            <div class="form-group">';
        str += '                                <div class="form-control-wrap">';
        str += '                                    <input type="text" class="form-control form-control-lg rounded fw-medium text-base" id="txtContactEmail" value="' + $("#hdnContactEmail").val() + '">';
        str += '                                                    </div>';
        str += '                                </div>';
        str += '                            </div>';
        str += '                            <div class="col-12 mt-4 mt-sm-5 text-center">';
        str += '                                <a href="javascript:void(0)" class="btn btn-primary btn-lg d-block" id="btnProceedBooking" onclick="ProceedBooking();">Proceed Booking</a>';
        str += '                            </div>';
        str += '                        </div>';
        str += '                    </div>';
        str += '                </div>';
        str += '                </div>';

        $("#dvpassengerdetails").append(str);

        $("#dvpassengerdetails").show();
    }
}
function isNumberKey(el, evt) {
    debugger;
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
function BackToBooking() {
    $("#dvpassengerdetails").hide();
    $("#div_Payment").hide();
    $("#dvBookingDetail").show();
    $("#div-ticket-detail").show();

}
function ProceedBooking() {
    $("#dvFormMessage").html("");
    debugger;

    var Gender = "";
    var strPopup = "";
    $("#popupDetail").html("");
    Passangers = "";
    Passangers += "[";
    for (var i = 1; i <= TotalAdult; ++i) {
        if ($("#titleAdult" + i + " :selected").val() == "" || $("#titleAdult" + i + " :selected").val() == "0") {
            $("#dvFormMessage").html("Please select Adult " + i + " title");
            return false;
        }
        else if ($("#countryAdult" + i + " :selected").val() == "" || $("#countryAdult" + i + " :selected").val() == "0") {
            $("#dvFormMessage").html("Please select Adult " + i + " country");
            return false;
        }
        else if ($("#fnameAdult" + i).val() == "") {
            $("#dvFormMessage").html("Please enter Adult " + i + " first name");
            return false;
        }
        else if ($("#lnameAdult" + i).val() == "") {
            $("#dvFormMessage").html("Please enter Adult " + i + " last name");
            return false;
        }
        if ($("#titleAdult" + i + " :selected").val() == "MR") {
            Gender = "M";
        }
        else {
            Gender = "F";
        }
        Passangers += '{"FirstName":"' + $("#fnameAdult" + i).val() + '","LastName":"' + $("#lnameAdult" + i).val() + '","Type":"ADULT","Title":"' + $("#titleAdult" + i + " :selected").val() + '","Gender":"' + Gender + '","Nationality":"' + $("#countryAdult" + i + " :selected").val() + '"}';

        if (i < TotalAdult) {
            Passangers = Passangers + ",";
        }
        else if (TotalChildren > 0) {
            Passangers = Passangers + ",";
        }
        strPopup += 'Adult ' + i + ' : ' + $("#titleAdult" + i + " :selected").val() + " " + $("#fnameAdult" + i).val() + " " + $("#lnameAdult" + i).val() + '<br/>';

    }
    for (var j = 1; j <= TotalChildren; ++j) {
        if ($("#titlechild" + j + " :selected").val() == "" || $("#titlechild" + j + " :selected").val() == "0") {
            $("#dvFormMessage").html("Please select Child " + j + " title");
            return false;
        }
        else if ($("#countrychild" + j + " :selected").val() == "" || $("#countrychild" + j + " :selected").val() == "0") {
            $("#dvFormMessage").html("Please select Child " + j + " country");
            return false;
        }
        else if ($("#fnamechild" + j).val() == "") {
            $("#dvFormMessage").html("Please enter Child " + j + " first name");
            return false;
        }
        else if ($("#lnamechild" + j).val() == "") {
            $("#dvFormMessage").html("Please enter Child " + j + " last name");
            return false;
        }

        if ($("#titlechild" + j + " :selected").val() == "MR") {
            Gender = "M";
        }
        else {
            Gender = "F";
        }
        Passangers += '{"FirstName":"' + $("#fnamechild" + j).val() + '","LastName":"' + $("#lnamechild" + j).val() + '","Type":"CHILD","Title":"' + $("#titlechild" + j + " :selected").val() + '","Gender":"' + Gender + '","Nationality":"' + $("#countrychild" + j + " :selected").val() + '"}';

        if (j < TotalChildren) {
            Passangers = Passangers + ",";
        }
        strPopup += 'Child ' + j + ' : ' + $("#titlechild" + j + " :selected").val() + " " + $("#fnamechild" + j).val() + " " + $("#lnamechild" + j).val() + '<br/>';
    }
    Passangers += "]";

    var ContactNumber = $("#txtContactNumber").val();
    var ContactName = $("#txtContactName").val();
    var ContactEmail = $("#txtContactEmail").val();
    if (ContactNumber == "") {
        $("#dvFormMessage").html("Please enter Contact Person Number");
        return false;
    }
    else if (ContactName == "") {
        $("#dvFormMessage").html("Please enter Contact Person Name");
        return false;
    }
    else {
        $("#popupContactName").html(ContactName);
        $("#popupContactNumber").html(ContactNumber);
        $("#popupContactEmail").html(ContactEmail);
        $("#popupDetail").append(strPopup);

        $("#confirmPassengerDetails").modal('show');
    }
}
$("#btnAddPassengerDetail").click(function () {
    $("#dvFormMessage").html("");
    var ContactNumber = $("#txtContactNumber").val();
    var ContactName = $("#txtContactName").val();
    var ContactEmail = $("#txtContactEmail").val();
    var BookingId = $("#hdnBookingId").val();
    $('#AjaxLoader').show();
    if (Passangers != "") {
        //Passangers = JSON.stringify(Passangers);

        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserFlightPassangerDetails",
                    data: '{"BookingID":"' + BookingId + '","ContactName":"' + ContactName + '","ContactPhone":"' + ContactNumber + '","ContactEmail":"' + ContactEmail + '","PassengersClassString":"' + Passangers.replaceAll("\"", "'") + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response != null) {
                            var objBank = '';
                            var jsonData;
                            var IsValidJson = false;
                            try {
                                jsonData = $.parseJSON(response);
                                var IsValidJson = true;
                            }
                            catch (err) {

                            }
                            $("#OkMessage").html("");
                            if (jsonData != null && jsonData.Message == "Success") {
                                $("#OkMessage").html(jsonData.Details);
                                $("#confirmPassengerDetails").modal('hide');
                                $("#confirmPassenger").modal('show');

                                $("#DivFlightDetail").show();

                                GetPassengerDetail(BookingId);
                                $("#dvpassengerdetails").hide();
                                $('#AjaxLoader').hide();
                            }
                            else {
                                if (!IsValidJson) {
                                    $("#dvFormMessage").html(response);

                                }
                                else {

                                    $("#dvFormMessage").html("No Passenger detail added, Please try again.");
                                }
                                $("#confirmPassengerDetails").modal('hide');
                                $('#AjaxLoader').hide();
                            }
                        }
                        else {
                            $("#confirmPassengerDetails").modal('hide');
                            $("#dvFormMessage").html("Something went wrong. Please try again later.");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                    },
                    failure: function (response) {
                        $("#dvFormMessage").html(response.responseText);
                        $('#AjaxLoader').hide();
                        return false;
                    },
                    error: function (response) {
                        $("#dvFormMessage").html(response.responseText);
                        $('#AjaxLoader').hide();
                        return false;
                    }
                });
            }, 10);
    }
});
function GetPassengerDetail(BookingId) {
    debugger;
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    if (BookingId != "") {

        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserGetFlightPassengerDetail",
                    data: '{"BookingID":"' + BookingId + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        debugger;
                        if (response != null) {
                            var objBank = '';
                            var jsonData;
                            var IsValidJson = false;
                            try {
                                jsonData = $.parseJSON(response);
                                var IsValidJson = true;
                            }
                            catch (err) {

                            }

                            $("#dvBookingDetail").html("");

                            var str = "";
                            if (jsonData != null && jsonData.Message == "success") {
                                if (jsonData.InBounds.length > 0) {
                                    for (var a = 0; a < jsonData.InBounds.length; ++a) {
                                        str += '<div class="border-top pt-4 mt-4 d-flex align-items-center">';
                                        str += '<img src="' + jsonData.InBounds[a].Airlinelogo + '" width="26">';
                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.InBounds[a].Airlinename + '</span>';
                                        str += '</div>';
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<span class="fs-12px">' + jsonData.InBounds[a].Departure + '</span>';
                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                        str += '<span class="fs-12px">' + jsonData.InBounds[a].Arrival + '</span>';
                                        str += '</div>';
                                        str += '<div class="text-soft mt-2 fs-13px">';
                                        str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Flightdatedt + '</span>';
                                        str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Departuretime + ' - ' + jsonData.InBounds[a].Arrivaltime + '</span>';
                                        if (jsonData.InBounds[a].Refundable) {
                                            str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Flightno + ' | ' + jsonData.InBounds[a].Flightclasscode + ' | Refundable</span>';
                                        }
                                        else {
                                            str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Flightno + ' | ' + jsonData.InBounds[a].Flightclasscode + ' | Non-Refundable</span>';
                                        }
                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.InBounds[a].Freebaggage + '</span>';
                                        str += '</div>';

                                    }

                                }
                                if (jsonData.OutBounds.length > 0) {
                                    for (var k = 0; k < jsonData.OutBounds.length; ++k) {
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<img src="' + jsonData.OutBounds[k].Airlinelogo + '" width="26">';
                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds[k].Airlinename + '</span>';
                                        str += '</div>';
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<span class="fs-12px">' + jsonData.OutBounds[k].Departure + '</span>';
                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                        str += '<span class="fs-12px">' + jsonData.OutBounds[k].Arrival + '</span>';
                                        str += '</div>';
                                        str += '<div class="text-soft mt-2 fs-13px">';
                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Flightdatedt + '</span>';
                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Departuretime + ' - ' + jsonData.OutBounds[k].Arrivaltime + '</span>';
                                        if (jsonData.OutBounds[k].Refundable) {
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Flightno + ' | ' + jsonData.OutBounds[k].Flightclasscode + ' | Refundable</span>';
                                        }
                                        else {
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Flightno + ' | ' + jsonData.OutBounds[k].Flightclasscode + ' | Non-Refundable</span>';
                                        }
                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds[k].Freebaggage + '</span>';
                                        str += '</div>';
                                        str += '<div class="border-top pt-4 mt-4">';
                                        str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                        str += '<span class="fs-14px w-100 d-inline-flex">';
                                        for (var j = 1; j <= jsonData.OutBounds[k].PassengersDetails.length; ++j) {
                                            str += '' + j + '. ' + jsonData.OutBounds[k].PassengersDetails[j - 1].Title + ' ' + jsonData.OutBounds[k].PassengersDetails[j - 1].Firstname + ' ' + jsonData.OutBounds[k].PassengersDetails[j - 1].Lastname + ' <br/>';
                                        }
                                        str += '</span >';
                                        str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[k].ContactName + '</span>';
                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[k].ContactPhone + '</span>';
                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[k].ContactEmail + '</span>';
                                        str += '</div>';
                                    }
                                }


                                $("#dvBookingDetail").append(str);
                                $("#dvBookingDetail").show();
                                ServiceCharge($("#hdnFinalAmount").val());
                                $("#div_Payment").show();
                                $('#AjaxLoader').hide();
                            }
                            else {
                                if (!IsValidJson) {
                                    $("#dvMessage").html(response);

                                }
                                else {

                                    $("#dvMessage").html("No Passenger detail found, Please try again.");
                                }
                                $('#AjaxLoader').hide();
                            }
                        }
                        else {
                            $("#dvMessage").html("Something went wrong. Please try again later.");
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
            }, 10);
    }
}
function ServiceCharge(Amount) {
    cashback = "0";
    var IsCouponUnlocked = false;
    $("#CouponDiscountPopup").text("0");
    var ServiceId = $("#hdnServiceId").val();
    var PaymentMode = $("#hfPaymentMode").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ServiceCharge",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"Amount":"' + Amount + '","ServiceId":"' + ServiceId + '"}',
                success: function (response) {
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                    }
                    catch (err) {

                    }
                    var arr = jsonData;
                    if (arr['Message'] == "success") {
                        $("#MobilePopup").text($("#hdnBookingId").val());
                        $("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        cashback = arr['CashbackAmount'];
                        $("#detail-cashback").text("Rs. " + cashback + " Cashback");
                        $("#lblAmount").text(arr['Amount']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
                        $("#lblDivAmount").text(arr['Amount']);
                        var smartPayCoin = parseFloat($("#smartpayCoins").text());
                        $('#DivCoin').show();
                        if (smartPayCoin <= 0) {
                            $('#DivCoin').hide();
                        }
                        $("#FlightBookingIdPopup").text($("#hdnBookingId").val());
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#MypayCoinCashBackPopup").text("0");
                        if (IsCouponUnlocked) {
                            $("#CouponDiscountPopup").text("0");
                        }
                        $("#TotalAmountPopup").text(arr['NetAmount']);
                        CheckCoinBalance(0);

                        debugger;
                        $("#TxnAmountPopup").text(arr['Amount']);
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#MypayCoinCreditedPopup").text(arr['RewardPoints']);
                        $("#MypayCoinDebitedPopup").closest('tr').hide();
                        var MPCoinsDebit = parseFloat(arr['MPCoinsDebit']);
                        if (PaymentMode == "4") {
                            $("#AmountPopup").text(arr['WalletAmountDeduct']);
                            if (MPCoinsDebit > 0) {
                                $("#MypayCoinDebitedPopup").closest('tr').show();
                            }
                            var DeductAmount = parseFloat(arr['WalletAmountDeduct']);
                            var DeductServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            $("#TotalAmountPopup").text(parseFloat(DeductAmount + DeductServiceCharge).toFixed(2));
                        }
                        else {
                            $("#AmountPopup").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            $("#TotalAmountPopup").text(parseFloat(Amount + ServiceCharge).toFixed(2));

                        }

                        var MPCoinsCredit = parseFloat(arr['RewardPoints']);

                        if (MPCoinsCredit > 0) {
                            $("#MypayCoinCreditedPopup").closest('tr').show();
                        }

                        $('#AjaxLoader').hide();
                        return false;
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html("Invalid Credentials");
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
        }, 20);
}
$("#btnPay").click(function () {
    $("#dvMessage").html("");
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var BookingId = $("#hdnBookingId").val();
    if (BookingId == "") {
        $("#dvMessage").html("Please enter BookingId");
        return false;
    }
    var Amount = $("#hdnFinalAmount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please Book a flight");
        return false;
    }
    $("#DivFlightDetail").hide();

    $('#AjaxLoader').show();
    setTimeout(function () {
        GetBankDetail();
        ServiceCharge(Amount);
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);

    $("#lblPayAmount").text(Amount);

    $("#DivProceedToPay").show();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
    $('#DivWallet')[0].click();
    $("#dvSingleBookings").hide();
});
function CheckCoinBalance(ShowPopup) {
    var smartpayCoinsTotal = $("#smartpayCoinsTotal").text();
    var smartpayCoins = $("#smartpayCoins").text();
    var smartpayRupees = $("#smartpayRupees").text();
    var walletbalance = $("#hfWalletbalance").val();

    if (parseFloat(walletbalance) < parseFloat(smartpayRupees)) {

        $("#DivWallet").css("background", "antiquewhite");
        $("#DivBank").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

        $("#hfPaymentMode").val('1');
        if ($("#PopupText").text() == '') {
            $("#PopupText").text('Insufficient Wallet Balance');
        }
        if (ShowPopup == '1') {
            $('#PopUpMsg').modal('show');
        }
    }
    else if (parseFloat(smartpayCoinsTotal) < parseFloat(smartpayCoins)) {

        $("#DivWallet").css("background", "antiquewhite");
        $("#DivBank").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");


        $("#hfPaymentMode").val('1');
        if ($("#PopupText").text() == '') {
            $("#PopupText").text('Insufficient MP Coins');
        }
        if (ShowPopup == '1') {
            $('#PopUpMsg').modal('show');
        }
    }
}
function GetBankDetail() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/GetBankDetail",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: null,
                success: function (response) {

                    var objBank = '';
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                    }
                    catch (err) {

                    }
                    if (jsonData != null && jsonData.data != null && jsonData.data.length > 0) {
                        for (var i = 0; i < jsonData.data.length; ++i) {
                            if (jsonData.data[i].IsPrimary == true) {
                                objBank += ' <div class="nk-wg-action cursor-pointer">';
                                objBank += ' <div class="nk-wg-action-content d-block">';
                                objBank += ' <div class="d-flex align-items-center">';
                                objBank += ' <em class="icon top-0"><img src="' + jsonData.data[i].ICON_NAME + ' " width="22"></em>';
                                objBank += ' <div class="title">' + jsonData.data[i].BankName + ' </div>';
                                objBank += '</div>';
                                $("#hfBankId").val(jsonData.data[i].Id);
                                objBank += '<div class="text-soft">';
                                objBank += '<div class="d-block"><small class="d-block">' + jsonData.data[i].Name + ' </small>' + jsonData.data[i].AccountNumber + '</div>';
                                objBank += '<div class="d-block text-uppercase text-success fw-bold fs-12px mt-1"><img src="/Content/assets/MyPayUserApp/images/dashboard/primary.png" width="12" height="12"> Primary</div>';
                                objBank += '</div>';
                                objBank += '</div>';
                                objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';

                            }
                            else {
                                objBank += '<div class="nk-wg-action cursor-pointer">';
                                objBank += '<div class="nk-wg-action-content">';
                                objBank += '<em class="icon"><img src="/Content/assets/MyPayUserApp/images/dashboard/banksm.svg" width="22"></em>';
                                objBank += '<div class="title">Link Your Bank </div>';
                                objBank += '</div>';
                                objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                                objBank += '</div>';
                            }
                        }

                        $("#hfIsBankAdded").val("1");
                    }
                    else {
                        $("#hfIsBankAdded").val("0");

                        objBank += '<div class="nk-wg-action cursor-pointer">';
                        objBank += '<div class="nk-wg-action-content">';
                        objBank += '<em class="icon"><img src="/Content/assets/MyPayUserApp/images/dashboard/banksm.svg" width="22"></em>';
                        objBank += '<div class="title">Link Your Bank </div>';
                        objBank += '</div>';
                        objBank += '<a href="javascript:void(0);" class="text-soft"><em class="icon ni ni-forward-ios pt-1"></em></a>';
                        objBank += '</div>';

                        $("#showmore").css("display", "none");
                    }
                    $("#DivBank").html(objBank);
                    $('#AjaxLoader').hide();
                    return false;
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

$("#Pin").keypress(function (event) {
    if (event.keyCode == 13) {
        $('#btnPin')[0].click();
        event.preventDefault();
        event.stopPropagation();
        return false;
    }
});
$("#btnPinBack").click(function () {

});
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});

$("#DivWallet").click(function () {
    $("#PopupText").text('')
    $("#hfPaymentMode").val('1');

    $("#DivWallet").css("background", "antiquewhite");
    $("#DivCoin").css("background", "#fff");
    $("#DivBank").css("background", "#fff");

    $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
    $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
    $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

    if ($("#hfKYCStatus").val() != '3') {
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
    }
    CheckCoinBalance(0);
})
$("#DivCoin").click(function () {
    var smartpayCoins = $("#smartpayCoins").text();
    if (smartpayCoins <= 0) {
        return;
    }
    $("#PopupText").text('')
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivWallet").trigger("click");
        $("#PopupText").text('Your KYC should be approved to proceed.')
        $('#PopUpMsg').modal('show');
    }
    else {
        $("#hfPaymentMode").val('4');
        $("#DivWallet").css("background", "#fff");
        $("#DivBank").css("background", "#fff");
        $("#DivCoin").css("background", "antiquewhite");

        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");


        if ($("#hfKYCStatus").val() != '3') {
            $("#DivCoin").css("background", "#efefef");
            $("#DivBank").css("background", "#efefef");
        }
    }
    CheckCoinBalance(1);
})
$("#DivBank").click(function () {
    $("#PopupText").text('')
    if ($("#hfKYCStatus").val() != '3') {
        $("#DivWallet").css("background", "#fff");
        $("#DivCoin").css("background", "#efefef");
        $("#DivBank").css("background", "#efefef");
        $("#PopupText").text('Your KYC should be approved to proceed.')
        $('#PopUpMsg').modal('show');
    }
    else {
        $("#hfPaymentMode").val('2');
        $("#DivWallet").css("background", "#fff");
        $("#DivBank").css("background", "antiquewhite");
        $("#DivCoin").css("background", "#fff");


        $("#DivWallet").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");
        $("#DivBank").attr("class", "card-inner border p-3 payment-method mt-2 active paymentoption");
        $("#DivCoin").attr("class", "card-inner border p-3 payment-method mt-2  paymentoption");

        if ($("#hfKYCStatus").val() == '3') {
            if ($("#hfIsBankAdded").val() == "0") {
                window.location.href = '/MyPayUser/MyPayUserBankListAll';
            }
        }
    }
    CheckCoinBalance(0);
});

function Pay() {
    debugger;
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var BookingId = $("#hdnBookingId").val();
    if (BookingId == "") {
        $("#dvMessage").html("Please book flight first");
        return false;
    }
    var FlightId = $("#hdnFlightId").val();
    var ReturnFlightId = $("#hdnReturnFlightId").val();
    var Amount = $("#hdnFinalAmount").val();
    if (Amount == "") {
        $("#dvMessage").html("Please book flight.");
        return false;
    }
    var Mpin = $("#Pin").val();
    if (Mpin == "") {
        $("#dvMessage").html("Please enter Pin");
        return false;
    }
    var PaymentMode = $("#hfPaymentMode").val();
    if (PaymentMode == "0") {
        $("#dvMessage").html("Please select Payment Mode");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserIssueFlight",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"BookingId":"' + BookingId + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","FlightId":"' + FlightId + '","ReturnFlightId":"' + ReturnFlightId + '","BankId":"' + $("#hfBankId").val() + '"}',
                    success: function (response) {
                        debugger;
                        var jsonData;
                        var IsValidJson = false;
                        try {
                            jsonData = $.parseJSON(response);
                            var IsValidJson = true;
                        }
                        catch (err) {

                        }

                        if (IsValidJson) {

                            if (jsonData.Message == "success") {
                                $("#DivPin").modal("hide");
                                if (jsonData.IsCouponUnlocked == true) {
                                    $('#AjaxLoader').hide();
                                    $("#ScratchCardwonpopup").modal("show");
                                    setTimeout(function () {
                                        $("#ScratchCardwonpopup").modal("hide");
                                        window.location.href = "/MyPayUser/MyPayUserflightBookings";
                                    }, 3000);
                                }
                                else {
                                    window.location.href = "/MyPayUser/MyPayUserflightBookings";
                                    $('#AjaxLoader').hide();
                                    return false;
                                }
                            }
                            else {
                                $('#AjaxLoader').hide();
                                $("#DivPin").modal("hide");
                                $("#txnMsg").html(jsonData.response);
                                $("#DivErrMessage").modal("show");
                            }
                        }

                        else {
                            if (response == "Session Expired") {
                                alert('Logged in from another device.');
                                window.location.href = "/MyPayUserLogin/Index";
                                $('#AjaxLoader').hide();
                                $("#dvMessage").html(response);
                            }
                            else {
                                $('#AjaxLoader').hide();
                                $("#DivPin").modal("hide");
                                $("#txnMsg").html(response);
                                $("#DivErrMessage").modal("show");
                            }
                        }


                    },
                    failure: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response);
                    },
                    error: function (response) {
                        $('#AjaxLoader').hide();
                        JsonOutput = (response);
                    }
                });
            }, 100);
    }
}

$("#btnProceedToPay").click(function () {
    var PaymentMode = $("#hfPaymentMode").val();
    if (PaymentMode == "0" || PaymentMode == "") {
        $("#dvMessage").html("Please select payment option");
        return false;
    }
    else if (PaymentMode == "1" && parseFloat($("#lblAmount").html()) > parseFloat($("#spnWalletDashboard").html())) {
        $("#txnMsg").html("Insufficient Balance");
        $("#DivErrMessage").modal("show");
    }
    else {
        ServiceCharge($("#hdnFinalAmount").val());
        $('#PaymentPopup').modal('show');
        $("#MobilePopup").closest("tr").hide();
        $("#FlightBookingIdPopup").closest("tr").show();
        $("#MypayCoinDebitedPopup").closest("tr").show();
        $("#MypayCoinCashBackPopup").closest("tr").show();
        $("#CouponDiscountPopup").closest("tr").show();
        $("#TotalAmountPopup").closest("tr").show();
        $("#dvMessage").html("");
        $("#txnMsg").html("");
    }
});

$("#btnOkPopup").click(function () {
    $('#PaymentPopup').modal('hide');
    $("#DivPin").modal("show");
    $("#Pin").val("");
});
$("#btnPin").click(function () {
    Pay();
});