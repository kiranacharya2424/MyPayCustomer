using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_UserDetail:CommonResponse
    {
        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Value
        private string _Value = string.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //EmailId
        private string _EmailId = string.Empty;
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //PhoneExt
        private string _PhoneExt = string.Empty;
        public string PhoneExt
        {
            get { return _PhoneExt; }
            set { _PhoneExt = value; }
        }

        //Gender
        private int _Gender = 0;
        public int Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        //IsPhoneVerified
        private bool _IsPhoneVerified = false;
        public bool IsPhoneVerified
        {
            get { return _IsPhoneVerified; }
            set { _IsPhoneVerified = value; }
        }

        //IsLoginWithPassword
        private bool _IsLoginWithPassword = false;
        public bool IsLoginWithPassword
        {
            get { return _IsLoginWithPassword; }
            set { _IsLoginWithPassword = value; }
        }

        //IsEmailVerified
        private bool _IsEmailVerified = false;
        public bool IsEmailVerified
        {
            get { return _IsEmailVerified; }
            set { _IsEmailVerified = value; }
        }

        //IsKycVerified
        private int _IsKycVerified = 0;
        public int IsKycVerified
        {
            get { return _IsKycVerified; }
            set { _IsKycVerified = value; }
        }

        //IsPinCreated
        private bool _IsPinCreated = false;
        public bool IsPinCreated
        {
            get { return _IsPinCreated; }
            set { _IsPinCreated = value; }
        }

        //IsPasswordCreated
        private bool _IsPasswordCreated = false;
        public bool IsPasswordCreated
        {
            get { return _IsPasswordCreated; }
            set { _IsPasswordCreated = value; }
        }

        //RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        //Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        //IsResetPasswordFromAdmin
        private bool _IsResetPasswordFromAdmin = false;
        public bool IsResetPasswordFromAdmin
        {
            get { return _IsResetPasswordFromAdmin; }
            set { _IsResetPasswordFromAdmin = value; }
        }


        private bool _IsDarkTheme = false;
        public bool IsDarkTheme
        {
            get { return _IsDarkTheme; }
            set { _IsDarkTheme = value; }
        }
        //VerificationOtp
        private string _VerificationOtp = string.Empty;
        public string VerificationOtp
        {
            get { return _VerificationOtp; }
            set { _VerificationOtp = value; }
        }

        //TotalAmount
        private string _TotalAmount = string.Empty;
        public string TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        //IsDetailUpdated
        private bool _IsDetailUpdated = false;
        public bool IsDetailUpdated
        {
            get { return _IsDetailUpdated; }
            set { _IsDetailUpdated = value; }
        }
    }
}