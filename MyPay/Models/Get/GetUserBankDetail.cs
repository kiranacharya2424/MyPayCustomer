using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetUserBankDetail:CommonGet
    {
        #region "Properties"
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //BankCode
        private string _BankCode = string.Empty;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        //BankName
        private string _BankName = string.Empty;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        //BranchId
        private string _BranchId = string.Empty;
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        //BranchName
        private string _BranchName = string.Empty;
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }

        //AccountNumber
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }

        //CheckPrimary
        private int _CheckPrimary = 2;// (int)clsData.BooleanValue.Both;
        public int CheckPrimary
        {
            get { return _CheckPrimary; }
            set { _CheckPrimary = value; }
        }

        //Today
        private string _Today = string.Empty;
        public string Today
        {
            get { return _Today; }
            set { _Today = value; }
        }

        //Weekly
        private string _Weekly = string.Empty;
        public string Weekly
        {
            get { return _Weekly; }
            set { _Weekly = value; }
        }

        //Monthly
        private string _Monthly = string.Empty;
        public string Monthly
        {
            get { return _Monthly; }
            set { _Monthly = value; }
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
                HT.Add("Name", Name);
                HT.Add("MemberId", MemberId);
                HT.Add("BankCode", BankCode);
                HT.Add("BankName", BankName);
                HT.Add("BranchId", BranchId);
                HT.Add("BranchName", BranchName);
                HT.Add("AccountNumber", AccountNumber);
                HT.Add("CheckPrimary", CheckPrimary);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                dt = obj.GetDataFromStoredProcedure("sp_UserBankDetail_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_UserBankDetail_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }


        public DataTable GetList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Name", Name);
                HT.Add("MemberId", MemberId);
                HT.Add("BankCode", BankCode);
                HT.Add("BankName", BankName);
                HT.Add("BranchId", BranchId);
                HT.Add("BranchName", BranchName);
                HT.Add("AccountNumber", AccountNumber);
                HT.Add("CheckPrimary", CheckPrimary);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                dt = obj.GetDataFromStoredProcedure("sp_UserBankDetail_Get", HT);
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