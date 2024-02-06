using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    public class Commissions
    {
        private long _Id;
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
        }
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }
        private decimal _FixedCommission;
        public decimal FixedCommission
        {
            get { return _FixedCommission; }
            set { _FixedCommission = value; }
        }
        private decimal _PercentageCommission;
        public decimal PercentageCommission
        {
            get { return _PercentageCommission; }
            set { _PercentageCommission = value; }
        }
        private decimal _MinimumAmountSC = 0;
        public decimal MinimumAmountSC
        {
            get { return _MinimumAmountSC; }
            set { _MinimumAmountSC = value; }
        }
        private decimal _MaximumAmountSC = 0;
        public decimal MaximumAmountSC
        {
            get { return _MaximumAmountSC; }
            set { _MaximumAmountSC = value; }
        }
        private decimal _PercentageRewardPoints;
        public decimal PercentageRewardPoints
        {
            get { return _PercentageRewardPoints; }
            set { _PercentageRewardPoints = value; }
        }
        private decimal _PercentageRewardPointsDebit;
        public decimal PercentageRewardPointsDebit
        {
            get { return _PercentageRewardPointsDebit; }
            set { _PercentageRewardPointsDebit = value; }
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
        private decimal _MaximumAllowed = 0;
        public decimal MaximumAllowed
        {
            get { return _MaximumAllowed; }
            set { _MaximumAllowed = value; }
        }
        private decimal _MinimumAllowed = 0;
        public decimal MinimumAllowed
        {
            get { return _MinimumAllowed; }
            set { _MinimumAllowed = value; }
        }
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        private decimal _MaximumAllowedSC = 0;
        public decimal MaximumAllowedSC
        {
            get { return _MaximumAllowedSC; }
            set { _MaximumAllowedSC = value; }
        }
        private decimal _MinimumAllowedSC = 0;
        public decimal MinimumAllowedSC
        {
            get { return _MinimumAllowedSC; }
            set { _MinimumAllowedSC = value; }
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

        //Running
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
                HT.Add("Id", Id);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("MinimumAmountSC", MinimumAmountSC);
                HT.Add("MaximumAmountSC", MaximumAmountSC);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("ServiceId", ServiceId);
                HT.Add("GenderType", GenderType);
                HT.Add("KycType", KycType);
                HT.Add("Running", Running);
                HT.Add("Scheduled", Scheduled);
                HT.Add("Expired", Expired);
                dt = obj.GetDataFromStoredProcedure("sp_Commission_Datatable", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }


    }
}