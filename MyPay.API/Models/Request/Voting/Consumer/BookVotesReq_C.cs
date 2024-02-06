using MyPay.API.Models.Request.Voting.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Consumer
{
    public class BookVotesReq_C : BookVotesReq_P, ICommonVotingProps
    {
        public string DeviceCode { get; set; }
        public string DeviceId { get; set; }
        public string Version { get; set; }
        public string PlatForm { get; set; }
        public long TimeStamp { get; set; }
        public string Token { get; set; }
        public string UniqueCustomerId { get; set; }
        public string SecretKey { get; set; } = "";
        public string Mpin { get; set; } = "";
        public string Hash { get; set; } = "";
        public string MemberId { get; set; }
    }
}
