using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Alloy_CorporateUser
    {
        private string _phonenumber = string.Empty;
        public string phoneNumber
        {
            get => _phonenumber;
            set => _phonenumber = value;
        }
        private string _email = string.Empty;
        public string email
        {
            get => _email;
            set => _email = value;
        }
        private Corporate_Company _company = new Corporate_Company();
        public Corporate_Company company
        {
            get => _company;
            set => _company = value;
        }
    }
    public class Corporate_BusinessAddress
    {
        private string _addressline1 = string.Empty;
        public string addressLine1
        {
            get => _addressline1;
            set => _addressline1 = value;
        }
        private string _addressline2 = string.Empty;
        public string addressLine2
        {
            get => _addressline2;
            set => _addressline2 = value;
        }
        private string _city = string.Empty;
        public string city
        {
            get => _city;
            set => _city = value;
        }
        private string _state = string.Empty;
        public string state
        {
            get => _state;
            set => _state = value;
        }
        private string _country = string.Empty;
        public string country
        {
            get => _country;
            set => _country = value;
        }
        private string _postalcode = string.Empty;
        public string postalCode
        {
            get => _postalcode;
            set => _postalcode = value;
        }
    }

    public class Corporate_Company
    {
        private string _type = string.Empty;
        public string type
        {
            get => _type;
            set => _type = value;
        }
        private string _name = string.Empty;
        public string name
        {
            get => _name;
            set => _name = value;
        }
        private string _registrationnumber = string.Empty;
        public string registrationNumber
        {
            get => _registrationnumber;
            set => _registrationnumber = value;
        }
        private string _registrationcountry = string.Empty;
        public string registrationCountry
        {
            get => _registrationcountry;
            set => _registrationcountry = value;
        }
        private string _houseid = string.Empty;
        public string houseId
        {
            get => _houseid;
            set => _houseid = value;
        }
        private Corporate_BusinessAddress _businessAddress = new Corporate_BusinessAddress();
        public Corporate_BusinessAddress businessAddress
        {
            get => _businessAddress;
            set => _businessAddress = value;
        }
    }
}