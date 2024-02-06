using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Voting.Partner

{ 
    public class VoteBookingData
    {
        public string orderId { get; set; }
        public string paymentMerchantId { get; set; }
        public string payableAmount { get; set; }
        public string signature { get; set; }
    }

    public class BookVotesResp_P: VotingPartnerResp
    {
        //public bool success { get; set; }
        //public string message { get; set; }
        public VoteBookingData data { get; set; }
        public int ReponseCode { get; set; }
        //public List<object> errors { get; set; }
    }


}
