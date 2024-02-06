using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_ViaNet_Lookup_Requests : CommonProp
    {
        private string _CustomerID = String.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}