using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddTicket:CommonAdd
    {
        #region "Enum"
        public enum Priorities
        {
            Low = 1,
            Medium = 2,
            High = 3
        }

        public enum TicketStatus
        {
            Open = 1,
            Close = 2
        }
        #endregion

        #region "Properties"

        //TicketId
        private string _TicketId = string.Empty;
        public string TicketId
        {
            get { return _TicketId; }
            set { _TicketId = value; }
        }

        //TotalTicket
        private int _TotalTicket = 0;
        public int TotalTicket
        {
            get { return _TotalTicket; }
            set { _TotalTicket = value; }
        }

        //PendingTicket
        private int _PendingTicket = 0;
        public int PendingTicket
        {
            get { return _PendingTicket; }
            set { _PendingTicket = value; }
        }

        //CloseTicket
        private int _CloseTicket = 0;
        public int CloseTicket
        {
            get { return _CloseTicket; }
            set { _CloseTicket = value; }
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

        //MainMessage
        private string _MainMessage = string.Empty;
        public string MainMessage
        {
            get { return _MainMessage; }
            set { _MainMessage = value; }
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

        //CategoryName
        private string _CategoryName = string.Empty;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        //UpdatedMessage
        private string _UpdatedMessage = string.Empty;
        public string UpdatedMessage
        {
            get { return _UpdatedMessage; }
            set { _UpdatedMessage = value; }
        }

        //TicketTitle
        private string _TicketTitle = string.Empty;
        public string TicketTitle
        {
            get { return _TicketTitle; }
            set { _TicketTitle = value; }
        }

        //IsAttached
        private bool _IsAttached = false;
        public bool IsAttached
        {
            get { return _IsAttached; }
            set { _IsAttached = value; }
        }

        //IsSeen
        private bool _IsSeen = false;
        public bool IsSeen
        {
            get { return _IsSeen; }
            set { _IsSeen = value; }
        }

        //IsFavourite
        private bool _IsFavourite = false;
        public bool IsFavourite
        {
            get { return _IsFavourite; }
            set { _IsFavourite = value; }
        }

        //UserType
        private int _UserType = 0;
        public int UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        //IpAddress
        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        //CloseDate
        private DateTime _CloseDate = DateTime.UtcNow;
        public DateTime CloseDate
        {
            get { return _CloseDate; }
            set { _CloseDate = value; }
        }

        //CloseDateDt
        private string _CloseDateDt = string.Empty;
        public string CloseDateDt
        {
            get { return _CloseDateDt; }
            set { _CloseDateDt = value; }
        }

        #endregion
    }
}