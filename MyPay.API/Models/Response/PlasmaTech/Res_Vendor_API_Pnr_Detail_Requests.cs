using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.PlasmaTech
{
    public class Res_Vendor_API_Pnr_Detail_Requests : CommonResponse
    {
        private string _Data = string.Empty;
        public string Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        public class Body
        {
            public GetPnrDetailResponse GetPnrDetailResponse { get; set; }
        }

        public class Envelope
        {
            public Body Body { get; set; }
        }

        public class GetPnrDetailResponse
        {
            public PnrDetail PnrDetail { get; set; }
        }

        public class PnrDetail
        {
            public string AccessURL { get; set; }
        }

        public class GetPnrDetailRootModel
        {
            public Envelope Envelope { get; set; }
        }
    }
}
