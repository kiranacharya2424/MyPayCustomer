using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Reliance
{
    public class Vendor_Input_RelianceInsurance_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        //amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        //policy_no
        private string _policy_no = string.Empty;
        public string policy_no
        {
            get { return _policy_no; }
            set { _policy_no = value; }
        }

        //transaction_id
        private string _transaction_id = string.Empty;
        public string transaction_id
        {
            get { return _transaction_id; }
            set { _transaction_id = value; }
        }

        //reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }        
    }
}