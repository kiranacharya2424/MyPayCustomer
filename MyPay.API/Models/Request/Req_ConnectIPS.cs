using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_ConnectIPS:CommonProp
    {
        //TXNID 
        private string _TXNID = string.Empty;
        public string TXNID
        {
            get { return _TXNID; }
            set { _TXNID = value; }
        }     

        //TXNCRNCY
        private string _TXNCRNCY = string.Empty;
        public string TXNCRNCY
        {
            get { return _TXNCRNCY; }
            set { _TXNCRNCY = value; }
        }

        //TXNAMT
        private int _TXNAMT = 0;
        public int TXNAMT
        {
            get { return _TXNAMT; }
            set { _TXNAMT = value; }
        }

        //REFERENCEID
        private string _REFERENCEID = string.Empty;
        public string REFERENCEID
        {
            get { return _REFERENCEID; }
            set { _REFERENCEID = value; }
        }

        //REMARKS
        private string _REMARKS = string.Empty;
        public string REMARKS
        {
            get { return _REMARKS; }
            set { _REMARKS = value; }
        }

        //PARTICULARS
        private string _PARTICULARS = string.Empty;
        public string PARTICULARS
        {
            get { return _PARTICULARS; }
            set { _PARTICULARS = value; }
        }
    }
}