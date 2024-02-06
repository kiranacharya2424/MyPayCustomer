using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_CIT_Journey_Details_Requests : CommonProp
    {
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _AppCode = string.Empty;
        public string AppCode
        {
            get { return _AppCode; }
            set { _AppCode = value; }
        }

    }
}