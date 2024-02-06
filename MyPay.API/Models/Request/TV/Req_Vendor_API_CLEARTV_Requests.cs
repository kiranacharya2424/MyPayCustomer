using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_CLEARTV_Requests : CommonProp
    {
        private string _Number = String.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _CustomerId = String.Empty;
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}