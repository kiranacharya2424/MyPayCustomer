using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{

    public class AddRemittanceUserCurrencies : CommonAdd
    {
        bool DataRecieved = false;

        #region "Properties" 
        private String _MerchantUniqueId = string.Empty;
        public String MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }
        private String _OrganizationName = string.Empty;
        public String OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private String _ContactName = string.Empty;
        public String ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        private String _EmailID = string.Empty;
        public String EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }
        private String _ContactNo = string.Empty;
        public String ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        private String _UserName = string.Empty;
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private String _CreatedDatedt = string.Empty;
        public String CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        private Int64 _FilterTotalCount = 0;
        public Int64 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }
        private Int64 _CurrencyID = 0;
        public Int64 CurrencyID
        {
            get { return _CurrencyID; }
            set { _CurrencyID = value; }
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

        private String _CurrencyName = string.Empty;
        public String CurrencyName
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }
        private String _Image = string.Empty;
        public String Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        private String _CountryName = string.Empty;
        public String CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private decimal _ODL = 0;
        public decimal ODL
        {
            get { return _ODL; }
            set { _ODL = value; }
        }

        private decimal _Prefund = 0;
        public decimal Prefund
        {
            get { return _Prefund; }
            set { _Prefund = value; }
        }

        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private decimal _Fees = 0;
        public decimal Fees
        {
            get { return _Fees; }
            set { _Fees = value; }
        }

        private decimal _ConvertedAmount = 0;
        public decimal ConvertedAmount
        {
            get { return _ConvertedAmount; }
            set { _ConvertedAmount = value; }
        }

        private decimal _ConversionRate = 0;
        public decimal ConversionRate
        {
            get { return _ConversionRate; }
            set { _ConversionRate = value; }
        }

        private int _WalletType = 0;
        public int WalletType
        {
            get { return _WalletType; }
            set { _WalletType = value; }
        }

        private string _WalletTypeName = string.Empty;
        public string WalletTypeName
        {
            get { return _WalletTypeName; }
            set { _WalletTypeName = value; }
        }

        //CurrencySymbol
        private string _CurrencySymbol = string.Empty;
        public string CurrencySymbol
        {
            get { return _CurrencySymbol; }
            set { _CurrencySymbol = value; }
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceUserCurrencies_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceUserCurrencies_Update", HT);
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
            HT.Add("MerchantUniqueId", MerchantUniqueId);
            HT.Add("OrganizationName", OrganizationName);
            HT.Add("ContactName", ContactName);
            HT.Add("EmailID", EmailID);
            HT.Add("ContactNo", ContactNo);
            HT.Add("UserName", UserName);
            HT.Add("CurrencyID", CurrencyID);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("Type", Type);
            HT.Add("ODL", ODL);
            HT.Add("Prefund", Prefund);
            HT.Add("IsActive", IsActive);
            HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            HT.Add("IsDeleted", IsDeleted);
            HT.Add("Amount", Amount);
            HT.Add("Fees", Fees);
            HT.Add("ConversionRate", ConversionRate);
            HT.Add("ConvertedAmount", ConvertedAmount);
            HT.Add("WalletType", WalletType);
            HT.Add("CurrencySymbol", CurrencySymbol);
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
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactName", ContactName);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("UserName", UserName);
                HT.Add("CurrencyID", CurrencyID);
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
                HT.Add("CurrencySymbol", CurrencySymbol);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceUserCurrencies_Get", HT);
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
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactName", ContactName);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("UserName", UserName);
                HT.Add("CurrencyID", CurrencyID);
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
                HT.Add("CurrencySymbol", CurrencySymbol);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceUserCurrencies_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MerchantUniqueId = Convert.ToString(dt.Rows[0]["MerchantUniqueId"].ToString());
                    OrganizationName = Convert.ToString(dt.Rows[0]["OrganizationName"].ToString());
                    ContactName = Convert.ToString(dt.Rows[0]["ContactName"].ToString());
                    EmailID = Convert.ToString(dt.Rows[0]["EmailID"].ToString());
                    ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                    UserName = Convert.ToString(dt.Rows[0]["UserName"].ToString());
                    CurrencyID = Convert.ToInt64(dt.Rows[0]["CurrencyID"].ToString());
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
                    ODL = Convert.ToDecimal(dt.Rows[0]["ODL"].ToString());
                    Prefund = Convert.ToDecimal(dt.Rows[0]["Prefund"].ToString());
                    Type = Convert.ToInt32(dt.Rows[0]["Type"].ToString());
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    Fees = Convert.ToDecimal(dt.Rows[0]["Fees"].ToString());
                    ConvertedAmount = Convert.ToDecimal(dt.Rows[0]["ConvertedAmount"].ToString());
                    ConversionRate = Convert.ToDecimal(dt.Rows[0]["ConversionRate"].ToString());
                    WalletType = Convert.ToInt32(dt.Rows[0]["WalletType"].ToString());
                    CurrencySymbol = dt.Rows[0]["CurrencySymbol"].ToString();
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
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("OrganizationName", OrganizationName);
                HT.Add("ContactName", ContactName);
                HT.Add("EmailID", EmailID);
                HT.Add("ContactNo", ContactNo);
                HT.Add("UserName", UserName);
                HT.Add("CurrencyID", CurrencyID);
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

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceUserCurrencies_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RemittanceUserCurrencies_DatatableCounter", HT);
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