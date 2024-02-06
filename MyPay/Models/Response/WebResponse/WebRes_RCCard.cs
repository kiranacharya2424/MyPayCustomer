using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_RCCard:WebCommonResponse
    {
        // TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        // WalletBalance
        private decimal _WalletBalance = 0;
        public decimal WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // Pin
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        // Serial
        private string _Serial = string.Empty;
        public string Serial
        {
            get { return _Serial; }
            set { _Serial = value; }
        }
    }
}