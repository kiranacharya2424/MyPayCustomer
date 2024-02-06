using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_LinkBankVerify : CommonProp
    {
        //OTPCode 
        private string _OTPCode = string.Empty;
        public string OTPCode
        {
            get { return _OTPCode; }
            set { _OTPCode = value; }
        }

        //BankCode
        private string _BankCode = string.Empty;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }

        //BankName
        private string _BankName = string.Empty;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        //LogoUrl
        private string _LogoUrl = string.Empty;
        public string LogoUrl
        {
            get { return _LogoUrl; }
            set { _LogoUrl = value; }
        }

        //AccountNumber
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
    }
}