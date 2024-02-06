using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddEventDetails : CommonAdd
    {
        bool DataRecieved = false;
        #region "Properties" 
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private Int64 _eventId = 0;
        public Int64 eventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }
        private String _eventName = string.Empty;
        public String eventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }
        private DateTime _eventDate = System.DateTime.UtcNow;
        public DateTime eventDate
        {
            get { return _eventDate; }
            set { _eventDate = value; }
        }
        private string _CheckeventDate = string.Empty;
        public string CheckeventDate
        {
            get { return _CheckeventDate; }
            set { _CheckeventDate = value; }
        }
        private String _eventDateString = string.Empty;
        public String eventDateString
        {
            get { return _eventDateString; }
            set { _eventDateString = value; }
        }
        private String _eventDateNepali = string.Empty;
        public String eventDateNepali
        {
            get { return _eventDateNepali; }
            set { _eventDateNepali = value; }
        }
        private String _eventStartTime = string.Empty;
        public String eventStartTime
        {
            get { return _eventStartTime; }
            set { _eventStartTime = value; }
        }
        private String _eventEndTime = string.Empty;
        public String eventEndTime
        {
            get { return _eventEndTime; }
            set { _eventEndTime = value; }
        }
        private String _eventDescription = string.Empty;
        public String eventDescription
        {
            get { return _eventDescription; }
            set { _eventDescription = value; }
        }
        private String _venueName = string.Empty;
        public String venueName
        {
            get { return _venueName; }
            set { _venueName = value; }
        }
        private String _venueAddress = string.Empty;
        public String venueAddress
        {
            get { return _venueAddress; }
            set { _venueAddress = value; }
        }
        private String _venueImagePath1 = string.Empty;
        public String venueImagePath1
        {
            get { return _venueImagePath1; }
            set { _venueImagePath1 = value; }
        }
        private Int64 _venueCapacity = 0;
        public Int64 venueCapacity
        {
            get { return _venueCapacity; }
            set { _venueCapacity = value; }
        }
        private String _parkingAvailable = string.Empty;
        public String parkingAvailable
        {
            get { return _parkingAvailable; }
            set { _parkingAvailable = value; }
        }
        private Int32 _latitude = 0;
        public Int32 latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        private Int32 _longitude = 0;
        public Int32 longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        private String _eventType = string.Empty;
        public String eventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }
        private String _organizerName = string.Empty;
        public String organizerName
        {
            get { return _organizerName; }
            set { _organizerName = value; }
        }
        private String _bannerImagePath = string.Empty;
        public String bannerImagePath
        {
            get { return _bannerImagePath; }
            set { _bannerImagePath = value; }
        }
        private Int32 _showArrivalTime = 0;
        public Int32 showArrivalTime
        {
            get { return _showArrivalTime; }
            set { _showArrivalTime = value; }
        }
        private String _arrivalTime = string.Empty;
        public String arrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }
        private Int32 _isSingleDayEvent = 0;
        public Int32 isSingleDayEvent
        {
            get { return _isSingleDayEvent; }
            set { _isSingleDayEvent = value; }
        }
        private String _merchantCode = string.Empty;
        public String merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        }
        private String _customerName = string.Empty;
        public String customerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        private String _customerMobile = string.Empty;
        public String customerMobile
        {
            get { return _customerMobile; }
            set { _customerMobile = value; }
        }
        private String _customerEmail = string.Empty;
        public String customerEmail
        {
            get { return _customerEmail; }
            set { _customerEmail = value; }
        }
        private Int64 _ticketCategoryId = 0;
        public Int64 ticketCategoryId
        {
            get { return _ticketCategoryId; }
            set { _ticketCategoryId = value; }
        }
        private String _ticketCategoryName = string.Empty;
        public String ticketCategoryName
        {
            get { return _ticketCategoryName; }
            set { _ticketCategoryName = value; }
        }
        private String _sectionName = string.Empty;
        public String sectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
        }
        private Decimal _ticketRate = 0;
        public Decimal ticketRate
        {
            get { return _ticketRate; }
            set { _ticketRate = value; }
        }
        private Int64 _noOfTicket = 0;
        public Int64 noOfTicket
        {
            get { return _noOfTicket; }
            set { _noOfTicket = value; }
        }
        private Decimal _totalPrice = 0;
        public Decimal totalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        private Int32 _paymentMethodId = 0;
        public Int32 paymentMethodId
        {
            get { return _paymentMethodId; }
            set { _paymentMethodId = value; }
        }
        private String _paymentMethodCode = string.Empty;
        public String paymentMethodCode
        {
            get { return _paymentMethodCode; }
            set { _paymentMethodCode = value; }
        }
        private String _paymentMethodName = string.Empty;
        public String paymentMethodName
        {
            get { return _paymentMethodName; }
            set { _paymentMethodName = value; }
        }
        private String _paymentMerchantId = string.Empty;
        public String paymentMerchantId
        {
            get { return _paymentMerchantId; }
            set { _paymentMerchantId = value; }
        }
        private bool _IsBooked = false;
        public bool IsBooked
        {
            get { return _IsBooked; }
            set { _IsBooked = value; }
        }
        private bool _IsPaymentDone = false;
        public bool IsPaymentDone
        {
            get { return _IsPaymentDone; }
            set { _IsPaymentDone = value; }
        }
        private String _OrderId = string.Empty;
        public String OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        private String _TransactionUniqueId = string.Empty;
        public String TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private String _TicketURL = string.Empty;
        public String TicketURL
        {
            get { return _TicketURL; }
            set { _TicketURL = value; }
        }
        private Int32 _Take = 0;
        public Int32 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        private Int32 _Skip = 0;
        public Int32 Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
        private Int32 _CheckDelete = 2;
        public Int32 CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }
        private Int32 _CheckActive = 2;
        public Int32 CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        private Int32 _CheckApprovedByAdmin = 2;
        public Int32 CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }
        private String _CheckCreatedDate = string.Empty;
        public String CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private String _StartDate = string.Empty;
        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _EndDate = string.Empty;
        public String EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private string _IndiaDate = string.Empty;
        public string IndiaDate
        {
            get { return _IndiaDate; }
            set { _IndiaDate = value; }
        }

        private Int32 _CheckPaymentDone = 2;
        public Int32 CheckPaymentDone
        {
            get { return _CheckPaymentDone; }
            set { _CheckPaymentDone = value; }
        }
        private Int32 _CheckIsBooked = 2;
        public Int32 CheckIsBooked
        {
            get { return _CheckIsBooked; }
            set { _CheckIsBooked = value; }
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
        private DateTime _ticketSentDatetime = System.DateTime.UtcNow;
        public DateTime ticketSentDatetime
        {
            get { return _ticketSentDatetime; }
            set { _ticketSentDatetime = value; }
        }

        #endregion

        #region "Add Delete Update Methods" 
        public bool Add()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_EventDetails_AddNew", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public bool Update()
        {
            try
            {
                DataRecieved = false;
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = SetObject();
                HT.Add("Id", Id);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_EventDetails_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Collections.Hashtable SetObject()
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("MemberId", MemberId);
            HT.Add("eventId", eventId);
            HT.Add("eventName", eventName);
            HT.Add("eventDate", eventDate);
            HT.Add("eventDateString", eventDateString);
            HT.Add("eventDateNepali", eventDateNepali);
            HT.Add("eventStartTime", eventStartTime);
            HT.Add("eventEndTime", eventEndTime);
            HT.Add("eventDescription", eventDescription);
            HT.Add("venueName", venueName);
            HT.Add("venueAddress", venueAddress);
            HT.Add("venueImagePath1", venueImagePath1);
            HT.Add("venueCapacity", venueCapacity);
            HT.Add("parkingAvailable", parkingAvailable);
            HT.Add("latitude", latitude);
            HT.Add("longitude", longitude);
            HT.Add("eventType", eventType);
            HT.Add("organizerName", organizerName);
            HT.Add("bannerImagePath", bannerImagePath);
            HT.Add("showArrivalTime", showArrivalTime);
            HT.Add("arrivalTime", arrivalTime);
            HT.Add("isSingleDayEvent", isSingleDayEvent);
            HT.Add("merchantCode", merchantCode);
            HT.Add("customerName", customerName);
            HT.Add("customerMobile", customerMobile);
            HT.Add("customerEmail", customerEmail);
            HT.Add("ticketCategoryId", ticketCategoryId);
            HT.Add("ticketCategoryName", ticketCategoryName);
            HT.Add("sectionName", sectionName);
            HT.Add("ticketRate", ticketRate);
            HT.Add("noOfTicket", noOfTicket);
            HT.Add("totalPrice", totalPrice);
            HT.Add("paymentMethodId", paymentMethodId);
            HT.Add("paymentMethodCode", paymentMethodCode);
            HT.Add("paymentMethodName", paymentMethodName);
            HT.Add("paymentMerchantId", paymentMerchantId);
            HT.Add("IsBooked", IsBooked);
            HT.Add("IsPaymentDone", IsPaymentDone);
            HT.Add("OrderId", OrderId);
            HT.Add("TransactionUniqueId", TransactionUniqueId);
            HT.Add("TicketURL", TicketURL);
            HT.Add("CreatedBy", CreatedBy);
            HT.Add("CreatedByName", CreatedByName);
            HT.Add("CreatedDate", CreatedDate);
            HT.Add("UpdatedDate", UpdatedDate);
            HT.Add("UpdatedBy", UpdatedBy);
            HT.Add("UpdatedByName", UpdatedByName);
            HT.Add("eventTermsAndCondition", eventTermsAndCondition);
            HT.Add("eventContactDtls", eventContactDtls);
            HT.Add("ticketSentDatetime", ticketSentDatetime);

            return HT;
        }
        #endregion

        #region "Get Methods" 
        public System.Data.DataTable GetList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("eventId", eventId);
                HT.Add("eventName", eventName);
                HT.Add("CheckeventDate", CheckeventDate);
                HT.Add("eventDateString", eventDateString);
                HT.Add("eventDateNepali", eventDateNepali);
                HT.Add("eventStartTime", eventStartTime);
                HT.Add("eventEndTime", eventEndTime);
                HT.Add("eventDescription", eventDescription);
                HT.Add("venueName", venueName);
                HT.Add("venueAddress", venueAddress);
                HT.Add("venueImagePath1", venueImagePath1);
                HT.Add("venueCapacity", venueCapacity);
                HT.Add("parkingAvailable", parkingAvailable);
                HT.Add("latitude", latitude);
                HT.Add("longitude", longitude);
                HT.Add("eventType", eventType);
                HT.Add("organizerName", organizerName);
                HT.Add("bannerImagePath", bannerImagePath);
                HT.Add("showArrivalTime", showArrivalTime);
                HT.Add("arrivalTime", arrivalTime);
                HT.Add("isSingleDayEvent", isSingleDayEvent);
                HT.Add("merchantCode", merchantCode);
                HT.Add("customerName", customerName);
                HT.Add("customerMobile", customerMobile);
                HT.Add("customerEmail", customerEmail);
                HT.Add("ticketCategoryId", ticketCategoryId);
                HT.Add("ticketCategoryName", ticketCategoryName);
                HT.Add("sectionName", sectionName);
                HT.Add("ticketRate", ticketRate);
                HT.Add("noOfTicket", noOfTicket);
                HT.Add("totalPrice", totalPrice);
                HT.Add("paymentMethodId", paymentMethodId);
                HT.Add("paymentMethodCode", paymentMethodCode);
                HT.Add("paymentMethodName", paymentMethodName);
                HT.Add("paymentMerchantId", paymentMerchantId);
                HT.Add("IsBooked", IsBooked);
                HT.Add("IsPaymentDone", IsPaymentDone);
                HT.Add("OrderId", OrderId);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("TicketURL", TicketURL);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckPaymentDone", CheckPaymentDone);
                HT.Add("CheckIsBooked", CheckIsBooked);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_EventDetails_Get", HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

        public bool GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("MemberId", MemberId);
                HT.Add("eventId", eventId);
                HT.Add("eventName", eventName);
                HT.Add("CheckeventDate", CheckeventDate);
                HT.Add("eventDateString", eventDateString);
                HT.Add("eventDateNepali", eventDateNepali);
                HT.Add("eventStartTime", eventStartTime);
                HT.Add("eventEndTime", eventEndTime);
                HT.Add("eventDescription", eventDescription);
                HT.Add("venueName", venueName);
                HT.Add("venueAddress", venueAddress);
                HT.Add("venueImagePath1", venueImagePath1);
                HT.Add("venueCapacity", venueCapacity);
                HT.Add("parkingAvailable", parkingAvailable);
                HT.Add("latitude", latitude);
                HT.Add("longitude", longitude);
                HT.Add("eventType", eventType);
                HT.Add("organizerName", organizerName);
                HT.Add("bannerImagePath", bannerImagePath);
                HT.Add("showArrivalTime", showArrivalTime);
                HT.Add("arrivalTime", arrivalTime);
                HT.Add("isSingleDayEvent", isSingleDayEvent);
                HT.Add("merchantCode", merchantCode);
                HT.Add("customerName", customerName);
                HT.Add("customerMobile", customerMobile);
                HT.Add("customerEmail", customerEmail);
                HT.Add("ticketCategoryId", ticketCategoryId);
                HT.Add("ticketCategoryName", ticketCategoryName);
                HT.Add("sectionName", sectionName);
                HT.Add("ticketRate", ticketRate);
                HT.Add("noOfTicket", noOfTicket);
                HT.Add("totalPrice", totalPrice);
                HT.Add("paymentMethodId", paymentMethodId);
                HT.Add("paymentMethodCode", paymentMethodCode);
                HT.Add("paymentMethodName", paymentMethodName);
                HT.Add("paymentMerchantId", paymentMerchantId);
                HT.Add("IsBooked", IsBooked);
                HT.Add("IsPaymentDone", IsPaymentDone);
                HT.Add("OrderId", OrderId);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("TicketURL", TicketURL);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                HT.Add("Take", Take);
                HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckPaymentDone", CheckPaymentDone);
                HT.Add("CheckIsBooked", CheckIsBooked);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_EventDetails_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"].ToString());
                    eventId = Convert.ToInt64(dt.Rows[0]["eventId"].ToString());
                    eventName = Convert.ToString(dt.Rows[0]["eventName"].ToString());
                    eventDate = Convert.ToDateTime(dt.Rows[0]["eventDate"].ToString());
                    eventDateString = Convert.ToString(dt.Rows[0]["eventDateString"].ToString());
                    eventDateNepali = Convert.ToString(dt.Rows[0]["eventDateNepali"].ToString());
                    eventStartTime = Convert.ToString(dt.Rows[0]["eventStartTime"].ToString());
                    eventEndTime = Convert.ToString(dt.Rows[0]["eventEndTime"].ToString());
                    eventDescription = Convert.ToString(dt.Rows[0]["eventDescription"].ToString());
                    venueName = Convert.ToString(dt.Rows[0]["venueName"].ToString());
                    venueAddress = Convert.ToString(dt.Rows[0]["venueAddress"].ToString());
                    venueImagePath1 = Convert.ToString(dt.Rows[0]["venueImagePath1"].ToString());
                    venueCapacity = Convert.ToInt64(dt.Rows[0]["venueCapacity"].ToString());
                    parkingAvailable = Convert.ToString(dt.Rows[0]["parkingAvailable"].ToString());
                    latitude = Convert.ToInt32(dt.Rows[0]["latitude"].ToString());
                    longitude = Convert.ToInt32(dt.Rows[0]["longitude"].ToString());
                    eventType = Convert.ToString(dt.Rows[0]["eventType"].ToString());
                    organizerName = Convert.ToString(dt.Rows[0]["organizerName"].ToString());
                    bannerImagePath = Convert.ToString(dt.Rows[0]["bannerImagePath"].ToString());
                    showArrivalTime = Convert.ToBoolean(dt.Rows[0]["showArrivalTime"].ToString()) ? 1 : 0;
                    arrivalTime = Convert.ToString(dt.Rows[0]["arrivalTime"].ToString());
                    isSingleDayEvent = Convert.ToBoolean(dt.Rows[0]["isSingleDayEvent"].ToString()) ? 1 : 0;
                    merchantCode = Convert.ToString(dt.Rows[0]["merchantCode"].ToString());
                    customerName = Convert.ToString(dt.Rows[0]["customerName"].ToString());
                    customerMobile = Convert.ToString(dt.Rows[0]["customerMobile"].ToString());
                    customerEmail = Convert.ToString(dt.Rows[0]["customerEmail"].ToString());
                    ticketCategoryId = Convert.ToInt64(dt.Rows[0]["ticketCategoryId"].ToString());
                    ticketCategoryName = Convert.ToString(dt.Rows[0]["ticketCategoryName"].ToString());
                    sectionName = Convert.ToString(dt.Rows[0]["sectionName"].ToString());
                    ticketRate = Convert.ToDecimal(dt.Rows[0]["ticketRate"].ToString());
                    noOfTicket = Convert.ToInt64(dt.Rows[0]["noOfTicket"].ToString());
                    totalPrice = Convert.ToDecimal(dt.Rows[0]["totalPrice"].ToString());
                    paymentMethodId = Convert.ToInt32(dt.Rows[0]["paymentMethodId"].ToString());
                    paymentMethodCode = Convert.ToString(dt.Rows[0]["paymentMethodCode"].ToString());
                    paymentMethodName = Convert.ToString(dt.Rows[0]["paymentMethodName"].ToString());
                    paymentMerchantId = Convert.ToString(dt.Rows[0]["paymentMerchantId"].ToString());
                    IsBooked = Convert.ToBoolean(dt.Rows[0]["IsBooked"].ToString());
                    IsPaymentDone = Convert.ToBoolean(dt.Rows[0]["IsPaymentDone"].ToString());
                    OrderId = Convert.ToString(dt.Rows[0]["OrderId"].ToString());
                    TransactionUniqueId = Convert.ToString(dt.Rows[0]["TransactionUniqueId"].ToString());
                    TicketURL = Convert.ToString(dt.Rows[0]["TicketURL"].ToString());
                    CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"].ToString());
                    CreatedByName = Convert.ToString(dt.Rows[0]["CreatedByName"].ToString());
                    CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                    UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());
                    UpdatedBy = Convert.ToInt64(dt.Rows[0]["UpdatedBy"].ToString());
                    UpdatedByName = Convert.ToString(dt.Rows[0]["UpdatedByName"].ToString());
                    IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());
                    IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[0]["IsApprovedByAdmin"].ToString());
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    Id = Convert.ToInt64(dt.Rows[0]["Id"].ToString());
                    eventTermsAndCondition = Convert.ToString(dt.Rows[0]["eventTermsAndCondition"].ToString());
                    eventContactDtls = Convert.ToString(dt.Rows[0]["eventContactDtls"].ToString());
                    DataRecieved = true;
                    ticketSentDatetime = Convert.ToDateTime(dt.Rows[0]["ticketSentDatetime"].ToString());
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        public System.Data.DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("MemberId", MemberId);
                HT.Add("eventId", eventId);
                HT.Add("eventName", eventName);
                HT.Add("CheckeventDate", CheckeventDate);
                HT.Add("eventDateString", eventDateString);
                HT.Add("eventDateNepali", eventDateNepali);
                HT.Add("eventStartTime", eventStartTime);
                HT.Add("eventEndTime", eventEndTime);
                HT.Add("eventDescription", eventDescription);
                HT.Add("venueName", venueName);
                HT.Add("venueAddress", venueAddress);
                HT.Add("venueImagePath1", venueImagePath1);
                HT.Add("venueCapacity", venueCapacity);
                HT.Add("parkingAvailable", parkingAvailable);
                HT.Add("latitude", latitude);
                HT.Add("longitude", longitude);
                HT.Add("eventType", eventType);
                HT.Add("organizerName", organizerName);
                HT.Add("bannerImagePath", bannerImagePath);
                HT.Add("showArrivalTime", showArrivalTime);
                HT.Add("arrivalTime", arrivalTime);
                HT.Add("isSingleDayEvent", isSingleDayEvent);
                HT.Add("merchantCode", merchantCode);
                HT.Add("customerName", customerName);
                HT.Add("customerMobile", customerMobile);
                HT.Add("customerEmail", customerEmail);
                HT.Add("ticketCategoryId", ticketCategoryId);
                HT.Add("ticketCategoryName", ticketCategoryName);
                HT.Add("sectionName", sectionName);
                HT.Add("ticketRate", ticketRate);
                HT.Add("noOfTicket", noOfTicket);
                HT.Add("totalPrice", totalPrice);
                HT.Add("paymentMethodId", paymentMethodId);
                HT.Add("paymentMethodCode", paymentMethodCode);
                HT.Add("paymentMethodName", paymentMethodName);
                HT.Add("paymentMerchantId", paymentMerchantId);
                HT.Add("IsBooked", IsBooked);
                HT.Add("IsPaymentDone", IsPaymentDone);
                HT.Add("OrderId", OrderId);
                HT.Add("TransactionUniqueId", TransactionUniqueId);
                HT.Add("TicketURL", TicketURL);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("UpdatedBy", UpdatedBy);
                HT.Add("UpdatedByName", UpdatedByName);
                //HT.Add("Take", Take);
                //HT.Add("Skip", Skip);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckPaymentDone", CheckPaymentDone);
                HT.Add("CheckIsBooked", CheckIsBooked);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckCreatedDate", CheckCreatedDate);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                HT.Add("Id", Id);

                dt = obj.GetDataFromStoredProcedure("sp_EventDetails_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonHelpers obj1 = new CommonHelpers();
                    System.Data.DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_EventDetails_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }
        #endregion

    }


}