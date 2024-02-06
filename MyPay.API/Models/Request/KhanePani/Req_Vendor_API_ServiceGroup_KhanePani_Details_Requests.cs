using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_ServiceGroup_KhanePani_Details_Requests : CommonProp
    {
        // Month_Id
        private string _Month_Id = string.Empty;
        public string Month_Id
        {
            get { return _Month_Id; }
            set { _Month_Id = value; }
        }
        // Consumer_Code
        private string _Consumer_Code = string.Empty;
        public string Consumer_Code
        {
            get { return _Consumer_Code; }
            set { _Consumer_Code = value; }
        }
        // Counter
        private string _Counter = string.Empty;
        public string Counter
        {
            get { return _Counter; }
            set { _Counter = value; }
        }
    }
}