using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet
{
    public class Res_Vendor_API_NT_FTTH_Requests : CommonResponse
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
        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
        private string _CreditsConsumed = string.Empty;
        public string CreditsConsumed
        {
            get { return _CreditsConsumed; }
            set { _CreditsConsumed = value; }
        }
        private string _CreditsAvailable = string.Empty;
        public string CreditsAvailable
        {
            get { return _CreditsAvailable; }
            set { _CreditsAvailable = value; }
        }
        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private object _ExtraData = new object();
        public object ExtraData
        {
            get { return _ExtraData; }
            set { _ExtraData = value; }
        }
    }
}