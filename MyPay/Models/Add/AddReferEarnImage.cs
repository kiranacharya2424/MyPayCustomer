using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddReferEarnImage : CommonAdd
    {
        #region "Properties"
        //Row
        private int _Row = 0;
        public int Row
        {
            get { return _Row; }
            set { _Row = value; }
        }
        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        //ContentText
        private string _ContentText = string.Empty;
        public string ContentText
        {
            get { return _ContentText; }
            set { _ContentText = value; }
        }

        //IpAddress
        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        //DisplayCashbackAmount
        private decimal _DisplayCashbackAmount = 0;
        public decimal DisplayCashbackAmount
        {
            get { return _DisplayCashbackAmount; }
            set { _DisplayCashbackAmount = value; }
        }
        //CashbackAmount
        private decimal _CashbackAmount = 0;
        public decimal CashbackAmount
        {
            get { return _CashbackAmount; }
            set { _CashbackAmount = value; }
        }
        #endregion
    }
}