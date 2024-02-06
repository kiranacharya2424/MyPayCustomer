using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_RegisterVerification:CommonResponse
    {
        

        //VerificationOtp
        private string _VerificationOtp = string.Empty;
        public string VerificationOtp
        {
            get { return _VerificationOtp; }
            set { _VerificationOtp = value; }
        }


        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
    }
}