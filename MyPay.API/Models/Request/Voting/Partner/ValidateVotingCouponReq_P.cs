using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Partner
{
    public class ValidateVotingCouponReq_P
    {
        public string customerId { get; set; }
        public string instanceId { get; set; }
        public double amount { get; set; }
        public string couponCode { get; set; }
    }
}
