using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddPaymentInstrument
    {
        //MerchantId
        private string _MerchantId = "";
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //MerchantName
        private string _MerchantName = "";
        public string MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }

        //Signature
        private string _Signature = "";
        public string Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }

    }
}