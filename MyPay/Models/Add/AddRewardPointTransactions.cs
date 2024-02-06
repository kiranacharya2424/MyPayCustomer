using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddRewardPointTransactions:CommonAdd
    {
        #region "Properties"
        
        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
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

        //recordTotal
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
        #endregion
    }
}