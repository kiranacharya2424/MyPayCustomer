using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetUserDetail : CommonResponse
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

        //Value
        //
        private string _Value = string.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
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
        //IsBankAdded
        private bool _IsBankAdded = false;
        public bool IsBankAdded
        {
            get { return _IsBankAdded; }
            set { _IsBankAdded = value; }
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

        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _District = string.Empty;
        public string District
        {
            get { return _District; }
            set { _District = value; }
        }
        private string _ZipCode = string.Empty;
        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        private Int32 _CountryId = 0;
        public Int32 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        private string _UserImage = string.Empty;
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }

        private Int32 _RoleId = 0;
        public Int32 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private Int32 _StateId = 0;
        public Int32 StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }
        private string _DateofBirth = System.DateTime.UtcNow.ToShortDateString();
        public string DateofBirth
        {
            get { return _DateofBirth; }
            set { _DateofBirth = value; }
        }
        private string _NationalIdProofFront = string.Empty;
        public string NationalIdProofFront
        {
            get { return _NationalIdProofFront; }
            set { _NationalIdProofFront = value; }
        }
        private string _NationalIdProofBack = string.Empty;
        public string NationalIdProofBack
        {
            get { return _NationalIdProofBack; }
            set { _NationalIdProofBack = value; }
        }
        private string _AddressProof = string.Empty;
        public string AddressProof
        {
            get { return _AddressProof; }
            set { _AddressProof = value; }
        }
        private string _AddressProofNumber = string.Empty;
        public string AddressProofNumber
        {
            get { return _AddressProofNumber; }
            set { _AddressProofNumber = value; }
        }
        private string _IdProofNumber = string.Empty;
        public string IdProofNumber
        {
            get { return _IdProofNumber; }
            set { _IdProofNumber = value; }
        }
        private string _AdditionalProof = string.Empty;
        public string AdditionalProof
        {
            get { return _AdditionalProof; }
            set { _AdditionalProof = value; }
        }
        private string _BankAccountProof = string.Empty;
        public string BankAccountProof
        {
            get { return _BankAccountProof; }
            set { _BankAccountProof = value; }
        }
        private float _TotalAmount = 0;
        public float TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private Int32 _ProofType = 0;
        public Int32 ProofType
        {
            get { return _ProofType; }
            set { _ProofType = value; }
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
        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }

        //GrandFatherName
        private string _GrandFatherName = string.Empty;
        public string GrandFatherName
        {
            get { return _GrandFatherName; }
            set { _GrandFatherName = value; }
        }

        //Occupation
        private string _Occupation = string.Empty;
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

        //DistrictId
        private int _DistrictId = 0;
        public int DistrictId
        {
            get { return _DistrictId; }
            set { _DistrictId = value; }
        }

        //City
        private string _City = string.Empty;
        public string City
        {
            get { return _City; }
            set { _City = value; }
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
        public string Municipality
        {
            get { return _Municipality; }
            set { _Municipality = value; }
        }

        //WardNumber
        private string _WardNumber = string.Empty;
        public string WardNumber
        {
            get { return _WardNumber; }
            set { _WardNumber = value; }
        }

        //StreetName
        private string _StreetName = string.Empty;
        public string StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }

        //HouseNumber
        private string _HouseNumber = string.Empty;
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
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
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
        public string CurrentMunicipality
        {
            get { return _CurrentMunicipality; }
            set { _CurrentMunicipality = value; }
        }

        //CurrentWardNumber
        private string _CurrentWardNumber = string.Empty;
        public string CurrentWardNumber
        {
            get { return _CurrentWardNumber; }
            set { _CurrentWardNumber = value; }
        }

        //CurrentStreetName
        private string _CurrentStreetName = string.Empty;
        public string CurrentStreetName
        {
            get { return _CurrentStreetName; }
            set { _CurrentStreetName = value; }
        }

        //CurrentHouseNumber
        private string _CurrentHouseNumber = string.Empty;
        public string CurrentHouseNumber
        {
            get { return _CurrentHouseNumber; }
            set { _CurrentHouseNumber = value; }
        }
        //FirstName
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        //MiddleName
        private string _MiddleName = string.Empty;
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        //LastName
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        //OccupationName
        private string _OccupationName = string.Empty;
        public string OccupationName
        {
            get { return _OccupationName; }
            set { _OccupationName = value; }
        }

        //IssuedBy
        private string _IssuedBy = String.Empty;
        public string IssuedBy
        {
            get { return _IssuedBy; }
            set { _IssuedBy = value; }
        }
        //RefCode
        private string _RefCode = String.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }
        //RefId
        private string _RefId = String.Empty;
        public string RefId
        {
            get { return _RefId; }
            set { _RefId = value; }
        }


        //DOBType
        private int _DOBType = 0;
        public int DOBType
        {
            get { return _DOBType; }
            set { _DOBType = value; }
        }

        private string _IssueDate = string.Empty;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IssueDate

        {

            get { return _IssueDate; }

            set { _IssueDate = value; }

        }

        private string _DocumentNumber = string.Empty;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DocumentNumber

        {

            get { return _DocumentNumber; }

            set { _DocumentNumber = value; }

        }
        private string _ExpiryDate = string.Empty;

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ExpiryDate

        {

            get { return _ExpiryDate; }

            set { _ExpiryDate = value; }

        }

        private string _Pin = "";
        public string Pin

        {

            get { return _Pin; }

            set { _Pin = value; }

        }
        private string _AlternateContactNumber = string.Empty;
        public string AlternateContactNumber
        {
            get { return _AlternateContactNumber; }

            set { _AlternateContactNumber = value; }

        }
        private string _MotherName = string.Empty;
        public string MotherName
        {
            get { return _MotherName; }

            set { _MotherName = value; }

        }
        private string _SpouseName = string.Empty;
        public string SpouseName
        {
            get { return _SpouseName; }

            set { _SpouseName = value; }

        }



        
        private bool _EnablePushNotification = true;
        public bool EnablePushNotification
        {
            get { return _EnablePushNotification; }

            set { _EnablePushNotification = value; }

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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IssueFromDistrictName
        {
            get { return _IssueFromDistrictName; }
            set { _IssueFromDistrictName = value; }
        }
        private string _IssueFromStateName = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IssueFromStateName
        {
            get { return _IssueFromStateName; }
            set { _IssueFromStateName = value; }
        }
    }
}