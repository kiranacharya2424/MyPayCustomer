using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetService_Providers
    {

        // ProviderName
        private string _ProviderName = string.Empty;
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }
        // ProviderTypeId
        private string _ProviderTypeId = string.Empty;
        public string ProviderTypeId
        {
            get { return _ProviderTypeId; }
            set { _ProviderTypeId = value; }
        }
        // ValidAmount
        private string _ValidAmount = string.Empty;
        public string ValidAmount
        {
            get { return _ValidAmount; }
            set { _ValidAmount = value; }
        }
        // Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        // LogoURL
        private string _LogoURL = string.Empty;
        public string LogoURL
        {
            get { return _LogoURL; }
            set { _LogoURL = value; }
        }

        // CashbackPercentage
        private string _CashbackPercentage = string.Empty;
        public string CashbackPercentage
        {
            get { return _CashbackPercentage; }
            set { _CashbackPercentage = value; }
        }
        // IsUtility
        private bool _IsUtility = false;
        public bool IsUtility
        {
            get { return _IsUtility; }
            set { _IsUtility = value; }
        }

        // MPCoinsCashback
        private string _MPCoinsCashback = string.Empty;
        public string MPCoinsCashback
        {
            get { return _MPCoinsCashback; }
            set { _MPCoinsCashback = value; }
        }

        private bool _IsServiceDown = false;
        public bool IsServiceDown
        {
            get { return _IsServiceDown; }
            set { _IsServiceDown = value; }
        }

        public int isActive = 0;


        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
        }
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }
        private int _SortOrder = 0;
        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }

        private string _CategoryID = "0";
        public string CategoryID { 
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private int _ParentID = 0;
        public int ParentID {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
    }
}