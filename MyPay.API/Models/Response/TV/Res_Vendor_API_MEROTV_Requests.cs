using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.SIMTV
{
    public class Res_Vendor_API_MEROTV_Requests : CommonResponse
    {
        public bool status { get; set; }
        public string state { get; set; }
        public string message { get; set; }
        // public ExtraData extra_data { get; set; }
        public string detail { get; set; }
        public double credits_consumed { get; set; }
        public double credits_available { get; set; }
        public int id { get; set; }

        public string UniqueTransactionId;

        //private string _UniqueTransactionId = string.Empty;
        //public string UniqueTransactionId
        //{
        //    get { return _UniqueTransactionId; }
        //    set { _UniqueTransactionId = value; }
        //}
        //private string _WalletBalance = string.Empty;
        //public string WalletBalance
        //{
        //    get { return _WalletBalance; }
        //    set { _WalletBalance = value; }
        //}
        //private string _Details = string.Empty;
        //public string Details
        //{
        //    get { return _Details; }
        //    set { _Details = value; }
        //} 


    }
}