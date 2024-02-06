using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_AllCoupons : WebCommonProp
    {
        private string _Providerlist = string.Empty;
        public string Providerlist
        {
            get { return _Providerlist; }
            set { _Providerlist = value; }
        }

        //private string _MobileNo = string.Empty;
        //public string MobileNo
        //{
        //    get { return _MobileNo; }
        //    set { _MobileNo = value; }
        //}
        private string _Number = string.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private int _id = 0;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
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