using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_Sales_Report_Requests : CommonProp
    {
        private string _UserID = string.Empty;
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private string _AgencyId = string.Empty;
        public string AgencyId
        {
            get { return _AgencyId; }
            set { _AgencyId = value; }
        }
        private string _FromDate = string.Empty;
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private string _ToDate = string.Empty;
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
    }
}
