using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_Commit_Insurance_Neco_Requests:CommonResponse
    {
        //Id
        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
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
        //credits_available
        private string _credits_available = string.Empty;
        public string credits_available
        {
            get { return _credits_available; }
            set { _credits_available = value; }
        }
        //message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        //state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }

        //credits_consumed
        private string _credits_consumed = string.Empty;
        public string credits_consumed
        {
            get { return _credits_consumed; }
            set { _credits_consumed = value; }
        }
    }
}