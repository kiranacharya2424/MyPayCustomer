﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_EChalan_Lookup_DistrictCodes_Requests : CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        } 
    }
}