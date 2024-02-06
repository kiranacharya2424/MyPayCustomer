using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_RecipientDetail:CommonResponse
    {
        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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

        //IsDetailUpdated
        private bool _IsDetailUpdated = false;
        public bool IsDetailUpdated
        {
            get { return _IsDetailUpdated; }
            set { _IsDetailUpdated = value; }
        }
    }
}