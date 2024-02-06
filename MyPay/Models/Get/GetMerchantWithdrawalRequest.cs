using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetMerchantWithdrawalRequest:CommonGet
    {
        #region "Properties"

        //MerchantUniqueId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        private string _BankId = string.Empty;
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }

        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
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

        //OrderId
        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //WithdrawalRequestType
        private int _WithdrawalRequestType = -1;
        public int WithdrawalRequestType
        {
            get { return _WithdrawalRequestType; }
            set { _WithdrawalRequestType = value; }
        }

        //CheckWithdrawalApproveByAdmin
        private int _CheckWithdrawalApproveByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckWithdrawalApproveByAdmin
        {
            get { return _CheckWithdrawalApproveByAdmin; }
            set { _CheckWithdrawalApproveByAdmin = value; }
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
                HT.Add("MerchantId", MerchantId);
                HT.Add("BankId", BankId);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Amount", Amount);
                HT.Add("Status", Status);
                HT.Add("WithdrawalRequestType", WithdrawalRequestType);
                HT.Add("CheckWithdrawalApproveByAdmin", CheckWithdrawalApproveByAdmin);
                dt = obj.GetDataFromStoredProcedure("sp_MerchantWithdrawalRequest_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_MerchantWithdrawalRequest_DatatableCounter", HT);
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

        #endregion
    }
}