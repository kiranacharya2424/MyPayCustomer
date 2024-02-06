using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddInsuranceDetail: CommonAdd
    {
        #region "Properties"
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //CustomerId
        private string _CustomerId = string.Empty;
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }

        //CustomerName
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        //NextDueDate
        private string _NextDueDate = string.Empty;
        public string NextDueDate
        {
            get { return _NextDueDate; }
            set { _NextDueDate = value; }
        }

        //ProductName
        private string _ProductName =string.Empty;
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        //FineAmount
        private decimal _FineAmount = 0;
        public decimal FineAmount
        {
            get { return _FineAmount; }
            set { _FineAmount = value; }
        }

        //InvoiceNumber
        private string _InvoiceNumber = string.Empty;
        public string InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }

        //PolicyType
        private string _PolicyType = string.Empty;
        public string PolicyType
        {
            get { return _PolicyType; }
            set { _PolicyType = value; }
        }

        //PolicyCategory
        private string _PolicyCategory = string.Empty;
        public string PolicyCategory
        {
            get { return _PolicyCategory; }
            set { _PolicyCategory = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //PolicyNumber
        private string _PolicyNumber = string.Empty;
        public string PolicyNumber
        {
            get { return _PolicyNumber; }
            set { _PolicyNumber = value; }
        }

        //MobileNumber
        private string _MobileNumber = string.Empty;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }

        //ServiceName
        private string _ServiceName = string.Empty;
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }

        //InsuranceSlug
        private string _InsuranceSlug = string.Empty;
        public string InsuranceSlug
        {
            get { return _InsuranceSlug; }
            set { _InsuranceSlug = value; }
        }

        //TaxInvoiceNo
        private string _TaxInvoiceNo = string.Empty;
        public string TaxInvoiceNo
        {
            get { return _TaxInvoiceNo; }
            set { _TaxInvoiceNo = value; }
        }

        //ReceiptNo
        private string _ReceiptNo = string.Empty;
        public string ReceiptNo
        {
            get { return _ReceiptNo; }
            set { _ReceiptNo = value; }
        }

        //DocumentNo
        private string _DocumentNo = string.Empty;
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        //InsuranceId
        private Int64 _InsuranceId = 0;
        public Int64 InsuranceId
        {
            get { return _InsuranceId; }
            set { _InsuranceId = value; }
        }

        //InsuranceType
        private int _InsuranceType = 0;
        public int InsuranceType
        {
            get { return _InsuranceType; }
            set { _InsuranceType = value; }
        }

        //VendorType
        private int _VendorType = 0;
        public int VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }

        //Status
        private string _Status = string.Empty;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //DebitNoteNo
        private string _DebitNoteNo = string.Empty;
        public string DebitNoteNo
        {
            get { return _DebitNoteNo; }
            set { _DebitNoteNo = value; }
        }

        //SessionId
        private Int64 _SessionId = 0;
        public Int64 SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        //Address
        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        //RequestId
        private Int64 _RequestId = 0;
        public Int64 RequestId
        {
            get { return _RequestId; }
            set { _RequestId = value; }
        }

        //ProformaNo
        private string _ProformaNo = string.Empty;
        public string ProformaNo
        {
            get { return _ProformaNo; }
            set { _ProformaNo = value; }
        }

        //TpPremium
        private decimal _TpPremium = 0;
        public decimal TpPremium
        {
            get { return _TpPremium; }
            set { _TpPremium = value; }
        }

        //SumInsured
        private decimal _SumInsured = 0;
        public decimal SumInsured
        {
            get { return _SumInsured; }
            set { _SumInsured = value; }
        }

        //Insured
        private string _Insured = string.Empty;
        public string Insured
        {
            get { return _Insured; }
            set { _Insured = value; }
        }

        //ClassName
        private string _ClassName = string.Empty;
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }



        //DateOfBirth
        private string _DateOfBirth = string.Empty;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        //Paymode
        private string _Paymode = string.Empty;
        public string Paymode
        {
            get { return _Paymode; }
            set { _Paymode = value; }
        }

        //TotalFine
        private decimal _TotalFine = 0;
        public decimal TotalFine
        {
            get { return _TotalFine; }
            set { _TotalFine = value; }
        }

        //RebateAmount
        private decimal _RebateAmount = 0;
        public decimal RebateAmount
        {
            get { return _RebateAmount; }
            set { _RebateAmount = value; }
        }

        //PaymentDate
        private string _PaymentDate = string.Empty;
        public string PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }

        //DueDate
        private string _DueDate = string.Empty;
        public string DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }

        //PolicyStatus
        private string _PolicyStatus = string.Empty;
        public string PolicyStatus
        {
            get { return _PolicyStatus; }
            set { _PolicyStatus = value; }
        }

        //Term
        private int _Term = 0;
        public int Term
        {
            get { return _Term; }
            set { _Term = value; }
        }

        //MaturityDate
        private string _MaturityDate = string.Empty;
        public string MaturityDate
        {
            get { return _MaturityDate; }
            set { _MaturityDate = value; }
        }

        //UniqueIdGUID
        private string _UniqueIdGUID = string.Empty;
        public string UniqueIdGUID
        {
            get { return _UniqueIdGUID; }
            set { _UniqueIdGUID = value; }
        }

        //ErrorCode
        private int _ErrorCode = 0;
        public int ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        //Error
        private string _Error = string.Empty;
        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }

        //CreditsConsumed
        private decimal _CreditsConsumed = 0;
        public decimal CreditsConsumed
        {
            get { return _CreditsConsumed; }
            set { _CreditsConsumed = value; }
        }

        //CreditsAvailable
        private decimal _CreditsAvailable = 0;
        public decimal CreditsAvailable
        {
            get { return _CreditsAvailable; }
            set { _CreditsAvailable = value; }
        }

        //PlanCode
        private string _PlanCode = string.Empty;
        public string PlanCode
        {
            get { return _PlanCode; }
            set { _PlanCode = value; }
        }

        //AdjustmentAmount
        private decimal _AdjustmentAmount = 0;
        public decimal AdjustmentAmount
        {
            get { return _AdjustmentAmount; }
            set { _AdjustmentAmount = value; }
        }

        //ExpiryDate
        private string _ExpiryDate = string.Empty;
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }

        //CoverageType
        private string _CoverageType = string.Empty;
        public string CoverageType
        {
            get { return _CoverageType; }
            set { _CoverageType = value; }
        }

        //ids
        private string _ids = string.Empty;
        public string ids
        {
            get { return _ids; }
            set { _ids = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        //WalletBalance
        private decimal _WalletBalance = 0;
        public decimal WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }

        }

        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }

        }

        private string _UpdatedDatedt = string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }

        }
        #endregion
    }
}