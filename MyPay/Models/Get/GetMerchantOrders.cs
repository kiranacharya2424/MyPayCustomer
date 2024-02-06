using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetMerchantOrders:CommonGet
    {
        #region "Properties"

        //MerchantUniqueId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        private string _OrderToken = string.Empty;
        public string OrderToken
        {
            get { return _OrderToken; }
            set { _OrderToken = value; }
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

        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        private Int32 _Type = 0;
        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _MemberContactNumber = string.Empty;
        public string MemberContactNumber
        {
            get { return _MemberContactNumber; }
            set { _MemberContactNumber = value; }
        }

        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _TrackerId = string.Empty;
        public string TrackerId
        {
            get { return _TrackerId; }
            set { _TrackerId = value; }
        }

        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
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
                HT.Add("MerchantUniqueId", MerchantId);
                HT.Add("OrdersId",OrderId); 
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin); 
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("TrackerId", TrackerId);
                HT.Add("Status", Status);
                HT.Add("MemberName", MemberName);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("TransactionId", TransactionId);
                HT.Add("Sign", Sign);
                HT.Add("Type", Type);
                dt = obj.GetDataFromStoredProcedure("sp_MerchantOrders_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_MerchantOrders_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
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
        public DataTable GetDataDump()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("MerchantUniqueId", MerchantId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Type", Type);
                HT.Add("Status", Status);
                HT.Add("Sign", Sign);
                HT.Add("TransactionId", TransactionId);
                HT.Add("TrackerId", TrackerId);
                HT.Add("MemberName", MemberName);
                HT.Add("MemberContactNumber", MemberContactNumber);
                HT.Add("OrdersId", OrderId);

                dt = obj.GetDataFromStoredProcedure("sp_MerchantOrders_DumpGet", HT);

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