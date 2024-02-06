using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_GetPnrDetail_Requests : CommonProp
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
        private string _PnrNo = string.Empty;
        public string PnrNo
        {
            get { return _PnrNo; }
            set { _PnrNo = value; }
        }
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
    }
}
