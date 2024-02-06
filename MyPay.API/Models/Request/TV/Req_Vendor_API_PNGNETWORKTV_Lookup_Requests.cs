using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_PNGNETWORKTV_Lookup_Requests : CommonProp
    {
        private string _PackageType = String.Empty;
        public string PackageType  
        {
            get { return _PackageType; }
            set { _PackageType = value; }
        }
    }
}