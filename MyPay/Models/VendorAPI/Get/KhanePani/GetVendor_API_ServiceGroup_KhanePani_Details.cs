using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_ServiceGroup_KhanePani_Details : CommonGet
    {


        // customer_code
        private string _customer_code =string.Empty;
        public string customer_code
        {
            get { return _customer_code; }
            set { _customer_code = value; }
        }
        // customer_name
        private string _customer_name =string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // address
        private string _address =string.Empty;
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        // mobile_number
        private string _mobile_number = string.Empty;
        public string mobile_number
        {
            get { return _mobile_number; }
            set { _mobile_number = value; }
        }
        // current_month_dues
        private string _current_month_dues =string.Empty;
        public string current_month_dues
        {
            get { return _current_month_dues; }
            set { _current_month_dues = value; }
        }


        // current_month_discount
        private string _current_month_discount = string.Empty;
        public string current_month_discount
        {
            get { return _current_month_discount; }
            set { _current_month_discount = value; }
        }

        // current_month_fine
        private string _current_month_fine = string.Empty;
        public string current_month_fine
        {
            get { return _current_month_fine; }
            set { _current_month_fine = value; }
        }
        // total_credit_sales_amount
        private string _total_credit_sales_amount = string.Empty;
        public string total_credit_sales_amount
        {
            get { return _total_credit_sales_amount; }
            set { _total_credit_sales_amount = value; }
        }
        // total_advance_amount
        private string _total_advance_amount = string.Empty;
        public string total_advance_amount
        {
            get { return _total_advance_amount; }
            set { _total_advance_amount = value; }
        }
        // previous_dues
        private string _previous_dues = string.Empty;
        public string previous_dues
        {
            get { return _previous_dues; }
            set { _previous_dues = value; }
        }

        // total_dues
        private string _total_dues = string.Empty;
        public string total_dues
        {
            get { return _total_dues; }
            set { _total_dues = value; }
        }
        // minimum_payable_amount
        private string _minimum_payable_amount = string.Empty;
        public string minimum_payable_amount
        {
            get { return _minimum_payable_amount; }
            set { _minimum_payable_amount = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // details
        private string _details =string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }
        // Message
        private string _Message =string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
       
    }

}