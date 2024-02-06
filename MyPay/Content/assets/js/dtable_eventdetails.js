///////////////////////////////////////////////////
//// ****  DataTable SCRIPT   **** //////

///////////////////////////////////////////////////

var table;

function BindDataTable() {
    $('#AjaxLoader').show();
    
    setTimeout(
        function () {
            debugger;
            var MemberId = $("#MemberId").val();
            var ContactNumber = $("#ContactNumber").val();
            var Name = $("#Name").val();
            var TransactionId = $("#TransactionId").val();
            var fromdate = $("#fromdate").val();
            var todate = $("#todate").val();
            var OrderId = $("#OrderId").val();
            var EventName = $("#EventName").val();
            var PaymentMerchantId = $("#PaymentMerchantId").val();
            var MerchantCode = $("#MerchantCode").val();
            var PaymentStatus = $("#paymentstatus :selected").val();
            console.log(PaymentStatus);
            
            
            table = $('#tblist').DataTable({
                "dom": 'lBfrtip',
                bFilter: false,
                "oLanguage": {
                    "sEmptyTable": "No Event Details"
                },
                "lengthMenu": [[50, 100, 200, 500], [50, 100, 200, 500]],
                "pageLength": 50,
                "processing": true,
                "serverSide": true,
                "destroy": true,
                "order": [[1, "desc"]],
                "ajax": {
                    "url": "/EventDetailsReport/GetEventDetailLists",
                    "type": "POST",
                    "async": false,
                    data: function (data) {
                        data.MemberId = MemberId;
                        data.ContactNumber = ContactNumber;
                        data.Name = Name;
                        data.TransactionId = TransactionId;
                        data.fromdate = fromdate;
                        data.todate = todate;
                        data.OrderId = OrderId;
                        data.MerchantCode = MerchantCode;
                        data.PaymentMerchantId = PaymentMerchantId;
                        data.EventName = EventName;
                        data.PaymentStatus = PaymentStatus;
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
                    { "data": "Sno", "name": "SrNo", "autoWidth": true, "bSortable": false },
                    { "data": "IndiaDate", "name": "Date", "autoWidth": true, "bSortable": true },
                    { "data": "TransactionUniqueId", "name": "Transaction Id", "autoWidth": true, "bSortable": false },
                    { "data": "OrderId", "name": "Order Id", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<span class="tb-lead">' + data.MemberId + '</span>';
                        },
                        bSortable: false,
                        sTitle: "Member Id"
                    },
                    { "data": "eventId", "name": "Event Id", "autoWidth": true, "bSortable": false },
                    { "data": "eventName", "name": "Event Name", "autoWidth": true, "bSortable": false },
                    { "data": "eventDateString", "name": "Event Date", "autoWidth": true, "bSortable": false },
                    { "data": "eventStartTime", "name": "Event Start Time", "autoWidth": true, "bSortable": false },
                    { "data": "eventEndTime", "name": "Event End Time", "autoWidth": true, "bSortable": false },
                    { "data": "venueName", "name": "Venue Name", "autoWidth": true, "bSortable": false },
                    { "data": "venueAddress", "name": "Venue Address", "autoWidth": true, "bSortable": false },
                    { "data": "venueCapacity", "name": "Venue Capacity", "autoWidth": true, "bSortable": false },
                    { "data": "parkingAvailable", "name": "Parking Available", "autoWidth": true, "bSortable": false },
                    { "data": "eventType", "name": "Event Type", "autoWidth": true, "bSortable": false },
                    { "data": "organizerName", "name": "Organizer Name", "autoWidth": true, "bSortable": false },
                    { "data": "merchantCode", "name": "Merchant Code", "autoWidth": true, "bSortable": false },
                    { "data": "customerName", "name": "Customer Name", "autoWidth": true, "bSortable": false },
                    { "data": "customerMobile", "name": "Customer Mobile", "autoWidth": true, "bSortable": false },
                    { "data": "customerEmail", "name": "Customer Email", "autoWidth": true, "bSortable": false },
                    { "data": "ticketCategoryName", "name": "Ticket Category", "autoWidth": true, "bSortable": false },
                    { "data": "sectionName", "name": "Section", "autoWidth": true, "bSortable": false },
                    { "data": "ticketRate", "name": "Ticket Rate", "autoWidth": true, "bSortable": false },
                    { "data": "noOfTicket", "name": "No of Tickets", "autoWidth": true, "bSortable": false },
                    { "data": "totalPrice", "name": "Total Price", "autoWidth": true, "bSortable": false },
                    { "data": "paymentMethodName", "name": "Payment Method", "autoWidth": true, "bSortable": false },
                    { "data": "paymentMerchantId", "name": "Payment MerchantId", "autoWidth": true, "bSortable": false },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsBooked) {
                                return '<span class="tb-status text-success">Yes</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">No</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Is Booked"
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (data.IsPaymentDone) {
                                return '<span class="tb-status text-success">Yes</span>';
                            }
                            else {
                                return '<span class="tb-status text-danger">No</span>';
                            }
                        },
                        bSortable: false,
                        sTitle: "Is Payment Done"
                    }
                ],
                "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $(nRow).addClass('nk-tb-item');

                    $('td', nRow).addClass('nk-tb-col tb-col-lg');

                    var oSettings = this.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                }
            });

            document.getElementById("tblist").deleteTFoot();

            $("#tblist").append(
                $('<tfoot/>').append($("#tblist thead tr").clone())
            );
            $('#AjaxLoader').hide();
        }, 100);
}