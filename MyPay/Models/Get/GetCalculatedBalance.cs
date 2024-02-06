using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetCalculatedBalance
    {
        //date
        private DateTime _date = DateTime.UtcNow;
        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}