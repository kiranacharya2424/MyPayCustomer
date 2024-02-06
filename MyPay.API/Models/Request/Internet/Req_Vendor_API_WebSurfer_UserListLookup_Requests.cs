using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_WebSurfer_UserListLookup_Requests : CommonProp
    {
        private string _CustomerId = String.Empty;
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}