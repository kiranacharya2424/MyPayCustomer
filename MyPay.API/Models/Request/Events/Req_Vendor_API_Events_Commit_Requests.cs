﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Events_Commit_Requests : CommonProp
    {
        private string _Value = String.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _MerchantCode = string.Empty;
        public string MerchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }

        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        private string _TxnId = string.Empty;
        public string TxnId
        {
            get { return _TxnId; }
            set { _TxnId = value; }
        }

        private int _PaymentMethodId = 0;
        public int PaymentMethodId
        {
            get { return _PaymentMethodId; }
            set { _PaymentMethodId = value; }
        }
        private string _PaymentMerchantId = string.Empty;
        public string PaymentMerchantId
        {
            get { return _PaymentMerchantId; }
            set { _PaymentMerchantId = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private decimal _CouponDiscount = 0;
        public decimal CouponDiscount
        {
            get { return _CouponDiscount; }
            set { _CouponDiscount = value; }
        }

        private decimal _TransactionAmount = 0;
        public decimal TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }


        private string _EventId = String.Empty;
        public string EventId
        {
            get { return _EventId; }
            set { _EventId = value; }
        }
        private string _paymentMethodName = String.Empty;
        public string paymentMethodName
        {
            get { return _paymentMethodName; }
            set { _paymentMethodName = value; }
        }
        private string _ticketCategoryName = String.Empty;
        public string ticketCategoryName
        {
            get { return _ticketCategoryName; }
            set { _ticketCategoryName = value; }
        }
    }
}