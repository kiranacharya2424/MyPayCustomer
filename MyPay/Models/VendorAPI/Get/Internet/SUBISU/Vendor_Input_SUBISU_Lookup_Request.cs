using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Internet.SUBISU
{
    public class Vendor_Input_SUBISU_Lookup_Request
    {
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        private string _username = string.Empty;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}
