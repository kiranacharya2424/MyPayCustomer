using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetTransactionLimit:CommonGet
    {
        #region "Properties"

        //TransactionTransferType
        private int _TransactionTransferType = 0;
        public int TransactionTransferType
        {
            get { return _TransactionTransferType; }
            set { _TransactionTransferType = value; }
        }
        private int _IsKycVerified = 0;
        public int IsKycVerified
        {
            get { return _IsKycVerified; }
            set { _IsKycVerified = value; }
        }
        #endregion
    }
}