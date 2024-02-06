using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_TransferByPhone:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //RecipientPhone
        private string _RecipientPhone = string.Empty;
        public string RecipientPhone
        {
            get { return _RecipientPhone; }
            set { _RecipientPhone = value; }
        }
         
        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Pin
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        //Referenceno
        private string _Referenceno = string.Empty;
        public string Referenceno
        {
            get { return _Referenceno; }
            set { _Referenceno = value; }
        }
    }
}