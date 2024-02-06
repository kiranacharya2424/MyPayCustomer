using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetCouponsScratched : CommonGet
    {
        #region "Properties"

        //CouponCode
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //CouponType
        private int _CouponType = 0;
        public int CouponType
        {
            get { return _CouponType; }
            set { _CouponType = value; }
        }

        //CouponId
        private Int64 _CouponId = 0;
        public Int64 CouponId
        {
            get { return _CouponId; }
            set { _CouponId = value; }
        }
        //IsScratched
        private int _IsScratched = 2;
        public int IsScratched
        {
            get { return _IsScratched; }
            set { _IsScratched = value; }
        }
        //IsUsed
        private int _IsUsed = 2;
        public int IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }
        private int _IsExpired = 2;

        public int IsExpired
        {
            get { return _IsExpired; }
            set { _IsExpired = value; }
        }
        //KycStatus
        private int _KycStatus = 0;
        public int KycStatus
        {
            get { return _KycStatus; }
            set { _KycStatus = value; }
        }
        //GenderStatus
        private int _GenderStatus = 0;
        public int GenderStatus
        {
            get { return _GenderStatus; }
            set { _GenderStatus = value; }
        }

        //ServiceId
        private int _ServiceId = 0;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private string _FromDate = string.Empty;
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        private string _ToDate = string.Empty;
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        private string _Running = string.Empty;
        public string Running
        {
            get { return _Running; }
            set { _Running = value; }
        }

        //Scheduled
        private string _Scheduled = string.Empty;
        public string Scheduled
        {
            get { return _Scheduled; }
            set { _Scheduled = value; }
        }

        //Expired
        private string _Expired = string.Empty;
        public string Expired
        {
            get { return _Expired; }
            set { _Expired = value; }
        }

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        #endregion

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
                //HT.Add("Id", Id);
                //HT.Add("Take", Take);
                //HT.Add("Skip", Skip);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("ServiceId", ServiceId);
                HT.Add("IsScratched", IsScratched);
                HT.Add("IsUsed", IsUsed);
                HT.Add("FromDate", FromDate);
                HT.Add("ToDate", ToDate);
                HT.Add("IsExpired", IsExpired);
                HT.Add("GenderStatus", GenderStatus);
                HT.Add("KycStatus", KycStatus);
                HT.Add("Status", Status);
                HT.Add("CouponType", CouponType);
                HT.Add("CouponId", CouponId);
                HT.Add("CouponCode", CouponCode);
                HT.Add("MemberId", MemberId);
                HT.Add("Running", Running);
                HT.Add("Scheduled", Scheduled);
                HT.Add("Expired", Expired);
                HT.Add("TransactionId", TransactionId);
                dt = obj.GetDataFromStoredProcedure("sp_CouponsScratched_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_CouponsScratched_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

    }
}