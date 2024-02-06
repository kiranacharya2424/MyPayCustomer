using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Antivirus.DataPack
{
    public class Res_Vendor_API_DataPack_Lookup_Requests : CommonResponse
    {
        // detail
        private DatapackDetails _Detail = new DatapackDetails();
        public DatapackDetails Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }
    }
    public class DatapackDetails
    {
        // DataPack_data
        private List<DataPack_Data> _Packages = new List<DataPack_Data>();
        public List<DataPack_Data> Packages
        {

            get { return _Packages; }

            set { _Packages = value; }
        }
    }
    public class DataPack_Data
    {
        // priority_no
        private string _priority_no = string.Empty;
        public string PriorityNo
        {
            get { return _priority_no; }
            set { _priority_no = value; }
        }
        // product_name
        private string _product_name = string.Empty;
        public string ProductName
        {
            get { return _product_name; }
            set { _product_name = value; }
        }

        // _amount
        private string _amount = string.Empty;
        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // short_detailt
        private string _short_detailt = string.Empty;
        public string ShortDetails
        {
            get { return _short_detailt; }
            set { _short_detailt = value; }
        }

        // product_codet
        private string _product_codet = string.Empty;
        public string ProductCode
        {
            get { return _product_codet; }
            set { _product_codet = value; }
        }

        // descriptiont
        private string _descriptiont = string.Empty;
        public string Description
        {
            get { return _descriptiont; }
            set { _descriptiont = value; }
        }

        // prodcut_type
        private string _product_type = string.Empty;
        public string ProductType
        {
            get { return _product_type; }
            set { _product_type = value; }
        }

        // PackageID
        private string _PackageID = string.Empty;
        public string PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }
        // validity
        private string _validity = string.Empty;
        public string Validity
        {
            get { return _validity; }
            set { _validity = value; }
        }







    }
}