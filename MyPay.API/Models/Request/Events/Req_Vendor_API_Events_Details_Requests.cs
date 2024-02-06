using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Events_Details_Requests : CommonProp
    {


        // session_id
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        // amount
       
        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
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