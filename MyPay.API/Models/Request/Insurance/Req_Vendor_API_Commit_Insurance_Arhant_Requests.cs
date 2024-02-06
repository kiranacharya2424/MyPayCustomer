using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_Commit_Insurance_Arhant_Requests:CommonProp
    {
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

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
        private string _ClassName = String.Empty;
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
    }
}