using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{

    public class GetVendor_API_MEROTV_Packages_Lookup : CommonGet
    {
            public List<MeroTVPackage> packages { get; set; }
            public int session_id { get; set; }
            public bool status { get; set; }

        public string Message;
    }

    public class MeroTVPackage
    {
        public string package_id { get; set; }
        public string package_name { get; set; }
        public int amount { get; set; }
    }

}