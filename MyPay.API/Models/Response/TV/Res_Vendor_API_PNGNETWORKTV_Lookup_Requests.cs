using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.PNGNETWORKTV
{
    public class Res_Vendor_API_PNGNETWORKTV_Lookup_Requests : CommonResponse
    {

        // PNGNETWORKTV_data
        private PNGNETWORKTV_Data _png_network_tv_details = new PNGNETWORKTV_Data();
        public PNGNETWORKTV_Data  png_network_tv_details
        {

            get { return _png_network_tv_details; }

            set { _png_network_tv_details = value; }
        }
    }
    public class PNGNETWORKTV_Data
    {
        public List<PNG_Network_TV_Package> packages { get; set; }

    }
}