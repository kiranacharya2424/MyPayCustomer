using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Voting.Partner
{
    public class VoteBookingCompletionData
    {
        public string orderId { get; set; }
        public string transactionId { get; set; }
        public string remarks { get; set; }
    }

    public class CompleteVoteBookingResp_P : VotingPartnerResp
    {
        //public bool success { get; set; }
        //public string message { get; set; }
        public VoteBookingCompletionData data { get; set; }
        //public List<object> errors { get; set; }
    }

}
