using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Antivirus : WebCommonResponse
    {
        // Kaspersky_data
        private List<Kaspersky_bills> _Kaspersky_bills = new List<Kaspersky_bills>();
        public List<Kaspersky_bills> Kaspersky_Bills
        {

            get { return _Kaspersky_bills; }

            set { _Kaspersky_bills = value; }
        }
        private List<Kaspersky_bills> _Data = new List<Kaspersky_bills>();
        public List<Kaspersky_bills> Data
        {

            get { return _Data; }

            set { _Data = value; }
        }
    }
}