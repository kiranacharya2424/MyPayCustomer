using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Marque
{
    public class Res_Marque : CommonResponse
    {

        //AddOccupation
        private List<AddMarque> _data = new List<AddMarque>();
        public List<AddMarque> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}