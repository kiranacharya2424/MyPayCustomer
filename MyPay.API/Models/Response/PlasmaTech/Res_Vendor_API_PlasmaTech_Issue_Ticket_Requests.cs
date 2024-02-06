using MyPay.API.Models.Airlines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyPay.Models.Response.Plasma_Tech_Response_Model;

namespace MyPay.API.Models.PlasmaTech
{
    public class Res_Vendor_API_PlasmaTech_Issue_Ticket_Requests : CommonResponse
    {
        public object Data { get; set; }
        public string ApiMessage { get; set; }
    }
    public class IssueTicketRes
    {
        public Itinerary Itinerary { get; set; }
    }
    public class Itinerary
    {
        public Passenger Passenger { get; set; }
    }

    public class Passenger
    {
        public string Airline { get; set; }
        public string PnrNo { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PaxNo { get; set; }
        public string PaxType { get; set; }
        public string Nationality { get; set; }
        public string PaxId { get; set; }
        public string IssueFrom { get; set; }
        public string AgencyName { get; set; }
        public string IssueDate { get; set; }
        public string IssueBy { get; set; }
        public int FlightNo { get; set; }
        public string FlightDate { get; set; }
        public string Departure { get; set; }
        public string FlightTime { get; set; }
        public int TicketNo { get; set; }
        public string BarCodeValue { get; set; }
        public string BarcodeImage { get; set; }
        public string Arrival { get; set; }
        public string ArrivalTime { get; set; }
        public string Sector { get; set; }
        public string ClassCode { get; set; }
        public string Currency { get; set; }
        public int Fare { get; set; }
        public int Surcharge { get; set; }
        public string TaxCurrency { get; set; }
        public int Tax { get; set; }
        public int CommissionAmount { get; set; }
        public string Refundable { get; set; }
        public string Invoice { get; set; }
        public string ReportingTime { get; set; }
        public string FreeBaggage { get; set; }
    }



}