using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_RewardPoints:CommonResponse
    {
        // RewardPoints
        private List<AddRewardPointTransactions> _RewardPoints = new List<AddRewardPointTransactions>();
        public List<AddRewardPointTransactions> RewardPoints
        {
            get { return _RewardPoints; }
            set { _RewardPoints = value; }
        }    
        // WalletBalance
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // TotalRewardPoints
        private string _TotalRewardPoints = string.Empty;
        public string TotalRewardPoints
        {
            get { return _TotalRewardPoints; }
            set { _TotalRewardPoints = value; }
        }
    }
}