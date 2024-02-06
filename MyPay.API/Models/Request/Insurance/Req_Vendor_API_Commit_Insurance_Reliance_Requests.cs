using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_Commit_Insurance_Reliance_Requests: CommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        // CustomerCode
        private string _CustomerCode = string.Empty;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //PolicyNo
        private string _PolicyNo = string.Empty;
        public string PolicyNo
        {
            get { return _PolicyNo; }
            set { _PolicyNo = value; }
        }

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        
    }
}