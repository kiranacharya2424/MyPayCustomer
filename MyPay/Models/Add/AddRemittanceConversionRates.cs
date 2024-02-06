using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{

    public class AddRemittanceConversionRates : CommonAdd
    {
        bool DataRecieved = false;
        #region "Properties" 
        private String _SourceCurrencyUniqueId = string.Empty;
        public String SourceCurrencyUniqueId
        {
            get { return _SourceCurrencyUniqueId; }
            set { _SourceCurrencyUniqueId = value; }
        }
        private String _DestinationCurrencyUniqueId = string.Empty;
        public String DestinationCurrencyUniqueId
        {
            get { return _DestinationCurrencyUniqueId; }
            set { _DestinationCurrencyUniqueId = value; }
        }

        private Decimal _ConversionRate = 0;
        public Decimal ConversionRate
        {
            get { return _ConversionRate; }
            set { _ConversionRate = value; }
        }
        private Decimal _InverseRate = 0;
        public Decimal InverseRate
        {
            get { return _InverseRate; }
            set { _InverseRate = value; }
        }
        private Decimal _Markup = 0;
        public Decimal Markup
        {
            get { return _Markup; }
            set { _Markup = value; }
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
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _CreatedDatedt = string.Empty;
        public String CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        private String _SourceCurrencyName = string.Empty;
        public String SourceCurrencyName
        {
            get { return _SourceCurrencyName; }
            set { _SourceCurrencyName = value; }
        }

        private String _DestinationCurrencyName = string.Empty;
        public String DestinationCurrencyName
        {
            get { return _DestinationCurrencyName; }
            set { _DestinationCurrencyName = value; }
        }

        private String _UniqueId = string.Empty;
        public String UniqueId
        {
            get { return _UniqueId; }
            set { _UniqueId = value; }
        }

        private bool _IsAutoUpdated = false;
        public bool IsAutoUpdated
        {
            get { return _IsAutoUpdated; }
            set { _IsAutoUpdated = value; }
        }
        private Int32 _CheckAutoUpdated = 2;
        public Int32 CheckAutoUpdated
        {
            get { return _CheckAutoUpdated; }
            set { _CheckAutoUpdated = value; }
        }

        private String _DestinationCurrencyMultiple = string.Empty;
        public String DestinationCurrencyMultiple
        {
            get { return _DestinationCurrencyMultiple; }
            set { _DestinationCurrencyMultiple = value; }
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceConversionRates_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceConversionRates_Update", HT);
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
            HT.Add("SourceCurrencyUniqueId", SourceCurrencyUniqueId);
            HT.Add("DestinationCurrencyUniqueId", DestinationCurrencyUniqueId);
            HT.Add("ConversionRate", ConversionRate);
            HT.Add("InverseRate", InverseRate);
            HT.Add("Markup", Markup);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("IsActive", IsActive);
            HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            HT.Add("SourceCurrencyName", SourceCurrencyName);
            HT.Add("DestinationCurrencyName", DestinationCurrencyName);
            HT.Add("UniqueId", UniqueId);
            HT.Add("IsAutoUpdated", IsAutoUpdated);

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
                HT.Add("SourceCurrencyUniqueId", SourceCurrencyUniqueId);
                HT.Add("DestinationCurrencyUniqueId", DestinationCurrencyUniqueId);
                HT.Add("ConversionRate", ConversionRate);
                HT.Add("InverseRate", InverseRate);
                HT.Add("Markup", Markup);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
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
                HT.Add("CheckAutoUpdated", CheckAutoUpdated);
                HT.Add("UniqueId", UniqueId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceConversionRates_Get", HT);
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
                HT.Add("SourceCurrencyUniqueId", SourceCurrencyUniqueId);
                HT.Add("DestinationCurrencyUniqueId", DestinationCurrencyUniqueId);
                HT.Add("SourceCurrencyName", SourceCurrencyName);
                HT.Add("DestinationCurrencyName", DestinationCurrencyName);
                HT.Add("ConversionRate", ConversionRate);
                HT.Add("InverseRate", InverseRate);
                HT.Add("Markup", Markup);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
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
                HT.Add("CheckAutoUpdated", CheckAutoUpdated);
                HT.Add("UniqueId", UniqueId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceConversionRates_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SourceCurrencyUniqueId = Convert.ToString(dt.Rows[0]["SourceCurrencyUniqueId"].ToString());
                    DestinationCurrencyUniqueId = Convert.ToString(dt.Rows[0]["DestinationCurrencyUniqueId"].ToString());
                    ConversionRate = Convert.ToDecimal(dt.Rows[0]["ConversionRate"].ToString());
                    InverseRate = Convert.ToDecimal(dt.Rows[0]["InverseRate"].ToString());
                    Markup = Convert.ToDecimal(dt.Rows[0]["Markup"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = Convert.ToString(dt.Rows[0]["CreatedByName"].ToString());
                    Sno = Convert.ToString(dt.Rows[0]["Sno"].ToString());
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    UpdatedBy = Convert.ToInt64(dt.Rows[0]["UpdatedBy"].ToString());
                    UpdatedByName = Convert.ToString(dt.Rows[0]["UpdatedByName"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    SourceCurrencyName = dt.Rows[0]["SourceCurrencyName"].ToString();
                    DestinationCurrencyName = dt.Rows[0]["DestinationCurrencyName"].ToString();
                    UniqueId = dt.Rows[0]["UniqueId"].ToString();
                    IsAutoUpdated = Convert.ToBoolean(dt.Rows[0]["IsAutoUpdated"].ToString());
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
                HT.Add("SourceCurrencyUniqueId", SourceCurrencyUniqueId);
                HT.Add("DestinationCurrencyUniqueId", DestinationCurrencyUniqueId);
                HT.Add("ConversionRate", ConversionRate);
                HT.Add("InverseRate", InverseRate);
                HT.Add("Markup", Markup);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("Sno", Sno);
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
                HT.Add("DestinationCurrencyMultiple", DestinationCurrencyMultiple);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceConversionRates_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RemittanceConversionRates_DatatableCounter", HT);
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
        #endregion

    }


}