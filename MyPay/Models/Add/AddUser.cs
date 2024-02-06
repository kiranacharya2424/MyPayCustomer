using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyPay.Models.Add
{
    public class AddUser : CommonAdd
    {
        #region "Enums"
        public enum sex
        {
            Not_Selected = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }
        public enum meritalstatus
        {
            Not_Selected = 0,
            Unmarried = 1,
            Married = 2,
            Divorced = 3
        }
        public enum UserRoles
        {
            Administrator = 1,
            User = 2,
            Agent = 3,
            Merchant = 4,
            Employee = 5,
            Business = 6,
            Marketing = 7,
            Auditor = 8,
            Remittance = 9
        }
        public enum nationality
        {
            Not_Selected = 0,
            Nepalese = 1,
            Others = 2,
            Indian=3
        }
        public enum ProofTypes
        {
            Passport = 1,
            Driving_Licence = 2,
            Voter_Id = 3,
            Citizenship = 4,
            National_IdCard=5
        }
        public enum kyc
        {
            Not_Filled = 0,
            Pending = 1,
            InComplete = 2,
            Verified = 3,
            Rejected = 4,
            Proof_Rejected = 5,
            Risk_High = 6
        }
        public enum ActiveStatus
        {
            Blocked = 0,
            Active = 1
        }

        public enum UserType
        {
            Admin = 1,
            User = 0
        }
        public enum OldAndNewUser
        {
            [Display(Name = "New User")]
            NewUser = 0,
            [Display(Name = "Old User")]
            OldUser = 1
        }
        public enum OldUserStatus
        {
            [Display(Name = "Password Reset")]
            Password_Reset = 1,
            [Display(Name = "Password Not Reset")]
            Password_Not_Reset = 2,
            [Display(Name = "Pin Reset")]
            Pin_Reset = 3,
            [Display(Name = "Pin Not Reset")]
            Pin_Not_Reset = 4,
            [Display(Name = "FirstName Updated")]
            FirstName_Updated = 5,
            [Display(Name = "FirstName Not Updated")]
            FirstName_NotUpdated = 6
        }

        #endregion
        private string _UserId = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _Password = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private string _Message = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private string _TotalUserCount = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TotalUserCount
        {
            get { return _TotalUserCount; }
            set { _TotalUserCount = value; }
        }
        private string _FirstName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _MotherName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MotherName
        {
            get { return _MotherName; }
            set { _MotherName = value; }
        }

        private string _SpouseName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SpouseName
        {
            get { return _SpouseName; }
            set { _SpouseName = value; }
        }

       

        private string _MUniqueId = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MUniqueId
        {
            get { return _MUniqueId; }
            set { _MUniqueId = value; }
        }
        private string _LastName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _Address = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _State = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _City = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _ZipCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        private Int32 _RoleId = 0;
        public Int32 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private string _RoleName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        private Int32 _CountryId = 0;
        public Int32 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        private string _TransactionPassword = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TransactionPassword
        {
            get { return _TransactionPassword; }
            set { _TransactionPassword = value; }
        }
        private string _UserImage = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _Email = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _ContactNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        private string _AlternateContactNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AlternateContactNumber
        {
            get { return _AlternateContactNumber; }
            set { _AlternateContactNumber = value; }
        }
        private string _IpAddress = Common.Common.GetUserIP();
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private bool _IsOldUser = false;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsOldUser
        {
            get { return _IsOldUser; }
            set { _IsOldUser = value; }
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
        private DateTime _TokenUpdatedTime = System.DateTime.UtcNow;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public DateTime TokenUpdatedTime
        {
            get { return _TokenUpdatedTime; }
            set { _TokenUpdatedTime = value; }
        }
        private string _VerificationCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }
        private string _DeviceCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }
        private string _RefCodeAttempted = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RefCodeAttempted
        {
            get { return _RefCodeAttempted; }
            set { _RefCodeAttempted = value; }
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
        private Int64 _RefId = 0;
        public Int64 RefId
        {
            get { return _RefId; }
            set { _RefId = value; }
        }
        private string _RefCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }
        private Int32 _StateId = 0;
        public Int32 StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }
        private string _DateofBirth = string.Empty;
        public string DateofBirth
        {
            get { return _DateofBirth; }
            set { _DateofBirth = value; }
        }
        private Int32 _Day = 0;
        public Int32 Day
        {
            get { return _Day; }
            set { _Day = value; }
        }
        private Int32 _Month = 0;
        public Int32 Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        private Int32 _Year = 0;
        public Int32 Year
        {
            get { return _Year; }
            set { _Year = value; }
        }


        private Int32 _Gender = 0;
        public Int32 Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        private string _NationalIdProofFront = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NationalIdProofFront
        {
            get { return _NationalIdProofFront; }
            set { _NationalIdProofFront = value; }
        }
        private string _NationalIdProofBack = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NationalIdProofBack
        {
            get { return _NationalIdProofBack; }
            set { _NationalIdProofBack = value; }
        }
        private Int32 _IsKYCApproved = 0;
        public Int32 IsKYCApproved
        {
            get { return _IsKYCApproved; }
            set { _IsKYCApproved = value; }
        }

        //KYCStatusName
        private string _KYCStatusName = string.Empty;
        public string KYCStatusName
        {
            get { return _KYCStatusName; }
            set { _KYCStatusName = value; }
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
        private decimal _TotalRewardPoints = 0;
        public decimal TotalRewardPoints
        {
            get { return _TotalRewardPoints; }
            set { _TotalRewardPoints = value; }
        }
        private Int32 _EmployeeType = 0;
        public Int32 EmployeeType
        {
            get { return _EmployeeType; }
            set { _EmployeeType = value; }
        }
        private string _PhoneExtension = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PhoneExtension
        {
            get { return _PhoneExtension; }
            set { _PhoneExtension = value; }
        }
        private Int32 _ProofType = 0;
        public Int32 ProofType
        {
            get { return _ProofType; }
            set { _ProofType = value; }
        }
        private string _MiddleName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _Pin = "";
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        private string _Lat = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Lat
        {
            get { return _Lat; }
            set { _Lat = value; }
        }
        private string _Lon = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Lon
        {
            get { return _Lon; }
            set { _Lon = value; }
        }
        private string _DocumentNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
        }
        private string _IssueDate = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }
        private string _ExpiryDate = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        private string _IssuedBy = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IssuedBy
        {
            get { return _IssuedBy; }
            set { _IssuedBy = value; }
        }
        private int _DOBType = 0;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int DOBType
        {
            get { return _DOBType; }
            set { _DOBType = value; }
        }
        private string _DOBType2 = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DOBType2
        {
            get { return _DOBType2; }
            set { _DOBType2 = value; }
        }
        //Issue date 
        private Int32 _IssueDay = 0;
        public Int32 IssueDay
        {
            get { return _IssueDay; }
            set { _IssueDay = value; }
        }
        private Int32 _IssueMonth = 0;
        public Int32 IssueMonth
        {
            get { return _IssueMonth; }
            set { _IssueMonth = value; }
        }
        private Int32 _IssueYear = 0;
        public Int32 IssueYear
        {
            get { return _IssueYear; }
            set { _IssueYear = value; }
        }
        private string _IssueDateType = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IssueDateType
        {
            get { return _IssueDateType; }
            set { _IssueDateType = value; }
        }

        //ExpiryDateType 
        private string _ExpiryDateType = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ExpiryDateType
        {
            get { return _ExpiryDateType; }
            set { _ExpiryDateType = value; }
        }
         
        private Int32 _ExpiryDay = 0;
        public Int32 ExpiryDay
        {
            get { return _ExpiryDay; }
            set { _ExpiryDay = value; }
        }
        private Int32 _ExpiryMonth = 0;
        public Int32 ExpiryMonth
        {
            get { return _ExpiryMonth; }
            set { _ExpiryMonth = value; }
        }
        private Int32 _ExpiryYear = 0;
        public Int32 ExpiryYear
        {
            get { return _ExpiryYear; }
            set { _ExpiryYear = value; }
        }
        private string _Remarks = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        //MeritalStatus
        private int _MeritalStatus = 0;
        public int MeritalStatus
        {
            get { return _MeritalStatus; }
            set { _MeritalStatus = value; }
        }
        //FatherName
        private string _FatherName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }
        //GrandFatherName
        private string _GrandFatherName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string GrandFatherName
        {
            get { return _GrandFatherName; }
            set { _GrandFatherName = value; }
        }
        //Occupation
        private string _Occupation = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }

        //Nationality
        private int _Nationality = 0;
        public int Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
        //UserRoles
        private UserRoles _UserRoleEnumType = 0;
        public UserRoles UserRoleEnumType
        {
            get { return _UserRoleEnumType; }
            set { _UserRoleEnumType = value; }
        }
        //UserRoles
        private ActiveStatus _IsActiveStatusEnum = 0;
        public ActiveStatus IsActiveStatusEnum
        {
            get { return _IsActiveStatusEnum; }
            set { _IsActiveStatusEnum = value; }
        }

        //OldUserStatuses
        private OldUserStatus _OldUserStatuses = 0;
        public OldUserStatus OldUserStatuses
        {
            get { return _OldUserStatuses; }
            set { _OldUserStatuses = value; }
        }

        //OldAndNewUsers
        private OldAndNewUser _OldAndNewUsers = 0;
        public OldAndNewUser OldAndNewUsers
        {
            get { return _OldAndNewUsers; }
            set { _OldAndNewUsers = value; }
        }
        //DistrictId
        private int _DistrictId = 0;
        public int DistrictId
        {
            get { return _DistrictId; }
            set { _DistrictId = value; }
        }
        //MunicipalityId
        private int _MunicipalityId = 0;
        public int MunicipalityId
        {
            get { return _MunicipalityId; }
            set { _MunicipalityId = value; }
        }
        //Municipality
        private string _Municipality = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Municipality
        {
            get { return _Municipality; }
            set { _Municipality = value; }
        }
        //WardNumber
        private string _WardNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string WardNumber
        {
            get { return _WardNumber; }
            set { _WardNumber = value; }
        }
        //StreetName
        private string _StreetName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        //HouseNumber
        private string _HouseNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HouseNumber
        {
            get { return _HouseNumber; }
            set { _HouseNumber = value; }
        }
        //IsYourPermanentAndTemporaryAddressSame
        private bool _IsYourPermanentAndTemporaryAddressSame = false;
        public bool IsYourPermanentAndTemporaryAddressSame
        {
            get { return _IsYourPermanentAndTemporaryAddressSame; }
            set { _IsYourPermanentAndTemporaryAddressSame = value; }
        }
        //CountryName
        private string _CountryName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        //TransactionPin
        private string _TransactionPin = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TransactionPin
        {
            get { return _TransactionPin; }
            set { _TransactionPin = value; }
        }
        //CurrentStateId
        private int _CurrentStateId = 0;
        public int CurrentStateId
        {
            get { return _CurrentStateId; }
            set { _CurrentStateId = value; }
        }
        //CurrentState
        private string _CurrentState = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentState
        {
            get { return _CurrentState; }
            set { _CurrentState = value; }
        }
        //CurrentDistrictId
        private int _CurrentDistrictId = 0;
        public int CurrentDistrictId
        {
            get { return _CurrentDistrictId; }
            set { _CurrentDistrictId = value; }
        }
        //CurrentDistrict
        private string _CurrentDistrict = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentDistrict
        {
            get { return _CurrentDistrict; }
            set { _CurrentDistrict = value; }
        }
        //CurrentMunicipalityId
        private int _CurrentMunicipalityId = 0;
        public int CurrentMunicipalityId
        {
            get { return _CurrentMunicipalityId; }
            set { _CurrentMunicipalityId = value; }
        }
        //CurrentMunicipality
        private string _CurrentMunicipality = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentMunicipality
        {
            get { return _CurrentMunicipality; }
            set { _CurrentMunicipality = value; }
        }
        //CurrentWardNumber
        private string _CurrentWardNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentWardNumber
        {
            get { return _CurrentWardNumber; }
            set { _CurrentWardNumber = value; }
        }
        //CurrentStreetName
        private string _CurrentStreetName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentStreetName
        {
            get { return _CurrentStreetName; }
            set { _CurrentStreetName = value; }
        }
        //CurrentHouseNumber
        private string _CurrentHouseNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentHouseNumber
        {
            get { return _CurrentHouseNumber; }
            set { _CurrentHouseNumber = value; }
        }
        private DateTime _LastLogin = System.DateTime.UtcNow;
        public DateTime LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }

        private DateTime _KYCCreatedDate = System.DateTime.UtcNow;
        public DateTime KYCCreatedDate
        {
            get { return _KYCCreatedDate; }
            set { _KYCCreatedDate = value; }
        }
        private Int64 _ApprovedorRejectedBy = 0;
        public Int64 ApprovedorRejectedBy
        {
            get { return _ApprovedorRejectedBy; }
            set { _ApprovedorRejectedBy = value; }
        }
        private string _ApprovedorRejectedByName = String.Empty;
        public string ApprovedorRejectedByName
        {
            get { return _ApprovedorRejectedByName; }
            set { _ApprovedorRejectedByName = value; }
        }
        private string _TimeElapsed = String.Empty;
        public string TimeElapsed
        {
            get { return _TimeElapsed; }
            set { _TimeElapsed = value; }
        }
        private string _GenderName = string.Empty;
        public string GenderName
        {
            get { return _GenderName; }
            set { _GenderName = value; }
        }
        private string _DateofBirthdt = string.Empty;
        public string DateofBirthdt
        {
            get { return _DateofBirthdt; }
            set { _DateofBirthdt = value; }
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
        private string _KYCCreatedDatedt = string.Empty;
        public string KYCCreatedDatedt
        {
            get { return _KYCCreatedDatedt; }
            set { _KYCCreatedDatedt = value; }
        }
        private string _KYCReviewDateDt = string.Empty;
        public string KYCReviewDateDt
        {
            get { return _KYCReviewDateDt; }
            set { _KYCReviewDateDt = value; }
        }
        private Int64 _IssueFromDistrictID = 0;
        public Int64 IssueFromDistrictID
        {
            get { return _IssueFromDistrictID; }
            set { _IssueFromDistrictID = value; }
        }
        private Int64 _IssueFromStateID = 0;
        public Int64 IssueFromStateID
        {
            get { return _IssueFromStateID; }
            set { _IssueFromStateID = value; }
        }
        private string _IssueFromDistrictName = string.Empty;
        public string IssueFromDistrictName
        {
            get { return _IssueFromDistrictName; }
            set { _IssueFromDistrictName = value; }
        }
        private string _IssueFromStateName = string.Empty;
        public string IssueFromStateName
        {
            get { return _IssueFromStateName; }
            set { _IssueFromStateName = value; }
        }
        private int _IsBankAdded = 0;
        public int IsBankAdded
        {
            get { return _IsBankAdded; }
            set { _IsBankAdded = value; }
        }
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

        private bool _WebLoginAttempted = false;
        public bool WebLoginAttempted
        {
            get { return _WebLoginAttempted; }
            set { _WebLoginAttempted = value; }
        }
        
        private bool _EnablePushNotification = true;
        public bool EnablePushNotification
        {
            get { return _EnablePushNotification; }
            set { _EnablePushNotification = value; }
        }
        private kyc _KycStatusEnum = 0;
        public kyc KycStatusEnum
        {
            get { return _KycStatusEnum; }
            set { _KycStatusEnum = value; }
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
        #region GetMethods
        public float CountBankListCheck(string MemberId, Int64 Id)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "SELECT count(0) FROM UserBankDetail with(nolock) where MemberId = " + MemberId + "  and IsActive= 1 and IsDeleted = 0";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (float.Parse(Result));
                }
            }
            catch (Exception ex)
            {
            }
            return data;
        }
        #endregion
        public bool LoginUpdate(string authenticationToken)
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
    public class AddUser_Export
    {

        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long MemberId { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int Gender { get; set; }
        public decimal TotalAmount { get; set; }
        public int Pin { get; set; }

        public int MeritalStatus { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public int Nationality { get; set; }
        public string Municipality { get; set; }
        public string WardNumber { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }

        public string CurrentDistrict { get; set; }

        public string CurrentMunicipality { get; set; }
        public string CurrentWardNumber { get; set; }
        public string CurrentStreetName { get; set; }
        public string CurrentHouseNumber { get; set; }
        public System.DateTime LastLogin { get; set; }
    }


}