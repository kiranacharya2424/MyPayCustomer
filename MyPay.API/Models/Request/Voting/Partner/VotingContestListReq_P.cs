using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Partner
{

    public class VotingContestListReq_P
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? pageSize { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? pageNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string searchVal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string sortOrder { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string dateFrom { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string dateTo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string contestName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string sortBy { get; set; }
    }
}
