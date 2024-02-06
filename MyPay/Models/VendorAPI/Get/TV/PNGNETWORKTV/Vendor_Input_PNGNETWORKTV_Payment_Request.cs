using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_PNGNETWORKTV_Payment_Request
    {

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // package
        private string _package = string.Empty;
        public string package
        {
            get { return _package; }
            set { _package = value; }
        }
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // user_name
        private string _user_name = string.Empty;
        public string user_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // _contact_number
        private string _contact_number = string.Empty;
        public string contact_number
        {
            get { return _contact_number; }
            set { _contact_number = value; }
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