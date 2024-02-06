using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_CIT_LoanType_Detail_Requests : CommonProp
    {
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _appCode = string.Empty;
        public string appCode
        {
            get { return _appCode; }
            set { _appCode = value; }
        }

    }
}