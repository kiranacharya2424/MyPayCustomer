﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Internet.SUBISU
{
    public class Vendor_Input_SUBISU_Outstanding_Payment_Request
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

        // offer_name (i.e. plan_name)
        private string _renew_type = string.Empty;
        public string renew_type
        {
            get { return _renew_type; }
            set { _renew_type = value; }
        }
    }
}