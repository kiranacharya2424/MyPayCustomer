using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Transaction_Lookup_API_Requests : CommonProp
    {
        //Req_Token
        private string _Req_Token = string.Empty;
        public string Req_Token
        {
            get { return _Req_Token; }
            set { _Req_Token = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //Req_ReferenceNo
        private string _Req_ReferenceNo = string.Empty;
        public string Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }
}