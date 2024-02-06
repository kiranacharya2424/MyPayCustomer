using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;

namespace MyPay.Models.Request
{
    public class Req_Web_TicketCategory
    {
        private string _CategoryName = String.Empty;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private List<AddTicketCategory> _objData = new List<AddTicketCategory>();
        public List<AddTicketCategory> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}