using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_SetPin:CommonProp
    {
        //Pin
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Otp
        private string _Otp = string.Empty;
        public string Otp
        {
            get { return _Otp; }
            set { _Otp = value; }
        }

        //IsForget
        private string _IsForget = string.Empty;
        public string IsForget
        {
            get { return _IsForget; }
            set { _IsForget = value; }
        }
    }
}