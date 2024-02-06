using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Internet
{
    public class Req_Vendor_API_SUBISU_Lookup_Requests : CommonProp
    {
        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}
