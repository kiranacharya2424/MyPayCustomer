using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Dishome
{
    public class Res_Vendor_API_Dishome_Lookup_Requests : CommonResponse
    {

        // dishome_data
        private Dishome_Data _dishome_data = new Dishome_Data();
        public Dishome_Data dishome_data
        {

            get { return _dishome_data; }

            set { _dishome_data = value; }
        }
    }
    public class Dishome_Data
    {
        // name
        private string _quantity = String.Empty;
        public string Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        private string _customer_address = String.Empty;
        public string Customer_address
        {
            get { return _customer_address; }
            set { _customer_address = value; }
        }
        private string _expiry_date = String.Empty;
        public string Expiry_date
        {
            get { return _expiry_date; }
            set { _expiry_date = value; }
        }
        private string _customer_name = String.Empty;
        public string Customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }
        private string _customer_id = String.Empty;
        public string Customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        private string _customer_type = String.Empty;
        public string Customer_type
        {
            get { return _customer_type; }
            set { _customer_type = value; }
        }
        private string _package = String.Empty;
        public string Package
        {
            get { return _package; }
            set { _package = value; }
        }
        private string _customer_status = String.Empty;
        public string Customer_status
        {
            get { return _customer_status; }
            set { _customer_status = value; }
        }
        private string _mobile_no = String.Empty;
        public string Mobile_no
        {
            get { return _mobile_no; }
            set { _mobile_no = value; }
        }



    }
}