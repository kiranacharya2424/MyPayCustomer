﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Antivirus
{
    public class Req_Vendor_API_K7_Lookup_Requests: CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}