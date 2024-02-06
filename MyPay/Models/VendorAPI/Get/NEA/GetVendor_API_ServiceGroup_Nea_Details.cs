using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_ServiceGroup_Nea_Details : CommonGet
    {

        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // consumer_name
        private string _consumer_name = string.Empty;
        public string consumer_name
        {
            get { return _consumer_name; }
            set { _consumer_name = value; }
        }
        // total_due_amount
        private string _total_due_amount = string.Empty;
        public string total_due_amount
        {
            get { return _total_due_amount; }
            set { _total_due_amount = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
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
        // due_bills
        private List<due_bills> _due_bills = new List<due_bills>();
        public List<due_bills> due_bills
        {
            get { return _due_bills; }
            set { _due_bills = value; }
        }
    }

    public class  due_bills
    {
        // bill_amount
        private string _bill_amount = string.Empty;
        public string bill_amount
        {
            get { return _bill_amount; }
            set { _bill_amount = value; }
        }

        // date
        private string _bill_date = string.Empty;
        public string bill_date
        {
            get { return _bill_date; }
            set { _bill_date = value; }
        }

        // days
        private string _days = string.Empty;
        public string days
        {
            get { return _days; }
            set { _days = value; }
        }

        // payable_amount
        private string _payable_amount = string.Empty;
        public string payable_amount
        {
            get { return _payable_amount; }
            set { _payable_amount = value; }
        }
        // due_bill_of
        private string _due_bill_of = string.Empty;
        public string due_bill_of
        {
            get { return _due_bill_of; }
            set { _due_bill_of = value; }
        }
        // status
        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}