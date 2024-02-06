var discount = 0;
var payable = 0;

var WebResponse = '';
var cashback = "0";
var RewardPoints = "0";
var Passangers = "";

var DepartureAirlinelogo = "";
var DepartureAirlinename = "";
var DepartureFaretotal = "";
var DepartureRefundable = "";
var DepartureDeparturetime = "";
var DepartureDeparture = "";
var DepartureArrivaltime = "";
var DepartureArrival = "";
var DepartureFlightclasscode = "";
var DepartureFreebaggage = "";
var DepartureFlightNo = "";
var DepartureChildFare = "";
var DepartureAdultFare = "";
var DepartureFuelCharges = "";
var DepartureTax = "";
var DepartureFlightId = "";
var IsRoundSearch = false;
var IsRefundableOnly = false;
var oneWayJson = "";
var roundWayJson = "";

$(document).ready(function () {
    $('#AjaxLoader').show();
    $('#depart-update').datepicker({
        format: "yyyy-mm-dd",
        startDate: '+0d'
    }).on('changeDate', dateChanged);
    $('#depart-round-update').datepicker({
        format: "yyyy-mm-dd",
        startDate: '+0d'
    }).on('changeDate', roundDateChanged);
    $('#in-round-update').datepicker({
        format: "yyyy-mm-dd",
        startDate: '+0d'
    }).on('changeDate', roundDateChanged);
    setTimeout(
        function () {
            $("#dvSingleFlights").css("display", "none");
            SectorsFlightLoad();
        }, 10);

    window.scrollTo(0, 0);
});
$("#dv-From").click(function () {
    $("#ddlFrom").select2("open");
});
$("#dv-To").click(function () {
    $("#ddlTo").select2("open");
});
$("#dv-RoundFrom").click(function () {
    $("#ddlRoundFrom").select2("open");
});
$("#dv-RoundTo").click(function () {
    $("#ddlRoundTo").select2("open");
});

function SectorsFlightLoad() {
    var ServiceId = $("#hdnServiceID").val();
    $("#dvflightsuggest").hide();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").show();
    $.ajax({
        type: "POST",
        url: "/MyPayUser/MyPayUserFlightSectorDetails",
        data: '{"ServiceId":"' + parseInt(ServiceId) + '"}',
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
                var strFrom = "";
                var strTo = "";
                $("#ddlFrom").html("");
                $("#ddlTo").html("");
                $("#ddlRoundFrom").html("");
                $("#ddlRoundTo").html("");
                if (jsonData != null && jsonData.sectors != null && jsonData.sectors.length > 0) {
                    strFrom = '<option value="0">Origin</option>';
                    strTo = '<option value="0">Destination</option>';
                    for (var i = 0; i < jsonData.sectors.length; ++i) {
                        strFrom += '<option value="' + jsonData.sectors[i].code + '">' + jsonData.sectors[i].name + '</option>';
                        strTo += '<option value="' + jsonData.sectors[i].code + '">' + jsonData.sectors[i].name + '</option>';
                    }
                }
                else {
                    if (!IsValidJson) {
                        $("#dvMessage").html(response);
                    }
                    else {

                        $("#dvMessage").html("No Flights Found");
                    }
                }
                $("#ddlFrom").append(strFrom);
                $("#ddlTo").append(strTo);
                $("#ddlRoundFrom").append(strFrom);
                $("#ddlRoundTo").append(strTo);
            }
            else {
                $("#dvMessage").html("Something went wrong. Please try again later.");
                $('#AjaxLoader').hide();
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

$("#searchflight").click(function () {
    IsRoundSearch = false;
    $("#depart-update").val($("#depart").val());
    searchOneWay();
    $("#dvChangeDate").show();
    $("#dvChangeOutDate").hide();
    $("#dvChangeInDate").hide();

});


function dateChanged() {
    $("#depart").val($("#depart-update").val());
    searchOneWay();
}
function DepartureClick() {
    if (IsRoundSearch) {
        $("#dvChangeOutDate").show();
        $("#dvChangeInDate").hide();
    }
}

function ReturnClick() {
    $("#dvChangeInDate").show();
    $("#dvChangeOutDate").hide();

}

function searchOneWay() {
    $("#tab-return").hide();
    $("#dvflightsuggest").hide();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").show();
    var TripType = "O";
    $("#hdnTripType").val(TripType);
    var From = $("#ddlFrom :selected").val();
    var To = $("#ddlTo :selected").val();

    if (From == "" || From == "0") {
        $("#dvMessage").html("Please select Origin field");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }

    if (To == "" || To == "0") {
        $("#dvMessage").html("Please select Destination field");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }
    var Adults = $("#TravellerAdults").val();
    var child = $("#TravellerChild").val();

    if (Adults == 0 && child == 0) {
        $("#dvMessage").html("Please select atleast 1 Adult or Child");
        return false;
    }
    var totalTraveller = parseInt(Adults) + parseInt(child);
    if (totalTraveller > 10) {
        $("#dvMessage").html("Max 10 tickets allow to book once");
        return false;
    }
    var Departure = $("#depart").val();
    if (Departure == "") {
        $("#dvMessage").html("Please select Departure Date");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }
    $("#spnPlaceDetail").html("");
    var string = "";
    string += '' + From + ' <img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" width="11" alt="" class=""> ' + To + '';
    string += '<label class="m-0 fs-12px text-soft fw-normal w-100" > ' + Departure + ', ' + Adults + ' Adult, ' + child + ' Child</label>';


    $("#spnPlaceDetail").append(string);
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserFlightLookupDetails",
                data: '{"Origin":"' + From + '","Destination":"' + To + '","Adults":"' + Adults + '","child":"' + child + '","Departure":"' + Departure + '","TripType":"' + TripType + '"}',
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
                            oneWayJson = jsonData;
                            var IsValidJson = true;
                        }
                        catch (err) {

                        }
                        if (jsonData != null) {
                            BindOneWayList();
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

};


function BindOneWayList() {
    var FlightOutbound = "";
    var str = "";
    $("#departureflight").html("");
    $("#returnflight").html("");
    if (oneWayJson != null) {
        if (oneWayJson.FlightOutbound.length > 0) {
            if (IsRefundableOnly) {
                FlightOutbound = oneWayJson.FlightOutbound.filter((item) => item.Refundable === true);
            }
            else {
                FlightOutbound = oneWayJson.FlightOutbound;
            }
            if (FlightOutbound.length > 0) {
                for (var i = 0; i < FlightOutbound.length; ++i) {
                    str += '<div class="round box-shadow mt-4 cursor-pointer indv-flight" onclick="DetailView(&apos;' + FlightOutbound[i].Airlinelogo + '&apos;,&apos;' + FlightOutbound[i].Airlinename + '&apos;,&apos;' + FlightOutbound[i].Faretotal + '&apos;,&apos;' + FlightOutbound[i].Refundable + '&apos;,&apos;' + FlightOutbound[i].Departuretime + '&apos;,&apos;' + FlightOutbound[i].Departure + '&apos;,&apos;' + FlightOutbound[i].Arrivaltime + '&apos;,&apos;' + FlightOutbound[i].Arrival + '&apos;,&apos;' + FlightOutbound[i].Flightclasscode + '&apos;,&apos;' + FlightOutbound[i].Freebaggage + '&apos;,&apos;' + FlightOutbound[i].Flightno + '&apos;,&apos;' + FlightOutbound[i].Childfare + '&apos;,&apos;' + FlightOutbound[i].Adultfare + '&apos;,&apos;' + FlightOutbound[i].Fuelsurcharge + '&apos;,&apos;' + FlightOutbound[i].Tax + '&apos;,&apos;' + FlightOutbound[i].Flightid + '&apos;,&apos;' + oneWayJson.BookingId + '&apos;);">';
                    str += '<div class="d-flex border-bottom-light p-3 align-items-center justify-content-between">';
                    str += '<div class="d-flex align-items-center">';
                    str += '<img src="' + FlightOutbound[i].Airlinelogo + '" width="22">';
                    str += '<span class="fs-12px ml-1 mr-1 fw-medium line-height-1">' + FlightOutbound[i].Airlinename + '</span>';
                    str += '<div class="badge border-0 bg-lighter text-soft rounded-pill">' + FlightOutbound[i].Flightno + '</div>';
                    str += '</div>';
                    str += '<div>';
                    str += '<span class="fw-medium"><small>Rs.</small>' + FlightOutbound[i].Faretotal + '</span>';
                    if (FlightOutbound[i].Refundable) {
                        str += '<label class="fs-11px text-soft m-0 display-inherit">Refundable</label>';
                    }
                    else {
                        str += '<label class="fs-11px text-soft m-0 display-inherit">Non-Refundable</label>';
                    }
                    str += '</div>';
                    str += '</div>';
                    str += '<div class="p-3">';
                    str += '<div class="d-flex justify-content-between align-items-center">';
                    str += '<span class="fs-12px fw-medium">';
                    str += '' + FlightOutbound[i].Departuretime + '';
                    str += '<label class="fs-11px m-0 display-inherit fw-normal  text-soft">' + FlightOutbound[i].Departure + '</label>';
                    str += '</span>';
                    //str += '<div class="flight_duration fs-12px position-relative line-height-1">1h 20m</div>';
                    str += '<span class="fs-12px fw-medium text-right">';
                    str += '' + FlightOutbound[i].Arrivaltime + '';
                    str += '<label class="fs-11px m-0 display-inherit fw-normal  text-soft">' + FlightOutbound[i].Arrival + '</label>';
                    str += '</span>';
                    str += '</div>';
                    str += '<div class="d-flex mt-2 align-items-center justify-content-between">';
                    str += '<span class="fs-11px fw-normal line-height-1">';
                    str += 'Class : ' + FlightOutbound[i].Flightclasscode + '';
                    str += '</span>';
                    str += '<div class="badge-warning cashbackamt line-height-1 text-base rounded-pill">';
                    str += 'MyPay Coins : ' + FlightOutbound[i].RewardPoints + '';
                    str += '</div>';
                    str += '</div>';
                    str += '<div class="d-flex mt-1 align-items-center justify-content-between">';
                    str += '<span class="fs-11px fw-normal line-height-1">';
                    str += 'Total Luggage : ' + FlightOutbound[i].Freebaggage + '';
                    str += '</span>';
                    str += '<div class="badge-warning cashbackamt line-height-1 text-base rounded-pill">';
                    str += 'Rs. ' + FlightOutbound[i].Cashback + ' Cashback';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';
                }
                $("#departureflight").append(str);
                str = "";
            }
        }

        $("#dvflightsuggest").show();
        $("#dvDeparture").hide();
        $("#dvpassengerdetails").hide();
        $("#dvStep1").hide();
        $('#AjaxLoader').hide();
    }
}

$("#searchflightRound").click(function () {
    IsRoundSearch = true;
    $("#depart-round-update").val($("#Rounddepart").val());
    $("#in-round-update").val($("#Roundreturn").val());
    $("#dvChangeDate").hide();
    $("#dvChangeInDate").hide();
    $("#dvChangeOutDate").show();
    searchRound();

});
function roundDateChanged() {
    $("#dvMessage").html("");
    $("#departureflight").html("");
    $("#returnflight").html("");
    $("#Rounddepart").val($("#depart-round-update").val());
    $("#Roundreturn").val($("#in-round-update").val());
    var Departure = $("#Rounddepart").val();
    var Return = $("#Roundreturn").val();
    if (Date.parse(Departure) > Date.parse(Return)) {
        $("#dvMessage").html("Return date should be greater than Departure date");
        return false;
    }
    searchRound();
}
function searchRound() {
    $("#tab-return").show();
    $("#dvflightsuggest").hide();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").show();
    var TripType = "R";
    $("#hdnTripType").val(TripType);
    var From = $("#ddlRoundFrom :selected").val();
    var To = $("#ddlRoundTo :selected").val();

    if (From == "" || From == "0") {
        $("#dvMessage").html("Please select Origin field");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }

    if (To == "" || To == "0") {
        $("#dvMessage").html("Please select Destination field");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }
    var Adults = $("#RoundTravellerAdults").val();
    var child = $("#RoundTravellerChild").val();
    if (Adults == 0 && child == 0) {
        $("#dvMessage").html("Please select atleast 1 Adult or Child");
        return false;
    }
    var totalTraveller = parseInt(Adults) + parseInt(child);
    if (totalTraveller > 10) {
        $("#dvMessage").html("Max 10 tickets allow to book once");
        return false;
    }

    var Departure = $("#Rounddepart").val();
    if (Departure == "") {
        $("#dvMessage").html("Please select Departure Date");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }
    var Return = $("#Roundreturn").val();
    if (Return == "") {
        $("#dvMessage").html("Please select Return Date");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }

    if (Date.parse(Departure) > Date.parse(Return)) {
        $("#dvMessage").html("Return date should be greater than Departure date");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }
    $("#spnPlaceDetail").html("");
    var string = "";
    string += '' + From + ' <img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" width="11" alt="" class=""> ' + To + '';
    string += '<label class="m-0 fs-12px text-soft fw-normal w-100" > ' + Departure + ' - ' + Return + ', ' + Adults + ' Adult, ' + child + ' Child</label>';


    $("#spnPlaceDetail").append(string);
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserFlightLookupDetails",
                data: '{"Origin":"' + From + '","Destination":"' + To + '","Adults":"' + Adults + '","child":"' + child + '","Departure":"' + Departure + '","Return":"' + Return + '","TripType":"' + TripType + '"}',
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
                            roundWayJson = jsonData;
                            var IsValidJson = true;
                        }
                        catch (err) {

                        }
                        BindRoundList();

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
    //$('#AjaxLoader').hide();
}
function BindRoundList() {
    var FlightOutbound = "";
    var FlightInbound = "";

    var str = "";
    $("#departureflight").html("");
    $("#returnflight").html("");
    if (roundWayJson != null) {
        if (roundWayJson.FlightOutbound.length > 0) {
            if (IsRefundableOnly) {
                FlightOutbound = roundWayJson.FlightOutbound.filter((item) => item.Refundable === true);
            }
            else {
                FlightOutbound = roundWayJson.FlightOutbound;
            }
            if (FlightOutbound.length > 0) {
                $(".DepartureDetailView").css('background-color', 'white');
                for (var i = 0; i < FlightOutbound.length; ++i) {
                    str += '<div class="round box-shadow mt-4 cursor-pointer indv-flight DepartureDetailView" onclick="GetDepartureDetailView(&apos;' + FlightOutbound[i].Airlinelogo + '&apos;,&apos;' + FlightOutbound[i].Airlinename + '&apos;,&apos;' + FlightOutbound[i].Faretotal + '&apos;,&apos;' + FlightOutbound[i].Refundable + '&apos;,&apos;' + FlightOutbound[i].Departuretime + '&apos;,&apos;' + FlightOutbound[i].Departure + '&apos;,&apos;' + FlightOutbound[i].Arrivaltime + '&apos;,&apos;' + FlightOutbound[i].Arrival + '&apos;,&apos;' + FlightOutbound[i].Flightclasscode + '&apos;,&apos;' + FlightOutbound[i].Freebaggage + '&apos;,&apos;' + FlightOutbound[i].Flightno + '&apos;,&apos;' + FlightOutbound[i].Childfare + '&apos;,&apos;' + FlightOutbound[i].Adultfare + '&apos;,&apos;' + FlightOutbound[i].Fuelsurcharge + '&apos;,&apos;' + FlightOutbound[i].Tax + '&apos;,&apos;' + FlightOutbound[i].Flightid + '&apos;,&apos;' + roundWayJson.BookingId + '&apos;, this);">';
                    str += '<div class="d-flex border-bottom-light p-3 align-items-center justify-content-between">';
                    str += '<div class="d-flex align-items-center">';
                    str += '<img src="' + FlightOutbound[i].Airlinelogo + '" width="22">';
                    str += '<span class="fs-12px ml-1 mr-1 fw-medium line-height-1">' + FlightOutbound[i].Airlinename + '</span>';
                    str += '<div class="badge border-0 bg-lighter text-soft rounded-pill">' + FlightOutbound[i].Flightno + '</div>';
                    str += '</div>';
                    str += '<div>';
                    str += '<span class="fw-medium"><small>Rs.</small>' + FlightOutbound[i].Faretotal + '</span>';
                    if (FlightOutbound[i].Refundable) {
                        str += '<label class="fs-11px text-soft m-0 display-inherit">Refundable</label>';
                    }
                    else {
                        str += '<label class="fs-11px text-soft m-0 display-inherit">Non-Refundable</label>';
                    }
                    str += '</div>';
                    str += '</div>';
                    str += '<div class="p-3">';
                    str += '<div class="d-flex justify-content-between align-items-center">';
                    str += '<span class="fs-12px fw-medium">';
                    str += '' + FlightOutbound[i].Departuretime + '';
                    str += '<label class="fs-11px m-0 display-inherit fw-normal  text-soft">' + FlightOutbound[i].Departure + '</label>';
                    str += '</span>';
                    //str += '<div class="flight_duration fs-12px position-relative line-height-1">1h 20m</div>';
                    str += '<span class="fs-12px fw-medium text-right">';
                    str += '' + FlightOutbound[i].Arrivaltime + '';
                    str += '<label class="fs-11px m-0 display-inherit fw-normal  text-soft">' + FlightOutbound[i].Arrival + '</label>';
                    str += '</span>';
                    str += '</div>';
                    str += '<div class="d-flex mt-2 align-items-center justify-content-between">';
                    str += '<span class="fs-11px fw-normal line-height-1">';
                    str += 'Class : ' + FlightOutbound[i].Flightclasscode + '';
                    str += '</span>';
                    str += '<div class="badge-warning cashbackamt line-height-1 text-base rounded-pill">';
                    str += 'MyPay Coins : ' + FlightOutbound[i].RewardPoints + '';
                    str += '</div>';
                    str += '</div>';
                    str += '<div class="d-flex mt-1 align-items-center justify-content-between">';
                    str += '<span class="fs-11px fw-normal line-height-1">';
                    str += 'Total Luggage : ' + FlightOutbound[i].Freebaggage + '';
                    str += '</span>';
                    str += '<div class="badge-warning cashbackamt line-height-1 text-base rounded-pill">';
                    str += 'Rs. ' + FlightOutbound[i].Cashback + ' Cashback';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';
                }
                $("#departureflight").append(str);
                str = "";
            }
        }

        if (roundWayJson.FlightInbound.length > 0) {
            if (IsRefundableOnly) {
                FlightInbound = roundWayJson.FlightInbound.filter((item) => item.Refundable === true);
            }
            else {
                FlightInbound = roundWayJson.FlightInbound;
            }
            if (FlightInbound.length > 0) {
                for (var i = 0; i < FlightInbound.length; ++i) {
                    str += '<div class="round box-shadow mt-4 cursor-pointer indv-flight" onclick="BothDetailView(&apos;' + FlightInbound[i].Airlinelogo + '&apos;,&apos;' + FlightInbound[i].Airlinename + '&apos;,&apos;' + FlightInbound[i].Faretotal + '&apos;,&apos;' + FlightInbound[i].Refundable + '&apos;,&apos;' + FlightInbound[i].Departuretime + '&apos;,&apos;' + FlightInbound[i].Departure + '&apos;,&apos;' + FlightInbound[i].Arrivaltime + '&apos;,&apos;' + FlightInbound[i].Arrival + '&apos;,&apos;' + FlightInbound[i].Flightclasscode + '&apos;,&apos;' + FlightInbound[i].Freebaggage + '&apos;,&apos;' + FlightInbound[i].Flightno + '&apos;,&apos;' + FlightInbound[i].Childfare + '&apos;,&apos;' + FlightInbound[i].Adultfare + '&apos;,&apos;' + FlightInbound[i].Fuelsurcharge + '&apos;,&apos;' + FlightInbound[i].Tax + '&apos;,&apos;' + FlightInbound[i].Flightid + '&apos;,&apos;' + roundWayJson.BookingId + '&apos;);">';
                    str += '<div class="d-flex border-bottom-light p-3 align-items-center justify-content-between">';
                    str += '<div class="d-flex align-items-center">';
                    str += '<img src="' + FlightInbound[i].Airlinelogo + '" width="22">';
                    str += '<span class="fs-12px ml-1 mr-1 fw-medium line-height-1">' + FlightInbound[i].Airlinename + '</span>';
                    str += '<div class="badge border-0 bg-lighter text-soft rounded-pill">' + FlightInbound[i].Flightno + '</div>';
                    str += '</div>';
                    str += '<div>';
                    str += '<span class="fw-medium"><small>Rs.</small>' + FlightInbound[i].Faretotal + '</span>';
                    if (FlightInbound[i].Refundable) {
                        str += '<label class="fs-11px text-soft m-0 display-inherit">Refundable</label>';
                    }
                    else {
                        str += '<label class="fs-11px text-soft m-0 display-inherit">Non-Refundable</label>';
                    }
                    str += '</div>';
                    str += '</div>';
                    str += '<div class="p-3">';
                    str += '<div class="d-flex justify-content-between align-items-center">';
                    str += '<span class="fs-12px fw-medium">';
                    str += '' + FlightInbound[i].Departuretime + '';
                    str += '<label class="fs-11px m-0 display-inherit fw-normal  text-soft">' + FlightInbound[i].Departure + '</label>';
                    str += '</span>';
                    //str += '<div class="flight_duration fs-12px position-relative line-height-1">1h 20m</div>';
                    str += '<span class="fs-12px fw-medium text-right">';
                    str += '' + FlightInbound[i].Arrivaltime + '';
                    str += '<label class="fs-11px m-0 display-inherit fw-normal  text-soft">' + FlightInbound[i].Arrival + '</label>';
                    str += '</span>';
                    str += '</div>';
                    str += '<div class="d-flex mt-2 align-items-center justify-content-between">';
                    str += '<span class="fs-11px fw-normal line-height-1">';
                    str += 'Class : ' + FlightInbound[i].Flightclasscode + '';
                    str += '</span>';
                    str += '<div class="badge-warning cashbackamt line-height-1 text-base rounded-pill">';
                    str += 'MyPay Coins : ' + FlightInbound[i].RewardPoints + '';
                    str += '</div>';
                    str += '</div>';
                    str += '<div class="d-flex mt-1 align-items-center justify-content-between">';
                    str += '<span class="fs-11px fw-normal line-height-1">';
                    str += 'Total Luggage : ' + FlightInbound[i].Freebaggage + '';
                    str += '</span>';
                    str += '<div class="badge-warning cashbackamt line-height-1 text-base rounded-pill">';
                    str += 'Rs. ' + FlightInbound[i].Cashback + ' Cashback';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';
                    str += '</div>';
                }
                $("#returnflight").append(str);
                str = "";
            }
        }
        $("#dvflightsuggest").show();
        $("#dvDeparture").hide();
        $("#dvpassengerdetails").hide();
        $("#dvStep1").hide();
        $('#AjaxLoader').hide();
    }
    else {
        $("#dvMsg").html(response);
        $('#AjaxLoader').hide();
        return false;
    }
}
$("#btnBackStep1").click(function () {
    $("#dvMsg").html("");
    $("#dvMessage").html("");
    $("#dvflightsuggest").hide();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").show();
});
function backPassenger() {
    $("#dvMsg").html("");
    $("#dvMessage").html("");
    $("#dvflightsuggest").hide();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").show();
}
function btnBackStep2Click() {
    $("#dvMsg").html("");
    $("#dvMessage").html("");
    $("#dvflightsuggest").show();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").hide();
}

function btnBackBothStep2Click() {
    $("#dvMsg").html("");
    $("#dvMessage").html("");
    $("#dvflightsuggest").show();
    $("#dvDeparture").hide();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").hide();
}

function ServiceCharge(Amount) {
    cashback = "0";
    RewardPoints = "0";
    var IsCouponUnlocked = false;
    $("#CouponDiscountPopup").text("0");
    var ServiceId = $("#hdnServiceID").val();
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
                        //$("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(arr['WalletAmountDeduct']);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        cashback = arr['CashbackAmount'];
                        RewardPoints = arr['RewardPoints'];
                        IsCouponUnlocked = arr['IsCouponUnlocked'];
                        $("#detail-cashback").text("Rs. " + cashback + " Cashback");
                        $("#detail-RewardPoints").text("MyPay Coins: " + RewardPoints);
                        $("#lblAmount").text(arr['Amount']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
                        $("#lblDivAmount").text(arr['Amount']);
                        $("#Amount").val($("#lblDivAmount").text());
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
                            var totalamount = parseFloat((DeductAmount + DeductServiceCharge) - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));
                        }
                        else {
                            $("#AmountPopup").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = parseFloat((Amount + ServiceCharge) - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));

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

function DetailView(Airlinelogo, Airlinename, Faretotal, Refundable, Departuretime, Departure, Arrivaltime, Arrival, Flightclasscode, Freebaggage, FlightNo, ChildFare, AdultFare, FuelCharges, Tax, FlightId, BookingId) {

    $("#dvMessage").html("");
    $("#hdnFlightId").val(FlightId);
    $("#hdnBookingId").val(BookingId);
    var From = $("#ddlFrom :selected").val();
    var To = $("#ddlTo :selected").val();
    var DepartureDate = $("#depart").val();
    var Adults = $("#TravellerAdults").val();
    var child = $("#TravellerChild").val();
    var Travellers = 0;
    Travellers = parseInt(Adults) + parseInt(child);
    var TotalAdultFare = Adults * AdultFare;
    var TotalChildFare = child * ChildFare;
    $("#dvDepartureView").html("");
    ServiceCharge(Faretotal);
    $("#hdnFinalAmount").val(Faretotal);

    cashback = "100";
    RewardPoints = "100";

    var str = "";
    str = '<a href="javascript:void(0)" id="btnBackStep2" onclick="btnBackStep2Click()" class="btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white float-right"> Back</a>';
    str += '<div class="d-flex align-items-center">';
    str += '<img src="' + Airlinelogo + '" width="26">';
    str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + Airlinename + '</span>';
    str += '</div>';
    str += '<div class="fs-15px text-uppercase fw-medium mt-3 mb-0">' + From + ' - ' + To + '</div>';
    str += '<div class="d-flex align-items-center">';
    str += '<span class="fs-12px">' + Departure + '</span>';
    str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
    str += '<span class="fs-12px">' + Arrival + '</span>';
    str += '</div>';
    str += '<div class="text-soft mt-2 fs-13px">';
    str += '<span class="w-100 display-inherit">' + DepartureDate + '</span>';
    str += '<span class="w-100 display-inherit">' + Departuretime + ' - ' + Arrivaltime + '</span>';

    if (Refundable === "true") {
        str += '<span class="w-100 display-inherit">' + FlightNo + ' | ' + Flightclasscode + ' | Refundable</span>';
    }
    else {
        str += '<span class="w-100 display-inherit">' + FlightNo + ' | ' + Flightclasscode + ' | Non-Refundable</span>';
    }
    str += '<span class="w-100 display-inherit">Total Luggage: ' + Freebaggage + '</span>';
    str += '</div>';
    str += '<div class="mt-3 mb-4 bg-lighter line-height-1 text-soft">';
    str += '<a href="javascript:void(0)" class="px-3 pt-2 pb-2 fs-13px fw-medium text-soft d-flex align-items-center faresummary active">Fare Summary <em class="icon ni ni-downward-ios ml-1"></em></a>';
    str += '<div class="text-soft fs-13px px-3 pb-3 mt-1 faresummarydet">';
    str += '<span class="d-flex justify-content-between fw-light mb-1">Travellers <label class="fw-medium m-0">' + Travellers + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">' + Adults + ' Adult x Rs. ' + AdultFare + ' <label class="fw-medium m-0">' + TotalAdultFare + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">' + child + ' Child x Rs. ' + ChildFare + ' <label class="fw-medium m-0">' + TotalChildFare + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">Fuel Charges <label class="fw-medium m-0">' + FuelCharges + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light">Tax <label class="fw-medium m-0">' + Tax + '</label></span>';
    str += '</div>';
    str += '</div>';
    str += '<div class="d-flex justify-content-between">';
    str += '<span class="fw-medium fs-15px">Total Amount</span>';
    str += '<div class="text-right">';
    str += '<span class="fw-medium text-right fs-15px"><small>Rs.</small>' + Faretotal + '</span>';
    str += '<label id="detail-cashback" class="badge-warning cashbackamt line-height-1 text-base rounded-pill m-0 display-inherit">Rs. ' + cashback + ' Cashback</label>';
    str += '<label id="detail-RewardPoints" class=" badge-warning cashbackamt line-height-1 text-base rounded-pill mt-1">MyPay Coins: ' + RewardPoints + '</label>';
    str += '</div>';
    str += '</div>';
    str += '<a href="javascript:void(0)" class="btn btn-primary btn-lg d-block mt-4" data-toggle="modal" data-target="#confirmFlight">Proceed</a>';
    $("#dvDepartureView").append(str);
    $("#dvflightsuggest").hide();
    $("#dvDeparture").show();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").hide();
    $("#dvBothView").hide();
    $('#AjaxLoader').hide();

}


function GetDepartureDetailView(Airlinelogo, Airlinename, Faretotal, Refundable, Departuretime, Departure, Arrivaltime, Arrival, Flightclasscode, Freebaggage, FlightNo, ChildFare, AdultFare, FuelCharges, Tax, FlightId, BookingId, obj) {

    $(".DepartureDetailView").css('background-color', 'white');
    DepartureAirlinelogo = Airlinelogo;
    DepartureAirlinename = Airlinename;
    DepartureFaretotal = Faretotal;
    DepartureRefundable = Refundable;
    DepartureDeparturetime = Departuretime;
    DepartureDeparture = Departure;
    DepartureArrivaltime = Arrivaltime;
    DepartureArrival = Arrival;
    DepartureFlightclasscode = Flightclasscode;
    DepartureFreebaggage = Freebaggage;
    DepartureFlightNo = FlightNo;
    DepartureChildFare = ChildFare;
    DepartureAdultFare = AdultFare;
    DepartureFuelCharges = FuelCharges;
    DepartureTax = Tax;
    DepartureFlightId = FlightId;
    $(obj).css('background-color', 'lightyellow');
}

function BothDetailView(Airlinelogo, Airlinename, Faretotal, Refundable, Departuretime, Departure, Arrivaltime, Arrival, Flightclasscode, Freebaggage, FlightNo, ChildFare, AdultFare, FuelCharges, Tax, ReturnFlightId, BookingId) {

    $("#dvMessage").html("");
    if (DepartureFlightId == "") {
        $("#dvMessage").html("Please select Departure Flight");
        return false;
    }
    else {
        $("#dvMessage").html("");
    }
    $("#hdnFlightId").val(DepartureFlightId);
    $("#hdnBookingId").val(BookingId);
    $("#hdnReturnFlightId").val(ReturnFlightId);
    var From = $("#ddlRoundFrom :selected").val();
    var To = $("#ddlRoundTo :selected").val();
    var DepartureDate = $("#Rounddepart").val();
    var ReturnDate = $("#Roundreturn").val();
    var Adults = $("#RoundTravellerAdults").val();
    var child = $("#RoundTravellerChild").val();
    var DepartureTravellers = 0;
    DepartureTravellers = parseInt(Adults) + parseInt(child);
    var TotalDepartureAdultFare = Adults * DepartureAdultFare;
    var TotalDepartureChildFare = child * DepartureChildFare;
    //var TotalDepartureFuelCharges = DepartureTravellers * DepartureFuelCharges;
    //var TotalDepartureTax = DepartureTravellers * DepartureTax;

    var Travellers = 0;
    Travellers = parseInt(Adults) + parseInt(child);
    var TotalAdultFare = Adults * AdultFare;
    var TotalChildFare = child * ChildFare;
    $("#dvBothView").html("");

    var TotalFare = parseInt(Faretotal) + parseInt(DepartureFaretotal);
    ServiceCharge(TotalFare);
    $("#hdnFinalAmount").val(TotalFare);
    var str = "";
    str = '<div class="col-md-10 col-lg-7 col-xl-7 col-xxl-5 m-auto">';
    str += '<a href="javascript:void(0)" id="btnBackBothStep2" onclick="btnBackBothStep2Click()" class="btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white float-right mt-5"> Back</a>';
    str += '<div class="">';
    str += '<h5 class="title mt-5 mb-3">Departure Flights Details</h5>';
    str += '<div class="d-flex align-items-center">';
    str += '<img src="' + DepartureAirlinelogo + '" width="26">';
    str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + DepartureAirlinename + '</span>';
    str += '</div>';
    str += '<div class="fs-15px text-uppercase fw-medium mt-3 mb-0">' + From + ' - ' + To + '</div>';
    str += '<div class="d-flex align-items-center">';
    str += '<span class="fs-12px">' + DepartureDeparture + '</span>';
    str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
    str += '<span class="fs-12px">' + DepartureArrival + '</span>';
    str += '</div>';
    str += '<div class="text-soft mt-2 fs-13px">';
    str += '<span class="w-100 display-inherit">' + DepartureDate + '</span>';
    str += '<span class="w-100 display-inherit">' + DepartureDeparturetime + ' - ' + DepartureArrivaltime + '</span>';
    if (DepartureRefundable === "true") {
        str += '<span class="w-100 display-inherit">' + DepartureFlightNo + ' | ' + DepartureFlightclasscode + ' | Refundable</span>';
    }
    else {
        str += '<span class="w-100 display-inherit">' + DepartureFlightNo + ' | ' + DepartureFlightclasscode + ' | Non-Refundable</span>';
    }
    str += '<span class="w-100 display-inherit">Total Luggage: ' + DepartureFreebaggage + '</span>';
    str += '</div>';
    str += '<div class="mt-3 mb-4 bg-lighter line-height-1 text-soft">';
    str += '<a href="javascript:void(0)" class="px-3 pt-2 pb-2 fs-13px fw-medium text-soft d-flex align-items-center faresummary">Fare Summary <em class="icon ni ni-downward-ios ml-1"></em></a>';
    str += '<div class="text-soft fs-13px px-3 pb-3 mt-1 faresummarydet">';
    str += '<span class="d-flex justify-content-between fw-light mb-1">Travellers <label class="fw-medium m-0">' + DepartureTravellers + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">' + Adults + ' Adult x Rs. ' + DepartureAdultFare + '<label class="fw-medium m-0">' + TotalDepartureAdultFare + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">' + child + ' Child x Rs. ' + DepartureChildFare + ' <label class="fw-medium m-0">' + TotalDepartureChildFare + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">Fuel Charges <label class="fw-medium m-0">' + DepartureFuelCharges + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light">Tax <label class="fw-medium m-0">' + DepartureTax + '</label></span>';
    str += '</div>';
    str += '</div>';

    str += '<h5 class="title mt-5 mb-3">Return Flights Details</h5>';
    str += '<div class="d-flex align-items-center">';
    str += '<img src="' + Airlinelogo + '" width="26">';
    str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + Airlinename + '</span>';
    str += '</div>';
    str += '<div class="fs-15px text-uppercase fw-medium mt-3 mb-0">' + To + ' - ' + From + '</div>';
    str += '<div class="d-flex align-items-center">';
    str += '<span class="fs-12px">' + Departure + '</span>';
    str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
    str += '<span class="fs-12px">' + Arrival + '</span>';
    str += '</div>';
    str += '<div class="text-soft mt-2 fs-13px">';
    str += '<span class="w-100 display-inherit">' + ReturnDate + '</span>';
    str += '<span class="w-100 display-inherit">' + Departuretime + ' - ' + Arrivaltime + '</span>';
    if (Refundable === "true") {
        str += '<span class="w-100 display-inherit">' + FlightNo + ' | ' + Flightclasscode + ' | Refundable</span>';
    }
    else {
        str += '<span class="w-100 display-inherit">' + FlightNo + ' | ' + Flightclasscode + ' | Non-Refundable</span>';
    }
    str += '<span class="w-100 display-inherit">Total Luggage: ' + Freebaggage + '</span>';
    str += '</div>';
    str += '<div class="mt-3 mb-4 bg-lighter line-height-1 text-soft">';
    str += '<a href="javascript:void(0)" class="px-3 pt-2 pb-2 fs-13px fw-medium text-soft d-flex align-items-center faresummary">Fare Summary <em class="icon ni ni-downward-ios ml-1"></em></a>';
    str += '<div class="text-soft fs-13px px-3 pb-3 mt-1 faresummarydet">';
    str += '<span class="d-flex justify-content-between fw-light mb-1">Travellers <label class="fw-medium m-0">' + Travellers + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">' + Adults + ' Adult x Rs. ' + AdultFare + '<label class="fw-medium m-0">' + TotalAdultFare + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">' + child + ' Child x Rs. ' + ChildFare + ' <label class="fw-medium m-0">' + TotalChildFare + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light mb-1">Fuel Charges <label class="fw-medium m-0">' + FuelCharges + '</label></span>';
    str += '<span class="d-flex justify-content-between fw-light">Tax <label class="fw-medium m-0">' + Tax + '</label></span>';
    str += '</div>';
    str += '</div>';

    str += '<div class="d-flex justify-content-between">';
    str += '<span class="fw-medium fs-15px">Total Amount</span>';
    str += '<div class="text-right">';
    str += '<span class="fw-medium text-right fs-15px"><small>Rs.</small>' + TotalFare + '</span>';
    str += '<label id="detail-cashback" class="badge-warning cashbackamt line-height-1 text-base rounded-pill m-0 display-inherit">Rs. ' + cashback + ' Cashback</label>';
    str += '<label id="detail-RewardPoints" class=" badge-warning cashbackamt line-height-1 text-base rounded-pill mt-1">MyPay Coins: ' + RewardPoints + '</label>';
    str += '</div>';
    str += '</div>';
    str += '<a href="javascript:void(0)" class="btn btn-primary btn-lg d-block mt-4" id="" data-toggle="modal" data-target="#confirmFlight">Proceed</a>';
    str += '</div>';
    str += '</div>';

    $("#dvBothView").append(str);
    $("#dvflightsuggest").hide();
    $("#dvDeparture").show();
    $("#dvpassengerdetails").hide();
    $("#dvStep1").hide();
    $("#dvDepartureView").hide();
    $("#hdnFinalAmount").val(TotalFare);
    $('#AjaxLoader').hide();

}
$("#btnBookFlight").click(function () {
    $("#dvMessage").html("");
    var FlightId = $("#hdnFlightId").val();
    var BookingId = $("#hdnBookingId").val();
    var ReturnFlightId = $("#hdnReturnFlightId").val();
    if (FlightId == "") {
        alert("Please select Flight to book");
    }
    else if (BookingId == "") {
        alert("Please select Flight to book");
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserFlightBook",
                    data: '{"FlightId":"' + FlightId + '","BookingId":"' + BookingId + '","ReturnFlightId":"' + ReturnFlightId + '"}',
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
                            $("#detailMessage").html("");
                            if (jsonData != null && jsonData.Message == "Success") {
                                $("#detailMessage").html(jsonData.Details);
                                $("#confirmFlight").modal('hide');
                                $("#confirmFlight2").modal('show');

                                $('#AjaxLoader').hide();
                            }
                            else {
                                if (!IsValidJson) {
                                    $("#dvMessage").html(response);

                                }
                                else {

                                    $("#dvMessage").html("No Flights booked, Please try again.");
                                }
                                $("#confirmFlight").modal('hide');
                                $('#AjaxLoader').hide();
                            }
                        }
                        else {
                            $("#confirmFlight").modal('hide');
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
});

$("#btnOK").click(function () {
    var serviceId = $("#hdnServiceID").val();

    $("#dvMessage").html("");
    var Adults = "";
    var child = "";
    if ($("#hdnTripType").val() == "O") {
        Adults = $("#TravellerAdults").val();
        child = $("#TravellerChild").val();
    }
    else if ($("#hdnTripType").val() == "R") {
        Adults = $("#RoundTravellerAdults").val();
        child = $("#RoundTravellerChild").val();
    }
    else {
        $("#dvMessage").html("Please select Trip Type");
        return false;
    }
    $("#dvpassengerdetails").html("");
    var str = "";
    str = '<div class="row">';
    str += '<div class="col-md-10 col-lg-7 col-xl-7 col-xxl-5 m-auto">';
    str += '<div class="card-title-group mb-4">';
    str += '<div class="card-title d-flex w-100 align-items-center pb-3 border-bottom">';
    str += '        <h6 class="title mb-0 d-flex align-items-center">';
    str += '            <span>Passenger Details</span>';
    str += '        </h6>';
    str += '        <a href="javascript:void(0)" onclick="backPassenger()" class="btn btn-sm btn-danger ml-auto color-black d-flex align-items-center text-white"> Back</a>';
    str += '    </div>';
    str += '</div>';
    str += '<div class="badge badge-dim bg-outline-warning p-2">';
    str += '    <img src="/Content/assets/MyPayUserApp/images/info.svg" width="15">';
    str += '        <p class="m-0 ml-2 fs-12px">Please ensure spelling of passenger name and other details <br />match with government  approved IDs as these cannot be <br />changed later. Errors might lead to cancellation penalties.</p>';
    str += '                                        </div>';
    for (var i = 1; i <= Adults; ++i) {
        str += '    <h6 class="fs-16px mt-4 mb-2">Adult ' + i + ' Details</h6>';
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

        if (serviceId == 104) {
            str += '            <div class="col-12 mt-3">';
            str += '                <div class="form-group">';
            str += '                    <div class="form-control-wrap divRemarks">';
            str += '                            <label class="form-label" id = "iremarks" for="remarks">Remarks <strong class="text-danger">*</strong></label>';
            str += '                           <input type="text" value="" class="form-control form-control-lg rounded" id="remarksAdult' + i + '">';
            str += '                                                </div>';
            str += '                    </div>';
            str += '                </div>';
        }
        else {

        }


    }
    for (var j = 1; j <= child; ++j) {
        str += '    <h6 class="fs-16px mt-4 mb-2">Child ' + j + ' Details</h6>';
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

        if (serviceId == 104) {
            str += '            <div class="col-12 mt-3" divRemarks>';
            str += '                <div class="form-group">';
            str += '                    <div class="form-control-wrap">';
            str += '                            <label class="form-label" id = "iremarks" for="remarks">Remarks <strong class="text-danger">*</strong></label>';
            str += '                           <input type="text" class="form-control form-control-lg rounded" id="remarkschild' + j + '">';
            str += '                                                </div>';
            str += '                    </div>';
            str += '                </div>';
        }
        else
        {

        }
    }

    str += '                <div class="col-12 mt-5">';
    str += '                    <h6 class="fs-16px mb-0">Contact Person Details</h6>';
    str += '                    <p class="m-0 text-soft fs-12px">Booking details will be sent to below contact details</p>';
    str += '                </div>';
    str += '                <div class="col-12 mt-3">';
    str += '                    <div class="form-group">';
    str += '                        <div class="form-control-wrap">';
    str += '                            <input type="text" class="form-control form-control-lg rounded fw-medium text-base" placeholder="Contact number" id="txtContactNumber" maxlength="10" onkeypress="return isNumberKey(this, event);" value="' + $("#hdnContactNumber").val() + '" >';
    str += '                                                    </div>';
    str += '                        </div>';
    str += '                    </div>';
    str += '                    <div class="col-12 mt-3">';
    str += '                        <div class="form-group">';
    str += '                            <div class="form-control-wrap">';
    str += '                                <input type="text" class="form-control form-control-lg rounded fw-medium text-base" placeholder="Name" id="txtContactName" value="' + $("#hdnContactName").val() + '">';
    str += '                                                    </div>';
    str += '                            </div>';
    str += '                        </div>';
    str += '                        <div class="col-12 mt-3">';
    str += '                            <div class="form-group">';
    str += '                                <div class="form-control-wrap">';
    str += '                                    <input type="text" class="form-control form-control-lg rounded fw-medium text-base" placeholder="Email" id="txtContactEmail" value="' + $("#hdnContactEmail").val() + '">';
    str += '                                                    </div>';
    str += '                                </div>';
    str += '                            </div>';
    str += '                            <div class="col-12 mt-4 mt-sm-5 text-center">';
    str += '                                <a href="javascript:void(0)" class="btn btn-primary btn-lg d-block" id="btnProceedBooking" onclick="ProceedBooking();">Proceed Booking</a>';
    str += '                            </div>';
    str += '                        </div>';
    str += '                    </div>';
    str += '                </div>';

    $("#dvpassengerdetails").append(str);

    $("#dvpassengerdetails").show();
});
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
function ProceedBooking() {
    $("#dvMessage").html("");
    var Adults = "";
    var child = "";
    if ($("#hdnTripType").val() == "O") {
        Adults = $("#TravellerAdults").val();
        child = $("#TravellerChild").val();
    }
    else if ($("#hdnTripType").val() == "R") {
        Adults = $("#RoundTravellerAdults").val();
        child = $("#RoundTravellerChild").val();
    }
    else {
        $("#dvMessage").html("Please select Trip Type");
        return false;
    }

    var Gender = "";
    var strPopup = "";
    $("#popupDetail").html("");
    Passangers = "";
    Passangers += "[";
    for (var i = 1; i <= Adults; ++i) {
        if ($("#titleAdult" + i + " :selected").val() == "" || $("#titleAdult" + i + " :selected").val() == "0") {
            $("#dvMessage").html("Please select Adult " + i + " title");
            return false;
        }
        else if ($("#countryAdult" + i + " :selected").val() == "" || $("#countryAdult" + i + " :selected").val() == "0") {
            $("#dvMessage").html("Please select Adult " + i + " country");
            return false;
        }
        else if ($("#fnameAdult" + i).val() == "") {
            $("#dvMessage").html("Please enter Adult " + i + " first name");
            return false;
        }
        else if ($("#lnameAdult" + i).val() == "") {
            $("#dvMessage").html("Please enter Adult " + i + " last name");
            return false;
        }
        else if ($("#remarksAdult" + i).val() == "") {
            $("#dvMessage").html("Please enter Adult " + i + " remarks");
            return false;
        }
        if ($("#titleAdult" + i + " :selected").val() == "MR") {
            Gender = "M";
        }
        else {
            Gender = "F";
        }
        Passangers += '{"FirstName":"' + $("#fnameAdult" + i).val() + '","LastName":"' + $("#lnameAdult" + i).val() + '","Remarks":"' + $("#remarksAdult" + i).val() + '","Type":"ADULT","Title":"' + $("#titleAdult" + i + " :selected").val() + '","Gender":"' + Gender + '","Nationality":"' + $("#countryAdult" + i + " :selected").val() + '"}';

        if (i < Adults) {
            Passangers = Passangers + ",";
        }
        else if (child > 0) {
            Passangers = Passangers + ",";
        }
        strPopup += 'Adult ' + i + ' : ' + $("#titleAdult" + i + " :selected").val() + " " + $("#fnameAdult" + i).val() + " " + $("#lnameAdult" + i).val() + '<br/>';

    }
    for (var j = 1; j <= child; ++j) {
        if ($("#titlechild" + j + " :selected").val() == "" || $("#titlechild" + j + " :selected").val() == "0") {
            $("#dvMessage").html("Please select child " + j + " title");
            return false;
        }
        else if ($("#countrychild" + j + " :selected").val() == "" || $("#countrychild" + j + " :selected").val() == "0") {
            $("#dvMessage").html("Please select child " + j + " country");
            return false;
        }
        else if ($("#fnamechild" + j).val() == "") {
            $("#dvMessage").html("Please enter child " + j + " first name");
            return false;
        }
        else if ($("#lnamechild" + j).val() == "") {
            $("#dvMessage").html("Please enter child " + j + " last name");
            return false;
        }
        else if ($("#remarkschild" + j).val() == "") {
            $("#dvMessage").html("Please enter child " + j + " remarks");
            return false;
        }
        if ($("#titlechild" + j + " :selected").val() == "MR") {
            Gender = "M";
        }
        else {
            Gender = "F";
        }
        Passangers += '{"FirstName":"' + $("#fnamechild" + j).val() + '","LastName":"' + $("#lnamechild" + j).val() + '","Remarks":"' + $("#remarkschild" + j).val() + '","Type":"CHILD","Title":"' + $("#titlechild" + j + " :selected").val() + '","Gender":"' + Gender + '","Nationality":"' + $("#countrychild" + j + " :selected").val() + '"}';

        if (j < child) {
            Passangers = Passangers + ",";
        }
        strPopup += 'Child ' + j + ' : ' + $("#titlechild" + j + " :selected").val() + " " + $("#fnamechild" + j).val() + " " + $("#lnamechild" + j).val() + '<br/>';
    }
    Passangers += "]";

    var ContactNumber = $("#txtContactNumber").val();
    var ContactName = $("#txtContactName").val();
    var ContactEmail = $("#txtContactEmail").val();
    if (ContactNumber == "") {
        $("#dvMessage").html("Please enter Contact Person Number");
        return false;
    }
    else if (ContactName == "") {
        $("#dvMessage").html("Please enter Contact Person Name");
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
    $("#dvMessage").html("");
    debugger
    var ContactNumber = $("#txtContactNumber").val();
    var ContactName = $("#txtContactName").val();
    var ContactEmail = $("#txtContactEmail").val();
    var BookingId = $("#hdnBookingId").val();
    var FlightId = $("#hdnFlightId").val();
    var serviceId = $("#hdnServiceID").val();
    var ReturnFlightId = $("#hdnReturnFlightId").val();
    $('#AjaxLoader').show();
    if (Passangers != "") {
        //Passangers = JSON.stringify(Passangers);

        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserFlightPassangerDetails",
                    data: '{"BookingID":"' + BookingId + '","FlightID":"' + FlightId + '","ReturnFlightID":"' + ReturnFlightId + '","ContactName":"' + ContactName + '","ContactPhone":"' + ContactNumber + '","ContactEmail":"' + ContactEmail + '","PassengersClassString":"' + Passangers.replaceAll("\"", "'") + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        debugger
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
                                GetPassengerDetail(BookingId, FlightId, ReturnFlightId);
                                $("#dvpassengerdetails").hide();
                                $('#AjaxLoader').hide();
                            }
                            else {
                                if (!IsValidJson) {
                                    $("#dvMessage").html(response);

                                }
                                else {

                                    $("#dvMessage").html("No Passenger detail added, Please try again.");
                                }
                                $("#confirmPassengerDetails").modal('hide');
                                $('#AjaxLoader').hide();
                            }
                        }
                        else {
                            $("#confirmPassengerDetails").modal('hide');
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
});

function GetPassengerDetail(BookingId, FlightId, ReturnFlightId) {
    debugger
    $("#dvMessage").html("");
    $('#AjaxLoader').show();
    if (BookingId == "undefined") {
        BookingId = "";
    }
    //if (BookingId != "") {

    //    setTimeout(
    //        function () {
    //            $.ajax({
    //                type: "POST",
    //                url: "/MyPayUser/MyPayUserGetFlightPassengerDetail",
    //                data: '{"BookingID":"' + BookingId + '"}',
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                async: false,
    //                success: function (response) {
    //                    if (response != null) {
    //                        var objBank = '';
    //                        var jsonData;
    //                        var IsValidJson = false;
    //                        try {
    //                            jsonData = $.parseJSON(response);
    //                            var IsValidJson = true;
    //                        }
    //                        catch (err) {

    //                        }
    //                        $("#tblFlightDetail").html("");
    //                        var str = "";
    //                        if (jsonData != null && jsonData.Message == "success") {
    //                            if ($("#hdnTripType").val() == "O") {
    //                                if (jsonData.OutBounds.length > 0) {
    //                                    for (var i = 0; i < jsonData.OutBounds.length; ++i) {
    //                                        str += '<div class="d-flex align-items-center">';
    //                                        str += '<img src="' + jsonData.OutBounds[i].Airlinelogo + '" width="26">';
    //                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds[i].Airlinename + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="d-flex align-items-center">';
    //                                        str += '<span class="fs-12px">' + jsonData.OutBounds[i].Departure + '</span>';
    //                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
    //                                        str += '<span class="fs-12px">' + jsonData.OutBounds[i].Arrival + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="text-soft mt-2 fs-13px">';
    //                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightdatedt + '</span>';
    //                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Departuretime + ' - ' + jsonData.OutBounds[i].Arrivaltime + '</span>';
    //                                        if (jsonData.OutBounds[i].Refundable) {
    //                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightno + ' | ' + jsonData.OutBounds[i].Flightclasscode + ' | Refundable</span>';
    //                                        }
    //                                        else {
    //                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightno + ' | ' + jsonData.OutBounds[i].Flightclasscode + ' | Non-Refundable</span>';
    //                                        }
    //                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds[i].Freebaggage + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="border-top pt-4 mt-4">';
    //                                        str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">';
    //                                        for (var j = 1; j <= jsonData.OutBounds[i].PassengersDetails.length; ++j) {
    //                                            str += '' + j + '. ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Title + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Firstname + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Lastname + ' <br/>';
    //                                        }
    //                                        str += '</span >';
    //                                        str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactName + '</span>';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactPhone + '</span>';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactEmail + '</span>';
    //                                        str += '</div>';
    //                                    }
    //                                }
    //                            }
    //                            else if ($("#hdnTripType").val() == "R") {
    //                                if (jsonData.OutBounds.length > 0) {
    //                                    for (var k = 0; k < jsonData.OutBounds.length; ++k) {
    //                                        str += '<div class="d-flex align-items-center">';
    //                                        str += '<img src="' + jsonData.OutBounds[k].Airlinelogo + '" width="26">';
    //                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds[k].Airlinename + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="d-flex align-items-center">';
    //                                        str += '<span class="fs-12px">' + jsonData.OutBounds[k].Departure + '</span>';
    //                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
    //                                        str += '<span class="fs-12px">' + jsonData.OutBounds[k].Arrival + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="text-soft mt-2 fs-13px">';
    //                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Flightdatedt + '</span>';
    //                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Departuretime + ' - ' + jsonData.OutBounds[k].Arrivaltime + '</span>';
    //                                        if (jsonData.OutBounds[k].Refundable) {
    //                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Flightno + ' | ' + jsonData.OutBounds[k].Flightclasscode + ' | Refundable</span>';
    //                                        }
    //                                        else {
    //                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[k].Flightno + ' | ' + jsonData.OutBounds[k].Flightclasscode + ' | Non-Refundable</span>';
    //                                        }
    //                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds[k].Freebaggage + '</span>';
    //                                        str += '</div>';
    //                                        //str += '<div class="border-top pt-4 mt-4">';
    //                                        //str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
    //                                        //str += '<span class="fs-14px w-100 d-inline-flex">';
    //                                        //for (var j = 1; j <= jsonData.OutBounds[i].PassengersDetails.length; ++j) {
    //                                        //    str += '' + j + '. ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Title + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Firstname + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Lastname + ' <br/>';
    //                                        //}
    //                                        //str += '</span >';
    //                                        //str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
    //                                        //str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactName + '</span>';
    //                                        //str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactPhone + '</span>';
    //                                        //str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactEmail + '</span>';
    //                                        //str += '</div>';
    //                                    }
    //                                }
    //                                if (jsonData.InBounds.length > 0) {
    //                                    for (var a = 0; a < jsonData.InBounds.length; ++a) {
    //                                        str += '<div class="border-top pt-4 mt-4 d-flex align-items-center">';
    //                                        str += '<img src="' + jsonData.InBounds[a].Airlinelogo + '" width="26">';
    //                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.InBounds[a].Airlinename + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="d-flex align-items-center">';
    //                                        str += '<span class="fs-12px">' + jsonData.InBounds[a].Departure + '</span>';
    //                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
    //                                        str += '<span class="fs-12px">' + jsonData.InBounds[a].Arrival + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="text-soft mt-2 fs-13px">';
    //                                        str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Flightdatedt + '</span>';
    //                                        str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Departuretime + ' - ' + jsonData.InBounds[a].Arrivaltime + '</span>';
    //                                        if (jsonData.InBounds[a].Refundable) {
    //                                            str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Flightno + ' | ' + jsonData.InBounds[a].Flightclasscode + ' | Refundable</span>';
    //                                        }
    //                                        else {
    //                                            str += '<span class="w-100 display-inherit">' + jsonData.InBounds[a].Flightno + ' | ' + jsonData.InBounds[a].Flightclasscode + ' | Non-Refundable</span>';
    //                                        }
    //                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.InBounds[a].Freebaggage + '</span>';
    //                                        str += '</div>';
    //                                        str += '<div class="border-top pt-4 mt-4">';
    //                                        str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">';
    //                                        for (var b = 1; b <= jsonData.InBounds[a].PassengersDetails.length; ++b) {
    //                                            str += '' + b + '. ' + jsonData.InBounds[a].PassengersDetails[b - 1].Title + ' ' + jsonData.InBounds[a].PassengersDetails[b - 1].Firstname + ' ' + jsonData.InBounds[a].PassengersDetails[b - 1].Lastname + ' <br/>';
    //                                        }
    //                                        str += '</span >';
    //                                        str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.InBounds[a].ContactName + '</span>';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.InBounds[a].ContactPhone + '</span>';
    //                                        str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.InBounds[a].ContactEmail + '</span>';
    //                                        str += '</div>';
    //                                    }
    //                                }
    //                            }

    //                            $("#tblFlightDetail").append(str);
    //                            ServiceCharge($("#hdnFinalAmount").val());
    //                            //$("#hdnFinalAmount").val(jsonData.OutBounds[0].TripFareTotal);
    //                            $('#AjaxLoader').hide();
    //                        }
    //                        else {
    //                            if (!IsValidJson) {
    //                                $("#dvMessage").html(response);

    //                            }
    //                            else {

    //                                $("#dvMessage").html("No Passenger detail found, Please try again.");
    //                            }
    //                            $('#AjaxLoader').hide();
    //                        }
    //                    }
    //                    else {
    //                        $("#dvMessage").html("Something went wrong. Please try again later.");
    //                        $('#AjaxLoader').hide();
    //                        return false;
    //                    }
    //                },
    //                failure: function (response) {
    //                    $("#dvMsg").html(response.responseText);
    //                    $('#AjaxLoader').hide();
    //                    return false;
    //                },
    //                error: function (response) {
    //                    $("#dvMsg").html(response.responseText);
    //                    $('#AjaxLoader').hide();
    //                    return false;
    //                }
    //            });
    //        }, 10
    //    );
    //}
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
                            $("#tblFlightDetail").html("");
                            var str = "";
                            if (jsonData != null && jsonData.Message == "success") {
                                if ($("#hdnTripType").val() == "O") {
                                    if (jsonData.OutBounds.length > 0) {
                                        for (var i = 0; i < jsonData.OutBounds.length; ++i) {
                                            str += '<div class="d-flex align-items-center">';
                                            str += '<img src="' + jsonData.OutBounds[i].Airlinelogo + '" width="26">';
                                            str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds[i].Airlinename + '</span>';
                                            str += '</div>';
                                            str += '<div class="d-flex align-items-center">';
                                            str += '<span class="fs-12px">' + jsonData.OutBounds[i].Departure + '</span>';
                                            str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                            str += '<span class="fs-12px">' + jsonData.OutBounds[i].Arrival + '</span>';
                                            str += '</div>';
                                            str += '<div class="text-soft mt-2 fs-13px">';
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightdatedt + '</span>';
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Departuretime + ' - ' + jsonData.OutBounds[i].Arrivaltime + '</span>';
                                            if (jsonData.OutBounds[i].Refundable) {
                                                str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightno + ' | ' + jsonData.OutBounds[i].Flightclasscode + ' | Refundable</span>';
                                            }
                                            else {
                                                str += '<span class="w-100 display-inherit">' + jsonData.OutBounds[i].Flightno + ' | ' + jsonData.OutBounds[i].Flightclasscode + ' | Non-Refundable</span>';
                                            }
                                            str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds[i].Freebaggage + '</span>';
                                            str += '</div>';
                                            str += '<div class="border-top pt-4 mt-4">';
                                            str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                            str += '<span class="fs-14px w-100 d-inline-flex">';
                                            for (var j = 1; j <= jsonData.OutBounds[i].PassengersDetails.length; ++j) {
                                                str += '' + j + '. ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Title + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Firstname + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Lastname + ' <br/>';
                                            }
                                            str += '</span >';
                                            str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                            str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactName + '</span>';
                                            str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactPhone + '</span>';
                                            str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactEmail + '</span>';
                                            str += '</div>';
                                        }
                                    }
                                }
                                else if ($("#hdnTripType").val() == "R") {
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
                                            //str += '<div class="border-top pt-4 mt-4">';
                                            //str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                            //str += '<span class="fs-14px w-100 d-inline-flex">';
                                            //for (var j = 1; j <= jsonData.OutBounds[i].PassengersDetails.length; ++j) {
                                            //    str += '' + j + '. ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Title + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Firstname + ' ' + jsonData.OutBounds[i].PassengersDetails[j - 1].Lastname + ' <br/>';
                                            //}
                                            //str += '</span >';
                                            //str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                            //str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactName + '</span>';
                                            //str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactPhone + '</span>';
                                            //str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.OutBounds[i].ContactEmail + '</span>';
                                            //str += '</div>';
                                        }
                                    }
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
                                            str += '<div class="border-top pt-4 mt-4">';
                                            str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                            str += '<span class="fs-14px w-100 d-inline-flex">';
                                            for (var b = 1; b <= jsonData.InBounds[a].PassengersDetails.length; ++b) {
                                                str += '' + b + '. ' + jsonData.InBounds[a].PassengersDetails[b - 1].Title + ' ' + jsonData.InBounds[a].PassengersDetails[b - 1].Firstname + ' ' + jsonData.InBounds[a].PassengersDetails[b - 1].Lastname + ' <br/>';
                                            }
                                            str += '</span >';
                                            str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                            str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.InBounds[a].ContactName + '</span>';
                                            str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.InBounds[a].ContactPhone + '</span>';
                                            str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.InBounds[a].ContactEmail + '</span>';
                                            str += '</div>';
                                        }
                                    }
                                }

                                $("#tblFlightDetail").append(str);
                                ServiceCharge($("#hdnFinalAmount").val());
                                //$("#hdnFinalAmount").val(jsonData.OutBounds[0].TripFareTotal);
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
    else {

        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/MyPayUser/MyPayUserGetFlightPassengerDetail",
                    data: '{"FlightID":"' + FlightId + '","ReturnFlightID":"' + ReturnFlightId + '"}',
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
                            $("#tblFlightDetail").html("");
                            var str = "";
                            if (jsonData != null) {
                                if ($("#hdnTripType").val() == "O") {
                                    if (jsonData.OutBounds != null) {

                                        str += '<div class="d-flex align-items-center">';
                                        str += '<img src="' + jsonData.OutBounds.Airlinelogo + '" width="26">';
                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds.Airlinename + '</span>';
                                        str += '</div>';
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<span class="fs-12px">' + jsonData.OutBounds.Departure + '</span>';
                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                        str += '<span class="fs-12px">' + jsonData.OutBounds.Arrival + '</span>';
                                        str += '</div>';
                                        str += '<div class="text-soft mt-2 fs-13px">';
                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Flightdate + '</span>';
                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Departuretime + ' - ' + jsonData.OutBounds.Arrivaltime + '</span>';
                                        if (jsonData.OutBounds.Refundable) {
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Flightno + ' | ' + jsonData.OutBounds.Flightclasscode + ' | Refundable</span>';
                                        }
                                        else {
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Flightno + ' | ' + jsonData.OutBounds.Flightclasscode + ' | Non-Refundable</span>';
                                        }
                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds.Freebaggage + '</span>';
                                        str += '</div>';
                                        str += '<div class="border-top pt-4 mt-4">';
                                        str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                        str += '<span class="fs-14px w-100 d-inline-flex">';
                                        for (var j = 1; j <= jsonData.OutBounds.Passengers.length; ++j) {
                                            str += '' + j + '. ' + jsonData.OutBounds.Passengers[j - 1].Title + ' ' + jsonData.OutBounds.Passengers[j - 1].Firstname + ' ' + jsonData.OutBounds.Passengers[j - 1].Lastname + ' <br/>';
                                        }
                                        str += '</span >';
                                        str += '</div>';
                                    }
                                }

                                else if ($("#hdnTripType").val() == "R") {
                                    if (jsonData.OutBounds != null) {

                                        str += '<div class="d-flex align-items-center">';
                                        str += '<img src="' + jsonData.OutBounds.Airlinelogo + '" width="26">';
                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.OutBounds.Airlinename + '</span>';
                                        str += '</div>';
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<span class="fs-12px">' + jsonData.OutBounds.Departure + '</span>';
                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                        str += '<span class="fs-12px">' + jsonData.OutBounds.Arrival + '</span>';
                                        str += '</div>';
                                        str += '<div class="text-soft mt-2 fs-13px">';
                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Flightdate + '</span>';
                                        str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Departuretime + ' - ' + jsonData.OutBounds.Arrivaltime + '</span>';
                                        if (jsonData.OutBounds.Refundable) {
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Flightno + ' | ' + jsonData.OutBounds.Flightclasscode + ' | Refundable</span>';
                                        }
                                        else {
                                            str += '<span class="w-100 display-inherit">' + jsonData.OutBounds.Flightno + ' | ' + jsonData.OutBounds.Flightclasscode + ' | Non-Refundable</span>';
                                        }
                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.OutBounds.Freebaggage + '</span>';
                                        str += '</div>';

                                    }
                                    if (jsonData.InBounds != null) {
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<span class="border-top pt-4 mt-4">';
                                        str += '<span class="text-soft fs-13px w-100 d-inline-flex">Return Flights Details</span > ';
                                        str += '<img src="' + jsonData.InBounds.Airlinelogo + '" width="26">';
                                        str += '<span class="fs-14px ml-1 mr-1 fw-medium line-height-1">' + jsonData.InBounds.Airlinename + '</span>';
                                        str += '</div>';
                                        str += '<div class="d-flex align-items-center">';
                                        str += '<span class="fs-12px">' + jsonData.InBounds.Departure + '</span>';
                                        str += '<img src="/Content/assets/MyPayUserApp/images/right_left_arrow.svg" class="ml-1 mr-1">';
                                        str += '<span class="fs-12px">' + jsonData.InBounds.Arrival + '</span>';
                                        str += '</div>';
                                        str += '<div class="text-soft mt-2 fs-13px">';
                                        str += '<span class="w-100 display-inherit">' + jsonData.InBounds.Flightdate + '</span>';
                                        str += '<span class="w-100 display-inherit">' + jsonData.InBounds.Departuretime + ' - ' + jsonData.InBounds.Arrivaltime + '</span>';
                                        if (jsonData.InBounds.Refundable) {
                                            str += '<span class="w-100 display-inherit">' + jsonData.InBounds.Flightno + ' | ' + jsonData.InBounds.Flightclasscode + ' | Refundable</span>';
                                        }
                                        else {
                                            str += '<span class="w-100 display-inherit">' + jsonData.InBounds.Flightno + ' | ' + jsonData.InBounds.Flightclasscode + ' | Non-Refundable</span>';
                                        }
                                        str += '<span class="w-100 display-inherit">Total Luggage: ' + jsonData.InBounds.Freebaggage + ' <br/>' + '</span>';
                                        str += '</div>';
                                        str += '<div class="border-top pt-4 mt-4">';
                                        str += '<span class="text-soft fs-13px w-100 d-inline-flex">Passengers Information</span > ';
                                        str += '<span class="fs-14px w-100 d-inline-flex">';
                                        for (var j = 1; j <= jsonData.OutBounds.Passengers.length; ++j) {
                                            str += '' + j + '. ' + jsonData.OutBounds.Passengers[j - 1].Title + ' ' + jsonData.OutBounds.Passengers[j - 1].Firstname + ' ' + jsonData.OutBounds.Passengers[j - 1].Lastname + ' <br/>';
                                        }
                                        str += '</span >';
                                        str += '</div>';
                                    }
                                }
                                str += '<div class="border-top pt-4 mt-4">';
                                str += '<span class="text-soft fs-13px mt-3 w-100 d-inline-flex">Contact Person Details</span>';
                                str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.ContactName + '</span>';
                                str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.ContactPhone + '</span>';
                                str += '<span class="fs-14px w-100 d-inline-flex">' + jsonData.ContactEmail + '</span>';
                                str += '</div>';

                                $("#tblFlightDetail").append(str);
                                ServiceCharge($("#hdnFinalAmount").val());
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

$("#btnPay").click(function () {
    debugger
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
});

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
    debugger
    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var BookingId = $("#hdnBookingId").val();
    if (BookingId == "") {
        $("#dvMessage").html("Please book flight first");
        return false;
    }
    var ContactNumber = $("#hdnContactNumber").val();
    var ContactName = $("#txtContactName").val();
    var ContactEmail = $("#txtContactEmail").val();
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
                    data: '{"ContactNumber":"' + ContactNumber + '","BookingId":"' + BookingId + '","Amount":"' + Amount + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","FlightId":"' + FlightId + '","ReturnFlightId":"' + ReturnFlightId + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
                    success: function (response) {
                        debugger;
                        console.log(response)
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
                                        return false;
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
                                $("#txnMsg").html(jsonData.Message);
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
    debugger
    var ServiceId = $("#hdnServiceID").val();
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
        $("#CouponDiscountPopup").text(discount);
        $("#MobilePopup").closest("tr").hide();
        if (ServiceId == "56") {
            $("#FlightBookingIdPopup").closest("tr").show();
        }
        else {

        }
        
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

$("#Roundreturn").change(function () {
    var departureDate = document.getElementById("Rounddepart").value;
    var returnDate = document.getElementById("Roundreturn").value;

    if ((Date.parse(departureDate) > Date.parse(returnDate))) {
        alert("Return date should be greater than Departure date");
        document.getElementById("Roundreturn").value = "";
    }
});
$("#site-off").change(function () {
    IsRefundableOnly = true;
    if (!this.checked) {
        IsRefundableOnly = false;
    }
    if (IsRoundSearch) {
        BindRoundList();
    }
    else {
        BindOneWayList();
    }
});

