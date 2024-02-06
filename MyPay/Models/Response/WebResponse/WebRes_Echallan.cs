using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Echallan : WebCommonResponse
    {
        private string _CreditorName =  string.Empty;
        public string CreditorName
        {
            get { return _CreditorName; }
            set { _CreditorName = value; }
        }
        private string _ChitNumber = string.Empty;
        public string ChitNumber
        {
            get { return _ChitNumber; }
            set { _ChitNumber = value; }
        }
        private string _FullName = string.Empty;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _EbpNumber = string.Empty;
        public string EbpNumber
        {
            get { return _EbpNumber; }
            set { _EbpNumber = value; }
        }
        private string _Amout = string.Empty;
        public string Amout
        {
            get { return _Amout; }
            set { _Amout = value; }
        }
        private string _Session_Id = string.Empty;
        public string Session_Id
        {
            get { return _Session_Id; }
            set { _Session_Id = value; }
        } private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}