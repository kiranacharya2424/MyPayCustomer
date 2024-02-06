using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_RelianceInsurance_Detail: CommonProp
    {
        private string _PolicyNo = String.Empty;
        public string PolicyNo
        {
            get { return _PolicyNo; }
            set { _PolicyNo = value; }
        }
        private string _dob = String.Empty;
        public string dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
    }
}