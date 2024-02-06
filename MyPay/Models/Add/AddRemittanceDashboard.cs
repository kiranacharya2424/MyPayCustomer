using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddRemittanceDashboard: CommonAdd
    {
        #region "Properties"


        private string _FeeAccountBalance = string.Empty;
        public string FeeAccountBalance
        {
            get { return _FeeAccountBalance; }
            set { _FeeAccountBalance = value; }
        }

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

        //AllAPILogs
        private int _AllAPILogs = 0;
        public int AllAPILogs
        {
            get { return _AllAPILogs; }
            set { _AllAPILogs = value; }
        }

        //TodayAPILogs
        private int _TodayAPILogs = 0;
        public int TodayAPILogs
        {
            get { return _TodayAPILogs; }
            set { _TodayAPILogs = value; }
        }

        //ThisWeekAPILogs
        private int _ThisWeekAPILogs = 0;
        public int ThisWeekAPILogs
        {
            get { return _ThisWeekAPILogs; }
            set { _ThisWeekAPILogs = value; }
        }

        //ThisMonthAPILogs
        private int _ThisMonthAPILogs = 0;
        public int ThisMonthAPILogs
        {
            get { return _ThisMonthAPILogs; }
            set { _ThisMonthAPILogs = value; }
        }

        private string _apipassword = string.Empty;
        public string apipassword
        {
            get { return _apipassword; }
            set { _apipassword = value; }
        }

        //secretkey
        private string _secretkey = string.Empty;
        public string secretkey
        {
            get { return _secretkey; }
            set { _secretkey = value; }
        }
        //ODL
        private string _ODL = string.Empty;
        public string ODL
        {
            get { return _ODL; }
            set { _ODL = value; }
        }
        //Prefund
        private string _Prefund = string.Empty;
        public string Prefund
        {
            get { return _Prefund; }
            set { _Prefund = value; }
        }
        #endregion
    }
}