using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_FlightDetail_Requests : CommonProp
    {
        private string _UserID = string.Empty;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _FlightId = string.Empty;
        public string FlightId
        {
            get { return _FlightId; }
            set { _FlightId = value; }
        }
    }
}
