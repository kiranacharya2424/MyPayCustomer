using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Alloy_EndUser
    {
        //private Alloy_EndUser_EnduserMeta _enduser_meta = new Alloy_EndUser_EnduserMeta();
        //public Alloy_EndUser_EnduserMeta enduser_meta
        //{
        //    get { return _enduser_meta; }
        //    set { _enduser_meta = value; }
        //}
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        private Alloy_EndUser_Person _person = new Alloy_EndUser_Person();
        public Alloy_EndUser_Person person
        {
            get { return _person; }
            set { _person = value; }
        }
    }

    public class Alloy_EndUser_Address
    {
        private string _address_city = string.Empty;

        public string address_city
        {
            get => _address_city;
            set => _address_city = value;
        }
        private string _address_iso_country = string.Empty;
        public string address_iso_country
        {
            get => _address_iso_country;
            set => _address_iso_country = value;
        }
        private string _address_number = string.Empty;
        public string address_number
        {
            get => _address_number;
            set => _address_number = value;
        }
        private string _address_postal_code = string.Empty;
        public string address_postal_code
        {
            get => _address_postal_code;
            set => _address_postal_code = value;
        }
        private string _address_refinement = string.Empty;
        public string address_refinement
        {
            get => _address_refinement;
            set => _address_refinement = value;
        }
        private string _address_region = string.Empty;
        public string address_region
        {
            get => _address_region;
            set => _address_region = value;
        }
        private string _address_street = string.Empty;
        public string address_street
        {
            get => _address_street;
            set => _address_street = value;
        }
    }

    public class Alloy_EndUser_EnduserMeta
    {

    }

    public class Alloy_EndUser_Person
    {
        private Alloy_EndUser_Address _address = new Alloy_EndUser_Address();
        public Alloy_EndUser_Address address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _first_name = string.Empty;
        public string first_name
        {
            get => _first_name;
            set => _first_name = value;
        }
        private string _last_name = string.Empty;
        public string last_name
        {
            get => _last_name;
            set => _last_name = value;
        }
        private string _name = string.Empty;
        public string name
        {
            get => _name;
            set => _name = value;
        }
        private string _telephone = string.Empty;
        public string telephone
        {
            get => _telephone;
            set => _telephone = value;
        }
        private string _nationality = string.Empty;
        public string nationality
        {
            get => _nationality;
            set => _nationality = value;
        }
        private string _date_of_birth = string.Empty;
        public string date_of_birth
        {
            get => _date_of_birth;
            set => _date_of_birth = value;
        }

        private Alloy_EndUser_Questionnaire _questionnaire = new Alloy_EndUser_Questionnaire();
        public Alloy_EndUser_Questionnaire questionnaire
        {
            get { return _questionnaire; }
            set { _questionnaire = value; }
        }

    }

    public class Alloy_EndUser_Questionnaire
    {
        private string _occupationType = string.Empty;
        public string occupationtype
        {
            get => _occupationType;
            set => _occupationType = value;
        }
        private string _employmentStatus = string.Empty;
        public string employmentstatus
        {
            get => _employmentStatus;
            set => _employmentStatus = value;
        }
        private List<string> _sourceOfIncome=new List<string>();
        public List<string> sourceofincome
        {
            get => _sourceOfIncome;
            set => _sourceOfIncome = value;
        }
        private string _incomeBand = string.Empty;
        public string incomeband
        {
            get => _incomeBand;
            set => _incomeBand = value;
        }
        private List<string> _accountDesignation = new List<string>();
        public List<string> accountdesignation
        {
            get => _accountDesignation;
            set => _accountDesignation = value;
        }
    }


}