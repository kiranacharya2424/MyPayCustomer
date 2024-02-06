using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Voting.Partner
{
    public class VotingPartnerResp
    {
        public bool success { get; set; }
        public string Message { get; set; }
        public List<String> errors { get; set; }
        public bool status { get; set; }
    }
}