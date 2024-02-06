using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_Check_Balance_Requests : CommonProp
    {
       
        private string _UserID = string.Empty;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _AirlineID = string.Empty;
        public string AirlineID
        {
            get { return _AirlineID; }
            set { _AirlineID = value; }
        }
    }
}