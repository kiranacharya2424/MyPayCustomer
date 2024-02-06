using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddLoadWalletWithToken
    {
        //MerchantId 
        private string _MerchantId = "";
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //MerchantName 
        private string _MerchantName = "";
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }

        //Signature 
        private string _Signature = "";
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }

        //TransactionId 
        private string _TransactionId = "";
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //Token 
        private string _Token = "";
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        //TimeStamp 
        private string _TimeStamp = Common.Common.GetDateByArea(DateTime.UtcNow, "Nepal Standard Time").ToString("yyyy-MM-ddTHH:mm:ss.fff");
        public string TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }

        //Amount 
        private string _Amount = "";
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //TransactionRemark 
        private string _TransactionRemarks = "";
        public string TransactionRemarks
        {
            get { return _TransactionRemarks; }
            set { _TransactionRemarks = value; }
        }


        //BankCode 
        private string _BankCode = "";
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
    }
}