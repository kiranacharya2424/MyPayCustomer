using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddVotingCandidate : CommonAdd
    {
        #region "Properties"

        private Int64 _VotingCompetitionID = 0;
        public Int64 VotingCompetitionID
        {
            get { return _VotingCompetitionID; }
            set { _VotingCompetitionID = value; }
        }

        private int _ContentestNo = 0;
        public int ContentestNo
        {
            get { return _ContentestNo; }
            set { _ContentestNo = value; }
        }
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _City = string.Empty;
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _EmailID = string.Empty;
        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }
        private string _ContactNo = string.Empty;
        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        private Int64 _CountryId = 0;
        public Int64 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }
        private string _CountryName = string.Empty;
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
        }

        private string _UniqueId = string.Empty;
        public string UniqueId
        {
            get { return _UniqueId; }
            set { _UniqueId = value; }
        }

 
        private Int64 _ZipCode = 0;
        public Int64 ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        private Int32 _Gender = 0;
        public Int32 Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        //Age
        private string _Age = string.Empty;
        public string Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        private string _CompetitionName = string.Empty;
        public string CompetitionName
        {
            get { return _CompetitionName; }
            set { _CompetitionName = value; }
        }

        //TotalVotes
        private int _TotalVotes = 0;
        public int TotalVotes
        {
            get { return _TotalVotes; }
            set { _TotalVotes = value; }
        }

        //PaidVotes
        private int _PaidVotes = 0;
        public int PaidVotes
        {
            get { return _PaidVotes; }
            set { _PaidVotes = value; }
        }
        //FreeVotes
        private int _FreeVotes = 0;
        public int FreeVotes
        {
            get { return _FreeVotes; }
            set { _FreeVotes = value; }
        }
        //Rank
        private int _Rank = 0;
        public int Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        //TotalAmount
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        //GenderName
        private string _GenderName = string.Empty;
        public string GenderName
        {
            get { return _GenderName; }
            set { _GenderName = value; }
        }

        #endregion

        #region "Enums"
        public enum GenderTypes
        {
            Not_Selected = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }
        #endregion
    }
}