using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetUser : CommonGet
    {

        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _RoleName = string.Empty;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        private string _CheckPasswordReset = string.Empty;
        public string CheckPasswordReset
        {
            get { return _CheckPasswordReset; }
            set { _CheckPasswordReset = value; }
        }
        private string _CheckNotPasswordReset = string.Empty;
        public string CheckNotPasswordReset
        {
            get { return _CheckNotPasswordReset; }
            set { _CheckNotPasswordReset = value; }
        }
        private string _CheckNotPin = string.Empty;
        public string CheckNotPin
        {
            get { return _CheckNotPin; }
            set { _CheckNotPin = value; }
        }
        private string _CheckPin = string.Empty;
        public string CheckPin
        {
            get { return _CheckPin; }
            set { _CheckPin = value; }
        }

        private string _CheckFirstName = string.Empty;
        public string CheckFirstName
        {
            get { return _CheckFirstName; }
            set { _CheckFirstName = value; }
        }

        private string _CheckNotFirstName = string.Empty;
        public string CheckNotFirstName
        {
            get { return _CheckNotFirstName; }
            set { _CheckNotFirstName = value; }
        }
        private int _RoleId = -1;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }
        private string _DeviceId = string.Empty;
        public string DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        private string _VerificationCode = string.Empty;
        public string VerificationCode
        {
            get { return _VerificationCode; }
            set { _VerificationCode = value; }
        }
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _LoginFromDate = string.Empty;
        public string LoginFromDate
        {
            get { return _LoginFromDate; }
            set { _LoginFromDate = value; }
        }
        private string _LoginToDate = string.Empty;
        public string LoginToDate
        {
            get { return _LoginToDate; }
            set { _LoginToDate = value; }
        }
        private int _CheckOldAndNewUser = 2;
        public int CheckOldAndNewUser
        {
            get { return _CheckOldAndNewUser; }
            set { _CheckOldAndNewUser = value; }
        }
        private int _CountryId = 0;
        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }
        private Int64 _RefId = 0;
        public Int64 RefId
        {
            get { return _RefId; }
            set { _RefId = value; }
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
   
        private int _IsKYCApproved = -1;
        public int IsKYCApproved
        {
            get { return _IsKYCApproved; }
            set { _IsKYCApproved = value; }
        }
        private int _StateId = 0;
        public int StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }
        private int _IsPhoneVerified = 2;
        public int IsPhoneVerified
        {
            get { return _IsPhoneVerified; }
            set { _IsPhoneVerified = value; }
        }
    
        private string _CheckCreatedDate = string.Empty;
        public string CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private string _Sort = string.Empty;
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        private int _IsEmailVerified = 2;
        public int IsEmailVerified
        {
            get { return _IsEmailVerified; }
            set { _IsEmailVerified = value; }
        }
    
        private long _TotalAmount = 0;
        public long TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private int _CheckRefId = 2;
        public int CheckRefId
        {
            get { return _CheckRefId; }
            set { _CheckRefId = value; }
        }
        private int _CheckDirectId = 2;
        public int CheckDirectId
        {
            get { return _CheckDirectId; }
            set { _CheckDirectId = value; }
        }
        private int _CheckDeviceCode = 2;
        public int CheckDeviceCode
        {
            get { return _CheckDeviceCode; }
            set { _CheckDeviceCode = value; }
        }
        private string _DateTo = string.Empty;
        public string DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }
        private string _DateFrom = string.Empty;
        public string DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }
     
        private int _EmployeeType = 0;
        public int EmployeeType
        {
            get { return _EmployeeType; }
            set { _EmployeeType = value; }
        }
        private int _ProofType = 0;
        public int ProofType
        {
            get { return _ProofType; }
            set { _ProofType = value; }
        }



        private string _JwtToken = string.Empty;
        public string JwtToken
        {
            get { return _JwtToken; }
            set { _JwtToken = value; }
        }
        private string _UpdatedBy = string.Empty;
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }

        private string _RefCodeAttempted = string.Empty;
        public string RefCodeAttempted
        {
            get { return _RefCodeAttempted; }
            set { _RefCodeAttempted = value; }
        }

        private string _ReviewDateTo = string.Empty;
        public string ReviewDateTo
        {
            get { return _ReviewDateTo; }
            set { _ReviewDateTo = value; }
        }
        private string _ReviewDateFrom = string.Empty;
        public string ReviewDateFrom
        {
            get { return _ReviewDateFrom; }
            set { _ReviewDateFrom = value; }
        }

        private string _DocumentNumber = string.Empty;
        public string DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
        }

        #region GetMethods

        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText, ref int FilterCount)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("Name", FirstName);
                HT.Add("Email", Email);
                HT.Add("StartDate", DateFrom);
                HT.Add("EndDate", DateTo);
                HT.Add("MemberId", MemberId);
                HT.Add("IsKYCApproved", IsKYCApproved);
                HT.Add("RoleId", RoleId);
                HT.Add("RefCode", RefCode);
                HT.Add("CheckPin", CheckPin);
                HT.Add("CheckPasswordReset", CheckPasswordReset);
                HT.Add("CheckFirstName", CheckFirstName);
                HT.Add("CheckNotFirstName", CheckNotFirstName);
                HT.Add("CheckNotPasswordReset", CheckNotPasswordReset);
                HT.Add("CheckNotPin", CheckNotPin);
                HT.Add("RefId", RefId);
                HT.Add("RefCodeAttempted", RefCodeAttempted);
                HT.Add("ReviewDateFrom", ReviewDateFrom);
                HT.Add("ReviewDateTo", ReviewDateTo);
                HT.Add("DocumentNumber", DocumentNumber);
                HT.Add("DeviceId", DeviceId);
                //dt = obj.GetDataFromStoredProcedure("sp_UsersKYC_Datatable", HT);
                dt = obj.GetDataFromStoredProcedure("sp_Users_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_UsersKYC_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        FilterCount = Convert.ToInt32(dtCounter.Rows[0]["DataCounter"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

        public DataTable GetData_OldUserExport()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("Name", FirstName);
                HT.Add("Email", Email);
                HT.Add("StartDate", DateFrom);
                HT.Add("EndDate", DateTo);
                HT.Add("MemberId", MemberId);
                HT.Add("IsKYCApproved", IsKYCApproved);
                HT.Add("RoleId", RoleId);
                HT.Add("RefCode", RefCode);
                HT.Add("CheckPin", CheckPin);
                HT.Add("CheckPasswordReset", CheckPasswordReset);
                HT.Add("CheckFirstName", CheckFirstName);
                HT.Add("CheckNotFirstName", CheckNotFirstName);
                HT.Add("CheckNotPasswordReset", CheckNotPasswordReset);
                HT.Add("CheckNotPin", CheckNotPin);
                HT.Add("RefId", RefId);
                HT.Add("RefCodeAttempted", RefCodeAttempted);
                dt = obj.GetDataFromStoredProcedure("sp_OldUserExport_Get", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

        public DataTable GetData_UserExport()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                dt = obj.GetDataFromStoredProcedure("sp_Users_Export", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }


        public DataTable GetData_UserKYCExport()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("ContactNumber", ContactNumber);
                HT.Add("Name", FirstName);
                HT.Add("Email", Email);
                HT.Add("StartDate", DateFrom);
                HT.Add("EndDate", DateTo);
                HT.Add("MemberId", MemberId);
                HT.Add("IsKYCApproved", IsKYCApproved);
                HT.Add("RefCode", RefCode);                
                HT.Add("ReviewDateFrom", ReviewDateFrom);
                HT.Add("ReviewDateTo", ReviewDateTo);
                dt = obj.GetDataFromStoredProcedure("sp_UserKYCExport_Get", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        public DataTable GetDataDump_ActiveUserNoTxnExport()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                dt = obj.GetDataFromStoredProcedure("sp_ActiveUsersNoTxn_Dump", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        #endregion
    }
}