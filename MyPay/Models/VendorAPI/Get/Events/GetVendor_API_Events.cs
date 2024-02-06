using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.Events
{
    public class GetVendor_API_Events : CommonGet
    {

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
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


        private Data _data = new Data();
        public Data data
        {
            get { return _data; }
            set { _data = value; }
        }



    }
    public class Data {
        private List<Events> _items = new List<Events>();
        public List<Events> items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
    public class Events
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