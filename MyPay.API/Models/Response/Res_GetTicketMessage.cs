using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetTicketMessage:CommonResponse
    {
        //TicketReplyList
        private List<AddTicketsReply> _TicketReplyList = new List<AddTicketsReply>();
        public List<AddTicketsReply> TicketReplyList
        {
            get { return _TicketReplyList; }
            set { _TicketReplyList = value; }
        }

        //TicketImageLink
        private string _TicketImageLink = "";
        public string TicketImageLink
        {
            get { return _TicketImageLink; }
            set { _TicketImageLink = value; }
        }
    }
}