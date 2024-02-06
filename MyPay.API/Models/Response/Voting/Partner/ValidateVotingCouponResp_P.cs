using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Voting.Partner
{
    public class CouponData
    {
        public double previousAmount { get; set; }
        public double discountAmt { get; set; }
        public double netPayableAmtAfterCouponApplied { get; set; }
    }

    public class ValidateVotingCouponResp_P : VotingPartnerResp
    {
        //public bool success { get; set; }
        //public string message { get; set; }
        public CouponData data { get; set; }
        //public List<object> errors { get; set; }
        public int ReponseCode { get; set; }
    }

}
