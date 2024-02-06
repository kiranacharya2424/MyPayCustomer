using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Voting
{
    public class Res_VotingCompetition : CommonResponse
    {

        //AddOccupation
        private List<AddVotingCompetition> _data = new List<AddVotingCompetition>();
        public List<AddVotingCompetition> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}