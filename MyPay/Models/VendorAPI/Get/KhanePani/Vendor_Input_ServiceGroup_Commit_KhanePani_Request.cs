using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_ServiceGroup_Commit_KhanePani_Request
    {

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // customer_code
        private string _customer_code = string.Empty;
        public string customer_code
        {
            get { return _customer_code; }
            set { _customer_code = value; }
        }
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
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