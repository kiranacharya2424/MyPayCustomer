using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetOccupation:CommonGet
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