var TotalPrice = 0;
var TotalTraveller = 0;
var discount = 0;
var datetime = '';
var Tax = 0;
var WalletResponse = "";
var ScreenLevel = 0;
var AmountToPay = 0;
var IsRedirectToBook = "false";
var seatData = '';
var seatRow = [];
var BusSewaData = "";
var BusId = "";
var inputTypeCode = "";
var busdetailId = "";
var ticketSrlNo = "";


$(document).ready(function () {
    debugger;
   
    ScreenLevel = 0;
    $("#passengerdetails").hide();
    

    $("#searchBus").click(function () {
        debugger;
        BusSearch();
    });
    $("#findBus").click(function () {
        debugger;
        BusSearch();
    });
    var i = 0;
    var totalSeat = $("#totalSeat").val();
    for (i; i < totalSeat; i++) {
        var SeatId = document.getElementById("seat-radio-1-" + i + "");
        var data = SeatId.value;
    }
    $('#ProceedToCalc').click(function () {
        var ss = $("#btnradio4").val();
        var yy = $("#btnradio5").val();
        var trip = "";
        var dataList = "";
        if (ss != "") {
            trip = "Oneway"
        }
        if (yy != "") {
            trip = "Twoway"
        }
        if (trip == "Oneway") {
            TicketType = "One Way";
            var table = document.getElementById("oneway");
            var count = parseInt($("#oneway tr").length) - 1;

            var inputElements = table.querySelectorAll('td[data-th="Number"] input');
            inputId = inputElements[0].id.split('_');
            NumId = inputId[0];

            var inputElements = table.querySelectorAll('td[data-th="Fare"] p');
            inputId = inputElements[0].id.split('_');
            PriceId = inputId[0];

        }
        else {
            TicketType = "Round Way";
            var table = document.getElementById("twoway");
            var count = parseInt($("#oneway tr").length) - 1;

            var inputElements = table.querySelectorAll('td[data-th="Number"] input');
            inputId = inputElements[0].id.split('_');
            NumId = inputId[0];

            var inputElements = table.querySelectorAll('td[data-th="Fare"] p');
            inputId = inputElements[0].id.split('_');
            PriceId = inputId[0];
        }

        $("#TicketType").text(TicketType);
        calculatePrice(table, count, NumId, PriceId);

        $("#TotalAmt").text("Rs." + TotalPrice);
        $("#TotalPayAmt").text("Rs." + AmountToPay);
        $("#Traveller").text(TotalTraveller);
        $("#Tax").text(Tax);
        var currentdate = new Date();
        // Array of month names
        var monthNames = [
            "January", "February", "March", "April", "May", "June", "July",
            "August", "September", "October", "November", "December"
        ];

        var monthIndex = currentdate.getMonth(); // Get the month index (0-11)
        var monthName = monthNames[monthIndex]; // Get the month name
        datetime = monthName + " " + currentdate.getDate() + " " + currentdate.getFullYear();

        $("#purchasedDate").text(datetime);

    });
});





$(".select-seat").click(function () {
    debugger;
    var i = 0;
    const rowData = [];
    var totalSeat = $("#totalSeat").val();
    for (i; i < totalSeat; i++) {
        var SeatId = document.getElementById("seat-radio-1-" + i + "");
        var data = SeatId.value;
        var checked = SeatId.checked;
        if (data != "na") {
            if (checked == true) {
                var seatName = SeatId.value;
                rowData.push(seatName);
            }
        }
    }
    seatRow = rowData;
    seatData = rowData.join(", ");
    console.log(seatData);
    var ticketPrice = $("#ticketPrice").text();
    var totalTicket = parseInt(ticketPrice.split("Rs.")[1]) * (rowData.length);
    $("#totalTicket").text("Rs." + totalTicket);
    $("#seatData").text(seatData);
    AmountToPay = totalTicket;
})
function BusSearch() {
    debugger;
    ScreenLevel = 1;
    data = "";
    var ServiceID = $("#hdnServiceID").val();
    var CouponCode = $("#hdncouponcode").val();
    if (ServiceID == "") {
        $("#dvMessage").html("Please select ServiceID");
        return false;
    }
    var from = $("#from").val();
    var to = $("#to").val();
    var date = $("#date").val();
    var shift = $("#shift").val();

    if (from == "" || from == "undefined") {
        $("#dvMessage").html("Please choose Origin");
        return false;
    }
    if (to == "" || to == "undefined") {
        $("#dvMessage").html("Please choose destination");
        return false;
    }
    if (date == "" || date == "undefined") {
        $("#dvMessage").html("Please choose date");
        return false;
    }
    if (shift == "" || shift == "undefined") {
        $("#dvMessage").html("Please select shift");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/BusSewa/SearchBus",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ From: from, To: to, Date: date, Shift: shift }),
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
                        if (response.Message == "success") {
                            $('#AjaxLoader').hide();
                            Busdata = JSON.stringify(response.objReq);
                            BusTrip = response.BusTrip;
                            window.location.href = "/BusSewa/SearchBusList";
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
                            $(".project-list-table").hide();
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
function BookSeat() {
    debugger;
    var check = $("#passengerdetails").css("display");

    var seatselect = $("#seatData").text();
    if (seatselect == null || seatselect == "") {
        $("#DivErrMessage").html("Please select any seat");
    }
    else {
        if (check == "none") {
            SubmitBus();
            var check = $("#passengerdetails").show();
        }
        else {
            var check = $("#passengerdetails").hide();
        }
    }
}

function SubmitBus() {
    ScreenLevel = 2;
    debugger;
    $("#hfPaymentMode").val('1');

    var from = $("#from").val();
    var to = $("#to").val();
    var date = $("#date").val();
    var departTime = $("#departTime").val();
    var shift = $("#shift").val();
    BusId = $("#id").val();
    var Operator = $("#operatorName").val();
    var BusType = $("#busType").val();

    var data = JSON.stringify({ id: BusId, seat: seatRow });
    if (passengerName == "") {
        $("#dvMessage").html("Please enter your Name");
        return false;
    }
    if (passengerAge == "") {
        $("#dvMessage").html("Please enter your Age");
        return false;
    }
    if (passengerNumber == "") {
        $("#dvMessage").html("Please enter your Number");
        return false;
    }
    if (passengerEmail == "") {
        $("#dvMessage").html("Please enter your Email");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/BusSewa/BookSeat",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ data: data, DepartureDate: date, DepartureTime: departTime, From: from, To: to, Operator: Operator, BusType: BusType }),
                    success: function (response) {
                        var jsonData;
                        var IsValidJson = false;
                        try {
                            jsonData = $.parseJSON(response);
                            var IsValidJson = true;

                        }
                        catch (err) {

                        }

                        if (response.objReq.Message == "success") {
                            $('#AjaxLoader').hide();
                            Busdata = JSON.stringify(response.objReq);
                            $("#ticketSrlNo").val(response.ticketSrlNo);
                            $("#busdetailId").val(response.objReq.BusdetailId);
                            $("#BoardingPoints").val(response.boardingPoints);
                            $("#BoardingPoints").val(response.boardingPoints);
                            $("#passengerdetails").show();

                            $('#BoardingPoints')
                                .find('option')
                                .remove()
                                .end();

                            var list = $("#BoardingPoints");
                            $.each(response.boardingPoints, function (index, item) {
                                debugger;
                                list.append(new Option(item, item));
                            });

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
            }, 20);
    }
}
function PassengerInfo() {
    ScreenLevel = 2;
    debugger;
    $("#hfPaymentMode").val('1');
    var passengerName = $("#passengerName").val();
    var passengerAge = $("#passengerAge").val();
    var passengerNumber = $("#passengerNumber").val();
    var passengerEmail = $("#passengerEmail").val();
    var boardingPoint = $("#BoardingPoints").val();
    ticketSrlNo = $("#ticketSrlNo").val();
    busdetailId = $("#busdetailId").val();
    var inputcode = $("#inputcode").val();

    var data = JSON.stringify({ id: BusId, name: passengerName, contactNumber: passengerNumber, email: passengerEmail, boardingpoint: boardingPoint, ticketSrlNo: ticketSrlNo });
    if (passengerName == "") {
        $("#dvMessage").html("Please enter your Name");
        return false;
    }
    if (passengerAge == "") {
        $("#dvMessage").html("Please enter your Age");
        return false;
    }
    if (passengerNumber == "") {
        $("#dvMessage").html("Please enter your Number");
        return false;
    }
    if (passengerEmail == "") {
        $("#dvMessage").html("Please enter your Email");
        return false;
    }
    else {
        $('#AjaxLoader').show();
        setTimeout(
            function () {
                $.ajax({
                    type: "POST",
                    url: "/BusSewa/MyPayBusPassenger",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ data: data, BusdetailId: busdetailId, inputcode: inputcode }),
                    success: function (response) {
                        var jsonData;
                        var IsValidJson = false;
                        try {
                            jsonData = $.parseJSON(response);
                            var IsValidJson = true;

                        }
                        catch (err) {

                        }

                        if (response.Message == "success") {
                            $('#AjaxLoader').hide();
                            Busdata = JSON.stringify(response.objReq);
                            $("#ticketSrlNo").val(response.ticketSrlNo);
                            $("#busdetailId").val(response.objReq.busdetailId);
                            goToPay();

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
            }, 20);
    }
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
function calculatePrice(table, count, NumId, PriceId) {
    // Access the rows within the table
    var rows = table.getElementsByTagName("tr");
    var i = 1;
    var result = 0;
    TotalTraveller = 0;
    var dataList = [];
    var CableCardata = [];
    // Get a reference to the table
    var trElements = document.querySelectorAll("tr th[id='ticketDetails']");

    // Loop through and remove each <tr> element
    trElements.forEach(function (thElement) {
        var trElement = thElement.closest("tr");
        if (trElement) {
            trElement.remove();
        }
    });
    Array.from(rows).forEach(function (element, index) {
        debugger;
        // Perform your desired operations on each row
        if (count > index) {
            var elementId = PriceId + "_" + i;
            var element = document.getElementById(elementId);
            TicketId = NumId + "_" + i;
            var NoOfTicket = $("#" + TicketId).val();
            /*var TicketName = element.parentElement.parentElement.childNodes[1];*/
            var nameId = "TicketName_" + i;
            var TicketId = document.getElementById(nameId);
            var PassengerType = TicketId.getAttribute("value");
            var Ticket = $(TicketId).text();
            if (NoOfTicket > 0) {
                TotalTraveller = parseInt(TotalTraveller) + parseInt(NoOfTicket);
                var TicketName = Ticket.split(" Ticket")[0]; // Assuming ticket name is in the first child node
                var TicketRate = element.childNodes[1].textContent; // Assuming ticket rate is in the second child node

                dataList.push({ PassengerType: TicketName, Price: TicketRate, PassengerCount: NoOfTicket, TicketType: TicketType });
                CableCardata.push({ TripType: TicketType, PassengerType: PassengerType, PassengerCount: NoOfTicket });
                result = parseInt(result) + parseInt(TicketRate * NoOfTicket);
                addRowAfterThree(dataList);
            }
            i = ++i;
        }
    });
    data = JSON.stringify(dataList);
    CableCarData = JSON.stringify(CableCardata);
    TotalPrice = result;
    AmountToPay = parseInt(result) + parseFloat(Tax);
}

function bookTicket() {
    $("#dvMessage").html('');
    if (TotalTraveller == "" || TotalTraveller == "0") {
        $("#dvMessage").html("Please select any Ticket Type");
        return false;
    }
    var name = $("#name").val();
    if (name == "") {
        $("#dvMessage").html("Please enter contact name");
        return false;
    }
    var phone = $("#phonenum").val();
    if (phone == "") {
        $("#dvMessage").html("Please enter contact name");
        return false;
    }
    if (Number(TotalTraveller) > 25) {
        $("#dvMessage").html("You can book max 25 tickets");
        return false;
    }
    if (phone.length < 10) {
        $("#dvMessage").html("Please enter valid Number");
        return;
    }

    var IsValid = false;
    var regex1 = /^([9][8][5][0-9]{7})$/;
    var regex2 = /^([9][7][4-5][0-9]{7})$/;
    var regex3 = /^([9][8][46][0-9]{7})$/;
    var regex4 = /^([9][8][0-2][0-9]{7})$/;
    var regex5 = /^([9][6][0-9]{8}|[9][8][8][0-9]{7})$/;

    if (phone.match(regex1)) {
        IsValid = true;
    }
    else if (phone.match(regex2)) {
        IsValid = true;
    }
    else if (phone.match(regex3)) {
        IsValid = true;
    }
    else if (phone.substring(0, 3) == "976") {
        IsValid = true;
    }
    else if (phone.match(regex4)) {
        IsValid = true;
    }
    else if (phone.match(regex5)) {
        IsValid = true;
    }

    if (!IsValid) {
        $("#dvMessage").html("Please enter valid Number");
        return;
    }
    var Email = $("#email").val();
    if (Email == "") {
        $("#dvMessage").html("Please enter Email Address");
        return false;
    }
    else if (!isEmail(Email)) {
        $("#dvMessage").html("Please enter a valid Email ID");
        return false;
    }
    goToPay();
    //$("#DivCableCar").hide();
    //$("#DivProceedToPay").show();
    //$("#lblAmount").text(AmountToPay);

}

$(".proceedBus").click(function () {
    debugger;
    var i = $(this).data('index');
    var id = $("#id_" + i).val();
    inputTypeCode = $("#inputTypeCode_" + i).val();
    var operator = $("#operator_" + i).text();
    var busType = $("#busType_" + i).text();
    var shift = $("#shift_" + i).text();
    var operatorName = operator + shift;
    var ticketPrice = $("#ticketPrice_" + i).text();
    var departTime = $("#departTime_" + i).text();
    console.log(id);
    window.location.href = "/BusSewa/BusProceed?TripId=" + id + "&TicketPrice=" + ticketPrice + "&DepartTime=" + departTime + "&Operator=" + operator + "&Shift=" + shift + "&BusType=" + busType + "&inputcode=" + inputTypeCode;

});

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
    debugger;
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
    $("#DivBusSewa").hide();
    $("#passengerdetails").hide();
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
    var Amount = "";
    Amount = AmountToPay;
    var WalletAmountDeduct = Amount;
    var Contact = $("#txt_Contact").val();

    var PaymentMode = $("#hfPaymentMode").val('1');
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
                    if (IsValidJson) {
                        var MPCoinsDebit = parseFloat(arr['MPCoinsDebit']);
                        if (MPCoinsDebit > 0) {
                            WalletAmountDeduct = parseFloat(WalletAmountDeduct) - MPCoinsDebit;
                        }
                        $("#MobilePopup").text(Contact);
                        $("#AmountPopup").text(arr['Amount']);
                        $("#CashbackPopup").text(arr['CashbackAmount']);
                        $("#ServiceChargesPopup").text(arr['ServiceChargeAmount']);
                        $("#smartpayRupees").text(WalletAmountDeduct);
                        $("#smartpayCoins").text(arr['MPCoinsDebit']);
                        $("#lblCashback").text(arr['CashbackAmount']);
                        $("#lblServiceCharge").text(arr['ServiceChargeAmount']);
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
                            var DeductServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = (parseFloat(DeductAmount + DeductServiceCharge) - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));
                        }
                        else {
                            $("#AmountPopup").text(arr['Amount']);
                            var Amount = parseFloat(arr['Amount']);
                            var ServiceCharge = parseFloat(arr['ServiceChargeAmount']);
                            var totalamount = (parseFloat(Amount + ServiceCharge) - discount);
                            $("#TotalAmountPopup").text(parseFloat(totalamount).toFixed(2));

                        }

                        var MPCoinsCredit = parseFloat(arr['RewardPoints']);

                        if (MPCoinsCredit > 0) {
                            $("#MypayCoinCreditedPopup").closest('tr').show();
                        }
                        /*CheckCoinBalance(0);*/

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
    debugger;
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
    //    CheckCoinBalance(0);
});
$("#btnProceedToPay").click(function () {
    debugger;
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
    debugger;
    data = "";
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

    var Id = $("#id").val();

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
                    url: "/BusSewa/MyPayBusSewaPayment",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: Id, ticketSrlNo : ticketSrlNo, Amount: AmountToPay, busdetailId : busdetailId, Mpin: Mpin, PaymentMode: PaymentMode }),
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
                            if (jsonData.Message.toLowerCase() == "success") {
                                $("#DivPin").modal("hide");
                                $("#txn-success").html(jsonData.responseMessage);
                                $('#AjaxLoader').hide();
                                $('#ReferenceNo').val(jsonData.ReferenceNo);
                                $('#TransactionUniqueId').val(jsonData.TransactionUniqueId);
                                $("#PaymentSuccessBusSewa").modal("show");
                                //TicketInvoice();
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

function DownloadTicket(TransactionId) {
    debugger;
    $('#AjaxLoader').show();
    setTimeout(

        $.ajax({
            type: "POST",
            url: "/MyPayUser/MyPayUserCableCarTicketDownload", //call your controller and action
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"TransactionId":"' + TransactionId + '"}',
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
                window.location.href = "/MyPayUser/MyPayUserCableCarBooking";

            }
        }), 20);
}