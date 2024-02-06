using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetAdmin:CommonGet
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

        //CountryId
        private int? _CountryId = 0;
        public int? CountryId
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

        //UserId
        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
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

        //UserName
        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //MemberId
        private Int64? _MemberId = 0;
        public Int64? MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //EquipmentId
        private Int64? _EquipmentId = 0;
        public Int64? EquipmentId
        {
            get { return _EquipmentId; }
            set { _EquipmentId = value; }
        }

        //StoreId
        private Int64? _StoreId = 0;
        public Int64? StoreId
        {
            get { return _StoreId; }
            set { _StoreId = value; }
        }

        //RoleId
        private int? _RoleId = 0;
        public int? RoleId
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
        private int? _StateId = 0;
        public int? StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }

        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private Int32 _CheckPasswordExpired = 2;
        public Int32 CheckPasswordExpired
        {
            get { return _CheckPasswordExpired; }
            set { _CheckPasswordExpired = value; }
        }

        #endregion

        #region GetMethods

        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
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
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("MemberId", MemberId);
                HT.Add("RoleId", RoleId);
                dt = obj.GetDataFromStoredProcedure("sp_AdminUser_Datatable", HT);
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