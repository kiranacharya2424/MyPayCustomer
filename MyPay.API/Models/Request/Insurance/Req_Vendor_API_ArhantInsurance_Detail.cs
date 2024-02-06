using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_ArhantInsurance_Detail:CommonProp
    {
        //InsuranceSlug
        private string _InsuranceSlug = String.Empty;
        public string InsuranceSlug
        {
            get { return _InsuranceSlug; }
            set { _InsuranceSlug = value; }
        }

        //RequestId
        private string _RequestId = String.Empty;
        public string RequestId
        {
            get { return _RequestId; }
            set { _RequestId = value; }
        }

       
    }
}