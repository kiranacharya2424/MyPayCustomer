using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_ServiceGroup_Details_Nea_Request
    {

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // sc_no
        private string _sc_no = string.Empty;
        public string sc_no
        {
            get { return _sc_no; }
            set { _sc_no = value; }
        }
        // consumer_id
        private string _consumer_id = string.Empty;
        public string consumer_id
        {
            get { return _consumer_id; }
            set { _consumer_id = value; }
        }
        // referenceno
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        // officecode
        private string _office_code = string.Empty;
        public string office_code
        {
            get { return _office_code; }
            set { _office_code = value; }
        }
    }
}