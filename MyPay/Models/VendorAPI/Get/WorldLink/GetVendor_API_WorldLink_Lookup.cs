using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.WorldLink
{
    public class GetVendor_API_WorldLink_Lookup:CommonGet
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
        private bool _first_online_payment;
        public bool first_online_payment
        {
            get { return _first_online_payment; }
            set { _first_online_payment = value; }
        }
        private bool _renew_option;
        public bool renew_option
        {
            get { return _renew_option; }
            set { _renew_option = value; }
        }
        private int _amount;
        public int amount
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
        public int due_amount_till_now
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
        public List<WorldLinkAmountDetail> amount_detail
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
        public string full_name
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
        private string _message;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private string _subscribed_package_name;
        public string subscribed_package_name
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

    public class WorldLinkAmountDetail
    {
        private string _particular { get; set; }
        public string particular
        {
            get { return _particular; }
            set { _particular = value; }
        }
        private string _amount { get; set; }
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }

    public class WorldLinkPackageOption
    {
        private string _packageId { get; set; }
        public string packageId
        {
            get { return _packageId; }
            set { _packageId = value; }
        }
        private string _packageName { get; set; }
        public string packageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }
        private string _packageRate { get; set; }
        public string packageRate
        {
            get { return _packageRate; }
            set { _packageRate = value; }
        }
        private string _packageLabel { get; set; }
        public string packageLabel
        {
            get { return _packageLabel; }
            set { _packageLabel = value; }
        }
    }


    public class AvailableRenewOption
    {
        private string _packageId { get; set; }
        public string packageId
        {
            get { return _packageId; }
            set { _packageId = value; }
        }
        private string _packageName { get; set; }
        public string packageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }
        private string _packageRate { get; set; }
        public string packageRate
        {
            get { return _packageRate; }
            set { _packageRate = value; }
        }
        private string _packageLabel { get; set; }
        public string packageLabel
        {
            get { return _packageLabel; }
            set { _packageLabel = value; }
        }
    }
}