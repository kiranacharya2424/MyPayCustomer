using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_School_Lookup_Requests : CommonProp
    {

        // idx
        private string _Idx = string.Empty;
        public string Idx
        {
            get { return _Idx; }
            set { _Idx = value; }
        }
    }
}