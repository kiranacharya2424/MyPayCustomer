using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetDealsandOffers: CommonGet
    {
        #region "Properties"

        //KycStatus
        private int _KycStatus = 0;
        public int KycStatus
        {
            get { return _KycStatus; }
            set { _KycStatus = value; }
        }

        //GenderStatus
        private int _GenderStatus = 0;
        public int GenderStatus
        {
            get { return _GenderStatus; }
            set { _GenderStatus = value; }
        }

        //ServiceId
        private int _ServiceId = 0;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }
        
        //Running
        private string _Running = "";
        public string Running
        {
            get { return _Running; }
            set { _Running = value; }
        }
        //PromoCode
        private string _PromoCode = "";
        public string PromoCode
        {
            get { return _PromoCode; }
            set { _PromoCode = value; }
        }

        private string _CoupounValue = string.Empty;
        public string CoupounValue { get { return _CoupounValue; } set { _CoupounValue = value; } }

        private string _CouponQuantity = string.Empty;
        public string CouponQuantity { get { return _CouponQuantity; } set { _CouponQuantity = value; } }

        #endregion
    }
}