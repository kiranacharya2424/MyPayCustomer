using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS
{
    public class NCHLTokenResp
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public object customerdetails { get; set; }
        public object error { get; set; }
        public object error_description { get; set; }

        public object message { get; set; }
        /*
         {
    "error": "unauthorized",
    "error_description": "Full authentication is required to access this resource"
}
         */
    }

}
