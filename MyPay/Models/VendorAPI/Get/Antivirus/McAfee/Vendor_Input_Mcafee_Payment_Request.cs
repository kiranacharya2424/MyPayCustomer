using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Antivirus.Mcafee
{
    public class Vendor_Input_Mcafee_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // value
        private string _value = string.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
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
    }
}