using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Insurance.Citizen
{
    public class Vendor_Input_CitizenLifeInsurance_Detail_Request
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

        // insurance_slug
        private string _insurance_slug = string.Empty;
        public string insurance_slug
        {
            get { return _insurance_slug; }
            set { _insurance_slug = value; }
        }

        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
    }
}
