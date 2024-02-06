using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{

    public class AddRemittanceDestinationCurrencyList : CommonAdd
    {
        bool DataRecieved = false;
        #region "Properties" 
        private String _Symbol = string.Empty;
        public String Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
        }
        private String _CurrencyName = string.Empty;
        public String CurrencyName
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }
        private String _UniqueId = string.Empty;
        public String UniqueId
        {
            get { return _UniqueId; }
            set { _UniqueId = value; }
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
        private Int64 _CurrencyId = 0;
        public Int64 CurrencyId
        {
            get { return _CurrencyId; }
            set { _CurrencyId = value; }
        }

        private string _SourceCurrencyId= string.Empty;
        public string SourceCurrencyId
        {
            get { return _SourceCurrencyId; }
            set { _SourceCurrencyId = value; }
        }
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceDestinationCurrencyList_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceDestinationCurrencyList_Update", HT);
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
            HT.Add("Symbol", Symbol);
            HT.Add("CurrencyId", CurrencyId);
            HT.Add("CurrencyName", CurrencyName);
            HT.Add("UniqueId", UniqueId);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("IsActive", IsActive);
            HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            HT.Add("IsDeleted", IsDeleted);
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
                HT.Add("Symbol", Symbol);
                HT.Add("CurrencyName", CurrencyName);
                HT.Add("UniqueId", UniqueId);
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
                HT.Add("CurrencyId", CurrencyId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceDestinationCurrencyList_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public List<AddRemittanceDestinationCurrencyList> GetRecordList()
        {
            List<AddRemittanceDestinationCurrencyList> objDestinationCurrencyList = new List<AddRemittanceDestinationCurrencyList>();
            AddRemittanceDestinationCurrencyList objDestinationCurrency = new AddRemittanceDestinationCurrencyList();

            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("Id", Id);
                HT.Add("Symbol", Symbol);
                HT.Add("CurrencyName", CurrencyName);
                HT.Add("UniqueId", UniqueId);
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
                HT.Add("CurrencyId", CurrencyId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceDestinationCurrencyList_Get", HT);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objDestinationCurrency = new AddRemittanceDestinationCurrencyList();
                    objDestinationCurrency.CurrencyName = dt.Rows[i]["CurrencyName"].ToString();
                    objDestinationCurrency.CurrencyId = Convert.ToInt64(dt.Rows[i]["CurrencyId"].ToString());
                    objDestinationCurrency.Symbol = dt.Rows[i]["Symbol"].ToString();
                    objDestinationCurrency.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString());
                    objDestinationCurrency.UpdatedDate = Convert.ToDateTime(dt.Rows[i]["UpdatedDate"].ToString());
                    objDestinationCurrency.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                    objDestinationCurrency.Id = Convert.ToInt64(dt.Rows[i]["Id"].ToString());
                    objDestinationCurrencyList.Add(objDestinationCurrency);
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return objDestinationCurrencyList;
        }


        public bool GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("Symbol", Symbol);
                HT.Add("CurrencyName", CurrencyName);
                HT.Add("UniqueId", UniqueId);
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
                HT.Add("CurrencyId", CurrencyId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceDestinationCurrencyList_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Symbol = Convert.ToString(dt.Rows[0]["Symbol"].ToString());
                    CurrencyName = Convert.ToString(dt.Rows[0]["CurrencyName"].ToString());
                    UniqueId = Convert.ToString(dt.Rows[0]["UniqueId"].ToString());
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
                    CurrencyId= Convert.ToInt64(dt.Rows[0]["CurrencyId"].ToString());
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
                HT.Add("Symbol", Symbol);
                HT.Add("CurrencyName", CurrencyName);
                HT.Add("UniqueId", UniqueId);
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

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceDestinationCurrencyList_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RemittanceDestinationCurrencyList_DatatableCounter", HT);
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

        public System.Data.DataTable GetRemittanceFilteredDestinationCurrencyList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();               
                HT.Add("SourceCurrencyId", SourceCurrencyId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceFilteredDestinationCurrencyList_DataTable", HT);
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