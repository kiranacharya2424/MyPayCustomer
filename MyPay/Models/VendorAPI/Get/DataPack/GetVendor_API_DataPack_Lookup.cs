using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_DataPack_Lookup : CommonGet
    {

        // error_code
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        // details
        //private string _details = string.Empty;
        //public string details
        //{
        //    get { return _details; }
        //    set { _details = value; }
        //}
        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // session_id
        private string _session_id = string.Empty;
        public string session_id
        {
            get { return _session_id; }
            set { _session_id = value; }
        }
        // customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        // _customer_id
        private string _customer_id = string.Empty;
        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        // detail
        private datapack_details _detail = new datapack_details();
        public datapack_details detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
       
    }

    public class datapack_details
    { 
        // DataPack_data
        private List<datapack_packages> _packages = new List<datapack_packages>();
        public List<datapack_packages> packages
        {

            get { return _packages; }

            set { _packages = value; }
        }
    }
    public class datapack_packages
    {
        // priority_no
        private string _priority_no = string.Empty;
        public string priority_no
        {
            get { return _priority_no; }
            set { _priority_no = value; }
        }


        // product_name
        private string _product_name = string.Empty;
        public string product_name
        {
            get { return _product_name; }
            set { _product_name = value; }
        }

        // _amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // short_detailt
        private string _short_detailt = string.Empty;
        public string short_detail
        {
            get { return _short_detailt; }
            set { _short_detailt = value; }
        }

        // product_codet
        private string _product_codet = string.Empty;
        public string product_code
        {
            get { return _product_codet; }
            set { _product_codet = value; }
        }

        // descriptiont
        private string _descriptiont = string.Empty;
        public string description
        {
            get { return _descriptiont; }
            set { _descriptiont = value; }
        }

        // product_type
        private string _product_type = string.Empty;
        public string product_type
        {
            get { return _product_type; }
            set { _product_type = value; }
        }

        // prodcut_type
        private string _prodcut_type = string.Empty;
        public string prodcut_type
        {
            get { return _prodcut_type; }
            set { _prodcut_type = value; }
        }


        // package_id
        private string _package_id = string.Empty;
        public string package_id
        {
            get { return _package_id; }
            set { _package_id = value; }
        }

        // validity
        private string _validity = string.Empty;
        public string validity
        {
            get { return _validity; }
            set { _validity = value; }
        }







    }

}