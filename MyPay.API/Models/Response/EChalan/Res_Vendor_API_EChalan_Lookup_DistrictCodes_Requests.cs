using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.EChalan
{
    public class Res_Vendor_API_EChalan_Lookup_DistrictCodes_Requests : CommonResponse
    {
        // locations
        private List<ChallanLocation> _locations = new List<ChallanLocation>();
        public List<ChallanLocation> locations
        {
            get { return _locations; }
            set { _locations = value; }
        }


    } 
}