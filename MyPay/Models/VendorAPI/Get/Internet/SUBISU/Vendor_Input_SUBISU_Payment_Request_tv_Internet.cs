using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Internet.SUBISU
{
    public class Vendor_Input_SUBISU_Payment_Request_tv_Internet
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // offer_name (i.e. plan_name)
        private string _offer_name = string.Empty;
        public string offer_name
        {
            get { return _offer_name; }
            set { _offer_name = value; }
        }

        //stb
        private string _stb = string.Empty;
        public string stb
        {
            get { return _stb; }
            set { _stb = value; }
        }
    }
}
