using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetAPIDealsAndOffers: CommonGet
    {
        #region "Properties"

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //CheckKycStatus
        private int _CheckKycStatus = -1;
        public int CheckKycStatus
        {
            get { return _CheckKycStatus; }
            set { _CheckKycStatus = value; }
        }

        //CheckGenderStatus
        private int _CheckGenderStatus = -1;
        public int CheckGenderStatus
        {
            get { return _CheckGenderStatus; }
            set { _CheckGenderStatus = value; }
        }
        #endregion
    }
}