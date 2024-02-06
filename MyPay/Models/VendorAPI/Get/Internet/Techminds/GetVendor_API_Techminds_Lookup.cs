using MyPay.Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Techminds_Lookup : CommonGet
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
        // data
        private techminds_data _data = new techminds_data();
        public techminds_data data
        {
            get { return _data; }
            set { _data = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
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
        // techminds_data
        private techminds_bills _available_plans = new techminds_bills();
        public techminds_bills available_plans
        {

            get { return _available_plans; }

            set { _available_plans = value; }
        }
    }
    public class techminds_data
    {
        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // expiration
        private string _expiration = string.Empty;
        public string expiration
        {
            get { return _expiration; }
            set { _expiration = value; }
        }
        // mobile_number
        private string _mobile_number = string.Empty;
        public string mobile_number
        {
            get { return _mobile_number; }
            set { _mobile_number = value; }
        }
        //previous_balance
        private string _previous_balance = string.Empty;
        public string previous_balance
        {
            get { return _previous_balance; }
            set { _previous_balance = value; }
        }
        // email
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        // monthly_charge
        private string _monthly_charge = string.Empty;
        public string monthly_charge
        {
            get { return _monthly_charge; }
            set { _monthly_charge = value; }
        }
    }
    public class techminds_bills
    {
        [JsonProperty("1 Month(s)")]
        public string _1_Month { get; set; }

        [JsonProperty("3 Month(s)")]
        public string _3_Month { get; set; }

        [JsonProperty("6 Month(s)")]
        public string _6_Month { get; set; }

        [JsonProperty("12 Month(s)")]
        public string _12_Month { get; set; }

        [JsonProperty("15 Days")]
        public string _15Days { get; set; }

        [JsonProperty("180 Days")]
        public string _180Days { get; set; }

        [JsonProperty("30 Days")]
        public string _30Days { get; set; }

        [JsonProperty("60 Days")]
        public string _60Days { get; set; }

        [JsonProperty("90 Days")]
        public string _90Days { get; set; }
    }

}