using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_JAGRITITV_Lookup_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _package_type = string.Empty;
        public string package_type
        {
            get { return _package_type; }
            set { _package_type = value; }
        }
    }
}