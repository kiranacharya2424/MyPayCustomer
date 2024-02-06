using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddSettingsRewardPoints:CommonAdd
    {
        #region "Properties"

        //RegistrationRewardPoints
        private decimal _RegistrationRewardPoints = 0;
        public decimal RegistrationRewardPoints
        {
            get { return _RegistrationRewardPoints; }
            set { _RegistrationRewardPoints = value; }
        }

        //KYCRewardPoints
        private decimal _KYCRewardPoints = 0;
        public decimal KYCRewardPoints
        {
            get { return _KYCRewardPoints; }
            set { _KYCRewardPoints = value; }
        }

        //TransactionRewardPoints
        private decimal _TransactionRewardPoints = 0;
        public decimal TransactionRewardPoints
        {
            get { return _TransactionRewardPoints; }
            set { _TransactionRewardPoints = value; }
        }
        #endregion
    }
}