﻿using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Antivirus.K7
{
    public class GetVendor_API_K7_Lookup : CommonGet
    {
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
        // Kaspersky_data
        private List<K7_bills> _data = new List<K7_bills>();
        public List<K7_bills> data
        {

            get { return _data; }

            set { _data = value; }
        }
    }
    public class K7_bills
    {
        // name
        private string _idx = string.Empty;
        public string idx
        {
            get { return _idx; }
            set { _idx = value; }
        }
        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _value = string.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}