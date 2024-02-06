using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    public class Coupons
    {
        private long _Id;
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _GenderType = 0;
        public int GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }
        private int _KycType = 0;
        public int KycType
        {
            get { return _KycType; }
            set { _KycType = value; }
        }
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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


        private DateTime _ScheduledDate = System.DateTime.UtcNow;
        public DateTime ScheduledDate
        {
            get { return _ScheduledDate; }
            set { _ScheduledDate = value; }
        }
        private int _CouponType = 0;
        public int CouponType
        {
            get { return _CouponType; }
            set { _CouponType = value; }
        }


        private int _KycStatus = 0;
        public int KycStatus
        {
            get { return _KycStatus; }
            set { _KycStatus = value; }
        }



        private int _GenderStatus = 0;
        public int GenderStatus
        {
            get { return _GenderStatus; }
            set { _GenderStatus = value; }
        }



        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        private decimal _CouponPercentage = 0;
        public decimal CouponPercentage
        {
            get { return _CouponPercentage; }
            set { _CouponPercentage = value; }
        }


        private int _CouponsCount = 0;
        public int CouponsCount
        {
            get { return _CouponsCount; }
            set { _CouponsCount = value; }
        }

        private int _CouponsUsedCount = 0;
        public int CouponsUsedCount
        {
            get { return _CouponsUsedCount; }
            set { _CouponsUsedCount = value; }
        }
        private int _ServiceId;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _ServiceName = string.Empty;
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }
        private int _Type;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private System.DateTime _CreatedDate = System.DateTime.UtcNow;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private System.DateTime _UpdatedDate = System.DateTime.UtcNow;
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }
        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckDelete
        private int _ScheduleStatus = 0;// (int)clsData.BooleanValue.Both;
        public int ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }
        //CreatedBy
        private int _CreatedBy = 0;// (int)clsData.BooleanValue.Both;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
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

        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }

        //CreatedByName
        private string _CreatedByName = string.Empty;
        public string CreatedByName
        {
            get { return _CreatedByName; }
            set { _CreatedByName = value; }
        }

        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("ServiceId", ServiceId);
                HT.Add("GenderStatus", GenderStatus);
                HT.Add("KycStatus", KycStatus);
                HT.Add("FromDate", FromDate);
                HT.Add("ToDate", ToDate);
                HT.Add("Status", Status);
                HT.Add("Type", Type);
                HT.Add("CouponType", CouponType);
                HT.Add("Running", Running);
                HT.Add("Scheduled", Scheduled);
                HT.Add("Expired", Expired);
                dt = obj.GetDataFromStoredProcedure("sp_Coupons_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }


    }
}