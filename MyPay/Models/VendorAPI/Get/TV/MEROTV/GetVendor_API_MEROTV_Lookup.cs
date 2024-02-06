using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{

    public class GetVendor_API_MEROTV_Lookup : CommonGet
    {

        public MeroTVCustomer customer { get; set; }
        public List<MeroTVConnection> connection { get; set; }
        public int session_id { get; set; }
        public bool status { get; set; }

        public string Message;
    }

    public class MeroTVConnection
    {
        public string stb { get; set; }
        public string status { get; set; }
    }

    public class MeroTVCustomer
    {
        public string cuid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string status { get; set; }
    }
    //public class GetVendor_API_MEROTV_Lookup : CommonGet
    //{

    //    public Customer customer { get; set; }
    //    public List<Connection> connection { get; set; }
    //    public int session_id { get; set; }
    //    public bool status { get; set; }

    //    public class Connection
    //    {
    //        public string stb { get; set; }
    //        public string status { get; set; }
    //    }

    //    public class Customer
    //    {
    //        public string cuid { get; set; }
    //        public string first_name { get; set; }
    //        public string last_name { get; set; }
    //        public string status { get; set; }
    //    }

    //    //// first_name
    //    //private string _first_name = string.Empty;
    //    //public string first_name
    //    //{
    //    //    get { return _first_name; }
    //    //    set { _first_name = value; }
    //    //}

    //    //// last_name
    //    //private string _last_name = string.Empty;
    //    //public string last_name
    //    //{
    //    //    get { return _last_name; }
    //    //    set { _last_name = value; }
    //    //}

    //    //// wallet_balance
    //    //private string _wallet_balance = string.Empty;
    //    //public string wallet_balance
    //    //{
    //    //    get { return _wallet_balance; }
    //    //    set { _wallet_balance = value; }
    //    //}
    //    //// code
    //    //private string _code = string.Empty;
    //    //public string code
    //    //{
    //    //    get { return _code; }
    //    //    set { _code = value; }
    //    //}
    //    //// error
    //    //private string _error = string.Empty;
    //    //public string error
    //    //{
    //    //    get { return _error; }
    //    //    set { _error = value; }
    //    //}
    //    //// details
    //    //private string _details = string.Empty;
    //    //public string details
    //    //{
    //    //    get { return _details; }
    //    //    set { _details = value; }
    //    //}
    //    //// session_id
    //    //private string _session_id = string.Empty;
    //    //public string session_id
    //    //{
    //    //    get { return _session_id; }
    //    //    set { _session_id = value; }
    //    //}
    //    //// Message
    //    //private string _Message = string.Empty;
    //    //public string Message
    //    //{
    //    //    get { return _Message; }
    //    //    set { _Message = value; }
    //    //}
    //    //// status
    //    //private bool _status = false;
    //    //public bool status
    //    //{
    //    //    get { return _status; }
    //    //    set { _status = value; }
    //    //}
    //    //// MEROTV_Lookup_Data -- OfferList
    //    //private List<MEROTV_Lookup_Data_OfferList> _offer_list = new List<MEROTV_Lookup_Data_OfferList>();
    //    //public List<MEROTV_Lookup_Data_OfferList> offer_list
    //    //{

    //    //    get { return _offer_list; }

    //    //    set { _offer_list = value; }
    //    //}



    //}
    public class MEROTV_Lookup_Data_OfferList
    {
        // name
        private string _offer_name = string.Empty;
        public string offer_name
        {
            get { return _offer_name; }
            set { _offer_name = value; }
        }
        // MEROTV_Lookup_Data -- Package 
        private List<MEROTV_Lookup_Data_PackageList> _package_list = new List<MEROTV_Lookup_Data_PackageList>();
        public List<MEROTV_Lookup_Data_PackageList> package_list
        {

            get { return _package_list; }

            set { _package_list = value; }
        }

    }
    public class MEROTV_Lookup_Data_PackageList
    {
        // name
        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        // id
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        // price
        private string _price = string.Empty;
        public string price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}