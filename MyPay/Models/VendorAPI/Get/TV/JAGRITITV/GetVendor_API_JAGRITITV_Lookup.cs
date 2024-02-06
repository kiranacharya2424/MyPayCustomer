using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_JAGRITITV_Lookup : CommonGet
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
        // JAGRITITV_Lookup_Data
        private JAGRITI_TV_LookupDetail _detail = new JAGRITI_TV_LookupDetail();
        public JAGRITI_TV_LookupDetail detail
        {

            get { return _detail; }

            set { _detail = value; }
        }
    }
    public class JAGRITI_TV_Package
    {
        public string package_name { get; set; }
        public double amount { get; set; }
    }

    public class JAGRITI_TV_LookupDetail
    {
        public List<JAGRITI_TV_Package> packages { get; set; }
    }
}