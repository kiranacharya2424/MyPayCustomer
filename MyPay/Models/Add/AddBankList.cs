using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddBankList
    {
        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //BANK_CD
        private string _BANK_CD = string.Empty;
        public string BANK_CD
        {
            get { return _BANK_CD; }
            set { _BANK_CD = value; }
        }
        private string _flag = string.Empty;
        public string flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        //BANK_NAME
        private string _BANK_NAME = string.Empty;
        public string BANK_NAME
        {
            get { return _BANK_NAME; }
            set { _BANK_NAME = value; }
        }

        //SHORTCODE
        private string _SHORTCODE = string.Empty;
        public string SHORTCODE
        {
            get { return _SHORTCODE; }
            set { _SHORTCODE = value; }
        }

        //BRANCH_CD
        private string _BRANCH_CD = string.Empty;
        public string BRANCH_CD
        {
            get { return _BRANCH_CD; }
            set { _BRANCH_CD = value; }
        }

        //BRANCH_NAME
        private string _BRANCH_NAME = string.Empty;
        public string BRANCH_NAME
        {
            get { return _BRANCH_NAME; }
            set { _BRANCH_NAME = value; }
        }

        //ICON_NAME
        private string _ICON_NAME = string.Empty;
        public string ICON_NAME
        {
            get { return _ICON_NAME; }
            set { _ICON_NAME = value; }
        }
    }
}