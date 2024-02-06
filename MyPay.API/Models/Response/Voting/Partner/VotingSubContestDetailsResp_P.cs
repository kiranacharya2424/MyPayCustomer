using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Voting.Partner
{
   
    public class Contestant
    {
        public int contestId { get; set; }
        public int subContestId { get; set; }
        public int contestantId { get; set; }
        public string contestantName { get; set; }
        public string photoImagePath { get; set; }
        public string contestantNumber { get; set; }
        public string contestantDescription { get; set; }
        public int age { get; set; }
        public object dateOfBirth { get; set; }
        public object country { get; set; }
        public string city { get; set; }
    }

    public class ContestPackage
    {
        public int contestId { get; set; }
        public int subContestId { get; set; }
        public int packageId { get; set; }
        public string packageName { get; set; }
        public object packageDescription { get; set; }
        public int minVote { get; set; }
        public int maxVote { get; set; }
        public double packagePricePerVote { get; set; }
        public double totalPackageAmount { get; set; }
        public bool isPaid { get; set; }
        public double defaultPricePerVote { get; set; }
    }

    public class SubContestDetails
    {
        public int contestId { get; set; }
        public int subContestId { get; set; }
        public string contestName { get; set; }
        public string contestDesc { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string contestBannerImgPath { get; set; }
        public string contestSliderImgPath { get; set; }
        public double defaultPricePerVote { get; set; }
        public string subContestName { get; set; }
        public string subContestDesc { get; set; }
        public string subContestBannerImgPath { get; set; }
        public string subContestSliderImgPath { get; set; }
        public List<Contestant> contestants { get; set; }
        public List<ContestPackage> contestPackages { get; set; }
    }

    public class VotingSubContestDetailsResp_P : VotingPartnerResp
    {
        //public bool success { get; set; }
        //public string message { get; set; }
        public SubContestDetails data { get; set; }
        public int ReponseCode { get; set; }
        //public List<object> errors { get; set; }
    }


}
