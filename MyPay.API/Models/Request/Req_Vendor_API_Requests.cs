using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Requests : CommonProp
    {
        //Req_Token
        private string _Req_Token = string.Empty;
        public string Req_Token
        {
            get { return _Req_Token; }
            set { _Req_Token = value; }
        }

        //Req_ReferenceNo
        private string _Req_ReferenceNo = string.Empty;
        public string Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }   
        //Number
        private string _Number = string.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
    }
}