using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddNPS
    {
        //MerchantId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //MerchantName
        private string _MerchantName = string.Empty;
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }

        //MerchantTxnId
        private string _MerchantTxnId = string.Empty;
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
        }

        //TransactionAmount
        private string _TransactionAmount = string.Empty;
        public string TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }


        //TransactionRemarks
        private string _TransactionRemarks = string.Empty;
        public string TransactionRemarks
        {
            get { return _TransactionRemarks; }
            set { _TransactionRemarks = value; }
        }

        //Signature
        private string _Signature = string.Empty;
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }

        //CustomerName
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        //CustomerEmail
        private string _CustomerEmail = string.Empty;
        public string CustomerEmail
        {
            get { return _CustomerEmail; }
            set { _CustomerEmail = value; }
        }

        //CustomerMobile
        private string _CustomerMobile = string.Empty;
        public string CustomerMobile
        {
            get { return _CustomerMobile; }
            set { _CustomerMobile = value; }
        }

        //ValidityTime
        private string _ValidityTime = string.Empty;
        public string ValidityTime
        {
            get { return _ValidityTime; }
            set { _ValidityTime = value; }
        }

        //PaymentType
        private string _PaymentType = string.Empty;
        public string PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }

        //ChargeCategory
        private string _ChargeCategory = string.Empty;
        public string ChargeCategory
        {
            get { return _ChargeCategory; }
            set { _ChargeCategory = value; }
        }

        //FurtherCreditEnable
        private string _FurtherCreditEnabled = string.Empty;
        public string FurtherCreditEnabled
        {
            get { return _FurtherCreditEnabled; }
            set { _FurtherCreditEnabled = value; }
        }

        //FurtherCreditBank
        private string _FurtherCreditBank = string.Empty;
        public string FurtherCreditBank
        {
            get { return _FurtherCreditBank; }
            set { _FurtherCreditBank = value; }
        }

        //FurtherCreditAccName
        private string _FurtherCreditAccName = string.Empty;
        public string FurtherCreditAccName
        {
            get { return _FurtherCreditAccName; }
            set { _FurtherCreditAccName = value; }
        }

        //FurtherCreditAccNumber
        private string _FurtherCreditAccNumber = string.Empty;
        public string FurtherCreditAccNumber
        {
            get { return _FurtherCreditAccNumber; }
            set { _FurtherCreditAccNumber = value; }
        }



    }
}