using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Demat_Lookup : CommonGet
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
        // details
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
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
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // _customer_id
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        } 
        // Demat_data
        private List<Demat_Data> _data = new List<Demat_Data>();
        public List<Demat_Data> Demat_Data
        {

            get { return _data; }

            set { _data = value; }
        }
    }
    public class Demat_Data
    {
        // name
        private string _boid= string.Empty;
        public string boid
        {
            get { return _boid; }
            set { _boid = value; }
        }
        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        private string _customer_key = string.Empty;
        public string customer_key
        {
            get { return _customer_key; }
            set { _customer_key = value; }
        }
        private string _created_date = string.Empty;
        public string created_date
        {
            get { return _created_date; }
            set { _created_date = value; }
        }

        private string _is_closed = string.Empty;
        public string is_closed
        {
            get { return _is_closed; }
            set { _is_closed = value; }
        }
        private string _full_name = string.Empty;
        public string full_name
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        private string _fiscal_year = string.Empty;
        public string fiscal_year
        {
            get { return _fiscal_year; }
            set { _fiscal_year = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
    }

}