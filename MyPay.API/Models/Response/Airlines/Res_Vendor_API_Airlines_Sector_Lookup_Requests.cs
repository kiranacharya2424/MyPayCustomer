using System;
using System.Collections.Generic;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Sector_Lookup_Requests : CommonResponse
    {
        // Inbound
        private List<FligthSectors> _FligthSectors = new List<FligthSectors>();
        public List<FligthSectors> FligthSectors
        {
            get { return _FligthSectors; }
            set { _FligthSectors = value; }
        }

    }
    public class FligthSectors
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsNational { get; set; }
        public bool IsInternational { get; set; }
        public bool IsActive { get; set; }
    }

}