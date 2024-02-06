using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Electricity : WebCommonResponse
    {
        private string _consumer_name =  string.Empty;
        public string consumer_name
        {
            get { return _consumer_name; }
            set { _consumer_name = value; }
        }
        private string _total_due_amount = string.Empty;
        public string total_due_amount
        {
            get { return _total_due_amount; }
            set { _total_due_amount = value; }
        }
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        private List<due_bills> _due_bills = new List<due_bills>();
        public List<due_bills> due_bills
        {

            get { return _due_bills; }

            set { _due_bills = value; }
        }
    }
}