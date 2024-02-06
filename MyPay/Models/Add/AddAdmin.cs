using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddAdmin:CommonAdd
    {
        #region "Properties"

        //FirstName
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        //VerificationCode
        private string _VerificationCode = string.Empty;
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
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

        //DistrictId
        private int _DistrictId = 0;
        public int DistrictId
        {
            get { return _DistrictId; }
            set { _DistrictId = value; }
        }

        //Municipality
        private string _Municipality = string.Empty;
        public string Municipality
        {
            get { return _Municipality; }
            set { _Municipality = value; }
        }

        //MunicipalityId
        private int _MunicipalityId = 0;
        public int MunicipalityId
        {
            get { return _MunicipalityId; }
            set { _MunicipalityId = value; }
        }

        //State
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        //CountryCode
        private string _CountryCode = string.Empty;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        //CountryName
        private string _CountryName = string.Empty;
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        //CountryId
        private int _CountryId = 0;
        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        //TransactionPassword
        private string _TransactionPassword = string.Empty;
        public string TransactionPassword
        {
            get { return _TransactionPassword; }
            set { _TransactionPassword = value; }
        }

        //ZipCode
        private string _ZipCode = string.Empty;
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        //UserId
        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        //LastName
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //RoleName
        private string _RoleName = string.Empty;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //IpAddress
        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        //StoreName
        private string _StoreName = string.Empty;
        public string StoreName
        {
            get { return _StoreName; }
            set { _StoreName = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //EquipmentId
        private Int64 _EquipmentId = 0;
        public Int64 EquipmentId
        {
            get { return _EquipmentId; }
            set { _EquipmentId = value; }
        }

        //StoreId
        private Int64 _StoreId = 0;
        public Int64 StoreId
        {
            get { return _StoreId; }
            set { _StoreId = value; }
        }

        //RoleId
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //DeviceToken
        private string _DeviceToken = string.Empty;
        public string DeviceToken
        {
            get { return _DeviceToken; }
            set { _DeviceToken = value; }
        }

        //SessionId
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        //StateId
        private int _StateId = 0;
        public int StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }

        //AllTransactions
        private int _AllTransactions = 0;
        public int AllTransactions
        {
            get { return _AllTransactions; }
            set { _AllTransactions = value; }
        }

        //TodayTransactions
        private int _TodayTransactions = 0;
        public int TodayTransactions
        {
            get { return _TodayTransactions; }
            set { _TodayTransactions = value; }
        }

        //ThisWeekTransactions
        private int _ThisWeekTransactions = 0;
        public int ThisWeekTransactions
        {
            get { return _ThisWeekTransactions; }
            set { _ThisWeekTransactions = value; }
        }

        //ThisMonthTransactions
        private int _ThisMonthTransactions = 0;
        public int ThisMonthTransactions
        {
            get { return _ThisMonthTransactions; }
            set { _ThisMonthTransactions = value; }
        }

        //TotalUsers
        private int _TotalUsers = 0;
        public int TotalUsers
        {
            get { return _TotalUsers; }
            set { _TotalUsers = value; }
        }

        //BlockUsers
        private int _BlockUsers = 0;
        public int BlockUsers
        {
            get { return _BlockUsers; }
            set { _BlockUsers = value; }
        }

        //ActiveUsers
        private int _ActiveUsers = 0;
        public int ActiveUsers
        {
            get { return _ActiveUsers; }
            set { _ActiveUsers = value; }
        }

        //PendingKycRequest
        private int _PendingKycRequest = 0;
        public int PendingKycRequest
        {
            get { return _PendingKycRequest; }
            set { _PendingKycRequest = value; }
        }

        //CompleteKycRequest
        private int _CompleteKycRequest = 0;
        public int CompleteKycRequest
        {
            get { return _CompleteKycRequest; }
            set { _CompleteKycRequest = value; }
        }

        //RejectKycRequest
        private int _RejectKycRequest = 0;
        public int RejectKycRequest
        {
            get { return _RejectKycRequest; }
            set { _RejectKycRequest = value; }
        }

        //InCompleteKycRequest
        private int _InCompleteKycRequest = 0;
        public int InCompleteKycRequest
        {
            get { return _InCompleteKycRequest; }
            set { _InCompleteKycRequest = value; }
        }

        //TodayUsers
        private int _TodayUsers = 0;
        public int TodayUsers
        {
            get { return _TodayUsers; }
            set { _TodayUsers = value; }
        }

        //ThisMonthUsers
        private int _ThisMonthUsers = 0;
        public int ThisMonthUsers
        {
            get { return _ThisMonthUsers; }
            set { _ThisMonthUsers = value; }
        }

        //ThisWeekUsers
        private int _ThisWeekUsers = 0;
        public int ThisWeekUsers
        {
            get { return _ThisWeekUsers; }
            set { _ThisWeekUsers = value; }
        }

        //TotalKYCRequests
        private int _TotalKYCRequests = 0;
        public int TotalKYCRequests
        {
            get { return _TotalKYCRequests; }
            set { _TotalKYCRequests = value; }
        }

        //TodayOldUser
        private int _TodayOldUser = 0;
        public int TodayOldUser
        {
            get { return _TodayOldUser; }
            set { _TodayOldUser = value; }
        }

        //MonthlyOldUser
        private int _MonthlyOldUser = 0;
        public int MonthlyOldUser
        {
            get { return _MonthlyOldUser; }
            set { _MonthlyOldUser = value; }
        }

        //WeeklyOldUser
        private int _WeeklyOldUser = 0;
        public int WeeklyOldUser
        {
            get { return _WeeklyOldUser; }
            set { _WeeklyOldUser = value; }
        }

        //YesterDayOldUser
        private int _YesterDayOldUser = 0;
        public int YesterDayOldUser
        {
            get { return _YesterDayOldUser; }
            set { _YesterDayOldUser = value; }
        }

        //TodayCreditTransactions
        private decimal _TodayCreditTransactions = 0;
        public decimal TodayCreditTransactions
        {
            get { return _TodayCreditTransactions; }
            set { _TodayCreditTransactions = value; }
        }

        //TotalAmount
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        //EodBalance
        private decimal _EodBalance = 0;
        public decimal EodBalance
        {
            get { return _EodBalance; }
            set { _EodBalance = value; }
        }


        //EodUserCount
        private int _EodUserCount = 0;
        public int EodUserCount
        {
            get { return _EodUserCount; }
            set { _EodUserCount = value; }
        }

        //OldUsers
        private int _OldUsers = 0;
        public int OldUsers
        {
            get { return _OldUsers; }
            set { _OldUsers = value; }
        }

        //NewUsers
        private int _NewUsers = 0;
        public int NewUsers
        {
            get { return _NewUsers; }
            set { _NewUsers = value; }
        }

        //OldUserPasswordSet
        private int _OldUserPasswordSet = 0;
        public int OldUserPasswordSet
        {
            get { return _OldUserPasswordSet; }
            set { _OldUserPasswordSet = value; }
        }

        //OldUserLogin
        private int _OldUserLogin = 0;
        public int OldUserLogin
        {
            get { return _OldUserLogin; }
            set { _OldUserLogin = value; }
        }

        //EodActiveUser
        private int _EodActiveUser = 0;
        public int EodActiveUser
        {
            get { return _EodActiveUser; }
            set { _EodActiveUser = value; }
        }

        //EodInActiveUser
        private int _EodInActiveUser = 0;
        public int EodInActiveUser
        {
            get { return _EodInActiveUser; }
            set { _EodInActiveUser = value; }
        }

        //_TodayDebitTransactions
        private decimal _TodayDebitTransactions = 0;
        public decimal TodayDebitTransactions
        {
            get { return _TodayDebitTransactions; }
            set { _TodayDebitTransactions = value; }
        }

        //ThisMonthCreditTransactions
        private decimal _ThisMonthCreditTransactions = 0;
        public decimal ThisMonthCreditTransactions
        {
            get { return _ThisMonthCreditTransactions; }
            set { _ThisMonthCreditTransactions = value; }
        }

        //ThisMonthDebitTransactions
        private decimal _ThisMonthDebitTransactions = 0;
        public decimal ThisMonthDebitTransactions
        {
            get { return _ThisMonthDebitTransactions; }
            set { _ThisMonthDebitTransactions = value; }
        }

        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }
        private string _UpdatedDatedt = string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }
        }

        private string _TotalUserCount = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TotalUserCount
        {
            get { return _TotalUserCount; }
            set { _TotalUserCount = value; }
        }

        private string _ServiceStatus = string.Empty;
        public string ServiceStatus
        {
            get { return _ServiceStatus; }
            set { _ServiceStatus = value; }
        }

        private DateTime _LastServiceConnected = DateTime.UtcNow;
        public DateTime LastServiceConnected
        {
            get { return _LastServiceConnected; }
            set { _LastServiceConnected = value; }
        }

        private bool _IsPasswordExpired = false;
        public bool IsPasswordExpired
        {
            get { return _IsPasswordExpired; }
            set { _IsPasswordExpired = value; }
        }

        private DateTime _PasswordExpireDate = DateTime.UtcNow;
        public DateTime PasswordExpireDate
        {
            get { return _PasswordExpireDate; }
            set { _PasswordExpireDate = value; }
        }

        #endregion
    }
}