using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyPay.Models.Add.AddUser;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_Issue_Ticket_Requests : CommonProp
    {
       
        private string _FlightID = string.Empty;
        public string FlightID
        {
            get { return _FlightID; }
            set { _FlightID = value; }
        }
        private string _ReturnFlightID = string.Empty;
        public string ReturnFlightID
        {
            get { return _ReturnFlightID; }
            set { _ReturnFlightID = value; }
        } private string _ContactName = string.Empty;
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        } 
        private string _ContactEmail = string.Empty;
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; }
        }
        private string _ContactMobile = string.Empty;
        public string ContactMobile
        {
            get { return _ContactMobile; }
            set { _ContactMobile = value; }
        }
        //private string _PassengerDetail = string.Empty;
        //public string PassengerDetail
        //{
        //    get { return _PassengerDetail; }
        //    set { _PassengerDetail = value; }
        //}
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _FareTotal = String.Empty;
        public string FareTotal
        {
            get { return _FareTotal; }
            set { _FareTotal = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _BookingID = String.Empty;
        public string BookingID
        {
            get { return _BookingID; }
            set { _BookingID = value; }
        }

        /////---------for passengerDetail Adult--------///
        ///
        //public object PassengerDetails { get; set; }
        //private string _PassengerType = String.Empty;
        //public string PassengerType
        //{
        //    get { return _PassengerType; }
        //    set { _PassengerType = value; }
        //}
        //private string _Title = String.Empty;
        //public string Title
        //{
        //    get { return _Title; }
        //    set { _Title = value; }
        //}
        //private string _Gender = String.Empty;
        //public string Gender
        //{
        //    get { return _Gender; }
        //    set { _Gender = value; }
        //}
        //private string _FirstName = String.Empty;
        //public string FirstName
        //{
        //    get { return _FirstName; }
        //    set { _FirstName = value; }
        //}
        //private string _LastName = String.Empty;
        //public string LastName
        //{
        //    get { return _LastName; }
        //    set { _LastName = value; }
        //}
        //private string _Nationality = String.Empty;
        //public string Nationality
        //{
        //    get { return _Nationality; }
        //    set { _Nationality = value; }
        //}
        //private string _Remarks = String.Empty;
        //public string Remarks
        //{
        //    get { return _Remarks; }
        //    set { _Remarks = value; }
        //}
        ///////---------for passengerDetail Child--------///
        /////
        //private string _c_PassengerType = String.Empty;
        //public string c_PassengerType
        //{
        //    get { return _c_PassengerType; }
        //    set { _c_PassengerType = value; }
        //}
        //private string _c_Title = String.Empty;
        //public string c_Title
        //{
        //    get { return _c_Title; }
        //    set { _c_Title = value; }
        //}
        //private string _c_Gender = String.Empty;
        //public string c_Gender
        //{
        //    get { return _c_Gender; }
        //    set { _c_Gender = value; }
        //}
        //private string _c_FirstName = String.Empty;
        //public string c_FirstName
        //{
        //    get { return _c_FirstName; }
        //    set { _c_FirstName = value; }
        //}
        //private string _c_LastName = String.Empty;
        //public string c_LastName
        //{
        //    get { return _c_LastName; }
        //    set { _c_LastName = value; }
        //}
        //private string _c_Nationality = String.Empty;
        //public string c_Nationality
        //{
        //    get { return _c_Nationality; }
        //    set { _c_Nationality = value; }
        //}
        //private string _c_Remarks = String.Empty;
        //public string c_Remarks
        //{
        //    get { return _c_Remarks; }
        //    set { _c_Remarks = value; }
        //}
    }
}