using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddEstatementPDFToken:CommonAdd
    {
        #region "Properties"

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        //Year
        private string _Year = string.Empty;
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        //Month
        private string _Month = string.Empty;
        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        //FromDate
        private string _FromDate = string.Empty;
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        //ToDate
        private string _ToDate = string.Empty;
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion




    }
}