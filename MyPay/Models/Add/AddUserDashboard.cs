using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddUserDashboard : CommonAdd
    {
        #region "Properties"



        //SessionId
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        private string _apikey = string.Empty;
        public string apikey
        {
            get { return _apikey; }
            set { _apikey = value; }
        }


        //AllTransactions
        private int _AllTransactions = 0;
        public int AllTransactions
        {
            get { return _AllTransactions; }
            set { _AllTransactions = value; }
        }

        //TodayTransactions
        private int _TodayTransactions = 0;
        public int TodayTransactions
        {
            get { return _TodayTransactions; }
            set { _TodayTransactions = value; }
        }

        //ThisWeekTransactions
        private int _ThisWeekTransactions = 0;
        public int ThisWeekTransactions
        {
            get { return _ThisWeekTransactions; }
            set { _ThisWeekTransactions = value; }
        }

        //ThisMonthTransactions
        private int _ThisMonthTransactions = 0;
        public int ThisMonthTransactions
        {
            get { return _ThisMonthTransactions; }
            set { _ThisMonthTransactions = value; }
        }



        //AllOrders
        private int _AllOrders = 0;
        public int AllOrders
        {
            get { return _AllOrders; }
            set { _AllOrders = value; }
        }

        //TodayOrders
        private int _TodayOrders = 0;
        public int TodayOrders
        {
            get { return _TodayOrders; }
            set { _TodayOrders = value; }
        }

        //ThisWeekOrders
        private int _ThisWeekOrders = 0;
        public int ThisWeekOrders
        {
            get { return _ThisWeekOrders; }
            set { _ThisWeekOrders = value; }
        }

        //ThisMonthOrders
        private int _ThisMonthOrders = 0;
        public int ThisMonthOrders
        {
            get { return _ThisMonthOrders; }
            set { _ThisMonthOrders = value; }
        }

        //TodayCreditTransactions
        private decimal _TodayCreditTransactions = 0;
        public decimal TodayCreditTransactions
        {
            get { return _TodayCreditTransactions; }
            set { _TodayCreditTransactions = value; }
        }

        //_TodayDebitTransactions
        private decimal _TodayDebitTransactions = 0;
        public decimal TodayDebitTransactions
        {
            get { return _TodayDebitTransactions; }
            set { _TodayDebitTransactions = value; }
        }

        //ThisMonthCreditTransactions
        private decimal _ThisMonthCreditTransactions = 0;
        public decimal ThisMonthCreditTransactions
        {
            get { return _ThisMonthCreditTransactions; }
            set { _ThisMonthCreditTransactions = value; }
        }

        //ThisMonthDebitTransactions
        private decimal _ThisMonthDebitTransactions = 0;
        public decimal ThisMonthDebitTransactions
        {
            get { return _ThisMonthDebitTransactions; }
            set { _ThisMonthDebitTransactions = value; }
        }
        private decimal _WalletBalance = 0;
        public decimal WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
    }
    #endregion
}