using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.ClassiTech
{
    public class Res_Vendor_API_Classitech_Lookup_Requests : CommonResponse
    {
        // session_id
        private string _SessionID = string.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        // Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        // ClassiTech_data
        private List<ClassiTech_Data> _Available_Plans = new List<ClassiTech_Data>();
        public  List<ClassiTech_Data> Available_Plans
        {

            get { return _Available_Plans; }

            set { _Available_Plans = value; }
        }
    }
    public class ClassiTech_Data
    {
        // name
        private string _Package = string.Empty;
        public string Package
        {
            get { return _Package; }
            set { _Package = value; }
        }
        private string _Duration = string.Empty;
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}