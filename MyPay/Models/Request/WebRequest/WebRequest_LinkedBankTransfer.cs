using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_LinkedBankTransfer : WebCommonProp
    {
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _BankId = "";
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Mpin = string.Empty;
        public string Mpin
        {
            get { return _Mpin; }
            set { _Mpin = value; }
        }
        private string _VendorType = string.Empty;
        public string VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
    }
}