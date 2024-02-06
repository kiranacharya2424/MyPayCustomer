using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddVotingList:CommonAdd
    {
        #region Enum
        public enum Type
        {
            VotingPackage = 1,
            Manual = 2
        }
        #endregion

        #region "Properties"

        //VotingCompetitionID
        private Int64 _VotingCompetitionId = 0;
        public Int64 VotingCompetitionId
        {
            get { return _VotingCompetitionId; }
            set { _VotingCompetitionId = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId=string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        //PackageID
        private Int64 _VotingPackageID=0;
        public Int64 VotingPackageID
        {
            get { return _VotingPackageID; }
            set { _VotingPackageID = value; }
        }

        //VotingCandidateUniqueId
        private string _VotingCandidateUniqueId = string.Empty;
        public string VotingCandidateUniqueId
        {
            get { return _VotingCandidateUniqueId; }
            set { _VotingCandidateUniqueId = value; }
        }

        //VotingCandidateName
        private string _VotingCandidateName = string.Empty;
        public string VotingCandidateName
        {
            get { return _VotingCandidateName; }
            set { _VotingCandidateName = value; }
        }

        //NoofVotes
        private int _NoofVotes = 0;
        public int NoofVotes
        {
            get { return _NoofVotes; }
            set { _NoofVotes = value; }
        }

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //MemberID
        private Int64 _MemberID = 0;
        public Int64 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        //MemberContactNumber
        private string _MemberContactNumber = string.Empty;
        public string MemberContactNumber
        {
            get { return _MemberContactNumber; }
            set { _MemberContactNumber = value; }
        }

        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        //PlatForm
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }

        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //IpAddress
        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
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

        //FreeVotes
        private int _FreeVotes = 0;
        public int FreeVotes
        {
            get { return _FreeVotes; }
            set { _FreeVotes = value; }
        }

        //PaidVotes
        private int _PaidVotes = 0;
        public int PaidVotes
        {
            get { return _PaidVotes; }
            set { _PaidVotes = value; }
        }

        #endregion
    }
}