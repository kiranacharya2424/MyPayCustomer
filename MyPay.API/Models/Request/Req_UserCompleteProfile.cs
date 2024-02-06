using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_UserCompleteProfile : CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //StepCompleted
        private string _StepCompleted = string.Empty;
        public string StepCompleted
        {
            get { return _StepCompleted; }
            set { _StepCompleted = value; }
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

        //MiddleName
        private string _MiddleName = string.Empty;
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        //DateOfBirth
        private string _DateOfBirth = string.Empty;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        //Gender
        private int _Gender = 0;
        public int Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
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

        //MotherName
        private string _MotherName = string.Empty;
        public string MotherName
        {
            get { return _MotherName; }
            set { _MotherName = value; }
        }

        //SpouseName
        private string _SpouseName = string.Empty;
        public string SpouseName
        {
            get { return _SpouseName; }
            set { _SpouseName = value; }
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

        //StateId
        private int _StateId = 0;
        public int StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }

        //State
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        //DistrictId
        private int _DistrictId = 0;
        public int DistrictId
        {
            get { return _DistrictId; }
            set { _DistrictId = value; }
        }

        //District
        private string _District = string.Empty;
        public string District
        {
            get { return _District; }
            set { _District = value; }
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

        //ProofType
        private int _ProofType = 0;
        public int ProofType
        {
            get { return _ProofType; }
            set { _ProofType = value; }
        }

        //NationalIdProofFront
        private string _NationalIdProofFront = string.Empty;
        public string NationalIdProofFront
        {
            get { return _NationalIdProofFront; }
            set { _NationalIdProofFront = value; }
        }

        //NationalIdProofBack
        private string _NationalIdProofBack = string.Empty;
        public string NationalIdProofBack
        {
            get { return _NationalIdProofBack; }
            set { _NationalIdProofBack = value; }
        }

        //UserImage
        private string _UserImage = string.Empty;
        public string UserImage
        {
            get { return _UserImage; }
            set { _UserImage = value; }
        }

        //IsYourPermanentAndTemporaryAddressSame
        private bool _IsYourPermanentAndTemporaryAddressSame = false;
        public bool IsYourPermanentAndTemporaryAddressSame
        {
            get { return _IsYourPermanentAndTemporaryAddressSame; }
            set { _IsYourPermanentAndTemporaryAddressSame = value; }
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

        //IssuedBy
        private string _IssuedBy = String.Empty;
        public string IssuedBy
        {
            get { return _IssuedBy; }
            set { _IssuedBy = value; }
        }

        //DOBType
        private int _DOBType = 0;
        public int DOBType
        {
            get { return _DOBType; }
            set { _DOBType = value; }
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