using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Add
{
    public class AddPlasmaTechIssueFlightDetails : CommonAdd
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
        public int MemberId { get; set; }
        public string FlightId { get; set; }
        public string ReturnFlightId { get; set; }
        public string IssueFrom { get; set; }
        public string AgencyName { get; set; }
        public string IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string FlightNo { get; set; }
        public string FlightDate { get; set; }
        public string Departure { get; set; }
        public string FlightTime { get; set; }
        public string TicketNo { get; set; }
        public string BarCodeValue { get; set; }
        public string BarcodeImage { get; set; }
        public string Arrival { get; set; }
        public string ArrivalTime { get; set; }
        public string Sector { get; set; }
        public string ClassCode { get; set; }
        public string Currency { get; set; }
        public string Fare { get; set; }
        public string Surcharge { get; set; }
        public string TaxCurrency { get; set; }
        public string Tax { get; set; }
        public string CommissionAmount { get; set; }
        public string Refundable { get; set; }
        public string Invoice { get; set; }
        public string ReportingTime { get; set; }
        public string FreeBaggage { get; set; }
        public string TicketPDF { get; set; }

    }
}
