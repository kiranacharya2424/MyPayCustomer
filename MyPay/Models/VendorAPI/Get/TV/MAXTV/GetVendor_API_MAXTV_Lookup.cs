using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_MAXTV_Lookup : CommonGet
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
        // Amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // Customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // cid_stb_smartcard
        private string _cid_stb_smartcard = string.Empty;
        public string cid_stb_smartcard
        {
            get { return _cid_stb_smartcard; }
            set { _cid_stb_smartcard = value; }
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
        // MAXTV_Lookup_Data
        private List<MAXTV_Lookup_Data> _tvs = new List<MAXTV_Lookup_Data>();
        public List<MAXTV_Lookup_Data>  tvs
        {

            get { return _tvs; }

            set { _tvs = value; }
        }
    }
    public class MAXTV_Lookup_Data
    {
        // name
        private string _stb_no = string.Empty;
        public string stb_no
        {
            get { return _stb_no; }
            set { _stb_no = value; }
        }
        private string _smartcard_no = string.Empty;
        public string smartcard_no
        {
            get { return _smartcard_no; }
            set { _smartcard_no = value; }
        }

    }
}