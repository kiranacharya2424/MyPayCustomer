using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddVotingPackages:CommonAdd
    {
        #region "Properties"

        //VotingCompetitionID
        private Int64 _VotingCompetitionID = 0;
        public Int64 VotingCompetitionID
        {
            get { return _VotingCompetitionID; }
            set { _VotingCompetitionID = value; }
        }

        //NoOfVotes
        private int _NoOfVotes = 0;
        public int NoOfVotes
        {
            get { return _NoOfVotes; }
            set { _NoOfVotes = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //UpdatedByName
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }


        //CompetitionName
        private string _CompetitionName = string.Empty;
        public string CompetitionName
        {
            get { return _CompetitionName; }
            set { _CompetitionName = value; }
        }

        //Type
        private bool _Type = false;
        public bool Type
        {
            get { return _Type; }
            set { _Type = value; }
        }



        #endregion
    }
}