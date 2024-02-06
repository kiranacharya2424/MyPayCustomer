using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddDataPackDetail:CommonAdd
    {
        #region "Properties"

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //PriorityNo
        private int _PriorityNo = 0;
        public int PriorityNo
        {
            get { return _PriorityNo; }
            set { _PriorityNo = value; }
        }

        //ProductName
        private string _ProductName = string.Empty;
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //ShortDetail
        private string _ShortDetail = string.Empty;
        public string ShortDetail
        {
            get { return _ShortDetail; }
            set { _ShortDetail = value; }
        }

        //ProductCode
        private string _ProductCode = string.Empty;
        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //ProductType
        private string _ProductType = string.Empty;
        public string ProductType
        {
            get { return _ProductType; }
            set { _ProductType = value; }
        }

        //Validity
        private string _Validity = string.Empty;
        public string Validity
        {
            get { return _Validity; }
            set { _Validity = value; }
        }

        //IsPurchased
        private bool _IsPurchased = false;
        public bool IsPurchased
        {
            get { return _IsPurchased; }
            set { _IsPurchased = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //PackageId
        private Int64 _PackageId = 0;
        public Int64 PackageId
        {
            get { return _PackageId; }
            set { _PackageId = value; }
        }

        //ReceiptPDF
        private string _ReceiptPDF = string.Empty;
        public string ReceiptPDF
        {
            get { return _ReceiptPDF; }
            set { _ReceiptPDF = value; }
        }
        #endregion
    }
}