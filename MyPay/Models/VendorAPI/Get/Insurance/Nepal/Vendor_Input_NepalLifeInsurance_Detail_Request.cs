﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Nepal
{
    public class Vendor_Input_NepalLifeInsurance_Detail_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        // policy_no
        private string _policy_no = string.Empty;
        public string policy_no
        {
            get { return _policy_no; }
            set { _policy_no = value; }
        }

        // dob
        private string _dob = string.Empty;
        public string dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        
    }
}