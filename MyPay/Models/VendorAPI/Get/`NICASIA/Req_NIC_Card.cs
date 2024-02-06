using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get._NICASIA
{
    public class Req_NIC_Card
    {
        // access_key
        private string _access_key = string.Empty;
        public string access_key
        {
            get { return _access_key; }
            set { _access_key = value; }
        }

        // profile_id
        private string _profile_id = string.Empty;
        public string profile_id
        {
            get { return _profile_id; }
            set { _profile_id = value; }
        }

        // transaction_uuid
        private string _transaction_uuid = string.Empty;
        public string transaction_uuid
        {
            get { return _transaction_uuid; }
            set { _transaction_uuid = value; }
        }

        // auth_trans_ref_no
        private string _auth_trans_ref_no = string.Empty;
        public string auth_trans_ref_no
        {
            get { return _auth_trans_ref_no; }
            set { _auth_trans_ref_no = value; }
        }

        private string _signed_field_names = string.Empty;
        public string signed_field_names
        {
            get { return _signed_field_names; }
            set { _signed_field_names = value; }
        }

        private string _unsigned_field_names = string.Empty;
        public string unsigned_field_names
        {
            get { return _unsigned_field_names; }
            set { _unsigned_field_names = value; }
        }

        private string _signed_date_time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
        public string signed_date_time
        {
            get { return _signed_date_time; }
            set { _signed_date_time = value; }
        }

        private string _locale = "en";
        public string locale
        {
            get { return _locale; }
            set { _locale = value; }
        }

        private decimal _amount = 0;
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string _bill_to_forename = string.Empty;
        public string bill_to_forename
        {
            get { return _bill_to_forename; }
            set { _bill_to_forename = value; }
        }

        private string _bill_to_surname = string.Empty;
        public string bill_to_surname
        {
            get { return _bill_to_surname; }
            set { _bill_to_surname = value; }
        }

        private string _bill_to_email = string.Empty;
        public string bill_to_email
        {
            get { return _bill_to_email; }
            set { _bill_to_email = value; }
        }

        private string _bill_to_phone = string.Empty;
        public string bill_to_phone
        {
            get { return _bill_to_phone; }
            set { _bill_to_phone = value; }
        }

        private string _bill_to_address_line1 = string.Empty;
        public string bill_to_address_line1
        {
            get { return _bill_to_address_line1; }
            set { _bill_to_address_line1 = value; }
        }

        private string _bill_to_address_city = string.Empty;
        public string bill_to_address_city
        {
            get { return _bill_to_address_city; }
            set { _bill_to_address_city = value; }
        }

        private string _bill_to_address_state = string.Empty;
        public string bill_to_address_state
        {
            get { return _bill_to_address_state; }
            set { _bill_to_address_state = value; }
        }

        private string _bill_to_address_country = string.Empty;
        public string bill_to_address_country
        {
            get { return _bill_to_address_country; }
            set { _bill_to_address_country = value; }
        }

        private string _bill_to_address_postal_code = string.Empty;
        public string bill_to_address_postal_code
        {
            get { return _bill_to_address_postal_code; }
            set { _bill_to_address_postal_code = value; }
        }

        private string _transaction_type = string.Empty;
        public string transaction_type
        {
            get { return _transaction_type; }
            set { _transaction_type = value; }
        }

        private string _reference_number = string.Empty;
        public string reference_number
        {
            get { return _reference_number; }
            set { _reference_number = value; }
        }

        private string _currency = string.Empty;
        public string currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        private string _payment_method = string.Empty;
        public string payment_method
        {
            get { return _payment_method; }
            set { _payment_method = value; }
        }

        private string _signature = string.Empty;
        public string signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        private string _card_type = string.Empty;
        public string card_type
        {
            get { return _card_type; }
            set { _card_type = value; }
        }

        private string _card_number = string.Empty;
        public string card_number
        {
            get { return _card_number; }
            set { _card_number = value; }
        }

        private string _card_expiry_date = string.Empty;
        public string card_expiry_date
        {
            get { return _card_expiry_date; }
            set { _card_expiry_date = value; }
        }
    }
}