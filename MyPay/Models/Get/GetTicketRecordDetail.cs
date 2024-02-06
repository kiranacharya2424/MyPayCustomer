using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetTicketRecordDetail
    {
        //CreatedBy
        private Int64 _CreatedBy = 0;
        public Int64 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
    }
}