using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetMenu:CommonGet
    {
        #region "Properties"

        //MenuName
        private string _MenuName = string.Empty;
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }

        //RoleId
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //CheckParentId
        private int _CheckParentId = -1;
        public int CheckParentId
        {
            get { return _CheckParentId; }
            set { _CheckParentId = value; }
        }

        //CheckInnerURL
        private int _CheckInnerURL = 2;
        public int CheckInnerURL
        {
            get { return _CheckInnerURL; }
            set { _CheckInnerURL = value; }
        }

        //CheckAdminMenu
        private int _CheckAdminMenu = 2;
        public int CheckAdminMenu
        {
            get { return _CheckAdminMenu; }
            set { _CheckAdminMenu = value; }
        }
        #endregion
    }
}