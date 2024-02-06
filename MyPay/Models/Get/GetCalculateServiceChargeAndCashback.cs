using MyPay.Models.Common;

using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;

namespace MyPay.Models.Add

{

    public class GetCalculateServiceChargeAndCashback 

    {


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
        //_ServiceId
        private Int32 _ServiceId = 0;
        public Int32 ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

    }

}