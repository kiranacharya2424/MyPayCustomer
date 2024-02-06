using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_User:CommonResponse
    {
        //UserId
        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        //VerificationOtp
        private string _VerificationOtp = string.Empty;
        public string VerificationOtp
        {
            get { return _VerificationOtp; }
            set { _VerificationOtp = value; }
        }

        //Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        //RewardPoint
        private string _RewardPoint = string.Empty;
        public string RewardPoint
        {
            get { return _RewardPoint; }
            set { _RewardPoint = value; }
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