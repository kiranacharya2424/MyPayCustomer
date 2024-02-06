using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetPurpose:CommonGet
    {
        #region "Properties"

        //CategoryName
        private string _CategoryName = string.Empty;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        //Position
        private int _Position = 0;
        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        #endregion
    }
}