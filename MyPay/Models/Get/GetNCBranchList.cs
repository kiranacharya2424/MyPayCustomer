using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetNCBranchList
    {
        #region "Properties"
        //branchId
        private string _branchId = "";
        public string branchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        //bankId
        private string _bankId = "";
        public string bankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        //branchName
        private string _branchName = "";
        public string branchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }
        #endregion
    }
}