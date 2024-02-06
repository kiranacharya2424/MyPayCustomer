using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.KhanePani
{
    public class Vendor_Output_KhanePani_Response
    {

        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        } 
        private detail _detail = new detail();
        public detail detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        private string _credits_consumed = string.Empty;
        public string credits_consumed
        {
            get { return _credits_consumed; }
            set { _credits_consumed = value; }
        }
        private string _credits_available = string.Empty;
        public string credits_available
        {
            get { return _credits_available; }
            set { _credits_available = value; }
        }
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        private extra_data _extra_data = new extra_data();
        public extra_data extra_data
        {
            get { return _extra_data; }
            set { _extra_data = value; }
        }
    }
    public class extra_data
    {

        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}