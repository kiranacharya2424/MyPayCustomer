using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Antivirus
{
    public class Req_Vendor_API_Mcafee_Requests : CommonProp
    {
        private string _Value = String.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
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

        private string _Name = String.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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