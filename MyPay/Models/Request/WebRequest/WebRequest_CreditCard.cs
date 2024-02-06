using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_CreditCard : WebCommonProp
    { 
         
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Code = string.Empty;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        private string _CardNumber = string.Empty;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        private string _ServiceCharge = string.Empty;
        public string ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
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
        private Int64 _ServiceId = 0;
        public Int64 ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }
    }
}