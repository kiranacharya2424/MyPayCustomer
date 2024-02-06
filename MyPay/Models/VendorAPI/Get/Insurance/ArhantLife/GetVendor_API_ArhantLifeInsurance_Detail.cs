using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Insurance.ArhantLife
{
    public class GetVendor_API_ArhantLifeInsurance_Detail
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
        //public string policy_no { get; set; }
        //public string assured_name { get; set; }

        //public string periodic_premium { get; set; }
        //public string fine_amount { get; set; }
        //public string rebate_amount { get; set; }
        //public string amount { get; set; }
        //public string product_name { get; set; }
        //public string address { get; set; }
        //public string payment_date { get; set; }
        //public string pay_mode { get; set; }
        //public string due_date { get; set; }
        //public string policy_status { get; set; }
        //public string installment_no { get; set; }
        //public string current_due_date { get; set; }
        //public string token { get; set; }
        //public string unique_id_guid { get; set; }
        //public string session_id { get; set; }

        public string request_id { get; set; }
        public int amount { get; set; }
        public Properties properties { get; set; }
        public int session_id { get; set; }
  

        public class Properties
        {
            public string policy_no { get; set; }
            public string customer_name { get; set; }
            public string address { get; set; }
            public string product_name { get; set; }
            public string premium { get; set; }
            public string late_fee { get; set; }
            public string waive_amount { get; set; }
            public string previous_excess_short { get; set; }
        }



    }
}
