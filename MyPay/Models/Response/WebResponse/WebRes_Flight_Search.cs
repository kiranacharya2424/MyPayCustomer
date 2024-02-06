using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Flight_Search : WebCommonResponse
    {
        private List<FlightOutbound> _FlightOutbound = new List<FlightOutbound>();
        public List<FlightOutbound> FlightOutbound
        {
            get { return _FlightOutbound; }
            set { _FlightOutbound = value; }
        }
        // Inbound
        private List<FlightInbound> _FlightInbound = new List<FlightInbound>();
        public List<FlightInbound> FlightInbound
        {
            get { return _FlightInbound; }
            set { _FlightInbound = value; }
        }
    }
}
