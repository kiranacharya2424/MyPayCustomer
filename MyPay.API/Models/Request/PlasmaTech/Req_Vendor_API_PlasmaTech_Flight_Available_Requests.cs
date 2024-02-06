using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_Flight_Available_Requests : CommonProp
    {
       
        private string _UserID = string.Empty;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private string _AgencyId = string.Empty;
        public string AgencyId
        {
            get { return _AgencyId; }
            set { _AgencyId = value; }
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
        private string _FlightDate = string.Empty;
        public string FlightDate
        {
            get { return _FlightDate; }
            set { _FlightDate = value; }
        }
        private string _TripType = string.Empty;
        public string TripType
        {
            get { return _TripType; }
            set { _TripType = value; }
        }
        private string _ReturnDate = string.Empty;
        public string ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        private string _Nationality = string.Empty;
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
        private string _Adult = string.Empty;
        public string Adult
        {
            get { return _Adult; }
            set { _Adult = value; }
        }
        private string _Child = string.Empty;
        public string Child
        {
            get { return _Child; }
            set { _Child = value; }
        }
        private string _ClientIP = string.Empty;
        public string ClientIP
        {
            get { return _ClientIP; }
            set { _ClientIP = value; }
        }
    }
}