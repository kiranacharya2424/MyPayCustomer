using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddFundTxnStatus
    {
        //ApiUsername
        private string _ApiUsername = string.Empty;
        public string ApiUsername
        {
            get { return _ApiUsername; }
            set { _ApiUsername = value; }
        }

        //MerchantId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //Signature
        private string _Signature = string.Empty;
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }

        //MerchantTxnId 
        private string _MerchantTxnId = string.Empty;
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
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