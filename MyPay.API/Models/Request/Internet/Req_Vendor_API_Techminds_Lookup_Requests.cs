using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Techminds_Lookup_Requests : CommonProp
    {
        private string _RequestID = String.Empty;
        public string RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}