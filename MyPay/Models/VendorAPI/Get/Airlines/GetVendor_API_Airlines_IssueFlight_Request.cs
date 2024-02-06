using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Airlines_IssueFlight_Request : CommonGet
    {

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

        public string credits_available { get; set; }
        public List<int> response_id { get; set; }
        public List<int> log_ids { get; set; }
        public List<IssueFlightPassenger> passengers { get; set; }
        public string return_date { get; set; }
        public string flight_date { get; set; }
        public string commission { get; set; }
        public string trip_type { get; set; }
        public string sector_from { get; set; }
        public string sector_to { get; set; }
        public IssueFlightInbound inbound { get; set; }
        public string credits_consumed { get; set; }
        public IssueFlightOutbound outbound { get; set; }
    }

    public class IssueFlightPassenger
    {
        public string barcode { get; set; }
        public string ticket_no { get; set; }
        public string inbound_ticket_no { get; set; }
        public string lastname { get; set; }
        public string passenger_type { get; set; }
        public string title { get; set; }
        public string inbound_barcode { get; set; }
        public string gender { get; set; }
        public string firstname { get; set; }
    }

    public class IssueFlightInbound
    {
        public string pnr_no { get; set; }
        public string fare_total { get; set; }
        public string airline_name { get; set; }
        public string airline { get; set; }
        public string departure_time { get; set; }
        public string flight_no { get; set; }
        public string flight_class_code { get; set; }
        public object currency { get; set; }
        public string inbound_reporting_time { get; set; }
        public string arrival_time { get; set; }
    }

    public class IssueFlightOutbound
    {
        public string pnr_no { get; set; }
        public string fare_total { get; set; }
        public string airline_name { get; set; }
        public string airline { get; set; }
        public string departure_time { get; set; }
        public string flight_no { get; set; }
        public string reporting_time { get; set; }
        public string flight_class_code { get; set; }
        public object currency { get; set; }
        public string arrival_time { get; set; }
    }


}