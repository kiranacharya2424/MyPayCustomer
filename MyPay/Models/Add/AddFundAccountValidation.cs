using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddFundAccountValidation
    {
        //MerchantId
        private string _MerchantId = "";
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //ApiUserName
        private string _ApiUserName = "";
        public string ApiUserName
        {
            get { return _ApiUserName; }
            set { _ApiUserName = value; }
        }

        //Signature
        private string _Signature = "";
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }

        //TimeStamp 
        private string _TimeStamp = Common.Common.GetDateByArea(DateTime.UtcNow, "Nepal Standard Time").ToString("yyyy-MM-ddTHH:mm:ss.fff");
        public string TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }

        //BankCode
        private string _BankCode = "";
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }

        //AccountNumber
        private string _AccountNumber = "";
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }

        //AccountName
        private string _AccountName = "";
        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }

    }
}