using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Response
{
    public class Res_Vendor_PlasmaTech_GetFlightDetails_Requests : Common_Response
    {
        public string UniqueTransactionId { get; set; }
        public string TransactionDatetime { get; set; }
        public string WalletBalance { get; set; }
        public string ResponseId { get; set; }
        public string LogIds { get; set; }
        
        public FlightInbounds InBounds { get; set; }
        public string CreditsConsumed { get; set; }
        public string ApiMessage { get; set; }
        public FlightOutbounds OutBounds { get; set; }
        public string Data { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        //private FlightOutbound _Outbound;
        //public FlightOutbound Outbound
        //{
        //    get { return _Outbound; }
        //    set { _Outbound = value; }
        //}
        //// Inbound
        //private FlightInbound _Inbound;
        //public FlightInbound Inbound
        //{
        //    get { return _Inbound; }
        //    set { _Inbound = value; }
        //}
    }

    public class FlightDetailRootModel
    {
        public Availability Availability { get; set; }
    }

    public class FlightInbounds
    {
        public string PnrNo { get; set; }
        public string Faretotal { get; set; }
        public string Airlinename { get; set; }
        public string Airlinelogo { get; set; }
        public string Airline { get; set; }
        public string Departuretime { get; set; }
        public string Flightno { get; set; }
        public string Flightclasscode { get; set; }
        public string Currency { get; set; }
        public string Inboundreportingtime { get; set; }
        public string Arrivaltime { get; set; }
        public string Freebaggage { get;  set; }
        public string Refundable { get;  set; }
        public string Arrival { get;  set; }
        public string Departure { get;  set; }
        public string Flightdate { get; set; }
    }

    public class FlightOutbounds
    {
        public string PnrNo { get; set; }
        public string Faretotal { get; set; }
        public string Airlinename { get; set; }
        public string Airlinelogo { get; set; }
        public string Airline { get; set; }
        public string Departuretime { get; set; }
        public string Flightno { get; set; }
        public string Flightclasscode { get; set; }
        public string Currency { get; set; }
        public string Inboundreportingtime { get; set; }
        public string Arrivaltime { get; set; }
        public List<Passengers> Passengers { get; set; }
        public string MemberId { get; set; }
        public string Freebaggage { get; set; }
        public string Refundable { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public string Flightdate { get; set; }
    }

    public class Passengers
    {
        public string Lastname { get; set; }
        public string PaxType { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string Firstname { get; set; }
        public string Remarks { get; set; }
        public string Nationality { get; set; }
    }
}
