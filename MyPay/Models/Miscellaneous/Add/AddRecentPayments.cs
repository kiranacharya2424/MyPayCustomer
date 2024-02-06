using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddRecentPayments : CommonAdd
    {
        #region "Properties"
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private string _TransactionAmount = string.Empty;
        public string TransactionAmount
        {
            get { return _TransactionAmount; }
            set { _TransactionAmount = value; }
        }
        private bool _IsFavourite = false;
        public bool IsFavourite
        {
            get { return _IsFavourite; }
            set { _IsFavourite = value; }
        }
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        //CustomerId
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        //Type
        private Int32 _Type = 0;
        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        #endregion
    }
}