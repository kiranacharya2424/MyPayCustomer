using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetMerchant:CommonGet
    {
        #region "Properties"

        //MerchantUniqueId
        private string _MerchantUniqueId = string.Empty;
        public string MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }

        //FirstName
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
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

        //CheckPasswordReset
        private int _CheckPasswordReset = 2;
        public int CheckPasswordReset
        {
            get { return _CheckPasswordReset; }
            set { _CheckPasswordReset = value; }
        }

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        //UserName
        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //RoleId
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private Int64 _MerchantMemberId = 0;
        public Int64 MerchantMemberId
        {
            get { return _MerchantMemberId; }
            set { _MerchantMemberId = value; }
        }

        //OrganizationName
        private string _OrganizationName = string.Empty;
        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private Int32 _MerchantType = 2;
        public Int32 MerchantType
        {
            get { return _MerchantType; }
            set { _MerchantType = value; }
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
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("Name", FirstName);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckPasswordReset", CheckPasswordReset);
                HT.Add("MerchantType", MerchantType);
                dt = obj.GetDataFromStoredProcedure("sp_Merchant_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        public DataTable GetDataDump()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("FirstName", FirstName);
                HT.Add("ContactNo", ContactNo);
                HT.Add("EmailID", EmailID);
                HT.Add("MerchantType", MerchantType);
                dt = obj.GetDataFromStoredProcedure("sp_Merchant_DumpGet", HT);
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