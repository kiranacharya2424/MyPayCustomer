using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Sanima
{
    public class Vendor_Input_SanimaLifeInsurance_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        //reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        //session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
    }
}