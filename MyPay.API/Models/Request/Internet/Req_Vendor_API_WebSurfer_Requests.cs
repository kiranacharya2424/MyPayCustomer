﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_WebSurfer_Requests : CommonProp
    {
        private string _Service = String.Empty;
        public string Service
        {
            get { return _Service; }
            set { _Service = value; }
        }
        private string _SessionID = String.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        private string _package_id = String.Empty;
        public string package_id
        {
            get { return _package_id; }
            set { _package_id = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}