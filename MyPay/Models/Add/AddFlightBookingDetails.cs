using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddFlightBookingDetails : CommonAdd
    {

        #region "Enums"
        public enum BookingTypes
        {
            One_Way = 1,
            Round_Trip = 2
        }
        #endregion


        #region "Properties"
        private Int64 _BookingId = 0;
        public Int64 BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        private string _Pax = string.Empty;
        public string Pax
        {
            get { return _Pax; }
            set { _Pax = value; }
        }

        private string _TripType = string.Empty;
        public string TripType
        {
            get { return _TripType; }
            set { _TripType = value; }
        }
        private string _FlightType = string.Empty;
        public string FlightType
        {
            get { return _FlightType; }
            set { _FlightType = value; }
        }
        private int _Adult = 0;
        public int Adult
        {
            get { return _Adult; }
            set { _Adult = value; }
        }
        private int _Child = 0;
        public int Child
        {
            get { return _Child; }
            set { _Child = value; }
        }
        private bool _IsInbound = false;
        public bool IsInbound
        {
            get { return _IsInbound; }
            set { _IsInbound = value; }
        }
        private string _Aircrafttype = string.Empty;
        public string Aircrafttype
        {
            get { return _Aircrafttype; }
            set { _Aircrafttype = value; }
        }
        private string _Airlinename = string.Empty;
        public string Airlinename
        {
            get { return _Airlinename; }
            set { _Airlinename = value; }
        }
        private string _Departure = string.Empty;
        public string Departure
        {
            get { return _Departure; }
            set { _Departure = value; }
        }
        private bool _Refundable = false;
        public bool Refundable
        {
            get { return _Refundable; }
            set { _Refundable = value; }
        }
        private decimal _Infantfare = 0;
        public decimal Infantfare
        {
            get { return _Infantfare; }
            set { _Infantfare = value; }
        }
        private string _Flightclasscode = string.Empty;
        public string Flightclasscode
        {
            get { return _Flightclasscode; }
            set { _Flightclasscode = value; }
        }
        private string _Currency = string.Empty;
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }
        private decimal _Faretotal = 0;
        public decimal Faretotal
        {
            get { return _Faretotal; }
            set { _Faretotal = value; }
        }
        private decimal _Adultfare = 0;
        public decimal Adultfare
        {
            get { return _Adultfare; }
            set { _Adultfare = value; }
        }
        private decimal _Childfare = 0;
        public decimal Childfare
        {
            get { return _Childfare; }
            set { _Childfare = value; }
        }
        private string _Departuretime = string.Empty;
        public string Departuretime
        {
            get { return _Departuretime; }
            set { _Departuretime = value; }
        }
        private decimal _Tax = 0;
        public decimal Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }
        private string _Airline = string.Empty;
        public string Airline
        {
            get { return _Airline; }
            set { _Airline = value; }
        }
        private string _Airlinelogo = string.Empty;
        public string Airlinelogo
        {
            get { return _Airlinelogo; }
            set { _Airlinelogo = value; }
        }
        private string _Flightno = string.Empty;
        public string Flightno
        {
            get { return _Flightno; }
            set { _Flightno = value; }
        }
        private decimal _Fuelsurcharge = 0;
        public decimal Fuelsurcharge
        {
            get { return _Fuelsurcharge; }
            set { _Fuelsurcharge = value; }
        }
        private string _Arrivaltime = string.Empty;
        public string Arrivaltime
        {
            get { return _Arrivaltime; }
            set { _Arrivaltime = value; }
        }
        private decimal _Resfare = 0;
        public decimal Resfare
        {
            get { return _Resfare; }
            set { _Resfare = value; }
        }
        private string _Freebaggage = string.Empty;
        public string Freebaggage
        {
            get { return _Freebaggage; }
            set { _Freebaggage = value; }
        }
        private string _Arrival = string.Empty;
        public string Arrival
        {
            get { return _Arrival; }
            set { _Arrival = value; }
        }
        private string _Flightid = string.Empty;
        public string Flightid
        {
            get { return _Flightid; }
            set { _Flightid = value; }
        }
        private DateTime _Flightdate = System.DateTime.UtcNow;
        public DateTime Flightdate
        {
            get { return _Flightdate; }
            set { _Flightdate = value; }
        }

        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        private string _ContactName = string.Empty;
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        private string _ContactPhone = string.Empty;
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }
        private string _ContactEmail = string.Empty;
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; }
        }
        private bool _IsFlightIssued=false;
        public bool IsFlightIssued
        {
            get { return _IsFlightIssued; }
            set { _IsFlightIssued = value; }
        }
        private string _PnrNumber = string.Empty;
        public string PnrNumber
        {
            get { return _PnrNumber; }
            set { _PnrNumber = value; }
        }
        private bool _IsFlightBooked = false;
        public bool IsFlightBooked
        {
            get { return _IsFlightBooked; }
            set { _IsFlightBooked = value; }
        }

        private string _LogIDs = string.Empty;
        public string LogIDs
        {
            get { return _LogIDs; }
            set { _LogIDs = value; }
        } 
        private string _ReturnDate = string.Empty;
        public string ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        private string _SectorFrom = string.Empty;
        public string SectorFrom
        {
            get { return _SectorFrom; }
            set { _SectorFrom = value; }
        }
        private string _SectorTo = string.Empty;
        public string SectorTo
        {
            get { return _SectorTo; }
            set { _SectorTo = value; }
        }
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private int _CheckDelete = 2;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }

        }
        private string _BookingCreateddt = string.Empty;
        public string BookingCreateddt
        {
            get { return _BookingCreateddt; }
            set { _BookingCreateddt = value; }

        }
        //BookingCreatedDate
        private DateTime _BookingCreatedDate = DateTime.UtcNow;
        public DateTime BookingCreatedDate
        {
            get { return _BookingCreatedDate; }
            set { _BookingCreatedDate = value; }
        }

        private string _UpdatedDatedt = string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }

        }
        private string _Flightdatedt = String.Empty;
        public string Flightdatedt
        {
            get { return _Flightdatedt; }
            set { _Flightdatedt = value; }
        }

        private List<AddFlightPassengersDetails> _PassengersDetails = new List<AddFlightPassengersDetails>();
        public List<AddFlightPassengersDetails> PassengersDetails
        {
            get { return _PassengersDetails; }
            set { _PassengersDetails = value; }

        }

        private string _BookingType = string.Empty;
        public string BookingType
        {
            get { return _BookingType; }
            set { _BookingType = value; }
        }
        private string _TripFareTotal = string.Empty;
        public string TripFareTotal
        {
            get { return _TripFareTotal; }

            set { _TripFareTotal = value; }

        }
        private string _TripTypeName = string.Empty;
        public string TripTypeName
        {
            get { return _TripTypeName; }
            set { _TripTypeName = value; }
        }
        private string _FlightTypeName = string.Empty;
        public string FlightTypeName
        {
            get { return _FlightTypeName; }
            set { _FlightTypeName = value; }
        }
        private string _TicketPDF = string.Empty;
        public string TicketPDF
        {
            get { return _TicketPDF; }
            set { _TicketPDF = value; }
        }
        #endregion
    }
}