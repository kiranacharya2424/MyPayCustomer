using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMerchant : CommonAdd
    {
        #region Enum
        public enum MerChantType
        {
            Merchant = 0,
            Bank = 1,
            Remittance = 3,
        }
        #endregion
        #region "Properties"

        //MerchantUniqueId
        private string _MerchantUniqueId = string.Empty;
        public string MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }

        //OrganizationName
        private string _OrganizationName = string.Empty;
        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }

        //FirstName
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        //LastName
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        //EmailID
        private string _EmailID = string.Empty;
        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }

        //ContactNo
        private string _ContactNo = string.Empty;
        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }

        //UserName
        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        //IsPasswordReset
        private bool _IsPasswordReset = false;
        public bool IsPasswordReset
        {
            get { return _IsPasswordReset; }
            set { _IsPasswordReset = value; }
        }

        //apikey
        private string _apikey = string.Empty;
        public string apikey
        {
            get { return _apikey; }
            set { _apikey = value; }
        }

        //secretkey
        private string _secretkey = string.Empty;
        public string secretkey
        {
            get { return _secretkey; }
            set { _secretkey = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //Address
        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        //City
        private string _City = string.Empty;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        //State
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        //UserMemberId
        private Int64 _UserMemberId = 0;
        public Int64 UserMemberId
        {
            get { return _UserMemberId; }
            set { _UserMemberId = value; }
        }
        //CountryId
        private Int64 _CountryId = 0;
        public Int64 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        //CountryName
        private string _CountryName = string.Empty;
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        //ZipCode
        private string _ZipCode = string.Empty;
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }
        private string _TotalUserCount = string.Empty;
        public string TotalUserCount
        {
            get { return _TotalUserCount; }
            set { _TotalUserCount = value; }
        }

        private string _SuccessURL = string.Empty;
        public string SuccessURL
        {
            get { return _SuccessURL; }
            set { _SuccessURL = value; }
        }

        private string _CancelURL = string.Empty;
        public string CancelURL
        {
            get { return _CancelURL; }
            set { _CancelURL = value; }
        }

        private string _WebsiteURL = string.Empty;
        public string WebsiteURL
        {
            get { return _WebsiteURL; }
            set { _WebsiteURL = value; }
        }

        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        private string _RoleName = string.Empty;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        private Int64 _MerchantMemberId = 0;
        public Int64 MerchantMemberId
        {
            get { return _MerchantMemberId; }
            set { _MerchantMemberId = value; }
        }

        private decimal _MerchantTotalAmount = 0;
        public decimal MerchantTotalAmount
        {
            get { return _MerchantTotalAmount; }
            set { _MerchantTotalAmount = value; }
        }

        private string _MerchantIpAddress = string.Empty;
        public string MerchantIpAddress
        {
            get { return _MerchantIpAddress; }
            set { _MerchantIpAddress = value; }
        }
        private string _PublicKey = string.Empty;
        public string PublicKey
        {
            get { return _PublicKey; }
            set { _PublicKey = value; }
        }
        private string _PrivateKey = string.Empty;
        public string PrivateKey
        {
            get { return _PrivateKey; }
            set { _PrivateKey = value; }
        }

        private string _API_User = string.Empty;
        public string API_User
        {
            get { return _API_User; }
            set { _API_User = value; }
        }
        private string _API_Password = string.Empty;
        public string API_Password
        {
            get { return _API_Password; }
            set { _API_Password = value; }
        }

        private MerChantType _MerChantTypeEnum = 0;
        public MerChantType MerChantTypeEnum
        {
            get { return _MerChantTypeEnum; }
            set { _MerChantTypeEnum = value; }
        }
        private Int32 _MerchantType = 0;
        public Int32 MerchantType
        {
            get { return _MerchantType; }
            set { _MerchantType = value; }
        }
        private string _MerchantTypeName = string.Empty;
        public string MerchantTypeName
        {
            get { return _MerchantTypeName; }
            set { _MerchantTypeName = value; }
        }
        #endregion
    }
}