using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetCouponsHistory:CommonGet
    {
        #region "Properties"

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
        //CouponType
        private int _CouponType = 0;
        public int CouponType
        {
            get { return _CouponType; }
            set { _CouponType = value; }
        }

        //CouponId
        private int _CouponId = 0;
        public int CouponId
        {
            get { return _CouponId; }
            set { _CouponId = value; }
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
                HT.Add("FromDate", FromDate);
                HT.Add("ToDate", ToDate);
                HT.Add("GenderStatus", GenderStatus);
                HT.Add("KycStatus", KycStatus);
                HT.Add("Status", Status);
                HT.Add("CouponType", CouponType);
                HT.Add("CouponId", CouponId);
                HT.Add("Running", Running);
                HT.Add("Scheduled", Scheduled);
                HT.Add("Expired", Expired);
                dt = obj.GetDataFromStoredProcedure("sp_CouponsHistory_Datatable", HT);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

    }
}