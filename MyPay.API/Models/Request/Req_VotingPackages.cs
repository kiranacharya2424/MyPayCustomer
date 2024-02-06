using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Voting
{
    public class Req_VotingPackages : CommonProp
    {
        //VotingCompetitionID
        private Int64 _VotingCompetitionID = 0;
        public Int64 VotingCompetitionID
        {
            get { return _VotingCompetitionID; }
            set { _VotingCompetitionID = value; }
        }


    }
}