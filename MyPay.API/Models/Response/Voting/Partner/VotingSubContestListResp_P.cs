using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Voting.Partner
{
    public class SubContest
    {
        public int contestId { get; set; }
        public int subContestId { get; set; }
        public string subContestName { get; set; }
        public string subContestDesc { get; set; }
        public string subContestBannerImgPath { get; set; }
        public string subContestSliderImgPath { get; set; }
        public double pricePerVote { get; set; }
        public string status { get; set; }
    }

    public class VotingSubContestListResp_P : VotingPartnerResp
    {
        //public bool success { get; set; }
        //public string message { get; set; }
        public List<SubContest> data { get; set; }
        //public List<object> errors { get; set; }

        public int ReponseCode { get; set; }


    }


}
