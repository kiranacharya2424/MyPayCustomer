using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_AddVoting:WebCommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //VotingCandidateUniqueId
        private string _VotingCandidateUniqueId;
        public string VotingCandidateUniqueId
        {
            get { return _VotingCandidateUniqueId; }
            set { _VotingCandidateUniqueId = value; }
        }
        //VotingPackageID

        private Int64 _VotingPackageID = 0;
        public Int64 VotingPackageID
        {
            get { return _VotingPackageID; }
            set { _VotingPackageID = value; }
        }

        private int _NoofVotes = 0;
        public int NoofVotes
        {
            get { return _NoofVotes; }
            set { _NoofVotes = value; }
        }
        private int _Type;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}