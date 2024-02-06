using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Ride
{
    public class Res_Ride_Tootle_Lookup:CommonResponse
    {
        // product_identity
        private string _product_identity = string.Empty;
        public string product_identity
        {
            get { return _product_identity; }
            set { _product_identity = value; }
        }
        // name
        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        // number
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }

        // _gender
        private string _gender = string.Empty;
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        // Ride_data
        private List<Ride_data> _Ride_bills = new List<Ride_data>();
        public List<Ride_data> Ride_bills
        {

            get { return _Ride_bills; }

            set { _Ride_bills = value; }
        }
    }
    public class Ride_data
    {
        // name
        private string _product_identity= string.Empty;
        public string product_identity
        {
            get { return _product_identity; }
            set { _product_identity = value; }
        }
        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _gender = string.Empty;
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }

        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}