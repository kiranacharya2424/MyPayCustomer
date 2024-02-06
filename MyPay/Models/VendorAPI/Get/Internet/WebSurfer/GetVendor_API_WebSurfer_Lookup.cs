using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_WebSurfer_Lookup : CommonGet
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
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        } 
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
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
        // Websurfer_data
        private List<Websurfer_packages> _packages = new List<Websurfer_packages>();
        public List<Websurfer_packages> packages
        {

            get { return _packages; }

            set { _packages = value; }
        }
    }
    public class Websurfer_packages
    {
        // _bouque
        private string _package_id = string.Empty;
        public string package_id
        {
            get { return _package_id; }
            set { _package_id = value; }
        }
        private string _package_name = string.Empty;
        public string package_name
        {
            get { return _package_name; }
            set { _package_name = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        } 
    }

}