using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Airlines_Sector_Lookup : CommonGet
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

        // Inbound
        private List<sectors> _sectors = new List<sectors>();
        public List<sectors> sectors
        {
            get { return _sectors; }
            set { _sectors = value; }
        }
          

    }
    public class sectors
    {
        public string name { get; set; }
        public string code { get; set; }
        public bool is_national { get; set; }
        public bool is_international { get; set; }
        public bool is_active { get; set; }
    }

}