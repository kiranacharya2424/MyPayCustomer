using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.School
{
    public class Res_Vendor_API_School_Lookup_Requests : CommonResponse
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
        public List<ResponseSchoolCategoryList> list { get; set; }
    }
    public class ResponseSchoolCategoryList
    {
        private string _idx = string.Empty;
        public string Idx
        {
            get { return _idx; }
            set { _idx = value; }
        }
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _info = string.Empty;
        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }
        private string _logo = string.Empty;
        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        private string _order = string.Empty;
        public string Order
        {
            get { return _order; }
            set { _order = value; }
        }
    }
}