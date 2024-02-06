using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Arhant
{
    public class Vendor_Input_ArhantInsurance_Payment_Request
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

        //request_id
        private string _request_id = string.Empty;
        public string request_id
        {
            get { return _request_id; }
            set { _request_id = value; }
        }        

        //insurance_slug
        private string _insurance_slug = string.Empty;
        public string insurance_slug
        {
            get { return _insurance_slug; }
            set { _insurance_slug = value; }
        }

        //amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}