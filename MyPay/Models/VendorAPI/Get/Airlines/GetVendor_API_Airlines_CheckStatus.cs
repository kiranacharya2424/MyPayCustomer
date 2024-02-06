using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Airlines_CheckStatus : CommonGet
    {

        // error_code
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

        // FlightStatusDetail
        private FlightStatusDetail _detail = new FlightStatusDetail();
        public FlightStatusDetail detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

    }

    public class FlightStatusDetail
    {

        public string outbound_state { get; set; }
        public string inbound_state { get; set; }
        public string outbound_status { get; set; }
        public string inbound_status { get; set; }
        public string flight_id { get; set; }
        public string inbound_flight_id { get; set; }
        public string reference { get; set; }
        public CheckFlightStatusOutbound outbound { get; set; }
        public CheckFlightStatusInbound inbound { get; set; }
        public List<CheckFlightStatusPassenger> passengers { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class CheckFlightStatusInbound
    {
        public string airline { get; set; }
        public string airline_name { get; set; }
        public string pnr_no { get; set; }
        public string flight_no { get; set; }
        public string arrival_time { get; set; }
        public string departure_time { get; set; }
        public string flight_class_code { get; set; }
        public object currency { get; set; }
        public double fare_total { get; set; }
        public string inbound_reporting_time { get; set; }
    }

    public class CheckFlightStatusOutbound
    {
        public string airline { get; set; }
        public string airline_name { get; set; }
        public string pnr_no { get; set; }
        public string flight_no { get; set; }
        public string arrival_time { get; set; }
        public string departure_time { get; set; }
        public string flight_class_code { get; set; }
        public object currency { get; set; }
        public double fare_total { get; set; }
        public string reporting_time { get; set; }
    }

    public class CheckFlightStatusPassenger
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string title { get; set; }
        public string passenger_type { get; set; }
        public string gender { get; set; }
        public string ticket_no { get; set; }
        public string barcode { get; set; }
        public string inbound_ticket_no { get; set; }
        public string inbound_barcode { get; set; }
    }
     

}