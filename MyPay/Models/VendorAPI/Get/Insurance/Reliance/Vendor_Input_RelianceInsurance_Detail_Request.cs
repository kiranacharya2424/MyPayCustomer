using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Reliance
{
    public class Vendor_Input_RelianceInsurance_Detail_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // policy_no
        private string _policy_no = string.Empty;
        public string policy_no
        {
            get { return _policy_no; }
            set { _policy_no = value; }
        }
        // dob
        private string _dob = string.Empty;
        public string dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
    }
}