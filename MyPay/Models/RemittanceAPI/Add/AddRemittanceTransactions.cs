using MyPay.Models.Common; 
using System; 

namespace MyPay.Models.RemittanceAPI.Add
{
    public class AddRemittanceTransactions : CommonAdd
    {
        bool DataRecieved = false;
        #region "Enums"

        public enum Statuses
        {
            Success = 1,
            Pending = 2,
            Failed = 3,
            Queued = 4,
            Processing = 5,
            Expired = 6,
            Error = 7,
            Status_Error = 8,
            Refund = 9
        }

        public enum Signs
        {
            Credit = 1,
            Debit = 2
        }

        public enum WalletTypes
        {
            NotSelected = 0,
            LoadWallet = 1,
            ConvertWallet = 2,
            ApiTransaction = 3,
            FeeAccountBalance=4
        }

        #endregion
        #region "Properties" 
        private Int64 _MerchantMemberId = 0;
        public Int64 MerchantMemberId
        {
            get { return _MerchantMemberId; }
            set { _MerchantMemberId = value; }
        }
        private String _MerchantName = string.Empty;
        public String MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }
        private String _MerchantContactNumber = string.Empty;
        public String MerchantContactNumber
        {
            get { return _MerchantContactNumber; }
            set { _MerchantContactNumber = value; }
        }
        private String _MerchantUniqueId = string.Empty;
        public String MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }
        private String _FromCurrency = string.Empty;
        public String FromCurrency
        {
            get { return _FromCurrency; }
            set { _FromCurrency = value; }
        }
        private String _ToCurrency = string.Empty;
        public String ToCurrency
        {
            get { return _ToCurrency; }
            set { _ToCurrency = value; }
        }
        private Decimal _FromAmount = 0;
        public Decimal FromAmount
        {
            get { return _FromAmount; }
            set { _FromAmount = value; }
        }
        private Decimal _ToAmount = 0;
        public Decimal ToAmount
        {
            get { return _ToAmount; }
            set { _ToAmount = value; }
        }
        private Int32 _Sign = 0;
        public Int32 Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }
        private String _Remarks = string.Empty;
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private String _Description = string.Empty;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private String _TransactionUniqueId = string.Empty;
        public String TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private String _ParentTransactionId = string.Empty;
        public String ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        private Int32 _Status = 0;
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private Int32 _CurrencyId = 0;
        public Int32 CurrencyId
        {
            get { return _CurrencyId; }
            set { _CurrencyId = value; }
        }
        private Int32 _Type = 0;
        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private Decimal _CurrentBalance = 0;
        public Decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }
        private Decimal _PreviousBalance = 0;
        public Decimal PreviousBalance
        {
            get { return _PreviousBalance; }
            set { _PreviousBalance = value; }
        }
        private String _GatewayTransactionId = string.Empty;
        public String GatewayTransactionId
        {
            get { return _GatewayTransactionId; }
            set { _GatewayTransactionId = value; }
        }
        private String _Reference = string.Empty;
        public String Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private String _BeneficiaryName = string.Empty;
        public String BeneficiaryName
        {
            get { return _BeneficiaryName; }
            set { _BeneficiaryName = value; }
        }
        private String _BeneficiaryContactNumber = string.Empty;
        public String BeneficiaryContactNumber
        {
            get { return _BeneficiaryContactNumber; }
            set { _BeneficiaryContactNumber = value; }
        }
        private String _BeneficiaryBankCode = string.Empty;
        public String BeneficiaryBankCode
        {
            get { return _BeneficiaryBankCode; }
            set { _BeneficiaryBankCode = value; }
        }
        private String _BeneficiaryAccountNo = string.Empty;
        public String BeneficiaryAccountNo
        {
            get { return _BeneficiaryAccountNo; }
            set { _BeneficiaryAccountNo = value; }
        }
        private String _BeneficiaryBankName = string.Empty;
        public String BeneficiaryBankName
        {
            get { return _BeneficiaryBankName; }
            set { _BeneficiaryBankName = value; }
        }
        private String _BeneficiaryBranchName = string.Empty;
        public String BeneficiaryBranchName
        {
            get { return _BeneficiaryBranchName; }
            set { _BeneficiaryBranchName = value; }
        }
        private Decimal _ServiceCharge = 0;
        public Decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        private String _GatewayStatus = string.Empty;
        public String GatewayStatus
        {
            get { return _GatewayStatus; }
            set { _GatewayStatus = value; }
        }
        private String _MerchantBankCode = string.Empty;
        public String MerchantBankCode
        {
            get { return _MerchantBankCode; }
            set { _MerchantBankCode = value; }
        }
        private String _MerchantAccountNo = string.Empty;
        public String MerchantAccountNo
        {
            get { return _MerchantAccountNo; }
            set { _MerchantAccountNo = value; }
        }
        private String _MerchantBranch = string.Empty;
        public String MerchantBranch
        {
            get { return _MerchantBranch; }
            set { _MerchantBranch = value; }
        }
        private String _MerchantBankName = string.Empty;
        public String MerchantBankName
        {
            get { return _MerchantBankName; }
            set { _MerchantBankName = value; }
        }
        private String _MerchantBranchName = string.Empty;
        public String MerchantBranchName
        {
            get { return _MerchantBranchName; }
            set { _MerchantBranchName = value; }
        }
        private String _IpAddress = Common.Common.GetUserIP();
        public String IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private String _ResponseCode = string.Empty;
        public String ResponseCode
        {
            get { return _ResponseCode; }
            set { _ResponseCode = value; }
        }
        private Int32 _WalletType = 0;
        public Int32 WalletType
        {
            get { return _WalletType; }
            set { _WalletType = value; }
        }
        private String _Purpose = string.Empty;
        public String Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }
        private Decimal _NetAmount = 0;
        public Decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        private String _Platform = string.Empty;
        public String Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
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
        private String _StatusName = string.Empty;
        public String StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        private String _CreatedDatedt = string.Empty;
        public String CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        private String _UpdatedDatedt = string.Empty;
        public String UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }
        }

        private String _SignName = string.Empty;
        public String SignName
        {
            get { return _SignName; }
            set { _SignName = value; }
        }

        //FilterTotalCount
        private Int32 _FilterTotalCount = 0;
        public Int32 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }

        //ConversionRate
        private decimal _ConversionRate = 0;
        public decimal ConversionRate
        {
            get { return _ConversionRate; }
            set { _ConversionRate = value; }
        }

        //ConvertedAmount
        private decimal _ConvertedAmount = 0;
        public decimal ConvertedAmount
        {
            get { return _ConvertedAmount; }
            set { _ConvertedAmount = value; }
        }

        //BaseCurrencyServiceCharge
        private decimal _BaseCurrencyServiceCharge = 0;
        public decimal BaseCurrencyServiceCharge
        {
            get { return _BaseCurrencyServiceCharge; }
            set { _BaseCurrencyServiceCharge = value; }
        }

        private String _WalletTypeName = string.Empty;
        public String WalletTypeName
        {
            get { return _WalletTypeName; }
            set { _WalletTypeName = value; }
        }
        private String _TypeName = string.Empty;
        public String TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        private String _ReceiptFile = string.Empty;
        public String ReceiptFile
        {
            get { return _ReceiptFile; }
            set { _ReceiptFile = value; }
        }

        private Int32 _IsFeeAccountTransaction = 2;
        public Int32 IsFeeAccountTransaction
        {
            get { return _IsFeeAccountTransaction; }
            set { _IsFeeAccountTransaction = value; }
        }

        private int _FeeType = 0;
        public int FeeType
        {
            get { return _FeeType; }
            set { _FeeType = value; }
        }

        private string _FeeTypeName = string.Empty;
        public string FeeTypeName
        {
            get { return _FeeTypeName; }
            set { _FeeTypeName = value; }
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
                HT.Add("FeeType", FeeType);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceTransactions_AddNew", HT);
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
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_RemittanceTransactions_Update", HT);
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
            HT.Add("MerchantMemberId", MerchantMemberId);
            HT.Add("MerchantName", MerchantName);
            HT.Add("MerchantContactNumber", MerchantContactNumber);
            HT.Add("MerchantUniqueId", MerchantUniqueId);
            HT.Add("FromCurrency", FromCurrency);
            HT.Add("ToCurrency", ToCurrency);
            HT.Add("FromAmount", FromAmount);
            HT.Add("ToAmount", ToAmount);
            HT.Add("Sign", Sign);
            HT.Add("Remarks", Remarks);
            HT.Add("Description", Description);
            HT.Add("TransactionUniqueId", TransactionUniqueId);
            HT.Add("ParentTransactionId", ParentTransactionId);
            HT.Add("CurrentBalance", CurrentBalance);
            HT.Add("Status", Status);
            HT.Add("GatewayTransactionId", GatewayTransactionId);
            HT.Add("Reference", Reference);
            HT.Add("BeneficiaryName", BeneficiaryName);
            HT.Add("BeneficiaryContactNumber", BeneficiaryContactNumber);
            HT.Add("BeneficiaryBankCode", BeneficiaryBankCode);
            HT.Add("BeneficiaryAccountNo", BeneficiaryAccountNo);
            HT.Add("BeneficiaryBankName", BeneficiaryBankName);
            HT.Add("BeneficiaryBranchName", BeneficiaryBranchName);
            HT.Add("ServiceCharge", ServiceCharge);
            HT.Add("GatewayStatus", GatewayStatus);
            HT.Add("MerchantBankCode", MerchantBankCode);
            HT.Add("MerchantAccountNo", MerchantAccountNo);
            HT.Add("MerchantBranch", MerchantBranch);
            HT.Add("MerchantBankName", MerchantBankName);
            HT.Add("MerchantBranchName", MerchantBranchName);
            HT.Add("IpAddress", IpAddress);
            HT.Add("ResponseCode", ResponseCode);
            HT.Add("WalletType", WalletType);
            HT.Add("Type", Type);
            HT.Add("CurrencyId", CurrencyId);
            HT.Add("Purpose", Purpose);
            HT.Add("NetAmount", NetAmount);
            HT.Add("Platform", Platform);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("Sno", Sno);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("ConversionRate", ConversionRate);
            HT.Add("ConvertedAmount", ConvertedAmount);
            HT.Add("BaseCurrencyServiceCharge", BaseCurrencyServiceCharge);
            HT.Add("ReceiptFile", ReceiptFile);
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
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantName", MerchantName);
                HT.Add("MerchantContactNumber", MerchantContactNumber);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("FromCurrency", FromCurrency);
                HT.Add("ToCurrency", ToCurrency);
                HT.Add("FromAmount", FromAmount);
                HT.Add("ToAmount", ToAmount);
                HT.Add("Sign", Sign);
                HT.Add("Remarks", Remarks);
                HT.Add("Description", Description);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("Status", Status);
                HT.Add("GatewayTransactionId", GatewayTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("BeneficiaryName", BeneficiaryName);
                HT.Add("BeneficiaryContactNumber", BeneficiaryContactNumber);
                HT.Add("BeneficiaryBankCode", BeneficiaryBankCode);
                HT.Add("BeneficiaryAccountNo", BeneficiaryAccountNo);
                HT.Add("BeneficiaryBankName", BeneficiaryBankName);
                HT.Add("BeneficiaryBranchName", BeneficiaryBranchName);
                HT.Add("ServiceCharge", ServiceCharge);
                HT.Add("GatewayStatus", GatewayStatus);
                HT.Add("MerchantBankCode", MerchantBankCode);
                HT.Add("MerchantAccountNo", MerchantAccountNo);
                HT.Add("MerchantBranch", MerchantBranch);
                HT.Add("MerchantBankName", MerchantBankName);
                HT.Add("MerchantBranchName", MerchantBranchName);
                HT.Add("IpAddress", IpAddress);
                HT.Add("ResponseCode", ResponseCode);
                HT.Add("WalletType", WalletType);
                HT.Add("Type", Type);
                HT.Add("CurrencyId", CurrencyId); 
                HT.Add("Purpose", Purpose);
                HT.Add("NetAmount", NetAmount);
                HT.Add("Platform", Platform);
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

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceTransactions_Get", HT);
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
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantName", MerchantName);
                HT.Add("MerchantContactNumber", MerchantContactNumber);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("FromCurrency", FromCurrency);
                HT.Add("ToCurrency", ToCurrency);
                HT.Add("FromAmount", FromAmount);
                HT.Add("ToAmount", ToAmount);
                HT.Add("Sign", Sign);
                HT.Add("Remarks", Remarks);
                HT.Add("Description", Description);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("Status", Status);
                HT.Add("GatewayTransactionId", GatewayTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("BeneficiaryName", BeneficiaryName);
                HT.Add("BeneficiaryContactNumber", BeneficiaryContactNumber);
                HT.Add("BeneficiaryBankCode", BeneficiaryBankCode);
                HT.Add("BeneficiaryAccountNo", BeneficiaryAccountNo);
                HT.Add("BeneficiaryBankName", BeneficiaryBankName);
                HT.Add("BeneficiaryBranchName", BeneficiaryBranchName);
                HT.Add("ServiceCharge", ServiceCharge);
                HT.Add("GatewayStatus", GatewayStatus);
                HT.Add("MerchantBankCode", MerchantBankCode);
                HT.Add("MerchantAccountNo", MerchantAccountNo);
                HT.Add("MerchantBranch", MerchantBranch);
                HT.Add("MerchantBankName", MerchantBankName);
                HT.Add("MerchantBranchName", MerchantBranchName);
                HT.Add("IpAddress", IpAddress);
                HT.Add("ResponseCode", ResponseCode);
                HT.Add("WalletType", WalletType);
                HT.Add("Type", Type);
                HT.Add("CurrencyId", CurrencyId);
                HT.Add("Purpose", Purpose);
                HT.Add("NetAmount", NetAmount);
                HT.Add("Platform", Platform);
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

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceTransactions_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MerchantMemberId = Convert.ToInt64(dt.Rows[0]["MerchantMemberId"].ToString());
                    MerchantName = Convert.ToString(dt.Rows[0]["MerchantName"].ToString());
                    MerchantContactNumber = Convert.ToString(dt.Rows[0]["MerchantContactNumber"].ToString());
                    MerchantUniqueId = Convert.ToString(dt.Rows[0]["MerchantUniqueId"].ToString());
                    FromCurrency = Convert.ToString(dt.Rows[0]["FromCurrency"].ToString());
                    ToCurrency = Convert.ToString(dt.Rows[0]["ToCurrency"].ToString());
                    FromAmount = Convert.ToDecimal(dt.Rows[0]["FromAmount"].ToString());
                    ToAmount = Convert.ToDecimal(dt.Rows[0]["ToAmount"].ToString());
                    Sign = Convert.ToInt32(dt.Rows[0]["Sign"].ToString());
                    Remarks = Convert.ToString(dt.Rows[0]["Remarks"].ToString());
                    Description = Convert.ToString(dt.Rows[0]["Description"].ToString());
                    TransactionUniqueId = Convert.ToString(dt.Rows[0]["TransactionUniqueId"].ToString());
                    ParentTransactionId = Convert.ToString(dt.Rows[0]["ParentTransactionId"].ToString());
                    CurrentBalance = Convert.ToDecimal(dt.Rows[0]["CurrentBalance"].ToString());
                    PreviousBalance = Convert.ToDecimal(dt.Rows[0]["PreviousBalance"].ToString());
                    Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                    GatewayTransactionId = Convert.ToString(dt.Rows[0]["GatewayTransactionId"].ToString());
                    Reference = Convert.ToString(dt.Rows[0]["Reference"].ToString());
                    BeneficiaryName = Convert.ToString(dt.Rows[0]["BeneficiaryName"].ToString());
                    BeneficiaryContactNumber = Convert.ToString(dt.Rows[0]["BeneficiaryContactNumber"].ToString());
                    BeneficiaryBankCode = Convert.ToString(dt.Rows[0]["BeneficiaryBankCode"].ToString());
                    BeneficiaryAccountNo = Convert.ToString(dt.Rows[0]["BeneficiaryAccountNo"].ToString());
                    BeneficiaryBankName = Convert.ToString(dt.Rows[0]["BeneficiaryBankName"].ToString());
                    BeneficiaryBranchName = Convert.ToString(dt.Rows[0]["BeneficiaryBranchName"].ToString());
                    ServiceCharge = Convert.ToDecimal(dt.Rows[0]["ServiceCharge"].ToString());
                    GatewayStatus = Convert.ToString(dt.Rows[0]["GatewayStatus"].ToString());
                    MerchantBankCode = Convert.ToString(dt.Rows[0]["MerchantBankCode"].ToString());
                    MerchantAccountNo = Convert.ToString(dt.Rows[0]["MerchantAccountNo"].ToString());
                    MerchantBranch = Convert.ToString(dt.Rows[0]["MerchantBranch"].ToString());
                    MerchantBankName = Convert.ToString(dt.Rows[0]["MerchantBankName"].ToString());
                    MerchantBranchName = Convert.ToString(dt.Rows[0]["MerchantBranchName"].ToString());
                    IpAddress = Convert.ToString(dt.Rows[0]["IpAddress"].ToString());
                    ResponseCode = Convert.ToString(dt.Rows[0]["ResponseCode"].ToString());
                    WalletType = Convert.ToInt32(dt.Rows[0]["WalletType"].ToString());
                    CurrencyId = Convert.ToInt32(dt.Rows[0]["CurrencyId"].ToString());
                    Type = Convert.ToInt32(dt.Rows[0]["Type"].ToString());
                    Purpose = Convert.ToString(dt.Rows[0]["Purpose"].ToString());
                    NetAmount = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());
                    Platform = Convert.ToString(dt.Rows[0]["Platform"].ToString());
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
                    ConvertedAmount = Convert.ToDecimal(dt.Rows[0]["ConvertedAmount"].ToString());
                    ConversionRate = Convert.ToDecimal(dt.Rows[0]["ConversionRate"].ToString());
                    BaseCurrencyServiceCharge = Convert.ToDecimal(dt.Rows[0]["BaseCurrencyServiceCharge"].ToString());
                    ReceiptFile = dt.Rows[0]["ReceiptFile"].ToString();
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
                HT.Add("MerchantMemberId", MerchantMemberId);
                HT.Add("MerchantName", MerchantName);
                HT.Add("MerchantContactNumber", MerchantContactNumber);
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("FromCurrency", FromCurrency);
                HT.Add("ToCurrency", ToCurrency);
                HT.Add("FromAmount", FromAmount);
                HT.Add("ToAmount", ToAmount);
                HT.Add("Sign", Sign);
                HT.Add("Remarks", Remarks);
                HT.Add("Description", Description);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("ParentTransactionId", ParentTransactionId);
                HT.Add("CurrentBalance", CurrentBalance);
                HT.Add("Status", Status);
                HT.Add("GatewayTransactionId", GatewayTransactionId);
                HT.Add("Reference", Reference);
                HT.Add("BeneficiaryName", BeneficiaryName);
                HT.Add("BeneficiaryContactNumber", BeneficiaryContactNumber);
                HT.Add("BeneficiaryBankCode", BeneficiaryBankCode);
                HT.Add("BeneficiaryAccountNo", BeneficiaryAccountNo);
                HT.Add("BeneficiaryBankName", BeneficiaryBankName);
                HT.Add("BeneficiaryBranchName", BeneficiaryBranchName);
                HT.Add("ServiceCharge", ServiceCharge);
                HT.Add("GatewayStatus", GatewayStatus);
                HT.Add("MerchantBankCode", MerchantBankCode);
                HT.Add("MerchantAccountNo", MerchantAccountNo);
                HT.Add("MerchantBranch", MerchantBranch);
                HT.Add("MerchantBankName", MerchantBankName);
                HT.Add("MerchantBranchName", MerchantBranchName);
                HT.Add("IpAddress", IpAddress);
                HT.Add("ResponseCode", ResponseCode);
                HT.Add("WalletType", WalletType);
                HT.Add("Type", Type);
                HT.Add("CurrencyId", CurrencyId);
                HT.Add("Purpose", Purpose);
                HT.Add("NetAmount", NetAmount);
                HT.Add("Platform", Platform);
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
                HT.Add("IsFeeAccountTransaction", IsFeeAccountTransaction);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_RemittanceTransactions_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_RemittanceTransactions_DatatableCounter", HT);
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