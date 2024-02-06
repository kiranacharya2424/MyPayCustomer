using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddVotingCompetition:CommonAdd
    {
        #region "Enums"

        public enum EventStatuses
        {
            Running = 1,
            Scheduled = 2,
            Closed = 3
        }

        #endregion

        #region "Properties"

        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //PublishTime
        private DateTime _PublishTime = DateTime.UtcNow;
        public DateTime PublishTime
        {
            get { return _PublishTime; }
            set { _PublishTime = value; }
        }

        //EndTime
        private DateTime _EndTime = DateTime.UtcNow;
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        //PricePerVote
        private decimal _PricePerVote = 0;
        public decimal PricePerVote
        {
            get { return _PricePerVote; }
            set { _PricePerVote = value; }
        }

        //UpdatedByName
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
        }

        //PublishTimeDt
        private string _PublishTimeDt = string.Empty;
        public string PublishTimeDt
        {
            get { return _PublishTimeDt; }
            set { _PublishTimeDt = value; }
        }

        //EndTimeDt
        private string _EndTimeDt = string.Empty;
        public string EndTimeDt
        {
            get { return _EndTimeDt; }
            set { _EndTimeDt = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        //UpdatedDateDt
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        //EventStatus
        private string _EventStatus = string.Empty;
        public string EventStatus
        {
            get { return _EventStatus; }
            set { _EventStatus = value; }
        }

        //TotalVotes
        private int _TotalVotes = 0;
        public int TotalVotes
        {
            get { return _TotalVotes; }
            set { _TotalVotes = value; }
        }

        //TotalFreeVotes
        private int _TotalFreeVotes = 0;
        public int TotalFreeVotes
        {
            get { return _TotalFreeVotes; }
            set { _TotalFreeVotes = value; }
        }

        //TotalAmount
        private decimal _TotalAmount = 0;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        //Order
        private int _Order = 0;
        public int Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        //EnumEventStatus
        private EventStatuses _EnumEventStatus = 0;
        public EventStatuses EnumEventStatus
        {
            get { return _EnumEventStatus; }
            set { _EnumEventStatus = value; }
        }

        #endregion
    }
}