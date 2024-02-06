using System;
using System.Collections.Generic;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Download_Ticket_Requests : CommonResponse
    {

        // FilePath
        private string _FilePath = string.Empty;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

    }
     
}