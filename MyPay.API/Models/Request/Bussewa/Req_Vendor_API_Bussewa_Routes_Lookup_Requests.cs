using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Bussewa_Routes_Lookup_Requests : CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }



    }
    public class Req_Vendor_API_Bussewa_Lookup_RequestsTrip : CommonProp
    {
        public string Reference { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string shift { get; set; }
        public string date { get; set; }
    }
    public class Req_Vendor_API_Bussewa_Lookup_RequestsRefresh : CommonProp
    {
        public string Reference { get; set; }
        public string Id { get; set; }

    }

    public class Req_Vendor_API_Bussewa_Lookup_Requestsbookseat : CommonProp
    {
        public string Reference { get; set; }
        // public RequestBookSeats data { get; set; }
        public string data { get; set; }
        public string BusDetailId { get; set; }
        public string DepatureDate { get; set; }
        public string DepatureTime { get; set; }
        public string Operator { get; set; }
        public string BusType { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }
    public class RequestBookSeats
    {
        public string id { get; set; }
        public object seat { get; set; }


    }
    public class Req_Vendor_API_Bussewa_Lookup_RequestsCancelQueue : CommonProp
    {
        public string Reference { get; set; }
        public string Id { get; set; }
        public string ticketSrlNo { get; set; }
        public string BusDetailId { get; set; }


    }
    public class Req_Vendor_API_Bussewa_Lookup_RequestPassengerDetail : CommonProp
    {
        public string Reference { get; set; }
        public string inputcode { get; set; }
        public string data { get; set; }
        public string BusDetailId { get; set; }
    }

    public class RequestPassengerDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
        public object passengerTypeDetail { get; set; }
        public object passengerPriceDetail { get; set; }
        public object passengerFullDetail { get; set; }
    }
    public class passengertype
    {
        public string seat { get; set; }
        public object passengerDetail { get; set; }
    }
    public class passengertype3
    {
        public string seat { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
    public class passengertype4
    {
        public string seat { get; set; }
        public string id { get; set; }
        public object passengerDetail { get; set; }
    }
    public class passengername
    {
        public string id { get; set; }
        public string detail { get; set; }
    }
    public class Req_Vendor_API_Bussewa_Lookup_RequestsPaymentConfirm : CommonProp
    {
        public string Reference { get; set; }
        public string Id { get; set; }
        public string ticketSrlNo { get; set; }
        public string refId { get; set; }
        public string Amount { get; set; }
        public string BusDetailId { get; set; }

    }

    public class BuscommonData
    {
        public string code { get; set; }
        public string message { get; set; }
        public string BusDetailId { get; set; }


    }


    public class Request_BusTicket_Download : CommonProp
    {
        public string Reference { get; set; }
        public string LogID { get; set; }
        public string VendorApiType { get; set; }
        public bool IsEmailSend { get; set; }

    }



    public class TripResponse
    {
        public int status { get; set; }
        public object trips { get; set; }
    }

    public class TripAPiData
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
        public object imgList { get; set; }
        public object amenities { get; set; }
        public object detailImage { get; set; }
        public float ticketPrice { get; set; }
        public object passengerDetail { get; set; }
        public object seatLayout { get; set; }
        public string operator_name { get; set; }
    }

    public class Passengerdetail
    {
        public int id { get; set; }
        public string detail { get; set; }
    }

    public class jsonPassenger2
    {
        public string id { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string boardingPoint { get; set; }
        public string ticketSrlNo { get; set; }
        public List<Passengertypedetail> passengerTypeDetail { get; set; }
    }
    public class PassengerRootobject
    {
        public List<Passengertypedetail> passengerTypeDetail = new List<Passengertypedetail>();
    }

    public class Passengertypedetail
    {
        public string seat { get; set; }
        public List<Passengerdetail> passengerDetail { get; set; }

    }

    //public class Passengerdetails
    //{
    //    public int id { get; set; }
    //    public string detail { get; set; }
    //}

    

    //public class Seatlayout
    //{
    //    public string displayName { get; set; }
    //    public string bookingStatus { get; set; }
    //    public string bookedByCustomer { get; set; }
    //}

}