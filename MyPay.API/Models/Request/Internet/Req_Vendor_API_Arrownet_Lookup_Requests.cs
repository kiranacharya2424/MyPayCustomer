using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Arrownet_Lookup_Requests : CommonProp
    {
        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}