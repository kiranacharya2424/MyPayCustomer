using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Bussewa_Lookup_Requests : CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _BoardingFrom = String.Empty;
        public string BoardingFrom
        {
            get { return _BoardingFrom; }
            set { _BoardingFrom = value; }
        }
        private string _ArrivalTo = String.Empty;
        public string ArrivalTo
        {
            get { return _ArrivalTo; }
            set { _ArrivalTo = value; }
        }
        private string _ShiftDayNight = String.Empty;
        public string ShiftDayNight
        {
            get { return _ShiftDayNight; }
            set { _ShiftDayNight = value; }
        }
        private string _Date = String.Empty;
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
    }
}