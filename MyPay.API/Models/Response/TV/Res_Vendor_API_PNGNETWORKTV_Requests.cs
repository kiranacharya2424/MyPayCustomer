using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.SIMTV
{
    public class Res_Vendor_API_PNGNETWORKTV_Requests : CommonResponse
    {

        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
        private string _Detail = string.Empty;
        public string Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
    }
}