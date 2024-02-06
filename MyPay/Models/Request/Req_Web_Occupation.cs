using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_Occupation
    {
        private string _CategoryName = String.Empty;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private List<AddOccupation> _objData = new List<AddOccupation>();
        public List<AddOccupation> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}