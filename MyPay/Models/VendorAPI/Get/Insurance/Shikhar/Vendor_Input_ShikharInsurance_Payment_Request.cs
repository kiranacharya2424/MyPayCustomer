using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Shikhar
{
    public class Vendor_Input_ShikharInsurance_Payment_Request
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

        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        // address
        private string _address = string.Empty;
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }

        // contact_number
        private string _contact_number = string.Empty;
        public string contact_number
        {
            get { return _contact_number; }
            set { _contact_number = value; }
        }

        // email
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        // policy_type
        private string _policy_type = string.Empty;
        public string policy_type
        {
            get { return _policy_type; }
            set { _policy_type = value; }
        }

        // policy_number
        private string _policy_number = string.Empty;
        public string policy_number
        {
            get { return _policy_number; }
            set { _policy_number = value; }
        }

        // branch
        private string _branch = string.Empty;
        public string branch
        {
            get { return _branch; }
            set { _branch = value; }
        }

        // policy_description
        private string _policy_description = string.Empty;
        public string policy_description
        {
            get { return _policy_description; }
            set { _policy_description = value; }
        }

        // policy_name
        private string _policy_name = string.Empty;
        public string policy_name
        {
            get { return _policy_name; }
            set { _policy_name = value; }
        }

        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}