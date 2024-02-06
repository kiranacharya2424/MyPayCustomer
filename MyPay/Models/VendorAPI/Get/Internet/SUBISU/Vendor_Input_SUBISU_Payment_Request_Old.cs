using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_SUBISU_Payment_Request_Old
    {

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        //// session_id
        //private string _session_id = string.Empty;
        //public string session_id
        //{
        //    get { return _session_id; }
        //    set { _session_id = value; }
        //}
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // offer_name (i.e. plan_name)
        //private string _offer_name = string.Empty;
        //public string offer_name
        //{
        //    get { return _offer_name; }
        //    set { _offer_name = value; }
        //}

        // customer_id
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        // number
        private string _number = string.Empty;
        public string number
        {
            get { return _number; }
            set { _number = value; }
        }

        
    }
}