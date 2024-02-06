using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_PRABHUTV_Lookup_Requests : CommonProp
    {
        private string _CAS_ID = String.Empty;
        public string CAS_ID
        {
            get { return _CAS_ID; }
            set { _CAS_ID = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}