
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyPay.API.Models.Request.CableCar
{
    public class ReqVendorAPiTicketTypesGet:CommonGet
    {
        // Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}