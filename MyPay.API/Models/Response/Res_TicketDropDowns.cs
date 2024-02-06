using MyPay.Models.Add;
using MyPay.Models.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_TicketDropDowns:CommonResponse
    {
        //Priority
        private List<EnumDropDowns> _Priority = new List<EnumDropDowns>();
        public List<EnumDropDowns> Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        //TicketStatus
        private List<EnumDropDowns> _TicketStatus = new List<EnumDropDowns>();
        public List<EnumDropDowns> TicketStatus
        {
            get { return _TicketStatus; }
            set { _TicketStatus = value; }
        }

        //Category
        private List<AddTicketCategory> _Category = new List<AddTicketCategory>();
        public List<AddTicketCategory> Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
    }
}