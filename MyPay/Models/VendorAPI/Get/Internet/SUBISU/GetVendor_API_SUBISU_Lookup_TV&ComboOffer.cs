using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.Common;

namespace MyPay.Models.VendorAPI.Get.Internet.SUBISU
{
    public class GetVendor_API_SUBISU_Lookup_TV_ComboOffer : CommonGet
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

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
        public PlanDetailListOffer plan_detail_list { get; set; }
        public string session_id { get; set; }
        
        
    }

    public class PlanDetailListOffer
    {
        public List<PlanDetailListOffer> plan_detail_list { get; set; }
        public string plan_type { get; set; }

        public string detail { get; set; }
        public bool status { get; set; }  = false;
        public string validity { get; set; }
        public string offer_name { get; set; }
        public string offer_id { get; set; }
        public string amount { get; set; }
        public string stb { get; set; }
        public List<ComboPlanDetail> combo_plan_details { get; set; }
        public List<TvPlanDetail> tv_plan_details { get; set; }
    }
    public class ComboPlanDetail
    {
        public string plan_type { get; set; }
        public string service_media { get; set; }
        public string activation_date { get; set; }
        public string expiry_date { get; set; }
        public string allow_to_renew { get; set; }
        public string service_media_id { get; set; }
        public string stb_id { get; set; }
        public string is_primary { get; set; }
        public string expiry_date_string { get; set; }
        public string activation_date_string { get; set; }
    }
}
