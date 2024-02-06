using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_NecoInsurance_Lookup:CommonProp
    {
        private string _insurance_slug = String.Empty;
        public string insurance_slug
        {
            get { return _insurance_slug; }
            set { _insurance_slug = value; }
        }
    }
}