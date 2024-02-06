using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Get;

namespace MyPay.API.Models.Demat
{
    public class Res_Vendor_API_Demat_Lookup_Requests : CommonResponse
    {
        // Demat_data
        private List<Demat_list> _Demat = new List<Demat_list>();
        public  List<Demat_list> Demat_data
        {

            get { return _Demat; }

            set { _Demat = value; }
        }
    }
    public class Demat_list
    {
        // name
        private string _boid = string.Empty;
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