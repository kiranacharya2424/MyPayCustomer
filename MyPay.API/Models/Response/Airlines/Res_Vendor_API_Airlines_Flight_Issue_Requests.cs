using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Flight_Issue_Requests : CommonResponse
    {

        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }

        public List<int> ResponseId { get; set; }
        public List<int> LogIds { get; set; }
        public List<IssueFlightPassengerResponse> Passengers { get; set; }
        public string ReturnDate { get; set; }
        public string FlightDate { get; set; }
        public string Commission { get; set; }
        public string TripType { get; set; }
        public string SectorFrom { get; set; }
        public string SectorTo { get; set; }
        public IssueFlightInboundResponse Inbound { get; set; }
        public string CreditsConsumed { get; set; }
        public string ApiMessage { get; set; }
        public IssueFlightOutboundResponse Outbound { get; set; }
    }

    public class IssueFlightPassengerResponse
    {
        public string Barcode { get; set; }
        public string TicketNo { get; set; }
        public string InboundTicketNo { get; set; }
        public string Lastname { get; set; }
        public string PassengerType { get; set; }
        public string Title { get; set; }
        public string InboundBarcode { get; set; }
        public string Gender { get; set; }
        public string Firstname { get; set; }
    }

    public class IssueFlightInboundResponse
    {
        public string PnrNo { get; set; }
        public string FareTotal { get; set; }
        public string AirlineName { get; set; }
        public string Airline { get; set; }
        public string DepartureTime { get; set; }
        public string FlightNo { get; set; }
        public string FlightClassCode { get; set; }
        public object Currency { get; set; }
        public string InboundReportingTime { get; set; }
        public string ArrivalTime { get; set; }
    }

    public class IssueFlightOutboundResponse
    {
        public string PnrNo { get; set; }
        public string FareTotal { get; set; }
        public string AirlineName { get; set; }
        public string Airline { get; set; }
        public string DepartureTime { get; set; }
        public string FlightNo { get; set; }
        public string ReportingTime { get; set; }
        public string FlightClassCode { get; set; }
        public object Currency { get; set; }
        public string ArrivalTime { get; set; }
    }
}