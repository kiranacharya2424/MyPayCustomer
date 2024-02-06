using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMenu:CommonAdd
    {
        #region "Properties"

        //MenuName
        private string _MenuName = string.Empty;
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }

        //Url
        private string _Url = string.Empty;
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        //RoleId
        private int _RoleId = 0;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }

        //ParentId
        private int _ParentId = 0;
        public int ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        //Icon
        private string _Icon = string.Empty;
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        //ParentName
        private string _ParentName = string.Empty;
        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }

        //IsApprove
        private bool _IsApprove = false;
        public bool IsApprove
        {
            get { return _IsApprove; }
            set { _IsApprove = value; }
        }

        //IsInnerURL
        private bool _IsInnerURL = false;
        public bool IsInnerURL
        {
            get { return _IsInnerURL; }
            set { _IsInnerURL = value; }
        }
        //SortingId
        private Int64 _SortingId = 0;
        public Int64 SortingId
        {
            get { return _SortingId; }
            set { _SortingId = value; }
        }

        //IsAdminMenu
        private bool _IsAdminMenu = false;
        public bool IsAdminMenu
        {
            get { return _IsAdminMenu; }
            set { _IsAdminMenu = value; }
        }

        #endregion
    }
}