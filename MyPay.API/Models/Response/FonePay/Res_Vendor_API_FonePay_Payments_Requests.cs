using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.FonePay
{
    public class Res_Vendor_API_FonePay_Payments_Requests : CommonResponse
    {

        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        private string _transactionIdentifier = string.Empty;
        public string transactionIdentifier
        {
            get { return _transactionIdentifier; }
            set { _transactionIdentifier = value; }
        }
        private string _actionCode = string.Empty;
        public string actionCode
        {
            get { return _actionCode; }
            set { _actionCode = value; }
        }
        private string _traceId = string.Empty;
        public string traceId
        {
            get { return _traceId; }
            set { _traceId = value; }
        }
        private string _transmissionDateTime = string.Empty;
        public string transmissionDateTime
        {
            get { return _transmissionDateTime; }
            set { _transmissionDateTime = value; }
        }
        private string _retrievalReferenceNumber = string.Empty;
        public string retrievalReferenceNumber
        {
            get { return _retrievalReferenceNumber; }
            set { _retrievalReferenceNumber = value; }
        }
        private string _merchantCategoryCode = string.Empty;
        public string merchantCategoryCode
        {
            get { return _merchantCategoryCode; }
            set { _merchantCategoryCode = value; }
        }
        private string _merchantName = string.Empty;
        public string merchantName
        {
            get { return _merchantName; }
            set { _merchantName = value; }
        }
        private FonePay_CardAcceptor _cardAcceptor = new FonePay_CardAcceptor();
        public FonePay_CardAcceptor cardAcceptor
        {
            get { return _cardAcceptor; }
            set { _cardAcceptor = value; }
        }

        // WalletBalance
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
    }
}