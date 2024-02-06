using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Topup : WebCommonResponse
    {
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private decimal _ServiceChargeAmount = 0;
        public decimal ServiceChargeAmount
        {
            get { return _ServiceChargeAmount; }
            set { _ServiceChargeAmount = value; }
        }
        private decimal _CashbackAmount = 0;
        public decimal CashbackAmount
        {
            get { return _CashbackAmount; }
            set { _CashbackAmount = value; }
        }
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
    }
}