using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetTicketTypesCableCar : CommonGet
    {
        public string Message { get; set; }
        public string responseMessage { get; set; }
        public string Details { get; set; }
        public int ReponseCode { get; set; }
        public bool status { get; set; }
        public string ios_version { get; set; }
        public string AndroidVersion { get; set; }
        public string CouponCode { get; set; }
        public bool IsCouponUnlocked { get; set; }
        public string TransactionUniqueId { get; set; }
        public TicketType[] Data { get; set; }
        public class TicketType
        {
            public string TripType { get; set; }
            public string PassengerType { get; set; }
            public float Price { get; set; }
            public int NumberofTicket { get; set; }
            public int PassengerCount { get; set; }
            public float BaseRate { get; set; }
            public float Vat { get; set; }
            public float Lc { get; set; }
            public string PassengerTypeDesc { get; set; }
        }

        #region GetMethods


        #endregion
    }
}
