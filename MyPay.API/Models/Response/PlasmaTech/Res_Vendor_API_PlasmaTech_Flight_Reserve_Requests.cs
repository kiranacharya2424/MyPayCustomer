﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.PlasmaTech
{
    public class Res_Vendor_API_PlasmaTech_Flight_Reserve_Requests : CommonResponse
    {
        public string Commission { get; set; }
        public string FlightID { get; set; }
        public string FlightFareTotal { get; set; }
        public string AdultCommission { get; set; }
        public string InboundCommission { get; set; }
        public string InboundAdultCommission { get; set; }
        public string ChildCommission { get; set; }
        public string TTL { get; set; }
        public string InboundChildCommission { get; set; }
        public string InboundFlightID { get; set; }
        public string InboundFlightFareTotal { get; set; }
        public string ApiMessage { get; set; }
    }
    public class PNRDetail
    {
        public string AirlineID { get; set; }
        public string FlightId { get; set; }
        public string PNRNO { get; set; }
        public string ReservationStatus { get; set; }
        public string TTLDate { get; set; }
        public string TTLTime { get; set; }
    }

    public class ReservationDetail
    {
        public PNRDetail PNRDetail { get; set; }
    }

    public class ReservationRootModel
    {
        public ReservationDetail ReservationDetail { get; set; }
    }
}