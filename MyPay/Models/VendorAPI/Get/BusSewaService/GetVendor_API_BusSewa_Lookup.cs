using MyPay.Models.Common;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.BusSewaService
{
    public class GetVendor_API_BusSewa_Lookup_new
    {
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        // details
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }
        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }

        // Bussewa_data
        private List<BusListResponse> _buses = new List<BusListResponse>();
        public List<BusListResponse> buses
        {

            get { return _buses; }

            set { _buses = value; }
        }
    }

    public class BusSeatLayoutResponse
    {
        public string displayName { get; set; }
        public string bookingStatus { get; set; }
    }

    public class BusListResponse
    {
        public int no_of_column { get; set; }
        public bool lock_status { get; set; }
        public string departure_time { get; set; }
        public string image_list { get; set; }
        public string bus_no { get; set; }
        public string @operator { get; set; }
        public object ticket_price { get; set; }
        public List<BusSeatLayoutResponse> seat_layout { get; set; }
        public List<object> passenger_detail { get; set; }
        public List<string> amenities { get; set; }
        public int input_type_code { get; set; }
        public string date { get; set; }
        public string id { get; set; }
        public string bus_type { get; set; }
        public bool multi_price { get; set; }
        public int rating { get; set; }
        public string date_en { get; set; }
    }


    public class GetVendor_API_BusSewa_Routes_Lookup
    {
        public string error { get; set; }
        public string details { get; set; }
        public string errorcode { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public object routes { get; set; }


    }
    public class GetDataFromBusSewa
    {
        public string message { get; set; }
        public string Id { get; set; }
        public bool IsException { get; set; }
        public string TransactionId { get; set; }
        public string MerchantOrderTxnId { get; set; }

    }
    public class Get_BusRoute
    {
        public string label { get; set; }

    }

    //public class Res_Vendor_API_BusSewa_Routes_Lookup_Requests
    //{
    //    public bool status { get; set; }
    //    public int ReponseCode { get; set; }
    //    public string Message { get; set; }
    //    public object Data { get; set; }

    //}
    #region bus-trips

    //public class Response_bus_trips
    //{
    //    public bool status { get; set; }
    //    public int ReponseCode { get; set; }
    //    public string Message { get; set; }
    //    public object Data { get; set; }

    //}
    public class TripDetail
    {
        public string from { get; set; }
        public string to { get; set; }
        public string shift { get; set; }
        public string date { get; set; }
    }

    public class Rootobject : error
    {
        public int status { get; set; }
        public Trip[] trips { get; set; }
    }
    public class Trip
    {
        public string id { get; set; }
        public string _operator { get; set; }
        public string date { get; set; }
        public string faceImage { get; set; }
        public string busNo { get; set; }
        public string busType { get; set; }
        public string departureTime { get; set; }
        public string shift { get; set; }
        public int journeyHour { get; set; }
        public string dateEn { get; set; }
        public bool lockStatus { get; set; }
        public bool multiPrice { get; set; }
        public bool touristFlag { get; set; }
        public int inputTypeCode { get; set; }
        public int noOfColumn { get; set; }
        public float rating { get; set; }
        public object[] imgList { get; set; }
        public string[] amenities { get; set; }
        public string[] detailImage { get; set; }
        public float ticketPrice { get; set; }
        public object passengerDetail { get; set; }
        public object seatLayout { get; set; }
        public string operator_name { get; set; }
    }


    #endregion

    #region Seat refresh

    public class RootobjectRefreshAPI : error
    {
        public int status { get; set; }
        public bool lockStatus { get; set; }
        public int noOfColumn { get; set; }
        public object seatLayout { get; set; }
    }


    #endregion
    #region book seat

  
    public class BookSeat
    {
        public string id { get; set; }
        public object seat { get; set; }
    }

    public class RootobjectBookSeat : error
    {
        public int status { get; set; }
        public string ticketSrlNo { get; set; }
        public string timeOut { get; set; }
        public string[] boardingPoints { get; set; }
    }


    #endregion
    #region Cancel Queue
    public class CancelQueue
    {
        public string id { get; set; }
        public object ticketSrlNo { get; set; }
    }

    public class RootobjectCancelQueue : error
    {
        public int status { get; set; }
        public int noOfColumn { get; set; }
        public bool lockStatus { get; set; }
        public object seatLayout { get; set; }
    }



    #endregion
    #region payment confirmation
    public class paymentConfirm
    {
        public string id { get; set; }
        public string refId { get; set; }
        public string ticketSrlNo { get; set; }
    }

    public class paymentConfirmation : error
    {
        public int status { get; set; }
        public string BusNo { get; set; }
        public string[] contactInfo { get; set; }
    }

    

    #endregion
    #region Ticket Query

    public class TicketQuery : error
    {
        public int status { get; set; }
    }

    #endregion
    #region Passenger Info
    public class PassengerConfirmation : error
    {
        public int status { get; set; }


    }
    public class jsonPassengerInfo1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
    }

    public class jsonPassengerInfo2
    {
        public string id { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
        public object passengerTypeDetail { get; set; }
    }
    public class jsonPassengerInfo3
    {
        public string id { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
        public object passengerPriceDetail { get; set; }
    }
    public class jsonPassengerInfo4
    {
        public string id { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
        public object passengerFullDetail { get; set; }
    }

    //public class Passengertypedetail
    //{
    //    public string seat { get; set; }
    //    public Passengerdetail[] passengerDetail { get; set; }
    //}

    //public class Passengerdetail
    //{
    //    public int id { get; set; }
    //    public string detail { get; set; }
    //}


    #endregion
    public class commonresponsedata
    {
        public bool status { get; set; }
        public string StatusCode { get; set; }
        public int ReponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string BusdetailId { get; set; }
        public string TransactionUniqueId { get; set; }

    }
    public class CommonResponseData
    {
        public bool status { get; set; }
        public string StatusCode { get; set; }
        public int ReponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string Id { get; set; }
        public string TransactionUniqueId { get; set; }
        public string value1 { get; set; }
        public string value2 { get; set; }

    }

    public class error
    {
        public string errorCode { get; set; }
        public string message { get; set; }
    }
    public class Receipt
    {
        public string id { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
        public string name { get; set; }
        public string BusNo { get; set; }
        public string PaymentStatus { get; set; }
        public string Platform { get; set; }
        public string contactInfo { get; set; }
        public string seat { get; set; }
        public string BookingDate { get; set; }
        public string transactionid { get; set; } 
        public string Amount { get; set; } 
        public string ServiceCharge { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string shift { get; set; }
        public string date { get; set; }
        public string Time { get; set; }
        public string Operator { get; set; }
        public string BusType { get; set; }
        public string TransactionDate { get; set; }
        public string Type { get; set; } 
        public string MiddleName { get; set; } 
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string userContact { get; set; }
        public string cashback { get; set; }
        public string commission { get; set; }
        public string NoofSeat { get; set; }
    }

    public class DownloadPDF
    {
        public string details { get; set; }
        public string Message { get; set; }
        public bool status { get; set; }
        public string FilePath { get; set; }
        public bool IsException { get; set; }



    }
}