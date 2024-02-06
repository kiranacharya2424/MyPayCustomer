using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_CLEARTV_Lookup : CommonGet
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
        // CLEARTV_Lookup_Data
        private CLEARTV_Lookup_Data _data = new CLEARTV_Lookup_Data();
        public CLEARTV_Lookup_Data data
        {

            get { return _data; }

            set { _data = value; }
        }
    }
    public class CLEARTV_Lookup_Data
    {
        // name
        private string _due_amount = string.Empty;
        public string due_amount
        {
            get { return _due_amount; }
            set { _due_amount = value; }
        }
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

    }
}