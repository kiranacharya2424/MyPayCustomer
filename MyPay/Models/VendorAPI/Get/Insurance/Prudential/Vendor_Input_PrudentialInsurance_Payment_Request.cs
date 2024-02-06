using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Insurance.Prudential
{
    public class Vendor_Input_PrudentialInsurance_Payment_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        
        //branch
        private string _issue_branch = string.Empty;
        public string issue_branch
        {
            get { return _issue_branch; }
            set { _issue_branch = value; }
        }
        //full_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        
        //mobile_number
        private string _mobile_number = string.Empty;
        public string mobile_number
        {
            get { return _mobile_number; }
            set { _mobile_number = value; }
        }
        //email
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        //debit_note_no
        private string _debit_note_or_bill_number = string.Empty;
        public string debit_note_or_bill_number
        {
            get { return _debit_note_or_bill_number; }
            set { _debit_note_or_bill_number = value; }
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
