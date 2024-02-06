using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddProviderLogoList
    {
        public bool DataRecieved = false;

        #region "Properties"

        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        //WebURL
        private string _WebURL = string.Empty;
        public string WebURL
        {
            get { return _WebURL; }
            set { _WebURL = value; }
        }
        //ProviderName
        private string _ProviderName = string.Empty;
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }

        //ProviderTypeId
        private string _ProviderTypeId = string.Empty;
        public string ProviderTypeId
        {
            get { return _ProviderTypeId; }
            set { _ProviderTypeId = value; }
        }

        //ProviderServiceCategoryId
        private string _ProviderServiceCategoryId = string.Empty;
        public string ProviderServiceCategoryId
        {
            get { return _ProviderServiceCategoryId; }
            set { _ProviderServiceCategoryId = value; }
        }

        //ProviderServiceName
        private string _ProviderServiceName = string.Empty;
        public string ProviderServiceName
        {
            get { return _ProviderServiceName; }
            set { _ProviderServiceName = value; }
        }

        //CashbackPercentage
        private string _CashbackPercentage = string.Empty;
        public string CashbackPercentage
        {
            get { return _CashbackPercentage; }
            set { _CashbackPercentage = value; }
        }

        //ProviderLogoURL
        private string _ProviderLogoURL = string.Empty;
        public string ProviderLogoURL
        {
            get { return _ProviderLogoURL; }
            set { _ProviderLogoURL = value; }
        }
        //IsActive
        private int _IsActive = 0;
        public int IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        //ServiceAPIName
        private string _ServiceAPIName = string.Empty;
        public string ServiceAPIName
        {
            get { return _ServiceAPIName; }
            set { _ServiceAPIName = value; }
        }

        //MPCoinsCashback
        private string _MPCoinsCashback = string.Empty;
        public string MPCoinsCashback
        {
            get { return _MPCoinsCashback; }
            set { _MPCoinsCashback = value; }
        }
        private bool _IsServiceDown = false;
        public bool IsServiceDown
        {
            get { return _IsServiceDown; }
            set { _IsServiceDown = value; }
        }

        private int _CheckIsServiceDown = 2;
        public int CheckIsServiceDown
        {
            get { return _CheckIsServiceDown; }
            set { _CheckIsServiceDown = value; }
        }
        private int _CheckActive = 2;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
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
        private int _SortOrder = 0;
        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }

        private int _ParentID = 0;
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        #endregion

        #region "Get Methods" 
        public System.Data.DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("PagingSize", PagingSize);
                HT.Add("SearchText", SearchText);
                HT.Add("ProviderServiceCategoryId", ProviderServiceCategoryId);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckIsServiceDown", CheckIsServiceDown);
                HT.Add("Id", Id);
                dt = obj.GetDataFromStoredProcedure("sp_ProviderLogosList_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public bool ProviderServiceCategoryListUpdate()
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("ProviderServiceCategoryId", ProviderServiceCategoryId); 
                HT.Add("IsServiceDown", IsServiceDown);  
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_ProviderServiceCategoryList_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }


        #endregion
    }
}