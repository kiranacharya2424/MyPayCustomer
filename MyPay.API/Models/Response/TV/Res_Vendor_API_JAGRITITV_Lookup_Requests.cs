using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.JAGRITITV
{
    public class Res_Vendor_API_JAGRITITV_Lookup_Requests : CommonResponse
    {

        // JAGRITITV_data
        private JAGRITITV_Data _jagriti_tv_details = new JAGRITITV_Data();
        public JAGRITITV_Data jagriti_tv_details
        {

            get { return _jagriti_tv_details; }

            set { _jagriti_tv_details = value; }
        }
    }
    public class JAGRITITV_Data
    {
        public List<JAGRITI_TV_Package> packages { get; set; }

    }
}