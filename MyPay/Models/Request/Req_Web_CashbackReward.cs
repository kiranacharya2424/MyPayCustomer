using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_CashbackReward
    {

        private Int32 _Id = 0;
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _RegistrationCommission = String.Empty;
        public string RegistrationCommission
        {
            get { return _RegistrationCommission; }
            set { _RegistrationCommission = value; }
        }

        private string _KYCCommission = String.Empty;
        public string  KYCCommission
        {
            get { return _KYCCommission; }
            set { _KYCCommission = value; }
        }

        private string _TransactionCommission = String.Empty;
        public string TransactionCommission
        {
            get { return _TransactionCommission; }
            set { _TransactionCommission = value; }
        }
        private string _RegistrationRewardPoint = String.Empty;
        public string RegistrationRewardPoint
        {
            get { return _RegistrationRewardPoint; }
            set { _RegistrationRewardPoint = value; }
        }

        private string _KYCRewardPoint = String.Empty;
        public string KYCRewardPoint
        {
            get { return _KYCRewardPoint; }
            set { _KYCRewardPoint = value; }
        }

        private string _TransactionRewardPoint = String.Empty;
        public string TransactionRewardPoint
        {
            get { return _TransactionRewardPoint; }
            set { _TransactionRewardPoint = value; }
        }

    }

}