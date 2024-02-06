var table;
var take = 50;
var skip = 0;
var sort = "";
var sortdir = "";


function BindFlightBookingDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var MemberId = $("#MemberId").val();
            var BookingId = $("#BookingId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var TripType = $("#TripType :selected").val();
            //var FilterTotalCount = 0;
            //var TotalCredit = 0;
            //var TotalDebit = 0;
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Flight Booking"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/FlightReport/GetFlightBookingLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.BookingId = BookingId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.TripType = TripType;
                        take = data.length;
                        skip = data.start;
                        sort = data.order[0].column;
                        sortdir = data.order[0].dir;
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
                    { "data": "BookingCreateddt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Member Id"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsFlightIssued) {
                                return '<a href="/AdminTransactions?SubscriberId=' + data.BookingId + '"> <span><strong>' + data.BookingId + '</strong></span></a>';
                            }
                            else {
                                return '<span class="tb-lead">' + data.BookingId + '</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Booking Id"
                    },
                    { "data": "TripType", "name": "Trip Type", "autoWidth": true, "bSortable": false },
                    { "data": "FlightType", "name": "Flight Type", "autoWidth": true, "bSortable": false },
                    { "data": "ContactName", "name": "Contact Name", "autoWidth": true, "bSortable": false },
                    { "data": "ContactPhone", "name": "Contact Phone", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsFlightIssued) {
                                return '<span class="tb-status text-success">Issued</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">Pending</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Flight Issued Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsFlightBooked) {
                                return '<span class="tb-status text-success">Booked</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">Pending</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Flight Booking Status"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.Refundable) {
                                return '<span class="tb-status text-success">Yes</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">No</span>';
                            }
                        },
                        bSortable: true,
                        sTitle: "Refundable"
                    },
                    { "data": "PnrNumber", "name": "Pnr Number", "autoWidth": true, "bSortable": false },
                    { "data": "Pax", "name": "Pax", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetFlightDetail(&apos;' + data.BookingId + '&apos;)" title="" data-original-title="Flight Detail"><em class="icon ni ni-table-view"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Flight Detail"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsFlightIssued) {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip"  onclick="return GetTxnDetail(&apos;' + data.BookingId + '&apos;)" title="" data-original-title="Txn Detail"><em class="icon ni ni-eye"></em></a> <a href="javascript:void(0);" style="display:none;" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled"  onclick="return GetFlightIssuedStatusCheck(&apos;' + data.BookingId + '&apos;)" title="" data-original-title="Txn Detail"><em class="icon ni ni-eye"></em></a>';
                            }
                            else {
                                return '<a href="javascript:void(0);" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled"  onclick="return GetFlightIssuedStatusCheck(&apos;' + data.BookingId + '&apos;)" title="" data-original-title="Txn Detail"><em class="icon ni ni-eye"></em></a>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Txn Detail"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/FlightReport/FlightPassengerDetail?BookingId=' + data.BookingId + '" class="btn btn-primary btn-sm btn-icon btn-tooltip" title="" data-original-title="Passenger Detail"><em class="icon ni ni-users"></em></a> <a href="javascript:void(0);" style="display:none" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled"  onclick="return GetFlightTicketDownload(&apos;' + data.BookingId + '&apos;)" title="" data-original-title="Ticket Detail"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Passenger Detail"
                    }
                    ,
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<a href="/FlightReport/GetFlightTicketDownload?BookingId=' + data.BookingId + '" class="btn btn-primary btn-sm btn-icon btn-tooltip" title="" data-original-title="Ticket Download"><em class="icon ni ni-ticket"></em></a> <a href="javascript:void(0);" style="display:none" class="btn btn-primary btn-sm btn-icon btn-tooltip disabled"  onclick="return GetFlightTicketDownload(&apos;' + data.BookingId + '&apos;)" title="" data-original-title="Ticket Download"><em class="icon ni ni-eye"></em></a>';
                        },
                        bSortable: false,
                        sTitle: "Ticket Download"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            //$("#totaltra").html(FilterTotalCount);
            //$("#totalCredit").html(" Rs." + parseFloat(TotalCredit).toFixed(2));
            //$("#totalDebit").html(" Rs." + parseFloat(TotalDebit).toFixed(2));
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}

function GetFlightDetail(BookingId) {
    $("#Inbound").html("");
    $("#Outbound").html("");
    if (BookingId != "") {
        $.ajax({
            type: "POST",
            url: "/FlightReport/GetFlightBookingDetail",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: '{"BookingId":"' + parseInt(BookingId) + '"}',
            success: function (response) {
                if (response != null) {
                    debugger;
                    var str = "";
                    if (response[0].IsInbound == false) {
                        str = '<h5>Departure Flight</h5>';
                        str += '<div class="table-responsive"><table class="nowrap nk-tb-list nk-tb-ulist table-bordered h-100">';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Booking Date :</span></td><td class="nk-tb-col"><span>' + response[0].BookingCreateddt + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Member Id :</span></td><td class="nk-tb-col"><span class="tb-lead">' + response[0].MemberId + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Booking Id :</span></td><td class="nk-tb-col"><span class="tb-lead">' + response[0].BookingId + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Trip Type :</span></td><td class="nk-tb-col"><span>' + response[0].TripTypeName + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Type :</span></td><td class="nk-tb-col"><span>' + response[0].FlightTypeName + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Adult :</span></td><td class="nk-tb-col"><span>' + response[0].Adult + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Child :</span></td><td class="nk-tb-col"><span>' + response[0].Child + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Aircraft Type :</span></td><td class="nk-tb-col"><span>' + response[0].Aircrafttype + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Airline Name :</span></td><td class="nk-tb-col"><span>' + response[0].Airlinename + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Departure :</span></td><td class="nk-tb-col"><span>' + response[0].Departure + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Departure Time :</span></td><td class="nk-tb-col"><span>' + response[0].Departuretime + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Arrival :</span></td><td class="nk-tb-col"><span>' + response[0].Arrival + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Arrival Time :</span></td><td class="nk-tb-col"><span>' + response[0].Arrivaltime + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Infant Fare :</span></td><td class="nk-tb-col"><span>' + response[0].Infantfare + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Child Fare :</span></td><td class="nk-tb-col"><span>' + response[0].Childfare + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Adult Fare :</span></td><td class="nk-tb-col"><span>' + response[0].Adultfare + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Tax :</span></td><td class="nk-tb-col"><span>' + response[0].Tax + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Total Fare :</span></td><td class="nk-tb-col"><span>' + response[0].Faretotal + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight ClassCode :</span></td><td class="nk-tb-col"><span>' + response[0].Flightclasscode + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight No. :</span></td><td class="nk-tb-col"><span>' + response[0].Flightno + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Free Baggage :</span></td><td class="nk-tb-col"><span>' + response[0].Freebaggage + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Date :</span></td><td class="nk-tb-col"><span>' + response[0].Flightdatedt.substring(0, 11) + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Contact Name :</span></td><td class="nk-tb-col"><span>' + response[0].ContactName + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>ContactPhone :</span></td><td class="nk-tb-col"><span>' + response[0].ContactPhone + '</span></td></tr>';
                        if (response[0].IsFlightIssued) {
                            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Issued Status : </span></td><td class="nk-tb-col"><span class="tb-status text-success">Issued</span></td></tr>';
                        }
                        else {
                            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Issued Status : </span></td><td class="nk-tb-col"><span class="tb-status text-danger">Pending</span></td></tr>';
                        }

                        if (response[0].IsFlightBooked) {
                            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Booking Status : </span></td><td class="nk-tb-col"><span class="tb-status text-success">Booked</span></td></tr>';
                        }
                        else {
                            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Booking Status : </span></td><td class="nk-tb-col"><span class="tb-status text-danger">Pending</span></td></tr>';
                        }

                        if (response[0].Refundable) {
                            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Refundable : </span></td><td class="nk-tb-col"><span class="tb-status text-success">Yes</span></td></tr>';
                        }
                        else {
                            str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Refundable : </span></td><td class="nk-tb-col"><span class="tb-status text-danger">No</span></td></tr>';
                        }

                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>PNR Number :</span></td><td class="nk-tb-col"><span>' + response[0].PnrNumber + '</span></td></tr>';
                        str += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Pax :</span></td><td class="nk-tb-col"><span>' + response[0].Pax + '</span></td></tr>';
                        str += '</table></div>';
                        $("#Inbound").append(str);
                        //$('#dvFlightDetail').modal('show');
                    }
                    if (response.length == 2) {
                        var str2 = "";
                        if (response[1].IsInbound == true) {
                            str2 = '<h5>Return Flight</h5>';
                            str2 += '<div class="table-responsive"><table class="nowrap nk-tb-list nk-tb-ulist table-bordered h-100">';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Booking Date :</span></td><td class="nk-tb-col"><span>' + response[1].BookingCreateddt + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Member Id :</span></td><td class="nk-tb-col"><span class="tb-lead">' + response[1].MemberId + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Booking Id :</span></td><td class="nk-tb-col"><span class="tb-lead">' + response[1].BookingId + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Trip Type :</span></td><td class="nk-tb-col"><span>' + response[1].TripTypeName + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Type :</span></td><td class="nk-tb-col"><span>' + response[1].FlightTypeName + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Adult :</span></td><td class="nk-tb-col"><span>' + response[1].Adult + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Child :</span></td><td class="nk-tb-col"><span>' + response[1].Child + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Aircraft Type :</span></td><td class="nk-tb-col"><span>' + response[1].Aircrafttype + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Airline Name :</span></td><td class="nk-tb-col"><span>' + response[1].Airlinename + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Departure :</span></td><td class="nk-tb-col"><span>' + response[1].Departure + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Departure Time :</span></td><td class="nk-tb-col"><span>' + response[1].Departuretime + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Arrival :</span></td><td class="nk-tb-col"><span>' + response[1].Arrival + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Arrival Time :</span></td><td class="nk-tb-col"><span>' + response[1].Arrivaltime + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Infant Fare :</span></td><td class="nk-tb-col"><span>' + response[1].Infantfare + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Child Fare :</span></td><td class="nk-tb-col"><span>' + response[1].Childfare + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Adult Fare :</span></td><td class="nk-tb-col"><span>' + response[1].Adultfare + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Tax :</span></td><td class="nk-tb-col"><span>' + response[1].Tax + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Total Fare :</span></td><td class="nk-tb-col"><span>' + response[1].Faretotal + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight ClassCode :</span></td><td class="nk-tb-col"><span>' + response[1].Flightclasscode + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight No. :</span></td><td class="nk-tb-col"><span>' + response[1].Flightno + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Free Baggage :</span></td><td class="nk-tb-col"><span>' + response[1].Freebaggage + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Date :</span></td><td class="nk-tb-col"><span>' + response[1].Flightdatedt.substring(0, 11) + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Contact Name :</span></td><td class="nk-tb-col"><span>' + response[1].ContactName + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>ContactPhone :</span></td><td class="nk-tb-col"><span>' + response[1].ContactPhone + '</span></td></tr>';
                            if (response[1].IsFlightIssued) {
                                str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Issued Status : </span></td><td class="nk-tb-col"><span class="tb-status text-success">Issued</span></td></tr>';
                            }
                            else {
                                str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Issued Status : </span></td><td class="nk-tb-col"><span class="tb-status text-danger">Pending</span></td></tr>';
                            }

                            if (response[1].IsFlightBooked) {
                                str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Booking Status : </span></td><td class="nk-tb-col"><span class="tb-status text-success">Booked</span></td></tr>';
                            }
                            else {
                                str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Flight Booking Status : </span></td><td class="nk-tb-col"><span class="tb-status text-danger">Pending</span></td></tr>';
                            }

                            if (response[1].Refundable) {
                                str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Refundable : </span></td><td class="nk-tb-col"><span class="tb-status text-success">Yes</span></td></tr>';
                            }
                            else {
                                str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Refundable : </span></td><td class="nk-tb-col"><span class="tb-status text-danger">No</span></td></tr>';
                            }

                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>PNR Number :</span></td><td class="nk-tb-col"><span>' + response[1].PnrNumber + '</span></td></tr>';
                            str2 += '<tr class="nk-tb-item"><td class="nk-tb-col"><span>Pax :</span></td><td class="nk-tb-col"><span>' + response[1].Pax + '</span></td></tr>';
                            str2 += '</table></div>';
                            $("#Outbound").append(str2);
                            //$('#dvFlightDetail').modal('show');
                        }
                    }
                    $('#dvFlightDetail').modal('show');
                    return false;
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

function BindFlightPassengerDataTable() {
    $('#AjaxLoader').show();
    setTimeout(
        function () {
            var FirstName = $("#FirstName").val();
            var LastName = $("#LastName").val();
            var BookingId = $("#BookingId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Flight Passenger Detail"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/FlightReport/GetFlightPassengerLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.FirstName = FirstName;
                        data.LastName = LastName;
                        data.BookingId = BookingId;
                    }
                },
                "columns": [
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": true },
                    { "data": "CreatedDatedt", "name": "Date", "autoWidth": true, "bSortable": true },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.BookingId + '</span>';
                        },
                        bSortable: true,
                        sTitle: "Booking Id"
                    },
                    { "data": "Firstname", "name": "First Name", "autoWidth": true, "bSortable": false },
                    { "data": "Lastname", "name": "Last Name", "autoWidth": true, "bSortable": false },
                    { "data": "Type", "name": "Type", "autoWidth": true, "bSortable": false },
                    { "data": "Gender", "name": "Gender", "autoWidth": true, "bSortable": false },
                    { "data": "Nationality", "name": "Nationality", "autoWidth": true, "bSortable": false },
                    { "data": "TicketNo", "name": "Ticket No", "autoWidth": true, "bSortable": false },
                    { "data": "InboundTicketNo", "name": "Inbound Ticket No", "autoWidth": true, "bSortable": false }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                }
            });
            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}