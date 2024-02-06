using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Classitech_Lookup : CommonGet
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
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // token
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
        // username
        private string _username = string.Empty;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        } 
        // classitech_data
        private List<classitech_bills> _available_plans = new List<classitech_bills>();
        public List<classitech_bills> available_plans
        {

            get { return _available_plans; }

            set { _available_plans = value; }
        }
    }
    public class classitech_bills
    {
        // name
        private string _package = string.Empty;
        public string  package
        {
            get { return _package; }
            set { _package = value; }
        }
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