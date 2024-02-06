using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetCountry
    {
        //Id
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //CountryCode
        private string _CountryCode = string.Empty;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        //CountryName
        private string _CountryName = string.Empty;
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

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

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        //SearchCodes
        private string _SearchCodes = string.Empty;
        public string SearchCodes
        {
            get { return _SearchCodes; }
            set { _SearchCodes = value; }
        }

    }
}