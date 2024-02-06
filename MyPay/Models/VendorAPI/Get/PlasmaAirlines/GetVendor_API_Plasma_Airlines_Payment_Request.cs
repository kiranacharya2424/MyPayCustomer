using MyPay.Models.Add;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.PlasmaAirlines
{
    public class GetVendor_API_Airlines_MyPay_Payment_Request : CommonGet
    {
       
        public string Data { get; set; }
        // Id
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        // credits_consumed
        private string _credits_consumed = string.Empty;
        public string credits_consumed
        {
            get { return _credits_consumed; }
            set { _credits_consumed = value; }
        }
        // credits_available
        private string _credits_available = string.Empty;
        public string credits_available
        {
            get { return _credits_available; }
            set { _credits_available = value; }
        }

        // message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        // WalletBalance
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // UniqueTransactionId
        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        // detailsstring
        private string _detail;
        public string detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

        // pin
        private string _pin;
        public string pin
        {
            get { return _pin; }
            set { _pin = value; }
        }
        // "serial"
        private string _serial;
        public string serial
        {
            get { return _serial; }
            set { _serial = value; }
        }
    }
 

    public class IssueTicketTwoWayResp
    {
        public ItineraryTwoWay Itinerary { get; set; }
    }
    public class IssueTicketResp
    {
        public Itinerary Itinerary { get; set; }
    }
    public class Itinerary
    {
       public Passenger Passenger { get; set; }

    }
    public class ItineraryTwoWay
    {
        public List<Passenger> Passenger { get; set; }

    }

    public class Passenger :CommonAdd
    {
        public string Airline { get; set; }
        public string PnrNo { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PaxNo { get; set; }
        public string PaxType { get; set; }
        public string Nationality { get; set; }
        public string PaxId { get; set; }
        public string IssueFrom { get; set; }
        public string AgencyName { get; set; }
        public string IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string FlightNo { get; set; }
        public string FlightDate { get; set; }
        public string Departure { get; set; }
        public string FlightTime { get; set; }
        public string TicketNo { get; set; }
        public string BarCodeValue { get; set; }
        public string BarcodeImage { get; set; }
        public string Arrival { get; set; }
        public string ArrivalTime { get; set; }
        public string Sector { get; set; }
        public string ClassCode { get; set; }
        public string Currency { get; set; }
        public string Fare { get; set; }
        public string Surcharge { get; set; }
        public string TaxCurrency { get; set; }
        public string Tax { get; set; }
        public string CommissionAmount { get; set; }
        public string Refundable { get; set; }
        public string Invoice { get; set; }
        public string ReportingTime { get; set; }
        public string FreeBaggage { get; set; }
        public int MemberId { get; set; }
        public string FlightId { get; set; }
        public string ReturnFlightId { get; set; }
        //public List<AddPlasmaTechIssueFlightDetails> PassengerDetail { get; set; }
    }
}