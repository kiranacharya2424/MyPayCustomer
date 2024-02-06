using MyPay.Models.Common;

using System;
using System.Collections;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

using System.Web;

namespace MyPay.Models.Add

{

    public class AddCalculateServiceChargeAndCashback : CommonAdd

    {

        private string _MerchantUniqueId = string.Empty;
        public string MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }

        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        } 
        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //CashbackAmount
        private decimal _CashbackAmount = 0;
        public decimal CashbackAmount
        {
            get { return _CashbackAmount; }
            set { _CashbackAmount = value; }
        }
        //PercentageServiceCharge
        private decimal _PercentageServiceCharge = 0;
        public decimal PercentageServiceCharge
        {
            get { return _PercentageServiceCharge; }
            set { _PercentageServiceCharge = value; }
        }
        //ServiceCharge
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        //Amount
        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        //RewardPoints
        private decimal _RewardPoints = 0;
        public decimal RewardPoints
        {
            get { return _RewardPoints; }
            set { _RewardPoints = value; }
        }
        //MPCoinsDebit
        private decimal _MPCoinsDebit = 0;
        public decimal MPCoinsDebit
        {
            get { return _MPCoinsDebit; }
            set { _MPCoinsDebit = value; }
        }
        //ServiceId
        private Int32 _ServiceId = 0;
        public Int32 ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

        //DiscountPercentage
        private decimal _DiscountPercentage = 0;
        public decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }

        //DiscountAmount
        private decimal _DiscountAmount = 0;
        public decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        //MerchantCommissionTotal
        private decimal _MerchantCommissionTotal = 0;
        public decimal MerchantCommissionTotal
        {
            get { return _MerchantCommissionTotal; }
            set { _MerchantCommissionTotal = value; }
        }
        //FixedCommissionMerchant
        private decimal _FixedCommissionMerchant = 0;
        public decimal FixedCommissionMerchant
        {
            get { return _FixedCommissionMerchant; }
            set { _FixedCommissionMerchant = value; }
        }
        //PercentageCommissionMerchant
        private decimal _PercentageCommissionMerchant = 0;
        public decimal PercentageCommissionMerchant
        {
            get { return _PercentageCommissionMerchant; }
            set { _PercentageCommissionMerchant = value; }
        }

        private decimal _MerchantContribution = 0;
        public decimal MerchantContribution
        {
            get { return _MerchantContribution; }
            set { _MerchantContribution = value; }
        }

        //ServiceChargeId
        private Int32 _ServiceChargeId = 0;
        public Int32 ServiceChargeId
        {
            get { return _ServiceChargeId; }
            set { _ServiceChargeId = value; }
        }

        //CashBackId
        private Int32 _CashBackId = 0;
        public Int32 CashBackId
        {
            get { return _CashBackId; }
            set { _CashBackId = value; }
        }

        bool DataRecieved = false;

        public bool CalculateServiceChargeAndCashbackMerchant()
        {
            DataTable dt = new DataTable();
            DataRecieved = false;
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                HT.Add("Amount", Amount);
                HT.Add("ServiceId", ServiceId);
                dt = obj.GetDataFromStoredProcedure(Common.Common.StoreProcedures.sp_CalculateServiceChargeAndCashbackMerchant_Get, HT);
                if (dt != null && dt.Rows.Count > 0)
                {

                    CashbackAmount = Convert.ToDecimal(dt.Rows[0]["CashbackAmount"].ToString());
                    MerchantUniqueId = dt.Rows[0]["MerchantUniqueId"].ToString();
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    CashbackAmount = Convert.ToDecimal(dt.Rows[0]["CashbackAmount"].ToString());
                    PercentageServiceCharge = Convert.ToDecimal(dt.Rows[0]["PercentageServiceCharge"].ToString());
                    ServiceCharge = Convert.ToDecimal(dt.Rows[0]["ServiceCharge"].ToString());
                    NetAmount = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());
                    RewardPoints = Convert.ToDecimal(dt.Rows[0]["RewardPoints"].ToString());
                    MPCoinsDebit = Convert.ToDecimal(dt.Rows[0]["MPCoinsDebit"].ToString());
                    ServiceId = Convert.ToInt32(dt.Rows[0]["ServiceId"].ToString());
                    DiscountAmount = Convert.ToDecimal(dt.Rows[0]["DiscountAmount"].ToString());
                    DiscountPercentage = Convert.ToDecimal(dt.Rows[0]["DiscountPercentage"].ToString());
                    MerchantCommissionTotal = Convert.ToDecimal(dt.Rows[0]["MerchantCommissionTotal"].ToString());
                    PercentageCommissionMerchant = Convert.ToDecimal(dt.Rows[0]["PercentageCommissionMerchant"].ToString());
                    MerchantContribution = Convert.ToDecimal(dt.Rows[0]["MerchantContribution"].ToString());
                    ServiceChargeId = Convert.ToInt32(dt.Rows[0]["ServiceChargeId"].ToString());
                    CashBackId = Convert.ToInt32(dt.Rows[0]["CashBackId"].ToString());
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

    }

}