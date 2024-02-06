using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_ScratchedCoupons : WebCommonProp
    {
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //ServiceId
        private string _ServiceId = string.Empty;
        public string ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

    }
}