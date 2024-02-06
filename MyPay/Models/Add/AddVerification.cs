using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddVerification:CommonAdd
    {
        #region "Enum"
        public enum VerifyType
        {
            Register = 1,
            Verification = 2,
            ForgotPin = 3,
            EmailVerification = 4 
        }

        public enum PlatformType
        {
            Email = 1,
            Phone = 2
        }
        #endregion

        #region "Properties"
        


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

        //PhoneExtension
        private string _PhoneExtension = string.Empty;
        public string PhoneExtension
        {
            get { return _PhoneExtension; }
            set { _PhoneExtension = value; }
        }

        //IsVerified
        private bool _IsVerified = false;
        public bool IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }

        #endregion
    }
}