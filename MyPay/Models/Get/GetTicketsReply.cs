using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetTicketsReply:CommonGet
    {
        #region "Enums"
        public enum Types
        {
            Sender = 1,
            Receiver = 2
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

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //CheckIsMainMessage
        private int _CheckIsMainMessage = 2;// (int)clsData.BooleanValue.Both;
        public int CheckIsMainMessage
        {
            get { return _CheckIsMainMessage; }
            set { _CheckIsMainMessage = value; }
        }

        //CheckIsAdmin
        private int _CheckIsAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckIsAdmin
        {
            get { return _CheckIsAdmin; }
            set { _CheckIsAdmin = value; }
        }

        //CheckIsNote
        private int _CheckIsNote = 2;// (int)clsData.BooleanValue.Both;
        public int CheckIsNote
        {
            get { return _CheckIsNote; }
            set { _CheckIsNote = value; }
        }

        #endregion
    }
}