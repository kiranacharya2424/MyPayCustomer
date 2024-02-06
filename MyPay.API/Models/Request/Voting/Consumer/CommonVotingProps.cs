using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Voting.Consumer
{
  
    public interface ICommonVotingProps

    {
        string DeviceCode { get; set; }
        string DeviceId { get; set; }
        string Version { get; set; }
        string PlatForm { get; set; }
        long TimeStamp { get; set; }
        string Token { get; set; }
        string UniqueCustomerId { get; set; }
        string SecretKey { get; set; } 
        string Mpin { get; set; }
        string Hash { get; set; }

        string MemberId { get; set; }
    }
}
