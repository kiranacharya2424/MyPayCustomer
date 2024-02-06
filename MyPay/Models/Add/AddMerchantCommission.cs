using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMerchantCommission : CommonAdd
    {
        #region "Enums"

        public enum CommissionStatus
        {
            Success = 1,
            Failure = 2,
            Cancelled = 3
        }

        public enum CommissionType
        {
            Service = 1,
            Other = 2
        }


        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }

        public enum Default
        {
            Regular = 0,
            Default = 1            
        }

        #endregion
        #region "Properties"
        //MerchantUniqueId
        private string _MerchantUniqueId = string.Empty;
        public string MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }

        //MinimumAmount
        [Required(ErrorMessage = "Minimum Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min slab Amount")]
        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
        }

        //MaximumAmount
        [Required(ErrorMessage = "Maximum  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max slab Amount")]
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }

        //FixedCommission
        [Required(ErrorMessage = "Fixed Commission  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Fixed Commission value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Fixed Amount")]
        private decimal _FixedCommission = 0;
        public decimal FixedCommission
        {
            get { return _FixedCommission; }
            set { _FixedCommission = value; }
        }
        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        //UpdatedDateDt
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        //PercentageCommission
        [Required(ErrorMessage = "Percentage  Commission  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Percentage Commission value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage Amount")]
        private decimal _PercentageCommission = 0;
        public decimal PercentageCommission
        {
            get { return _PercentageCommission; }
            set { _PercentageCommission = value; }
        }

        //PercentageRewardPoints
        [Required(ErrorMessage = "Percentage  RewardPoints  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter MP-Coins(%) Credit value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage RewardPoints")]
        private decimal _PercentageRewardPoints = 0;
        public decimal PercentageRewardPoints
        {
            get { return _PercentageRewardPoints; }
            set { _PercentageRewardPoints = value; }
        }

        //PercentageRewardPointsDebit
        [Required(ErrorMessage = "Percentage  RewardPoints  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter MP-Coins(%) Debit value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage RewardPoints")]
        private decimal _PercentageRewardPointsDebit = 0;
        public decimal PercentageRewardPointsDebit
        {
            get { return _PercentageRewardPointsDebit; }
            set { _PercentageRewardPointsDebit = value; }
        }

        //MaximumAllowed
        [Required(ErrorMessage = "Maximum Allowed Amount is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum Allowed value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max Allowed Amount")]
        private decimal _MaximumAllowed = 0;
        public decimal MaximumAllowed
        {
            get { return _MaximumAllowed; }
            set { _MaximumAllowed = value; }
        }

        //MinimumAllowed
        [Required(ErrorMessage = "Minimum Allowed Amount is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum Allowed value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min Allowed Amount")]
        private decimal _MinimumAllowed = 0;
        public decimal MinimumAllowed
        {
            get { return _MinimumAllowed; }
            set { _MinimumAllowed = value; }
        }
        //ServiceCharge
        [Required(ErrorMessage = "Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Service Charge")]
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        //MaximumAllowedSC
        [Required(ErrorMessage = "Maximum Allowed Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max Allowed Service Charge")]
        private decimal _MaximumAllowedSC = 0;
        public decimal MaximumAllowedSC
        {
            get { return _MaximumAllowedSC; }
            set { _MaximumAllowedSC = value; }
        }

        //MinimumAllowed
        [Required(ErrorMessage = "Minimum Allowed Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min Allowed Amount")]
        private decimal _MinimumAllowedSC = 0;
        public decimal MinimumAllowedSC
        {
            get { return _MinimumAllowedSC; }
            set { _MinimumAllowedSC = value; }
        }
        //FromDate
        [Required(ErrorMessage = "From Date is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter From Date")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "From Date")]
        private DateTime _FromDate = System.DateTime.UtcNow;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        //ToDate
        [Required(ErrorMessage = "To Date is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter To Date")]
        [Display(Name = "To Date")]
        private DateTime _ToDate = System.DateTime.UtcNow;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        //ServiceId
        private int _ServiceId = 0;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

        //ScheduleStatus
        private string _ScheduleStatus = String.Empty;
        public string ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }

        //ServiceName
        private string _ServiceName = String.Empty;
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
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
        private string _UpdateIndiaDate = string.Empty;
        public string UpdateIndiaDate
        {
            get { return _UpdateIndiaDate; }
            set { _UpdateIndiaDate = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //EnumScheduleStatus
        private ScheduleStatuses _EnumScheduleStatus = 0;
        public ScheduleStatuses EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
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

        private string _CheckFromDate = string.Empty;
        public string CheckFromDate
        {
            get { return _CheckFromDate; }
            set { _CheckFromDate = value; }
        }

        private string _CheckToDate = string.Empty;
        public string CheckToDate
        {
            get { return _CheckToDate; }
            set { _CheckToDate = value; }
        }

        //Running
        private string _Running = string.Empty;
        public string Running
        {
            get { return _Running; }
            set { _Running = value; }
        }

        //Expired
        private string _Expired = string.Empty;
        public string Expired
        {
            get { return _Expired; }
            set { _Expired = value; }
        }

        //Scheduled
        private string _Scheduled = string.Empty;
        public string Scheduled
        {
            get { return _Scheduled; }
            set { _Scheduled = value; }
        }

        //FixedCommissionMerchant
        private decimal _FixedCommissionMerchant = 0;
        public decimal FixedCommissionMerchant
        {
            get { return _FixedCommissionMerchant; }
            set { _FixedCommissionMerchant = value; }
        }

        //PercentageCommissionMerchant
        private decimal _PercentageCommissionMerchant = 0;
        public decimal PercentageCommissionMerchant
        {
            get { return _PercentageCommissionMerchant; }
            set { _PercentageCommissionMerchant = value; }
        }

        //MyPayContribution
        private decimal _MyPayContribution = 0;
        public decimal MyPayContribution
        {
            get { return _MyPayContribution; }
            set { _MyPayContribution = value; }
        }

        //MerchantContribution
        private decimal _MerchantContribution = 0;
        public decimal MerchantContribution
        {
            get { return _MerchantContribution; }
            set { _MerchantContribution = value; }
        }

        //Discount
        private decimal _Discount = 0;
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        //TransactionCountLimit
        private Int32 _TransactionCountLimit = 0;
        public Int32 TransactionCountLimit
        {
            get { return _TransactionCountLimit; }
            set { _TransactionCountLimit = value; }
        }

        //IsDefault
        private bool _IsDefault =false;
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }

        //CheckIsDefault
        private Int32 _CheckIsDefault = 2;
        public Int32 CheckIsDefault
        {
            get { return _CheckIsDefault; }
            set { _CheckIsDefault = value; }
        }

        //DefaultTypeName
        private string _DefaultTypeName = String.Empty;
        public string DefaultTypeName
        {
            get { return _DefaultTypeName; }
            set { _DefaultTypeName = value; }
        }

        //FixedDiscount
        private decimal _FixedDiscount = 0;
        public decimal FixedDiscount
        {
            get { return _FixedDiscount; }
            set { _FixedDiscount = value; }
        }

        //MinimumDiscount
        private decimal _MinimumDiscount = 0;
        public decimal MinimumDiscount
        {
            get { return _MinimumDiscount; }
            set { _MinimumDiscount = value; }
        }

        //MaximumDiscount
        private decimal _MaximumDiscount = 0;
        public decimal MaximumDiscount
        {
            get { return _MaximumDiscount; }
            set { _MaximumDiscount = value; }
        }

        bool DataRecieved = false;
        #endregion

        #region "Add Delete Update Methods" 
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_MerchantCommission_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_MerchantCommission_Update", HT);
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
            HT.Add("MinimumAmount", MinimumAmount);
            HT.Add("MaximumAmount", MaximumAmount);
            HT.Add("FixedCommission", FixedCommission);
            HT.Add("PercentageCommission", PercentageCommission);
            HT.Add("PercentageRewardPoints", PercentageRewardPoints);
            HT.Add("PercentageRewardPointsDebit", PercentageRewardPointsDebit);
            HT.Add("FromDate", FromDate);
            HT.Add("ToDate", ToDate);
            HT.Add("MinimumAllowed", MinimumAllowed);
            HT.Add("MaximumAllowed", MaximumAllowed);
            HT.Add("ServiceId", ServiceId);
            HT.Add("Status", Status);
            HT.Add("Type", Type);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("IsActive", IsActive);
            HT.Add("IsApprovedByAdmin", IsApprovedByAdmin);
            HT.Add("IsDeleted", IsDeleted);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("ServiceCharge", ServiceCharge);
            HT.Add("MinimumAllowedSC", MinimumAllowedSC);
            HT.Add("FixedCommissionMerchant", FixedCommissionMerchant);
            HT.Add("PercentageCommissionMerchant", PercentageCommissionMerchant);
            HT.Add("MyPayContribution", MyPayContribution);
            HT.Add("MerchantContribution", MerchantContribution);
            HT.Add("TransactionCountLimit", TransactionCountLimit);
            HT.Add("Discount", Discount);
            HT.Add("IsDefault", IsDefault);
            HT.Add("MaximumAllowedSC", MaximumAllowedSC);
            HT.Add("FixedDiscount", FixedDiscount);
            HT.Add("MinimumDiscount", MinimumDiscount);
            HT.Add("MaximumDiscount", MaximumDiscount);
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
                HT.Add("Id", Id);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("ServiceId", ServiceId);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("FromDate", CheckFromDate);
                HT.Add("ToDate", CheckToDate);
                HT.Add("Status", Status);
                HT.Add("Type", Type);
                HT.Add("Running", Running);
                HT.Add("Expired", Expired);
                HT.Add("Scheduled", Scheduled);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("CheckIsDefault", CheckIsDefault);
                dt = obj.GetDataFromStoredProcedure("sp_MerchantCommission_Get", HT);
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
                HT.Add("Id", Id);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("ServiceId", ServiceId);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("FromDate", CheckFromDate);
                HT.Add("ToDate", CheckToDate);
                HT.Add("Status", Status);
                HT.Add("Type", Type);
                HT.Add("Running", Running);
                HT.Add("Expired", Expired);
                HT.Add("Scheduled", Scheduled);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("CheckIsDefault", CheckIsDefault);
                dt = obj.GetDataFromStoredProcedure("sp_MerchantCommission_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    MerchantUniqueId = dt.Rows[0]["MerchantUniqueId"].ToString();
                    MinimumAmount = Convert.ToDecimal(dt.Rows[0]["MinimumAmount"].ToString());
                    MaximumAmount = Convert.ToDecimal(dt.Rows[0]["MaximumAmount"].ToString());
                    FixedCommission = Convert.ToDecimal(dt.Rows[0]["FixedCommission"].ToString());
                    PercentageCommission = Convert.ToDecimal(dt.Rows[0]["PercentageCommission"].ToString());
                    PercentageRewardPoints = Convert.ToDecimal(dt.Rows[0]["PercentageRewardPoints"].ToString());
                    PercentageRewardPointsDebit = Convert.ToDecimal(dt.Rows[0]["PercentageRewardPointsDebit"].ToString());
                    FromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString());
                    ToDate = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString());
                    MinimumAllowed = Convert.ToDecimal(dt.Rows[0]["MinimumAllowed"].ToString());
                    MaximumAllowed = Convert.ToDecimal(dt.Rows[0]["MaximumAllowed"].ToString());
                    ServiceId = Convert.ToInt32(dt.Rows[0]["ServiceId"].ToString());
                    Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    Type = Convert.ToInt32(dt.Rows[0]["Type"].ToString());
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = Convert.ToString(dt.Rows[0]["CreatedByName"].ToString());
                    UpdatedBy = Convert.ToInt64(dt.Rows[0]["UpdatedBy"].ToString());
                    UpdatedByName = Convert.ToString(dt.Rows[0]["UpdatedByName"].ToString());
                    ServiceCharge = Convert.ToDecimal(dt.Rows[0]["ServiceCharge"].ToString());                   
                    MinimumAllowedSC = Convert.ToDecimal(dt.Rows[0]["MinimumAllowedSC"].ToString());
                    MaximumAllowedSC = Convert.ToDecimal(dt.Rows[0]["MaximumAllowedSC"].ToString());
                    FixedCommissionMerchant = Convert.ToDecimal(dt.Rows[0]["FixedCommissionMerchant"].ToString());
                    PercentageCommissionMerchant = Convert.ToDecimal(dt.Rows[0]["PercentageCommissionMerchant"].ToString());
                    MyPayContribution = Convert.ToDecimal(dt.Rows[0]["MyPayContribution"].ToString());
                    MerchantContribution = Convert.ToDecimal(dt.Rows[0]["MerchantContribution"].ToString());
                    Discount = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    TransactionCountLimit = Convert.ToInt32(dt.Rows[0]["TransactionCountLimit"].ToString());
                    IsDefault = Convert.ToBoolean(dt.Rows[0]["IsDefault"].ToString());
                    FixedDiscount = Convert.ToDecimal(dt.Rows[0]["FixedDiscount"].ToString());
                    MinimumDiscount = Convert.ToDecimal(dt.Rows[0]["MinimumDiscount"].ToString());
                    MaximumDiscount = Convert.ToDecimal(dt.Rows[0]["MaximumDiscount"].ToString());
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
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("PagingSize", PagingSize);
                HT.Add("SearchText", SearchText);               
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("MinimumAmount", MinimumAmount);
                HT.Add("MaximumAmount", MaximumAmount);
                HT.Add("MinimumAllowedSC", MinimumAllowedSC);
                HT.Add("MaximumAllowedSC", MaximumAllowedSC);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("Id", Id);
                HT.Add("FromDate", CheckFromDate);
                HT.Add("ToDate", CheckToDate);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("ServiceId", ServiceId);
                HT.Add("Running", Running);
                HT.Add("Expired", Expired);               
                HT.Add("Scheduled", Scheduled);
                HT.Add("CheckIsDefault", CheckIsDefault);
                dt = obj.GetDataFromStoredProcedure("sp_MerchantCommission_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public float CountMerchantCommissionCheck(string serviceId, string MerchantUniqueId, decimal MinimumAmount, decimal MaximumAmount, Int64 Id)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Id) from MerchantCommission with(nolock) where Id != '" + Id.ToString() + "' and MerchantUniqueId = '" + MerchantUniqueId + "' and  ServiceId='" + serviceId + "' and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ))) and IsActive=1 and IsDeleted=0  and IsDefault=0 and cast(ToDate as date) > cast(getdate() as date ) ";
                //string str = "select count(Id) from Commission where Id != '" + Id.ToString() + "' and  ServiceId='" + serviceId + "'  and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ) and IsActive=1 and IsDeleted=0))";
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