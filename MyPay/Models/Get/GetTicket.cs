using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetTicket : CommonGet
    {
        #region "Properties"
        //TicketId
        private string _TicketId = string.Empty;
        public string TicketId
        {
            get { return _TicketId; }
            set { _TicketId = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //AssignedBy
        private Int64 _AssignedBy = 0;
        public Int64 AssignedBy
        {
            get { return _AssignedBy; }
            set { _AssignedBy = value; }
        }

        //AssignedTo
        private Int64 _AssignedTo = 0;
        public Int64 AssignedTo
        {
            get { return _AssignedTo; }
            set { _AssignedTo = value; }
        }

        //Priority
        private int _Priority = 0;
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        //CategoryId
        private int _CategoryId = 0;
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //CheckIsSeen
        private int _CheckIsSeen = 2;//(int)clsData.BooleanValue.Both;
        public int CheckIsSeen
        {
            get { return _CheckIsSeen; }
            set { _CheckIsSeen = value; }
        }

        //CheckIsFavourite
        private int _CheckIsFavourite = 2;// (int)clsData.BooleanValue.Both;
        public int CheckIsFavourite
        {
            get { return _CheckIsFavourite; }
            set { _CheckIsFavourite = value; }
        }

        //CheckIsAttached
        private int _CheckIsAttached = 2;// (int)clsData.BooleanValue.Both;
        public int CheckIsAttached
        {
            get { return _CheckIsAttached; }
            set { _CheckIsAttached = value; }
        }

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        //Name
        private string _TicketTitle = string.Empty;
        public string TicketTitle
        {
            get { return _TicketTitle; }
            set { _TicketTitle = value; }
        }
        #endregion
    }
}