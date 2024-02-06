//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPay
{
    using System;
    using System.Collections.Generic;
    
    public partial class FlightBookingDetail
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public long BookingId { get; set; }
        public string TripType { get; set; }
        public string FlightType { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public bool IsInbound { get; set; }
        public string Aircrafttype { get; set; }
        public string Airlinename { get; set; }
        public string Departure { get; set; }
        public bool Refundable { get; set; }
        public decimal Infantfare { get; set; }
        public string Flightclasscode { get; set; }
        public string Currency { get; set; }
        public decimal Faretotal { get; set; }
        public decimal Adultfare { get; set; }
        public decimal Childfare { get; set; }
        public string Departuretime { get; set; }
        public decimal Tax { get; set; }
        public string Airline { get; set; }
        public string Airlinelogo { get; set; }
        public string Flightno { get; set; }
        public decimal Fuelsurcharge { get; set; }
        public string Arrivaltime { get; set; }
        public decimal Resfare { get; set; }
        public string Freebaggage { get; set; }
        public string Arrival { get; set; }
        public string Flightid { get; set; }
        public System.DateTime Flightdate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public bool IsFlightIssued { get; set; }
        public string IpAddress { get; set; }
        public bool IsFlightBooked { get; set; }
        public string PnrNumber { get; set; }
        public System.DateTime BookingCreatedDate { get; set; }
        public string LogIDs { get; set; }
        public string ReturnDate { get; set; }
        public string SectorFrom { get; set; }
        public string SectorTo { get; set; }
        public string TicketPDF { get; set; }
    }
}
