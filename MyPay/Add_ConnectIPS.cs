using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class Add_ConnectIPS
    {
        //MERCHANTID 
        private string _MERCHANTID = string.Empty;
        public string MERCHANTID
        {
            get { return _MERCHANTID; }
            set { _MERCHANTID = value; }
        }

        //APPID 
        private string _APPID = string.Empty;
        public string APPID
        {
            get { return _APPID; }
            set { _APPID = value; }
        }

        //APPNAME 
        private string _APPNAME = string.Empty;
        public string APPNAME
        {
            get { return _APPNAME; }
            set { _APPNAME = value; }
        }

        //TXNID 
        private string _TXNID = string.Empty;
        public string TXNID
        {
            get { return _TXNID; }
            set { _TXNID = value; }
        }

        //TXNDATE
        private string _TXNDATE = string.Empty;
        public string TXNDATE
        {
            get { return _TXNDATE; }
            set { _TXNDATE = value; }
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

        //TOKEN
        private string _TOKEN = string.Empty;
        public string TOKEN
        {
            get { return _TOKEN; }
            set { _TOKEN = value; }
        }
    }
}