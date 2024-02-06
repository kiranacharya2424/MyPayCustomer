using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_DataPack: WebCommonResponse
    {
        private datapack_details _detail = new datapack_details();
        public datapack_details detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        // DataPack_data
        private List<datapack_packages> _packages = new List<datapack_packages>();
        public List<datapack_packages> packages
        {

            get { return _packages; }

            set { _packages = value; }
        }
    }
}