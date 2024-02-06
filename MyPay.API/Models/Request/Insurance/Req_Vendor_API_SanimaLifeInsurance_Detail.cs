using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_SanimaLifeInsurance_Detail:CommonProp
    {
        private string _policy_number = String.Empty;
        public string policy_number
        {
            get { return _policy_number; }
            set { _policy_number = value; }
        }

        private string _date_of_birth = String.Empty;
        public string date_of_birth
        {
            get { return _date_of_birth; }
            set { _date_of_birth = value; }
        }

        private string _reference = String.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
    }
}