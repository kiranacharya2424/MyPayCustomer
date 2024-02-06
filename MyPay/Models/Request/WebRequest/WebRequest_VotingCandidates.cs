using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_VotingCandidates:WebCommonProp
    {
        //VotingCompetitionID
        private Int64 _VotingCompetitionID = 0;
        public Int64 VotingCompetitionID
        {
            get { return _VotingCompetitionID; }
            set { _VotingCompetitionID = value; }
        }

        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}