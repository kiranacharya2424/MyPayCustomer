using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMerchantWithdrawalRequest : CommonAdd
    {
        #region Enums
        public enum MerchantWithdrawalStatus
        {
            Success = 1,
            Failed = 2,
            Cancelled = 3,
            Pending = 4,
            Incomplete = 5,
            Refund = 6,
            ApprovalPending = 7
        }

        public enum MerchantWithdrawalRequestType
        {
            MerchantBalance = 0,
            WalletBalance = 1
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

        private Int64 _BankId = 0;
        public Int64 BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }

        private string _BankCode = string.Empty;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }

        private Int64 _BankStatus = 0;
        public Int64 BankStatus
        {
            get { return _BankStatus; }
            set { _BankStatus = value; }
        }
        private string _Particulars = string.Empty;
        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }
        private string _TrackerId = string.Empty;
        public string TrackerId
        {
            get { return _TrackerId; }
            set { _TrackerId = value; }
        }
        private string _JsonResponse = string.Empty;
        public string JsonResponse
        {
            get { return _JsonResponse; }
            set { _JsonResponse = value; }
        }

        private string _MerchantName = string.Empty;
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }
        private string _MerchantOrganization = string.Empty;
        public string MerchantOrganization
        {
            get { return _MerchantOrganization; }
            set { _MerchantOrganization = value; }
        }
        private string _MerchantContactNumber = string.Empty;
        public string MerchantContactNumber
        {
            get { return _MerchantContactNumber; }
            set { _MerchantContactNumber = value; }
        }
        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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

        private string _BankName = string.Empty;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }

        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        private string _BankStatusName = string.Empty;
        public string BankStatusName
        {
            get { return _BankStatusName; }
            set { _BankStatusName = value; }
        }
        //FilterTotalCount
        private Int32 _FilterTotalCount = 0;
        public Int32 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }

        //WithdrawalRequestType
        private int _WithdrawalRequestType = -1;
        public int WithdrawalRequestType
        {
            get { return _WithdrawalRequestType; }
            set { _WithdrawalRequestType = value; }
        }

        //WithdrawalRequestTypeName
        private string _WithdrawalRequestTypeName = string.Empty;
        public string WithdrawalRequestTypeName
        {
            get { return _WithdrawalRequestTypeName; }
            set { _WithdrawalRequestTypeName = value; }
        }
        //IsWithdrawalApproveByAdmin
        private bool _IsWithdrawalApproveByAdmin = false;
        public bool IsWithdrawalApproveByAdmin
        {
            get { return _IsWithdrawalApproveByAdmin; }
            set { _IsWithdrawalApproveByAdmin = value; }
        }
        #endregion
    }
}