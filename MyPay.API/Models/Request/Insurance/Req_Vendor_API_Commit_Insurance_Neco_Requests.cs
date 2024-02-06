using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_Commit_Insurance_Neco_Requests:CommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        // CustomerCode
        private string _CustomerCode = string.Empty;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        // policy_type <Fresh OR Renew>
        private string _policy_type = string.Empty;
        public string policy_type
        {
            get { return _policy_type; }
            set { _policy_type = value; }
        }

        //customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        //policy_category <Any one value from categories list>
        private string _policy_category = string.Empty;
        public string policy_category
        {
            get { return _policy_category; }
            set { _policy_category = value; }
        }

        //amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        //reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        //policy_number <Optional Field | 12 >
        private string _policy_number = string.Empty;
        public string policy_number
        {
            get { return _policy_number; }
            set { _policy_number = value; }
        }

        //mobile_number
        private string _mobile_number = string.Empty;
        public string mobile_number
        {
            get { return _mobile_number; }
            set { _mobile_number = value; }
        }

        //service_name
        private string _service_name = string.Empty;
        public string service_name
        {
            get { return _service_name; }
            set { _service_name = value; }
        }

        //insurance_slug
        private string _insurance_slug = string.Empty;
        public string insurance_slug
        {
            get { return _insurance_slug; }
            set { _insurance_slug = value; }
        }
    }
}