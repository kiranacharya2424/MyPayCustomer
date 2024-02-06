using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_Itinerary_Requests : CommonProp
    {
       
        private string _PnoNo = string.Empty;
        public string PnoNo
        {
            get { return _PnoNo; }
            set { _PnoNo = value; }
        }
        private string _TicketNo = string.Empty;
        public string TicketNo
        {
            get { return _TicketNo; }
            set { _TicketNo = value; }
        } 
        private string _AirlineId = string.Empty;
        public string AirlineId
        {
            get { return _AirlineId; }
            set { _AirlineId = value; }
        }
        private string _AgencyId = string.Empty;
        public string AgencyId
        {
            get { return _AgencyId; }
            set { _AgencyId = value; }
        }

    }
}