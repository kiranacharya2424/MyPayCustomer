using DocumentFormat.OpenXml.Wordprocessing;
using MyPay.Models.Common;

using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;

namespace MyPay.Models.Add

{

    public class AddRemittanceCalculateServiceCharge : CommonAdd

    {

        private Int64 _SourceCurrencyId = 0;
        public Int64 SourceCurrencyId
        {
            get { return _SourceCurrencyId; }
            set { _SourceCurrencyId = value; }
        }
        private Int64 _DestinationCurrencyId = 0;
        public Int64 DestinationCurrencyId
        {
            get { return _DestinationCurrencyId; }
            set { _DestinationCurrencyId = value; }
        }


        private string _SourceCurrency = string.Empty;
        public string SourceCurrency
        {
            get { return _SourceCurrency; }
            set { _SourceCurrency = value; }
        }

        private string _DestinationCurrency = string.Empty;
        public string DestinationCurrency
        {
            get { return _DestinationCurrency; }
            set { _DestinationCurrency = value; }
        }

        private Int64 _ServiceChargeId = 0;
        public Int64 ServiceChargeId
        {
            get { return _ServiceChargeId; }
            set { _ServiceChargeId = value; }
        }

        private decimal _ServiceChargeFixed = 0;
        public decimal ServiceChargeFixed
        {
            get { return _ServiceChargeFixed; }
            set { _ServiceChargeFixed = value; }
        }
        private decimal _MinimumAllowedAmountSC = 0;
        public decimal MinimumAllowedAmountSC
        {
            get { return _MinimumAllowedAmountSC; }
            set { _MinimumAllowedAmountSC = value; }
        }
        private decimal _MaximumAllowedAmountSC = 0;
        public decimal MaximumAllowedAmountSC
        {
            get { return _MaximumAllowedAmountSC; }
            set { _MaximumAllowedAmountSC = value; }
        }
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private decimal _ConversionRate = 0;
        public decimal ConversionRate
        {
            get { return _ConversionRate; }
            set { _ConversionRate = value; }
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
        //NetAmount
        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        public bool DataRecieved = false;

        public bool GetRecord()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRecieved = false;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = new System.Collections.Hashtable();
                HT.Add("SourceCurrencyId", SourceCurrencyId);
                HT.Add("DestinationCurrencyId", DestinationCurrencyId);
                HT.Add("SourceCurrency", SourceCurrency);
                HT.Add("DestinationCurrency", DestinationCurrency);
                HT.Add("Amount", Amount);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceCalculateServiceCharge_Get", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SourceCurrencyId = Convert.ToInt64(dt.Rows[0]["SourceCurrencyId"].ToString());
                    DestinationCurrencyId = Convert.ToInt64(dt.Rows[0]["DestinationCurrencyId"].ToString());
                    ServiceChargeId = Convert.ToInt64(dt.Rows[0]["ServiceChargeId"].ToString());
                    MinimumAllowedAmountSC = Convert.ToDecimal(dt.Rows[0]["MinimumAllowedAmountSC"].ToString());
                    MaximumAllowedAmountSC = Convert.ToDecimal(dt.Rows[0]["MaximumAllowedAmountSC"].ToString());
                    Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                    ServiceChargeFixed = Convert.ToDecimal(dt.Rows[0]["ServiceChargeFixed"].ToString());
                    ServiceCharge = Convert.ToDecimal(dt.Rows[0]["ServiceCharge"].ToString());
                    PercentageServiceCharge = Convert.ToDecimal(dt.Rows[0]["PercentageServiceCharge"].ToString());
                    NetAmount = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());
                    ConversionRate = Convert.ToDecimal(dt.Rows[0]["ConversionRate"].ToString());
                    DataRecieved = true;
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