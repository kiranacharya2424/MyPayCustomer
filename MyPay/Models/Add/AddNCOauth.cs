using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddNCOauth
    {
        //grant_type 
        private string _grant_type = "password";
        public string grant_type
        {
            get { return _grant_type; }
            set { _grant_type = value; }
        }

        //username 
        private string _username = RepNCHL.withdrawusername;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        //password 
        private string _password = RepNCHL.withdrawpassword;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}