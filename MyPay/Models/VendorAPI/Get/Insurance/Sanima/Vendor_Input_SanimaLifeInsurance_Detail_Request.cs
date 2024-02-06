using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Sanima
{
    public class Vendor_Input_SanimaLifeInsurance_Detail_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        // policy_number
        private string _policy_number = string.Empty;
        public string policy_number
        {
            get { return _policy_number; }
            set { _policy_number = value; }
        }

        // date_of_birth
        private string _date_of_birth = string.Empty;
        public string date_of_birth
        {
            get { return _date_of_birth; }
            set { _date_of_birth = value; }
        }
    }
}