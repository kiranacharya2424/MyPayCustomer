using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.VendorAPI.Get.Internet.SUBISU;

namespace MyPay.API.Models.Response.Internet
{
    public class Res_Vendor_API_Subisu_Lookup_Requests : CommonResponse
    {
        
        // plan_type
        private string _plan_type = string.Empty;
        public string plan_type
        {
            get { return _plan_type; }
            set { _plan_type = value; }
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

        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }

        

        public List<InternetPlanDetail> internet_plan_details { get; set; }

        public List<TvDetail> tv_details { get; set; }

        public List<TvPlanDetail> tv_plan_details { get; set; }

        public List<PlanDetailListOffer> plan_detail_list_offer { get; set; }

        //// internet plan details
        //private List<tv_plan_details> _tv_plan_details = new List<tv_plan_details>();
        //public List<tv_plan_details> tv_plan_details
        //{

        //    get { return _tv_plan_details; }

        //    set { _tv_plan_details = value; }
        //}

        //// subisu_data
        //private List<subisu_plan_details> _subisu_plan_details = new List<subisu_plan_details>();
        //public List<subisu_plan_details> subisu_plan_details
        //{

        //    get { return _subisu_plan_details; }

        //    set { _subisu_plan_details = value; }
        //}

        //// tv plan details
        //private List<subisu_internet_plan_details> _subisu_internet_plan_details = new List<subisu_internet_plan_details>();
        //public List<subisu_internet_plan_details> subisu_internet_plan_details
        //{

        //    get { return _subisu_internet_plan_details; }

        //    set { _subisu_internet_plan_details = value; }
        //}



    }

    //public class InternetPlanDetail
    //{
    //    public string amount { get; set; }
    //    public string plan_name { get; set; }
    //    public string description { get; set; }
    //    public string primary_speed { get; set; }
    //    public string volume_quota { get; set; }
    //    public string validity { get; set; }
    //}

    //public class TvPlanDetail
    //{
    //    public string current_video_plan_name { get; set; }
    //    public string stb { get; set; }
    //    public string expiry_date { get; set; }
    //    public string amount { get; set; }
    //    public string plan_name { get; set; }
    //    public string validity { get; set; }
    //}

    //public class subisu_internet_plan_details
    //{
    //    private string _amount = string.Empty;
    //    public string amount
    //    {
    //        get { return _amount; }
    //        set { _amount = value; }
    //    }

    //    private string _plan_name = string.Empty;
    //    public string plan_name
    //    {
    //        get { return _plan_name; }
    //        set { _plan_name = value; }
    //    }

    //    private string _description = string.Empty;
    //    public string description
    //    {
    //        get { return _description; }
    //        set { _description = value; }
    //    }

    //    private string _primary_speed = string.Empty;
    //    public string primary_speed
    //    {
    //        get { return _primary_speed; }
    //        set { _primary_speed = value; }
    //    }

    //    private string _volume_quota = string.Empty;
    //    public string volume_quota
    //    {
    //        get { return _volume_quota; }
    //        set { _volume_quota = value; }
    //    }

    //    private string _validity = string.Empty;
    //    public string validity
    //    {
    //        get { return _validity; }
    //        set { _validity = value; }
    //    }
    //}

    //public class tv_plan_details
    //{
    //    // name
    //    private string _plan_name = string.Empty;
    //    public string plan_name
    //    {
    //        get { return _plan_name; }
    //        set { _plan_name = value; }
    //    }
    //    private string _validity = string.Empty;
    //    public string validity
    //    {
    //        get { return _validity; }
    //        set { _validity = value; }
    //    }
    //    private string _amount = string.Empty;
    //    public string amount
    //    {
    //        get { return _amount; }
    //        set { _amount = value; }
    //    }
    //}

    //public class subisu_plan_details
    //{
    //    // _current_video_plan_name
    //    private string _current_video_plan_name = string.Empty;
    //    public string current_video_plan_name
    //    {
    //        get { return _current_video_plan_name; }
    //        set { _current_video_plan_name = value; }
    //    }
    //    private string _stb = string.Empty;
    //    public string stb
    //    {
    //        get { return _stb; }
    //        set { _stb = value; }
    //    }
    //    private string _expiry_date = string.Empty;
    //    public string expiry_date
    //    {
    //        get { return _expiry_date; }
    //        set { _expiry_date = value; }
    //    }
    //}
}

