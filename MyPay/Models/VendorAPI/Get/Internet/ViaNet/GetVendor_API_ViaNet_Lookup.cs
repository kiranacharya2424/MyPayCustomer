using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_ViaNet_Lookup : CommonGet
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
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // _customer_id
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        } 
        // ViaNet_data
        private List<vianet_bills> _bills = new List<vianet_bills>();
        public List<vianet_bills> bills
        {

            get { return _bills; }

            set { _bills = value; }
        }
    }
    public class vianet_bills
    {
        // name
        private string _payment_id = string.Empty;
        public string  payment_id
        {
            get { return _payment_id; }
            set { _payment_id = value; }
        }
        private string _bill_date = string.Empty;
        public string bill_date
        {
            get { return _bill_date; }
            set { _bill_date = value; }
        }
        private string _service_details = string.Empty;
        public string service_details
        {
            get { return _service_details; }
            set { _service_details = value; }
        }
        private string _service_name = string.Empty;
        public string service_name
        {
            get { return _service_name; }
            set { _service_name = value; }
        }
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }

}