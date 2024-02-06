using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_SagarmathaInsurance_Lookup:CommonProp
    {
        private string _DebitNote = String.Empty;
        public string DebitNote
        {
            get { return _DebitNote; }
            set { _DebitNote = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}