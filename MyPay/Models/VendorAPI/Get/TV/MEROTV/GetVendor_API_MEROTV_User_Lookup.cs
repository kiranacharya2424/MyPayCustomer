using MyPay.Models.Common;
using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_MEROTV_User_Lookup : WebCommonProp
    {
        // code
        private string _code = string.Empty;
        public string code
        {
            get { return _code; }
            set { _code = value; }
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
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
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
        // MEROTV_Lookup_Data -- OfferList
        private MEROTV_Lookup_Data_Customer _customer = new MEROTV_Lookup_Data_Customer();
        public MEROTV_Lookup_Data_Customer customer
        {

            get { return _customer; }

            set { _customer = value; }
        }

        private List<MEROTV_Lookup_Data_Connection> _connection = new List<MEROTV_Lookup_Data_Connection>();
        public List<MEROTV_Lookup_Data_Connection> connection
        {

            get { return _connection; }

            set { _connection = value; }
        }

    }
    public class MEROTV_Lookup_Data_Customer
    {
        // cuid
        private string _cuid = string.Empty;
        public string cuid
        {
            get { return _cuid; }
            set { _cuid = value; }
        }
        // first name
        private string _first_name = string.Empty;
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        // last name
        private string _last_name = string.Empty;
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        // status
        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

    }
    public class MEROTV_Lookup_Data_Connection
    {
        // stb
        private string _stb = string.Empty;
        public string stb
        {
            get { return _stb; }
            set { _stb = value; }
        }
        // status
        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}