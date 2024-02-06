using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Ride
{
    public class Req_Vendor_API_Ride_Tootle_Lookup:CommonProp
    {
        private string _App = String.Empty;
        public string App
        {
            get { return _App; }
            set { _App = value; }
        }

        private string _PhoneNumber = String.Empty;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
    }
}