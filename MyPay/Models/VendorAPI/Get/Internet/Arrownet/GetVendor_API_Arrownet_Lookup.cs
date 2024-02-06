using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Arrownet_Lookup : CommonGet
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
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
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
        // full_name
        private string _full_name = string.Empty;
        public string full_name
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        // days_remaining
        private string _days_remaining = string.Empty;
        public string days_remaining
        {
            get { return _days_remaining; }
            set { _days_remaining = value; }
        }
        // current_plan
        private string _current_plan = string.Empty;
        public string current_plan
        {
            get { return _current_plan; }
            set { _current_plan = value; }
        }
        // has_due
        private string _has_due = string.Empty;
        public string has_due
        {
            get { return _has_due; }
            set { _has_due = value; }
        }
        // arrownet_data
        private List<arrownet_bills> _plan_details = new List<arrownet_bills>();
        public List<arrownet_bills> plan_details
        {

            get { return _plan_details; }

            set { _plan_details = value; }
        }
    }
    public class arrownet_bills
    { 
        private string _duration = string.Empty;
        public string duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        } 
    }

}