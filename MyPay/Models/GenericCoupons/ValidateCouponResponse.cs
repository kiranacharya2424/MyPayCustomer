using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.GenericCoupons
{
    public class ValidateCouponResponse
    {
        //New parameters added for 
        public bool Success = false;
        public string Message { get; set; }
        public bool status { get; set; }

        public string[] Errors { get; set; }
        public EventCouponData Data { get; set; }

    }


    public class EventCouponData
    {
        public decimal PreviousAmount { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal netPayableAmtAfterCouponApplied { get; set; }
    }
}
