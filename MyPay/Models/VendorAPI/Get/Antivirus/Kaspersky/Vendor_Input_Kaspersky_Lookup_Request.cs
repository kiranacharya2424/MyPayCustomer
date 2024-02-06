using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Kaspersky_Lookup_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        } 
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
    }
}