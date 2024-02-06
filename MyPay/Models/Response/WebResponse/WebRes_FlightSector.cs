using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_FlightSector:WebCommonResponse
    {
        private List<sectors> _sectors = new List<sectors>();
        public List<sectors> sectors
        {
            get { return _sectors; }
            set { _sectors = value; }
        }


        public class FlightSector
        {
            public List<Sector> Sector { get; set; }
        }

        public class SectorCodeRootModel
        {
            public FlightSector FlightSector { get; set; }
        }

        public class Sector
        {
            public string SectorCode { get; set; }
            public string SectorName { get; set; }
        }
    }


}