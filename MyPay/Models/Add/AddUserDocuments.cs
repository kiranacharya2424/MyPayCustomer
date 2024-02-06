using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddUserDocuments:CommonAdd
    {

        public enum ReasonTypes
        {
            Pending = 0,
            Rejected = 1,
            Accepted = 2,
            Updated = 3
        }

        #region "Properties"

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //FirstName
        private string _event = string.Empty;
        public string @event
        {
            get { return _event; }
            set { _event = value; }
        }

        //refrence
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        //amlrefrence
        private string _amlreference = string.Empty;
        public string amlreference
        {
            get { return _amlreference; }
            set { _amlreference = value; }
        }

        //additionalproof
        private string _additionalproof = string.Empty;
        public string additionalproof
        {
            get { return _additionalproof; }
            set { _additionalproof = value; }
        }

        //addressproof
        private string _addressproof = string.Empty;
        public string addressproof
        {
            get { return _addressproof; }
            set { _addressproof = value; }
        }

        //email
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        //country
        private string _country = string.Empty;
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        //document_number
        private string _document_number = string.Empty;
        public string document_number
        {
            get { return _document_number; }
            set { _document_number = value; }
        }

        //issue_date
        private DateTime _issue_date = DateTime.UtcNow;
        public DateTime issue_date
        {
            get { return _issue_date; }
            set { _issue_date = value; }
        }

        //expiry_date
        private DateTime _expiry_date = DateTime.UtcNow;
        public DateTime expiry_date
        {
            get { return _expiry_date; }
            set { _expiry_date = value; }
        }

        //dob
        private string _dob = string.Empty;
        public string dob
        {
            get { return _dob; }
            set { _dob = value; }
        }

        //first_name
        private string _first_name = string.Empty;
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }


        //middle_name
        private string _middle_name = string.Empty;
        public string middle_name
        {
            get { return _middle_name; }
            set { _middle_name = value; }
        }

        //last_name
        private string _last_name = string.Empty;
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

        //Reason
        private string _Reason = string.Empty;
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }

        //selected_type
        private int _selected_type = 0;
        public int selected_type
        {
            get { return _selected_type; }
            set { _selected_type = value; }
        }

        //document
        private int _document = 0;
        public int document
        {
            get { return _document; }
            set { _document = value; }
        }

        //document_visibility
        private int _document_visibility = 0;
        public int document_visibility
        {
            get { return _document_visibility; }
            set { _document_visibility = value; }
        }

        //document_must_not_be_expired
        private int _document_must_not_be_expired = 0;
        public int document_must_not_be_expired
        {
            get { return _document_must_not_be_expired; }
            set { _document_must_not_be_expired = value; }
        }

        //CountryCode
        private string _document_proof = string.Empty;
        public string document_proof
        {
            get { return _document_proof; }
            set { _document_proof = value; }
        }

        //Take
        private int _ReasonType = 0;
        public int ReasonType
        {
            get { return _ReasonType; }
            set { _ReasonType = value; }
        }      

        #endregion
    }
}