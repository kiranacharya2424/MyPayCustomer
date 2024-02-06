using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request
{
    //public enum CouponTypes
    //{
    //    EventPromoCode = 0,
    //    VotingPromoCode = 1,
    //    OtherPromoCode = 2
    //}
    public class Req_GenericCoupon : Req_CouponsScratchedApply
    {
        private CouponTypes _CouponType = CouponTypes.OtherPromoCode;
        public CouponTypes CouponType
        {
            get { return _CouponType; }
            set { _CouponType = value; }
        }

        public String MerchantCode { get; set; }
        public int EventId { get; set; }
        public decimal Amount { get; set; }
        public String CouponCode { get; set; }

        public string CustomerEmail { get; set; }

        public string ReferenceNo { get; set; }

    }
}
