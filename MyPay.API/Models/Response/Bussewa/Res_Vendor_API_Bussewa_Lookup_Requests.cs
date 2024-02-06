using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Antivirus.Bussewa
{
    public class Res_Vendor_API_Bussewa_Lookup_Requests : CommonResponse
    {
        // session_id
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        // Bussewa_data
        private List<Buses> _Buses = new List<Buses>();
        public List<Buses> Buses
        {

            get { return _Buses; }

            set { _Buses = value; }
        }
    }
    public class BusSeatLayout
    {
        public string DisplayName { get; set; }
        public string BookingStatus { get; set; }
    }

    public class Buses
    {
        public int NoOfColumn { get; set; }
        public bool LockStatus { get; set; }
        public string DepartureTime { get; set; }
        public string ImageList { get; set; }
        public string BusNo { get; set; }
        public string Operator { get; set; }
        public object TicketPrice { get; set; }
        public List<BusSeatLayout> SeatLayout { get; set; }
        public List<object> PassengerDetail { get; set; }
        public List<string> Amenities { get; set; }
        public int InputTypeCode { get; set; }
        public string Date { get; set; }
        public string Id { get; set; }
        public string BusType { get; set; }
        public bool MultiPrice { get; set; }
        public int Rating { get; set; }
        public string DateEn { get; set; }
    }

}