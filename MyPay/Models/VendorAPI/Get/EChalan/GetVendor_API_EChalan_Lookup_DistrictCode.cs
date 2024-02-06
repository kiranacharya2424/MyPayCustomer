using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_EChalan_Lookup_DistrictCode : CommonGet
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
        // locations
        private List<ChallanLocation> _locations = new List<ChallanLocation>();
        public List<ChallanLocation> locations
        {
            get { return _locations; }
            set { _locations = value; }
        } 

    }
    public class ChallanDistrict
    {
        public string district_code { get; set; }
        public string district_name { get; set; }
        public string district_name_np { get; set; }
    }

    public class ChallanLocation
    {
        public string province_code { get; set; }
        public string province_name { get; set; }
        public string province_name_np { get; set; }
        public List<ChallanDistrict> district { get; set; }
    }

}