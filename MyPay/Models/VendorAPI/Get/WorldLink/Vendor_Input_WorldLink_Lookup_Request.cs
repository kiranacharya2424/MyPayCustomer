using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Ride
{
    public class Vendor_Input_WorldLink_Lookup_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // _reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        // number
        private string _username = string.Empty;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}