
var discount = 0;
var payable = 0;
var WalletResponse = '';
var ScreenLevel = 0;
var SelectedEvent = '';
var MerchantCode = '';
var AmountToPay = '';
var IsRedirectToBook = "false";


$(document).ready(function () {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            EventsLoad();
        }, 10);
    $("#DivWallet").trigger("click");
    $("#ddl_ticket_type").on("change", function () {
        var selectedTicket = $("#ddl_ticket_type").val();
        if (selectedTicket == 0) {
            $("#div_TicketDetail").hide();
            return;
        }
        $("#div_TicketDetail").show();
        var arrSelected = SelectedEvent.data.ticketCategoryList;
        // var ticketsAvailable = arrSelected.find(x => x.ticketCategoryId == selectedTicket).availableTickets;
        var ticketRate = arrSelected.find(x => x.ticketCategoryId == selectedTicket).ticketRate;
        var section = arrSelected.find(x => x.ticketCategoryId == selectedTicket).ticketCategoryName;
        var amenities = arrSelected.find(x => x.ticketCategoryId == selectedTicket).amenityList;
        var amenityString = "";
        if (amenities.length > 0) {
            $('#dv_amenities').css('color', '');
            for (var i = 0; i < amenities.length; i++) {
                amenityString = amenityString + amenities[i].amenityName;
                if (i != amenities.length - 1) {
                    amenityString = amenityString + ", "
                }
            }
        }
        else {
            amenityString = "No Amenities";
            $('#dv_amenities').css('color', 'red');
        }
        $("#dv_section").html(section);
        // $("#dv_available_Tickets").html(ticketsAvailable);
        $("#dv_ticket_Rate").html('Rs ' + ticketRate);
        $("#dv_amenities").html(amenityString);
        $("#txt_noOfTickets").val("");
        $("#spn_TotalPrice").html("");
    });
});





function EventsLoad() {
    ScreenLevel = 0;
    var PageSize = "100";
    var PageNumber = "1";
    var SearchVal = "";
    var SortOrder = "asc";
    var DateFrom = "2022-12-11";
    var DateTo = "2024-11-05";
    var SortBy = "eventdate";
    var objEvents = '';
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserEventList",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"PageSize":"' + PageSize + '","PageNumber":"' + PageNumber + '","SearchVal":"' + SearchVal + '","SortOrder":"' + SortOrder + '","DateFrom":"' + DateFrom + '","DateTo":"' + DateTo + '","SortBy":"' + SortBy + '"}',
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
                    var arr = jsonData;
                    if (IsValidJson) {
                        debugger;
                        if (arr['Message'] == "success") {
                            if (jsonData != null && jsonData.items != null && jsonData.items.length > 0) {
                                for (var i = 0; i < jsonData.items.length; i++) {
                                    objEvents += '<div class="col-md-6 mb-3">';
                                    objEvents += '<div class="votingsec p-3 round bg-lighter position-relative">';
                                    objEvents += '<img src="' + jsonData.items[i].promotionalBannerImagePath + '" class="w-100 cursor-pointer" onclick="showEventDetail(&apos;' + jsonData.items[i].eventId + '&apos;,&apos;' + jsonData.items[i].eventDate + '&apos;,&apos;' + jsonData.items[i].merchantCode + '&apos;,&apos;' + false + '&apos;)">';
                                    objEvents += '<div class="d-lg-flex mt-1 align-items-center justify-content-between">';
                                    objEvents += '<span>' + jsonData.items[i].eventDateDT + ' | ' + jsonData.items[i].venueAddress + '</span>';
                                    objEvents += '</div>';
                                    objEvents += '<div class="d-lg-flex mt-1 align-items-center justify-content-between">';
                                    objEvents += '<div class="w-60 w-sm-100">';
                                    objEvents += '<h6 class="title cursor-pointer" onclick="showEventDetail(&apos;' + jsonData.items[i].eventId + '&apos;,&apos;' + jsonData.items[i].eventDate + '&apos;,&apos;' + jsonData.items[i].merchantCode + '&apos;,&apos;' + false + '&apos;)">' + jsonData.items[i].eventName + ' </h6>';
                                    objEvents += '<p>' + jsonData.items[i].eventDescription + '</p>';
                                    objEvents += '</div>';
                                    objEvents += '<div class="w-40 w-sm-100 text-right">';
                                    objEvents += '<a href="javascript:void(0)" onclick="showEventDetail(&apos;' + jsonData.items[i].eventId + '&apos;,&apos;' + jsonData.items[i].eventDate + '&apos;,&apos;' + jsonData.items[i].merchantCode + '&apos;,&apos;' + true + '&apos;)" class="btn btn-sm btn-danger color-black text-white cursor-pointer"> Buy</a>';
                                    objEvents += '</div>';
                                    objEvents += '</div>';

                                    objEvents += '<div class="d-lg-flex mt-1 align-items-center justify-content-between">';
                                    objEvents += '<div class="w-50 w-sm-100">';
                                    objEvents += '<span> Organizer :</span>';
                                    objEvents += '</div>';
                                    objEvents += '<div class="w-50 w-sm-100 text-right">';
                                    objEvents += '<span>' + jsonData.items[i].organizerName + ' </span>';
                                    objEvents += '</div>';
                                    objEvents += '</div>';

                                    objEvents += '</div><!--votingsec-->';
                                    objEvents += '</div>';
                                }
                                $('#AjaxLoader').hide();
                            }
                            else {
                                $('#AjaxLoader').hide();
                                objEvents += '<div class="col-md-12 mb-6">';
                                objEvents += '<div style="color:red;text-align:center">No Events available at the moment</div>';
                                objEvents += '</div>';

                            }
                            $("#dvMessage").html("");
                            $("#txnMsg").html("");
                            $("#dvEventList").show();
                            $("#dvEvents").html('');
                            $("#dvEvents").append(objEvents);

                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html("Invalid Credentials");
                        }
                    }
                    else if (response == "ServiceDown") {
                        $('#AjaxLoader').hide();
                        $("#DivAntivirusStep2").hide();
                        $("#DivAntivirus").show();
                        window.location.href = "/MyPayUser/MyPayUserServiceDown";
                    }
                    else if (response == "Invalid Request") {
                        window.location.href = "/MyPayUserLogin";
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html(response);
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
function showEventDetail(eventId, eventDate, merchantCode, directToBook) {
    IsRedirectToBook = directToBook;
    MerchantCode = merchantCode;
    ScreenLevel = 1;
    var objEvents = '';
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserEventDetail",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"EventId":"' + eventId + '","EventDate":"' + eventDate + '","MerchantCode":"' + merchantCode + '"}',
                success: function (response) {
                    debugger;
                    var jsonData;
                    var IsValidJson = false;
                    try {
                        jsonData = $.parseJSON(response);
                        var IsValidJson = true;
                        SelectedEvent = jsonData;

                    }
                    catch (err) {

                    }
                    var arr = jsonData;
                    if (IsValidJson) {
                        debugger;
                        if (arr['Message'] == "success") {
                            $("#dvEventList").hide();
                            if (IsRedirectToBook == "true") {
                                $("#dvMessage").html("");
                                $("#txnMsg").html("");
                                $('#AjaxLoader').hide();
                                redirectToBook();
                            }
                            else {
                                if (jsonData != null && jsonData.data != null) {
                                    objEvents += '<div style="position: relative;" class="post_img p-3 round bg-lighter w-60">';
                                    if (jsonData.data.bannerImagePath != "") {
                                        objEvents += '<img style="display: block;" src="' + jsonData.data.bannerImagePath + '" class="w-100">';
                                    }
                                    else {
                                        objEvents += '<img style="display: block;" src="/Content/assets/Images/noimageblank2.png"class="w-100" />';
                                    }
                                    objEvents += '<img src="/Content/assets/MyPayUserApp/images/icons/google-map.svg" style="position: absolute; bottom:20px; right:20px;" class="dark_i cursor-pointer" onclick="openMap()">';
                                    objEvents += '</div><!--post_img-->';
                                    objEvents += '<div class="d-lg-flex mt-1 align-items-center justify-content-between">';
                                    objEvents += '<span>' + jsonData.data.eventDateDT + ' | ' + jsonData.data.venueAddress + '</span>';
                                    objEvents += '</div>';
                                    objEvents += '<div class="card-title d-flex w-100 mt-4 mb-4 align-items-center big-card-title">';
                                    objEvents += '<h4 class="card-title mb-0">' + jsonData.data.eventName + '</h4><!--card-title-->';
                                    objEvents += '</div>';
                                    objEvents += '<p class="text-base">' + jsonData.data.eventDescription + '</p >';
                                    objEvents += '<a href="javascript:void(0);" class="btn btn-primary btn-lg d-block" onclick="redirectToBook()">Buy</a>';

                                    if (jsonData.data.sponserList != null && jsonData.data.sponserList.length > 0) {
                                        objEvents += '<div class="card-title d-flex w-100 mt-4 mb-4 align-items-center big-card-title">';
                                        objEvents += '<h6 class="card-title mb-0">Sponsors</h4><!--card-title-->';
                                        objEvents += '</div>';
                                        for (var i = 0; i < jsonData.data.sponserList.length; i++) {
                                            objEvents += '<img class="ml-2" src="' + jsonData.data.sponserList[i].sponserLogoImagePath + '" width="50" height="50" id="img' + jsonData.data.sponserList[i].sponserId + '">';
                                        }
                                    }

                                    if (jsonData.data.guestList != null && jsonData.data.guestList.length > 0) {
                                        objEvents += '<h6 class="card-title mb-2 mt-2">Guests</h6><!--card-title-->';
                                        objEvents += '</div>';
                                        for (var i = 0; i < jsonData.data.guestList.length; i++) {
                                            objEvents += '<img  src="' + jsonData.data.guestList[i].guestImagePath + '" width="40" height="40" id="img' + jsonData.data.sponserList[i].sponserId + ' class="mb-2 ml-2" ">';
                                            objEvents += '<h6>' + jsonData.data.guestList[i].guestName + '</h6>';
                                        }
                                    }
                                }
                                $("#dvMessage").html("");
                                $("#txnMsg").html("");
                                $("#dv_EventDetail").show();
                                $("#dv_ineer_EventDetail").html('');
                                $("#dv_ineer_EventDetail").append(objEvents);
                                $('#AjaxLoader').hide();
                                return false;
                            }
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html("Invalid Credentials");
                        }

                    }
                    else if (response == "ServiceDown") {
                        $('#AjaxLoader').hide();
                        $("#DivAntivirusStep2").hide();
                        $("#DivAntivirus").show();
                        window.location.href = "/MyPayUser/MyPayUserServiceDown";
                    }
                    else if (response == "Invalid Request") {
                        window.location.href = "/MyPayUserLogin";
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html(response);
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

function redirectToBook() {
    ScreenLevel = 2;
    $("#div_TicketDetail").hide();
    $("#txt_noOfTickets").val("");
    $("#txt_BookedBy").val("");
    $("#txt_Contact").val("");
    $("#txt_Email").val("");
    $("#spn_TotalPrice").html("");
    $("#dv_EventDetail").hide();
    $("#dv_BookEvent").show();

    var str = "";
    str += '<option value="0">Select ticket type</option>';
    var arr = SelectedEvent.data.ticketCategoryList;
    if (arr.length > 0) {
        for (var i = 0; i < arr.length; ++i) {
            str += '<option value="' + arr[i].ticketCategoryId + '">' + arr[i].ticketCategoryName + '</option>';
        }
    }
    $("#ddl_ticket_type").html("");
    $("#ddl_ticket_type").append(str);
    $("#dv_Terms").show();
    if (SelectedEvent.data.eventTermsAndCondition == "") {
        $("#dv_Terms").hide();
    }
}
function calculatePrice() {
    var NoOfTicket = $("#txt_noOfTickets").val();
    if (NoOfTicket == "" || Number(NoOfTicket) < 1) {
        $("#spn_TotalPrice").html("");
        return false;
    }
    var ticketType = $("#ddl_ticket_type :selected").val();
    var TicketRate = SelectedEvent.data.ticketCategoryList.find(x => x.ticketCategoryId == ticketType).ticketRate;
    var TotalPrice = TicketRate * Number(NoOfTicket);
    $("#spn_TotalPrice").html("Total Amount: Rs " + TotalPrice.toFixed(2));
}

function openTermsModal() {
    $('#dv_TermsBody').html('');
    $('#dv_TermsBody').html(SelectedEvent.data.eventTermsAndCondition);
    $('#model_Terms').modal('show');
}

function openMap() {
    var url = "https://maps.google.com/?q=" + SelectedEvent.data.venueAddress;
    window.open(url, '_blank');
}

function bookTicket() {
    $("#dvMessage").html('');
    var ticketType = $("#ddl_ticket_type :selected").val();
    if (ticketType == "" || ticketType == "0") {
        $("#dvMessage").html("Please select Ticket Type");
        return false;
    }

    var NoOfTicket = $("#txt_noOfTickets").val();
    if (NoOfTicket == "" || Number(NoOfTicket) < 1) {
        $("#dvMessage").html("Please enter Number Of Ticket");
        return false;
    }
    var AvailableTickets = SelectedEvent.data.ticketCategoryList.find(x => x.ticketCategoryId == ticketType).availableTickets;
    if (NoOfTicket > AvailableTickets) {
        $("#dvMessage").html(" No of ticket can not be more than Available Tickets");
        return false;
    }

    if (Number(NoOfTicket) > 25) {
        $("#dvMessage").html("You can book max 25 tickets");
        return false;
    }
    var BookedBy = $("#txt_BookedBy").val();
    if (BookedBy == "") {
        $("#dvMessage").html("Please enter Booking person name");
        return false;
    }
    var Contact = $("#txt_Contact").val();
    if (Contact == "") {
        $("#dvMessage").html("Please enter Contact Number");
        return false;
    }
    if (Contact.length < 10) {
        $("#dvMessage").html("Please enter valid Number");
        return;
    }

    var IsValid = false;
    var regex1 = /^([9][8][5][0-9]{7})$/;
    var regex2 = /^([9][7][4-5][0-9]{7})$/;
    var regex3 = /^([9][8][46][0-9]{7})$/;
    var regex4 = /^([9][8][0-2][0-9]{7})$/;
    var regex5 = /^([9][6][0-9]{8}|[9][8][8][0-9]{7})$/;

    if (Contact.match(regex1)) {
        IsValid = true;
    }
    else if (Contact.match(regex2)) {
        IsValid = true;
    }
    else if (Contact.match(regex3)) {
        IsValid = true;
    }
    else if (Contact.substring(0, 3) == "976") {
        IsValid = true;
    }
    else if (Contact.match(regex4)) {
        IsValid = true;
    }
    else if (Contact.match(regex5)) {
        IsValid = true;
    }

    if (!IsValid) {
        $("#dvMessage").html("Please enter valid Number");
        return;
    }
    var Email = $("#txt_Email").val();
    if (Email == "") {
        $("#dvMessage").html("Please enter Email Address");
        return false;
    }
    else if (!isEmail(Email)) {
        $("#dvMessage").html("Please enter a valid Email ID");
        return false;
    }

    if (SelectedEvent.data.eventTermsAndCondition != "") {
        var terms = $("#Terms:checked").val();
        if (!terms) {
            $("#dvMessage").html("Please accept the terms & conditions and Policy of MyPay.");
            return false;
        }
    }

    var EventId = SelectedEvent.data.eventId;
    var TicketCategoryName = $("#ddl_ticket_type :selected").text();
    var EventDate = SelectedEvent.data.eventDate;
    var TicketRate = SelectedEvent.data.ticketCategoryList.find(x => x.ticketCategoryId == ticketType).ticketRate;
    var TotalPrice = TicketRate * Number(NoOfTicket);
    var PaymentMethodId = SelectedEvent.data.paymentMethodList[0].paymentMethodId;
    AmountToPay = TotalPrice;
    $("#hdnTicketCategoryName").val(TicketCategoryName);
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/MyPayUserBookEvent",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"MerchantCode":"' + MerchantCode + '","CustomerName":"' + BookedBy + '","CustomerMobile":"' + Contact + '","CustomerEmail":"' + Email + '","EventId":"'
                    + EventId + '","TicketCategoryId":"' + ticketType + '","TicketCategoryName":"' + TicketCategoryName + '","EventDate":"' + EventDate + '","TicketRate":"'
                    + TicketRate + '","NoOfTicket":"' + NoOfTicket + '","TotalPrice":"' + TotalPrice + '","PaymentMethodId":"' + PaymentMethodId + '"}',
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
                    var arr = jsonData;
                    if (IsValidJson) {
                        debugger;
                        if (arr['message'] == "success") {


                            if (jsonData != null && jsonData.data != null) {
                                ScreenLevel = 3;
                                $("#dv_BookEvent").hide();
                                $("#dvMessage").html("");
                                $("#hdnOrderID").val(jsonData.data.OrderId);
                                $("#hdnPaymentMethodId").val(jsonData.data.PaymentMethodId);
                                goToPay();
                            }
                            $("#dvMessage").html("");
                            $("#txnMsg").html("");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else {
                            $('#AjaxLoader').hide();
                            $("#dvMessage").html("Invalid Credentials");
                        }
                    }
                    else if (response == "ServiceDown") {
                        $('#AjaxLoader').hide();
                        $("#DivAntivirusStep2").hide();
                        $("#DivAntivirus").show();
                        window.location.href = "/MyPayUser/MyPayUserServiceDown";
                    }
                    else if (response == "Invalid Request") {
                        window.location.href = "/MyPayUserLogin";
                    }
                    else {
                        $('#AjaxLoader').hide();
                        $("#dvMessage").html(response);
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

$("#btn_back").click(function () {
    debugger;
    $("#dvMessage").html("");
    $("#dvEventList").hide();
    $("#dv_EventDetail").hide();
    $("#dv_BookEvent").hide();
    $("#DivProceedToPay").hide();
    if (ScreenLevel == 0) {
        window.location.replace("/MyPayUserLogin/Dashboard");
    }
    else if (ScreenLevel == 1) {
        ScreenLevel = 0;
        $("#dvEventList").show();
    }
    else if (ScreenLevel == 2) {
        if (IsRedirectToBook == "true") {
            ScreenLevel = 0;
            $("#dvEventList").show();
        } else {
            ScreenLevel = 1;
            $("#dv_EventDetail").show();
        }
    }
    else if (ScreenLevel == 3) {
        ScreenLevel = 2;
        $("#dv_BookEvent").show();
    }
})

function goToPay() {
    var ServiceID = $("#hdnServiceID").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }

    var Amount = "";
    Amount = AmountToPay;
    $("#lblAmount").text(Amount);
    $("#Amount").val($("#lblAmount").text());

    $('#AjaxLoader').show();
    setTimeout(function () {
        $("html, body").animate({ scrollTop: "0" });
        GetBankDetail();
        ServiceCharge();
    }, 20);

    var arr = $.parseJSON(WalletResponse);
    $("#smartpayCoinsTotal").text(arr['TotalRewardPoints']);
    $("#lblPayAmount").text(AmountToPay);
    $("#DivProceedToPay").show();
    $("#btnPaymentScreenBack").hide();
    $("#dvMessage").html("");
    $("#txnMsg").html("");
    $('#DivWallet')[0].click();
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
function ServiceCharge() {

    var ServiceId = $("#hdnServiceID").val();
    var PaymentMerchantId = SelectedEvent.data.paymentMethodList[0].paymentMerchantId;
    var Amount = "";
    Amount = AmountToPay;
    var WalletAmountDeduct = Amount;
    var Contact = $("#txt_Contact").val();

    var PaymentMode = $("#hfPaymentMode").val();
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            $.ajax({
                type: "POST",
                url: "/MyPayUser/ServiceChargeMerchant",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: '{"MerchantId":"' + PaymentMerchantId + '","Amount":"' + Amount + '","ServiceId":"' + ServiceId + '"}',
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
                    if (IsValidJson) {
                        var MPCoinsDebit = parseFloat(arr['MPCoinsDebit']);
                        if (MPCoinsDebit > 0) {
                            WalletAmountDeduct = parseFloat(WalletAmountDeduct) - MPCoinsDebit;
                        }
                        $("#MobilePopup").text(Contact);
                        $("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceCharge']);
                        $("#smartpayRupees").text(WalletAmountDeduct);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceCharge']);
                        var smartPayCoin = parseFloat($("#smartpayCoins").text());
                        $('#DivCoin').show();
                        if (smartPayCoin <= 0) {
                            $('#DivCoin').hide();
                        }
                        $("#TxnAmountPopup").text(arr['Amount']);
                        $("#MypayCoinDebitedPopup").text(arr['MPCoinsDebit']);
                        $("#MypayCoinCreditedPopup").text(arr['RewardPoints']);
                        $("#MypayCoinDebitedPopup").closest('tr').hide();

                        if (PaymentMode == "4") {
                            $("#AmountPopup").text(WalletAmountDeduct);
                            if (MPCoinsDebit > 0) {
                                $("#MypayCoinDebitedPopup").closest('tr').show();
                            }
                            var DeductAmount = parseFloat(WalletAmountDeduct);
                            var DeductServiceCharge = parseFloat(arr['ServiceCharge']);
                            var totalamount = (parseFloat(DeductAmount + DeductServiceCharge) - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));
                        }
                        else {
                            $("#AmountPopup").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceCharge']);
                            var totalamount = (parseFloat(Amount + ServiceCharge) - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));

                        }

                        var MPCoinsCredit = parseFloat(arr['RewardPoints']);

                        if (MPCoinsCredit > 0) {
                            $("#MypayCoinCreditedPopup").closest('tr').show();
                        }
                        CheckCoinBalance(0);

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
        ServiceCharge();
        $('#PaymentPopup').modal('show');
        $("#CouponDiscountPopup").text(discount);
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
function Pay() {

    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }

    var Mpin = $("#Pin").val();
    if (Mpin == "") {
        $("#dvMessage").html("Please enter Pin");
        return false;
    }
    var OrderId = $("#hdnOrderID").val();
    var EventId = SelectedEvent.data.eventId;
    var PaymentMethodId = SelectedEvent.data.paymentMethodList[0].paymentMethodId;
    var PaymentMerchantId = SelectedEvent.data.paymentMethodList[0].paymentMerchantId;
    var PaymentMethodName = SelectedEvent.data.paymentMethodList[0].paymentMethodName;
    var TicketCategoryName = $("#hdnTicketCategoryName").val();

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
                    url: "/MyPayUser/MyPayUserEventTicketCommit",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: '{"ServiceID":"' + ServiceID + '","Amount":"' + AmountToPay + '","Mpin":"' + Mpin + '","PaymentMode":"' + PaymentMode + '","MerchantCode":"' + MerchantCode + '","OrderId":"' + OrderId + '","EventId":"' + EventId + '","PaymentMethodId":"' + PaymentMethodId + '","PaymentMerchantId":"' + PaymentMerchantId + '","PaymentMethodName":"' + PaymentMethodName + '","TicketCategoryName":"' + TicketCategoryName + '","CouponCode":"' + CouponCode + '","BankId":"' + $("#hfBankId").val() + '"}',
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

                        if (response == "success") {
                            $("#DivPin").modal("hide");
                            $("#PaymentSuccessEvents").modal("show");
                            $("#dvMessage").html("Success");
                            $('#AjaxLoader').hide();
                            return false;
                        }
                        else if (IsValidJson) {
                            if (jsonData.message == "success") {
                                $("#DivPin").modal("hide");
                                $("#PaymentSuccessEvents").modal("show");
                                $("#txn-success").html(jsonData.responseMessage);
                                $('#AjaxLoader').hide();
                                return false;
                            }
                            else {

                                $('#AjaxLoader').hide();
                                $("#DivPin").modal("hide");
                                $("#txnMsg").html(jsonData.responseMessage);
                                $("#DivErrMessage").modal("show");
                            }
                        }
                        else if (response == "Session Expired") {

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
$("#btnPinForgot").click(function () {
    window.location.href = "/MyPayUser/MyPayUserChangePin";
});


function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
var specialKeys = new Array();
specialKeys.push(8);  //Backspace
specialKeys.push(9);  //Tab
specialKeys.push(46); //Delete
specialKeys.push(36); //Home
specialKeys.push(35); //End
specialKeys.push(37); //Left
specialKeys.push(39); //Right
function IsAlphaNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    return ret;
}