using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddBankTransactions : CommonAdd
    {
        #region Method
        public string GetBankTransactionSno()
        {
            string data = "0";
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "Select case when count(Id)=0 then 10000 else (count(Id)+10000) end from BankTransactions with(nolock)";
                Result = obj.GetScalarValueWithValue(str);
                data = Result;
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        #endregion
        #region "Enums"
        public enum Signs
        {
            Credit = 1,
            Debit = 2
        }

        public enum RewardTypes
        {
            Registration = 1,
            Kyc = 2
        }

        public enum Types
        {
            Paused = 1,
            Cancelled = 2,
            Completed = 3,
            Rejected = 4,
            Reciept_Unverified = 5,
            Recieved = 6,
            Started = 7,
            Pending = 8,
            Amount_NotMatched = 9,
            Refund = 10
        }
        #endregion"

        #region "Properties"

        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
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

        //RecieverName
        private string _RecieverName = string.Empty;
        public string RecieverName
        {
            get { return _RecieverName; }
            set { _RecieverName = value; }
        }
        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
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

        //SenderBankName
        private string _SenderBankName = string.Empty;
        public string SenderBankName
        {
            get { return _SenderBankName; }
            set { _SenderBankName = value; }
        }
        //SenderBranchName
        private string _SenderBranchName = string.Empty;
        public string SenderBranchName
        {
            get { return _SenderBranchName; }
            set { _SenderBranchName = value; }
        }
        //RecieverBankName
        private string _RecieverBankName = string.Empty;
        public string RecieverBankName
        {
            get { return _RecieverBankName; }
            set { _RecieverBankName = value; }
        }
        //RecieverBranchName
        private string _RecieverBranchName = string.Empty;
        public string RecieverBranchName
        {
            get { return _RecieverBranchName; }
            set { _RecieverBranchName = value; }
        }
        //TransferType
        private int _TransferType = 0;
        public int TransferType
        {
            get { return _TransferType; }
            set { _TransferType = value; }
        }
        //ServiceCharge
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }

        //_NetAmount
        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        //ResponseCode
        private string _ResponseCode = string.Empty;
        public string ResponseCode
        {
            get { return _ResponseCode; }
            set { _ResponseCode = value; }
        }

        //Purpose
        private string _Purpose = string.Empty;
        public string Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //VendorTransactionId
        private string _VendorTransactionId = string.Empty;
        public string VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }

        //BatchId
        private string _BatchId = string.Empty;
        public string BatchId
        {
            get { return _BatchId; }
            set { _BatchId = value; }
        }

        //InstructionId
        private string _InstructionId = string.Empty;
        public string InstructionId
        {
            get { return _InstructionId; }
            set { _InstructionId = value; }
        }

        //CreditStatus
        private string _CreditStatus = string.Empty;
        public string CreditStatus
        {
            get { return _CreditStatus; }
            set { _CreditStatus = value; }
        }

        //DebitStatus
        private string _DebitStatus = string.Empty;
        public string DebitStatus
        {
            get { return _DebitStatus; }
            set { _DebitStatus = value; }
        }

        //GatewayStatus
        private string _GatewayStatus = string.Empty;
        public string GatewayStatus
        {
            get { return _GatewayStatus; }
            set { _GatewayStatus = value; }
        }

        //ParentTransactionId
        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        //SignName
        private string _SignName = string.Empty;
        public string SignName
        {
            get { return _SignName; }
            set { _SignName = value; }
        }

        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        //Sign
        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        //Sign
        private int _recordTotal = 0;
        public int recordTotal
        {
            get { return _recordTotal; }
            set { _recordTotal = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //CurrentBalance
        private decimal _CurrentBalance = 0;
        public decimal CurrentBalance
        {
            get { return _CurrentBalance; }
            set { _CurrentBalance = value; }
        }


        //CreatedDatedt
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        //UpdatedDatedt
        private string _UpdatedDatedt= string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }
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

        //AmountSum
        private decimal _AmountSum = 0;
        public decimal AmountSum
        {
            get { return _AmountSum; }
            set { _AmountSum = value; }
        }

        //FilterTotalCount
        private int _FilterTotalCount = 0;
        public int FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }

        //StatusName
        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        //VendorType
        private Int32 _VendorType = 0;
        public Int32 VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }
        //VendorTypeName
        private string _VendorTypeName = "";
        public string VendorTypeName
        {
            get { return _VendorTypeName; }
            set { _VendorTypeName = value; }
        }
        #endregion
    }
}