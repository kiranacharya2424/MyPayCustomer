using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.TouristBus
{
    public class Req_Vendor_API_TouristBus_Routes_Lookup_Requests : CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
    public class Req_Vendor_API_TouristBus_Lookup_RequestsTrip : CommonProp
    {
        public string Reference { get; set; }
        public string from_location { get; set; }
        public string to_location { get; set; }
        public string date { get; set; }
    }
    public class Req_Vendor_API_TouristBus_Requestsbookseat : CommonProp
    {
        public string Reference { get; set; }
        public string seat { get; set; }
        public string totalseat { get; set; }
        public string busno { get; set; }
        public string from_location { get; set; }
        public string to_location { get; set; }
        public string date { get; set; }
        public string CompanyName { get; set; } 
        public string type { get; set; }
        public string BUS { get; set; }
        public string time { get; set; }
        public string Comission { get; set; }
        public string Cashback { get; set; }
        public string staffnum { get; set; }
    }

    public class Req_Vendor_API_TouristBus_Lookup_RequestsCancel : CommonProp
    {
        public string Reference { get; set; }
        public string holdingnumber { get; set; }

    }
    public class Req_Vendor_API_TouristBus_Lookup_RequestsPassenger : CommonProp
    {
        public string Reference { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string pickup { get; set; }
        public string drop { get; set; }
        public string TicketNo { get; set; }
        public string BusDetailId { get; set; }

    }
    public class Req_Vendor_API_TouristBus_Lookup_RequestsPayment : CommonProp
    {
        public string Reference { get; set; }
        public string TicketNo { get; set; }
        public string pidx { get; set; }
        public string cashbackamount { get; set; }
        public string amount { get; set; }
        public string commission { get; set; }
        public string BusDetailId { get; set; }

    }
}