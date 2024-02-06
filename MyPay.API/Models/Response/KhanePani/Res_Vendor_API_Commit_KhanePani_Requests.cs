using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_Vendor_API_Commit_KhanePani_Requests : CommonResponse
    {
        // UniqueTransactionId
        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        // Due_Amount
        private string _Due_Amount = string.Empty;
        public string Due_Amount
        {
            get { return _Due_Amount; }
            set { _Due_Amount = value; }
        }
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        // WalletBalance
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // Detail
        private Detail _Detail = new Detail();
        public Detail Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }
        // Extra_Data
        private Extra_Data _Extra_Data = new Extra_Data();
        public Extra_Data Extra_Data
        {
            get { return _Extra_Data; }
            set { _Extra_Data = value; }
        }

    }
    public class Detail
    {
        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        // Due_Amount
        private string _Due_Amount = string.Empty;
        public string Due_Amount
        {
            get { return _Due_Amount; }
            set { _Due_Amount = value; }
        }

    }
    public class Extra_Data
    {
      

    }
}