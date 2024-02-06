using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_EventDetail : WebCommonProp
    {
        
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private int _EventId = 0;
        public int EventId
        {
            get { return _EventId; }
            set { _EventId = value; }
        }

        private string _EventDate = string.Empty;
        public string EventDate
        {
            get { return _EventDate; }
            set { _EventDate = value; }
        }
        private string _MerchantCode = string.Empty;
        public string MerchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }
    }
}