using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_MEROTV_Packages_Lookup_Request
    {
        public string reference;
        public string session_id;
        public string service_slug = "merotv-v2";
        public string token;
        public string stb;

        /*
         {
	‘token’ :<Account_Token>,
	‘stb’:’<stb from detail fetch API>’,
	‘session_id : ‘<session_id from detail fetch API>’,
	‘service_slug’ : ’<service_slug>’
	‘reference’: <unique reference id>
}

         */

        //private string _reference = string.Empty;
        //public string reference
        //{
        //    get { return _reference; }
        //    set { _reference = value; }
        //}

        //private string _token = string.Empty;
        //public string token
        //{
        //    get { return _token; }
        //    set { _token = value; }
        //}
        //private string _stb = string.Empty;
        //public string stb
        //{
        //    get { return _stb; }
        //    set { _stb = value; }
        //}
    }
}