using DocumentFormat.OpenXml.Spreadsheet;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyPay.Repository.RepKhalti;

namespace MyPay.Models.Response
{
    public class Res_Vendor_PlasmaTech_Flight_Available_Requests : Common_Response
    {
        //public Flightavailability Data = new Flightavailability();
        //public PlasmaAvailableFlights data
        //{
        //    get { return Data; }
        //    set { Data = value; }
        //}
        // Outbound
        private List<FlightOutbound> _FlightOutbound = new List<FlightOutbound>();
        public List<FlightOutbound> FlightOutbound
        {
            get { return _FlightOutbound; }
            set { _FlightOutbound = value; }
        }
        // Inbound
        private List<FlightInbound> _FlightInbound = new List<FlightInbound>();
        public List<FlightInbound> FlightInbound
        {
            get { return _FlightInbound; }
            set { _FlightInbound = value; }
        }
    }
    public class FlightInbound
    {
        public string Aircrafttype { get; set; }
        public string Airlinename { get; set; }
        public string Departure { get; set; }
        public bool Refundable { get; set; }
        public string Infantfare { get; set; }
        public string Flightclasscode { get; set; }
        public string Currency { get; set; }
        public string Faretotal { get; set; }
        public string Childfare { get; set; }
        public string Child { get; set; }
        public string Departuretime { get; set; }
        public string Tax { get; set; }
        public string Airline { get; set; }
        public string Adult { get; set; }
        public string Adultfare { get; set; }
        public string Airlinelogo { get; set; }
        public string Flightno { get; set; }
        public string Fuelsurcharge { get; set; }
        public string Arrivaltime { get; set; }
        public string Resfare { get; set; }
        public string Freebaggage { get; set; }
        public string Arrival { get; set; }
        public string Flightid { get; set; }
        public string Flightdate { get; set; }
        public string Infant { get; set; }
        // WalletSufficient
        private bool _IsWalletSufficient = false;
        public bool IsWalletBalanceSufficient
        {
            get { return _IsWalletSufficient; }
            set { _IsWalletSufficient = value; }
        }
        private string _Cashback = String.Empty;
        public string Cashback
        {
            get { return _Cashback; }
            set { _Cashback = value; }
        }
        private string _RewardPoints = String.Empty;
        public string RewardPoints
        {
            get { return _RewardPoints; }
            set { _RewardPoints = value; }
        }
        private string _ServiceCharge = String.Empty;
        public string ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }

        public string Agencycommission { get; set; }
        public string Childcommission { get; set; }
        public string Callingstationid { get; set; }
        public string Callingstation { get; set; }
    }

    public class FlightOutbound
    {
        public string Aircrafttype { get; set; }
        public string Airlinename { get; set; }
        public string Rtdepaure { get; set; }
        public string Departure { get; set; }
        public bool Refundable { get; set; }
        public string Infantfare { get; set; }
        public string Flightclasscode { get; set; }
        public string Currency { get; set; }
        public string Faretotal { get; set; }
        public string Childfare { get; set; }
        public string Child { get; set; }
        public string Childcommission { get; set; }
        public string Departuretime { get; set; }
        public string Tax { get; set; }
        public string Agencycommission { get; set; }
        public string Airline { get; set; }
        public string Adult { get; set; }
        public string Adultfare { get; set; }
        public string Airlinelogo { get; set; }
        public string Flightno { get; set; }
        public string Fuelsurcharge { get; set; }
        public string Arrivaltime { get; set; }
        public string Resfare { get; set; }
        public string Freebaggage { get; set; }
        public string Arrival { get; set; }
        public string Flightid { get; set; }
        public string Flightdate { get; set; }
        public string Infant { get; set; }
        public string Callingstationid { get; set; }
        public string Callingstation { get; set; }
        // WalletSufficient
        private bool _IsWalletSufficient = false;
        public bool IsWalletBalanceSufficient
        {
            get { return _IsWalletSufficient; }
            set { _IsWalletSufficient = value; }
        }
        private string _Cashback = String.Empty;
        public string Cashback
        {
            get { return _Cashback; }
            set { _Cashback = value; }
        }
        private string _RewardPoints = String.Empty;
        public string RewardPoints
        {
            get { return _RewardPoints; }
            set { _RewardPoints = value; }
        }
        private string _ServiceCharge = String.Empty;
        public string ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
    }
    public class Availability
    {
        private string airline;
        public string Airline
        {
            get => airline;
            set => airline = value;
        }
        private string airlineLogo;
        public string AirlineLogo
        {
            get => airlineLogo;
            set => airlineLogo = value;
        }
        private string flightDate;
        public string FlightDate
        {
            get => flightDate;
            set => flightDate = value;
        }
        private string flightNo;
        public string FlightNo
        {
            get => flightNo;
            set => flightNo = value;
        }
        private string departure;
        public string Departure
        {
            get => departure;
            set => departure = value;
        }
        private string departureTime;
        public string DepartureTime
        {
            get => departureTime;
            set => departureTime = value;
        }
        private string arrival;
        public string Arrival
        {
            get => arrival;
            set => arrival = value;
        }
        private string arrivalTime;
        public string ArrivalTime
        {
            get => arrivalTime;
            set => arrivalTime = value;
        }
        private string aircraftType;
        public string AircraftType
        {
            get => aircraftType;
            set => aircraftType = value;
        }
        private string adult;
        public string Adult
        {
            get => adult;
            set => adult = value;
        }
        private string child;
        public string Child
        {
            get => child;
            set => child = value;
        }
        private string infant;
        public string Infant
        {
            get => infant;
            set => infant = value;
        }
        private string flightId;
        public string FlightId
        {
            get => flightId;
            set => flightId = value;
        }
        private string flightClassCode;
        public string FlightClassCode
        {
            get => flightClassCode;
            set => flightClassCode = value;
        }
        private string currency;
        public string Currency
        {
            get => currency;
            set => currency = value;
        }
        private string adultFare;
        public string AdultFare
        {
            get => adultFare;
            set => adultFare = value;
        }
        private string childFare;
        public string ChildFare
        {
            get => childFare;
            set => childFare = value;
        }
        private string infantFare;
        public string InfantFare
        {
            get => infantFare;
            set => infantFare = value;
        }
        private string resFare;
        public string ResFare
        {
            get => resFare;
            set => resFare = value;
        }
        private string fuelSurcharge;
        public string FuelSurcharge
        {
            get => fuelSurcharge;
            set => fuelSurcharge = value;
        }
        private string tax;
        public string Tax
        {
            get => tax;
            set => tax = value;
        }
        private string refundable;
        public string Refundable
        {
            get => refundable;
            set => refundable = value;
        }
        private string freeBaggage;
        public string FreeBaggage
        {
            get => freeBaggage;
            set => freeBaggage = value;
        }
        private string agencyCommission;
        public string AgencyCommission
        {
            get => agencyCommission;
            set => agencyCommission = value;
        }
        private string childCommission;
        public string ChildCommission
        {
            get => childCommission;
            set => childCommission = value;
        }
        private string callingStationId;
        public string CallingStationId
        {
            get => callingStationId;
            set => callingStationId = value;
        }
        private string callingStation;
        public string CallingStation
        {
            get => callingStation;
            set => callingStation = value;
        }
    }

    public class Flightavailability
    {
        private Outbound outbound;


        public Outbound Outbound
        {
            get => outbound;
            set => outbound = value;
        }
        private Inbound inbound;
        public Inbound Inbound
        {
            get => inbound;
            set => inbound = value;
        }
    }

    public class Inbound
    {
        private List<Availability> availability;

        public List<Availability> Availability
        {
            get => availability;
            set => availability = value;
        }
    }

    public class Outbound
    {
        private List<Availability> availability;

        public List<Availability> Availability
        {
            get => availability;
            set => availability = value;
        }
    }

    public class PlasmaAvailableFlights
    {
        private Flightavailability flightavailability;

        public Flightavailability Flightavailability
        {
            get => flightavailability;
            set => flightavailability = value;
        }
    }
}
