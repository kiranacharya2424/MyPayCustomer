﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_Commit_Insurance_NepalLife_Requests:CommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        // CustomerCode
        private string _CustomerCode = string.Empty;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //SessionId
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
    }
}