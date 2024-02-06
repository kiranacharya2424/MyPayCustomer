using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetAllTickets: CommonResponse
    {
        //TicketList
        private List<AddTicket> _TicketList = new List<AddTicket>();
        public List<AddTicket> TicketList
        {
            get { return _TicketList; }
            set { _TicketList = value; }
        }
    }
}