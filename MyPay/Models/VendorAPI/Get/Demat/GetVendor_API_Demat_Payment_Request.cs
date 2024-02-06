using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Demat_Payment_Request : CommonGet
    {
        // message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }


        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }


        // detailsstring
        private string _detail;
        public string detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        // WalletBalance
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // UniqueTransactionId
        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        //Extradata
        private List<DematExtra_Data> _Extrdata = new List<DematExtra_Data>();
        public List<DematExtra_Data> extra_Data
        {

            get { return _Extrdata; }

            set { _Extrdata = value; }
        }

    }
    public class DematExtra_Data
    {
        
      
    }

}