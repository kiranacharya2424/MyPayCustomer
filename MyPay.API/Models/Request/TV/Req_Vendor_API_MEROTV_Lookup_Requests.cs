using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_MEROTV_Lookup_Requests : CommonProp
    {
        //private string _STB = String.Empty;
        //public string STB
        //{
        //    get { return _STB; }
        //    set { _STB = value; }
        //}
        //private string _Reference = String.Empty;
        //public string Reference
        //{
        //    get { return _Reference; }
        //    set { _Reference = value; }
        //}
        public string Reference;
        public string customer_id;
        public string service_slug;
    }
}