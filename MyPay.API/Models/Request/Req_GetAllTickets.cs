using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_GetAllTickets : CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
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
        //Name
        private string _TicketTitle = string.Empty;
        public string TicketTitle
        {
            get { return _TicketTitle; }
            set { _TicketTitle = value; }
        }
        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
    }
}