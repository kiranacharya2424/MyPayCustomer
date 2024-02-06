using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_Events_Requests : CommonResponse
    {

       
        // status
        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }


        private List<EventsData> _items = new List<EventsData>();
        public List<EventsData> items
        {
            get { return _items; }
            set { _items = value; }
        }

        // MaxTicketsAllowed
        private Int32 _MaxTicketsAllowed = 25;
        public Int32 MaxTicketsAllowed
        {
            get { return _MaxTicketsAllowed; }
            set { _MaxTicketsAllowed = value; }
        }

    }
   
       
    
    public class EventsData
    {
        private int _eventId = 0;
        public int eventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }

        private string _eventName = String.Empty;
        public string eventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }


        private string _eventDate = String.Empty;
        public string eventDate
        {
            get { return _eventDate; }
            set { _eventDate = value; }
        }
        private string _eventDateDT = String.Empty;
        public string eventDateDT
        {
            get { return _eventDateDT; }
            set { _eventDateDT = value; }
        }
        private string _eventDescription = String.Empty;
        public string eventDescription
        {
            get { return _eventDescription; }
            set { _eventDescription = value; }
        }
        private string _sliderImagePath = String.Empty;
        public string sliderImagePath
        {
            get { return _sliderImagePath; }
            set { _sliderImagePath = value; }
        }
        private string _promotionalBannerImagePath = String.Empty;
        public string promotionalBannerImagePath
        {
            get { return _promotionalBannerImagePath; }
            set { _promotionalBannerImagePath = value; }
        }
        private string _merchantCode = String.Empty;
        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        }

        private string _organizerName = String.Empty;
        public string organizerName
        {
            get { return _organizerName; }
            set { _organizerName = value; }
        }
        private string _venueAddress = String.Empty;
        public string venueAddress
        {
            get { return _venueAddress; }
            set { _venueAddress = value; }
        }
    }
}