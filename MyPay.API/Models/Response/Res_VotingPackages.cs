using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Voting
{
    public class Res_VotingPackages : CommonResponse
    {

        //AddOccupation
        private List<AddVotingPackages> _data = new List<AddVotingPackages>();
        public List<AddVotingPackages> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}