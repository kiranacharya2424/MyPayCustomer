using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class CommonProp
    {
        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //DeviceId
        private string _DeviceId = string.Empty;
        public string DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        //Version
        private string _Version = string.Empty;
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        public string Version_Number = "";
       

        //PlatForm
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }

        //TimeStamp
        private long _TimeStamp = 0;
        public long TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }

        //Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        //CustomerId
        private string _UniqueCustomerId = string.Empty;
        public string UniqueCustomerId
        {
            get { return _UniqueCustomerId; }
            set { _UniqueCustomerId = value; }
        }

        //PaymentMode
        private string _PaymentMode = "";
        public string PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }

        //BankTransactionId
        private string _BankTransactionId = "";
        public string BankTransactionId
        {
            get { return _BankTransactionId; }
            set { _BankTransactionId = value; }
        }

        //SecretKey
        private string _SecretKey = "";
        public string SecretKey
        {
            get { return _SecretKey; }
            set { _SecretKey = value; }
        }

        //Mpin
        private string _Mpin = "";
        public string Mpin
        {
            get { return _Mpin; }
            set { _Mpin = value; }
        }
        //VendorJsonLookup
        private string _VendorJsonLookup = "";
        public string VendorJsonLookup
        {
            get { return _VendorJsonLookup; }
            set { _VendorJsonLookup = value; }
        }
        //CouponCode
        private string _CouponCode = "";
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //Hash
        private string _Hash = string.Empty;
        public string Hash
        {
            get { return _Hash; }
            set { _Hash = value; }
        }
    }
}