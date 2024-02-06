using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_WebSurfer_UserList_Lookup : CommonGet
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
        public Websurfer_Customer customer { get; set; }
        public List<Websurfer_Connection> connection { get; set; }
        public int session_id { get; set; }
        public bool status { get; set; }
        public string Message { get; set; }

    }
    public class Websurfer_Customer
    {
        public string cuid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string status { get; set; }
    }

    public class Websurfer_Connection
    {
        public string username { get; set; }
        public string status { get; set; }
    }
     

}