using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Voting
{
    public class Res_VotingCandidates : CommonResponse
    {

        //AddOccupation
        private List<AddVotingCandidate> _data = new List<AddVotingCandidate>();
        public List<AddVotingCandidate> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}