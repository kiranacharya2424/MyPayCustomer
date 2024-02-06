using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_School_Lookup
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
        public List<SchoolCategoryList> list { get; set; }
        public List<SChoolDetailFromIDX> records { get; set; }
    }
    public class SchoolCategoryList
    {

        public string idx { get; set; }
        public string name { get; set; }
        public object info { get; set; }
        public string logo { get; set; }
        public int order { get; set; }
    }
    public class SChoolDetailFromIDX
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public string address { get; set; }
        public bool has_child { get; set; }
        public string cashback_upto { get; set; }
    }
}