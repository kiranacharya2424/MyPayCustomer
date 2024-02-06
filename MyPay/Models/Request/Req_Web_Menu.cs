using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_Menu
    {
        private List<AddMenu> _objData = new List<AddMenu>();
        public List<AddMenu> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}