using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Airlines_Lookup : CommonGet
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

        // FilePath
        private string _FilePath = string.Empty;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        // Inbound
        private List<Inbound> _inbound = new List<Inbound>();
        public List<Inbound> inbound
        {
            get { return _inbound; }
            set { _inbound = value; }
        }

        // Outbound
        private List<Outbound> _outbound = new List<Outbound>();
        public List<Outbound> outbound
        {
            get { return _outbound; }
            set { _outbound = value; }
        }
        // BookingId
        private string _booking_id = string.Empty;
        public string booking_id
        {
            get { return _booking_id; }
            set { _booking_id = value; }
        }

    }
    public class Inbound
    {
        public string aircraft_type { get; set; }
        public string airline_name { get; set; }
        public string departure { get; set; }
        public bool refundable { get; set; }
        public string infant_fare { get; set; }
        public string flight_class_code { get; set; }
        public string currency { get; set; }
        public string fare_total { get; set; }
        public string child_fare { get; set; }
        public string child { get; set; }
        public string departure_time { get; set; }
        public string tax { get; set; }
        public string airline { get; set; }
        public string adult { get; set; }
        public string adult_fare { get; set; }
        public string airline_logo { get; set; }
        public string flight_no { get; set; }
        public string fuel_surcharge { get; set; }
        public string arrival_time { get; set; }
        public string res_fare { get; set; }
        public string free_baggage { get; set; }
        public string arrival { get; set; }
        public string flight_id { get; set; }
        public string flight_date { get; set; }
        public string infant { get; set; }
    }

    public class Outbound
    {
        public string aircraft_type { get; set; }
        public string airline_name { get; set; }
        public string rtdepaure { get; set; }
        public bool refundable { get; set; }
        public string infant_fare { get; set; }
        public string flight_class_code { get; set; }
        public string currency { get; set; }
        public string fare_total { get; set; }
        public string child_fare { get; set; }
        public string child { get; set; }
        public string child_commission { get; set; }
        public string departure { get; set; }
        public string departure_time { get; set; }
        public string tax { get; set; }
        public string agency_commission { get; set; }
        public string airline { get; set; }
        public string adult { get; set; }
        public string adult_fare { get; set; }
        public string airline_logo { get; set; }
        public string flight_no { get; set; }
        public string fuel_surcharge { get; set; }
        public string arrival_time { get; set; }
        public string res_fare { get; set; }
        public string free_baggage { get; set; }
        public string arrival { get; set; }
        public string flight_id { get; set; }
        public string flight_date { get; set; }
        public string infant { get; set; }
    }

    public class ReqCablecardownload : CommonGet
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

        // FilePath
        private string _FilePath = string.Empty;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
      
        // BookingId
        private string _booking_id = string.Empty;
        public string booking_id
        {
            get { return _booking_id; }
            set { _booking_id = value; }
        }

    }

}