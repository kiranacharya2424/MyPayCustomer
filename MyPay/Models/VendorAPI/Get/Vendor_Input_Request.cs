using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }
        private string _remarks = string.Empty;
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        } 
    }
}