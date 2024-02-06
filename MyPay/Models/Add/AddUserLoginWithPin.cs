using Microsoft.Office.Interop.Excel;
using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
namespace MyPay.Models.Add
{
    public class AddUserLoginWithPin
    {


        private string _FirstName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _ContactNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        private string _Email = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Password = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private bool _IsResetPasswordFromAdmin = false;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
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


        private bool _WebLoginAttempted = false;
        public bool WebLoginAttempted
        {
            get { return _WebLoginAttempted; }
            set { _WebLoginAttempted = value; }
        }


        private bool _IsActive = false;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }


        private string _PhoneExtension = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PhoneExtension
        {
            get { return _PhoneExtension; }
            set { _PhoneExtension = value; }
        }
        private string _RefCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        private string _DateofBirth = string.Empty;
        public string DateofBirth
        {
            get { return _DateofBirth; }
            set { _DateofBirth = value; }
        }

        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        private Int64 _RefId = 0;
        public Int64 RefId
        {
            get { return _RefId; }
            set { _RefId = value; }
        }

        private int _IsBankAdded = 0;
        public int IsBankAdded
        {
            get { return _IsBankAdded; }
            set { _IsBankAdded = value; }
        }

        private int _DOBType = 0;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int DOBType
        {
            get { return _DOBType; }
            set { _DOBType = value; }
        }

        private Int32 _RoleId = 0;
        public Int32 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private string _IpAddress = Common.Common.GetUserIP();
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        private int _LoginAttemptCount = 0;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int LoginAttemptCount
        {
            get { return _LoginAttemptCount; }
            set { _LoginAttemptCount = value; }
        }
        private DateTime _LastLoginAttempt = System.DateTime.UtcNow;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime LastLoginAttempt
        {
            get { return _LastLoginAttempt; }
            set { _LastLoginAttempt = value; }
        }


        private string _DeviceCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }


        private string _DeviceId = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        private string _PlatForm = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }

        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }


        private Int32 _Gender = 0;
        public Int32 Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private Int32 _ProofType = 0;
        public Int32 ProofType
        {
            get { return _ProofType; }
            set { _ProofType = value; }
        }


        private Int32 _EmployeeType = 0;
        public Int32 EmployeeType
        {
            get { return _EmployeeType; }
            set { _EmployeeType = value; }
        }

        private Int32 _CountryId = 0;
        public Int32 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        private Int32 _IsKYCApproved = 0;
        public Int32 IsKYCApproved
        {
            get { return _IsKYCApproved; }
            set { _IsKYCApproved = value; }
        }


        private bool _IsPhoneVerified = false;
        public bool IsPhoneVerified
        {
            get { return _IsPhoneVerified; }
            set { _IsPhoneVerified = value; }
        }
        private bool _IsEmailVerified = false;
        public bool IsEmailVerified
        {
            get { return _IsEmailVerified; }
            set { _IsEmailVerified = value; }
        }
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        //CreatedDate
        private DateTime _CreatedDate = DateTime.UtcNow;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private bool _IsOldUser = false;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsOldUser
        {
            get { return _IsOldUser; }
            set { _IsOldUser = value; }
        }

        private decimal _TotalRewardPoints = 0;
        public decimal TotalRewardPoints
        {
            get { return _TotalRewardPoints; }
            set { _TotalRewardPoints = value; }
        }
        private string _Pin = "";
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        private string _VerificationCode = "";
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }
        private string _JwtToken = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string JwtToken
        {
            get { return _JwtToken; }
            set { _JwtToken = value; }
        }


        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        private DateTime _KYCReviewDate = System.DateTime.UtcNow;
        public DateTime KYCReviewDate
        {
            get { return _KYCReviewDate; }
            set { _KYCReviewDate = value; }
        }

        private string _Message = "";
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private string _UserImage = "";
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        private string _NationalIdProofFront = "";
        public string NationalIdProofFront
        {
            get { return _NationalIdProofFront; }
            set { _NationalIdProofFront = value; }
        }

        private string _NationalIdProofBack = "";
        public string NationalIdProofBack
        {
            get { return _NationalIdProofBack; }
            set { _NationalIdProofBack = value; }
        }

        private DateTime _LastLogin = System.DateTime.UtcNow;
        public DateTime LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }

        private string _UserId = "";
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _RoleName = "";
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        public bool LoginUpdate(string authenticationToken, string version_number = "")
        {
            try
            {
                string APIUser = Common.Common.GetCreatedByName(authenticationToken);
                if (APIUser.ToLower() == "web")
                {
                    DataRecieved = true;
                }
                else
                {
                    DataRecieved = false;
                    Common.CommonHelpers obj = new Common.CommonHelpers();
                    Hashtable HT = SetObject();
                    HT.Add("Id", Id);
                    HT.Add("Version_number", version_number);
                    string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_LoginUpdate", HT);
                    if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                    {
                        DataRecieved = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }


        public bool LoginUpdateWebClient(string authenticationToken)
        {
            try
            {
                string APIUser = Common.Common.GetCreatedByName(authenticationToken);

                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_LoginUpdate", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }

        public bool LogoutDevice(Int64 Uid, string DeviceCode)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("DeviceCode", DeviceCode);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_LogoutDevice", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public bool WebLoginUpdate(Int64 Uid)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_WebLoginUpdate", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public bool LastInvalidPinUpdate(Int64 Uid, int LoginAttemptCount, DateTime LastLoginAttempt)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("LoginAttemptCount", LoginAttemptCount);
                HT.Add("LastLoginAttempt", LastLoginAttempt);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_LoginInvalidPinUpdate", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }

        public bool ConfirmEmail(Int64 Uid, string Email, bool IsEmailVerified)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("IsEmailVerified", IsEmailVerified);
                HT.Add("Email", Email);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_ConfirmEmail", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool SetPin(Int64 Uid, string NewPin)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("Pin", NewPin);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_SetPin", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }
        public bool EnablePushNotification(Int64 Uid, bool EnablePushNotification)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("EnablePushNotification", EnablePushNotification);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_EnablePushNotification", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }

        public bool ResetPassword(Int64 Uid, string NewPassword, bool IsEmailVerified)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("Password", NewPassword);
                HT.Add("TransactionPassword", NewPassword);
                HT.Add("IsEmailVerified", IsEmailVerified);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_ResetPassword", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }
        public bool ChangePassword(Int64 Uid, string NewPassword, bool IsResetPasswordFromAdmin)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("Password", NewPassword);
                HT.Add("TransactionPassword", NewPassword);
                HT.Add("IsResetPasswordFromAdmin", IsResetPasswordFromAdmin);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_ChangePassword", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;

        }

        public bool ResetVerificationCode(Int64 Uid, string VerificationCode)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("VerificationCode", VerificationCode);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_ResetVerificationCode", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public bool UpdatePlatform(Int64 Uid, string Platform, string DeviceCode, string VerificationCode)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("DeviceCode", DeviceCode);
                HT.Add("Platform", Platform);
                HT.Add("VerificationCode", VerificationCode);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_UpdatePlatform", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool UpdateUserDocuments(Int64 Uid)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("Gender", Gender);
                HT.Add("EmployeeType", EmployeeType);
                HT.Add("NationalIdProofFront", NationalIdProofFront);
                HT.Add("NationalIdProofBack", NationalIdProofBack);
                HT.Add("UserImage", UserImage);
                HT.Add("ProofType", ProofType);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_UpdateUserDocuments", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }


        public bool UpdateDeviceId(Int64 Uid, string DeviceId)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("DeviceId", DeviceId);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_UpdateDeviceId", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        //public bool UpdateDeviceId_New(Int64 Uid, string DeviceId, string version)
        //{
        //    try
        //    {
        //        DataRecieved = false;
        //        Common.CommonHelpers obj = new Common.CommonHelpers();
        //        Hashtable HT = new Hashtable();
        //        HT.Add("Id", Uid);
        //        HT.Add("DeviceId", DeviceId);
        //        HT.Add("IpAddress", Common.Common.GetUserIP());
        //        HT.Add("Version", version);
        //        string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_UpdateDeviceId_New", HT);
        //        if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
        //        {
        //            DataRecieved = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DataRecieved = false;
        //    }
        //    return DataRecieved;
        //}


        public bool UpdateIsBankAdded(Int64 Uid, int IsBankAdded)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("IsBankAdded", IsBankAdded);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_UpdateIsBankAdded", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public bool UpdateKycDetails(Int64 Uid, string Remarks, int IsKYCApproved, Int64 UpdatedBy, Int64 ApprovedorRejectedBy, string ApprovedorRejectedByName, DateTime KYCReviewDate)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Uid);
                HT.Add("Remarks", Remarks);
                HT.Add("IsKYCApproved", IsKYCApproved);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("ApprovedorRejectedBy", ApprovedorRejectedBy);
                HT.Add("ApprovedorRejectedByName", ApprovedorRejectedByName);
                HT.Add("KYCReviewDate", KYCReviewDate);
                HT.Add("IpAddress", Common.Common.GetUserIP());
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_Users_UpdateKycDetails", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public Hashtable SetObject()
        {
            Hashtable Ht = new Hashtable();
            Ht.Add("MemberId", MemberId);
            Ht.Add("ContactNumber", ContactNumber);
            Ht.Add("DeviceCode", DeviceCode);
            Ht.Add("JWTToken", JwtToken);
            Ht.Add("IpAddress", IpAddress);
            Ht.Add("DeviceId", DeviceId);
            Ht.Add("Platform", PlatForm);
            Ht.Add("IsPhoneVerified", IsPhoneVerified);
            return Ht;
        }

    }



}