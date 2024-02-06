using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Output_Response
    {

        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        //private string _detail = string.Empty;
        //public string detail
        //{
        //    get { return _detail; }
        //    set { _detail = value; }
        //}
        private string _credits_consumed = string.Empty;
        public string credits_consumed
        {
            get { return _credits_consumed; }
            set { _credits_consumed = value; }
        }
        private string _credits_available = string.Empty;
        public string credits_available
        {
            get { return _credits_available; }
            set { _credits_available = value; }
        }
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _pin = string.Empty;
        public string pin
        {
            get { return _pin; }
            set { _pin = value; }
        }
        private string _serial = string.Empty;
        public string serial
        {
            get { return _serial; }
            set { _serial = value; }
        }
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        private List<int> _log_ids = new List<int>();
        public List<int> log_ids
        {
            get { return _log_ids; }
            set { _log_ids = value; }
        }
        private extra_data _extra_data = new Get.extra_data();
        public extra_data extra_data
        {
            get { return _extra_data; }
            set { _extra_data = value; }
        }
        private events_data _data = new Get.events_data();
        public events_data data
        {
            get { return _data; }
            set { _data = value; }
        }

        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }
        private PassengerData Passenger = new Get.PassengerData();
        public PassengerData PassengerData
        {
            get { return Passenger; }
            set { Passenger = value; }
        }
    }
    public class extra_data
    {

        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
    }

    public class events_data
    {

        private string _merchantCode = string.Empty;
        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        }
        private string _orderId = string.Empty;
        public string orderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
        private string _transactionId = string.Empty;
        public string transactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }
        private string _remarks = string.Empty;
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class PassengerData
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
    }



}