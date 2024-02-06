using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.PlasmaAirlines
{
    public class Vendor_Input_Plasma_Lookup_Request
    {
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
