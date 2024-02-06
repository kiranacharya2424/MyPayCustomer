using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_Roles
    {
        private List<AddRole> _objData = new List<AddRole>();
        public List<AddRole> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}