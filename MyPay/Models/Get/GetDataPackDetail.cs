using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetDataPackDetail:CommonGet
    {
        #region "Properties"

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //PackageId
        private Int64 _PackageId = 0;
        public Int64 PackageId
        {
            get { return _PackageId; }
            set { _PackageId = value; }
        }

        //ProductCode
        private string _ProductCode = string.Empty;
        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }

        //ProductType
        private string _ProductType = string.Empty;
        public string ProductType
        {
            get { return _ProductType; }
            set { _ProductType = value; }
        }

        //CheckPurchased
        private int _CheckPurchased = 2;
        public int CheckPurchased
        {
            get { return _CheckPurchased; }
            set { _CheckPurchased = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        #endregion
    }
}