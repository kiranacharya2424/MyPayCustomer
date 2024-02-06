using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Airlines_AddPassenger_Request : CommonGet
    {
        // message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        } 
        // detailsstring
        private string _detail;
        public string detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }


    public class FlightPassenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
    }
}