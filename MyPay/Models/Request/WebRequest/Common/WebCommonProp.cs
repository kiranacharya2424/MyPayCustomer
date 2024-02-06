using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest.Common
{
    public class WebCommonProp
    {
        //DeviceCode
        private string _DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //DeviceId
        private string _DeviceId = string.Empty;
        public string DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        //Version
        private string _Version = "1.0";
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        //PlatForm
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }

        //TimeStamp
        private string _TimeStamp = DateTime.Now.TimeOfDay.Ticks.ToString();
        public string TimeStamp
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

        //Hash
        private string _Hash = string.Empty;
        public string Hash
        {
            get { return _Hash; }
            set { _Hash = value; }
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
        //pin
        private string _Pin = "";
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        //VendorJsonLookup
        private string _VendorJsonLookup = "";
        public string VendorJsonLookup
        {
            get { return _VendorJsonLookup; }
            set { _VendorJsonLookup = value; }
        }
        //Take
        private string _Take = "10";
        public string Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        //Skip
        private string _Skip = "0";
        public string Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }

        private string _CouponCode = "";
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
    }
}