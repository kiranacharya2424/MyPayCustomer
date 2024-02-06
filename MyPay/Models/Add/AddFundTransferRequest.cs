using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddFundTransferRequest
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

        //MerchantTxnId
        private string _MerchantTxnId = "";
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
        }

        //MerchantProcessID
        private string _MerchantProcessID = "";
        public string MerchantProcessID
        {
            get { return _MerchantProcessID; }
            set { _MerchantProcessID = value; }
        }

        //Amount
        private string _Amount = "";
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //SourceBank
        private string _SourceBank = "";
        public string SourceBank
        {
            get { return _SourceBank; }
            set { _SourceBank = value; }
        }

        //SourceAccNo
        private string _SourceAccNo = "";
        public string SourceAccNo
        {
            get { return _SourceAccNo; }
            set { _SourceAccNo = value; }
        }

        //SourceAccName
        private string _SourceAccName = "";
        public string SourceAccName
        {
            get { return _SourceAccName; }
            set { _SourceAccName = value; }
        }

        //SourceCurrency
        private string _SourceCurrency = "";
        public string SourceCurrency
        {
            get { return _SourceCurrency; }
            set { _SourceCurrency = value; }
        }

        //DestinationBank
        private string _DestinationBank = "";
        public string DestinationBank
        {
            get { return _DestinationBank; }
            set { _DestinationBank = value; }
        }

        //DestinationAccNo
        private string _DestinationAccNo = "";
        public string DestinationAccNo
        {
            get { return _DestinationAccNo; }
            set { _DestinationAccNo = value; }
        }

        //DestinationAccName
        private string _DestinationAccName = "";
        public string DestinationAccName
        {
            get { return _DestinationAccName; }
            set { _DestinationAccName = value; }
        }

        //DestinationCurrency
        private string _DestinationCurrency = "";
        public string DestinationCurrency
        {
            get { return _DestinationCurrency; }
            set { _DestinationCurrency = value; }
        }

        //TransactionRemarks
        private string _TransactionRemarks = "";
        public string TransactionRemarks
        {
            get { return _TransactionRemarks; }
            set { _TransactionRemarks = value; }
        }

        //TransactionRemarks2
        private string _TransactionRemarks2 = "";
        public string TransactionRemarks2
        {
            get { return _TransactionRemarks2; }
            set { _TransactionRemarks2 = value; }
        }

        //TransactionRemarks3
        private string _TransactionRemarks3 = "";
        public string TransactionRemarks3
        {
            get { return _TransactionRemarks3; }
            set { _TransactionRemarks3 = value; }
        }

    }
}