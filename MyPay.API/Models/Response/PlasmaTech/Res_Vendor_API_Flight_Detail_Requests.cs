using MyPay.API.Models.PlasmaTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.PlasmaTech
{
    public class Res_Vendor_API_Flight_Detail_Requests : CommonResponse
    {
        public GetFlightDetailRes Data = new GetFlightDetailRes();
     
    }
    public class GetFlightDetailRes
    {
        public Availability Availability { get; set; }
    }

    public class Availability
    {
        public string Airline { get; set; }
        public string FlightDate { get; set; }
        public string FlightNo { get; set; }
        public string Departure { get; set; }
        public string DepartureTime { get; set; }
        public string Arrival { get; set; }
        public string ArrivalTime { get; set; }
        public string AircraftType { get; set; }
        public string Adult { get; set; }
        public string Child { get; set; }
        public string Infant { get; set; }
        public string FlightId { get; set; }
        public string FlightClassCode { get; set; }
        public string Currency { get; set; }
        public string AdultFare { get; set; }
        public string ChildFare { get; set; }
        public string InfantFare { get; set; }
        public string ResFare { get; set; }
        public string FuelSurcharge { get; set; }
        public string Tax { get; set; }
        public string Refundable { get; set; }
        public string FreeBaggage { get; set; }
        public string AgencyCommission { get; set; }
        public string ChildCommission { get; set; }
        public string CallingStationId { get; set; }
        public string CallingStation { get; set; }
    }
}
