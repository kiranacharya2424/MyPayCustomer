using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVerification:CommonGet
    {
        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //VerificationType
        private int _VerificationType = 0;
        public int VerificationType
        {
            get { return _VerificationType; }
            set { _VerificationType = value; }
        }

        //PhoneExtension
        private string _PhoneExtension = string.Empty;
        public string PhoneExtension
        {
            get { return _PhoneExtension; }
            set { _PhoneExtension = value; }
        }

        //Otp
        private string _Otp = string.Empty;
        public string Otp
        {
            get { return _Otp; }
            set { _Otp = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //PhoneNumber
        private string _PhoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        //CheckVerified
        private int? _CheckVerified = 2;
        public int? CheckVerified
        {
            get { return _CheckVerified; }
            set { _CheckVerified = value; }
        }
    }
}