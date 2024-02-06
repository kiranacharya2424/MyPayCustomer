using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_FlightLookup: WebCommonResponse
    {
        // FilePath
        private string _FilePath = string.Empty;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        // Inbound
        private List<Inbound> _inbound = new List<Inbound>();
        public List<Inbound> inbound
        {
            get { return _inbound; }
            set { _inbound = value; }
        }

        // Outbound
        private List<Outbound> _outbound = new List<Outbound>();
        public List<Outbound> outbound
        {
            get { return _outbound; }
            set { _outbound = value; }
        }
        // BookingId
        private string _booking_id = string.Empty;
        public string booking_id
        {
            get { return _booking_id; }
            set { _booking_id = value; }
        }
    }
}