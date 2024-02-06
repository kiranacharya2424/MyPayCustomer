using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.MEROTV
{
    public class Res_Vendor_API_MEROTV_Packages_Requests : CommonResponse
    {
       

        public List<MeroTVPackage> packages { get; set; }
        public int session_id { get; set; }
        public bool status { get; set; }
        //// first_name
        //private string _first_name = string.Empty;
        //public string first_name
        //{
        //    get { return _first_name; }
        //    set { _first_name = value; }
        //}

        //// last_name
        //private string _last_name = string.Empty;
        //public string last_name
        //{
        //    get { return _last_name; }
        //    set { _last_name = value; }
        //}

        //// Mero TV wallet_balance
        //private string _MERO_TV_Wallet_balance = string.Empty;
        //public string MERO_TV_Wallet_balance
        //{
        //    get { return _MERO_TV_Wallet_balance; }
        //    set { _MERO_TV_Wallet_balance = value; }
        //}
        //// code
        //private string _code = string.Empty;
        //public string code
        //{
        //    get { return _code; }
        //    set { _code = value; }
        //}
        //// error
        //private string _error = string.Empty;
        //public string error
        //{
        //    get { return _error; }
        //    set { _error = value; }
        //}
        //// details
        //private string _details = string.Empty;
        //public string details
        //{
        //    get { return _details; }
        //    set { _details = value; }
        //}
        //// session_id
        //private string _session_id = string.Empty;
        //public string session_id
        //{
        //    get { return _session_id; }
        //    set { _session_id = value; }
        //}
        //// MEROTV_Lookup-data -- OfferList
        //private List<MEROTV_Lookup_Data_OfferList> _MEROTV_OfferLit = new List<MEROTV_Lookup_Data_OfferList>();
        //public List<MEROTV_Lookup_Data_OfferList> MEROTV_OfferLit
        //{

        //    get { return _MEROTV_OfferLit; }

        //    set { _MEROTV_OfferLit = value; }
        //}
    } 
}