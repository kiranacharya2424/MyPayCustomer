﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Ride
{
    public class Vendor_Input_WorldLink_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        // package_id
        private string _package_id = string.Empty;
        public string package_id
        {
            get { return _package_id; }
            set { _package_id = value; }
        }
    }
}