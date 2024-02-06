using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.Events
{
    public class GetVendor_API_Events_Details : CommonGet
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

        public EventDetail _data = new EventDetail();
         public EventDetail data {
            get { return _data; }
            set { _data = value; }
        }
    }
    public class EventDetail {
        private int _eventId = 0;
        public int eventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }
        private string _eventName = string.Empty;
        public string eventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }
        private string _eventDate = string.Empty;
        public string eventDate
        {
            get { return _eventDate; }
            set { _eventDate = value; }
        }
        private string _eventDateString = string.Empty;
        public string eventDateString
        {
            get { return _eventDateString; }
            set { _eventDateString = value; }
        }
        private string _eventDateNepali = string.Empty;
        public string eventDateNepali
        {
            get { return _eventDateNepali; }
            set { _eventDateNepali = value; }
        }
        private string _eventStartTime = string.Empty;
        public string eventStartTime
        {
            get { return _eventStartTime; }
            set { _eventStartTime = value; }
        }
        private string _eventEndTime = string.Empty;
        public string eventEndTime
        {
            get { return _eventEndTime; }
            set { _eventEndTime = value; }
        }
        private string _eventDescription = string.Empty;
        public string eventDescription
        {
            get { return _eventDescription; }
            set { _eventDescription = value; }
        }
        private string _venueName = string.Empty;
        public string venueName
        {
            get { return _venueName; }
            set { _venueName = value; }
        }
        private string _venueAddress = string.Empty;
        public string venueAddress
        {
            get { return _venueAddress; }
            set { _venueAddress = value; }
        }
        private string _venueImagePath1 = string.Empty;
        public string venueImagePath1
        {
            get { return _venueImagePath1; }
            set { _venueImagePath1 = value; }
        }
        private string _venueImagePath2 = string.Empty;
        public string venueImagePath2
        {
            get { return _venueImagePath2; }
            set { _venueImagePath2 = value; }
        }
        private string _venueImagePath3 = string.Empty;
        public string venueImagePath3
        {
            get { return _venueImagePath3; }
            set { _venueImagePath3 = value; }
        }
        private string _venueCapacity = string.Empty;
        public string venueCapacity
        {
            get { return _venueCapacity; }
            set { _venueCapacity = value; }
        }
        private string _parkingAvailable = string.Empty;
        public string parkingAvailable
        {
            get { return _parkingAvailable; }
            set { _parkingAvailable = value; }
        }
        private decimal _latitude = 0;
        public decimal latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        private decimal _longitude = 0;
        public decimal longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        private string _eventType = string.Empty;
        public string eventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }
        private string _organizerName = string.Empty;
        public string organizerName
        {
            get { return _organizerName; }
            set { _organizerName = value; }
        }
        private string _bannerImagePath = string.Empty;
        public string bannerImagePath
        {
            get { return _bannerImagePath; }
            set { _bannerImagePath = value; }
        }
        private string _sliderImagePath = string.Empty;
        public string sliderImagePath
        {
            get { return _sliderImagePath; }
            set { _sliderImagePath = value; }
        }
        private string _promotionalBannerImagePath = string.Empty;
        public string promotionalBannerImagePath
        {
            get { return _promotionalBannerImagePath; }
            set { _promotionalBannerImagePath = value; }
        }
        private Boolean _showArrivalTime = false;
        public Boolean showArrivalTime
        {
            get { return _showArrivalTime; }
            set { _showArrivalTime = value; }
        }
        private string _arrivalTime = string.Empty;
        public string arrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }
        private Boolean _isSingleDayEvent = false;
        public Boolean isSingleDayEvent
        {
            get { return _isSingleDayEvent; }
            set { _isSingleDayEvent = value; }
        }
        private Boolean _isActive = false;
        public Boolean isActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private string _paymentMethodCode = string.Empty;
        public string paymentMethodCode
        {
            get { return _paymentMethodCode; }
            set { _paymentMethodCode = value; }
        }
        
        private string _paymentMethodName = string.Empty;
        public string paymentMethodName
        {
            get { return _paymentMethodName; }
            set { _paymentMethodName = value; }
        }
        private string _paymentMerchantId = string.Empty;
        public string paymentMerchantId
        {
            get { return _paymentMerchantId; }
            set { _paymentMerchantId = value; }
        }
        private string _ticketCategoryName = string.Empty;
        public string ticketCategoryName
        {
            get { return _ticketCategoryName; }
            set { _ticketCategoryName = value; }
        }
        private string _sectionName = string.Empty;
        public string sectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        private decimal _ticketRate = 0;
        public decimal ticketRate
        {
            get { return _ticketRate; }
            set { _ticketRate = value; }
        }
        private int _availableTickets = 0;
        public int availableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }

        private string _eventTermsAndCondition = string.Empty;
        public string eventTermsAndCondition
        {
            get { return _eventTermsAndCondition; }
            set { _eventTermsAndCondition = value; }
        }

        private string _eventContactDtls = string.Empty;
        public string eventContactDtls
        {
            get { return _eventContactDtls; }
            set { _eventContactDtls = value; }
        }
        
        private List<Sponser> _sponserList = new List<Sponser>();
        public List<Sponser> sponserList
        {
            get { return _sponserList; }
            set { _sponserList = value; }
        }

        private List<Guest> _guestList = new List<Guest>();
        public List<Guest> guestList
        {
            get { return _guestList; }
            set { _guestList = value; }
        }
        private List<PaymentMethod> _paymentMethodList = new List<PaymentMethod>();
        public List<PaymentMethod> paymentMethodList
        {
            get { return _paymentMethodList; }
            set { _paymentMethodList = value; }
        }
        private List<TicketCategory> _ticketCategoryList = new List<TicketCategory>();

        public List<TicketCategory> ticketCategoryList
        {
            get { return _ticketCategoryList; }
            set { _ticketCategoryList = value; }
        }
       
    }

    public class Sponser {
        private int _sponserId = 0;
        public int sponserId
        {
            get { return _sponserId; }
            set { _sponserId = value; }
        }
        private int _sponserTypeId = 0;
        public int sponserTypeId
        {
            get { return _sponserTypeId; }
            set { _sponserTypeId = value; }
        }
        private string _sponserName = string.Empty;
        public string sponserName
        {
            get { return _sponserName; }
            set { _sponserName = value; }
        }
        private string _sponserLogoImagePath = string.Empty;
        public string sponserLogoImagePath
        {
            get { return _sponserLogoImagePath; }
            set { _sponserLogoImagePath = value; }
        }
        private string _sponserTypeName = string.Empty;
        public string sponserTypeName
        {
            get { return _sponserTypeName; }
            set { _sponserTypeName = value; }
        }
       
    }
    public class Guest {
        private int _guestId = 0;
        public int guestId
        {
            get { return _guestId; }
            set { _guestId = value; }
        }
        private int _guestTypeId = 0;
        public int guestTypeId
        {
            get { return _guestTypeId; }
            set { _guestTypeId = value; }
        }
        private string _guestName = string.Empty;
        public string guestName
        {
            get { return _guestName; }
            set { _guestName = value; }
        }
        private string _speciality = string.Empty;
        public string speciality
        {
            get { return _speciality; }
            set { _speciality = value; }
        }
        private string _guestImagePath = string.Empty;
        public string guestImagePath
        {
            get { return _guestImagePath; }
            set { _guestImagePath = value; }
        }
        private string _guestTypeName = string.Empty;
        public string guestTypeName
        {
            get { return _guestTypeName; }
            set { _guestTypeName = value; }
        }
        
    }
    public class PaymentMethod {
        private int _paymentMethodId = 0;
        public int paymentMethodId
        {
            get { return _paymentMethodId; }
            set { _paymentMethodId = value; }
        }

        private string _paymentMethodCode = string.Empty;
        public string paymentMethodCode
        {
            get { return _paymentMethodCode; }
            set { _paymentMethodCode = value; }
        }
        private string _paymentMethodName = string.Empty;
        public string paymentMethodName
        {
            get { return _paymentMethodName; }
            set { _paymentMethodName = value; }
        }
        private string _paymentMerchantId = string.Empty;
        public string paymentMerchantId
        {
            get { return _paymentMerchantId; }
            set { _paymentMerchantId = value; }
        }
    }
    public class TicketCategory {
        private int _ticketCategoryId = 0;
        public int ticketCategoryId
        {
            get { return _ticketCategoryId; }
            set { _ticketCategoryId = value; }
        }
        private string _ticketCategoryName = string.Empty;
        public string ticketCategoryName
        {
            get { return _ticketCategoryName; }
            set { _ticketCategoryName = value; }
        }
        private string _sectionName = string.Empty;
        public string sectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        private decimal _ticketRate = 0;
        public decimal ticketRate
        {
            get { return _ticketRate; }
            set { _ticketRate = value; }
        }
        private int _availableTickets = 0;
        public int availableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }
        private List<Amenity> _amenityList = new List<Amenity>();
        public List<Amenity> amenityList
        {
            get { return _amenityList; }
            set { _amenityList = value; }
        }
        
    }
    public class Amenity {
        private int _amenityId = 0;
        public int amenityId
        {
            get { return _amenityId; }
            set { _amenityId = value; }
        }
        private string _amenityName = string.Empty;
        public string amenityName
        {
            get { return _amenityName; }
            set { _amenityName = value; }
        }
    }
}