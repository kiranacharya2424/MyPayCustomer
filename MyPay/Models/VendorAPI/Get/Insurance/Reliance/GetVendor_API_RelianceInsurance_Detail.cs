using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Reliance
{
    public class GetVendor_API_RelianceInsurance_Detail: CommonGet
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
        public string paymode { get; set; }
        public string transaction_id { get; set; }
        public string product_name { get; set; }
        public string customer_id { get; set; }
        public string next_due_date { get; set; }
        public string invoice_number { get; set; }
        public string amount { get; set; }
        public string customer_name { get; set; }
        public string address { get; set; }
        public string fine_amount { get; set; }
    }
}