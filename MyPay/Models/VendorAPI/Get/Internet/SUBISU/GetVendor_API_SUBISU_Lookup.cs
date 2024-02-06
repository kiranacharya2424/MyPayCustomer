using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.Common;
using MyPay.Models.Get;

namespace MyPay.Models.VendorAPI.Get.Internet.SUBISU
{
    public class GetVendor_API_SUBISU_Lookup : CommonGet
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

        //current_plan_name
        private string _current_plan_name = string.Empty;
        public string current_plan_name
        {
            get { return _current_plan_name; }
            set { _current_plan_name = value; }
        }

        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
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
        // username
        private string _username = string.Empty;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        // address
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

        // partner_name
        private string _partner_name = string.Empty;
        public string partner_name
        {
            get { return _partner_name; }
            set { _partner_name = value; }
        }


        public TvPlanList tv_plan_list { get; set; }

        public PlanDetailList plan_detail_list { get; set; }


        



    }

    

    public class InternetDetails
    {
        public List<InternetPlanDetail> internet_plan_details { get; set; }
    }

    public class InternetPlanDetail
    {
        public string amount { get; set; }
        public string plan_name { get; set; }
        public string description { get; set; }
        public string primary_speed { get; set; }
        public string volume_quota { get; set; }
        public string validity { get; set; }
    }

    public class PlanDetailList
    {
        public PlanDetailList plan_detail_list { get; set; }
        public string plan_type { get; set; }

        public string detail { get; set; }
        public bool status { get; set; } = false;
        public List<InternetPlanDetail> internet_plan_details { get; set; }
        public InternetDetails internet_details { get; set; }
        public List<TvDetail> tv_details { get; set; }
    }



    public class TvDetail
    {
        public string stb { get; set; }
        public List<TvPlanDetail> tv_plan_details { get; set; }
    }

    public class TvPlanDetail
    {
        public string current_video_plan_name { get; set; }
        public string stb { get; set; }
        public string expiry_date { get; set; }
        public string amount { get; set; }
        public string plan_name { get; set; }
        public string validity { get; set; }
    }

    public class TvPlanList
    {
        public List<TvPlanDetail> tv_plan_details { get; set; }
    }


}
