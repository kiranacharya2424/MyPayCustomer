using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Pokhara_Requests : CommonProp
    {
        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Number = String.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _Address = String.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
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