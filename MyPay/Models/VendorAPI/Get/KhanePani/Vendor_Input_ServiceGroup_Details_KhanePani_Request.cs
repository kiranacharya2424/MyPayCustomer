using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_ServiceGroup_Details_KhanePani_Request
    {

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // month_id
        private string _month_id = string.Empty;
        public string month_id
        {
            get { return _month_id; }
            set { _month_id = value; }
        }
        // customer_code
        private string _customer_code = string.Empty;
        public string customer_code
        {
            get { return _customer_code; }
            set { _customer_code = value; }
        }
        // counter
        private string _counter = string.Empty;
        public string counter
        {
            get { return _counter; }
            set { _counter = value; }
        }
        
    }
}