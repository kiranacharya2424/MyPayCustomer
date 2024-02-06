using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Ride
{
    public class Vendor_Input_Ride_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // product_identity
        private string _product_identity = string.Empty;
        public string product_identity
        {
            get { return _product_identity; }
            set { _product_identity = value; }
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
        // number
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }
    }
}