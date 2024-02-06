using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddAccountTransferLinkBank
    {
        //MerchantName  
        private string _MerchantName = "";
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }

        //MerchantId  
        private string _MerchantId = "";
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
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

        //Amount   
        private string _Amount = "";
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //TransactionRemark   
        private string _TransactionRemark = "";
        public string TransactionRemark
        {
            get { return _TransactionRemark; }
            set { _TransactionRemark = value; }
        }

        //SourceBankCode   
        private string _SourceBankCode = "";
        public string SourceBankCode
        {
            get { return _SourceBankCode; }
            set { _SourceBankCode = value; }
        }

        //DestinationBankCode   
        private string _DestinationBankCode = "";
        public string DestinationBankCode
        {
            get { return _DestinationBankCode; }
            set { _DestinationBankCode = value; }
        }

        //DestinationAccountNo   
        private string _DestinationAccountNo = "";
        public string DestinationAccountNo
        {
            get { return _DestinationAccountNo; }
            set { _DestinationAccountNo = value; }
        }

        //DestinationAccountName   
        private string _DestinationAccountName = "";
        public string DestinationAccountName
        {
            get { return _DestinationAccountName; }
            set { _DestinationAccountName = value; }
        }

        //CommissionAmount   
        private string _CommissionAmount = "";
        public string CommissionAmount
        {
            get { return _CommissionAmount; }
            set { _CommissionAmount = value; }
        }
    }
}