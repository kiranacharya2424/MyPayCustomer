using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetFlightBookingDetails : CommonGet
    {

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //BookingId
        private Int64 _BookingId = 0;
        public Int64 BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }
        //LogIDs
        private string _LogIDs = string.Empty;
        public string LogIDs
        {
            get { return _LogIDs; }
            set { _LogIDs = value; }
        }

        //Flightid
        private string _Flightid = string.Empty;
        public string Flightid
        {
            get { return _Flightid; }
            set { _Flightid = value; }
        }

        //Flightno
        private string _Flightno = string.Empty;
        public string Flightno
        {
            get { return _Flightno; }
            set { _Flightno = value; }
        }


        //IsInbound
        private int _CheckInbound = 2;
        public int CheckInbound
        {
            get { return _CheckInbound; }
            set { _CheckInbound = value; }
        }
        //IsInbound
        private int _CheckTTL = 0;
        public int CheckTTL
        {
            get { return _CheckTTL; }
            set { _CheckTTL = value; }
        }
        //CheckFlightBooked
        private int _CheckFlightBooked = 2;
        public int CheckFlightBooked
        {
            get { return _CheckFlightBooked; }
            set { _CheckFlightBooked = value; }
        }
        //Departuretime
        private string _Departuretime = string.Empty;
        public string Departuretime
        {
            get { return _Departuretime; }
            set { _Departuretime = value; }
        }

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        //TripType
        private string _TripType = string.Empty;
        public string TripType
        {
            get { return _TripType; }
            set { _TripType = value; }
        }
        private string _PnrNumber = string.Empty;
        public string PnrNumber
        {
            get { return _PnrNumber; }
            set { _PnrNumber = value; }
        }
       // #endregion


        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("Flightid", Flightid);
                HT.Add("BookingId", BookingId);
                HT.Add("MemberId", MemberId);
                HT.Add("Departuretime", Departuretime);
                HT.Add("CheckInbound", CheckInbound);
                HT.Add("CheckFlightBooked", CheckFlightBooked);
                HT.Add("CheckTTL", CheckTTL);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("TripType", TripType);
                HT.Add("PnrNumber", PnrNumber);
                HT.Add("Flightno", Flightno);
                dt = obj.GetDataFromStoredProcedure("sp_FlightBookingDetails_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

    }
    public class GetFlightBookingDetails_Plasma : CommonGet
    {

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //BookingId
        private Int64 _BookingId = 0;
        public Int64 BookingId
        {
            get { return _BookingId; }
            set { _BookingId = value; }
        }
        //LogIDs
        private string _LogIDs = string.Empty;
        public string LogIDs
        {
            get { return _LogIDs; }
            set { _LogIDs = value; }
        }

        //Flightid
        private string _Flightid = string.Empty;
        public string Flightid
        {
            get { return _Flightid; }
            set { _Flightid = value; }
        }

        //Flightno
        private string _Flightno = string.Empty;
        public string Flightno
        {
            get { return _Flightno; }
            set { _Flightno = value; }
        }



        //IsInbound
        private int _CheckTTL = 0;
        public int CheckTTL
        {
            get { return _CheckTTL; }
            set { _CheckTTL = value; }
        }
        //CheckFlightBooked
        private int _CheckFlightBooked = 2;
        public int CheckFlightBooked
        {
            get { return _CheckFlightBooked; }
            set { _CheckFlightBooked = value; }
        }
        //Departuretime
        private string _Departuretime = string.Empty;
        public string Departuretime
        {
            get { return _Departuretime; }
            set { _Departuretime = value; }
        }

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        //TripType
        private string _TripType = string.Empty;
        public string TripType
        {
            get { return _TripType; }
            set { _TripType = value; }
        }


        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("Flightid", Flightid);
                HT.Add("BookingId", BookingId);
                HT.Add("MemberId", MemberId);
                HT.Add("Departuretime", Departuretime);
                HT.Add("CheckFlightBooked", CheckFlightBooked);
                HT.Add("CheckTTL", CheckTTL);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("TripType", TripType);
                dt = obj.GetDataFromStoredProcedure("sp_FlightBookingDetails_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
    }
}
        
