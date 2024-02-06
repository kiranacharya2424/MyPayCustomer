using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetBankList
    {
        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }

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
        }private string _flag = string.Empty;
        public string flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
    }
}