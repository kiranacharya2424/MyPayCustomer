using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Merchant
{
    public class Req_FeaturedPartners : CommonProp
    {
      
        public string OrganizationName { get; set; }
        public string Image { get; set; }
        public string WebsiteUrl { get; set; }
        public int SortOrder { get; set; }
        public bool IsFeaturedPartner { get; set; }

    }
}