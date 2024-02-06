using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddCountry
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
        //Region
        private string _Region = string.Empty;
        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }
        //IsActive
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        //LangCode
        private string _LangCode = string.Empty;
        public string LangCode
        {
            get { return _LangCode; }
            set { _LangCode = value; }
        }
        //Zone
        private string _Zone = string.Empty;
        public string Zone
        {
            get { return _Zone; }
            set { _Zone = value; }
        }
        //GoldBv
        private double _GoldBv = 0;
        public double GoldBv
        {
            get { return _GoldBv; }
            set { _GoldBv = value; }
        }
        //GoldBv
        private double _BronzeBv = 0;
        public double BronzeBv
        {
            get { return _BronzeBv; }
            set { _BronzeBv = value; }
        }

        //GoldBv
        private double _PromoMco = 0;
        public double PromoMco
        {
            get { return _PromoMco; }
            set { _PromoMco = value; }
        }
        //Currency
        private string _Currency = string.Empty;
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }
        //CurrencyCode
        private string _CurrencyCode = string.Empty;
        public string CurrencyCode
        {
            get { return _CurrencyCode; }
            set { _CurrencyCode = value; }
        }
        //CurrencyCode
        private string _CurrencySymbol = string.Empty;
        public string CurrencySymbol
        {
            get { return _CurrencySymbol; }
            set { _CurrencySymbol = value; }
        }
        //IsDefault
        private bool _IsDefault = false;
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }
    }
}