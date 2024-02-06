using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Dishome_Lookup : CommonGet
    {

        // error_code
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // Dishome_Lookup_Data
        private Dishome_Lookup_Data _data = new Dishome_Lookup_Data();
        public Dishome_Lookup_Data data
        {

            get { return _data; }

            set { _data = value; }
        }
        private Dishome_Error_Data _error_data = new Dishome_Error_Data();
        public Dishome_Error_Data error_data
        {

            get { return _error_data; }

            set { _error_data = value; }
        }
        private Dishome_Error_Data _details = new Dishome_Error_Data();
        public Dishome_Error_Data details
        {

            get { return _details; }

            set { _details = value; }
        }
    }
    public class Dishome_Lookup_Data
    {
        // name
        private string _quantity = string.Empty;
        public string quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        private string _customer_address = string.Empty;
        public string customer_address
        {
            get { return _customer_address; }
            set { _customer_address = value; }
        }
        private string _expiry_date = string.Empty;
        public string expiry_date
        {
            get { return _expiry_date; }
            set { _expiry_date = value; }
        }
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        private string _customer_type = string.Empty;
        public string customer_type
        {
            get { return _customer_type; }
            set { _customer_type = value; }
        }
        private string _package = string.Empty;
        public string package
        {
            get { return _package; }
            set { _package = value; }
        }
        private string _customer_status = string.Empty;
        public string customer_status
        {
            get { return _customer_status; }
            set { _customer_status = value; }
        }
        private string _mobile_no = string.Empty;
        public string mobile_no
        {
            get { return _mobile_no; }
            set { _mobile_no = value; }
        }



    }


    public class Dishome_Error_Data
    {
        private string _casid = string.Empty;
        public string casid
        {
            get { return _casid; }
            set { _casid = value; }
        }

    }
}