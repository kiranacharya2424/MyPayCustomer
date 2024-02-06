﻿using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Neco
{
    public class GetVendor_API_NecoInsurance_Lookup:CommonGet
    {
        // error_code
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        // details
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }
        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // _customer_id
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        // _policy_categories
        private List<string> _policy_categories;
        public List<string> policy_categories
        {
            get { return _policy_categories; }
            set { _policy_categories = value; }
        }
        private List<string> _branches;
        public List<string> branches
        {
            get { return _branches; }
            set { _branches = value; }
        }
    }
    
}