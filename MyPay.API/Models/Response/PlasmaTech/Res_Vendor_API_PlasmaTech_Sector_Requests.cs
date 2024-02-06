using MyPay.API.Models.Airlines;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.PlasmaTech 
{
    public class Res_Vendor_API_PlasmaTech_Sector_Requests : CommonResponse
    {
        private List<FligthSectors> _sectors = new List<FligthSectors>();
        public List<FligthSectors> FligthSectors
        {
            get { return _sectors; }
            set { _sectors = value; }
        }


    }
    //public class FligthSectors
    //{
    //    public string Name { get; set; }
    //    public string Code { get; set; }
    //}
 
 
    
}