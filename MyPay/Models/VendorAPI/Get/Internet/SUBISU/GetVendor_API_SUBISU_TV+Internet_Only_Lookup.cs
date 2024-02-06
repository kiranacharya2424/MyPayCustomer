using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Internet.SUBISU
{
    public class GetVendor_API_SUBISU_TV_Internet_Only_Lookup
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        
        public string customer_name { get; set; }
        public string address { get; set; }
        public string current_plan_name { get; set; }
        public string user_id { get; set; }
        public string outstanding_amount { get; set; }
        public string expiry_date { get; set; }
        public string mobile_no { get; set; }
        public TvPlanList tv_plan_list { get; set; }
        public string onu_id { get; set; }
        public string partner_name { get; set; }
        public PlanDetailList plan_detail_list { get; set; }
        public string session_id { get; set; }
        public string status { get; set; }

        //public class InternetDetails
        //{
        //    public List<InternetPlanDetail> internet_plan_details { get; set; }
        //}

        //public class InternetPlanDetail
        //{
        //    public string amount { get; set; }
        //    public string plan_name { get; set; }
        //    public string description { get; set; }
        //    public string primary_speed { get; set; }
        //    public string volume_quota { get; set; }
        //    public string validity { get; set; }
        //}

        //public class PlanDetailList
        //{
        //    public PlanDetailList plan_detail_list { get; set; }
        //    public string plan_type { get; set; }
        //    public List<InternetPlanDetail> internet_plan_details { get; set; }
        //    public InternetDetails internet_details { get; set; }
        //    public List<TvDetail> tv_details { get; set; }
        //}

        

        //public class TvDetail
        //{
        //    public string stb { get; set; }
        //    public List<TvPlanDetail> tv_plan_details { get; set; }
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

        //public class TvPlanList
        //{
        //    public List<TvPlanDetail> tv_plan_details { get; set; }
        //}


    }
}
