using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddExportData : CommonAdd
    {
        #region "Enums"
        public enum ExportType
        {
            User = 1,
            Transaction = 2,
            Merchant = 3,
            MerchantOrders = 4,
            MerchantLogin = 5,
            Voting = 6
        }
        #endregion

        #region "Properties"

        //FilePath
        private string _FilePath = string.Empty;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        //Type 
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        #endregion
    }
}