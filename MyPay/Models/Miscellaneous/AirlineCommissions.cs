using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services.Description;

namespace MyPay.Models.Miscellaneous
{
    public class AirlineCommissions
    {
        private long _Id;
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _Flag;
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        private int _FromSectorId;
        public int FromSectorId
        {
            get { return _FromSectorId; }
            set { _FromSectorId = value; }
        }
        private int _ToSectorId;
        public int ToSectorId
        {
            get { return _ToSectorId; }
            set { _ToSectorId = value; }
        }
        private int _AirlineId;
        public int AirlineId
        {
            get { return _AirlineId; }
            set { _AirlineId = value; }
        }
        private string _AirlineName;
        public string AirlineName
        {
            get { return _AirlineName; }
            set { _AirlineName = value; }
        }
        private int _AirlineClassId;
        public int AirlineClassId
        {
            get { return _AirlineClassId; }
            set { _AirlineClassId = value; }
        }
        private string _ClassName;
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        private decimal _Cashback_Percentage;
        public decimal Cashback_Percentage
        {
            get { return _Cashback_Percentage; }
            set { _Cashback_Percentage = value; }
        }
        private decimal _MPCoinsCredit;
        public decimal MPCoinsCredit
        {
            get { return _MPCoinsCredit; }
            set { _MPCoinsCredit = value; }
        }
        private decimal _MPCoinsDebit;
        public decimal MPCoinsDebit
        {
            get { return _MPCoinsDebit; }
            set { _MPCoinsDebit = value; }
        }
        private DateTime _FromDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private DateTime _ToDate;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private string _FromDateDT;
        public string FromDateDT
        {
            get { return _FromDateDT; }
            set { _FromDateDT = value; }
        }
        private string _ToDateDT;
        public string ToDateDT
        {
            get { return _ToDateDT; }
            set { _ToDateDT = value; }
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
        private decimal _MaximumCashbackAllowed = 0;
        public decimal MaximumCashbackAllowed
        {
            get { return _MaximumCashbackAllowed; }
            set { _MaximumCashbackAllowed = value; }
        }
        private decimal _MinimumCashbackAllowed = 0;
        public decimal MinimumCashbackAllowed
        {
            get { return _MinimumCashbackAllowed; }
            set { _MinimumCashbackAllowed = value; }
        }
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        private bool _IsCashbackPerTicket;
        public bool IsCashbackPerTicket
        {
            get { return _IsCashbackPerTicket; }
            set { _IsCashbackPerTicket = value; }
        }
        private decimal _MinServiceCharge;
        public decimal MinServiceCharge
        {
            get { return _MinServiceCharge; }
            set { _MinServiceCharge = value; }
        }
        private decimal _MaxServiceCharge;
        public decimal MaxServiceCharge
        {
            get { return _MaxServiceCharge; }
            set { _MaxServiceCharge = value; }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private bool _IsDeleted;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
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
                HT.Add("AirlineId", AirlineId);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckDelete", CheckDelete);

                //HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                //HT.Add("CreatedBy", CreatedBy);
                //HT.Add("Running", Running);
                //HT.Add("Scheduled", Scheduled);
                //HT.Add("Expired", Expired);
                dt = obj.GetDataFromStoredProcedure("sp_AirlinesCommission", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }
        public CommonDBResonse AddAgentCommission()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                HT.Add("@flag", "I");
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_AddAirlineCommision", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public CommonDBResonse UpdateAirlineCommission()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Flag = "U";
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_AddAirlineCommision", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public CommonDBResonse UpdateStatusAirlineCommission()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Flag = "US";
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_AddAirlineCommision", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public CommonDBResonse DeleteAirlineCommission()
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                Flag = "D";
                System.Collections.Hashtable HT = SetObject();
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_AddAirlineCommision", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            if(Flag == "U")
            {
                HT.Add("Flag", "U");
                HT.Add("Id", Id);
                HT.Add("FromSectorId", FromSectorId);
                HT.Add("ToSectorId", ToSectorId);
                HT.Add("AirlineId", AirlineId);
                HT.Add("AirlineClassId", AirlineClassId);
                HT.Add("KycType", KycType);
                HT.Add("GenderType", GenderType);
                HT.Add("Cashback_Percentage", Cashback_Percentage);
                HT.Add("MPCoinsDebit", MPCoinsDebit);
                HT.Add("MPCoinsCredit", MPCoinsCredit);
                HT.Add("ServiceCharge", ServiceCharge);
                HT.Add("IsCashbackPerTicket", IsCashbackPerTicket);
                HT.Add("MinServiceCharge", MinServiceCharge);
                HT.Add("MaxServiceCharge", MaxServiceCharge);
                HT.Add("MinimumCashbackAllowed", MinimumCashbackAllowed);
                HT.Add("MaximumCashbackAllowed", MaximumCashbackAllowed);
                HT.Add("FromDate", FromDate);
                HT.Add("ToDate", ToDate);
            }
            else if(Flag == "US")
            {
                HT.Add("Flag", "US");
                HT.Add("Id", Id);
                HT.Add("IsActive", IsActive);
            }
            else if(Flag == "D")
            {
                HT.Add("Flag", "D");
                HT.Add("Id", Id);
                HT.Add("IsDeleted", IsDeleted);
            }
            return HT;
        }
    }
}