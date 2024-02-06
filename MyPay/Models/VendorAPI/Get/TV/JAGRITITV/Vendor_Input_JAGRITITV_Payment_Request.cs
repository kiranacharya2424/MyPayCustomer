using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_JAGRITITV_Payment_Request
    {

        // token
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
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        private string _package = string.Empty;
        public string package
        {
            get { return _package; }
            set { _package = value; }
        }
        private string _stb_or_cas_id = string.Empty;
        public string stb_or_cas_id
        {
            get { return _stb_or_cas_id; }
            set { _stb_or_cas_id = value; }
        }
        private string _old_ward_number = string.Empty;
        public string old_ward_number
        {
            get { return _old_ward_number; }
            set { _old_ward_number = value; }
        }
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
        private string _mobile_number_1 = String.Empty;
        public string mobile_number_1
        {
            get { return _mobile_number_1; }
            set { _mobile_number_1 = value; }
        }
    }
}