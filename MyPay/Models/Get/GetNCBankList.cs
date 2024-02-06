using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetNCBankList
    {
        #region "Properties"    

        //bankId
        private string _bankId = "";
        public string bankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        //branchName
        private string _bankName = "";
        public string bankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }
        #endregion
    }
}