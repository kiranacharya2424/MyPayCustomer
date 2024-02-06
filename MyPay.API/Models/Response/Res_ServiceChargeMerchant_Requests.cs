using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_ServiceChargeMerchant_Requests : CommonResponse
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //NetAmount
        private string _NetAmount = "0.00";
        public string NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        //Amount
        private string _Amount = "0.00";
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //PercentageServiceCharge
        private string _PercentageServiceCharge = "0.00";
        public string PercentageServiceCharge
        {
            get { return _PercentageServiceCharge; }
            set { _PercentageServiceCharge = value; }
        }
        //ServiceChargeAmount
        private string _ServiceChargeAmount = "0.00";
        public string ServiceChargeAmount
        {
            get { return _ServiceChargeAmount; }
            set { _ServiceChargeAmount = value; }
        }
        //CashbackAmount
        private string _CashbackAmount = "0.00";
        public string CashbackAmount
        {
            get { return _CashbackAmount; }
            set { _CashbackAmount = value; }
        }
        //MerchantCommissionTotal
        private string _MerchantCommissionTotal = "0.00";
        public string MerchantCommissionTotal
        {
            get { return _MerchantCommissionTotal; }
            set { _MerchantCommissionTotal = value; }
        }
        //RewardPoints
        private string _RewardPoints = "0.00";
        public string RewardPoints
        {
            get { return _RewardPoints; }
            set { _RewardPoints = value; }
        }
        //MPCoinsDebit
        private string _MPCoinsDebit = "0.00";
        public string MPCoinsDebit
        {
            get { return _MPCoinsDebit; }
            set { _MPCoinsDebit = value; }
        }
        //DiscountAmount
        private string _DiscountAmount = "0.00";
        public string DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        //DiscountPercentage
        private string _DiscountPercentage = "0.00";
        public string DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        //WalletAmountDeduct
        private string _WalletAmountDeduct = "0.00";
        public string WalletAmountDeduct
        {
            get { return _WalletAmountDeduct; }
            set { _WalletAmountDeduct = value; }
        }
    }
}