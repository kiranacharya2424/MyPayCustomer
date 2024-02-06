using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_PRABHUTV_Lookup : CommonGet
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
        // balance
        private string _balance = string.Empty;
        public string balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
        // stb_count
        private string _stb_count = string.Empty;
        public string stb_count
        {
            get { return _stb_count; }
            set { _stb_count = value; }
        }
        // customer_id
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // PRABHUTV_Lookup_Data
        private List<PRABHUTV_Lookup_Data> _current_packages = new List<PRABHUTV_Lookup_Data>();
        public List<PRABHUTV_Lookup_Data> current_packages
        {

            get { return _current_packages; }

            set { _current_packages = value; }
        }
    }
    public class PRABHUTV_Lookup_Data
    {
        // name
        private string _product_name = string.Empty;
        public string product_name
        {
            get { return _product_name; }
            set { _product_name = value; }
        }
        private string _service_start_date = string.Empty;
        public string service_start_date
        {
            get { return _service_start_date; }
            set { _service_start_date = value; }
        }
        private string _expiry_date = string.Empty;
        public string expiry_date
        {
            get { return _expiry_date; }
            set { _expiry_date = value; }
        }
        private string _serial_number = string.Empty;
        public string serial_number
        {
            get { return _serial_number; }
            set { _serial_number = value; }
        }
        private string _mac_vc_number = string.Empty;
        public string mac_vc_number
        {
            get { return _mac_vc_number; }
            set { _mac_vc_number = value; }
        }
        private string _bill_amount = string.Empty;
        public string bill_amount
        {
            get { return _bill_amount; }
            set { _bill_amount = value; }
        }

    }
}