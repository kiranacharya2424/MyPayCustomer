using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Voting.Partner
{
   

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public List<Item> items { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int filteredCount { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
    }

    public class Item
    {
        public int contestId { get; set; }
        public string contestName { get; set; }
        public string contestDesc { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string contestBannerImgPath { get; set; }
        public string contestSliderImgPath { get; set; }
        public double pricePerVote { get; set; }
        public string status { get; set; }
    }

    public class VotingContestListResp_P : VotingPartnerResp
    {
        //public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public int ReponseCode { get; set; }
        //public List<object> errors { get; set; }
        //public string errors { get; set; }
    }


}
