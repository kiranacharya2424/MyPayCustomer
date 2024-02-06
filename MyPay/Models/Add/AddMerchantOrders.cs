using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMerchantOrders : CommonAdd
    {
        #region Enums
        public enum MerchantOrderStatus
        {
            Success = 1,
            Failed = 2,
            Cancelled = 3,
            Pending = 4,
            Incomplete = 5,
            Refund = 6,
            ApprovalPending = 7
        }

        public enum MerchantOrderSign
        {
            Credit = 1,
            Debit = 2
        }
        #endregion

        #region "Properties"
        //MerchantUniqueId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        private string _MerchantName = string.Empty;
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }
        private string _OrganizationName = string.Empty;
        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private string _MerchantContactNo = string.Empty;
        public string MerchantContactNo
        {
            get { return _MerchantContactNo; }
            set { _MerchantContactNo = value; }
        }


        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        private string _MemberContactNumber = string.Empty;
        public string MemberContactNumber
        {
            get { return _MemberContactNumber; }
            set { _MemberContactNumber = value; }
        }

        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        private string _Particulars = string.Empty;
        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }

        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        private decimal _CashbackAmount = 0;
        public decimal CashbackAmount
        {
            get { return _CashbackAmount; }
            set { _CashbackAmount = value; }
        }
        private decimal _MerchantContributionPercentage = 0;
        public decimal MerchantContributionPercentage
        {
            get { return _MerchantContributionPercentage; }
            set { _MerchantContributionPercentage = value; }
        }

        private string _TrackerId = string.Empty;
        public string TrackerId
        {
            get { return _TrackerId; }
            set { _TrackerId = value; }
        }

        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private decimal _ServiceCharges = 0;
        public decimal ServiceCharges
        {
            get { return _ServiceCharges; }
            set { _ServiceCharges = value; }
        }

        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        private string _OrderToken = string.Empty;
        public string OrderToken
        {
            get { return _OrderToken; }
            set { _OrderToken = value; }
        }

        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        private string _OrderOTP = string.Empty;
        public string OrderOTP
        {
            get { return _OrderOTP; }
            set { _OrderOTP = value; }
        }

        private int _OTPAttemptCount = 0;
        public int OTPAttemptCount
        {
            get { return _OTPAttemptCount; }
            set { _OTPAttemptCount = value; }
        }

        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        private string _SignName = string.Empty;
        public string SignName
        {
            get { return _SignName; }
            set { _SignName = value; }
        }

        private decimal _CurrentBalance = 0;
        public decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }

        private decimal _PreviousBalance = 0;
        public decimal PreviousBalance
        {
            get { return _PreviousBalance; }
            set { _PreviousBalance = value; }
        }

        //FilterTotalCount
        private Int32 _FilterTotalCount = 0;
        public Int32 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }

        //TotalCredit
        private decimal _TotalCredit = 0;
        public decimal TotalCredit
        {
            get { return _TotalCredit; }
            set { _TotalCredit = value; }
        }

        //TotalDebit
        private decimal _TotalDebit = 0;
        public decimal TotalDebit
        {
            get { return _TotalDebit; }
            set { _TotalDebit = value; }
        }

        private decimal _CommissionAmount = 0;
        public decimal CommissionAmount
        {
            get { return _CommissionAmount; }
            set { _CommissionAmount = value; }
        }

        private decimal _DiscountAmount = 0;
        public decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        private string _ReturnUrl = string.Empty;
        public string ReturnUrl
        {
            get { return _ReturnUrl; }
            set { _ReturnUrl = value; }
        }
        #endregion
    }
}