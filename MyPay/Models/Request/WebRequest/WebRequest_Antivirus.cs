using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_Antivirus : WebCommonProp
    {
        private string _Providerlist = string.Empty;
        public string Providerlist
        {
            get { return _Providerlist; }
            set { _Providerlist = value; }
        }
         
        private string _Value = string.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _CustomerCode = string.Empty;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
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
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}