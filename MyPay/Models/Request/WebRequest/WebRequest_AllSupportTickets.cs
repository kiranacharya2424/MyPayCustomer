using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_AllSupportTickets : WebCommonProp
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

        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
        //Name
        private string _TicketTitle = string.Empty;
        public string TicketTitle
        {
            get { return _TicketTitle; }
            set { _TicketTitle = value; }
        }
    }
}