using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.GenericCoupons
{
    public class ValidateCouponRequest
    {

        /* TO BE SENT TO EVENTS API FOR COUPON VALIDATION
         {
            "merchantCode": "MER000001",
            "customerEmail": "rahul.rajbanshi@email.com",
            "eventId": 37,
            "amount": 2000.00,
            "couponCode": "4145LBWE"
            }
         */

        public string MerchantCode { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerId { get; set; }

        public int EventID { get; set; }
        public string InstanceId { get; set; }
        public decimal Amount { get; set; }
        public string CouponCode { get; set; }


    }
}
