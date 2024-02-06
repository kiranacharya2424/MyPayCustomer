using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.PlasmaTech
{
    public class Res_Vendor_API_Sales_Report_Requests : CommonResponse
    {
        public SalesSummary Data = new SalesSummary();
    }
    public class SalesReportRes
    {
        public SalesSummary SalesSummary { get; set; }
    }

    public class SalesSummary
    {
        public List<TicketDetail> TicketDetail { get; set; }
    }

    public class TicketDetail
    {
        public string PnrNo { get; set; }
        public string Airline { get; set; }
        public string IssueDate { get; set; }
        public string FlightNo { get; set; }
        public string FlightDate { get; set; }
        public string SectorPair { get; set; }
        public string ClassCode { get; set; }
        public string TicketNo { get; set; }
        public string PassengerMame { get; set; }
        public string Nationality { get; set; }
        public string PaxType { get; set; }
        public string Currency { get; set; }
        public string Fare { get; set; }
        public string FSC { get; set; }
        public string TAX { get; set; }
    }

}
