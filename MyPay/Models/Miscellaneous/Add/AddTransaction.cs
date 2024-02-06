using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddTransaction : CommonAdd
    {
        #region "Properties"

        private string _Row = string.Empty;
        public string Row
        {
            get { return _Row; }
            set { _Row = value; }
        }
        private string _MemberId = string.Empty;
        public string MemberId
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
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _Sign = string.Empty;
        public string Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }
        private string _Type = string.Empty;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _ProviderName = string.Empty;
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private string _VendorType = string.Empty;
        public string VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }
        private string _VendorResponsePin = string.Empty;
        public string VendorResponsePin
        {
            get { return _VendorResponsePin; }
            set { _VendorResponsePin = value; }
        }
        private string _VendorResponseSerial = string.Empty;
        public string VendorResponseSerial
        {
            get { return _VendorResponseSerial; }
            set { _VendorResponseSerial = value; }
        }
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private string _CurrentBalance = string.Empty;
        public string CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }
        private string _Status = string.Empty;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _VendorTransactionId = string.Empty;
        public string VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }
        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _IndiaDate = string.Empty;
        public string IndiaDate
        {
            get { return _IndiaDate; }
            set { _IndiaDate = value; }
        }
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }


        private string _CardType = string.Empty;
        public string CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }

        private string _CardNumber = string.Empty;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }

        private string _ExpiryDate = string.Empty;
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }

        }



        //RecieverName
        private string _RecieverName = string.Empty;
        public string RecieverName
        {
            get { return _RecieverName; }
            set { _RecieverName = value; }
        }

        //UpdateBy
        private Int64 _UpdateBy = 0;
        public Int64 UpdateBy
        {
            get { return _UpdateBy; }
            set { _UpdateBy = value; }
        }

        //RecieverId
        private Int64 _RecieverId = 0;
        public Int64 RecieverId
        {
            get { return _RecieverId; }
            set { _RecieverId = value; }
        }

        //RecieverContactNumber
        private string _RecieverContactNumber = string.Empty;
        public string RecieverContactNumber
        {
            get { return _RecieverContactNumber; }
            set { _RecieverContactNumber = value; }
        }

        //CashBack
        private decimal _CashBack = 0;
        public decimal CashBack
        {
            get { return _CashBack; }
            set { _CashBack = value; }
        }

        //ServiceCharge
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }

        //RewardPoint
        private decimal _RewardPoint = 0;
        public decimal RewardPoint
        {
            get { return _RewardPoint; }
            set { _RewardPoint = value; }
        }
        //MPCoinsCredit
        private decimal _MPCoinsCredit = 0;
        public decimal MPCoinsCredit
        {
            get { return _MPCoinsCredit; }
            set { _MPCoinsCredit = value; }
        }

        //MPCoinsDebit
        private decimal _MPCoinsDebit = 0;
        public decimal MPCoinsDebit
        {
            get { return _MPCoinsDebit; }
            set { _MPCoinsDebit = value; }
        }

        //TransactionAmount
        private decimal _TransactionAmount = 0;
        public decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        
        //UpdateByName
        private string _UpdateByName = string.Empty;
        public string UpdateByName
        {
            get { return _UpdateByName; }
            set { _UpdateByName = value; }
        }

        //TransferType
        private int _TransferType = 0;
        public int TransferType
        {
            get { return _TransferType; }
            set { _TransferType = value; }
        }

        //_NetAmount
        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        //RecieverBankCode
        private string _RecieverBankCode = string.Empty;
        public string RecieverBankCode
        {
            get { return _RecieverBankCode; }
            set { _RecieverBankCode = value; }
        }
        //RecieverAccountNo
        private string _RecieverAccountNo = string.Empty;
        public string RecieverAccountNo
        {
            get { return _RecieverAccountNo; }
            set { _RecieverAccountNo = value; }
        }
        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
      
        //SenderBankName
        private string _SenderBankName = string.Empty;
        public string SenderBankName
        {
            get { return _SenderBankName; }
            set { _SenderBankName = value; }
        }
        //RecieverBankName
        private string _RecieverBankName = string.Empty;
        public string RecieverBankName
        {
            get { return _RecieverBankName; }
            set { _RecieverBankName = value; }
        }
        //SenderBranchName
        private string _SenderBranchName = string.Empty;
        public string SenderBranchName
        {
            get { return _SenderBranchName; }
            set { _SenderBranchName = value; }
        }
        //RecieverBranchName
        private string _RecieverBranchName = string.Empty;
        public string RecieverBranchName
        {
            get { return _RecieverBranchName; }
            set { _RecieverBranchName = value; }
        }
      


        //WalletType
        private int _WalletType = 0;
        public int WalletType
        {
            get { return _WalletType; }
            set { _WalletType = value; }
        }

        //GatewayStatus
        private string _GatewayStatus = string.Empty;
        public string GatewayStatus
        {
            get { return _GatewayStatus; }
            set { _GatewayStatus = value; }
        }


        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }


        
        //RecieverBranch
        private string _RecieverBranch = string.Empty;
        public string RecieverBranch
        {
            get { return _RecieverBranch; }
            set { _RecieverBranch = value; }
        }
        //SenderBankCode
        private string _SenderBankCode = string.Empty;
        public string SenderBankCode
        {
            get { return _SenderBankCode; }
            set { _SenderBankCode = value; }
        }
        //SenderAccountNo
        private string _SenderAccountNo = string.Empty;
        public string SenderAccountNo
        {
            get { return _SenderAccountNo; }
            set { _SenderAccountNo = value; }
        }
        //SenderBranch
        private string _SenderBranch = string.Empty;
        public string SenderBranch
        {
            get { return _SenderBranch; }
            set { _SenderBranch = value; }
        }
        //BatchTransactionId
        private string _BatchTransactionId = string.Empty;
        public string BatchTransactionId
        {
            get { return _BatchTransactionId; }
            set { _BatchTransactionId = value; }
        }
        //TxnInstructionId
        private string _TxnInstructionId = string.Empty;
        public string TxnInstructionId
        {
            get { return _TxnInstructionId; }
            set { _TxnInstructionId = value; }
        }
        //CustomerId
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        //SignName
        private string _SignName = string.Empty;
        public string SignName
        {
            get { return _SignName; }
            set { _SignName = value; }
        }

        //StatusName
        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }
        //CouponCode
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //CouponDiscount
        private string _CouponDiscount = string.Empty;
        public string CouponDiscount
        {
            get { return _CouponDiscount; }
            set { _CouponDiscount = value; }
        }


        //WalletImage
        private string _WalletImage = string.Empty;
        public string WalletImage
        {
            get { return _WalletImage; }
            set { _WalletImage = value; }
        }
        private bool _IsFavourite = false;
        public bool IsFavourite
        {
            get { return _IsFavourite; }
            set { _IsFavourite = value; }
        }
        //AdditionalInfo1
        private string _AdditionalInfo1 = string.Empty;
        public string AdditionalInfo1
        {
            get { return _AdditionalInfo1; }
            set { _AdditionalInfo1 = value; }
        }
        //AdditionalInfo2
        private string _AdditionalInfo2 = string.Empty;
        public string AdditionalInfo2
        {
            get { return _AdditionalInfo2; }
            set { _AdditionalInfo2 = value; }
        }
        #endregion
    }

    public class AddTransactionSumCount 
    {
        #region "Properties"

        private Int64 _Count = 0;
        public Int64 Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private decimal _TotalSum = 0;
        public decimal TotalSum
        {
            get { return _TotalSum; }
            set { _TotalSum = value; }
        }
        #endregion
    }
}