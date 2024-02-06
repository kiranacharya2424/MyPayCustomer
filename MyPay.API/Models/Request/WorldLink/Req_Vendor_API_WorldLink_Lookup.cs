using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Ride
{
    public class Req_Vendor_API_WorldLink_Lookup : CommonProp
    {
        private string _reference = String.Empty;
        public string Reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        private string _username = String.Empty;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}