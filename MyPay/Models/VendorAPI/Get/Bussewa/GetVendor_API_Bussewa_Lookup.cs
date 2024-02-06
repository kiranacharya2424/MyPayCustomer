using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Bussewa_Lookup : CommonGet
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


    public class GetVendor_API_Bussewa_Routes_Lookup : CommonGet
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
        public bool status { get; set; }
        public List<BusRoute> routes { get; set; }

    }
    public class BusRoute
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}