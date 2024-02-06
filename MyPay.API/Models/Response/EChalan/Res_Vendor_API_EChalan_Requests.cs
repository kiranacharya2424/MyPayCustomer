using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.EChalan
{
    public class Res_Vendor_API_EChalan_Requests : CommonResponse
    {

        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        } 
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        private string _Serial = string.Empty;
        public string Serial
        {
            get { return _Serial; }
            set { _Serial = value; }
        }
    }
}