using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral
{
    public class Vendor_Input_IMEGeneralInsurance_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        //policy_type
        private string _policy_type = string.Empty;
        public string policy_type
        {
            get { return _policy_type; }
            set { _policy_type = value; }
        }
        //insurance_type
        private string _insurance_type = string.Empty;
        public string insurance_type
        {
            get { return _insurance_type; }
            set { _insurance_type = value; }
        }
        //branch
        private string _branch = string.Empty;
        public string branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
        //full_name
        private string _full_name = string.Empty;
        public string full_name
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        //address
        private string _address = string.Empty;
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        //mobile_number
        private string _mobile_number = string.Empty;
        public string mobile_number
        {
            get { return _mobile_number; }
            set { _mobile_number = value; }
        }
        //policy_description
        private string _policy_description = string.Empty;
        public string policy_description
        {
            get { return _policy_description; }
            set { _policy_description = value; }
        }
        //debit_note_no
        private string _debit_note_no = string.Empty;
        public string debit_note_no
        {
            get { return _debit_note_no; }
            set { _debit_note_no = value; }
        }
        //bill_no
        private string _bill_no = string.Empty;
        public string bill_no
        {
            get { return _bill_no; }
            set { _bill_no = value; }
        }
        //email
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
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

        

        
    }
}
