using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddBankingNPSStatus
    {
        //MerchantId 
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //MerchantName 
        private string _MerchantName = string.Empty;
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }

        //MerchantTxnId  
        private string _MerchantTxnId = string.Empty;
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
        }

        //Signature  
        private string _Signature = string.Empty;
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }
    }
}