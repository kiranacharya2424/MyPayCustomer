using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Pokhara_Payment_Request
    {

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // username
        private string _username = string.Empty;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
        // number
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // address
        private string _address = string.Empty;
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        } 
    }
}