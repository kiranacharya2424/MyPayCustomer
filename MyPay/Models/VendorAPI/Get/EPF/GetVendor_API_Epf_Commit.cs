using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Epf_Commit : CommonGet
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
        // validity
        private string _validity = string.Empty;
        public string validity
        {
            get { return _validity; }
            set { _validity = value; }
        }

        // creditor_name
        private string _creditor_name = string.Empty;
        public string creditor_name
        {
            get { return _creditor_name; }
            set { _creditor_name = value; }
        }
        // debitor_name
        private string _debitor_name = string.Empty;
        public string debitor_name
        {
            get { return _debitor_name; }
            set { _debitor_name = value; }
        }

        // app_id
        private string _app_id = string.Empty;
        public string app_id
        {
            get { return _app_id; }
            set { _app_id = value; }
        }
        // voucher_no
        private string _voucher_no = string.Empty;
        public string voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }

        // remarks
        private string _remarks = string.Empty;
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        // particulars
        private string _particulars = string.Empty;
        public string particulars
        {
            get { return _particulars; }
            set { _particulars = value; }
        }

        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // organization
        private string _organization = string.Empty;
        public string organization
        {
            get { return _organization; }
            set { _organization = value; }
        }

        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // description
        private string _description = string.Empty;
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        // full_name
        private string _full_name = string.Empty;
        public string full_name
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        // vehicle_category
        private string _vehicle_category = string.Empty;
        public string vehicle_category
        {
            get { return _vehicle_category; }
            set { _vehicle_category = value; }
        }
        // tracing_id
        private string _tracing_id = string.Empty;
        public string tracing_id
        {
            get { return _tracing_id; }
            set { _tracing_id = value; }
        }
        // bank_code
        private string _bank_code = string.Empty;
        public string bank_code
        {
            get { return _bank_code; }
            set { _bank_code = value; }
        }
        // chit_number
        private string _chit_number = string.Empty;
        public string chit_number
        {
            get { return _chit_number; }
            set { _chit_number = value; }
        }
        // ebp_number
        private string _ebp_number = string.Empty;
        public string ebp_number
        {
            get { return _ebp_number; }
            set { _ebp_number = value; }
        }
    }

}