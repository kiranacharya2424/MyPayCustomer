using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_TV: WebCommonResponse
    {
        // Dishome_Lookup_Data
        private Dishome_Lookup_Data _data_dishhome = new Dishome_Lookup_Data();
        public Dishome_Lookup_Data data_dishhome
        {

            get { return _data_dishhome; }

            set { _data_dishhome = value; }
        }
        // SimTV_Lookup_Data
        private SIMTV_Lookup_Data _data_simtv = new SIMTV_Lookup_Data();
        public SIMTV_Lookup_Data data_simtv
        {

            get { return _data_simtv; }

            set { _data_simtv = value; }
        }

        // MeroTV_Lookup_Data
        private List<MEROTV_Lookup_Data_OfferList> _data_merotv = new List<MEROTV_Lookup_Data_OfferList>();
        public List<MEROTV_Lookup_Data_OfferList> data_merotv
        {

            get { return _data_merotv; }

            set { _data_merotv = value; }
        }

        // first_name
        private string _first_name = string.Empty;
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

        // last_name
        private string _last_name = string.Empty;
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        // code
        private string _code = string.Empty;
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        // wallet_balance
        private string _wallet_balance = string.Empty;
        public string wallet_balance
        {
            get { return _wallet_balance; }
            set { _wallet_balance = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }



        // MaxTV_Lookup_Data
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        
        private string _cid_stb_smartcard = string.Empty;
        public string cid_stb_smartcard
        {
            get { return _cid_stb_smartcard; }
            set { _cid_stb_smartcard = value; }
        }
        
        private string _stb_no = string.Empty;
        public string stb_no
        {
            get { return _stb_no; }
            set { _stb_no = value; }
        }
        
        private string _smartcard_no = string.Empty;
        public string smartcard_no
        {
            get { return _smartcard_no; }
            set { _smartcard_no = value; }
        }
        
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // PrabhuTV_Lookup_Data

        private string _stb_count = string.Empty;
        public string stb_count
        {
            get { return _stb_count; }
            set { _stb_count = value; }
        }
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        private string _expiry_date = string.Empty;
        public string expiry_date
        {
            get { return _expiry_date; }
            set { _expiry_date = value; }
        }
        private string _product_name = string.Empty;
        public string product_name
        {
            get { return _product_name; }
            set {_product_name = value; }
        }
    }
}