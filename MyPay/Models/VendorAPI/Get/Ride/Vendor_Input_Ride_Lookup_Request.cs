using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Ride
{
    public class Vendor_Input_Ride_Lookup_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // app
        private string _app = string.Empty;
        public string app
        {
            get { return _app; }
            set { _app = value; }
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