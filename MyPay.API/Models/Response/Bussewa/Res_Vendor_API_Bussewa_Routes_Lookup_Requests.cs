using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Antivirus.Bussewa
{
    public class Res_Vendor_API_Bussewa_Routes_Lookup_Requests : CommonResponse
    {
        public List<BusRoute> routes { get; set; }
    } 

}