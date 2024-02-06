using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Ride
{
    public class GetVendor_API_Ride_Lookup:CommonGet
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
        // product_identity
        private string _product_identity = string.Empty;
        public string product_identity
        {
            get { return _product_identity; }
            set { _product_identity = value; }
        }
        // name
        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        // number
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }

        // _gender
        private string _gender = string.Empty;
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

    }

}