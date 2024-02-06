using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddBankingNPS
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

        //Amount 
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        //MerchantTxnId  
        private string _MerchantTxnId = "";
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
        }

        //TransactionRemarks  
        private string _TransactionRemarks = "";
        public string TransactionRemarks
        {
            get { return _TransactionRemarks; }
            set { _TransactionRemarks = value; }
        }

        //InstrumentCode  
        private string _InstrumentCode = "";
        public string InstrumentCode
        {
            get { return _InstrumentCode; }
            set { _InstrumentCode = value; }
        }

        //ProcessId   
        private string _ProcessId = "";
        public string ProcessId
        {
            get { return _ProcessId; }
            set { _ProcessId = value; }
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