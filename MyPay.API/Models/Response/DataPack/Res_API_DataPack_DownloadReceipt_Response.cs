using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.DataPack
{
    public class Res_API_DataPack_DownloadReceipt_Response: CommonResponse
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