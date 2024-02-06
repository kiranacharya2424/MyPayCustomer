using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_GetUserWalletWithKYC : WebCommonResponse
    {
        private int _IsKycVerified = 0;
        public int IsKycVerified
        {
            get { return _IsKycVerified; }
            set { _IsKycVerified = value; }
        }
        //RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }



        //IsBankAdded
        private bool _IsBankAdded = false;
        public bool IsBankAdded
        {
            get { return _IsBankAdded; }
            set { _IsBankAdded = value; }
        }
        //TotalAmount
        private string _TotalAmount = string.Empty;
        public string TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        //Pin
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        //TotalRewardPoints
        private string _TotalRewardPoints = string.Empty;
        public string TotalRewardPoints
        {
            get { return _TotalRewardPoints; }
            set { _TotalRewardPoints = value; }
        }
        //TotalCashback
        private string _TotalCashback = string.Empty;
        public string TotalCashback
        {
            get { return _TotalCashback; }
            set { _TotalCashback = value; }
        }

        //TotalTransactions
        private string _TotalTransactions = string.Empty;
        public string TotalTransactions
        {
            get { return _TotalTransactions; }
            set { _TotalTransactions = value; }
        }
        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        //MPCoinsFlushDateDt
        private string _MPCoinsFlushDateDt = string.Empty;
        public string MPCoinsFlushDateDt
        {
            get { return _MPCoinsFlushDateDt; }
            set { _MPCoinsFlushDateDt = value; }
        }
        //IsActive
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        //IsResetPasswordFromAdmin
        private bool _IsResetPasswordFromAdmin = false;
        public bool IsResetPasswordFromAdmin
        {
            get { return _IsResetPasswordFromAdmin; }
            set { _IsResetPasswordFromAdmin = value; }
        }
        //IsDeviceActive
        private bool _IsDeviceActive = false;
        public bool IsDeviceActive
        {
            get { return _IsDeviceActive; }
            set { _IsDeviceActive = value; }
        }
        //IsLogout
        private bool _IsLogout = false;
        public bool IsLogout
        {
            get { return _IsLogout; }
            set { _IsLogout = value; }
        }

    }
}