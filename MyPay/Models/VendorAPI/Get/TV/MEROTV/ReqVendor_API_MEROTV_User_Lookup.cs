using MyPay.Models.Common;
using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class ReqVendor_API_MEROTV_User_Lookup : WebCommonProp
    {
        public Int64 MemberId { get; set; }
        public string Reference { get; set; }
        public Int64 Amount { get; set; }
        public string session_id { get; set; }
        public string customer_id { get; set; }
        public string stb { get; set; }
    }

}