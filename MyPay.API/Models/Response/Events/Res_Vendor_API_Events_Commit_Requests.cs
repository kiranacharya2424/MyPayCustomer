using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_Events_Commit_Requests : CommonResponse
    {

       
        // status
        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private string _MerchantCode = String.Empty;
        public string MerchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }


        private string _OrderId = String.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        private Commit _data = new Commit();
        public Commit data
        {
            get { return _data; }
            set { _data = value; }
        }



    }

    public class Commit
    {
        private string _MerchantCode = String.Empty;
        public string MerchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }


        private string _OrderId = String.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        private string _TransactionId = String.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        private string _Remarks = String.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
   
}
