using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddRemittanceConversionRateSettings : CommonAdd
    {
        bool DataRecieved = false;
        #region "Properties" 


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
        //ScheduleStatus
        private string _ScheduleStatus = String.Empty;
        public string ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }

        private Int64 _SourceCurrencyId = 0;
        public Int64 SourceCurrencyId
        {
            get { return _SourceCurrencyId; }
            set { _SourceCurrencyId = value; }
        }

        private string _SourceCurrency = string.Empty;
        public string SourceCurrency
        {
            get { return _SourceCurrency; }
            set { _SourceCurrency = value; }
        }
        private Int64 _DestinationCurrencyId = 0;
        public Int64 DestinationCurrencyId
        {
            get { return _DestinationCurrencyId; }
            set { _DestinationCurrencyId = value; }
        }

        private string _DestinationCurrency = string.Empty;
        public string DestinationCurrency
        {
            get { return _DestinationCurrency; }
            set { _DestinationCurrency = value; }
        }
        private Decimal _ConversionRate = 0;
        public Decimal ConversionRate
        {
            get { return _ConversionRate; }
            set { _ConversionRate = value; }
        }
        private TimeSpan _FromDate = System.DateTime.UtcNow.TimeOfDay;
        public TimeSpan FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private string _CheckFromDate = string.Empty;
        public string CheckFromDate
        {
            get { return _CheckFromDate; }
            set { _CheckFromDate = value; }
        }
        private TimeSpan _ToDate = System.DateTime.UtcNow.TimeOfDay;
        public TimeSpan ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private string _CheckToDate = string.Empty;
        public string CheckToDate
        {
            get { return _CheckToDate; }
            set { _CheckToDate = value; }
        }
        private string _FromDateDT = string.Empty;
        public string FromDateDT
        {
            get { return _FromDateDT; }
            set { _FromDateDT = value; }
        }
        private string _ToDateDT = string.Empty;
        public string ToDateDT
        {
            get { return _ToDateDT; }
            set { _ToDateDT = value; }
        }
        private Int32 _Take = 0;
        public Int32 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        private Int32 _Skip = 0;
        public Int32 Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
        private Int32 _CheckDelete = 2;
        public Int32 CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }
        private Int32 _CheckActive = 2;
        public Int32 CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        private Int32 _CheckApprovedByAdmin = 2;
        public Int32 CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

        private String _UpdatedDateDt = string.Empty;
        public String UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        private String _CreatedDateDt = string.Empty;
        public String CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        private String _CheckCreatedDate = string.Empty;
        public String CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private String _StartDate = string.Empty;
        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _EndDate = string.Empty;
        public String EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        #endregion

        #region "Add Delete Update Methods" 
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceConversionRateSettings_AddNew", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool Update()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceConversionRateSettings_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("SourceCurrencyId", SourceCurrencyId);
            HT.Add("DestinationCurrencyId", DestinationCurrencyId);
            HT.Add("SourceCurrency", SourceCurrency);
            HT.Add("DestinationCurrency", DestinationCurrency);
            HT.Add("ConversionRate", ConversionRate);
            HT.Add("FromDate", FromDate);
            HT.Add("ToDate", ToDate);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("IsActive", IsActive);
            HT.Add("IsDeleted", IsDeleted);
            HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            return HT;
        }
        #endregion

        #region "Get Methods" 
        public System.Data.DataTable GetList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SourceCurrencyId", SourceCurrencyId);
                HT.Add("DestinationCurrencyId", DestinationCurrencyId);
                HT.Add("ConversionRate", ConversionRate);
                HT.Add("CheckFromDate", CheckFromDate);
                HT.Add("CheckToDate", CheckToDate);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceConversionRateSettings_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public bool GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SourceCurrencyId", SourceCurrencyId);
                HT.Add("DestinationCurrencyId", DestinationCurrencyId);
                HT.Add("ConversionRate", ConversionRate);
                HT.Add("CheckFromDate", CheckFromDate);
                HT.Add("CheckToDate", CheckToDate);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceConversionRateSettings_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SourceCurrencyId = Convert.ToInt64(dt.Rows[0]["SourceCurrencyId"].ToString());
                    DestinationCurrencyId = Convert.ToInt64(dt.Rows[0]["DestinationCurrencyId"].ToString());
                    ConversionRate = Convert.ToDecimal(dt.Rows[0]["ConversionRate"].ToString());
                    FromDate = TimeSpan.Parse(dt.Rows[0]["FromDate"].ToString());
                    ToDate = TimeSpan.Parse(dt.Rows[0]["ToDate"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = Convert.ToString(dt.Rows[0]["CreatedByName"].ToString());
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    UpdatedBy = Convert.ToInt64(dt.Rows[0]["UpdatedBy"].ToString());
                    UpdatedByName = Convert.ToString(dt.Rows[0]["UpdatedByName"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    SourceCurrency = dt.Rows[0]["SourceCurrency"].ToString();
                    DestinationCurrency = dt.Rows[0]["DestinationCurrency"].ToString();

                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Data.DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("SourceCurrencyId", SourceCurrencyId);
                HT.Add("DestinationCurrencyId", DestinationCurrencyId);
                HT.Add("ConversionRate", ConversionRate);
                HT.Add("CheckFromDate", CheckFromDate);
                HT.Add("CheckToDate", CheckToDate);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceConversionRateSettings_DataTable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RemittanceConversionRateSettings_DataTableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public float CountCommissionCheck(TimeSpan FromDate, TimeSpan ToDate, Int64 Id, Int64 SourceCurrencyId, Int64 DestinationCurrencyId)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Id) from RemittanceConversionRateSettings with(nolock) where Id  != " + Id + " and SourceCurrencyId  = " + SourceCurrencyId + " and DestinationCurrencyId  = " + DestinationCurrencyId + " and IsActive=1 and IsDeleted=0 and (convert(varchar(5), FromDate, 108) between cast(('" + FromDate + "') as time) and cast(('" + ToDate + "') as time) OR convert(varchar(5), ToDate, 108) between cast(('" + FromDate + "') as time) and cast(('" + ToDate + "') as time))";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (float.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        #endregion
    }
}