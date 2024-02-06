using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Dishome_Lookup_Requests : CommonProp
    {
        private string _casid = String.Empty;
        public string CasId
        {
            get { return _casid; }
            set { _casid = value; }
        }
    }
}