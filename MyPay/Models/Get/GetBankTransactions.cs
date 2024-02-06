using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetBankTransactions : CommonGet
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //MemberName 
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Sign
        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //VendorTransactionId
        private string _VendorTransactionId = string.Empty;
        public string VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }

        //ParentTransactionId
        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }

        //CurrentBalance
        private decimal _CurrentBalance = 0;
        public decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
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

        //VendorType
        private Int32 _VendorType = 0;
        public Int32 VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }


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
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("MemberId", MemberId);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("Status", Status);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                HT.Add("Reference", Reference);
                HT.Add("VendorType", VendorType);
                dt = obj.GetDataFromStoredProcedure("sp_BankTransactions_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_BankTransactions_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        dt.Rows[0]["AmountSum"] = dtCounter.Rows[0]["AmountSum"].ToString();
                        dt.Rows[0]["TotalCredit"] = dtCounter.Rows[0]["TotalCredit"].ToString();
                        dt.Rows[0]["TotalDebit"] = dtCounter.Rows[0]["TotalDebit"].ToString();
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
                HT.Add("MemberId", MemberId);
                HT.Add("MemberName", MemberName);
                HT.Add("Amount", Amount);
                HT.Add("Sign", Sign);
                HT.Add("Type", Type);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("Status", Status);
                HT.Add("VendorTransactionId", VendorTransactionId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);                
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                dt = obj.GetDataFromStoredProcedure("sp_BankTransactions_Get", HT);
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