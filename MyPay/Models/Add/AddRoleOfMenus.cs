using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddRoleOfMenus:CommonAdd
    {
        #region "Properties"

        //RoleId
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //MenuId
        private int _MenuId = 0;
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }

        //ParentId
        private int _ParentId = 0;
        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        #endregion

      
    }
}