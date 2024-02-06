using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_RoleAssign
    {
        private List<AddRoleOfMenus> _objData = new List<AddRoleOfMenus>();
        public List<AddRoleOfMenus> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}