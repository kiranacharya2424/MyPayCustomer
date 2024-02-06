using System;
using System.Collections.Generic;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Lookup_Requests : CommonResponse
    {
        // Inbound
        private List<FlightInbound> _FlightInbound = new List<FlightInbound>();
        public List<FlightInbound> FlightInbound
        {
            get { return _FlightInbound; }
            set { _FlightInbound = value; }
        }

        // Outbound
        private List<FlightOutbound> _FlightOutbound = new List<FlightOutbound>();
        public List<FlightOutbound> FlightOutbound
        {
            get { return _FlightOutbound; }
            set { _FlightOutbound = value; }
        }
        // BookingId
        private string _booking_id = string.Empty;
        public string BookingId
        {
            get { return _booking_id; }
            set { _booking_id = value; }
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
}