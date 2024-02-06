using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.WebSurfer
{
    public class Res_Vendor_API_WebSurfer_Lookup_Requests : CommonResponse
    {
        // session_id
        private string _Session_Id = string.Empty;
        public string Session_Id
        {
            get { return _Session_Id; }
            set { _Session_Id = value; }
        } 
        // Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        // Websurfer_data
        private List<Websurfer_packages> _packages = new List<Websurfer_packages>();
        public List<Websurfer_packages> packages
        {

            get { return _packages; }

            set { _packages = value; }
        }
    }
}