using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using MyPay.Models.VendorAPI.Get.Internet.SUBISU;
using MyPay.Models.VendorAPI.Get.Ride;
using MyPay.Models.VendorAPI.Get.WorldLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Internet:WebCommonResponse
    {
        // session_id
        private string _SessionID = string.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        // customer_name
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        // CustomerID
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        // ViaNet_data
        private List<vianet_bills> _bills = new List<vianet_bills>();
        public List<vianet_bills> bills
        {

            get { return _bills; }

            set { _bills = value; }
        }

        // classitech_data
        private List<classitech_bills> _available_plans = new List<classitech_bills>();
        public List<classitech_bills> available_plans
        {

            get { return _available_plans; }

            set { _available_plans = value; }
        }

        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        // arrownet_data
        private List<arrownet_bills> _plan_details = new List<arrownet_bills>();
        public List<arrownet_bills> plan_details
        {

            get { return _plan_details; }

            set { _plan_details = value; }
        }

        // full_name
        private string _full_name = string.Empty;
        public string full_name
        {
            get { return _full_name; }
            set { _full_name = value; }
        }
        // days_remaining
        private string _days_remaining = string.Empty;
        public string days_remaining
        {
            get { return _days_remaining; }
            set { _days_remaining = value; }
        }
        // current_plan
        private string _current_plan = string.Empty;
        public string current_plan
        {
            get { return _current_plan; }
            set { _current_plan = value; }
        }
        // has_due
        private string _has_due = string.Empty;
        public string has_due
        {
            get { return _has_due; }
            set { _has_due = value; }
        }

        // Websurfer_UserListdata
        public Websurfer_Customer customer { get; set; }
        public List<Websurfer_Connection> connection { get; set; }

        // Websurfer_data
        private List<Websurfer_packages> _packages = new List<Websurfer_packages>();
        public List<Websurfer_packages> packages
        {

            get { return _packages; }

            set { _packages = value; }
        }

        // techminds_data
        private techminds_bills _techmindsdata = new techminds_bills();
        public techminds_bills techmindsdata
        {

            get { return _techmindsdata; }

            set { _techmindsdata = value; }
        }

        // data
        private techminds_data _data = new techminds_data();
        public techminds_data data
        {
            get { return _data; }
            set { _data = value; }
        }

        // PNGNETWORKTV_Lookup_Data
        private PNG_Network_TV_LookupDetail _detail = new PNG_Network_TV_LookupDetail();
        public PNG_Network_TV_LookupDetail detail
        {

            get { return _detail; }

            set { _detail = value; }
        }

        // JAGRITITV_Lookup_Data
        private JAGRITI_TV_LookupDetail _Jagritidetail = new JAGRITI_TV_LookupDetail();
        public JAGRITI_TV_LookupDetail Jagritidetail
        {

            get { return _Jagritidetail; }

            set { _Jagritidetail = value; }
        }
        //Worldlink
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
        private int _due_amount_till_now;
        public int due_amount_till_now
        {
            get { return _due_amount_till_now; }
            set { _due_amount_till_now = value; }
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

        //subisu
        private string _address = string.Empty;
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        // user_id
        private string _user_id = string.Empty;
        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        // outstanding_amount
        private string _outstanding_amount = string.Empty;
        public string outstanding_amount
        {
            get { return _outstanding_amount; }
            set { _outstanding_amount = value; }
        }

        // expiry_date
        private string _expiry_date = string.Empty;
        public string expiry_date
        {
            get { return _expiry_date; }
            set { _expiry_date = value; }
        }

        // mobile_no
        private string _mobile_no = string.Empty;
        public string mobile_no
        {
            get { return _mobile_no; }
            set { _mobile_no = value; }
        }
        // onu_id
        private string _onu_id = string.Empty;
        public string onu_id
        {
            get { return _onu_id; }
            set { _onu_id = value; }
        }
        private string _plan_type = string.Empty;
        public string plan_type
        {
            get { return _plan_type; }
            set { _plan_type = value; }
        }

        private string _current_plan_name = string.Empty;
        public string current_plan_name
        {
            get { return _current_plan_name; }
            set { _current_plan_name = value; }
        }

        // partner_name
        private string _partner_name = string.Empty;
        public string partner_name
        {
            get { return _partner_name; }
            set { _partner_name = value; }
        }
        public TvPlanList tv_plan_list { get; set; }
        public PlanDetailList plan_detail_list { get; set; }
        public PlanDetailListOffer plan_detail_list_offer { get; set; }

        private string _branch;
        public string branch
        {
            get { return _branch; }
            set { _branch = value; }
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
        // Techminds_Plans
        private dynamic _Available_Plans = new Techminds_Plans();
        public dynamic Available_Plans
        {

            get { return _Available_Plans; }

            set { _Available_Plans = value; }
        }
    }
    public class Techminds_Plans
    {
        private string _Plan_12Month = string.Empty;
        public string Plan_12Month
        {
            get { return _Plan_12Month; }
            set { _Plan_12Month = value; }
        }
        private string _Plan_6Month = string.Empty;
        public string Plan_6Month
        {
            get { return _Plan_6Month; }
            set { _Plan_6Month = value; }
        }
        private string _Plan_3Month = string.Empty;
        public string Plan_3Month
        {
            get { return _Plan_3Month; }
            set { _Plan_3Month = value; }
        }
        private string _Plan_1Month = string.Empty;
        public string Plan_1Month
        {
            get { return _Plan_1Month; }
            set { _Plan_1Month = value; }
        }

        private string _Plan_15Days = string.Empty;
        public string Plan_15Days
        {
            get { return _Plan_15Days; }
            set { _Plan_15Days = value; }
        }

        private string _Plan_180Days = string.Empty;
        public string Plan_180Days
        {
            get { return _Plan_180Days; }
            set { _Plan_180Days = value; }
        }

        private string _Plan_30Days = string.Empty;
        public string Plan_30Days
        {
            get { return _Plan_30Days; }
            set { _Plan_30Days = value; }
        }

        private string _Plan_60Days = string.Empty;
        public string Plan_60Days
        {
            get { return _Plan_60Days; }
            set { _Plan_60Days = value; }
        }

        private string _Plan_90Days = string.Empty;
        public string Plan_90Days
        {
            get { return _Plan_90Days; }
            set { _Plan_90Days = value; }
        }
    }

}