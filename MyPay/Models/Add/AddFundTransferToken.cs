using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddFundTransferToken
    {
        //MerchantId
        private string _MerchantId = "";
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //ApiUserName
        private string _ApiUserName = "";
        public string ApiUserName
        {
            get { return _ApiUserName; }
            set { _ApiUserName = value; }
        }

        //Signature
        private string _Signature = "";
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }

        //TimeStamp 
        private string _TimeStamp = Common.Common.GetDateByArea(DateTime.UtcNow, "Nepal Standard Time").ToString("yyyy-MM-ddTHH:mm:ss.fff");
        public string TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }

    }
}