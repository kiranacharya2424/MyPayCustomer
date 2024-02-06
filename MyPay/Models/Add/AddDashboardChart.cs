using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddDashboardChart:CommonAdd
    {
        #region "Properties"

        //TransactionDate
        private string _TransactionDate = string.Empty;
        public string TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }

        //CreditNetAmount
        private string _CreditNetAmount = string.Empty;
        public string CreditNetAmount
        {
            get { return _CreditNetAmount; }
            set { _CreditNetAmount = value; }
        }

        //DebitNetAmount
        private string _DebitNetAmount = string.Empty;
        public string DebitNetAmount
        {
            get { return _DebitNetAmount; }
            set { _DebitNetAmount = value; }
        }

        #endregion
    }
}