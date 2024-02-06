using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetDepositOrders:CommonGet
    {
        #region "Properties"

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Currency
        private string _Currency = string.Empty;
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }

        //RefferalsId
        private string _RefferalsId = string.Empty;
        public string RefferalsId
        {
            get { return _RefferalsId; }
            set { _RefferalsId = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
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

        #endregion

        #region "Get Methods"
        public DataTable GetList()
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("MemberId", MemberId);
                HT.Add("Amount", Amount);
                HT.Add("Type", Type);
                HT.Add("Status", Status);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Today", Today);
                HT.Add("Weekly", Weekly);
                HT.Add("Monthly", Monthly);
                dt = obj.GetDataFromStoredProcedure("sp_DepositOrders_Get", HT);
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