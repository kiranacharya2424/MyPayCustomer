using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetOfferBanners:CommonGet
    {
        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //CheckFromDate
        private string _CheckFromDate = string.Empty;
        public string CheckFromDate
        {
            get { return _CheckFromDate; }
            set { _CheckFromDate = value; }
        }

        //CheckToDate
        private string _CheckToDate = string.Empty;
        public string CheckToDate
        {
            get { return _CheckToDate; }
            set { _CheckToDate = value; }
        }

        //Running
        private string _Running = string.Empty;
        public string Running
        {
            get { return _Running; }
            set { _Running = value; }
        }

        //Scheduled
        private string _Scheduled = string.Empty;
        public string Scheduled
        {
            get { return _Scheduled; }
            set { _Scheduled = value; }
        }

        //Expired
        private string _Expired = string.Empty;
        public string Expired
        {
            get { return _Expired; }
            set { _Expired = value; }
        }

        //Priority
        private int _Priority = 0;
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        //IsHome
        private int _IsHome = 0;
        public int IsHome
        {
            get { return _IsHome; }
            set { _IsHome = value; }
        }
        private string _URL;
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
    }
}