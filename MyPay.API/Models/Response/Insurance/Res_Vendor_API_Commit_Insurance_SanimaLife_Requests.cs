using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_Commit_Insurance_SanimaLife_Requests:CommonResponse
    {
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        private string _State = string.Empty;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Detail = string.Empty;
        public string Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }

        public SanimaLifeExtraData _ExtraData = new SanimaLifeExtraData();

    }

    public class SanimaLifeExtraData
    {
        public string TransactionId { get; set; }

    }
}