using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_ConfirmEmail:CommonProp
    {
      

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //Otp
        private string _Otp = string.Empty;
        public string Otp
        {
            get { return _Otp; }
            set { _Otp = value; }
        }
    }
}