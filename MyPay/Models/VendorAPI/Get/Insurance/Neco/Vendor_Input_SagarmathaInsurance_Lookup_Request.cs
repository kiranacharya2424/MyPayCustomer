using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Sagarmatha
{
    public class Vendor_Input_SagarmathaInsurance_Lookup_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // debit_note_no
        private string _debit_note_no = string.Empty;
        public string debit_note_no
        {
            get { return _debit_note_no; }
            set { _debit_note_no = value; }
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