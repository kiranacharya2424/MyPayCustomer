using MyPay.Models.VendorAPI.Get.Ride;
using MyPay.Models.VendorAPI.Get.WorldLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Ride
{
    public class Res_WorldLink_Lookup : CommonResponse
    {
        private bool _first_online_payment;
        public bool  first_online_payment
        {
            get { return _first_online_payment; }
            set { _first_online_payment = value; }
        }
        private bool _renew_option;
        public bool  renew_option
        {
            get { return _renew_option; }
            set { _renew_option = value; }
        }
        private int _amount;
        public int  amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private int _days_remaining;
        public int days_remaining
        {
            get { return _days_remaining; }
            set { _days_remaining = value; }
        }
        private int _due_amount_till_now;
        public int  due_amount_till_now
        {
            get { return _due_amount_till_now; }
            set { _due_amount_till_now = value; }
        }
        private int _session_id;
        public int session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        private List<WorldLinkAmountDetail> _amount_detail;
        public List<WorldLinkAmountDetail>  amount_detail
        {
            get { return _amount_detail; }
            set { _amount_detail = value; }
        }
        private List<AvailableRenewOption> _available_renew_options;
        public List<AvailableRenewOption> available_renew_options
        {
            get { return _available_renew_options; }
            set { _available_renew_options = value; }
        }
        private List<WorldLinkPackageOption> _package_options;
        public List<WorldLinkPackageOption> package_options
        {
            get { return _package_options; }
            set { _package_options = value; }
        }
        private string _branch; 
        public string branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
        private string _full_name;
        public string  full_name
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        private string _log_idx;
        public string log_idx
        {
            get { return _log_idx; }
            set { _log_idx = value; }
        }
        private string _Apimessage;
        public string  Apimessage
        {
            get { return _Apimessage; }
            set { _Apimessage = value; }
        }
        private string _subscribed_package_name;
        public string  subscribed_package_name
        {
            get { return _subscribed_package_name; }
            set { _subscribed_package_name = value; }
        }
        private string _subscribed_package_type;
        public string subscribed_package_type
        {
            get { return _subscribed_package_type; }
            set { _subscribed_package_type = value; }
        }
        private string _username;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

    }
}